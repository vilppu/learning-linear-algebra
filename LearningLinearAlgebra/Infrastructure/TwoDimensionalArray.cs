namespace LearningLinearAlgebra.Infrastructure;

static class TwoDimensionalArray
{
    public static TElement[,] Initialize<TElement>(int m, int n, Func<int, int, TElement> initializer)
    {
        var entries = new TElement[m, n];

        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                entries[i, j] = initializer(i, j);
            }
        }
        return entries;
    }

    public static int NumberOfRows<TElement>(this TElement[,] source) => 
        source.GetLength(0);

    public static int NumberOfColumns<TElement>(this TElement[,] source) => 
        source.GetLength(1);

    public static IEnumerable<(int i, int j)> Indices<TElement>(this TElement[,] source) => 
        Enumerable.Range(0, source.NumberOfRows()).SelectMany(i => Enumerable.Range(0, source.NumberOfColumns()).Select(j => (i, j)));

    public static IEnumerable<IEnumerable<TElement>> ToEnumerable<TElement>(this TElement[,] source) =>
        source.Rows();

    public static IEnumerable<TElement> Flatten<TElement>(this TElement[,] source) =>
        source.Cast<TElement>();

    public static IEnumerable<IEnumerable<TElement>> Rows<TElement>(this TElement[,] source) =>
        Enumerable.Range(0, source.NumberOfRows()).Select(source.Row);

    public static IEnumerable<TElement> Row<TElement>(this TElement[,] source, int i) =>
        Enumerable.Range(0, source.NumberOfColumns()).Select(columnIndex => source.Element(i, columnIndex));
   
    public static IEnumerable<TElement> Column<TElement>(this TElement[,] source, int j) =>
        Enumerable.Range(0, source.NumberOfRows()).Select(i => source[i, j]);

    public static IEnumerable<IEnumerable<TElement>> Columns<TElement>(this TElement[,] source) =>
        Enumerable.Range(0, source.NumberOfColumns()).Select(i => Column(source, i));

    public static TElement Element<TElement>(this TElement[,] source, int rowIndex, int j) =>
        source[rowIndex, j];
}