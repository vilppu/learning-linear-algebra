using LearningLinearAlgebra.Matrices.Complex.Abstract;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.Matrices.Complex.Managed;

public class Computation : IComputation<RowVector<float>, ColumnVector<float>, SquareMatrix<float>, float>
{
    public static RowVector<float> U(ComplexNumber<float>[] entries) => RowVector<float>.U(entries);
    public static RowVector<float> U(IEnumerable<ComplexNumber<float>> entries) => RowVector<float>.U(entries);
    public static RowVector<float> U(int length, Func<int, ComplexNumber<float>> initializer) => RowVector<float>.U(length, initializer);

    public static bool AreEquivalent(RowVector<float> left, RowVector<float> right) => RowVector<float>.AreEquivalent(left, right);
    public static ColumnVector<float> Adjoint(RowVector<float> self) => RowVector<float>.Adjoint(self);
    public static RowVector<float> Add(RowVector<float> left, RowVector<float> right) => RowVector<float>.Add(left, right);
    public static RowVector<float> AdditiveInverse(RowVector<float> self) => RowVector<float>.AdditiveInverse(self);
    public static RowVector<float> Conjucate(RowVector<float> self) => RowVector<float>.Conjucate(self);

    public static float Distance(RowVector<float> left, RowVector<float> right) => RowVector<float>.Distance(left, right);
    public static int Length(RowVector<float> self) => RowVector<float>.Length(self);
    public static RowVector<float> Map(RowVector<float> source, Func<ComplexNumber<float>, ComplexNumber<float>> elementMapping) => RowVector<float>.Map(source, elementMapping);
    public static ComplexNumber<float> Multiply(RowVector<float> left, ColumnVector<float> right) => RowVector<float>.Multiply(left, right);
    public static RowVector<float> Multiply(ComplexNumber<float> scalar, RowVector<float> self) => RowVector<float>.Multiply(scalar, self);
    public static RowVector<float> Multiply(float scalar, RowVector<float> self) => RowVector<float>.Multiply(scalar, self);
    public static float Norm(RowVector<float> self) => RowVector<float>.Norm(self);
    public static RowVector<float> Normalized(RowVector<float> self) => RowVector<float>.Normalized(self);
    public static ComplexNumber<float> InnerProduct(RowVector<float> left, RowVector<float> right) => RowVector<float>.InnerProduct(left, right);
    public static RowVector<float> Orthonormal(RowVector<float> self) => RowVector<float>.Orthonormal(self);
    public static RowVector<float> Round(RowVector<float> self) => RowVector<float>.Round(self);
    public static RowVector<float> Subtract(RowVector<float> left, RowVector<float> right) => RowVector<float>.Subtract(left, right);
    public static ComplexNumber<float> Sum(RowVector<float> self) => RowVector<float>.Sum(self);
    public static RowVector<float> TensorProduct(RowVector<float> left, RowVector<float> right) => RowVector<float>.TensorProduct(left, right);
    public static ColumnVector<float> Transpose(RowVector<float> self) => RowVector<float>.Transpose(self);
    public static RowVector<float> Zip(RowVector<float> first, RowVector<float> second, Func<ComplexNumber<float>, ComplexNumber<float>, ComplexNumber<float>> elementMapping) => RowVector<float>.Zip(first, second, elementMapping);
    
    public static ColumnVector<float> V(ComplexNumber<float>[] entries) => ColumnVector<float>.V(entries);
    public static ColumnVector<float> V(IEnumerable<ComplexNumber<float>> entries) => ColumnVector<float>.V(entries);
    public static ColumnVector<float> V(int length, Func<int, ComplexNumber<float>> initializer) => ColumnVector<float>.V(length, initializer);

    public static bool AreEquivalent(ColumnVector<float> left, ColumnVector<float> right) => ColumnVector<float>.AreEquivalent(left, right);
    public static RowVector<float> Adjoint(ColumnVector<float> self) => ColumnVector<float>.Adjoint(self);
    public static ColumnVector<float> Add(ColumnVector<float> left, ColumnVector<float> right) => ColumnVector<float>.Add(left, right);
    public static ColumnVector<float> AdditiveInverse(ColumnVector<float> self) => ColumnVector<float>.AdditiveInverse(self);
    public static ColumnVector<float> Conjucate(ColumnVector<float> self) => ColumnVector<float>.Conjucate(self);

