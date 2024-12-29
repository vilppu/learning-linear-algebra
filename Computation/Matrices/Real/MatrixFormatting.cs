using System.Numerics;
using Computation.Infrastructure;

namespace Computation.Matrices.Real;

public static class MatrixFormatting
{
    public static string Formatted<TRealNumber>(this SquareMatrix<TRealNumber> source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> => 
        source.Entries.Formatted();

    public static string Formatted<TRealNumber>(this RowVector<TRealNumber> source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> => 
        source.Entries.Formatted();

    public static string Formatted<TRealNumber>(this ColumnVector<TRealNumber> source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        source.Entries.Formatted();

    public static string Formatted<TRealNumber>(this IEnumerable<TRealNumber> source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        "{ " + string.Join(", ", source) + " }";

    public static string Formatted<TRealNumber>(this IEnumerable<IEnumerable<TRealNumber>> source)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        "{\r\n" + string.Join(",\r\n", source.Select(Formatted)) + "\r\n}";

    public static string Formatted<TRealNumber>(this TRealNumber[,] source) 
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        source.ToEnumerable().Formatted();
}