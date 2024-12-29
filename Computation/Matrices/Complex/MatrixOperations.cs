using System.Numerics;
using Computation.Numbers;

namespace Computation.Matrices.Complex;

public interface IAction<in TSelf, TRowVector, TColumnVector, TRealNumber>
    where TSelf : IAction<TSelf, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TColumnVector Act(TSelf self, TColumnVector vector);
    public static abstract TRowVector Act(TRowVector vector, TSelf self);
    public static abstract TColumnVector operator *(TSelf self, TColumnVector vector);
    public static abstract TRowVector operator *(TRowVector vector, TSelf self);
}

public interface IAddition<TSelf>
    where TSelf : IAddition<TSelf>
{
    public static abstract TSelf Add(TSelf left, TSelf right);
    public static abstract TSelf operator +(TSelf left, TSelf right);
}

public interface IHasSquareMatrixAdjoint<TSelf>
    where TSelf : IHasSquareMatrixAdjoint<TSelf>
{
    public static abstract TSelf Adjoint(TSelf self);
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

public interface IHasConjucate<TSelf>
    where TSelf : IHasConjucate<TSelf>
{
    public static abstract TSelf Conjucate(TSelf self);
}

public interface IHasInverse<TSelf>
    where TSelf : IHasInverse<TSelf>
{
    public static abstract TSelf AdditiveInverse(TSelf self);
    public static abstract TSelf operator -(TSelf self);
}

public interface IInnerProduct<in TSelf, TRealNumber>
    where TSelf : IInnerProduct<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract ComplexNumber<TRealNumber> InnerProduct(TSelf left, TSelf right);
    public static abstract ComplexNumber<TRealNumber> operator *(TSelf left, TSelf right);
}

public interface IMultiplication<TSelf>
    where TSelf : IMultiplication<TSelf>
{
    public static abstract TSelf Multiply(TSelf left, TSelf right);
    public static abstract TSelf operator *(TSelf left, TSelf right);
}

public interface IScalarMultiplication<TSelf, TRealNumber>
    where TSelf : IScalarMultiplication<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf Multiply(TRealNumber scalar, TSelf self);
    public static abstract TSelf operator *(TRealNumber scalar, TSelf self);

    public static abstract TSelf Multiply(ComplexNumber<TRealNumber> scalar, TSelf self);
    public static abstract TSelf operator *(ComplexNumber<TRealNumber> scalar, TSelf self);
}

public interface IVectorMultiplication<in TRowVector, in TColumnVector, TRealNumber>
    where TRowVector : IVectorMultiplication<TRowVector, TColumnVector, TRealNumber>, IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract ComplexNumber<TRealNumber> Multiply(TRowVector left, TColumnVector right);
    public static abstract ComplexNumber<TRealNumber> operator *(TRowVector left, TColumnVector right);
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
public interface IHasColumnVectorAdjoint<in TRowVector, out TColumnVector, TRealNumber>
    where TRowVector : IHasColumnVectorAdjoint<TRowVector, TColumnVector, TRealNumber>, IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TColumnVector Adjoint(TRowVector self);
}

public interface IHasRowVectorAdjoint<in TColumnVector, out TRowVector, TRealNumber>
    where TColumnVector : IHasRowVectorAdjoint<TColumnVector, TRowVector, TRealNumber>, IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TRowVector Adjoint(TColumnVector self);
}

public interface IHasColumnVectorTranspose<in TRowVector, out TColumnVector, TRealNumber>
    where TRowVector : IHasColumnVectorTranspose<TRowVector, TColumnVector, TRealNumber>, IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TColumnVector Transpose(TRowVector self);
}

public interface IHasRowVectorTranspose<in TColumnVector, out TRowVector, TRealNumber>
    where TColumnVector : IHasRowVectorTranspose<TColumnVector, TRowVector, TRealNumber>, IColumnVector<TColumnVector, TRowVector, TRealNumber>
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

    public static TRowVector Act<TSelf, TRowVector, TColumnVector, TRealNumber>(this IAction<TSelf, TRowVector, TColumnVector, TRealNumber> self, TRowVector vector)
        where TSelf : IAction<TSelf, TRowVector, TColumnVector, TRealNumber>
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Act(vector, (TSelf)self);

    public static TRowVector Act<TSelf, TRowVector, TColumnVector, TRealNumber>(this TRowVector vector, IAction<TSelf, TRowVector, TColumnVector, TRealNumber> self)
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

    public static IEnumerable<ComplexNumber<TRealNumber>> Column<TSelf, TRealNumber>(this TSelf self, int j)
        where TSelf : IHasColumns<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Column(self, j);

    public static IEnumerable<IEnumerable<ComplexNumber<TRealNumber>>> Columns<TSelf, TRealNumber>(this TSelf self)
        where TSelf : IHasColumns<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Columns(self);

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

    public static IEnumerable<ComplexNumber<TRealNumber>> Row<TSelf, TRealNumber>(this TSelf self, int i)
        where TSelf : IHasRows<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Row(self, i);

    public static IEnumerable<IEnumerable<ComplexNumber<TRealNumber>>> Rows<TSelf, TRealNumber>(this TSelf self)
        where TSelf : IHasRows<TSelf, TRealNumber> 
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
            TSelf.Rows(self);

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
