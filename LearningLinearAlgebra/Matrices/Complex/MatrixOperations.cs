using LearningLinearAlgebra.Numbers;
using System.Numerics;

namespace LearningLinearAlgebra.Matrices.Complex;

public interface IAction<TSelf, TRowVector, TColumnVector, TRealNumber>
    where TSelf : IAction<TSelf, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public abstract static TColumnVector Act(TSelf self, TColumnVector vector);
    public abstract static TRowVector Act(TRowVector vector, TSelf self);
    public abstract static TColumnVector operator *(TSelf self, TColumnVector vector);
    public abstract static TRowVector operator *(TRowVector vector, TSelf self);
}

public interface IAddition<TSelf>
    where TSelf : IAddition<TSelf>
{
    public abstract static TSelf Add(TSelf left, TSelf right);
    public abstract static TSelf operator +(TSelf left, TSelf right);
}

public interface IHasSquareMatrixAdjoint<TSelf>
    where TSelf : IHasSquareMatrixAdjoint<TSelf>
{
    public abstract static TSelf Adjoint(TSelf self);
}

public interface ICanBeNormalized<TSelf, TRealNumber>
    where TSelf : ICanBeNormalized<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public abstract static TSelf Normalized(TSelf self);
}

public interface ICanBeRounded<TSelf>
    where TSelf : ICanBeRounded<TSelf>
{
    public abstract static TSelf Round(TSelf self);
}

public interface IHasConjucate<TSelf>
    where TSelf : IHasConjucate<TSelf>
{
    public abstract static TSelf Conjucate(TSelf self);
}

public interface IHasInverse<TSelf>
    where TSelf : IHasInverse<TSelf>
{
    public abstract static TSelf AdditiveInverse(TSelf self);
    public abstract static TSelf operator -(TSelf self);
}

public interface IInnerProduct<TSelf, TRealNumber>
    where TSelf : IInnerProduct<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public abstract static ComplexNumber<TRealNumber> InnerProduct(TSelf left, TSelf right);
    public abstract static ComplexNumber<TRealNumber> operator *(TSelf left, TSelf right);
}

public interface IMultiplication<TSelf>
    where TSelf : IMultiplication<TSelf>
{
    public abstract static TSelf Multiply(TSelf left, TSelf right);
    public abstract static TSelf operator *(TSelf left, TSelf right);
}

public interface IScalarMultiplication<TSelf, TRealNumber>
    where TSelf : IScalarMultiplication<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public abstract static TSelf Multiply(TRealNumber scalar, TSelf self);
    public abstract static TSelf operator *(TRealNumber scalar, TSelf self);

    public abstract static TSelf Multiply(ComplexNumber<TRealNumber> scalar, TSelf self);
    public abstract static TSelf operator *(ComplexNumber<TRealNumber> scalar, TSelf self);
}

public interface IVectorMultiplication<TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IVectorMultiplication<TRowVector, TColumnVector, TRealNumber>, IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public abstract static ComplexNumber<TRealNumber> Multiply(TRowVector left, TColumnVector right);
    public abstract static ComplexNumber<TRealNumber> operator *(TRowVector left, TColumnVector right);
}

public interface ISubtraction<TSelf>
    where TSelf : ISubtraction<TSelf>
{
    public abstract static TSelf Subtract(TSelf left, TSelf right);
    public abstract static TSelf operator -(TSelf left, TSelf right);
}

public interface ITensorProduct<TSelf>
{
    public abstract static TSelf TensorProduct(TSelf left, TSelf right);
}

public interface IHasSquareMatrixTranspose<TSelf>
    where TSelf : IHasSquareMatrixTranspose<TSelf>
{
    public abstract static TSelf Transpose(TSelf self);
}
public interface IHasColumnVectorAdjoint<TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IHasColumnVectorAdjoint<TRowVector, TColumnVector, TRealNumber>, IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public abstract static TColumnVector Adjoint(TRowVector self);
}

public interface IHasRowVectorAdjoint<TColumnVector, TRowVector, TRealNumber>
    where TColumnVector : IHasRowVectorAdjoint<TColumnVector, TRowVector, TRealNumber>, IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public abstract static TRowVector Adjoint(TColumnVector self);
}

