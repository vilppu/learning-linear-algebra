using System.Numerics;
using LearningLinearAlgebra.Cuda;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.Matrices.Real;

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

    public static TSelf Multiply<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self, TRealNumber scalar)
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

public record ColumnVector<TRealNumber>(TRealNumber[] Entries)
    : IColumnVector<ColumnVector<TRealNumber>, RowVector<TRealNumber>, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(ColumnVector<TRealNumber>? other) =>
        other?.Entries != null && Entries.SequenceEqual(other.Entries);

    public override int GetHashCode() =>
        Entries.GetHashCode();

    public static ColumnVector<TRealNumber> V(TRealNumber[] entries) =>
        new(entries);

    public static ColumnVector<TRealNumber> V(int[] entries) =>
        new(V(entries.Select(RealNumber<TRealNumber>.R)));

    public static ColumnVector<TRealNumber> V(float[] entries) =>
        new(V(entries.Select(RealNumber<TRealNumber>.R)));

    public static ColumnVector<TRealNumber> V(double[] entries) =>
        new(V(entries.Select((number, _) => RealNumber<TRealNumber>.R(number))));

    public static ColumnVector<TRealNumber> V(IEnumerable<TRealNumber> entries) =>
        new(entries.ToArray());

    public static ColumnVector<TRealNumber> V(int length, Func<int, TRealNumber> initializer) =>
        V(Enumerable.Range(0, length).Select(initializer));

    public static ColumnVector<TRealNumber> Zero(int length) =>
        V(Enumerable.Repeat(TRealNumber.Zero, length).ToArray());

    public static bool AreEquivalent(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Entries.SequenceEqual(right.Entries);

    public static ColumnVector<TRealNumber> Add(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        (left, right) switch
        {
            (ColumnVector<float> l, ColumnVector<float> r) => V(l.Entries.Add(r.Entries)),
            (ColumnVector<double> l, ColumnVector<double> r) => V(l.Entries.Add(r.Entries)),
            _ => left.Zip(right, (a, b) => a + b)
        };

    public static ColumnVector<TRealNumber> AdditiveInverse(ColumnVector<TRealNumber> vector) =>
        vector.Map(entry => -entry);

    public static ColumnVector<TRealNumber> Map(ColumnVector<TRealNumber> source, Func<TRealNumber, TRealNumber> elementMapping) =>
        V(source.Entries.Select(elementMapping));

    public static ColumnVector<TRealNumber> Multiply(TRealNumber scalar, ColumnVector<TRealNumber> vector) =>
        vector.Map(entry => entry * scalar);

    public static ColumnVector<TRealNumber> Normalized(ColumnVector<TRealNumber> vector) =>
        TRealNumber.One / Norm(vector) * vector;

    public static ColumnVector<TRealNumber> Orthonormal(ColumnVector<TRealNumber> vector) =>
        TRealNumber.One / Norm(vector) * vector;

    public static ColumnVector<TRealNumber> Round(ColumnVector<TRealNumber> vector) =>
        vector.Map(entry => entry.Round());

    public static ColumnVector<TRealNumber> Subtract(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a - b);

    public static RowVector<TRealNumber> Transpose(ColumnVector<TRealNumber> vector) =>
        RowVector<TRealNumber>.U(vector.Entries);

    public static ColumnVector<TRealNumber> TensorProduct(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        V(left.Entries.SelectMany(leftElement => right.Entries.Select(rightElement => leftElement * rightElement)));

    public static ColumnVector<TRealNumber> Zip(ColumnVector<TRealNumber> first, ColumnVector<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
        V(first.Entries.Zip(second.Entries, elementMapping));

    public static TRealNumber InnerProduct(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a * b).Sum();

    public static TRealNumber Sum(ColumnVector<TRealNumber> vector) =>
        vector.Entries.Aggregate(TRealNumber.Zero, (a, b) => a + b);

    public static int Length(ColumnVector<TRealNumber> vector) =>
        vector.Entries.Length;

    public static TRealNumber Distance(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        Norm(left - right);

    public static TRealNumber Norm(ColumnVector<TRealNumber> vector) =>
        TRealNumber.Sqrt(vector * vector);

    public TRealNumber this[int index] => Entries[index];
    public static ColumnVector<TRealNumber> operator +(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) => Add(left, right);
    public static ColumnVector<TRealNumber> operator -(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) => Subtract(left, right);
    public static ColumnVector<TRealNumber> operator *(TRealNumber scalar, ColumnVector<TRealNumber> vector) => Multiply(scalar, vector);
    public static TRealNumber operator *(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) => InnerProduct(left, right);
    public static ColumnVector<TRealNumber> operator -(ColumnVector<TRealNumber> vector) => AdditiveInverse(vector);
}