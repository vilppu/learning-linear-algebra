using System.Numerics;
using LearningLinearAlgebra.Matrices.Real.Abstract;
using LearningLinearAlgebra.Matrices.Real.Managed;

namespace LearningLinearAlgebra.LinearAlgebra.RealVectorSpace;

public record Operator<TRealNumber>(SquareMatrix<TRealNumber> Components)
    : IOperator<Operator<TRealNumber>, Ket<TRealNumber>, Bra<TRealNumber>, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public TRealNumber this[int i, int j] => Components[i, j];

    public static Operator<TRealNumber> M(SquareMatrix<TRealNumber> components) =>
        new(components);

    public static Operator<TRealNumber> M(TRealNumber[,] components) =>
        M(SquareMatrix<TRealNumber>.M(components));

    public static Operator<TRealNumber> M(float[,] components) =>
        M(SquareMatrix<TRealNumber>.M(components));

    public static Operator<TRealNumber> M(double[,] components) =>
        M(SquareMatrix<TRealNumber>.M(components));

    public static Operator<TRealNumber> Identity(int dimension) =>
        M(SquareMatrix<TRealNumber>.Identity(dimension));

    public static Operator<TRealNumber> Zero(int dimension) =>
        M(SquareMatrix<TRealNumber>.Zero(dimension));

    public static int Dimension(Operator<TRealNumber> @operator) =>
        @operator.Components.M();

    public static Operator<TRealNumber> Add(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(SquareMatrix<TRealNumber>.Add(left.Components, right.Components));

    public static Operator<TRealNumber> operator +(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(SquareMatrix<TRealNumber>.Add(left.Components, right.Components));

    public static Operator<TRealNumber> Subtract(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(SquareMatrix<TRealNumber>.Subtract(left.Components, right.Components));

    public static Operator<TRealNumber> operator -(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(SquareMatrix<TRealNumber>.Subtract(left.Components, right.Components));

    public static Operator<TRealNumber> AdditiveInverse(Operator<TRealNumber> @operator) =>
        M(SquareMatrix<TRealNumber>.AdditiveInverse(@operator.Components));

    public static Operator<TRealNumber> operator -(Operator<TRealNumber> @operator) =>
        M(SquareMatrix<TRealNumber>.AdditiveInverse(@operator.Components));

    public static Operator<TRealNumber> Multiply(TRealNumber scalar, Operator<TRealNumber> @operator) =>
        M(SquareMatrix<TRealNumber>.Multiply(scalar, @operator.Components));

    public static Operator<TRealNumber> operator *(TRealNumber scalar, Operator<TRealNumber> @operator) =>
        M(SquareMatrix<TRealNumber>.Multiply(scalar, @operator.Components));

    public static Operator<TRealNumber> Multiply(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(SquareMatrix<TRealNumber>.Multiply(left.Components, right.Components));

    public static Operator<TRealNumber> operator *(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(SquareMatrix<TRealNumber>.Multiply(left.Components, right.Components));

    public static Operator<TRealNumber> TensorProduct(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(SquareMatrix<TRealNumber>.TensorProduct(left.Components, right.Components));

    public static Operator<TRealNumber> Commutator(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(SquareMatrix<TRealNumber>.Commutator(left.Components, right.Components));

    public static Ket<TRealNumber> Act(Operator<TRealNumber> @operator, Ket<TRealNumber> ket) =>
        Ket<TRealNumber>.Create(SquareMatrix<TRealNumber>.Act(@operator.Components, ket.Components));

    public static Ket<TRealNumber> operator *(Operator<TRealNumber> @operator, Ket<TRealNumber> ket) =>
        Ket<TRealNumber>.Create(SquareMatrix<TRealNumber>.Act(@operator.Components, ket.Components));

    public static Bra<TRealNumber> Act(Bra<TRealNumber> bra, Operator<TRealNumber> @operator) =>
        Bra<TRealNumber>.Create(SquareMatrix<TRealNumber>.Act(bra.Components, @operator.Components));

    public static Bra<TRealNumber> operator *(Bra<TRealNumber> bra, Operator<TRealNumber> @operator) =>
        Bra<TRealNumber>.Create(SquareMatrix<TRealNumber>.Act(bra.Components, @operator.Components));

    public static Operator<TRealNumber> Round(Operator<TRealNumber> @operator) =>
        M(SquareMatrix<TRealNumber>.Round(@operator.Components));

    public static bool IsIdentity(Operator<TRealNumber> @operator) =>
        SquareMatrix<TRealNumber>.IsIdentity(@operator.Components);

    public override string ToString() => 
        Components.ToString();
}