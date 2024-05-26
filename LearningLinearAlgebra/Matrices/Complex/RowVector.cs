using System.Collections;
using System.Numerics;
using LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace;
using LearningLinearAlgebra.Matrices.Complex;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.Matrices.Complex;

public record RowVector<TRealNumber>(ComplexNumber<TRealNumber>[] Entries)
    : IRowVector<RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    IEnumerator IEnumerable.GetEnumerator() => Entries.GetEnumerator();

    public IEnumerator<ComplexNumber<TRealNumber>> GetEnumerator() => Entries.AsEnumerable().GetEnumerator();

    public static RowVector<TRealNumber> U(ComplexNumber<TRealNumber>[] entries) =>
        new(entries);

    public static RowVector<TRealNumber> U(IEnumerable<ComplexNumber<TRealNumber>> entries) =>
        new(entries.ToArray());

    public static RowVector<TRealNumber> U(int length, Func<int, ComplexNumber<TRealNumber>> initializer) =>
        U(Enumerable.Range(0, length).Select(index => initializer(index)));

    public static RowVector<TRealNumber> Zero(int length) =>
        U(Enumerable.Repeat(ComplexNumber<TRealNumber>.Zero, length).ToArray());

    public static bool AreEquivalent(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.Entries.Cast<ComplexNumber<TRealNumber>>().SequenceEqual(right.Entries.Cast<ComplexNumber<TRealNumber>>());

    public static RowVector<TRealNumber> Add(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a + b);

    // TODO: Move to linear vector space
    public static ColumnVector<TRealNumber> Adjoint(RowVector<TRealNumber> vector) =>
        ColumnVector<TRealNumber>.Conjucate(RowVector<TRealNumber>.Transpose(vector));

    public static RowVector<TRealNumber> Conjucate(RowVector<TRealNumber> vector) =>
        vector.Map(ComplexNumber<TRealNumber>.Conjucate);

    public static RowVector<TRealNumber> AdditiveInverse(RowVector<TRealNumber> vector) =>
        vector.Map(entry => -entry);

    public static RowVector<TRealNumber> Map(RowVector<TRealNumber> source, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
        U(source.Select(value => elementMapping(value)));

    public static RowVector<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar, RowVector<TRealNumber> vector) =>
        vector.Map(entry => entry * scalar);

    public static RowVector<TRealNumber> Multiply(TRealNumber scalar, RowVector<TRealNumber> vector) =>
        vector.Map(entry => entry * scalar);

    public static RowVector<TRealNumber> Normalized(RowVector<TRealNumber> vector) =>
        ComplexNumber<TRealNumber>.One / Norm(vector) * vector;

    // TODO: Move to linear vector space
    public static RowVector<TRealNumber> Orthonormal(RowVector<TRealNumber> vector) =>
        ComplexNumber<TRealNumber>.One / Norm(vector) * vector;

    public static RowVector<TRealNumber> Round(RowVector<TRealNumber> vector) =>
        vector.Map(entry => entry.Round());

    public static RowVector<TRealNumber> Subtract(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a - b);

    public static ColumnVector<TRealNumber> Transpose(RowVector<TRealNumber> vector) =>
        ColumnVector<TRealNumber>.V(vector.Entries);

    // TODO: Move to linear vector space
    public static RowVector<TRealNumber> TensorProduct(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        U(left.SelectMany(leftElement => right.Select(rightElement => leftElement * rightElement)));

    public static RowVector<TRealNumber> Zip(RowVector<TRealNumber> first, RowVector<TRealNumber> second, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
        U(first.Entries.Zip(second.Entries, elementMapping));

    // TODO: Move to linear vector space
    public static ComplexNumber<TRealNumber> InnerProduct(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a * ComplexNumber<TRealNumber>.Conjucate(b)).Sum();

    public static ComplexNumber<TRealNumber> Multiply(RowVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Entries.Zip(right.Entries, (a, b) => a * b).Aggregate(ComplexNumber<TRealNumber>.Zero, (a, b) => a + b);

    public static ComplexNumber<TRealNumber> Sum(RowVector<TRealNumber> vector) =>
        vector.Aggregate(ComplexNumber<TRealNumber>.Zero, (a, b) => a + b);

    public static int Length(RowVector<TRealNumber> vector) =>
        vector.Entries.Length;

    public static TRealNumber Distance(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        Norm(left - right);

    // TODO: Move to linear vector space?
    public static TRealNumber Norm(RowVector<TRealNumber> vector) =>
        ComplexNumber<TRealNumber>.Sqrt(vector * vector).Real;

    public ComplexNumber<TRealNumber> this[int index] => Entries[index];
    public static RowVector<TRealNumber> operator +(RowVector<TRealNumber> left, RowVector<TRealNumber> right) => Add(left, right);
    public static RowVector<TRealNumber> operator -(RowVector<TRealNumber> left, RowVector<TRealNumber> right) => Subtract(left, right);
    public static RowVector<TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, RowVector<TRealNumber> vector) => Multiply(scalar, vector);
    public static RowVector<TRealNumber> operator *(TRealNumber scalar, RowVector<TRealNumber> vector) => Multiply(scalar, vector);
    public static ComplexNumber<TRealNumber> operator *(RowVector<TRealNumber> left, ColumnVector<TRealNumber> right) => Multiply(left, right);
    public static ComplexNumber<TRealNumber> operator *(RowVector<TRealNumber> left, RowVector<TRealNumber> right) => InnerProduct(left, right);
    public static RowVector<TRealNumber> operator -(RowVector<TRealNumber> vector) => AdditiveInverse(vector);
}
