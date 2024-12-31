using System.Numerics;
using Computation.Numbers;

namespace Computation.Matrices.Complex;

public interface IMatrices<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract SquareMatrix<TRealNumber> M(ComplexNumber<TRealNumber>[,] entries);

    public static abstract SquareMatrix<TRealNumber> M(ComplexNumber<float>[,] entries);

    public static abstract SquareMatrix<TRealNumber> M(ComplexNumber<double>[,] entries);

    public static abstract SquareMatrix<TRealNumber> M(int m, Func<int, int, ComplexNumber<TRealNumber>> initializer);

    public static abstract SquareMatrix<TRealNumber> Zero(int m);

    public static abstract SquareMatrix<TRealNumber> Identity(int m);

    public static abstract ColumnVector<TRealNumber> V(ComplexNumber<TRealNumber>[] entries);

    public static abstract ColumnVector<TRealNumber> V(IEnumerable<ComplexNumber<TRealNumber>> entries);

    public static abstract ColumnVector<TRealNumber> V(int length, Func<int, ComplexNumber<TRealNumber>> initializer);

    public static abstract ColumnVector<TRealNumber> ZeroColumnVector(int length);

    public static abstract RowVector<TRealNumber> U(ComplexNumber<TRealNumber>[] entries);

    public static abstract RowVector<TRealNumber> U(IEnumerable<ComplexNumber<TRealNumber>> entries);

    public static abstract RowVector<TRealNumber> U(int length, Func<int, ComplexNumber<TRealNumber>> initializer);

    public static abstract RowVector<TRealNumber> ZeroRowVector(int length);
}

public class Matrices<TRealNumber> : IMatrices<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static SquareMatrix<TRealNumber> M(ComplexNumber<TRealNumber>[,] entries) =>
        Cuda.Complex.Matrices<TRealNumber>.M(entries);

    public static SquareMatrix<TRealNumber> M(ComplexNumber<float>[,] entries) =>
        Cuda.Complex.Matrices<TRealNumber>.M(entries);

    public static SquareMatrix<TRealNumber> M(ComplexNumber<double>[,] entries) =>
        Cuda.Complex.Matrices<TRealNumber>.M(entries);

    public static SquareMatrix<TRealNumber> M(int m, Func<int, int, ComplexNumber<TRealNumber>> initializer) =>
        Cuda.Complex.Matrices<TRealNumber>.M(m, initializer);

    public static SquareMatrix<TRealNumber> Zero(int m) =>
        Cuda.Complex.Matrices<TRealNumber>.Zero(m);

    public static SquareMatrix<TRealNumber> Identity(int m) =>
        Cuda.Complex.Matrices<TRealNumber>.Identity(m);

    public static ColumnVector<TRealNumber> V(ComplexNumber<TRealNumber>[] entries) =>
        Cuda.Complex.Matrices<TRealNumber>.V(entries);

    public static ColumnVector<TRealNumber> V(IEnumerable<ComplexNumber<TRealNumber>> entries) =>
        Cuda.Complex.Matrices<TRealNumber>.V(entries);

    public static ColumnVector<TRealNumber> V(int length, Func<int, ComplexNumber<TRealNumber>> initializer) =>
        Cuda.Complex.Matrices<TRealNumber>.V(length, initializer);

    public static ColumnVector<TRealNumber> ZeroColumnVector(int length) =>
        Cuda.Complex.Matrices<TRealNumber>.ZeroColumnVector(length);

    public static RowVector<TRealNumber> U(ComplexNumber<TRealNumber>[] entries) =>
        Cuda.Complex.Matrices<TRealNumber>.U(entries);

    public static RowVector<TRealNumber> U(IEnumerable<ComplexNumber<TRealNumber>> entries) =>
        Cuda.Complex.Matrices<TRealNumber>.U(entries);

    public static RowVector<TRealNumber> U(int length, Func<int, ComplexNumber<TRealNumber>> initializer) =>
        Cuda.Complex.Matrices<TRealNumber>.U(length, initializer);

    public static RowVector<TRealNumber> ZeroRowVector(int length) =>
        Cuda.Complex.Matrices<TRealNumber>.ZeroRowVector(length);
}
