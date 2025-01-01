#include <exception>
#include <string>

#include "cuda_computation.hpp"
#include "cuda_runtime.h"
#include "device_launch_parameters.h"

__global__ void single_precision_vector_addition_kernel(
	const float* left_vector,
	const float* right_vector,
	float* result_vector)
{
	const unsigned int element_index = threadIdx.x;

	result_vector[element_index] = left_vector[element_index] + right_vector[element_index];
}

extern "C" __declspec(dllexport) computation_result single_precision_vector_addition(
	const float* left_vector,
	const float* right_vector,
	float* result_vector,
	const unsigned long vector_length)
{
	try
	{
		throw_on_cuda_error(cudaSetDevice(0), cuda_set_device_failed);
		throw_on_cuda_error(cudaDeviceReset(), cuda_device_reset_failed);

		const size_t buffer_size = vector_length * sizeof(float);

		const float_vector_in_device_memory left_vector_in_device_memory(vector_length);
		const float_vector_in_device_memory right_vector_in_device_memory(vector_length);
		const float_vector_in_device_memory result_vector_in_device_memory(vector_length);		

		throw_on_cuda_error(cudaMemcpy(left_vector_in_device_memory.device_pointer, left_vector, buffer_size, cudaMemcpyHostToDevice), cuda_memcpy_failed);
		throw_on_cuda_error(cudaMemcpy(right_vector_in_device_memory.device_pointer, right_vector, buffer_size, cudaMemcpyHostToDevice), cuda_memcpy_failed);

		single_precision_vector_addition_kernel<<<1, vector_length>>> (
			left_vector_in_device_memory.device_pointer, 
			right_vector_in_device_memory.device_pointer,
			result_vector_in_device_memory.device_pointer
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

__global__ void double_precision_vector_addition_kernel(
	const double* left_vector,
	const double* right_vector,
	double* result_vector)
{
	const unsigned int element_index = threadIdx.x;

	result_vector[element_index] = left_vector[element_index] + right_vector[element_index];
}

extern "C" __declspec(dllexport) computation_result double_precision_vector_addition(
	const double* left_vector,
	const double* right_vector,
	double* result_vector,
	const unsigned long vector_length)
{
	try
	{
		throw_on_cuda_error(cudaSetDevice(0), cuda_set_device_failed);
		throw_on_cuda_error(cudaDeviceReset(), cuda_device_reset_failed);

		const double_vector_in_device_memory left_vector_in_device_memory(vector_length);
		const double_vector_in_device_memory right_vector_in_device_memory(vector_length);
		const double_vector_in_device_memory result_vector_in_device_memory(vector_length);

		const size_t buffer_size = vector_length * sizeof(double);

		throw_on_cuda_error(cudaMemcpy(left_vector_in_device_memory.device_pointer, left_vector, buffer_size, cudaMemcpyHostToDevice), cuda_memcpy_failed);
		throw_on_cuda_error(cudaMemcpy(right_vector_in_device_memory.device_pointer, right_vector, buffer_size, cudaMemcpyHostToDevice), cuda_memcpy_failed);

		double_precision_vector_addition_kernel<<<1, vector_length>>> (
			left_vector_in_device_memory.device_pointer,
			right_vector_in_device_memory.device_pointer,
			result_vector_in_device_memory.device_pointer
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
