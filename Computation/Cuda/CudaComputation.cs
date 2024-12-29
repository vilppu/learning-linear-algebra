using System.Runtime.InteropServices;

namespace Computation.Cuda;

public static partial class CudaComputation
{

    public static int ComputeInGpu()
    {
        return Compute();
    }


    [LibraryImport("Cuda/CudaComputation.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    private static partial int Compute();
}