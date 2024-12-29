using System.Numerics;
using Computation.Matrices.Real;

namespace LearningLinearAlgebra.RealVectorSpace;

public record Operator<TRealNumber>(SquareMatrix<TRealNumber> Components)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static Operator<TRealNumber> M(SquareMatrix<TRealNumber> components) =>
        new(components);

    public static Operator<TRealNumber> M(TRealNumber[,] components) =>
        M(Matrices<TRealNumber>.M(components));

    public static Operator<TRealNumber> M(float[,] components) =>
        M(Matrices<TRealNumber>.M(components));

    public static Operator<TRealNumber> M(double [,] components) =>
        M(Matrices<TRealNumber>.M(components));

    public static Operator<TRealNumber> Zero(int dimension) =>
        M(Matrices<TRealNumber>.Zero(dimension));

    public static Operator<TRealNumber> Identity(int dimension) =>
        M(Matrices<TRealNumber>.Identity(dimension));

    public TRealNumber this[int i, int j] => Components[i, j];

    public static Operator<TRealNumber> Add(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(left.Components.Add(right.Components));

    public static Operator<TRealNumber> AdditiveInverse(Operator<TRealNumber> @operator) =>
        M(@operator.Components.AdditiveInverse());

    public static Bra<TRealNumber> Act(Bra<TRealNumber> bra, Operator<TRealNumber> @operator) =>
        Bra<TRealNumber>.U(@operator.Components.Act(bra.Components));

    public static Ket<TRealNumber> Act(Operator<TRealNumber> @operator, Ket<TRealNumber> ket) =>
        Ket<TRealNumber>.V(@operator.Components.Act(ket.Components));

    public static int Dimension(Operator<TRealNumber> @operator) =>
        @operator.Components.M();

    public static bool IsIdentity(Operator<TRealNumber> @operator) =>
        @operator.Components.IsIdentity();

    public static Operator<TRealNumber> Multiply(TRealNumber scalar, Operator<TRealNumber> @operator) =>
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

    public static Operator<TRealNumber> operator *(TRealNumber scalar, Operator<TRealNumber> @operator) =>
        M(@operator.Components.Multiply(scalar));

    public static Operator<TRealNumber> operator *(Operator<TRealNumber> left, Operator<TRealNumber> right) =>
        M(left.Components.Multiply(right.Components));

    public static Ket<TRealNumber> operator *(Operator<TRealNumber> @operator, Ket<TRealNumber> ket) =>
        Ket<TRealNumber>.V(@operator.Components.Act(ket.Components));

    public static Bra<TRealNumber> operator *(Bra<TRealNumber> bra, Operator<TRealNumber> @operator) =>
        Bra<TRealNumber>.U(@operator.Components.Act(bra.Components));
}