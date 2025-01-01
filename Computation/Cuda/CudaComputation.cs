using System.Runtime.InteropServices;

namespace Computation.Cuda;

public class CudaComputationFailedException(CudaComputationResult failure)
    : Exception($"Cuda computation failed on: {failure}")
{
    public CudaComputationResult Failure { get; } = failure;
}

public enum CudaComputationResult
{
    Succeeded,
    CudaSetDeviceFailed,
    CudaDeviceResetFailed,
    CudaMallocFailed,
    CudaMemcpyFailed,
    CudaKernelFailed,
    CudaDeviceSynchronizeFailed
};

static class ComputationResultToException
{
    public static TResult ThrowOnFailureOrReturn<TResult>(this CudaComputationResult cudaComputationResult, TResult result) =>
        cudaComputationResult switch
        {
            CudaComputationResult.Succeeded => result,
            _ => throw new CudaComputationFailedException(cudaComputationResult)
        };
}

public static partial class CudaComputation
{
    private static readonly object ThreadSynchronization = new();

    public static int Warmup()
    {
        lock (ThreadSynchronization)
        {
            return warmup();
        }
    }

    [LibraryImport("Cuda/CudaComputation.dll")]
    public static partial CudaComputationResult single_precision_vector_addition(
        float[] left,
        float[] right,
        float[] result,
        long vectorLength);

    [LibraryImport("Cuda/CudaComputation.dll")]
    public static partial CudaComputationResult double_precision_vector_addition(
        double[] left,
        double[] right,
        double[] result,
        long vectorLength);

    [LibraryImport("Cuda/CudaComputation.dll")]
    public static partial int warmup();
}