using System.Numerics;
using Computation.Numbers;

namespace Computation.Matrices.Complex;

// TODO: Remove and have generic math interfaces only for linear operator (or share same interface with linear operator?)
public interface ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
    where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf M(ComplexNumber<double>[,] entries);
    public static abstract TSelf M(ComplexNumber<float>[,] entries);
    public static abstract TSelf M(ComplexNumber<TRealNumber>[,] entries);
    public static abstract TSelf M(int m, Func<int, int, ComplexNumber<TRealNumber>> initializer);

    public static abstract bool IsHermitian(TSelf self);
    public static abstract bool IsIdentity(TSelf self);
    public static abstract bool IsUnitary(TSelf self);
    public static abstract IEnumerable<ComplexNumber<TRealNumber>> Column(TSelf self, int j);
    public static abstract IEnumerable<ComplexNumber<TRealNumber>> Row(TSelf self, int i);
    public static abstract IEnumerable<IEnumerable<ComplexNumber<TRealNumber>>> Columns(TSelf self);
    public static abstract IEnumerable<IEnumerable<ComplexNumber<TRealNumber>>> Rows(TSelf self);
    public static abstract int M(TSelf self);
    public static abstract int N(TSelf self);
    public static abstract TColumnVector Act(TSelf self, TColumnVector vector);
    public static abstract TRowVector Act(TRowVector vector, TSelf self);
    public static abstract TSelf Add(TSelf left, TSelf right);
    public static abstract TSelf AdditiveInverse(TSelf self);
    public static abstract TSelf Adjoint(TSelf self);
    public static abstract TSelf Commutator(TSelf left, TSelf right);
    public static abstract TSelf Conjucate(TSelf self);
    public static abstract TSelf Identity(int m);
    public static abstract TSelf Map(TSelf self, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
    public static abstract TSelf Multiply(ComplexNumber<TRealNumber> scalar, TSelf self);
    public static abstract TSelf Multiply(TRealNumber scalar, TSelf self);
    public static abstract TSelf Multiply(TSelf left, TSelf right);
    public static abstract TSelf Round(TSelf self);
    public static abstract TSelf Subtract(TSelf left, TSelf right);
    public static abstract TSelf TensorProduct(TSelf left, TSelf right);
    public static abstract TSelf Transpose(TSelf self);
    public static abstract TSelf Zero(int m);
    public static abstract TSelf Zip(TSelf left, TSelf right, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);

    public ComplexNumber<TRealNumber> this[int i, int j] { get; }
    public ComplexNumber<TRealNumber>[,] Entries { get; }
    public static abstract TSelf operator +(TSelf left, TSelf right);
    public static abstract TSelf operator -(TSelf left, TSelf right);
    public static abstract TSelf operator -(TSelf self);
    public static abstract TSelf operator *(ComplexNumber<TRealNumber> scalar, TSelf self);
    public static abstract TSelf operator *(TRealNumber scalar, TSelf self);
    public static abstract TSelf operator *(TSelf left, TSelf right);
    public static abstract TColumnVector operator *(TSelf self, TColumnVector vector);
    public static abstract TRowVector operator *(TRowVector vector, TSelf self);
}

public static class SquareMatrix
{
    public static bool IsHermitian<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.IsHermitian((TSelf)self);

    public static bool IsIdentity<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.IsIdentity((TSelf)self);

    public static bool IsUnitary<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.IsUnitary((TSelf)self);

    public static IEnumerable<ComplexNumber<TRealNumber>> Column<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self, int j)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Column((TSelf)self, j);

    public static IEnumerable<ComplexNumber<TRealNumber>> Row<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self, int i)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Row((TSelf)self, i);

    public static IEnumerable<IEnumerable<ComplexNumber<TRealNumber>>> Columns<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Columns((TSelf)self);

    public static IEnumerable<IEnumerable<ComplexNumber<TRealNumber>>> Rows<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
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
        TSelf.Act((TRowVector)vector, self);

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

    public static TSelf Adjoint<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Adjoint((TSelf)self);

    public static TSelf Commutator<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> left, TSelf right)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Commutator((TSelf)left, right);

    public static TSelf Conjucate<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Conjucate((TSelf)self);

    public static TSelf Map<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Map((TSelf)self, elementMapping);

    public static TSelf Multiply<TSelf, TRowVector, TColumnVector, TRealNumber>(this ComplexNumber<TRealNumber> scalar, ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> self)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Multiply(scalar, (TSelf)self);

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

    public static TSelf Zip<TSelf, TRowVector, TColumnVector, TRealNumber>(this ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> left, TSelf right, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping)
        where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Zip((TSelf)left, right, elementMapping);

}

