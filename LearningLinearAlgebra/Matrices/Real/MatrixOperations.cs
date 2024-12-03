using System.Numerics;

namespace LearningLinearAlgebra.Matrices.Real;

public interface IAction<in TSelf, TRowVector, TColumnVector, TRealNumber>
    where TSelf : IAction<TSelf, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TColumnVector Act(TSelf self, TColumnVector vector);
    public static abstract TColumnVector operator *(TSelf self, TColumnVector vector);
}

public interface IAddition<TSelf>
    where TSelf : IAddition<TSelf>
{

    public static abstract TSelf Add(TSelf left, TSelf right);
    public static abstract TSelf operator +(TSelf left, TSelf right);
}

public interface ICanBeNormalized<TSelf, TRealNumber>
    where TSelf : ICanBeNormalized<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf Normalized(TSelf self);
}

public interface ICanBeRounded<TSelf>
    where TSelf : ICanBeRounded<TSelf>
{
    public static abstract TSelf Round(TSelf self);
}

public interface IHasInverse<TSelf>
    where TSelf : IHasInverse<TSelf>
{
    public static abstract TSelf AdditiveInverse(TSelf self);
    public static abstract TSelf operator -(TSelf self);
}

public interface IInnerProduct<in TSelf, out TRealNumber>
    where TSelf : IInnerProduct<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TRealNumber InnerProduct(TSelf left, TSelf right);
    public static abstract TRealNumber operator *(TSelf left, TSelf right);
}

public interface IMultiplication<TSelf>
    where TSelf : IMultiplication<TSelf>
{
    public static abstract TSelf Multiply(TSelf left, TSelf right);
    public static abstract TSelf operator *(TSelf left, TSelf right);
}

public interface IScalarMultiplication<TSelf, in TRealNumber>
    where TSelf : IScalarMultiplication<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf Multiply(TRealNumber scalar, TSelf self);
    public static abstract TSelf operator *(TRealNumber scalar, TSelf self);
}

public interface IVectorMultiplication<in TRowVector, in TColumnVector, out TRealNumber>
    where TRowVector : IVectorMultiplication<TRowVector, TColumnVector, TRealNumber>, IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TRealNumber Multiply(TRowVector left, TColumnVector right);
    public static abstract TRealNumber operator *(TRowVector left, TColumnVector right);
}

public interface ISubtraction<TSelf>
    where TSelf : ISubtraction<TSelf>
{
    public static abstract TSelf Subtract(TSelf left, TSelf right);
    public static abstract TSelf operator -(TSelf left, TSelf right);
}

public interface ITensorProduct<TSelf>
{
    public static abstract TSelf TensorProduct(TSelf left, TSelf right);
}

public interface IHasSquareMatrixTranspose<TSelf>
    where TSelf : IHasSquareMatrixTranspose<TSelf>
{
    public static abstract TSelf Transpose(TSelf self);
}

public interface IHasColumnVectorTranspose<in TRowVector, out TColumnVector, TRealNumber>
    where TRowVector : IHasColumnVectorTranspose<TRowVector, TColumnVector, TRealNumber>, IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TColumnVector Transpose(TRowVector self);
}

public interface IHasRowVectorTranspose<in TColumnVector, out TRowVector, TRealNumber>
    where TColumnVector : IHasRowVectorTranspose<TColumnVector, TRowVector, TRealNumber>,  IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TRowVector Transpose(TColumnVector self);
}

public static class FluentMatrixOperations
{
    public static TColumnVector Act<TSelf, TRowVector, TColumnVector, TRealNumber>(this IAction<TSelf, TRowVector, TColumnVector, TRealNumber> self, TColumnVector vector)
        where TSelf : IAction<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Act((TSelf)self, vector);

    public static TSelf Add<TSelf>(this TSelf left, TSelf right)
        where TSelf : IAddition<TSelf> =>
        TSelf.Add(left, right);

    public static TRealNumber Multiply<TRowVector, TColumnVector, TRealNumber>(this IVectorMultiplication<TRowVector, TColumnVector, TRealNumber> left, TColumnVector right)
        where TRowVector : IVectorMultiplication<TRowVector, TColumnVector, TRealNumber>, IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TRowVector.Multiply((TRowVector)left, right);

    public static TRealNumber InnerProduct<TSelf, TRealNumber>(this IInnerProduct<TSelf, TRealNumber> left, TSelf right)
        where TSelf : IInnerProduct<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.InnerProduct((TSelf)left, right);

    public static TSelf AdditiveInverse<TSelf>(this TSelf self)
        where TSelf : IHasInverse<TSelf> =>
        TSelf.AdditiveInverse(self);

    public static TSelf Multiply<TSelf, TRealNumber>(this TRealNumber scalar, IScalarMultiplication<TSelf, TRealNumber> matrix)
        where TSelf : IScalarMultiplication<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Multiply(scalar, (TSelf)matrix);

    public static TSelf Normalized<TSelf, TRealNumber>(this ICanBeNormalized<TSelf, TRealNumber> self)
        where TSelf : ICanBeNormalized<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Normalized((TSelf)self);

    public static TSelf Multiply<TSelf>(this TSelf left, TSelf right)
        where TSelf : IMultiplication<TSelf> =>
        TSelf.Multiply(left, right);

    public static TSelf Round<TSelf>(this TSelf self)
        where TSelf : ICanBeRounded<TSelf> =>
        TSelf.Round(self);

    public static TSelf Subtract<TSelf>(this ISubtraction<TSelf> left, TSelf right)
        where TSelf : ISubtraction<TSelf> =>
        TSelf.Subtract((TSelf)left, right);

    public static TSelf Subtract<TSelf>(this TSelf left, TSelf right)
        where TSelf : ISubtraction<TSelf> =>
        TSelf.Subtract(left, right);

    public static TRealNumber Sum<TSelf, TRealNumber>(this ISum<TSelf, TRealNumber> self)
        where TSelf : ISum<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Sum((TSelf)self);

    public static TSelf TensorProduct<TSelf>(this TSelf left, TSelf right)
        where TSelf : ITensorProduct<TSelf> => TSelf.TensorProduct(left, right);

    public static TSelf Transpose<TSelf>(this TSelf self)
        where TSelf : IHasSquareMatrixTranspose<TSelf> => TSelf.Transpose(self);

    public static TColumnVector Transpose<TRowVector, TColumnVector, TRealNumber>(this IHasColumnVectorTranspose<TRowVector, TColumnVector, TRealNumber> self)
        where TRowVector : IHasColumnVectorTranspose<TRowVector, TColumnVector, TRealNumber>, IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TRowVector.Transpose((TRowVector)self);

    public static TRowVector Transpose<TColumnVector, TRowVector, TRealNumber>(this IHasRowVectorTranspose<TColumnVector, TRowVector, TRealNumber> self)
        where TColumnVector : IHasRowVectorTranspose<TColumnVector, TRowVector, TRealNumber>, IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TColumnVector.Transpose((TColumnVector)self);
}
