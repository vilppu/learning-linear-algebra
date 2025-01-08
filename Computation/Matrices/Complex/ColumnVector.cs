using System.Numerics;
using Computation.Numbers;

namespace Computation.Matrices.Complex;

// TODO: Remove and have generic math interfaces only for Bra (or share same interface with Bra?)
public interface IColumnVector<TSelf, out TRowVector, TRealNumber>
    where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf V(ComplexNumber<double>[] entries);
    public static abstract TSelf V(ComplexNumber<float>[] entries);
    public static abstract TSelf V(ComplexNumber<TRealNumber>[] entries);
    public static abstract TSelf V(IEnumerable<ComplexNumber<TRealNumber>> entries);
    public static abstract TSelf V(int length, Func<int, ComplexNumber<TRealNumber>> initializer);

    public static abstract ComplexNumber<TRealNumber> InnerProduct(TSelf left, TSelf right);
    public static abstract ComplexNumber<TRealNumber> Sum(TSelf vector);
    public static abstract int Length(TSelf vector);
    public static abstract TRealNumber Distance(TSelf left, TSelf right);
    public static abstract TRealNumber Norm(TSelf vector);
    public static abstract TRowVector Adjoint(TSelf self);
    public static abstract TRowVector Transpose(TSelf self);
    public static abstract TSelf Add(TSelf left, TSelf right);
    public static abstract TSelf AdditiveInverse(TSelf self);
    public static abstract TSelf Conjucate(TSelf self);
    public static abstract TSelf Map(TSelf source, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
    public static abstract TSelf Multiply(ComplexNumber<TRealNumber> scalar, TSelf self);
    public static abstract TSelf Multiply(TRealNumber scalar, TSelf self);
    public static abstract TSelf Normalized(TSelf self);
    public static abstract TSelf Orthonormal(TSelf vector);
    public static abstract TSelf Round(TSelf self);
    public static abstract TSelf Subtract(TSelf left, TSelf right);
    public static abstract TSelf TensorProduct(TSelf left, TSelf right);
    public static abstract TSelf Zero(int length);
    public static abstract TSelf Zip(TSelf first, TSelf second, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);

    public ComplexNumber<TRealNumber> this[int index] { get; }
    public ComplexNumber<TRealNumber>[] Entries { get; }
    public static abstract TSelf operator +(TSelf left, TSelf right);
    public static abstract TSelf operator -(TSelf left, TSelf right);
    public static abstract TSelf operator -(TSelf self);
    public static abstract TSelf operator *(ComplexNumber<TRealNumber> scalar, TSelf self);
    public static abstract TSelf operator *(TRealNumber scalar, TSelf self);
    public static abstract ComplexNumber<TRealNumber> operator *(TSelf left, TSelf right);
}

public static class ColumnVector
{
    public static ComplexNumber<TRealNumber> InnerProduct<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> left, TSelf right)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.InnerProduct((TSelf)left, right);

    public static ComplexNumber<TRealNumber> Sum<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Sum((TSelf)self);

    public static int Length<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Length((TSelf)self);

    public static TRowVector Adjoint<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Adjoint((TSelf)self);

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

    public static TSelf Conjucate<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Conjucate((TSelf)self);

    public static TSelf Map<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> source, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Map((TSelf)source, elementMapping);

    public static TSelf Multiply<TSelf, TRowVector, TRealNumber>(this ComplexNumber<TRealNumber> scalar, IColumnVector<TSelf, TRowVector, TRealNumber> self)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Multiply(scalar, (TSelf)self);

    public static TSelf Multiply<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> self, ComplexNumber<TRealNumber> scalar)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Multiply(scalar, (TSelf)self);

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

    public static TSelf Zip<TSelf, TRowVector, TRealNumber>(this IColumnVector<TSelf, TRowVector, TRealNumber> first, TSelf second, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping)
        where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Zip((TSelf)first, (TSelf)second, elementMapping);
}

