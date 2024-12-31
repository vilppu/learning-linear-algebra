using System.Numerics;
using Computation.Matrices.Real;
using Computation.Numbers;

namespace Computation.Cuda.Real;

public record ColumnVector<TRealNumber>(TRealNumber[] Entries)
    : IColumnVector<ColumnVector<TRealNumber>, RowVector<TRealNumber>, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static ColumnVector<TRealNumber> V(TRealNumber[] entries) =>
        new(entries);

    public static ColumnVector<TRealNumber> V(int[] entries) =>
        new(V(entries.Select(RealNumber<TRealNumber>.R)));

    public static ColumnVector<TRealNumber> V(float[] entries) =>
        new(V(entries.Select(RealNumber<TRealNumber>.R)));

    public static ColumnVector<TRealNumber> V(double[] entries) =>
        new(V(entries.Select(RealNumber<TRealNumber>.R)));

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
