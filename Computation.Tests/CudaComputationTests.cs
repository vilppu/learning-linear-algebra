using Computation.Cuda;
using FluentAssertions;

namespace Computation.Tests;

public class CudaComputationTests
{
    [Fact]
    public void Compute_on_NVIDIA_GPU()
    {
        var result = CudaComputation.ComputeInGpu();

        result.Should().Be(11);
    }
}