using System.Runtime.InteropServices;

namespace Computation.Cuda;

public class CudaComputationFailedException(string message) : Exception(message);

enum ComputationResult
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
    public static TResult ThrowOnFailureOrReturn<TResult>(this ComputationResult singlePrecisionVectorAddition, TResult result) =>
        singlePrecisionVectorAddition switch
        {
            ComputationResult.Succeeded => result,
            var failure => throw new CudaComputationFailedException(failure.ToString())
        };
}

static partial class CudaComputation
{
    [LibraryImport(
        libraryName: "Cuda/CudaComputation.dll",
        StringMarshalling = StringMarshalling.Utf16, 
        SetLastError = true, 
        EntryPoint = "single_precision_vector_addition")]
    public static partial ComputationResult single_precision_vector_addition(
        float[] left,
        float[] right,
        float[] result,
        long vectorLength);
}