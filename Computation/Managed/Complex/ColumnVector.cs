using System.Numerics;
using Computation.Matrices.Complex;
using Computation.Numbers;

namespace Computation.Managed.Complex;

public record ColumnVector<TRealNumber>(ComplexNumber<TRealNumber>[] Entries)
    : IColumnVector<ColumnVector<TRealNumber>, RowVector<TRealNumber>, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
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
