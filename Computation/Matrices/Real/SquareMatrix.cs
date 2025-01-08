using Computation.Cuda;
using Computation.Infrastructure;
using Computation.Numbers;
using System.Numerics;

namespace Computation.Matrices.Real;

// TODO: Remove and have generic math interfaces only for linear Operator (or share same interface with linear operator?)
public interface ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
    where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf M(double[,] entries);
    public static abstract TSelf M(float[,] entries);
    public static abstract TSelf M(int[,] entries);
    public static abstract TSelf M(TRealNumber[,] entries);
    public static abstract TSelf M(int m, Func<int, int, TRealNumber> initializer);

    public static abstract bool IsIdentity(TSelf self);
    public static abstract IEnumerable<TRealNumber> Column(TSelf self, int j);
    public static abstract IEnumerable<TRealNumber> Row(TSelf self, int i);
    public static abstract IEnumerable<IEnumerable<TRealNumber>> Columns(TSelf self);
    public static abstract IEnumerable<IEnumerable<TRealNumber>> Rows(TSelf self);
    public static abstract int M(TSelf self);
    public static abstract int N(TSelf self);
    public static abstract TColumnVector Act(TSelf self, TColumnVector vector);
    public static abstract TRowVector Act(TRowVector vector, TSelf self);
    public static abstract TSelf Add(TSelf left, TSelf right);
    public static abstract TSelf AdditiveInverse(TSelf self);
    public static abstract TSelf Commutator(TSelf left, TSelf right);
    public static abstract TSelf Identity(int m);
    public static abstract TSelf Map(TSelf self, Func<TRealNumber, TRealNumber> elementMapping);
    public static abstract TSelf Multiply(TRealNumber scalar, TSelf self);
    public static abstract TSelf Multiply(TSelf left, TSelf right);
    public static abstract TSelf Round(TSelf self);
    public static abstract TSelf Subtract(TSelf left, TSelf right);
    public static abstract TSelf TensorProduct(TSelf left, TSelf right);
    public static abstract TSelf Transpose(TSelf self);
    public static abstract TSelf Zero(int m);
    public static abstract TSelf Zip(TSelf left, TSelf right, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping);

    public TRealNumber this[int i, int j] { get; }
    public TRealNumber[,] Entries { get; }
    public static abstract TSelf operator +(TSelf left, TSelf right);
    public static abstract TSelf operator -(TSelf left, TSelf right);
    public static abstract TSelf operator -(TSelf self);
    public static abstract TSelf operator *(TRealNumber scalar, TSelf self);
    public static abstract TSelf operator *(TSelf left, TSelf right);
    public static abstract TColumnVector operator *(TSelf self, TColumnVector vector);
    public static abstract TRowVector operator *(TRowVector vector, TSelf self);
}

public static class SquareMatrix
{
    public static bool IsIdentity<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.IsIdentity((TSelf)self);

    public static IEnumerable<TRealNumber> Column<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self, int j)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Column((TSelf)self, j);

    public static IEnumerable<TRealNumber> Row<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self, int i)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Row((TSelf)self, i);

    public static IEnumerable<IEnumerable<TRealNumber>> Columns<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Columns((TSelf)self);

    public static IEnumerable<IEnumerable<TRealNumber>> Rows<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Rows((TSelf)self);

    public static int M<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.M((TSelf)self);

    public static int N<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.N((TSelf)self);

    public static TColumnVector Act<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self, TColumnVector vector)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Act((TSelf)self, vector);

    public static TRowVector Act<TSelf, TRowVector, TColumnVector, TRealNumber>(this IRowVector<TRowVector, TColumnVector, TRealNumber> vector, TSelf self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Act((TRowVector)vector,  self);

    public static TSelf Add<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> left, TSelf right)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Add((TSelf)left, right);

    public static TSelf AdditiveInverse<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.AdditiveInverse((TSelf)self);

    public static TSelf Commutator<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> left, TSelf right)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Commutator((TSelf)left, right);

    public static TSelf Map<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self, Func<TRealNumber, TRealNumber> elementMapping)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Map((TSelf)self, elementMapping);

    public static TSelf Multiply<TSelf, TRowVector, TColumnVector, TRealNumber>(this TRealNumber scalar, ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Multiply(scalar, (TSelf)self);

    public static TSelf Multiply<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self, TRealNumber scalar)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Multiply(scalar, (TSelf)self);

    public static TSelf Multiply<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> left, TSelf right)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Multiply((TSelf)left, right);

    public static TSelf Round<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Round((TSelf)self);

    public static TSelf Subtract<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> left, TSelf right)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Subtract((TSelf)left, right);

    public static TSelf TensorProduct<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> left, TSelf right)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.TensorProduct((TSelf)left, right);

    public static TSelf Transpose<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Transpose((TSelf)self);

    public static TSelf Zip<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> left, TSelf right, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Zip((TSelf)left, right, elementMapping);
}

