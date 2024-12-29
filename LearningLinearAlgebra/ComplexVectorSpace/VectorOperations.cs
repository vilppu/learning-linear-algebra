using System.Numerics;
using Computation.Numbers;

namespace LearningLinearAlgebra.ComplexVectorSpace;

public static class FluentKetOperations
{
    public static int Dimension<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Dimension(ket);

    public static Ket<TRealNumber> Add<TRealNumber>(this Ket<TRealNumber> left, Ket<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Add(left, right);

    public static Ket<TRealNumber> Subtract<TRealNumber>(this Ket<TRealNumber> left, Ket<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Subtract(left, right);

    public static Ket<TRealNumber> AdditiveInverse<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.AdditiveInverse(ket);

    public static Ket<TRealNumber> Multiply<TRealNumber>(this ComplexNumber<TRealNumber> scalar, Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Multiply(scalar, ket);

    public static ComplexNumber<TRealNumber> Multiply<TRealNumber>(this Bra<TRealNumber> bra, Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Multiply(bra, ket);

    public static ComplexNumber<TRealNumber> InnerProduct<TRealNumber>(this Ket<TRealNumber> left, Ket<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.InnerProduct(left, right);

    public static Ket<TRealNumber> TensorProduct<TRealNumber>(this Ket<TRealNumber> left, Ket<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.TensorProduct(left, right);

    public static Bra<TRealNumber> Bra<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Bra(ket);

    public static TRealNumber Norm<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Norm(ket);

    public static TRealNumber Distance<TRealNumber>(this Ket<TRealNumber> left, Ket<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Distance(left, right);

    public static Ket<TRealNumber> Normalized<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Normalized(ket);

    public static Ket<TRealNumber> Conjucate<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Conjucate(ket);
}

public static class FluentBraOperations
{
    public static int Dimension<TRealNumber>(this Bra<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Dimension(ket);

    public static Bra<TRealNumber> Add<TRealNumber>(this Bra<TRealNumber> left, Bra<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Add(left, right);

    public static Bra<TRealNumber> Subtract<TRealNumber>(this Bra<TRealNumber> left, Bra<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Subtract(left, right);

    public static Bra<TRealNumber> AdditiveInverse<TRealNumber>(this Bra<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.AdditiveInverse(ket);

    public static Bra<TRealNumber> Multiply<TRealNumber>(this ComplexNumber<TRealNumber> scalar, Bra<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Multiply(scalar, ket);

    public static ComplexNumber<TRealNumber> InnerProduct<TRealNumber>(this Bra<TRealNumber> left, Bra<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.InnerProduct(left, right);

    public static Bra<TRealNumber> TensorProduct<TRealNumber>(this Bra<TRealNumber> left, Bra<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.TensorProduct(left, right);

    public static Ket<TRealNumber> Ket<TRealNumber>(this Bra<TRealNumber> bra)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Ket(bra);

    public static TRealNumber Norm<TRealNumber>(this Bra<TRealNumber> bra)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Norm(bra);

    public static TRealNumber Distance<TRealNumber>(this Bra<TRealNumber> left, Bra<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Distance(left, right);

    public static Bra<TRealNumber> Normalized<TRealNumber>(this Bra<TRealNumber> bra)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Normalized(bra);

    public static Bra<TRealNumber> Conjucate<TRealNumber>(this Bra<TRealNumber> bra)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Conjucate(bra);
}

public static class FluentOperatorOperations
{
    public static int Dimension<TRealNumber>(this Operator<TRealNumber> @operator)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Dimension(@operator);

    public static Operator<TRealNumber> Add<TRealNumber>(this Operator<TRealNumber> left, Operator<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Add(left, right);

    public static Operator<TRealNumber> Subtract<TRealNumber>(this Operator<TRealNumber> left, Operator<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Subtract(left, right);

    public static Operator<TRealNumber> AdditiveInverse<TRealNumber>(this Operator<TRealNumber> @operator)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.AdditiveInverse(@operator);

    public static Operator<TRealNumber> Multiply<TRealNumber>(this ComplexNumber<TRealNumber> scalar, Operator<TRealNumber> @operator)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
    Operator<TRealNumber>.Multiply(scalar, @operator);

    public static Operator<TRealNumber> Multiply<TRealNumber>(this Operator<TRealNumber> left, Operator<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Multiply(left, right);

    public static Operator<TRealNumber> TensorProduct<TRealNumber>(this Operator<TRealNumber> left, Operator<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.TensorProduct(left, right);

    public static Operator<TRealNumber> Commutator<TRealNumber>(this Operator<TRealNumber> left, Operator<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Commutator(left, right);

    public static Ket<TRealNumber> Act<TRealNumber>(this Operator<TRealNumber> @operator, Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Act(@operator, ket);

    public static Bra<TRealNumber> Act<TRealNumber>(this Bra<TRealNumber> bra, Operator<TRealNumber> @operator)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Act(bra, @operator);

    public static Operator<TRealNumber> Conjucate<TRealNumber>(this Operator<TRealNumber> @operator)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Conjucate(@operator);

    public static Operator<TRealNumber> Adjoint<TRealNumber>(this Operator<TRealNumber> @operator)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Adjoint(@operator);

    public static Operator<TRealNumber> Round<TRealNumber>(this Operator<TRealNumber> @operator)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Round(@operator);

    public static bool IsIdentity<TRealNumber>(this Operator<TRealNumber> @operator)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.IsIdentity(@operator);

    public static bool IsUnitary<TRealNumber>(this Operator<TRealNumber> @operator)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.IsUnitary(@operator);

    public static bool IsHermitian<TRealNumber>(this Operator<TRealNumber> @operator)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.IsHermitian(@operator);
}