public interface IHasColumnVectorTranspose<TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IHasColumnVectorTranspose<TRowVector, TColumnVector, TRealNumber>, IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public abstract static TColumnVector Transpose(TRowVector self);
}

public interface IHasRowVectorTranspose<TColumnVector, TRowVector, TRealNumber>
    where TColumnVector : IHasRowVectorTranspose<TColumnVector, TRowVector, TRealNumber>,  IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public abstract static TRowVector Transpose(TColumnVector self);
}

public static class FluentMatrixOperations
{
    public static TColumnVector Act<TSelf, TRowVector, TColumnVector, TRealNumber>(this IAction<TSelf, TRowVector, TColumnVector, TRealNumber> self, TColumnVector vector)
        where TSelf : IAction<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Act((TSelf)self, vector);

    public static TRowVector Act<TSelf, TRowVector, TColumnVector, TRealNumber>(this IAction<TSelf, TRowVector, TColumnVector, TRealNumber> self, TRowVector vector)
        where TSelf : IAction<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Act(vector, (TSelf)self);

    public static TSelf Add<TSelf>(this TSelf left, TSelf right)
        where TSelf : IAddition<TSelf> =>
        TSelf.Add(left, right);

    public static TSelf Adjoint<TSelf>(this TSelf self) 
        where TSelf : IHasSquareMatrixAdjoint<TSelf> =>
        TSelf.Adjoint(self);

    public static TColumnVector Adjoint<TRowVector, TColumnVector, TRealNumber>(this IHasColumnVectorAdjoint<TRowVector, TColumnVector, TRealNumber> self)
        where TRowVector : IHasColumnVectorAdjoint<TRowVector, TColumnVector, TRealNumber>, IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TRowVector.Adjoint((TRowVector)self);

    public static TRowVector Adjoint<TColumnVector, TRowVector, TRealNumber>(this IHasRowVectorAdjoint<TColumnVector, TRowVector, TRealNumber> self) 
        where TColumnVector : IHasRowVectorAdjoint<TColumnVector, TRowVector, TRealNumber>, IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TColumnVector.Adjoint((TColumnVector)self);

    public static TSelf Conjucate<TSelf>(this TSelf self) where TSelf : IHasConjucate<TSelf> =>
        TSelf.Conjucate(self);

    public static ComplexNumber<TRealNumber> Multiply<TRowVector, TColumnVector, TRealNumber>(this IVectorMultiplication<TRowVector, TColumnVector, TRealNumber> left, TColumnVector right)
        where TRowVector : IVectorMultiplication<TRowVector, TColumnVector, TRealNumber>, IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TRowVector.Multiply((TRowVector)left, right);

    public static ComplexNumber<TRealNumber> InnerProduct<TSelf, TRealNumber>(this IInnerProduct<TSelf, TRealNumber> left, TSelf right)
        where TSelf : IInnerProduct<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.InnerProduct((TSelf)left, right);

    public static TSelf AdditiveInverse<TSelf>(this TSelf self)
        where TSelf : IHasInverse<TSelf> =>
        TSelf.AdditiveInverse(self);

    public static TSelf Multiply<TSelf, TRealNumber>(this ComplexNumber<TRealNumber> scalar, IScalarMultiplication<TSelf, TRealNumber> matrix)
        where TSelf : IScalarMultiplication<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Multiply(scalar, (TSelf)matrix);

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

    public static ComplexNumber<TRealNumber> Sum<TSelf, TRealNumber>(this ISum<TSelf, TRealNumber> self)
        where TSelf : ISum<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Sum((TSelf)self);

    public static TSelf TensorProduct<TSelf>(this TSelf left, TSelf right)
        where TSelf : ITensorProduct<TSelf> => TSelf.TensorProduct(left, right);

    public static TSelf Transpose<TSelf>(this TSelf self)
        where TSelf : IHasSquareMatrixTranspose<TSelf> => 
        TSelf.Transpose(self);

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
