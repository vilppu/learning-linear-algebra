using FluentAssertions;
using Xunit;

namespace LearningLinearAlgebra.Tests;

public class CudaComputationTests
{
    [Fact]
    public void Compute_on_NVIDIA_GPU()
    {
        var result = CudaComputation.ComputeInGpu();

        result.Should().Be(11);
    }
}