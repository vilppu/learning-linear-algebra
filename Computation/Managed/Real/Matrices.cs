using System.Numerics;
using Computation.Matrices.Real;

namespace Computation.Managed.Real;

public class Matrices<TRealNumber> : IMatrices<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static Computation.Matrices.Real.SquareMatrix<TRealNumber> M(TRealNumber[,] entries) =>
        BoxedSquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .M(entries)
            .SquareMatrix();

    public static Computation.Matrices.Real.SquareMatrix<TRealNumber> M(float[,] entries) =>
        BoxedSquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .M(entries)
            .SquareMatrix();

    public static Computation.Matrices.Real.SquareMatrix<TRealNumber> M(double[,] entries) =>
        BoxedSquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .M(entries)
            .SquareMatrix();

    public static Computation.Matrices.Real.SquareMatrix<TRealNumber> M(int m, Func<int, int, TRealNumber> initializer) =>
        BoxedSquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .M(m, initializer)
            .SquareMatrix();

    public static Computation.Matrices.Real.SquareMatrix<TRealNumber> Zero(int m) =>
        BoxedSquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .Zero(m)
            .SquareMatrix();

    public static Computation.Matrices.Real.SquareMatrix<TRealNumber> Identity(int m) =>
        BoxedSquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .Identity(m)
            .SquareMatrix();

    public static Computation.Matrices.Real.ColumnVector<TRealNumber> V(TRealNumber[] entries) =>
        BoxedColumnVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .V(entries)
            .ColumnVector();

    public static Computation.Matrices.Real.ColumnVector<TRealNumber> V(float[] entries) =>
        BoxedColumnVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .V(entries)
            .ColumnVector();

    public static Computation.Matrices.Real.ColumnVector<TRealNumber> V(double[] entries) =>
        BoxedColumnVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .V(entries)
            .ColumnVector();

    public static Computation.Matrices.Real.ColumnVector<TRealNumber> V(IEnumerable<TRealNumber> entries) =>
        BoxedColumnVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .V(entries)
            .ColumnVector();

    public static Computation.Matrices.Real.ColumnVector<TRealNumber> V(int length, Func<int, TRealNumber> initializer) =>
        BoxedColumnVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .V(length, initializer)
            .ColumnVector();

    public static Computation.Matrices.Real.ColumnVector<TRealNumber> ZeroColumnVector(int length) =>
        BoxedColumnVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .Zero(length)
            .ColumnVector();

    public static Computation.Matrices.Real.RowVector<TRealNumber> U(TRealNumber[] entries) =>
        BoxedRowVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .U(entries)
            .RowVector();

    public static Computation.Matrices.Real.RowVector<TRealNumber> U(float[] entries) =>
        BoxedRowVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .U(entries)
            .RowVector();

    public static Computation.Matrices.Real.RowVector<TRealNumber> U(double[] entries) =>
        BoxedRowVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .U(entries)
            .RowVector();

    public static Computation.Matrices.Real.RowVector<TRealNumber> U(IEnumerable<TRealNumber> entries) =>
        BoxedRowVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .U(entries)
            .RowVector();

    public static Computation.Matrices.Real.RowVector<TRealNumber> U(int length, Func<int, TRealNumber> initializer) =>
        BoxedRowVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .U(length, initializer)
            .RowVector();

    public static Computation.Matrices.Real.RowVector<TRealNumber> ZeroRowVector(int length) =>
        BoxedRowVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .Zero(length)
            .RowVector();
}
