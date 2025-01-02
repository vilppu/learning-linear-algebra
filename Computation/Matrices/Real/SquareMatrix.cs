using System.Numerics;
using Computation.Numbers;

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

public record SquareMatrix<TRealNumber>(IBoxedSquareMatrix<TRealNumber> Self)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public bool IsIdentity() =>
        Self.IsIdentity();

    public ColumnVector<TRealNumber> Act(ColumnVector<TRealNumber> vector) =>
        ColumnVector<TRealNumber>.V(Self.Act(vector.BoxedColumnVector));

    public IEnumerable<TRealNumber> Column(int j) =>
        Self.Column(j);

    public IEnumerable<TRealNumber> Row(int i) =>
        Self.Row(i);

    public int M() =>
        Self.M();

    public int N() =>
        Self.N();

    public RowVector<TRealNumber> Act(RowVector<TRealNumber> vector) =>
        RowVector<TRealNumber>.U(Self.Act(vector.BoxedRowVector));

    public SquareMatrix<TRealNumber> Add(SquareMatrix<TRealNumber> right) =>
        M(Self.Add(right.Self));

    public SquareMatrix<TRealNumber> AdditiveInverse() =>
        M(Self.AdditiveInverse());

    public SquareMatrix<TRealNumber> Commutator(SquareMatrix<TRealNumber> right) =>
        M(Self.Commutator(right.Self));

    public SquareMatrix<TRealNumber> Map(Func<TRealNumber, TRealNumber> elementMapping) =>
        M(Self.Map(elementMapping));

    public SquareMatrix<TRealNumber> Multiply(TRealNumber scalar) =>
        M(Self.Multiply(scalar));

    public SquareMatrix<TRealNumber> Multiply(SquareMatrix<TRealNumber> right) =>
        M(Self.Multiply(right.Self));

    public SquareMatrix<TRealNumber> Round() =>
        M(Self.Round());

    public SquareMatrix<TRealNumber> Subtract(SquareMatrix<TRealNumber> right) =>
        M(Self.Subtract(right.Self));

    public SquareMatrix<TRealNumber> TensorProduct(SquareMatrix<TRealNumber> right) =>
        M(Self.TensorProduct(right.Self));

    public SquareMatrix<TRealNumber> Transpose() =>
        M(Self.Transpose());

    public SquareMatrix<TRealNumber> Zip(
        SquareMatrix<TRealNumber> second,
        Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
        M(Self.Zip(second.Self, elementMapping));

    public static SquareMatrix<TRealNumber> M(IBoxedSquareMatrix<TRealNumber> squareMatrix) =>
        new(squareMatrix);

    public TRealNumber this[int i, int j] => Self[i, j];

    public TRealNumber[,] Entries => Self.Entries;

    public static SquareMatrix<TRealNumber> operator +(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.Add(right);

    public static SquareMatrix<TRealNumber> operator -(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.Subtract(right);

    public static SquareMatrix<TRealNumber> operator -(SquareMatrix<TRealNumber> self) =>
        self.AdditiveInverse();

    public static SquareMatrix<TRealNumber> operator *(TRealNumber scalar, SquareMatrix<TRealNumber> self) =>
        self.Multiply(scalar);

    public static SquareMatrix<TRealNumber> operator *(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.Multiply(right);

    public static ColumnVector<TRealNumber> operator *(SquareMatrix<TRealNumber> self, ColumnVector<TRealNumber> vector) =>
        self.Act(vector);

    public static RowVector<TRealNumber> operator *(RowVector<TRealNumber> vector, SquareMatrix<TRealNumber> self) =>
        self.Act(vector);
}