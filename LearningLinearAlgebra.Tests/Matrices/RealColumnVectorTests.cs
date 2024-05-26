using LearningLinearAlgebra.Matrices.Real;
using LearningLinearAlgebra.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

using static LearningLinearAlgebra.Matrices.Real.SquareMatrix<float>;
using static LearningLinearAlgebra.Matrices.Real.ColumnVector<float>;
using static LearningLinearAlgebra.Numbers.RealNumber<float>;

namespace LearningLinearAlgebra.Tests.Matrices;

public class RealColumnVectorTests
{
    [Fact]
    public void Sum_of_two_vectors_is_calculated_as_sum_of_the_components()
    {
        var a = V([-1, -3]);
        var b = V([-7, -13]);

        var sum = Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().Equal(V([-8, -16]));
        (a + b).Should().Equal(Add(a, b));
        a.Add(b).Should().Equal(Add(a, b));
    }

    [Fact]
    public void Sum_of_vectors_is_commutative()
    {
        var a = V([-1, -3]);
        var b = V([-7, -13]);

        (a + b).Should().Equal(b + a);
    }

    [Fact]
    public void Sum_of_vectors_is_associative()
    {
        var a = V([-1, -3]);
        var b = V([-7, -13]);
        var c = V([-23, -31]);

        (a + (b + c)).Should().Equal(a + b + c);
    }

    [Fact]
    public void Sum_of_vector_and_its_the_inverse_is_zero()
    {
        var vector = V([-1, -3]);

        var zero = ColumnVector<float>.Zero(2);

        (vector + -vector).Should().Equal(zero);
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var vector = V([-1, -3]);
        var zero = ColumnVector<float>.Zero(2);

        using var _ = new AssertionScope();

        (vector + zero).Should().Equal(vector);
        (zero + vector).Should().Equal(vector);
    }

    [Fact]
    public void Difference_of_two_vectors_is_calculated_as_difference_of_the_components()
    {
        var a = V([1, 3]);
        var b = V([7, 13]);

        var difference = Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().Equal(V([-6, -10]));
        (a - b).Should().Equal(Subtract(a, b));
    }

    [Fact]
    public void When_multiplying_a_vector_by_scalar_then_each_element_of_the_vector_is_multiplied_by_the_scalar()
    {
        var scalar = R(5);
        var vector = V([11, 19]);

        var multiplied = Multiply(scalar, vector);

        using var _ = new AssertionScope();

        multiplied.Should().Equal(V([55, 95]));
        (scalar * vector).Should().Equal(Multiply(scalar, vector));
    }

    [Fact]
    public void Scalar_multiplication_respects_vector_multiplication()
    {
        var scalarA = R(-3);
        var scalarB = R(-7);
        var vector = V([-23, -31]);

        (scalarA * scalarB * vector).Should().Equal(scalarA * (scalarB * vector));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = R(-3);
        var vectorA = V([-7, -13]);
        var vectorB = V([-23, -31]);

        (scalar * vectorA + scalar * vectorB).Should().Equal(scalar * (vectorA + vectorB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_vector_addition()
    {
        var scalarA = R(-3);
        var scalarB = R(-7);
        var vector = V([-23, -31]);

        (scalarA * vector + scalarB * vector).Should().Equal((scalarA + scalarB) * vector);
    }

    [Fact]
    public void Transpose_of_a_column_vector_is_row_vector_with_same_entries_of_the_original_vector()
    {
        var vector = V([5, 3, -7]);

        var transpose = ColumnVector<float>.Transpose(vector);

        using var _ = new AssertionScope();

        transpose.Should().Equal(RowVector<float>.V([5, 3, -7]));
        vector.Transpose().Should().Equal(transpose);
    }

    [Fact]
    public void Inner_product_is_a_sum_of_products_of_vector_components()
    {
        var a = V([5, 3, -7]);
        var b = V([6, 2, 0]);

        var innerProduct = InnerProduct(a, b);

        using var _ = new AssertionScope();

        innerProduct.Should().Be(R(36));
        a.InnerProduct(b).Should().Be(InnerProduct(a, b));
        (a * b).Should().Be(InnerProduct(a, b));
    }

    [Fact]
    public void Inner_product_respects_addition()
    {
        var a = V([-1, -3]);
        var b = V([-7, -13]);
        var c = V([-23, -31]);

        (a * c + b * c).Should().Be((a + b) * c);
    }

    [Fact]
    public void Inner_product_respects_scalar_multiplication()
    {
        var a = V([-1, -3]);
        var b = V([-7, -13]);
        var scalar = R(23);

        (scalar * (a * b)).Should().Be(scalar * a * b);
    }

    [Fact]
    public void Norm_is_square_root_of_inner_product_of_vector_with_itself()
    {
        var vector = V([3, -6, 2]);

        var norm = Norm(vector);

        using var _ = new AssertionScope();

        norm.Should().Be(R(7));
        vector.Norm().Should().Be(Norm(vector));
    }

    [Fact]
    public void Vector_can_be_normalized_to_have_length_of_one_by_dividing_it_by_its_length()
    {
        var vector = V([3, -6, 2]);

        var normalized = Normalized(vector);

        using var _ = new AssertionScope();

        normalized.Should().Equal(1 / Sqrt(vector * vector) * vector);
        vector.Normalized().Should().Equal(Normalized(vector));
    }

    [Fact]
    public void Distance_of_the_two_vectors_is_the_norm_of_the_difference()
    {
        var a = V([3, 1, 2]);
        var b = V([2, 2, -1]);

        var distance = Distance(a, b);

        using var _ = new AssertionScope();

        distance.Should().Be(Sqrt(11));
        a.Distance(b).Should().Be(Distance(a, b));
    }

    [Fact]
    public void Tensor_product_of_vectors_contains_combinations_of_products_of_all_elements_of_both_vectors()
    {

        var a = V([3, 4, 7]);
        var b = V([-1, 2]);

        var tensorProduct = TensorProduct(a, b);

        using var _ = new AssertionScope();

        tensorProduct.Should().Equal(V([-3, -4, -7, 6, 8, 14]));
        a.TensorProduct(b).Should().Equal(TensorProduct(a, b));
    }

    [Fact]
    public void Another_example_of_tensor_product()
    {

        var a = V([-1, 2]);
        var b = V([3, 4, 7]);

        var tensorProduct = TensorProduct(a, b);

        tensorProduct.Should().Equal(V([-3, 6, -4, 8, -7, 14]));
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = V([-1, -3]);
        var b = V([-7, -13]);
        var c = V([-23, -31]);

        TensorProduct(TensorProduct(a, b), c).Should().Equal(TensorProduct(a, TensorProduct(b, c)));
    }

    [Fact]
    public void Vector_is_converted_from_linearly_independent_base_to_orthonormal_base_by_dividing_it_by_its_norm()
    {
        var I = V([3, 0, 0]);
        var II = V([0, 1, 2]);
        var III = V([0, 25]);

        using var _ = new AssertionScope();

        Orthonormal(I).Should().Equal(1 / Sqrt(I * I) * I);
        Orthonormal(II).Should().Equal(1 / Sqrt(II * II) * II);
        Orthonormal(III).Should().Equal(1 / Sqrt(III * III) * III);

        I.Orthonormal().Should().Equal(Orthonormal(I));
    }
}