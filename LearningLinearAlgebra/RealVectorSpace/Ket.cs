using System.Numerics;
using LearningLinearAlgebra.Matrices.Real.Abstract;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.RealVectorSpace;

public record Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(TColumnVector Components)
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public TRealNumber this[int index] => Components[index];

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Create(TColumnVector components) =>
        new(components);

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> V(TRealNumber[] components) =>
        Create(TColumnVector.V(components));

    public static int Dimension(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket) =>
        TColumnVector.Length(ket.Components);

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Zero(int dimension) =>
        Create(TColumnVector.Zero(dimension));

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Add(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        Create(TColumnVector.Add(left.Components, right.Components));

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator +(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        Create(TColumnVector.Add(left.Components, right.Components));

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Subtract(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        Create(TColumnVector.Subtract(left.Components, right.Components));

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator -(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        Create(TColumnVector.Subtract(left.Components, right.Components));

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> AdditiveInverse(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket) =>
        Create(TColumnVector.AdditiveInverse(ket.Components));

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator -(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket) =>
        Create(TColumnVector.AdditiveInverse(ket.Components));

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Multiply(TRealNumber scalar, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket) =>
        Create(TColumnVector.Multiply(scalar, ket.Components));

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator *(TRealNumber scalar, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket) =>
        Create(TColumnVector.Multiply(scalar, ket.Components));

    public static TRealNumber Multiply(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket) =>
        TRowVector.Multiply(bra.Components, ket.Components);

    public static TRealNumber operator *(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket) =>
        TRowVector.Multiply(bra.Components, ket.Components);

    public static TRealNumber InnerProduct(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        TColumnVector.InnerProduct(left.Components, right.Components);

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> TensorProduct(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        Create(TColumnVector.TensorProduct(left.Components, right.Components));

    public static TRealNumber operator *(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        TColumnVector.InnerProduct(left.Components, right.Components);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Bra(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket) =>
        Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Create(TColumnVector.Transpose(ket.Components));

    public static TRealNumber Norm(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket) =>
        TColumnVector.Norm(ket.Components);

    public static TRealNumber Distance(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        TColumnVector.Distance(left.Components, right.Components);

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Normalized(Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> ket) =>
        Create(TColumnVector.Normalized(ket.Components));
}
