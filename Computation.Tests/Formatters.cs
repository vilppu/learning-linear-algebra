using System.Numerics;
using Computation.Matrices.Complex;
using Computation.Matrices.Real;
using Computation.Numbers;
using FluentAssertions.Formatting;

namespace Computation.Tests;

public static class Formatters<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    static Formatters()
    {
        Formatter.AddFormatter(new MatrixFormatter<TRealNumber>());
        Formatter.AddFormatter(new NumberFormatter<TRealNumber>());
    }

    public static void Register()
    {
    }
}

public class MatrixFormatter<TRealNumber> : IValueFormatter
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public bool CanHandle(object value) =>
        value is IEnumerable<TRealNumber> or
            Matrices.Real.SquareMatrix<TRealNumber> or
            Matrices.Real.RowVector<TRealNumber> or
            Matrices.Real.ColumnVector<TRealNumber> or
            Matrices.Complex.SquareMatrix<TRealNumber> or
            Matrices.Complex.RowVector<TRealNumber> or
            Matrices.Complex.ColumnVector<TRealNumber>;

    public void Format(object value, FormattedObjectGraph formattedGraph, FormattingContext context, FormatChild formatChild) =>
        Add(formattedGraph, context, value switch
        {
            Matrices.Real.SquareMatrix<TRealNumber> source => source.Formatted(),
            Matrices.Real.RowVector<TRealNumber> source => source.Formatted(),
            Matrices.Real.ColumnVector<TRealNumber> source => source.Formatted(),
            Matrices.Complex.SquareMatrix<TRealNumber> source => source.Formatted(),
            Matrices.Complex.RowVector<TRealNumber> source => source.Formatted(),
            Matrices.Complex.ColumnVector<TRealNumber> source => source.Formatted(),
            _ => throw new InvalidOperationException($"{nameof(MatrixFormatter<TRealNumber>)} cannot handle {value.GetType()}")
        });

    private static void Add(FormattedObjectGraph formattedGraph, FormattingContext context, string formatted)
    {
        if (context.UseLineBreaks)
        {
            formattedGraph.AddLine(formatted);
        }
        else
        {
            formattedGraph.AddFragment(formatted);
        }
    }
}

public class NumberFormatter<TRealNumber> : IValueFormatter
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public bool CanHandle(object value) =>
        value is ComplexNumber<TRealNumber>;

    public void Format(object value, FormattedObjectGraph formattedGraph, FormattingContext context, FormatChild formatChild) => 
        Add(formattedGraph, context, ((ComplexNumber<TRealNumber>)value).Formatted());

    private static void Add(FormattedObjectGraph formattedGraph, FormattingContext context, string formatted)
    {
        if (context.UseLineBreaks)
        {
            formattedGraph.AddLine(formatted);
        }
        else
        {
            formattedGraph.AddFragment(formatted);
        }
    }
}