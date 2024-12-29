using System.Numerics;
using Computation.Matrices.Complex;
using Computation.Numbers;

namespace Computation.Managed.Complex;

public class Matrices<TRealNumber> : IMatrices<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static Computation.Matrices.Complex.SquareMatrix<TRealNumber> M(ComplexNumber<TRealNumber>[,] entries) =>
        BoxedSquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .M(entries)
            .SquareMatrix();

    public static Computation.Matrices.Complex.SquareMatrix<TRealNumber> M(ComplexNumber<float>[,] entries) =>
        BoxedSquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .M(entries)
            .SquareMatrix();

    public static Computation.Matrices.Complex.SquareMatrix<TRealNumber> M(ComplexNumber<double>[,] entries) =>
        BoxedSquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .M(entries)
            .SquareMatrix();

    public static Computation.Matrices.Complex.SquareMatrix<TRealNumber> M(int m, Func<int, int, ComplexNumber<TRealNumber>> initializer) =>
        BoxedSquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .M(m, initializer)
            .SquareMatrix();

    public static Computation.Matrices.Complex.SquareMatrix<TRealNumber> Zero(int m) =>
        BoxedSquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .Zero(m)
            .SquareMatrix();

    public static Computation.Matrices.Complex.SquareMatrix<TRealNumber> Identity(int m) =>
        BoxedSquareMatrix<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .Identity(m)
            .SquareMatrix();

    public static Computation.Matrices.Complex.ColumnVector<TRealNumber> V(ComplexNumber<TRealNumber>[] entries) =>
        BoxedColumnVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .V(entries)
            .ColumnVector();

    public static Computation.Matrices.Complex.ColumnVector<TRealNumber> V(IEnumerable<ComplexNumber<TRealNumber>> entries) =>
        BoxedColumnVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .V(entries)
            .ColumnVector();

    public static Computation.Matrices.Complex.ColumnVector<TRealNumber> V(int length, Func<int, ComplexNumber<TRealNumber>> initializer) =>
        BoxedColumnVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .V(length, initializer)
            .ColumnVector();

    public static Computation.Matrices.Complex.ColumnVector<TRealNumber> ZeroColumnVector(int length) =>
        BoxedColumnVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .Zero(length)
            .ColumnVector();

    public static Computation.Matrices.Complex.RowVector<TRealNumber> U(ComplexNumber<TRealNumber>[] entries) =>
        BoxedRowVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .U(entries)
            .RowVector();

    public static Computation.Matrices.Complex.RowVector<TRealNumber> U(IEnumerable<ComplexNumber<TRealNumber>> entries) =>
        BoxedRowVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .U(entries)
            .RowVector();

    public static Computation.Matrices.Complex.RowVector<TRealNumber> U(int length, Func<int, ComplexNumber<TRealNumber>> initializer) =>
        BoxedRowVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .U(length, initializer)
            .RowVector();

    public static Computation.Matrices.Complex.RowVector<TRealNumber> ZeroRowVector(int length) =>
        BoxedRowVector<SquareMatrix<TRealNumber>, RowVector<TRealNumber>, ColumnVector<TRealNumber>, TRealNumber>
            .Zero(length)
            .RowVector();
}
