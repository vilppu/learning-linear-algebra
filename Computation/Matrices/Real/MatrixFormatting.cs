using System.Numerics;
using Computation.Infrastructure;
using Computation.Numbers;

namespace Computation.Matrices.Real;

public static class MatrixFormatting
{
    public static string Formatted<TRealNumber>(this SquareMatrix<TRealNumber> source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        source.Entries.Formatted();

    public static string Formatted<TRealNumber>(this RowVector<TRealNumber> source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        source.Entries.Formatted(source.Length());

    public static string Formatted<TRealNumber>(this ColumnVector<TRealNumber> source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        source.Entries.Formatted(source.Length());

    private static string Formatted<TRealNumber>(this IEnumerable<TRealNumber> source, long length)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        length > 50
            ? "{" + string.Join(", ", source.Take(50)) + " ...}"
            : "{" + string.Join(", ", source.Select(NumberFormatting.Formatted)) + "}";

    private static string Formatted<TRealNumber>(this IEnumerable<IEnumerable<TRealNumber>> source, long width)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        width > 50
            ? "{\r\n" + string.Join(",\r\n", source.Take(50).Select(row => Formatted(row, width))) + " ...\r\n}"
            : "{\r\n" + string.Join(",\r\n", source.Select(row => Formatted(row, width))) + "\r\n}";

    private static string Formatted<TRealNumber>(this TRealNumber[,] source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Formatted(source.ToEnumerable(), source.Length);
}