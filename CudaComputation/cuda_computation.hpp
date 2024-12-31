#pragma once
#include <exception>
#include <string>

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
	std::string error_string;
	computation_result failure;

	explicit computation_failed_exception(const cudaError_t cuda_result, const computation_result failure) :
		error_string(cudaGetErrorString(cuda_result)),
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

struct float_vector_in_device_memory final
{
	float* device_pointer = nullptr;

	float_vector_in_device_memory(const float_vector_in_device_memory&) = default;
	//float_vector_in_device_memory(const float_vector_in_device_memory&&) = default;
	float_vector_in_device_memory& operator=(float_vector_in_device_memory&& other) = default;
	float_vector_in_device_memory& operator=(const float_vector_in_device_memory& other) = default;

	explicit float_vector_in_device_memory(const unsigned long vector_length)
	{
		throw_on_cuda_error(cudaMalloc(&device_pointer, vector_length * sizeof(float)), cuda_malloc_failed);
	}

	~float_vector_in_device_memory()
	{
		cudaFree(device_pointer);
	}
};
