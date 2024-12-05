using System.Runtime.InteropServices;

namespace LearningLinearAlgebra
{
    public static partial class CudaComputation
    {

        public static int ComputeInGpu()
        {
            return Compute();
        }


        [LibraryImport("CudaComputation.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
        private static partial int Compute();
    }
}
