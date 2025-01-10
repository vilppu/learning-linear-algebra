using System.Numerics;
using FluentAssertions.Formatting;
using LearningLinearAlgebra.Matrices.Real;
using LearningLinearAlgebra.Numbers;
using MatrixFormatting = LearningLinearAlgebra.Matrices.Complex.MatrixFormatting;

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
            SquareMatrix<TRealNumber> or
            RowVector<TRealNumber> or
            ColumnVector<TRealNumber> or Matrices.Complex.SquareMatrix<TRealNumber> or Matrices.Complex.RowVector<TRealNumber> or Matrices.Complex.ColumnVector<TRealNumber> or
            RealVectorSpace.Operator<TRealNumber> or
            RealVectorSpace.Ket<TRealNumber> or
            RealVectorSpace.Bra<TRealNumber> or
            ComplexVectorSpace.Operator<TRealNumber> or
            ComplexVectorSpace.Ket<TRealNumber> or
            ComplexVectorSpace.Bra<TRealNumber>;

    public void Format(object value, FormattedObjectGraph formattedGraph, FormattingContext context, FormatChild formatChild) =>
        Add(formattedGraph, context, value switch
        {
            SquareMatrix<TRealNumber> source => source.Formatted(),
            RowVector<TRealNumber> source => source.Formatted(),
            ColumnVector<TRealNumber> source => source.Formatted(),
            Matrices.Complex.SquareMatrix<TRealNumber> source => MatrixFormatting.Formatted(source),
            Matrices.Complex.RowVector<TRealNumber> source => MatrixFormatting.Formatted(source),
            Matrices.Complex.ColumnVector<TRealNumber> source => MatrixFormatting.Formatted(source),
            RealVectorSpace.Operator<TRealNumber> source => source.Components.Formatted(),
            RealVectorSpace.Ket<TRealNumber> source => source.Components.Formatted(),
            RealVectorSpace.Bra<TRealNumber> source => source.Components.Formatted(),
            ComplexVectorSpace.Operator<TRealNumber> source => MatrixFormatting.Formatted(source.Components),
            ComplexVectorSpace.Ket<TRealNumber> source => MatrixFormatting.Formatted(source.Components),
            ComplexVectorSpace.Bra<TRealNumber> source => MatrixFormatting.Formatted(source.Components),
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