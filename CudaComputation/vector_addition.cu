#include <exception>
#include <iostream>

#include "cuda_computation.hpp"
#include "cuda_runtime.h"
#include "device_launch_parameters.h"

template<typename TRealNumber>
__global__ void vector_addition_kernel(
	const TRealNumber left_vector[],
	const TRealNumber right_vector[],
	TRealNumber* result_vector,
	const unsigned long vector_length)
{
	const auto i = blockIdx.x * blockDim.x;
	const auto element_index = i + threadIdx.x;

	if (element_index < vector_length) {
		result_vector[element_index] = left_vector[element_index] + right_vector[element_index];
	}
}

template<typename TRealNumber>
int vector_addition(
	const TRealNumber left_vector[],
	const TRealNumber right_vector[],
	TRealNumber* result_vector,
	const unsigned long vector_length)
{
	try
	{
		throw_on_cuda_error(cudaSetDevice(0), cuda_set_device_failed);
		throw_on_cuda_error(cudaDeviceReset(), cuda_device_reset_failed);

		const auto buffer_size = vector_length * sizeof(TRealNumber);

		const vector_in_device_memory<TRealNumber> left_vector_in_device_memory(vector_length);
		const vector_in_device_memory<TRealNumber> right_vector_in_device_memory(vector_length);
		const vector_in_device_memory<TRealNumber> result_vector_in_device_memory(vector_length);

		throw_on_cuda_error(cudaMemcpy(left_vector_in_device_memory.device_pointer, left_vector, buffer_size, cudaMemcpyHostToDevice), cuda_memcpy_failed);
		throw_on_cuda_error(cudaMemcpy(right_vector_in_device_memory.device_pointer, right_vector, buffer_size, cudaMemcpyHostToDevice), cuda_memcpy_failed);

		auto threads_per_block = 1024;
		auto blocks_per_grid = (vector_length + threads_per_block - 1) / threads_per_block;

		vector_addition_kernel<TRealNumber> << <blocks_per_grid, threads_per_block >> > (
			left_vector_in_device_memory.device_pointer,
			right_vector_in_device_memory.device_pointer,
			result_vector_in_device_memory.device_pointer,
			vector_length
			);

		throw_on_cuda_error(cudaGetLastError(), cuda_kernel_failed);
		throw_on_cuda_error(cudaDeviceSynchronize(), cuda_device_synchronize_failed);

		throw_on_cuda_error(cudaMemcpy(result_vector, result_vector_in_device_memory.device_pointer, buffer_size, cudaMemcpyDeviceToHost), cuda_memcpy_failed);

		return succeeded;
	}
	catch (const computation_failed_exception& exception)
	{
		return exception.failure;
	}
}

extern "C" __declspec(dllexport) int single_precision_vector_addition(
	const float* left_vector,
	const float* right_vector,
	float* result_vector,
	const unsigned long vector_length)
{
	return vector_addition<float>(left_vector, right_vector, result_vector, vector_length);
}

extern "C" __declspec(dllexport) int double_precision_vector_addition(
	const double* left_vector,
	const double* right_vector,
	double* result_vector,
	const unsigned long vector_length)
{
	return vector_addition<double>(left_vector, right_vector, result_vector, vector_length);
}