public record ColumnVector<TRealNumber>(ComplexNumber<TRealNumber>[] Entries)
    : IColumnVector<ColumnVector<TRealNumber>, RowVector<TRealNumber>, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(ColumnVector<TRealNumber>? other) =>
        other?.Entries != null && Entries.SequenceEqual(other.Entries);

    public override int GetHashCode() =>
        Entries.GetHashCode();

    public static ColumnVector<TRealNumber> V(ComplexNumber<double>[] entries) =>
        V(entries.Select(ComplexNumber<TRealNumber>.C));

    public static ColumnVector<TRealNumber> V(ComplexNumber<float>[] entries) =>
        V(entries.Select(ComplexNumber<TRealNumber>.C));

    public static ColumnVector<TRealNumber> V(ComplexNumber<TRealNumber>[] entries) =>
        new(entries);

    public static ColumnVector<TRealNumber> V(IEnumerable<ComplexNumber<TRealNumber>> entries) =>
        new(entries.ToArray());

    public static ColumnVector<TRealNumber> V(int length, Func<int, ComplexNumber<TRealNumber>> initializer) =>
        V(Enumerable.Range(0, length).Select(initializer));

    public static ColumnVector<TRealNumber> Zero(int length) =>
        V(Enumerable.Repeat(ComplexNumber<TRealNumber>.Zero, length).ToArray());

    public static bool AreEquivalent(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Entries.SequenceEqual(right.Entries);

    public static ColumnVector<TRealNumber> Add(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a + b);

    public static RowVector<TRealNumber> Adjoint(ColumnVector<TRealNumber> vector) =>
        RowVector<TRealNumber>.Conjucate(Transpose(vector));

    public static ColumnVector<TRealNumber> Conjucate(ColumnVector<TRealNumber> vector) =>
        vector.Map(ComplexNumber<TRealNumber>.Conjucate);

    public static ColumnVector<TRealNumber> AdditiveInverse(ColumnVector<TRealNumber> vector) =>
        vector.Map(entry => -entry);

    public static ColumnVector<TRealNumber> Map(ColumnVector<TRealNumber> source, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
        V(source.Entries.Select(elementMapping));

    public static ColumnVector<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar, ColumnVector<TRealNumber> vector) =>
        vector.Map(entry => entry * scalar);

    public static ColumnVector<TRealNumber> Multiply(TRealNumber scalar, ColumnVector<TRealNumber> vector) =>
        vector.Map(entry => entry * scalar);

    public static ColumnVector<TRealNumber> Normalized(ColumnVector<TRealNumber> vector) =>
        ComplexNumber<TRealNumber>.One / Norm(vector) * vector;

    public static ColumnVector<TRealNumber> Orthonormal(ColumnVector<TRealNumber> vector) =>
        ComplexNumber<TRealNumber>.One / Norm(vector) * vector;

    public static ColumnVector<TRealNumber> Round(ColumnVector<TRealNumber> vector) =>
        vector.Map(entry => entry.Round());

    public static ColumnVector<TRealNumber> Subtract(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a - b);

    public static RowVector<TRealNumber> Transpose(ColumnVector<TRealNumber> vector) =>
        RowVector<TRealNumber>.U(vector.Entries);

    public static ColumnVector<TRealNumber> TensorProduct(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        V(left.Entries.SelectMany(leftElement => right.Entries.Select(rightElement => leftElement * rightElement)));

    public static ColumnVector<TRealNumber> Zip(ColumnVector<TRealNumber> first, ColumnVector<TRealNumber> second, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
        V(first.Entries.Zip(second.Entries, elementMapping));

    public static ComplexNumber<TRealNumber> InnerProduct(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a * ComplexNumber<TRealNumber>.Conjucate(b)).Sum();

    public static ComplexNumber<TRealNumber> Sum(ColumnVector<TRealNumber> vector) =>
        vector.Entries.Aggregate(ComplexNumber<TRealNumber>.Zero, (a, b) => a + b);

    public static int Length(ColumnVector<TRealNumber> vector) =>
        vector.Entries.Length;

    public static TRealNumber Distance(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        Norm(left - right);

    public static TRealNumber Norm(ColumnVector<TRealNumber> vector) =>
        ComplexNumber<TRealNumber>.Sqrt(vector * vector).Real;

    public ComplexNumber<TRealNumber> this[int index] => Entries[index];
    public static ColumnVector<TRealNumber> operator +(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) => Add(left, right);
    public static ColumnVector<TRealNumber> operator -(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) => Subtract(left, right);
    public static ColumnVector<TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, ColumnVector<TRealNumber> vector) => Multiply(scalar, vector);
    public static ColumnVector<TRealNumber> operator *(TRealNumber scalar, ColumnVector<TRealNumber> vector) => Multiply(scalar, vector);
    public static ComplexNumber<TRealNumber> operator *(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) => InnerProduct(left, right);
    public static ColumnVector<TRealNumber> operator -(ColumnVector<TRealNumber> vector) => AdditiveInverse(vector);
}