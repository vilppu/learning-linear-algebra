﻿using System.Numerics;
using Computation.Infrastructure;
using Computation.Matrices.Real;
using Computation.Numbers;

namespace Computation.Managed.Real;

public record SquareMatrix<TRealNumber>(TRealNumber[,] Entries)
    : ISquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static SquareMatrix<TRealNumber> M(TRealNumber[,] entries) =>
        new(entries);

    public static SquareMatrix<TRealNumber> M(int[,] entries) =>
        M(entries.NumberOfRows(), entries.NumberOfColumns(), (i, j) => RealNumber<TRealNumber>.R(entries[i, j]));

    public static SquareMatrix<TRealNumber> M(float[,] entries) =>
        M(entries.NumberOfRows(), entries.NumberOfColumns(), (i, j) => RealNumber<TRealNumber>.R(entries[i, j]));

    public static SquareMatrix<TRealNumber> M(double[,] entries) =>
        M(entries.NumberOfRows(), entries.NumberOfColumns(), (i, j) => RealNumber<TRealNumber>.R(entries[i, j]));

    public static SquareMatrix<TRealNumber> M(int m, int n, Func<int, int, TRealNumber> initializer) =>
        new(TwoDimensionalArray.Initialize(m, n, initializer));

    public static SquareMatrix<TRealNumber> Zero(int m, int n) =>
         M(m, m, (i, j) => TRealNumber.Zero);

    public static SquareMatrix<TRealNumber> M(int m, Func<int, int, TRealNumber> initializer) =>
        new(TwoDimensionalArray.Initialize(m, m, initializer));

    public static SquareMatrix<TRealNumber> Zero(int m) =>
         M(m, (i, j) => TRealNumber.Zero);

    public static SquareMatrix<TRealNumber> Identity(int m) =>
         M(m, (i, j) => i == j ? TRealNumber.One : TRealNumber.Zero);

    public static int M(SquareMatrix<TRealNumber> matrix) =>
        matrix.Entries.NumberOfRows();

    public static int N(SquareMatrix<TRealNumber> matrix) =>
        matrix.Entries.NumberOfColumns();

    public static IEnumerable<(int i, int j)> Indices(SquareMatrix<TRealNumber> matrix) =>
        matrix.Entries.Indices();

    public static IEnumerable<TRealNumber> Row(SquareMatrix<TRealNumber> matrix, int i) =>
        matrix.Entries.Row(i);

    public static IEnumerable<IEnumerable<TRealNumber>> Rows(SquareMatrix<TRealNumber> matrix) =>
        matrix.Entries.Rows();

    public static IEnumerable<TRealNumber> Column(SquareMatrix<TRealNumber> matrix, int j) =>
        matrix.Entries.Column(j);

    public static IEnumerable<IEnumerable<TRealNumber>> Columns(SquareMatrix<TRealNumber> matrix) =>
        matrix.Entries.Columns();

    public static SquareMatrix<TRealNumber> Map(SquareMatrix<TRealNumber> matrix, Func<TRealNumber, TRealNumber> elementMapping) =>
         M(matrix.M(), (i, j) => elementMapping(matrix[i, j]));

    public static SquareMatrix<TRealNumber> Zip(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
         M(left.M(), (i, j) => elementMapping(left[i, j], right[i, j]));

    public static SquareMatrix<TRealNumber> AdditiveInverse(SquareMatrix<TRealNumber> matrix) =>
        matrix.Map(entry => entry * TRealNumber.NegativeOne);

    public static SquareMatrix<TRealNumber> Transpose(SquareMatrix<TRealNumber> matrix) =>
        M(matrix.N(), (i, j) => matrix[j, i]);

    public static SquareMatrix<TRealNumber> Round(SquareMatrix<TRealNumber> matrix) =>
        matrix.Map(entry => entry.Round());

    // TODO: Move to linear vector space
    public static bool IsIdentity(SquareMatrix<TRealNumber> matrix) =>
        Indices(matrix).Aggregate(true,
            (identity, x) => x.i == x.j
            ? matrix[x.i, x.j].Round() == TRealNumber.One
            : matrix[x.i, x.j].Round() == TRealNumber.Zero);

    public static SquareMatrix<TRealNumber> Add(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
         left.Zip(right, (x, y) => x + y);

    public static SquareMatrix<TRealNumber> Subtract(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
         left.Zip(right, (x, y) => x - y);

    public static SquareMatrix<TRealNumber> Multiply(TRealNumber scalar, SquareMatrix<TRealNumber> matrix) =>
         M(matrix.M(), (i, j) => scalar * matrix[i, j]);

    public static SquareMatrix<TRealNumber> Multiply(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        M(left.M(), (i, j) =>
            left.Row(i).Zip(right.Column(j), (x, y) => x * y).Aggregate(TRealNumber.Zero, (x, y) => x + y)
        );

    public static ColumnVector<TRealNumber> Act(SquareMatrix<TRealNumber> matrix, ColumnVector<TRealNumber> vector) =>
        ColumnVector<TRealNumber>.V(matrix.M(), i => matrix.Row(i).Zip(vector.Entries, (x, y) => x * y).Aggregate(TRealNumber.Zero, (x, y) => x + y));

    public static RowVector<TRealNumber> Act(RowVector<TRealNumber> vector, SquareMatrix<TRealNumber> matrix) =>
        RowVector<TRealNumber>.U(matrix.N(), i => matrix.Row(i).Zip(vector.Entries, (x, y) => x * y).Aggregate(TRealNumber.Zero, (x, y) => x + y));

    // TODO: Move to linear vector space
    public static SquareMatrix<TRealNumber> TensorProduct(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        M(left.M() * right.M(),
          (j, k) => left[j / right.N(), k / right.M()] * right[j % right.N(), k % right.M()]);

    // TODO: Move to linear vector space
    public static SquareMatrix<TRealNumber> Commutator(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
       left * right - right * left;

    public static bool AreEquivalent(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.Entries.Cast<TRealNumber>().SequenceEqual(right.Entries.Cast<TRealNumber>());

    public TRealNumber this[int i, int j] => Entries[i, j];
    public static SquareMatrix<TRealNumber> operator +(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) => Add(left, right);
    public static SquareMatrix<TRealNumber> operator -(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) => Subtract(left, right);
    public static SquareMatrix<TRealNumber> operator *(TRealNumber scalar, SquareMatrix<TRealNumber> matrix) => Multiply(scalar, matrix);
    public static SquareMatrix<TRealNumber> operator *(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) => Multiply(left, right);
    public static ColumnVector<TRealNumber> operator *(SquareMatrix<TRealNumber> matrix, ColumnVector<TRealNumber> vector) => Act(matrix, vector);
    public static RowVector<TRealNumber> operator *(RowVector<TRealNumber> vector, SquareMatrix<TRealNumber> matrix) => Act(vector, matrix);
    public static SquareMatrix<TRealNumber> operator -(SquareMatrix<TRealNumber> vector) => AdditiveInverse(vector);
}
