using System.Numerics;

namespace Computation.Matrices.Real;

// TODO: Remove and have generic math interfaces only for Ket (or share same interface with Ket?)
public interface IRowVector<TSelf, TColumnVector, TRealNumber>

    where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf U(double[] entries);
    public static abstract TSelf U(float[] entries);
    public static abstract TSelf U(int[] entries);
    public static abstract TSelf U(TRealNumber[] entries);
    public static abstract TSelf U(IEnumerable<TRealNumber> entries);
    public static abstract TSelf U(int length, Func<int, TRealNumber> initializer);

    public static abstract TRealNumber InnerProduct(TSelf left, TSelf right);
    public static abstract TRealNumber Multiply(TSelf left, TColumnVector right);
    public static abstract TRealNumber Sum(TSelf self);
    public static abstract int Length(TSelf self);
    public static abstract TColumnVector Transpose(TSelf self);
    public static abstract TRealNumber Distance(TSelf left, TSelf right);
    public static abstract TRealNumber Norm(TSelf self);
    public static abstract TSelf Add(TSelf left, TSelf right);
    public static abstract TSelf AdditiveInverse(TSelf self);
    public static abstract TSelf Map(TSelf source, Func<TRealNumber, TRealNumber> elementMapping);
    public static abstract TSelf Multiply(TRealNumber scalar, TSelf self);
    public static abstract TSelf Normalized(TSelf self);
    public static abstract TSelf Orthonormal(TSelf self);
    public static abstract TSelf Round(TSelf self);
    public static abstract TSelf Subtract(TSelf left, TSelf right);
    public static abstract TSelf TensorProduct(TSelf left, TSelf right);
    public static abstract TSelf Zero(int length);
    public static abstract TSelf Zip(TSelf first, TSelf second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping);

    public TRealNumber this[int index] { get; }
    public TRealNumber[] Entries { get; }
    public static abstract TSelf operator +(TSelf left, TSelf right);
    public static abstract TSelf operator -(TSelf left, TSelf right);
    public static abstract TSelf operator -(TSelf self);
    public static abstract TSelf operator *(TRealNumber scalar, TSelf self);
    public static abstract TRealNumber operator *(TSelf left, TColumnVector right);
    public static abstract TRealNumber operator *(TSelf left, TSelf right);
}

public static class RowVector
{
    public static TRealNumber InnerProduct<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> left, TSelf right)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.InnerProduct((TSelf)left, (TSelf)right);

    public static TRealNumber Multiply<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> left, TColumnVector right)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Multiply((TSelf)left, right);

    public static TRealNumber Sum<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> self)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Sum((TSelf)self);

    public static int Length<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> self)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Length((TSelf)self);

    public static TColumnVector Transpose<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> self)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Transpose((TSelf)self);

    public static TRealNumber Distance<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> left, TSelf right)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Distance((TSelf)left, (TSelf)right);

    public static TRealNumber Norm<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> self)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Norm((TSelf)self);

    public static TSelf Add<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> left, TSelf right)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Add((TSelf)left, (TSelf)right);

    public static TSelf AdditiveInverse<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> self)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.AdditiveInverse((TSelf)self);

    public static TSelf Map<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> source, Func<TRealNumber, TRealNumber> elementMapping)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Map((TSelf)source, elementMapping);

    public static TSelf Multiply<TSelf, TColumnVector, TRealNumber>(this TRealNumber scalar, IRowVector<TSelf, TColumnVector, TRealNumber> self)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Multiply(scalar, (TSelf)self);

    public static TSelf Normalized<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> self)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Normalized((TSelf)self);

    public static TSelf Orthonormal<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> self)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Orthonormal((TSelf)self);

    public static TSelf Round<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> self)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Round((TSelf)self);

    public static TSelf Subtract<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> left, TSelf right)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Subtract((TSelf)left, right);

    public static TSelf TensorProduct<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> left, TSelf right)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.TensorProduct((TSelf)left, right);

    public static TSelf Zip<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> first, TSelf second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping)
        where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Zip((TSelf)first, second, elementMapping);
}

public record RowVector<TRealNumber>(IBoxedRowVector<TRealNumber> Self)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(RowVector<TRealNumber>? other) =>
        Self.Equals(other?.Self);

    public override int GetHashCode() =>
        Self.GetHashCode();

    public TRealNumber[] Entries =>
        Self.Entries;

    public TRealNumber this[int index] =>
        Self[index];

    public TRealNumber InnerProduct(RowVector<TRealNumber> right) =>
        Self.InnerProduct(right.Self);

    public TRealNumber Multiply(ColumnVector<TRealNumber> right) =>
        Self.Multiply(right.Self);

    public TRealNumber Sum() =>
        Self.Sum();

    public ColumnVector<TRealNumber> Transpose() =>
        ColumnVector<TRealNumber>.V(Self.Transpose());

    public int Length() =>
        Self.Length();

    public RowVector<TRealNumber> Add(RowVector<TRealNumber> right) =>
        U(Self.Add(right.Self));

    public RowVector<TRealNumber> AdditiveInverse() =>
        U(Self.AdditiveInverse());

    public RowVector<TRealNumber> Map(Func<TRealNumber, TRealNumber> elementMapping) =>
        U(Self.Map(elementMapping));

    public RowVector<TRealNumber> Multiply(TRealNumber scalar) =>
        U(Self.Multiply(scalar));

    public RowVector<TRealNumber> Normalized() =>
        U(Self.Normalized());

    public RowVector<TRealNumber> Orthonormal() =>
        U(Self.Orthonormal());

    public RowVector<TRealNumber> Round() =>
        U(Self.Round());

    public RowVector<TRealNumber> Subtract(RowVector<TRealNumber> right) =>
        U(Self.Subtract(right.Self));

    public RowVector<TRealNumber> TensorProduct(RowVector<TRealNumber> right) =>
        U(Self.TensorProduct(right.Self));

    public RowVector<TRealNumber> Zip(RowVector<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
        U(Self.Zip(second.Self, elementMapping));

    public TRealNumber Distance(RowVector<TRealNumber> right) =>
        Self.Distance(right.Self);

    public TRealNumber Norm() =>
        Self.Norm();

    public static RowVector<TRealNumber> U(IBoxedRowVector<TRealNumber> vector) =>
        new(vector);

    public static RowVector<TRealNumber> operator +(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.Add(right);

    public static RowVector<TRealNumber> operator -(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.Subtract(right);

    public static RowVector<TRealNumber> operator -(RowVector<TRealNumber> self) =>
        self.AdditiveInverse();

    public static TRealNumber operator *(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.InnerProduct(right);

    public static RowVector<TRealNumber> operator *(TRealNumber scalar, RowVector<TRealNumber> self) =>
        self.Multiply(scalar);
}