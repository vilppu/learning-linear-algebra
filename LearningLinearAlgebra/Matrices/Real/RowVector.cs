﻿using System.Collections;
using System.Numerics;
using LearningLinearAlgebra.Matrices.Real;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.Matrices.Real;

public record RowVector<TRealNumber>(TRealNumber[] Entries)
    : IRowVector<RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public IEnumerator<TRealNumber> GetEnumerator() => Entries.AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Entries.GetEnumerator();

    public static RowVector<TRealNumber> V(TRealNumber[] entries) =>
        new(entries);

    public static RowVector<TRealNumber> V(float[] entries) =>
        V(entries.Select(RealNumber<TRealNumber>.R));

    public static RowVector<TRealNumber> V(IEnumerable<TRealNumber> entries) =>
        new(entries.ToArray());

    public static RowVector<TRealNumber> V(int length, Func<int, TRealNumber> initializer) =>
        V(Enumerable.Range(0, length).Select(index => initializer(index)));

    public static RowVector<TRealNumber> Zero(int length) =>
        V(Enumerable.Repeat(TRealNumber.Zero, length).ToArray());

    public static bool AreEquivalent(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.Entries.Cast<TRealNumber>().SequenceEqual(right.Entries.Cast<TRealNumber>());

    public static RowVector<TRealNumber> Add(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a + b);

    public static RowVector<TRealNumber> AdditiveInverse(RowVector<TRealNumber> vector) =>
        vector.Map(entry => -entry);

    public static RowVector<TRealNumber> Map(RowVector<TRealNumber> source, Func<TRealNumber, TRealNumber> elementMapping) =>
        V(source.Select(value => elementMapping(value)));

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
        V(right.SelectMany(rightElement => left.Select(leftElement => leftElement * rightElement)));

    public static RowVector<TRealNumber> Zip(RowVector<TRealNumber> first, RowVector<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
        V(first.Entries.Zip(second.Entries, elementMapping));

    // TODO: This is wrong
    public static TRealNumber InnerProduct(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.Zip(right, (a, b) => a * b).Sum();

    public static TRealNumber Multiply(RowVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Entries.Zip(right.Entries, (a, b) => a * b).Aggregate(TRealNumber.Zero, (a, b) => a + b);

    public static TRealNumber Sum(RowVector<TRealNumber> vector) =>
        vector.Aggregate(TRealNumber.Zero, (a, b) => a + b);

    public static int Length(RowVector<TRealNumber> vector) =>
        vector.Entries.Length;

    public static TRealNumber Distance(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        Norm(left - right);

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
