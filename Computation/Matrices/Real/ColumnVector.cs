using System.Numerics;

namespace Computation.Matrices.Real;

// TODO: Remove and have generic math interfaces only for Bra (or share same interface with Bra?)
public interface IColumnVector<TSelf, out TRowVector, TRealNumber>
    where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf V(double[] entries);
    public static abstract TSelf V(float[] entries);
    public static abstract TSelf V(int[] entries);
    public static abstract TSelf V(TRealNumber[] entries);
    public static abstract TSelf V(IEnumerable<TRealNumber> entries);
    public static abstract TSelf V(int length, Func<int, TRealNumber> initializer);

    public static abstract TRealNumber InnerProduct(TSelf left, TSelf right);
    public static abstract TRealNumber Sum(TSelf vector);
    public static abstract int Length(TSelf vector);
    public static abstract TRealNumber Distance(TSelf left, TSelf right);
    public static abstract TRealNumber Norm(TSelf vector);
    public static abstract TRowVector Transpose(TSelf self);
    public static abstract TSelf Add(TSelf left, TSelf right);
    public static abstract TSelf AdditiveInverse(TSelf self);
    public static abstract TSelf Map(TSelf source, Func<TRealNumber, TRealNumber> elementMapping);
    public static abstract TSelf Multiply(TRealNumber scalar, TSelf self);
    public static abstract TSelf Normalized(TSelf self);
    public static abstract TSelf Orthonormal(TSelf vector);
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
    public static abstract TRealNumber operator *(TSelf left, TSelf right);
}

public static class ColumnVector
{
    public static TRealNumber InnerProduct<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> left, TSelf right)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.InnerProduct((TSelf)left, (TSelf)right);

    public static TRealNumber Sum<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Sum((TSelf)self);

    public static int Length<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Length((TSelf)self);

    public static TRowVector Transpose<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Transpose((TSelf)self);

    public static TRealNumber Distance<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> left, TSelf right)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Distance((TSelf)left, (TSelf)right);

    public static TRealNumber Norm<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Norm((TSelf)self);

    public static TSelf Add<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> left, TSelf right)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Add((TSelf)left, (TSelf)right);

    public static TSelf AdditiveInverse<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.AdditiveInverse((TSelf)self);

    public static TSelf Map<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> source, Func<TRealNumber, TRealNumber> elementMapping)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Map((TSelf)source, elementMapping);

    public static TSelf Multiply<TSelf, TRowVector, TRealNumber>(this TRealNumber scalar, IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Multiply(scalar, (TSelf)self);

    public static TSelf Normalized<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Normalized((TSelf)self);

    public static TSelf Orthonormal<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Orthonormal((TSelf)self);

    public static TSelf Round<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Round((TSelf)self);

    public static TSelf Subtract<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> left, TSelf right)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Subtract((TSelf)left, (TSelf)right);

    public static TSelf TensorProduct<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> left, TSelf right)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.TensorProduct((TSelf)left, (TSelf)right);

    public static TSelf Zip<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> first, TSelf second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Zip((TSelf)first, (TSelf)second, elementMapping);
}

public record ColumnVector<TRealNumber>(IBoxedColumnVector<TRealNumber> Self)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(ColumnVector<TRealNumber>? other) =>
        Self.Equals(other?.Self);

    public override int GetHashCode() =>
        Self.GetHashCode();

    public static ColumnVector<TRealNumber> V(IBoxedColumnVector<TRealNumber> vector) => new(vector);

    public ColumnVector<TRealNumber> Add(ColumnVector<TRealNumber> right) =>
        V(Self.Add(right.Self));

    public ColumnVector<TRealNumber> AdditiveInverse() =>
        V(Self.AdditiveInverse());

    public ColumnVector<TRealNumber> Map(ColumnVector<TRealNumber> source, Func<TRealNumber, TRealNumber> elementMapping) =>
        V(Self.Map(source.Self, elementMapping));

    public ColumnVector<TRealNumber> Multiply(TRealNumber scalar) =>
        V(Self.Multiply(scalar));

    public ColumnVector<TRealNumber> Normalized() =>
        V(Self.Normalized());

    public ColumnVector<TRealNumber> Orthonormal() =>
        V(Self.Orthonormal());

    public ColumnVector<TRealNumber> Round() =>
        V(Self.Round());

    public ColumnVector<TRealNumber> Subtract(ColumnVector<TRealNumber> right) =>
        V(Self.Subtract(right.Self));

    public ColumnVector<TRealNumber> TensorProduct(ColumnVector<TRealNumber> right) =>
        V(Self.TensorProduct(right.Self));

    public ColumnVector<TRealNumber> Zip(ColumnVector<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
        V(Self.Zip(second.Self, elementMapping));

    public TRealNumber InnerProduct(ColumnVector<TRealNumber> right) =>
        Self.InnerProduct(right.Self);

    public TRealNumber Sum() =>
        Self.Sum();

    public TRealNumber[] Entries =>
        Self.Entries;

    public int Length() =>
        Self.Length();

    public RowVector<TRealNumber> Transpose() =>
        RowVector<TRealNumber>.U(Self.Transpose());

    public TRealNumber Distance(ColumnVector<TRealNumber> right) =>
        Self.Distance(right.Self);

    public TRealNumber Norm() =>
        Self.Norm();

    public TRealNumber this[int index] =>
        Self[index];

    public static ColumnVector<TRealNumber> operator +(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Add(right);

    public static ColumnVector<TRealNumber> operator -(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Subtract(right);

    public static ColumnVector<TRealNumber> operator -(ColumnVector<TRealNumber> self) =>
        self.AdditiveInverse();

    public static ColumnVector<TRealNumber> operator *(TRealNumber scalar, ColumnVector<TRealNumber> self) =>
        self.Multiply(scalar);

    public static TRealNumber operator *(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.InnerProduct(right);

    public static TRealNumber operator *(RowVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Multiply(right);
}