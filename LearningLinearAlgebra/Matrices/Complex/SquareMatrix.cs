﻿using System.Numerics;
using LearningLinearAlgebra.Matrices.Complex;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.Matrices.Complex;

public record SquareMatrix<TRealNumber>(ComplexNumber<TRealNumber>[,] Entries)
    : ISquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static SquareMatrix<TRealNumber> M(ComplexNumber<TRealNumber>[,] entries) =>
        new(entries);

    public static SquareMatrix<TRealNumber> M(ComplexNumber<float>[,] entries) =>
        M(entries.GetLength(0), entries.GetLength(1), (i, j) => ComplexNumber<TRealNumber>.C(entries[i, j]));

    public static SquareMatrix<TRealNumber> M(ComplexNumber<double>[,] entries) =>
        M(entries.GetLength(0), entries.GetLength(1), (i, j) => ComplexNumber<TRealNumber>.C(entries[i, j]));

    public static SquareMatrix<TRealNumber> M(int m, int n, Func<int, int, ComplexNumber<TRealNumber>> initializer) =>
        new(TwoDimensionalArray<ComplexNumber<TRealNumber>>.Initialize(m, n, initializer));

    public static SquareMatrix<TRealNumber> Zero(int m, int n) =>
         M(m, m, (i, j) => TRealNumber.Zero);

    public static SquareMatrix<TRealNumber> M(int m, Func<int, int, ComplexNumber<TRealNumber>> initializer) =>
        new(TwoDimensionalArray<ComplexNumber<TRealNumber>>.Initialize(m, m, initializer));

    public static SquareMatrix<TRealNumber> Zero(int m) =>
         M(m, (i, j) => TRealNumber.Zero);

    public static SquareMatrix<TRealNumber> Identity(int m) =>
         M(m, (i, j) => i == j ? TRealNumber.One : TRealNumber.Zero);

    public static int M(SquareMatrix<TRealNumber> matrix) =>
        matrix.Entries.GetLength(0);

    public static int N(SquareMatrix<TRealNumber> matrix) =>
        matrix.Entries.GetLength(1);

    public static IEnumerable<(int i, int j)> Indices(SquareMatrix<TRealNumber> matrix) =>
        Enumerable.Range(0, matrix.M()).SelectMany(i => Enumerable.Range(0, matrix.N()).Select(j => (i, j)));

    public static IEnumerable<ComplexNumber<TRealNumber>> Row(SquareMatrix<TRealNumber> matrix, int i) =>
        Enumerable.Range(0, matrix.N()).Select(j => matrix[i, j]);

    public static IEnumerable<ComplexNumber<TRealNumber>> Column(SquareMatrix<TRealNumber> matrix, int j) =>
        Enumerable.Range(0, matrix.M()).Select(i => matrix[i, j]);

    public static SquareMatrix<TRealNumber> Map(SquareMatrix<TRealNumber> matrix, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
         M(matrix.M(), (i, j) => elementMapping(matrix[i, j]));

    public static SquareMatrix<TRealNumber> Zip(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
         M(left.M(), (i, j) => elementMapping(left[i, j], right[i, j]));

    public static SquareMatrix<TRealNumber> AdditiveInverse(SquareMatrix<TRealNumber> matrix) =>
        matrix.Map(entry => entry * ComplexNumber<TRealNumber>.NegativeOne);

    public static SquareMatrix<TRealNumber> Transpose(SquareMatrix<TRealNumber> matrix) =>
        M(matrix.N(), (i, j) => matrix[j, i]);

    // TODO: Move to linear vector space
    public static SquareMatrix<TRealNumber> Conjucate(SquareMatrix<TRealNumber> matrix) =>
        M(matrix.N(), (i, j) => ComplexNumber<TRealNumber>.Conjucate(matrix[i, j]));

    public static SquareMatrix<TRealNumber> Adjoint(SquareMatrix<TRealNumber> matrix) =>
       Transpose(Conjucate(matrix));

    public static SquareMatrix<TRealNumber> Round(SquareMatrix<TRealNumber> matrix) =>
        matrix.Map(entry => entry.Round());

    // TODO: Move to linear vector space
    public static bool IsIdentity(SquareMatrix<TRealNumber> matrix) =>
        Indices(matrix).Aggregate(true,
            (identity, x) => x.i == x.j
            ? matrix[x.i, x.j].Round() == TRealNumber.One
            : matrix[x.i, x.j].Round() == TRealNumber.Zero);

    // TODO: Move to linear vector space
    public static bool IsHermitian(SquareMatrix<TRealNumber> matrix) =>
       AreEquivalent(matrix, Adjoint(matrix));

    // TODO: Move to linear vector space
    public static bool IsUnitary(SquareMatrix<TRealNumber> matrix) =>
       IsIdentity(matrix * Adjoint(matrix)) &&
       IsIdentity(Adjoint(matrix) * matrix);

    public static SquareMatrix<TRealNumber> Add(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
         left.Zip(right, (x, y) => x + y);

    public static SquareMatrix<TRealNumber> Subtract(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
         left.Zip(right, (x, y) => x - y);

    public static SquareMatrix<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar, SquareMatrix<TRealNumber> matrix) =>
         M(matrix.M(), (i, j) => scalar * matrix[i, j]);

    public static SquareMatrix<TRealNumber> Multiply(TRealNumber scalar, SquareMatrix<TRealNumber> matrix) =>
         M(matrix.M(), (i, j) => scalar * matrix[i, j]);

    public static SquareMatrix<TRealNumber> Multiply(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        M(left.M(), (i, j) =>
            left.Row(i).Zip(right.Column(j), (x, y) => x * y).Aggregate(ComplexNumber<TRealNumber>.Zero, (x, y) => x + y)
        );

    public static ColumnVector<TRealNumber> Act(SquareMatrix<TRealNumber> matrix, ColumnVector<TRealNumber> vector) =>
        ColumnVector<TRealNumber>.V(matrix.M(), i => matrix.Row(i).Zip(vector, (x, y) => x * y).Aggregate(ComplexNumber<TRealNumber>.Zero, (x, y) => x + y));

    public static RowVector<TRealNumber> Act(RowVector<TRealNumber> vector, SquareMatrix<TRealNumber> matrix) =>
        RowVector<TRealNumber>.U(matrix.N(), i => matrix.Row(i).Zip(vector, (x, y) => x * y).Aggregate(ComplexNumber<TRealNumber>.Zero, (x, y) => x + y));

    // TODO: Move to linear vector space
    public static SquareMatrix<TRealNumber> TensorProduct(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        M(left.M() * right.M(),
          (j, k) => left[j / right.N(), k / right.M()] * right[j % right.N(), k % right.M()]);

    // TODO: Move to linear vector space
    public static SquareMatrix<TRealNumber> Commutator(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
       left * right - right * left;

    public static bool AreEquivalent(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.Entries.Cast<ComplexNumber<TRealNumber>>().SequenceEqual(right.Entries.Cast<ComplexNumber<TRealNumber>>());

    public ComplexNumber<TRealNumber> this[int i, int j] => Entries[i, j];
    public static SquareMatrix<TRealNumber> operator +(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) => Add(left, right);
    public static SquareMatrix<TRealNumber> operator -(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) => Subtract(left, right);
    public static SquareMatrix<TRealNumber> operator *(TRealNumber scalar, SquareMatrix<TRealNumber> matrix) => Multiply(scalar, matrix);
    public static SquareMatrix<TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, SquareMatrix<TRealNumber> matrix) => Multiply(scalar, matrix);
    public static SquareMatrix<TRealNumber> operator *(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) => Multiply(left, right);
    public static ColumnVector<TRealNumber> operator *(SquareMatrix<TRealNumber> matrix, ColumnVector<TRealNumber> vector) => Act(matrix, vector);
    public static RowVector<TRealNumber> operator *(RowVector<TRealNumber> vector, SquareMatrix<TRealNumber> matrix) => Act(vector, matrix);
    public static SquareMatrix<TRealNumber> operator -(SquareMatrix<TRealNumber> vector) => AdditiveInverse(vector);
}
