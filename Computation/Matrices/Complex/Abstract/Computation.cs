using LearningLinearAlgebra.Numbers;
using System.Numerics;

namespace LearningLinearAlgebra.Matrices.Complex.Abstract;

public interface IComputation<TRowVector, TColumnVector, TSquareMatrix, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TRowVector U(ComplexNumber<TRealNumber>[] entries);
    public static abstract TRowVector U(IEnumerable<ComplexNumber<TRealNumber>> entries);
    public static abstract TRowVector U(int length, Func<int, ComplexNumber<TRealNumber>> initializer);

    public static abstract bool AreEquivalent(TRowVector left, TRowVector right);
    public static abstract TColumnVector Adjoint(TRowVector self);
    public static abstract TRowVector Add(TRowVector left, TRowVector right);
    public static abstract TRowVector AdditiveInverse(TRowVector self);
    public static abstract TRowVector Conjucate(TRowVector self);
    public static abstract TRealNumber Distance(TRowVector left, TRowVector right);
    public static abstract int Length(TRowVector self);
    public static abstract TRowVector Map(TRowVector source, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
    public static abstract ComplexNumber<TRealNumber> Multiply(TRowVector left, TColumnVector right);
    public static abstract TRowVector Multiply(ComplexNumber<TRealNumber> scalar, TRowVector self);
    public static abstract TRowVector Multiply(TRealNumber scalar, TRowVector self);
    public static abstract TRealNumber Norm(TRowVector self);
    public static abstract TRowVector Normalized(TRowVector self);
    public static abstract ComplexNumber<TRealNumber> InnerProduct(TRowVector left, TRowVector right);
    public static abstract TRowVector Orthonormal(TRowVector self);
    public static abstract TRowVector Round(TRowVector self);
    public static abstract TRowVector Subtract(TRowVector left, TRowVector right);
    public static abstract ComplexNumber<TRealNumber> Sum(TRowVector self);
    public static abstract TRowVector TensorProduct(TRowVector left, TRowVector right);
    public static abstract TColumnVector Transpose(TRowVector self);
    public static abstract TRowVector Zip(TRowVector first, TRowVector second, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
    
    public static abstract TColumnVector V(ComplexNumber<TRealNumber>[] entries);
    public static abstract TColumnVector V(IEnumerable<ComplexNumber<TRealNumber>> entries);
    public static abstract TColumnVector V(int length, Func<int, ComplexNumber<TRealNumber>> initializer);

    public static abstract bool AreEquivalent(TColumnVector left, TColumnVector right);
    public static abstract TRowVector Adjoint(TColumnVector self);
    public static abstract TColumnVector Add(TColumnVector left, TColumnVector right);
    public static abstract TColumnVector AdditiveInverse(TColumnVector self);
    public static abstract TColumnVector Conjucate(TColumnVector self);
    public static abstract TRealNumber Distance(TColumnVector left, TColumnVector right);
    public static abstract int Length(TColumnVector self);
    public static abstract TColumnVector Map(TColumnVector source, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
    public static abstract TColumnVector Multiply(ComplexNumber<TRealNumber> scalar, TColumnVector self);
    public static abstract TColumnVector Multiply(TRealNumber scalar, TColumnVector self);
    public static abstract TRealNumber Norm(TColumnVector self);
    public static abstract TColumnVector Normalized(TColumnVector self);
    public static abstract ComplexNumber<TRealNumber> InnerProduct(TColumnVector left, TColumnVector right);
    public static abstract TColumnVector Orthonormal(TColumnVector self);
    public static abstract TColumnVector Round(TColumnVector self);
    public static abstract TColumnVector Subtract(TColumnVector left, TColumnVector right);
    public static abstract ComplexNumber<TRealNumber> Sum(TColumnVector self);
    public static abstract TColumnVector TensorProduct(TColumnVector left, TColumnVector right);
    public static abstract TRowVector Transpose(TColumnVector self);
    public static abstract TColumnVector Zip(TColumnVector first, TColumnVector second, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);

    public static abstract TSquareMatrix M(ComplexNumber<TRealNumber>[,] entries);
    public static abstract TSquareMatrix M(ComplexNumber<float>[,] entries);
    public static abstract TSquareMatrix M(ComplexNumber<double>[,] entries);
    public static abstract TSquareMatrix M(int m, Func<int, int, ComplexNumber<TRealNumber>> initializer);
    public static abstract TSquareMatrix Identity(int m);

    public static abstract bool AreEquivalent(TSquareMatrix left, TSquareMatrix right);
    public static abstract TSquareMatrix Adjoint(TSquareMatrix self);
    public static abstract IEnumerable<ComplexNumber<TRealNumber>> Column(TSquareMatrix self, int j);
    public static abstract TSquareMatrix Add(TSquareMatrix left, TSquareMatrix right);
    public static abstract TSquareMatrix AdditiveInverse(TSquareMatrix self);
    public static abstract TSquareMatrix Commutator(TSquareMatrix left, TSquareMatrix right);
    public static abstract TSquareMatrix Conjucate(TSquareMatrix self);
    public static abstract bool IsHermitian(TSquareMatrix self);
    public static abstract bool IsIdentity(TSquareMatrix self);
    public static abstract bool IsUnitary(TSquareMatrix self);
    public static abstract int M(TSquareMatrix self);
    public static abstract TSquareMatrix Map(TSquareMatrix source, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
    public static abstract TSquareMatrix Multiply(ComplexNumber<TRealNumber> scalar, TSquareMatrix self);
    public static abstract TSquareMatrix Multiply(TRealNumber scalar, TSquareMatrix self);
    public static abstract int N(TSquareMatrix self);
    public static abstract TSquareMatrix Round(TSquareMatrix self);
    public static abstract IEnumerable<ComplexNumber<TRealNumber>> Row(TSquareMatrix self, int i);
    public static abstract TSquareMatrix Subtract(TSquareMatrix left, TSquareMatrix right);
    public static abstract TSquareMatrix TensorProduct(TSquareMatrix left, TSquareMatrix right);
    public static abstract TSquareMatrix Transpose(TSquareMatrix self);
    public static abstract TSquareMatrix Zip(TSquareMatrix left, TSquareMatrix right, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
}