public record SquareMatrix<TRealNumber>(IBoxedSquareMatrix<TRealNumber> Self)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(SquareMatrix<TRealNumber>? other) =>
        Self.Equals(other?.Self);

    public override int GetHashCode() =>
        Self.GetHashCode();

    public bool IsHermitian() =>
        Self.IsHermitian();

    public bool IsIdentity() =>
        Self.IsIdentity();

    public bool IsUnitary() =>
        Self.IsUnitary();

    public ColumnVector<TRealNumber> Act(ColumnVector<TRealNumber> vector) =>
        ColumnVector<TRealNumber>.V(Self.Act(vector.Self));

    public IEnumerable<ComplexNumber<TRealNumber>> Column(int j) =>
        Self.Column(j);

    public IEnumerable<ComplexNumber<TRealNumber>> Row(int i) =>
        Self.Row(i);

    public int M() =>
        Self.M();

    public int N() =>
        Self.N();

    public RowVector<TRealNumber> Act(RowVector<TRealNumber> vector) =>
        RowVector<TRealNumber>.U(Self.Act(vector.Self));

    public SquareMatrix<TRealNumber> Add(SquareMatrix<TRealNumber> right) =>
        M(Self.Add(right.Self));

    public SquareMatrix<TRealNumber> AdditiveInverse() =>
        M(Self.AdditiveInverse());

    public SquareMatrix<TRealNumber> Adjoint() =>
        M(Self.Adjoint());

    public SquareMatrix<TRealNumber> Commutator(SquareMatrix<TRealNumber> right) =>
        M(Self.Commutator(right.Self));

    public SquareMatrix<TRealNumber> Conjucate() =>
        M(Self.Conjucate());

    public SquareMatrix<TRealNumber> Map(Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
        M(Self.Map(elementMapping));

    public SquareMatrix<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar) =>
        M(Self.Multiply(scalar));

    public SquareMatrix<TRealNumber> Multiply(SquareMatrix<TRealNumber> right) =>
        M(Self.Multiply(right.Self));

    public SquareMatrix<TRealNumber> Multiply(TRealNumber scalar) =>
        M(Self.Multiply(scalar));

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
        Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
        M(Self.Zip(second.Self, elementMapping));

    public static SquareMatrix<TRealNumber> M(IBoxedSquareMatrix<TRealNumber> squareMatrix) =>
        new(squareMatrix);

    public ComplexNumber<TRealNumber> this[int i, int j] => Self[i, j];

    public ComplexNumber<TRealNumber>[,] Entries => Self.Entries;

    public static SquareMatrix<TRealNumber> operator +(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.Add(right);

    public static SquareMatrix<TRealNumber> operator -(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.Subtract(right);

    public static SquareMatrix<TRealNumber> operator -(SquareMatrix<TRealNumber> self) =>
        self.AdditiveInverse();

    public static SquareMatrix<TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, SquareMatrix<TRealNumber> self) =>
        self.Multiply(scalar);

    public static SquareMatrix<TRealNumber> operator *(TRealNumber scalar, SquareMatrix<TRealNumber> self) =>
        self.Multiply(scalar);

    public static SquareMatrix<TRealNumber> operator *(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.Multiply(right);

    public static ColumnVector<TRealNumber> operator *(SquareMatrix<TRealNumber> self, ColumnVector<TRealNumber> vector) =>
        self.Act(vector);

    public static RowVector<TRealNumber> operator *(RowVector<TRealNumber> vector, SquareMatrix<TRealNumber> self) =>
        self.Act(vector);
}