using System.Numerics;

namespace Computation.Matrices.Real;

public interface IMatrices<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract SquareMatrix<TRealNumber> M(TRealNumber[,] entries);

    public static abstract SquareMatrix<TRealNumber> M(float[,] entries);

    public static abstract SquareMatrix<TRealNumber> M(double[,] entries);

    public static abstract SquareMatrix<TRealNumber> M(int m, Func<int, int, TRealNumber> initializer);

    public static abstract SquareMatrix<TRealNumber> Zero(int m);

    public static abstract SquareMatrix<TRealNumber> Identity(int m);

    public static abstract ColumnVector<TRealNumber> V(int[] entries);

    public static abstract ColumnVector<TRealNumber> V(float[] entries);

    public static abstract ColumnVector<TRealNumber> V(double[] entries);

    public static abstract ColumnVector<TRealNumber> V(TRealNumber[] entries);

    public static abstract ColumnVector<TRealNumber> V(IEnumerable<TRealNumber> entries);

    public static abstract ColumnVector<TRealNumber> V(int length, Func<int, TRealNumber> initializer);

    public static abstract ColumnVector<TRealNumber> ZeroColumnVector(int length);

    public static abstract RowVector<TRealNumber> U(int[] entries);

    public static abstract RowVector<TRealNumber> U(float[] entries);

    public static abstract RowVector<TRealNumber> U(double[] entries);

    public static abstract RowVector<TRealNumber> U(TRealNumber[] entries);

    public static abstract RowVector<TRealNumber> U(IEnumerable<TRealNumber> entries);

    public static abstract RowVector<TRealNumber> U(int length, Func<int, TRealNumber> initializer);

    public static abstract RowVector<TRealNumber> ZeroRowVector(int length);
}

public class Matrices<TRealNumber> : IMatrices<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static SquareMatrix<TRealNumber> M(TRealNumber[,] entries) =>
        Managed.Real.Matrices<TRealNumber>.M(entries);

    public static SquareMatrix<TRealNumber> M(float[,] entries) =>
        Managed.Real.Matrices<TRealNumber>.M(entries);

    public static SquareMatrix<TRealNumber> M(double[,] entries) =>
        Managed.Real.Matrices<TRealNumber>.M(entries);

    public static SquareMatrix<TRealNumber> M(int m, Func<int, int, TRealNumber> initializer) =>
        Managed.Real.Matrices<TRealNumber>.M(m, initializer);

    public static SquareMatrix<TRealNumber> Zero(int m) =>
        Managed.Real.Matrices<TRealNumber>.Zero(m);

    public static SquareMatrix<TRealNumber> Identity(int m) =>
        Managed.Real.Matrices<TRealNumber>.Identity(m);

    public static ColumnVector<TRealNumber> V(int[] entries) =>
        Managed.Real.Matrices<TRealNumber>.V(entries);

    public static ColumnVector<TRealNumber> V(float[] entries) =>
        Managed.Real.Matrices<TRealNumber>.V(entries);

    public static ColumnVector<TRealNumber> V(double[] entries) =>
        Managed.Real.Matrices<TRealNumber>.V(entries);

    public static ColumnVector<TRealNumber> V(TRealNumber[] entries) =>
        Managed.Real.Matrices<TRealNumber>.V(entries);

    public static ColumnVector<TRealNumber> V(IEnumerable<TRealNumber> entries) =>
        Managed.Real.Matrices<TRealNumber>.V(entries);

    public static ColumnVector<TRealNumber> V(int length, Func<int, TRealNumber> initializer) =>
        Managed.Real.Matrices<TRealNumber>.V(length, initializer);

    public static ColumnVector<TRealNumber> ZeroColumnVector(int length) =>
        Managed.Real.Matrices<TRealNumber>.ZeroColumnVector(length);

    public static RowVector<TRealNumber> U(int[] entries) =>
        Managed.Real.Matrices<TRealNumber>.U(entries);

    public static RowVector<TRealNumber> U(float[] entries) =>
        Managed.Real.Matrices<TRealNumber>.U(entries);

    public static RowVector<TRealNumber> U(double[] entries) =>
        Managed.Real.Matrices<TRealNumber>.U(entries);

    public static RowVector<TRealNumber> U(TRealNumber[] entries) =>
        Managed.Real.Matrices<TRealNumber>.U(entries);

    public static RowVector<TRealNumber> U(IEnumerable<TRealNumber> entries) =>
        Managed.Real.Matrices<TRealNumber>.U(entries);

    public static RowVector<TRealNumber> U(int length, Func<int, TRealNumber> initializer) =>
        Managed.Real.Matrices<TRealNumber>.U(length, initializer);

    public static RowVector<TRealNumber> ZeroRowVector(int length) =>
        Managed.Real.Matrices<TRealNumber>.ZeroRowVector(length);
}