    public static float Distance(ColumnVector<float> left, ColumnVector<float> right) => ColumnVector<float>.Distance(left, right);
    public static int Length(ColumnVector<float> self) => ColumnVector<float>.Length(self);
    public static ColumnVector<float> Map(ColumnVector<float> source, Func<ComplexNumber<float>, ComplexNumber<float>> elementMapping) => ColumnVector<float>.Map(source, elementMapping);
    public static ColumnVector<float> Multiply(ComplexNumber<float> scalar, ColumnVector<float> self) => ColumnVector<float>.Multiply(scalar, self);
    public static ColumnVector<float> Multiply(float scalar, ColumnVector<float> self) => ColumnVector<float>.Multiply(scalar, self);
    public static float Norm(ColumnVector<float> self) => ColumnVector<float>.Norm(self);
    public static ColumnVector<float> Normalized(ColumnVector<float> self) => ColumnVector<float>.Normalized(self);
    public static ComplexNumber<float> InnerProduct(ColumnVector<float> left, ColumnVector<float> right) => ColumnVector<float>.InnerProduct(left, right);
    public static ColumnVector<float> Orthonormal(ColumnVector<float> self) => ColumnVector<float>.Orthonormal(self);
    public static ColumnVector<float> Round(ColumnVector<float> self) => ColumnVector<float>.Round(self);
    public static ColumnVector<float> Subtract(ColumnVector<float> left, ColumnVector<float> right) => ColumnVector<float>.Subtract(left, right);
    public static ComplexNumber<float> Sum(ColumnVector<float> self) => ColumnVector<float>.Sum(self);
    public static ColumnVector<float> TensorProduct(ColumnVector<float> left, ColumnVector<float> right) => ColumnVector<float>.TensorProduct(left, right);
    public static RowVector<float> Transpose(ColumnVector<float> self) => ColumnVector<float>.Transpose(self);
    public static ColumnVector<float> Zip(ColumnVector<float> first, ColumnVector<float> second, Func<ComplexNumber<float>, ComplexNumber<float>, ComplexNumber<float>> elementMapping) => ColumnVector<float>.Zip(first, second, elementMapping);

    public static SquareMatrix<float> M(ComplexNumber<float>[,] entries) => SquareMatrix<float>.M(entries);
    public static SquareMatrix<float> M(ComplexNumber<double>[,] entries) => SquareMatrix<float>.M(entries);
    public static SquareMatrix<float> M(int m, Func<int, int, ComplexNumber<float>> initializer) => SquareMatrix<float>.M(m, initializer);
    public static SquareMatrix<float> Identity(int m) => SquareMatrix<float>.Identity(m);

    public static bool AreEquivalent(SquareMatrix<float> left, SquareMatrix<float> right) => SquareMatrix<float>.AreEquivalent(left, right);
    public static SquareMatrix<float> Adjoint(SquareMatrix<float> self) => SquareMatrix<float>.Adjoint(self);
    public static IEnumerable<ComplexNumber<float>> Column(SquareMatrix<float> self, int j) => SquareMatrix<float>.Column(self, j);
    public static SquareMatrix<float> Add(SquareMatrix<float> left, SquareMatrix<float> right) => SquareMatrix<float>.Add(left, right);
    public static SquareMatrix<float> AdditiveInverse(SquareMatrix<float> self) => SquareMatrix<float>.AdditiveInverse(self);
    public static SquareMatrix<float> Commutator(SquareMatrix<float> left, SquareMatrix<float> right) => SquareMatrix<float>.Commutator(left, right);
    public static SquareMatrix<float> Conjucate(SquareMatrix<float> self) => SquareMatrix<float>.Conjucate(self);
    public static bool IsHermitian(SquareMatrix<float> self) => SquareMatrix<float>.IsHermitian(self);
    public static bool IsIdentity(SquareMatrix<float> self) => SquareMatrix<float>.IsIdentity(self);
    public static bool IsUnitary(SquareMatrix<float> self) => SquareMatrix<float>.IsUnitary(self);
    public static int M(SquareMatrix<float> self) => SquareMatrix<float>.M(self);
    public static SquareMatrix<float> Map(SquareMatrix<float> source, Func<ComplexNumber<float>, ComplexNumber<float>> elementMapping) => SquareMatrix<float>.Map(source, elementMapping);
    public static SquareMatrix<float> Multiply(ComplexNumber<float> scalar, SquareMatrix<float> self) => SquareMatrix<float>.Multiply(scalar, self);
    public static SquareMatrix<float> Multiply(float scalar, SquareMatrix<float> self) => SquareMatrix<float>.Multiply(scalar, self);
    public static int N(SquareMatrix<float> self) => SquareMatrix<float>.N(self);
    public static SquareMatrix<float> Round(SquareMatrix<float> self) => SquareMatrix<float>.Round(self);
    public static IEnumerable<ComplexNumber<float>> Row(SquareMatrix<float> self, int i) => SquareMatrix<float>.Row(self, i);
    public static SquareMatrix<float> Subtract(SquareMatrix<float> left, SquareMatrix<float> right) => SquareMatrix<float>.Subtract(left, right);
    public static SquareMatrix<float> TensorProduct(SquareMatrix<float> left, SquareMatrix<float> right) => SquareMatrix<float>.TensorProduct(left, right);
    public static SquareMatrix<float> Transpose(SquareMatrix<float> self) => SquareMatrix<float>.Transpose(self);
    public static SquareMatrix<float> Zip(SquareMatrix<float> left, SquareMatrix<float> right, Func<ComplexNumber<float>, ComplexNumber<float>, ComplexNumber<float>> elementMapping) => SquareMatrix<float>.Zip(left, right, elementMapping);
}