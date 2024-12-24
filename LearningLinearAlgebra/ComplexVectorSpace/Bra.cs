using System.Collections;
using System.Numerics;
using LearningLinearAlgebra.Matrices.Complex.Abstract;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.ComplexVectorSpace;

public record Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>(TRowVector Components)
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public ComplexNumber<TRealNumber> this[int index] => Components[index];

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Create(TRowVector components) =>
        new(components);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> U(ComplexNumber<TRealNumber>[] components) =>
        Create(TRowVector.U(components));

    public static int Dimension(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra) =>
        TRowVector.Length(bra.Components);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Zero(int dimension) =>
        Create(TRowVector.Zero(dimension));

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Add(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        Create(TRowVector.Add(left.Components, right.Components));

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator +(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        Create(TRowVector.Add(left.Components, right.Components));

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Subtract(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        Create(TRowVector.Subtract(left.Components, right.Components));

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator -(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        Create(TRowVector.Subtract(left.Components, right.Components));

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> AdditiveInverse(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra) =>
        Create(TRowVector.AdditiveInverse(bra.Components));

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator -(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra) =>
        Create(TRowVector.AdditiveInverse(bra.Components));

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra) =>
        Create(TRowVector.Multiply(scalar, bra.Components));

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra) =>
        Create(TRowVector.Multiply(scalar, bra.Components));

    public static ComplexNumber<TRealNumber> InnerProduct(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        TRowVector.InnerProduct(left.Components, right.Components);

    public static ComplexNumber<TRealNumber> operator *(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        TRowVector.InnerProduct(left.Components, right.Components);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> TensorProduct(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        Create(TRowVector.TensorProduct(left.Components, right.Components));

    public static Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Ket(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra) =>
        Ket<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>.Create(TRowVector.Adjoint(bra.Components));

    public static TRealNumber Norm(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra) =>
        TRowVector.Norm(bra.Components);

    public static TRealNumber Distance(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> left, Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> right) =>
        TRowVector.Distance(left.Components, right.Components);

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Normalized(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra) =>
        Create(TRowVector.Normalized(bra.Components));

    public static Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> Conjucate(Bra<TRowVector, TColumnVector, TSquareMatrix, TRealNumber> bra) =>
        Create(TRowVector.Conjucate(bra.Components));
}