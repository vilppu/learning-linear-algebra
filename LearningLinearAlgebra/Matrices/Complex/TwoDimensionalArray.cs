namespace LearningLinearAlgebra.Matrices.Complex;

static class TwoDimensionalArray<TElement>
{
    public static TElement[,] Initialize(int m, int n, Func<int, int, TElement> initializer)
    {
        var entries = new TElement[m, n];

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                entries[i, j] = initializer(i, j);
            }
        }

        return entries;
    }
}