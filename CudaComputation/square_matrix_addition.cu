#include <exception>
#include <iostream>

#include "cuda_computation.hpp"
#include "cuda_runtime.h"
#include "device_launch_parameters.h"

template<typename TRealNumber>
__global__ void square_matrix_addition_kernel(
	TRealNumber left_matrix[],
	TRealNumber right_matrix[],
	TRealNumber result_matrix[],
	const unsigned long dimension_of_matrix)
{
	const auto column = threadIdx.x + blockDim.x * blockIdx.x;
	const auto row = threadIdx.y + blockDim.y * blockIdx.y;

	auto element_index = row * dimension_of_matrix + column;

	if (column < dimension_of_matrix && row < dimension_of_matrix) {
		result_matrix[element_index] = left_matrix[element_index] + right_matrix[element_index];
	}
}

template<typename TRealNumber>
int square_matrix_addition(
	const TRealNumber left_matrix[],
	const TRealNumber right_matrix[],
	TRealNumber result_matrix[],
	const unsigned long dimension_of_matrix)
{
	try
	{
		throw_on_cuda_error(cudaSetDevice(0), cuda_set_device_failed);
		throw_on_cuda_error(cudaDeviceReset(), cuda_device_reset_failed);

		const auto number_of_elements_in_matrix = dimension_of_matrix * dimension_of_matrix;
		const auto buffer_size = number_of_elements_in_matrix * sizeof(TRealNumber);

		const matrix_in_device_memory<TRealNumber> left_matrix_in_device_memory(dimension_of_matrix);
		const matrix_in_device_memory<TRealNumber> right_matrix_in_device_memory(dimension_of_matrix);
		const matrix_in_device_memory<TRealNumber> result_matrix_in_device_memory(dimension_of_matrix);

		throw_on_cuda_error(cudaMemcpy(left_matrix_in_device_memory.device_pointer, left_matrix, buffer_size, cudaMemcpyHostToDevice), cuda_memcpy_failed);
		throw_on_cuda_error(cudaMemcpy(right_matrix_in_device_memory.device_pointer, right_matrix, buffer_size, cudaMemcpyHostToDevice), cuda_memcpy_failed);

		dim3 threads_per_block(std::min(dimension_of_matrix, 32ul), std::min(dimension_of_matrix, 32ul));
		dim3 blocks_per_grid(dimension_of_matrix / threads_per_block.x, dimension_of_matrix / threads_per_block.y);

		square_matrix_addition_kernel<TRealNumber><<<blocks_per_grid, threads_per_block>>>(
			left_matrix_in_device_memory.device_pointer,
			right_matrix_in_device_memory.device_pointer,
			result_matrix_in_device_memory.device_pointer,
			dimension_of_matrix
			);

		throw_on_cuda_error(cudaGetLastError(), cuda_kernel_failed);
		throw_on_cuda_error(cudaDeviceSynchronize(), cuda_device_synchronize_failed);

		throw_on_cuda_error(cudaMemcpy(result_matrix, result_matrix_in_device_memory.device_pointer, buffer_size, cudaMemcpyDeviceToHost), cuda_memcpy_failed);

		return succeeded;
	}
	catch (const computation_failed_exception& exception)
	{
		return exception.failure;
	}
}

extern "C" __declspec(dllexport) int single_precision_square_matrix_addition(
	const float* left_matrix,
	const float* right_matrix,
	float* result_matrix,
	const unsigned long dimension_of_matrix)
{
	return square_matrix_addition<float>(left_matrix, right_matrix, result_matrix, dimension_of_matrix);
}

extern "C" __declspec(dllexport) int double_precision_square_matrix_addition(
	const double* left_matrix,
	const double* right_matrix,
	double* result_matrix,
	const unsigned long dimension_of_matrix)
{
	return square_matrix_addition<double>(left_matrix, right_matrix, result_matrix, dimension_of_matrix);
}
