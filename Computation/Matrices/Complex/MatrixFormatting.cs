using System.Numerics;
using Computation.Infrastructure;
using Computation.Numbers;

namespace Computation.Matrices.Complex;

public static class MatrixFormatting
{
    public static string Formatted<TRealNumber>(this SquareMatrix<TRealNumber> source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Formatted(source.Entries);

    public static string Formatted<TRealNumber>(this RowVector<TRealNumber> source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Formatted(source.Entries);

    public static string Formatted<TRealNumber>(this ColumnVector<TRealNumber> source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Formatted(source.Entries);

    public static string Formatted<TRealNumber>(IEnumerable<ComplexNumber<TRealNumber>> source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        "{" + string.Join(", ", source.Select(NumberFormatting.Formatted)) + "}";

    public static string Formatted<TRealNumber>(IEnumerable<IEnumerable<ComplexNumber<TRealNumber>>> source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        "{\r\n" + string.Join(",\r\n", source.Select(Formatted)) + "\r\n}";

    private static string Formatted<TRealNumber>(ComplexNumber<TRealNumber>[,] source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Formatted(source.ToEnumerable());
}