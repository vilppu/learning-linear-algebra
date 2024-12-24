using System.Numerics;
using LearningLinearAlgebra.Matrices.Real.Abstract;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.RealVectorSpace;

public record Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(TSquareMatrix Components)
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public TRealNumber this[int i, int j] => Components[i, j];

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> M(TSquareMatrix components) =>
        new(components);

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> M(TRealNumber[,] components) =>
        M(TSquareMatrix.M(components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> M(float[,] components) =>
        M(TSquareMatrix.M(components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> M(double[,] components) =>
        M(TSquareMatrix.M(components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Identity(int dimension) =>
        M(TSquareMatrix.Identity(dimension));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Zero(int dimension) =>
        M(TSquareMatrix.Zero(dimension));

    public static int Dimension(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator) =>
        @operator.Components.M();

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Add(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        M(TSquareMatrix.Add(left.Components, right.Components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator +(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        M(TSquareMatrix.Add(left.Components, right.Components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Subtract(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        M(TSquareMatrix.Subtract(left.Components, right.Components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator -(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        M(TSquareMatrix.Subtract(left.Components, right.Components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> AdditiveInverse(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator) =>
        M(TSquareMatrix.AdditiveInverse(@operator.Components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator -(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator) =>
        M(TSquareMatrix.AdditiveInverse(@operator.Components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Multiply(TRealNumber scalar, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator) =>
        M(TSquareMatrix.Multiply(scalar, @operator.Components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator *(TRealNumber scalar, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator) =>
        M(TSquareMatrix.Multiply(scalar, @operator.Components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Multiply(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        M(TSquareMatrix.Multiply(left.Components, right.Components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator *(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        M(TSquareMatrix.Multiply(left.Components, right.Components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> TensorProduct(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        M(TSquareMatrix.TensorProduct(left.Components, right.Components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Commutator(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        M(TSquareMatrix.Commutator(left.Components, right.Components));

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Act(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket) =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Create(TSquareMatrix.Act(@operator.Components, ket.Components));

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator *(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket) =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Create(TSquareMatrix.Act(@operator.Components, ket.Components));

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Act(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator) =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Create(TSquareMatrix.Act(bra.Components, @operator.Components));

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator *(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra, Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator) =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Create(TSquareMatrix.Act(bra.Components, @operator.Components));

    public static Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Round(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator) =>
        M(TSquareMatrix.Round(@operator.Components));

    public static bool IsIdentity(Operator<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> @operator) =>
        TSquareMatrix.IsIdentity(@operator.Components);
}