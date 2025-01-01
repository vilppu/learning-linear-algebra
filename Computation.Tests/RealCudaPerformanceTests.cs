using System.Numerics;
using Computation.Cuda;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Computation.Tests;

public class SinglePrecisionRealCudaPerformanceTests : RealCudaPerformanceTests<float>;
public class DoublePrecisionRealCudaPerformanceTests : RealCudaPerformanceTests<double>;

public abstract class RealCudaPerformanceTests<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    protected RealCudaPerformanceTests()
    {
        Formatters<TRealNumber>.Register();
        CudaComputation.Warmup();
    }

    [Fact]
    public void Vector_addition_should_be_faster_with_CUDA()
    {
        const int vectorLength = 20000000;

        var firstVectorEntries = Enumerable.Range(0, vectorLength).Select(index => index + 0.1).ToArray();
        var secondVectorEntries = Enumerable.Range(0, vectorLength).Select(index => index + 1.2).ToArray();

        var firstManagedVector = Managed.Real.Matrices<TRealNumber>.V(firstVectorEntries);
        var secondManagedVector = Managed.Real.Matrices<TRealNumber>.V(secondVectorEntries);

        var firstCudaVector = Cuda.Real.Matrices<TRealNumber>.V(firstVectorEntries);
        var secondCudaVector = Cuda.Real.Matrices<TRealNumber>.V(secondVectorEntries);

        var (_, managedComputationTime) = ComputationStopwatch.MeasureTime(() => firstManagedVector.Add(secondManagedVector));
        var (_, cudaComputationTime) = ComputationStopwatch.MeasureTime(() => firstCudaVector.Add(secondCudaVector));

        using var _ = new AssertionScope();
        
        cudaComputationTime.Should().BeLessThan(managedComputationTime);

    }
}