using System.Numerics;
using Computation.Matrices.Complex;
using Computation.Matrices.Real;
using Computation.Numbers;
using FluentAssertions.Formatting;

namespace LearningLinearAlgebra.Tests.Helpers;

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
            Computation.Matrices.Real.SquareMatrix<TRealNumber> or
            Computation.Matrices.Real.RowVector<TRealNumber> or
            Computation.Matrices.Real.ColumnVector<TRealNumber> or
            Computation.Matrices.Complex.SquareMatrix<TRealNumber> or
            Computation.Matrices.Complex.RowVector<TRealNumber> or
            Computation.Matrices.Complex.ColumnVector<TRealNumber> or
            RealVectorSpace.Operator<TRealNumber> or
            RealVectorSpace.Ket<TRealNumber> or
            RealVectorSpace.Bra<TRealNumber> or
            ComplexVectorSpace.Operator<TRealNumber> or
            ComplexVectorSpace.Ket<TRealNumber> or
            ComplexVectorSpace.Bra<TRealNumber>;

    public void Format(object value, FormattedObjectGraph formattedGraph, FormattingContext context, FormatChild formatChild) =>
        Add(formattedGraph, context, value switch
        {
            Computation.Matrices.Real.SquareMatrix<TRealNumber> source => source.Formatted(),
            Computation.Matrices.Real.RowVector<TRealNumber> source => source.Formatted(),
            Computation.Matrices.Real.ColumnVector<TRealNumber> source => source.Formatted(),
            Computation.Matrices.Complex.SquareMatrix<TRealNumber> source => source.Formatted(),
            Computation.Matrices.Complex.RowVector<TRealNumber> source => source.Formatted(),
            Computation.Matrices.Complex.ColumnVector<TRealNumber> source => source.Formatted(),
            RealVectorSpace.Operator<TRealNumber> source => source.Components.Formatted(),
            RealVectorSpace.Ket<TRealNumber> source => source.Components.Formatted(),
            RealVectorSpace.Bra<TRealNumber> source => source.Components.Formatted(),
            ComplexVectorSpace.Operator<TRealNumber> source => source.Components.Formatted(),
            ComplexVectorSpace.Ket<TRealNumber> source => source.Components.Formatted(),
            ComplexVectorSpace.Bra<TRealNumber> source => source.Components.Formatted(),
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