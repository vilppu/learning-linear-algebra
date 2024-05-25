using LearningLinearAlgebra.Matrices.Real;
using LearningLinearAlgebra.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Numerics;
using Xunit;

namespace LearningLinearAlgebra.Tests.Matrices;

public class SinglePrecisionCpuRealColumnVectorTests : RealColumnVectorTests<SquareMatrix<float>, RowVector<float>, ColumnVector<float>, float> { }
public class DoublePrecisionCpuRealColumnVectorTests : RealColumnVectorTests<SquareMatrix<double>, RowVector<double>, ColumnVector<double>, double> { }

public abstract class RealColumnVectorTests<TMatrix, TRowVector, TColumnVector, TRealNumber>
    where TMatrix : ISquareMatrix<TMatrix, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    [Fact]
    public void Sum_of_two_vectors_is_calculated_as_sum_of_the_components()
    {
        var a = TColumnVector.V([-1, -3]);
        var b = TColumnVector.V([-7, -13]);

        var sum = TColumnVector.Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(TColumnVector.V([-8, -16]));
        (a + b).Should().BeEquivalentTo(TColumnVector.Add(a, b));
        a.Add(b).Should().BeEquivalentTo(TColumnVector.Add(a, b));
    }

    [Fact]
    public void Sum_of_vectors_is_commutative()
    {
        var a = TColumnVector.V([-1, -3]);
        var b = TColumnVector.V([-7, -13]);

        (a + b).Should().BeEquivalentTo(b + a);
    }

    [Fact]
    public void Sum_of_vectors_is_associative()
    {
        var a = TColumnVector.V([-1, -3]);
        var b = TColumnVector.V([-7, -13]);
        var c = TColumnVector.V([-23, -31]);

        (a + (b + c)).Should().BeEquivalentTo(a + b + c);
    }

    [Fact]
    public void Sum_of_vector_and_its_the_inverse_is_zero()
    {
        var vector = TColumnVector.V([-1, -3]);

        var zero = TColumnVector.Zero(2);

        (vector + -vector).Should().BeEquivalentTo(zero);
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var vector = TColumnVector.V([-1, -3]);
        var zero = TColumnVector.Zero(2);

        using var _ = new AssertionScope();

        (vector + zero).Should().BeEquivalentTo(vector);
        (zero + vector).Should().BeEquivalentTo(vector);
    }

    [Fact]
    public void Difference_of_two_vectors_is_calculated_as_difference_of_the_components()
    {
        var a = TColumnVector.V([1, 3]);
        var b = TColumnVector.V([7, 13]);

        var difference = TColumnVector.Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(TColumnVector.V([-6, -10]));
        (a - b).Should().BeEquivalentTo(TColumnVector.Subtract(a, b));
    }

    [Fact]
    public void When_multiplying_a_vector_by_scalar_then_each_element_of_the_vector_is_multiplied_by_the_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5);
        var vector = TColumnVector.V([11, 19]);

        var multiplied = TColumnVector.Multiply(scalar, vector);

        using var _ = new AssertionScope();

        multiplied.Should().BeEquivalentTo(TColumnVector.V([55, 95]));
        (scalar * vector).Should().BeEquivalentTo(TColumnVector.Multiply(scalar, vector));
    }

    [Fact]
    public void Scalar_multiplication_respects_vector_multiplication()
    {
        var scalarA = RealNumber<TRealNumber>.R(-3);
        var scalarB = RealNumber<TRealNumber>.R(-7);
        var vector = TColumnVector.V([-23, -31]);

        (scalarA * scalarB * vector).Should().BeEquivalentTo(scalarA * (scalarB * vector));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = RealNumber<TRealNumber>.R(-3);
        var vectorA = TColumnVector.V([-7, -13]);
        var vectorB = TColumnVector.V([-23, -31]);

        (scalar * vectorA + scalar * vectorB).Should().BeEquivalentTo(scalar * (vectorA + vectorB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_vector_addition()
    {
        var scalarA = RealNumber<TRealNumber>.R(-3);
        var scalarB = RealNumber<TRealNumber>.R(-7);
        var vector = TColumnVector.V([-23, -31]);

        (scalarA * vector + scalarB * vector).Should().BeEquivalentTo((scalarA + scalarB) * vector);
    }

    [Fact]
    public void Transpose_of_a_column_vector_is_row_vector_with_same_entries_of_the_original_vector()
    {
        var vector = TColumnVector.V([5, 3, -7]);

        var transpose = TColumnVector.Transpose(vector);

        using var _ = new AssertionScope();

        transpose.Should().BeEquivalentTo(TRowVector.V([5, 3, -7]));
        vector.Transpose().Should().BeEquivalentTo(transpose);
    }

    [Fact]
    public void Inner_product_is_a_sum_of_products_of_vector_components()
    {
        var a = TColumnVector.V([5, 3, -7]);
        var b = TColumnVector.V([6, 2, 0]);

        var innerProduct = TColumnVector.InnerProduct(a, b);

        using var _ = new AssertionScope();

        innerProduct.Should().Be(RealNumber<TRealNumber>.R(36));
        a.InnerProduct(b).Should().Be(TColumnVector.InnerProduct(a, b));
        (a * b).Should().Be(TColumnVector.InnerProduct(a, b));
    }

    [Fact]
    public void Inner_product_respects_addition()
    {
        var a = TColumnVector.V([-1, -3]);
        var b = TColumnVector.V([-7, -13]);
        var c = TColumnVector.V([-23, -31]);

        (a * c + b * c).Should().Be((a + b) * c);
    }

    [Fact]
    public void Inner_product_respects_scalar_multiplication()
    {
        var a = TColumnVector.V([-1, -3]);
        var b = TColumnVector.V([-7, -13]);
        var scalar = RealNumber<TRealNumber>.R(23);

        (scalar * (a * b)).Should().Be(scalar * a * b);
    }

    [Fact]
    public void Norm_is_square_root_of_inner_product_of_vector_with_itself()
    {
        var vector = TColumnVector.V([3, -6, 2]);

        var norm = TColumnVector.Norm(vector);

        using var _ = new AssertionScope();

        norm.Should().Be(RealNumber<TRealNumber>.R(7));
        vector.Norm().Should().Be(TColumnVector.Norm(vector));
    }

    [Fact]
    public void Vector_can_be_normalized_to_have_length_of_one_by_dividing_it_by_its_length()
    {
        var vector = TColumnVector.V([3, -6, 2]);

        var normalized = TColumnVector.Normalized(vector);

        using var _ = new AssertionScope();

        normalized.Should().BeEquivalentTo(TRealNumber.One / RealNumber<TRealNumber>.Sqrt(vector * vector) * vector);
        vector.Normalized().Should().BeEquivalentTo(TColumnVector.Normalized(vector));
    }

    [Fact]
    public void Distance_of_the_two_vectors_is_the_norm_of_the_difference()
    {
        var a = TColumnVector.V([3, 1, 2]);
        var b = TColumnVector.V([2, 2, -1]);

        var distance = TColumnVector.Distance(a, b);

        using var _ = new AssertionScope();

        distance.Should().Be(RealNumber<TRealNumber>.Sqrt(11));
        a.Distance(b).Should().Be(TColumnVector.Distance(a, b));
    }

    [Fact]
    public void Tensor_product_of_vectors_contains_combinations_of_products_of_all_elements_of_both_vectors()
    {

        var a = TColumnVector.V([3, 4, 7]);
        var b = TColumnVector.V([-1, 2]);

        var tensorProduct = TColumnVector.TensorProduct(a, b);

        using var _ = new AssertionScope();

        tensorProduct.Should().BeEquivalentTo(TColumnVector.V([-3, 6, -4, 8, -7, 14]));
        a.TensorProduct(b).Should().BeEquivalentTo(TColumnVector.TensorProduct(a, b));
    }

    [Fact]
    public void Another_example_of_tensor_product()
    {

        var a = TColumnVector.V([-1, 2]);
        var b = TColumnVector.V([3, 4, 7]);

        var tensorProduct = TColumnVector.TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(TColumnVector.V([-3, -4, -7, 6, 8, 14]));
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = TColumnVector.V([-1, -3]);
        var b = TColumnVector.V([-7, -13]);
        var c = TColumnVector.V([-23, -31]);

        TColumnVector.TensorProduct(TColumnVector.TensorProduct(a, b), c).Should().BeEquivalentTo(TColumnVector.TensorProduct(a, TColumnVector.TensorProduct(b, c)));
    }

    [Fact]
    public void Vector_is_converted_from_linearly_independent_base_to_orthonormal_base_by_dividing_it_by_its_norm()
    {
        var I = TColumnVector.V([3, 0, 0]);
        var II = TColumnVector.V([0, 1, 2]);
        var III = TColumnVector.V([0, 25]);

        using var _ = new AssertionScope();

        TColumnVector.Orthonormal(I).Should().BeEquivalentTo(TRealNumber.One / RealNumber<TRealNumber>.Sqrt(I * I) * I);
        TColumnVector.Orthonormal(II).Should().BeEquivalentTo(TRealNumber.One / RealNumber<TRealNumber>.Sqrt(II * II) * II);
        TColumnVector.Orthonormal(III).Should().BeEquivalentTo(TRealNumber.One / RealNumber<TRealNumber>.Sqrt(III * III) * III);

        I.Orthonormal().Should().BeEquivalentTo(TColumnVector.Orthonormal(I));
    }
}