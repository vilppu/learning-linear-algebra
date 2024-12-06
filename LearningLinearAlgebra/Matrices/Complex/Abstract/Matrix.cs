using System.Numerics;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.Matrices.Complex.Abstract;

public interface IMatrix<TSelf, TRealNumber> :
    IAddition<TSelf>,
    ICanBeRounded<TSelf>,
    IEquality<TSelf>,
    IHasConjucate<TSelf>,
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
    IHasSquareMatrixAdjoint<TSelf>,
    ICanBeHermitian<TSelf>,
    ICanBeIdentity<TSelf>,
    ICanBeRounded<TSelf>,
    ICanBeUnitary<TSelf>,
    IEquality<TSelf>,
    IHasColumns<TSelf, TRealNumber>,
    IHasColumns<TSelf>,
    IHasCommutator<TSelf>,
    IHasConjucate<TSelf>,
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
    public static abstract TSelf M(ComplexNumber<TRealNumber>[,] entries);
    public static abstract TSelf M(ComplexNumber<float>[,] entries);
    public static abstract TSelf M(ComplexNumber<double>[,] entries);
    public static abstract TSelf M(int m, Func<int, int, ComplexNumber<TRealNumber>> initializer);
    public static abstract TSelf Zero(int m);
    public static abstract TSelf Identity(int m);
}

public interface IColumnVector<TSelf, out TRowVector, TRealNumber> :
    IMatrix<TSelf, TRealNumber>,
    IAddition<TSelf>,
    ICanBeNormalized<TSelf, TRealNumber>,
    ICanBeRounded<TSelf>,
    IDistance<TSelf, TRealNumber>,
    IEnumerable<ComplexNumber<TRealNumber>>,
    IEquality<TSelf>,
    IHasConjucate<TSelf>,
    IHasInverse<TSelf>,
    IHasLength<TSelf>,
    IHasNorm<TSelf, TRealNumber>,
    IHasRowVectorAdjoint<TSelf, TRowVector, TRealNumber>,
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
    public static abstract TSelf V(ComplexNumber<TRealNumber>[] entries);
    public static abstract TSelf V(IEnumerable<ComplexNumber<TRealNumber>> entries);
    public static abstract TSelf V(int length, Func<int, ComplexNumber<TRealNumber>> initializer);
    public static abstract TSelf Zero(int length);
}

public interface IRowVector<TSelf, TColumnVector, TRealNumber> :
    IMatrix<TSelf, TRealNumber>,
    IAddition<TSelf>,
    ICanBeNormalized<TSelf, TRealNumber>,
    ICanBeRounded<TSelf>,
    IDistance<TSelf, TRealNumber>,
    IEnumerable<ComplexNumber<TRealNumber>>,
    IEquality<TSelf>,
    IHasConjucate<TSelf>,
    IHasInverse<TSelf>,
    IHasLength<TSelf>,
    IHasNorm<TSelf, TRealNumber>,
    IHasColumnVectorAdjoint<TSelf, TColumnVector, TRealNumber>,
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
    public static abstract TSelf U(ComplexNumber<TRealNumber>[] entries);
    public static abstract TSelf U(IEnumerable<ComplexNumber<TRealNumber>> entries);
    public static abstract TSelf U(int length, Func<int, ComplexNumber<TRealNumber>> initializer);
    public static abstract TSelf Zero(int length);
}

