using System.Numerics;
using LearningLinearAlgebra.Matrices.Complex;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.ComplexVectorSpace;

public record Operator<TRealNumber>(SquareMatrix<TRealNumber> Components)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(Operator<TRealNumber>? other) =>
        Components.Equals(other?.Components);

    public override int GetHashCode() =>
        Components.GetHashCode();

    public static Operator<TRealNumber> M(SquareMatrix<TRealNumber> components) =>
        new(components);

    public static Operator<TRealNumber> M(ComplexNumber<TRealNumber>[,] components) =>
        M(SquareMatrix<TRealNumber>.M(components));

    public static Operator<TRealNumber> M(ComplexNumber<float>[,] components) =>
        M(SquareMatrix<TRealNumber>.M(components));

    public static Operator<TRealNumber> M(ComplexNumber<double>[,] components) =>
        M(SquareMatrix<TRealNumber>.M(components));

    public static Operator<TRealNumber> Zero(int dimension) =>
        M(SquareMatrix<TRealNumber>.Zero(dimension));

    public static Operator<TRealNumber> Identity(int dimension) =>
        M(SquareMatrix<TRealNumber>.Identity(dimension));

    public ComplexNumber<TRealNumber> this[int i, int j] => Components[i, j];

    public static Operator<TRealNumber> Add(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(left.Components.Add(right.Components));

    public static Operator<TRealNumber> AdditiveInverse(Operator<TRealNumber> @operator) =>
        M(@operator.Components.AdditiveInverse());

    public static Bra<TRealNumber> Act(Bra<TRealNumber> bra, Operator<TRealNumber> @operator) =>
        Bra<TRealNumber>.U(bra.Components.Act(@operator.Components));

    public static Ket<TRealNumber> Act(Operator<TRealNumber> @operator, Ket<TRealNumber> ket) =>
        Ket<TRealNumber>.V(@operator.Components.Act(ket.Components));

    public static Operator<TRealNumber> Adjoint(Operator<TRealNumber> @operator) =>
        M(@operator.Components.Adjoint());

    public static Operator<TRealNumber> Transpose(Operator<TRealNumber> @operator) =>
        M(@operator.Components.Transpose());

    public static Operator<TRealNumber> Commutator(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(left.Components.Commutator(right.Components));

    public static Operator<TRealNumber> Conjucate(Operator<TRealNumber> @operator) =>
        M(@operator.Components.Conjucate());

    public static int Dimension(Operator<TRealNumber> @operator) =>
        @operator.Components.M();

    public static bool IsHermitian(Operator<TRealNumber> @operator) =>
        @operator.Components.IsHermitian();

    public static bool IsIdentity(Operator<TRealNumber> @operator) =>
        @operator.Components.IsIdentity();

    public static bool IsUnitary(Operator<TRealNumber> @operator) =>
        @operator.Components.IsUnitary();

    public static Operator<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar, Operator<TRealNumber> @operator) =>
        M(@operator.Components.Multiply(scalar));

    public static Operator<TRealNumber> Multiply(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(left.Components.Multiply(right.Components));

    public static Operator<TRealNumber> Round(Operator<TRealNumber> @operator) =>
        M(@operator.Components.Round());

    public static Operator<TRealNumber> Subtract(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(left.Components.Subtract(right.Components));

    public static Operator<TRealNumber> TensorProduct(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(left.Components.TensorProduct(right.Components));

    public static Operator<TRealNumber> operator +(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(left.Components.Add(right.Components));

    public static Operator<TRealNumber> operator -(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(left.Components.Subtract(right.Components));

    public static Operator<TRealNumber> operator -(Operator<TRealNumber> @operator) =>
        M(@operator.Components.AdditiveInverse());

    public static Operator<TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, Operator<TRealNumber> @operator) =>
        M(@operator.Components.Multiply(scalar));

    public static Operator<TRealNumber> operator *(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(left.Components.Multiply(right.Components));

    public static Ket<TRealNumber> operator *(Operator<TRealNumber> @operator, Ket<TRealNumber> ket) =>
        Ket<TRealNumber>.V(@operator.Components.Act(ket.Components));

    public static Bra<TRealNumber> operator *(Bra<TRealNumber> bra, Operator<TRealNumber> @operator) =>
        Bra<TRealNumber>.U(bra.Components.Act(@operator.Components));
}

public static class Operator
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

    public static Operator<TRealNumber> Multiply<TRealNumber>(this Operator<TRealNumber> @operator, ComplexNumber<TRealNumber> scalar)
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

    public static Operator<TRealNumber> Transpose<TRealNumber>(this Operator<TRealNumber> @operator)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Transpose(@operator);

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
