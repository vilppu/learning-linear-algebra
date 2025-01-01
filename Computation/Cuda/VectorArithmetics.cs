namespace Computation.Cuda;

static class VectorArithmetics
{
    private static readonly object ThreadSynchronization = new();

    public static float[] Add(this float[] left,
        float[] right)
    {
        lock (ThreadSynchronization)
        {
            var vector = new float[left.Length];

            return CudaComputation
                .single_precision_vector_addition(left, right, vector, left.LongLength)
                .ThrowOnFailureOrReturn(vector);
        }
    }

    public static double[] Add(this double[] left,
        double[] right)
    {
        lock (ThreadSynchronization)
        {
            var vector = new double[left.Length];

            return CudaComputation
                .double_precision_vector_addition(left, right, vector, left.LongLength)
                .ThrowOnFailureOrReturn(vector);
        }
    }
}