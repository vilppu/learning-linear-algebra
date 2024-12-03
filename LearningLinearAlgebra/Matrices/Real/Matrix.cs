using System.Numerics;

namespace LearningLinearAlgebra.Matrices.Real;

public interface IMatrix<TSelf, in TRealNumber> :
    IAddition<TSelf>,
    ICanBeRounded<TSelf>,
    IEquality<TSelf>,
    IHasInverse<TSelf>,
    IScalarMultiplication<TSelf, TRealNumber>,
    ISubtraction<TSelf>,
    ITensorProduct<TSelf>

    where TSelf : IMatrix<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
}

public interface ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> :
    IMatrix<TSelf, TRealNumber>,
    IAction<TSelf, TRowVector, TColumnVector, TRealNumber>,
    IAddition<TSelf>,
    ICanBeIdentity<TSelf>,
    ICanBeRounded<TSelf>,
    IEquality<TSelf>,
    IHasColumns<TSelf, TRealNumber>,
    IHasColumns<TSelf>,
    IHasCommutator<TSelf>,
    IHasInverse<TSelf>,
    IHasMatrixEntries<TSelf, TRealNumber>,
    IHasRows<TSelf, TRealNumber>,
    IHasRows<TSelf>,
    IHasSquareMatrixTranspose<TSelf>,
    IMultiplication<TSelf>,
    IScalarMultiplication<TSelf, TRealNumber>,
    ISubtraction<TSelf>,
    ITensorProduct<TSelf>,
    ITwoDimensionalMap<TSelf, TRealNumber>,
    ITwoDimensionalZip<TSelf, TRealNumber>

    where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf M(TRealNumber[,] entries);
    public static abstract TSelf M(float[,] entries);
    public static abstract TSelf M(double[,] entries);
    public static abstract TSelf M(int m, Func<int, int, TRealNumber> initializer);
    public static abstract TSelf Zero(int m);
    public static abstract TSelf Identity(int m);
}

public interface IColumnVector<TSelf, out TRowVector, TRealNumber> :
    IMatrix<TSelf, TRealNumber>,
    IAddition<TSelf>,
    ICanBeNormalized<TSelf, TRealNumber>,
    ICanBeRounded<TSelf>,
    IDistance<TSelf, TRealNumber>,
    IEnumerable<TRealNumber>,
    IEquality<TSelf>,
    IHasInverse<TSelf>,
    IHasLength<TSelf>,
    IHasNorm<TSelf, TRealNumber>,
    IHasRowVectorTranspose<TSelf, TRowVector, TRealNumber>,
    IHasVectorEntries<TSelf, TRealNumber>,
    IInnerProduct<TSelf, TRealNumber>,
    IOneDimensionalMap<TSelf, TRealNumber>,
    IOneDimensionalZip<TSelf, TRealNumber>,
    IOrthonormalization<TSelf>,
    IScalarMultiplication<TSelf, TRealNumber>,
    ISubtraction<TSelf>,
    ISum<TSelf, TRealNumber>,
    ITensorProduct<TSelf>

    where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf V(TRealNumber[] entries);
    public static abstract TSelf V(float[] entries);
    public static abstract TSelf V(IEnumerable<TRealNumber> entries);
    public static abstract TSelf V(int length, Func<int, TRealNumber> initializer);
    public static abstract TSelf Zero(int length);
}

public interface IRowVector<TSelf, TColumnVector, TRealNumber> :
    IMatrix<TSelf, TRealNumber>,
    IAddition<TSelf>,
    ICanBeNormalized<TSelf, TRealNumber>,
    ICanBeRounded<TSelf>,
    IDistance<TSelf, TRealNumber>,
    IEnumerable<TRealNumber>,
    IEquality<TSelf>,
    IHasInverse<TSelf>,
    IHasLength<TSelf>,
    IHasNorm<TSelf, TRealNumber>,
    IHasColumnVectorTranspose<TSelf, TColumnVector, TRealNumber>,
    IHasVectorEntries<TSelf, TRealNumber>,
    IInnerProduct<TSelf, TRealNumber>,
    IOneDimensionalMap<TSelf, TRealNumber>,
    IOneDimensionalZip<TSelf, TRealNumber>,
    IOrthonormalization<TSelf>,
    IScalarMultiplication<TSelf, TRealNumber>,
    ISubtraction<TSelf>,
    ISum<TSelf, TRealNumber>,
    IVectorMultiplication<TSelf, TColumnVector, TRealNumber>,
    ITensorProduct<TSelf>

    where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf U(TRealNumber[] entries);
    public static abstract TSelf U(float[] entries);
    public static abstract TSelf U(IEnumerable<TRealNumber> entries);
    public static abstract TSelf U(int length, Func<int, TRealNumber> initializer);
    public static abstract TSelf Zero(int length);
}