public record SquareMatrix<TRealNumber>(TRealNumber[,] Entries)
    : ISquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(SquareMatrix<TRealNumber>? other) =>
        other?.Entries != null && Entries.Flatten().SequenceEqual(other.Entries.Flatten());

    public override int GetHashCode() =>
        Entries.GetHashCode();

    public static SquareMatrix<TRealNumber> M(TRealNumber[,] entries) =>
        new(entries);

    public static SquareMatrix<TRealNumber> M(int[,] entries) =>
        M(entries.NumberOfRows(), entries.NumberOfColumns(), (i, j) => RealNumber<TRealNumber>.R(entries[i, j]));

    public static SquareMatrix<TRealNumber> M(float[,] entries) =>
        M(entries.NumberOfRows(), entries.NumberOfColumns(), (i, j) => RealNumber<TRealNumber>.R(entries[i, j]));

    public static SquareMatrix<TRealNumber> M(double[,] entries) =>
        M(entries.NumberOfRows(), entries.NumberOfColumns(), (i, j) => RealNumber<TRealNumber>.R(entries[i, j]));

    public static SquareMatrix<TRealNumber> M(int m, int n, Func<int, int, TRealNumber> initializer) =>
        new(TwoDimensionalArray.Initialize(m, n, initializer));

    public static SquareMatrix<TRealNumber> Zero(int m, int n) =>
         M(m, m, (i, j) => TRealNumber.Zero);

    public static SquareMatrix<TRealNumber> M(int m, Func<int, int, TRealNumber> initializer) =>
        new(TwoDimensionalArray.Initialize(m, m, initializer));

    public static SquareMatrix<TRealNumber> Zero(int m) =>
         M(m, (i, j) => TRealNumber.Zero);

    public static SquareMatrix<TRealNumber> Identity(int m) =>
         M(m, (i, j) => i == j ? TRealNumber.One : TRealNumber.Zero);

    public static int M(SquareMatrix<TRealNumber> matrix) =>
        matrix.Entries.NumberOfRows();

    public static int N(SquareMatrix<TRealNumber> matrix) =>
        matrix.Entries.NumberOfColumns();

    public static IEnumerable<(int i, int j)> Indices(SquareMatrix<TRealNumber> matrix) =>
        matrix.Entries.Indices();

    public static IEnumerable<TRealNumber> Row(SquareMatrix<TRealNumber> matrix, int i) =>
        matrix.Entries.Row(i);

    public static IEnumerable<IEnumerable<TRealNumber>> Rows(SquareMatrix<TRealNumber> matrix) =>
        matrix.Entries.Rows();

    public static IEnumerable<TRealNumber> Column(SquareMatrix<TRealNumber> matrix, int j) =>
        matrix.Entries.Column(j);

    public static IEnumerable<IEnumerable<TRealNumber>> Columns(SquareMatrix<TRealNumber> matrix) =>
        matrix.Entries.Columns();

    public static SquareMatrix<TRealNumber> Map(SquareMatrix<TRealNumber> matrix, Func<TRealNumber, TRealNumber> elementMapping) =>
         M(matrix.M(), (i, j) => elementMapping(matrix[i, j]));

    public static SquareMatrix<TRealNumber> Zip(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
         M(left.M(), (i, j) => elementMapping(left[i, j], right[i, j]));

    public static SquareMatrix<TRealNumber> AdditiveInverse(SquareMatrix<TRealNumber> matrix) =>
        matrix.Map(entry => entry * TRealNumber.NegativeOne);

    public static SquareMatrix<TRealNumber> Transpose(SquareMatrix<TRealNumber> matrix) =>
        M(matrix.N(), (i, j) => matrix[j, i]);

    public static SquareMatrix<TRealNumber> Round(SquareMatrix<TRealNumber> matrix) =>
        matrix.Map(entry => entry.Round());

    // TODO: Move to linear vector space
    public static bool IsIdentity(SquareMatrix<TRealNumber> matrix) =>
        Indices(matrix).Aggregate(true,
            (identity, x) => x.i == x.j
            ? matrix[x.i, x.j].Round() == TRealNumber.One
            : matrix[x.i, x.j].Round() == TRealNumber.Zero);

    public static SquareMatrix<TRealNumber> Add(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) => (left, right) switch
    {
        (SquareMatrix<float> l, SquareMatrix<float> r) => M(l.Entries.Add(r.Entries)),
        (SquareMatrix<double> l, SquareMatrix<double> r) => M(l.Entries.Add(r.Entries)),
        _ => left.Zip(right, (a, b) => a + b)
    };

    public static SquareMatrix<TRealNumber> Subtract(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
         left.Zip(right, (x, y) => x - y);

    public static SquareMatrix<TRealNumber> Multiply(TRealNumber scalar, SquareMatrix<TRealNumber> matrix) =>
         M(matrix.M(), (i, j) => scalar * matrix[i, j]);

    public static SquareMatrix<TRealNumber> Multiply(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        M(left.M(), (i, j) =>
            left.Row(i).Zip(right.Column(j), (x, y) => x * y).Aggregate(TRealNumber.Zero, (x, y) => x + y)
        );

    public static ColumnVector<TRealNumber> Act(SquareMatrix<TRealNumber> matrix, ColumnVector<TRealNumber> vector) =>
        ColumnVector<TRealNumber>.V(matrix.M(), i => matrix.Row(i).Zip(vector.Entries, (x, y) => x * y).Aggregate(TRealNumber.Zero, (x, y) => x + y));

    public static RowVector<TRealNumber> Act(RowVector<TRealNumber> vector, SquareMatrix<TRealNumber> matrix) =>
        RowVector<TRealNumber>.U(matrix.N(), i => matrix.Row(i).Zip(vector.Entries, (x, y) => x * y).Aggregate(TRealNumber.Zero, (x, y) => x + y));

    // TODO: Move to linear vector space
    public static SquareMatrix<TRealNumber> TensorProduct(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        M(left.M() * right.M(),
          (j, k) => left[j / right.N(), k / right.M()] * right[j % right.N(), k % right.M()]);

    // TODO: Move to linear vector space
    public static SquareMatrix<TRealNumber> Commutator(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
       left * right - right * left;

    public static bool AreEquivalent(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.Entries.Cast<TRealNumber>().SequenceEqual(right.Entries.Cast<TRealNumber>());

    public TRealNumber this[int i, int j] => Entries[i, j];
    public static SquareMatrix<TRealNumber> operator +(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) => Add(left, right);
    public static SquareMatrix<TRealNumber> operator -(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) => Subtract(left, right);
    public static SquareMatrix<TRealNumber> operator *(TRealNumber scalar, SquareMatrix<TRealNumber> matrix) => Multiply(scalar, matrix);
    public static SquareMatrix<TRealNumber> operator *(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) => Multiply(left, right);
    public static ColumnVector<TRealNumber> operator *(SquareMatrix<TRealNumber> matrix, ColumnVector<TRealNumber> vector) => Act(matrix, vector);
    public static RowVector<TRealNumber> operator *(RowVector<TRealNumber> vector, SquareMatrix<TRealNumber> matrix) => Act(vector, matrix);
    public static SquareMatrix<TRealNumber> operator -(SquareMatrix<TRealNumber> vector) => AdditiveInverse(vector);
}
