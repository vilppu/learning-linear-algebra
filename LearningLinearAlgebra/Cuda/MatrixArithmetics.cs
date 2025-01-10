namespace LearningLinearAlgebra.Cuda;

static class MatrixArithmetics
{
    private static readonly object ThreadSynchronization = new();

    public static float[] Add(this float[] left, float[] right)
    {
        lock (ThreadSynchronization)
        {
            var result = new float[left.Length];

            unsafe
            {
                fixed (float* pointerToLeft = &left[0])
                fixed (float* pointerToRight = &right[0])
                fixed (float* pointerToResult = &result[0])
                {
                    return CudaComputation
                        .single_precision_vector_addition(pointerToLeft, pointerToRight, pointerToResult, left.LongLength)
                        .ThrowOnFailureOrReturn(result);
                }
            }
        }
    }

    public static double[] Add(this double[] left, double[] right)
    {
        lock (ThreadSynchronization)
        {
            var result = new double[left.Length];

            unsafe
            {
                fixed (double* pointerToLeft = &left[0])
                fixed (double* pointerToRight = &right[0])
                fixed (double* pointerToResult = &result[0])
                {
                    return CudaComputation
                        .double_precision_vector_addition(pointerToLeft, pointerToRight, pointerToResult, left.LongLength)
                        .ThrowOnFailureOrReturn(result);
                }
            }
        }
    }

    public static float[,] Add(this float[,] left, float[,] right)
    {
        lock (ThreadSynchronization)
        {
            var dimensionOfMatrix = left.GetLength(0);
            var result = new float[dimensionOfMatrix, dimensionOfMatrix];

            unsafe
            {
                fixed (float* pointerToLeft = &left[0, 0])
                fixed (float* pointerToRight = &right[0, 0])
                fixed (float* pointerToResult = &result[0, 0])
                {
                    return CudaComputation
                        .single_precision_square_matrix_addition(pointerToLeft, pointerToRight, pointerToResult, dimensionOfMatrix)
                        .ThrowOnFailureOrReturn(result);
                }
            }
        }
    }

    public static double[,] Add(this double[,] left, double[,] right)
    {
        lock (ThreadSynchronization)
        {
            var dimensionOfMatrix = left.GetLength(0);
            var result = new double[dimensionOfMatrix, dimensionOfMatrix];

            unsafe
            {
                fixed (double* pointerToLeft = &left[0, 0])
                fixed (double* pointerToRight = &right[0, 0])
                fixed (double* pointerToResult = &result[0, 0])
                {
                    return CudaComputation
                        .double_precision_square_matrix_addition(pointerToLeft, pointerToRight, pointerToResult, dimensionOfMatrix)
                        .ThrowOnFailureOrReturn(result);
                }
            }
        }
    }
}