using System.Numerics;
using LearningLinearAlgebra.Matrices.Complex.Abstract;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.ComplexVectorSpace;

public static class FluentKetOperations
{
    public static int Dimension<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Dimension(ket);

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Add<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Add(left, right);

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Subtract<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Subtract(left, right);

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> AdditiveInverse<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.AdditiveInverse(ket);

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Multiply<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this ComplexNumber<TRealNumber> scalar, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Multiply(scalar, ket);

    public static ComplexNumber<TRealNumber> Multiply<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Multiply(bra, ket);

    public static ComplexNumber<TRealNumber> InnerProduct<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.InnerProduct(left, right);

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> TensorProduct<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.TensorProduct(left, right);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Bra(ket);

    public static TRealNumber Norm<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Norm(ket);

    public static TRealNumber Distance<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Distance(left, right);

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Normalized<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Normalized(ket);

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Conjucate<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Conjucate(ket);
}

public static class FluentBraOperations
{
    public static int Dimension<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Dimension(ket);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Add<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Add(left, right);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Subtract<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Subtract(left, right);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> AdditiveInverse<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.AdditiveInverse(ket);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Multiply<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this ComplexNumber<TRealNumber> scalar, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Multiply(scalar, ket);

    public static ComplexNumber<TRealNumber> InnerProduct<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.InnerProduct(left, right);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> TensorProduct<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.TensorProduct(left, right);

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Ket(bra);

    public static TRealNumber Norm<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Norm(bra);

    public static TRealNumber Distance<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Distance(left, right);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Normalized<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Normalized(bra);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Conjucate<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Conjucate(bra);
}

public static class FluentOperatorOperations
{
    public static int Dimension<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Dimension(@operator);

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Add<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Add(left, right);

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Subtract<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Subtract(left, right);

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> AdditiveInverse<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.AdditiveInverse(@operator);

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Multiply<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this ComplexNumber<TRealNumber> scalar, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
    Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Multiply(scalar, @operator);

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Multiply<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Multiply(left, right);

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> TensorProduct<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.TensorProduct(left, right);

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Commutator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Commutator(left, right);

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Act<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Act(@operator, ket);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Act<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Act(bra, @operator);

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Conjucate<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Conjucate(@operator);

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Adjoint<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Adjoint(@operator);

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Round<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Round(@operator);

    public static bool IsIdentity<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.IsIdentity(@operator);

    public static bool IsUnitary<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.IsUnitary(@operator);

    public static bool IsHermitian<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(this Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator)
        where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
        where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
        where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.IsHermitian(@operator);
}