using System.Diagnostics;

namespace LearningLinearAlgebra.Tests.Helpers;

static class ComputationStopwatch
{
    public static (TResult result, TimeSpan Elapsed) MeasureTime<TResult>(Func<TResult> computation)
    {
        var stopWatch = new Stopwatch();

        stopWatch.Restart();

        var result = computation();

        stopWatch.Stop();

        return (result, stopWatch.Elapsed);
    }
}