using System.Runtime.InteropServices;

namespace LearningLinearAlgebra.CudaComputation
{
    public static partial class CudaComputation
    {

        public static int ComputeInGpu()
        {
            return Compute();
        }


        [LibraryImport("CudaComputation/CudaComputation.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
        private static partial int Compute();
    }
}
