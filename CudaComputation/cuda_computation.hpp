#pragma once
#include <exception>

#include "cuda_runtime.h"

enum computation_result : uint8_t
{
	succeeded,
	cuda_set_device_failed,
	cuda_device_reset_failed,
	cuda_malloc_failed,
	cuda_memcpy_failed,
	cuda_kernel_failed,
	cuda_device_synchronize_failed
};

struct computation_failed_exception final : std::exception
{
	cudaError_t cuda_result;
	computation_result failure;

	explicit computation_failed_exception(const cudaError_t cuda_result, const computation_result failure) :
		cuda_result(cuda_result),
		failure(failure)
	{
	}
};

inline void throw_on_cuda_error(const cudaError_t cuda_result, const computation_result failure)
{
	if (cuda_result != cudaSuccess) {
		throw computation_failed_exception(cuda_result, failure);
	}
}

template<typename TRealNumber>
struct vector_in_device_memory final
{
	TRealNumber* device_pointer = nullptr;

	vector_in_device_memory(const vector_in_device_memory&) = default;
	//vector_in_device_memory(const vector_in_device_memory&&) = default;
	vector_in_device_memory& operator=(vector_in_device_memory&& other) = default;
	vector_in_device_memory& operator=(const vector_in_device_memory& other) = default;

	explicit vector_in_device_memory(const unsigned long vector_length)
	{
		throw_on_cuda_error(cudaMalloc(&device_pointer, vector_length * sizeof(TRealNumber)), cuda_malloc_failed);
	}

	~vector_in_device_memory()
	{
		cudaFree(device_pointer);
	}
};
