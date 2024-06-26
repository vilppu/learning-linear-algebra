﻿using System.Collections;
using System.Numerics;
using LearningLinearAlgebra.Matrices.Real;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.Matrices.Real;

public record ColumnVector<TRealNumber>(TRealNumber[] Entries)
    : IColumnVector<ColumnVector<TRealNumber>, RowVector<TRealNumber>, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public IEnumerator<TRealNumber> GetEnumerator() => Entries.AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Entries.GetEnumerator();

    public static ColumnVector<TRealNumber> V(TRealNumber[] entries) =>
        new(entries);

    public static ColumnVector<TRealNumber> V(float[] entries) =>
        V(entries.Select(RealNumber<TRealNumber>.R));

    public static ColumnVector<TRealNumber> V(IEnumerable<TRealNumber> entries) =>
        new(entries.ToArray());

    public static ColumnVector<TRealNumber> V(int length, Func<int, TRealNumber> initializer) =>
        V(Enumerable.Range(0, length).Select(index => initializer(index)));

    public static ColumnVector<TRealNumber> Zero(int length) =>
        V(Enumerable.Repeat(TRealNumber.Zero, length).ToArray());

    public static bool AreEquivalent(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Entries.Cast<TRealNumber>().SequenceEqual(right.Entries.Cast<TRealNumber>());

    public static ColumnVector<TRealNumber> Add(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a + b);

    public static ColumnVector<TRealNumber> AdditiveInverse(ColumnVector<TRealNumber> vector) =>
        vector.Map(entry => -entry);

    public static ColumnVector<TRealNumber> Map(ColumnVector<TRealNumber> source, Func<TRealNumber, TRealNumber> elementMapping) =>
        V(source.Select(value => elementMapping(value)));

    public static ColumnVector<TRealNumber> Multiply(TRealNumber scalar, ColumnVector<TRealNumber> vector) =>
        vector.Map(entry => entry * scalar);

    public static ColumnVector<TRealNumber> Normalized(ColumnVector<TRealNumber> vector) =>
        TRealNumber.One / Norm(vector) * vector;

    // TODO: Move to linear vector space
    public static ColumnVector<TRealNumber> Orthonormal(ColumnVector<TRealNumber> vector) =>
        TRealNumber.One / Norm(vector) * vector;

    public static ColumnVector<TRealNumber> Round(ColumnVector<TRealNumber> vector) =>
        vector.Map(entry => entry.Round());

    public static ColumnVector<TRealNumber> Subtract(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a - b);

    public static RowVector<TRealNumber> Transpose(ColumnVector<TRealNumber> vector) =>
        RowVector<TRealNumber>.V(vector.Entries);

    public static ColumnVector<TRealNumber> TensorProduct(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        V(right.SelectMany(rightElement => left.Select(leftElement => leftElement * rightElement)));

    public static ColumnVector<TRealNumber> Zip(ColumnVector<TRealNumber> first, ColumnVector<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
        V(first.Entries.Zip(second.Entries, elementMapping));

    // TODO: Move to linear vector space
    public static TRealNumber InnerProduct(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a * b).Sum();

    public static TRealNumber Sum(ColumnVector<TRealNumber> vector) =>
        vector.Aggregate(TRealNumber.Zero, (a, b) => a + b);

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
