using System.Numerics;
using LearningLinearAlgebra.Cuda;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.Matrices.Real;

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

    public static TSelf Multiply<TSelf, TColumnVector, TRealNumber>(this IRowVector<TSelf, TColumnVector, TRealNumber> self, TRealNumber scalar)
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

public record RowVector<TRealNumber>(TRealNumber[] Entries)
    : IRowVector<RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(RowVector<TRealNumber>? other) =>
        other?.Entries != null && Entries.SequenceEqual(other.Entries);

    public override int GetHashCode() =>
        Entries.GetHashCode();

    public static RowVector<TRealNumber> U(TRealNumber[] entries) =>
        new(entries);

    public static RowVector<TRealNumber> U(int[] entries) =>
        new(U(entries.Select(RealNumber<TRealNumber>.R)));

    public static RowVector<TRealNumber> U(float[] entries) =>
        new(U(entries.Select(RealNumber<TRealNumber>.R)));

    public static RowVector<TRealNumber> U(double[] entries) =>
        new(U(entries.Select((number, number2) => RealNumber<TRealNumber>.R(number))));

    public static RowVector<TRealNumber> U(IEnumerable<TRealNumber> entries) =>
        new(entries.ToArray());

    public static RowVector<TRealNumber> U(int length, Func<int, TRealNumber> initializer) =>
        U(Enumerable.Range(0, length).Select(initializer));

    public static RowVector<TRealNumber> Zero(int length) =>
        U(Enumerable.Repeat(TRealNumber.Zero, length).ToArray());

    public static RowVector<TRealNumber> Add(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        (left, right) switch
        {
            (RowVector<float> leftVector, RowVector<float> rightVector) => U(leftVector.Entries.Add(rightVector.Entries)),
            (RowVector<double> leftVector, RowVector<double> rightVector) => U(leftVector.Entries.Add(rightVector.Entries)),
            _ => left.Zip(right, (a, b) => a + b)
        };

    public static RowVector<TRealNumber> AdditiveInverse(RowVector<TRealNumber> vector) =>
        vector.Map(entry => -entry);

    public static RowVector<TRealNumber> Map(RowVector<TRealNumber> source, Func<TRealNumber, TRealNumber> elementMapping) =>
        U(source.Entries.Select(elementMapping));

    public static RowVector<TRealNumber> Multiply(TRealNumber scalar, RowVector<TRealNumber> vector) =>
        vector.Map(entry => entry * scalar);

    public static RowVector<TRealNumber> Normalized(RowVector<TRealNumber> vector) =>
        TRealNumber.One / Norm(vector) * vector;

    // TODO: Move to linear vector space
    public static RowVector<TRealNumber> Orthonormal(RowVector<TRealNumber> vector) =>
        TRealNumber.One / Norm(vector) * vector;

    public static RowVector<TRealNumber> Round(RowVector<TRealNumber> vector) =>
        vector.Map(entry => entry.Round());

    public static RowVector<TRealNumber> Subtract(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a - b);

    public static ColumnVector<TRealNumber> Transpose(RowVector<TRealNumber> vector) =>
        ColumnVector<TRealNumber>.V(vector.Entries);

    // TODO: Move to linear vector space
    public static RowVector<TRealNumber> TensorProduct(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        U(left.Entries.SelectMany(leftElement => right.Entries.Select(rightElement => leftElement * rightElement)));

    public static RowVector<TRealNumber> Zip(RowVector<TRealNumber> first, RowVector<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
        U(first.Entries.Zip(second.Entries, elementMapping));

    // TODO: Move to linear vector space
    public static TRealNumber InnerProduct(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a * b).Sum();

    public static TRealNumber Multiply(RowVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Entries.Zip(right.Entries, (a, b) => a * b).Aggregate(TRealNumber.Zero, (a, b) => a + b);

    public static TRealNumber Sum(RowVector<TRealNumber> vector) =>
        vector.Entries.Aggregate(TRealNumber.Zero, (a, b) => a + b);

    public static int Length(RowVector<TRealNumber> vector) =>
        vector.Entries.Length;

    public static TRealNumber Distance(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        Norm(left - right);

    // TODO: Move to linear vector space?
    public static TRealNumber Norm(RowVector<TRealNumber> vector) =>
        TRealNumber.Sqrt(vector * vector);

    public TRealNumber this[int index] => Entries[index];
    public static RowVector<TRealNumber> operator +(RowVector<TRealNumber> left, RowVector<TRealNumber> right) => Add(left, right);
    public static RowVector<TRealNumber> operator -(RowVector<TRealNumber> left, RowVector<TRealNumber> right) => Subtract(left, right);
    public static RowVector<TRealNumber> operator *(TRealNumber scalar, RowVector<TRealNumber> vector) => Multiply(scalar, vector);
    public static TRealNumber operator *(RowVector<TRealNumber> left, ColumnVector<TRealNumber> right) => Multiply(left, right);
    public static TRealNumber operator *(RowVector<TRealNumber> left, RowVector<TRealNumber> right) => InnerProduct(left, right);
    public static RowVector<TRealNumber> operator -(RowVector<TRealNumber> vector) => AdditiveInverse(vector);
}
