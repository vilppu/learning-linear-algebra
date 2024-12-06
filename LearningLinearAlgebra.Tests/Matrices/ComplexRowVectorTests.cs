using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using static LearningLinearAlgebra.Numbers.ComplexNumber<float>;
using static LearningLinearAlgebra.Matrices.Complex.Managed.ColumnVector<float>;
using static LearningLinearAlgebra.Matrices.Complex.Managed.RowVector<float>;
using static LearningLinearAlgebra.Numbers.RealNumber<float>;
using LearningLinearAlgebra.Matrices.Complex.Abstract;
using LearningLinearAlgebra.Matrices.Complex.Managed;

namespace LearningLinearAlgebra.Tests.Matrices;

public class ComplexRowVectorTests
{
    [Fact]
    public void Sum_of_two_vectors_is_calculated_as_sum_of_the_components()
    {
        var a = U([(1, 2), (3, 5)]);
        var b = U([(7, 11), (13, 19)]);

        var sum = Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().Equal(U([(8, 13), (16, 24)]));
        (a + b).Should().Equal(Add(a, b));
        a.Add(b).Should().Equal(Add(a, b));
    }

    [Fact]
    public void Sum_of_complex_vectors_is_commutative()
    {
        var a = U([(1, 2), (3, 5)]);
        var b = U([(7, 11), (13, 19)]);

        (b + a).Should().Equal(a + b);
    }

    [Fact]
    public void Sum_of_complex_vectors_is_associative()
    {
        var a = U([(1, 2), (3, 5)]);
        var b = U([(7, 11), (13, 19)]);
        var c = U([(23, 29), (31, 37)]);

        (a + (b + c)).Should().Equal(a + b + c);
    }

    [Fact]
    public void Sum_of_vector_and_its_the_inverse_is_zero()
    {
        var vector = U([(1, 2), (3, 5)]);

        var zero = RowVector<float>.Zero(2);

        (vector + -vector).Should().Equal(zero);
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var vector = U([(1, 2), (3, 5)]);

        var zero = RowVector<float>.Zero(2);

        using var _ = new AssertionScope();

        (vector + zero).Should().Equal(vector);
        (zero + vector).Should().Equal(vector);
    }

    [Fact]
    public void Difference_of_two_vectors_is_calculated_as_difference_of_the_components()
    {
        var a = U([(1, 2), (3, 5)]);

        var b = U([(7, 11), (13, 19)]);

        var difference = Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().Equal(U([(-6, -9), (-10, -14)]));
        (a - b).Should().Equal(Subtract(a, b));
        a.Subtract(b).Should().Equal(Subtract(a, b));
    }

    [Fact]
    public void When_multiplying_a_vector_by_scalar_then_each_element_of_the_vector_is_multiplied_by_the_scalar()
    {
        var scalar = C(5, 7);
        var vector = U([(11, 13), (19, 21)]);

        var multiplied = Multiply(scalar, vector);

        using var _ = new AssertionScope();

        multiplied.Should().Equal(U([(-36, 142), (-52, 238)]));
        (scalar * vector).Should().Equal(Multiply(scalar, vector));
        scalar.Multiply(vector).Should().Equal(Multiply(scalar, vector));
    }

    [Fact]
    public void Complex_vector_can_by_multiplied_by_a_real_scalar()
    {
        var scalar = R(5);
        var vector = U([(1, 0), (2, 0)]);

        var multiplied = Multiply(scalar, vector);

        using var _ = new AssertionScope();

        multiplied.Should().Equal(U([(5, 0), (10, 0)]));
        (scalar * vector).Should().Equal(Multiply(scalar, vector));
        scalar.Multiply(vector).Should().Equal(Multiply(scalar, vector));
    }

    [Fact]
    public void Scalar_multiplication_respects_complex_multiplication()
    {
        var scalarA = C(3, 5);
        var scalarB = C(7, 11);

        var vector = U([(23, 29), (31, 37)]);

        (scalarA * scalarB * vector).Should().Equal(scalarA * (scalarB * vector));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = C(3, 5);
        var vectorA = U([(7, 11), (13, 19)]);

        var vectorB = U([(23, 29), (31, 37)]);

        (scalar * vectorA + scalar * vectorB).Should().Equal(scalar * (vectorA + vectorB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_complex_addition()
    {
        var scalarA = C(3, 5);
        var scalarB = C(7, 11);

        var vector = U([(23, 29), (31, 37)]);

        (scalarA * vector + scalarB * vector).Should().Equal((scalarA + scalarB) * vector);
    }

    [Fact]
    public void Conjucate_of_a_vector_is_where_each_element_is_a_complex_conjucate_of_the_original_vector()
    {
        var vector = U([(1, 2), (3, 5)]);

        var conjucate = Conjucate(vector);

        using var _ = new AssertionScope();

        conjucate.Should().Equal(U([(1, -2), (3, -5)]));
        vector.Conjucate().Should().Equal(conjucate);
    }

    [Fact]
    public void Transpose_of_a_row_vector_is_column_vector_with_same_entries_of_the_original_vector()
    {
        var vector = U([(1, 2), (3, 5)]);

        var transpose = Transpose(vector);

        using var _ = new AssertionScope();

        transpose.Should().Equal(V([(1, 2), (3, 5)]));
        vector.Transpose().Should().Equal(transpose);
    }

    [Fact]
    public void Adjoint_of_a_row_vector_is_column_vector_where_each_entry_is_a_complex_conjucate_of_the_original_vector()
    {
        var vector = U([(1, 2), (3, 5)]);

        var adjoint = Adjoint(vector);

        using var _ = new AssertionScope();

        adjoint.Should().Equal(V([(1, -2), (3, -5)]));
        vector.Adjoint().Should().Equal(adjoint);
    }

    [Fact]
    public void Product_of_row_vector_and_column_vector_is_sum_of_products_of_vector_components()
    {
        var a = U([(1, 2), (3, 5)]);
        var b = V([(7, 11), (13, 19)]);

        var product = Multiply(a, b);

        using var _ = new AssertionScope();

        product.Should().BeEquivalentTo(C(-71, 147));
        a.Multiply(b).Should().Be(Multiply(a, b));
        (a * b).Should().BeEquivalentTo(Multiply(a, b));
    }

    [Fact]
    public void Inner_product_is_a_sum_of_products_of_left_vector_components_and_conjucates_of_right_vector_components()
    {
        var a = U([(1, 2), (3, 5)]);
        var b = U([(7, 11), (13, 19)]);

        var innerProduct = InnerProduct(a, b);

        using var _ = new AssertionScope();

        innerProduct.Should().BeEquivalentTo(C(163, 11));
        (a * b).Should().BeEquivalentTo(InnerProduct(a, b));
    }

    [Fact]
    public void Inner_product_respects_addition()
    {
        var a = U([(1, 2), (3, 5)]);

        var b = U([(7, 11), (13, 19)]);

        var c = U([(23, 29), (31, 37)]);

        (a * c + b * c).Should().BeEquivalentTo((a + b) * c);
    }

    [Fact]
    public void Inner_product_respects_scalar_multiplication()
    {
        var a = U([(1, 2), (3, 5)]);

        var b = U([(7, 11), (13, 19)]);

        var scalar = (23, 29);

        (scalar * (a * b)).Should().BeEquivalentTo(scalar * a * b);
    }

    [Fact]
    public void Inner_product_of_a_complex_vector_with_itself_is_a_real_number()
    {
        var vector = U([(1, 2), (3, 5)]);

        var innerProduct = InnerProduct(vector, vector);

        innerProduct.Should().BeEquivalentTo(C(39, 0));
    }

    [Fact]
    public void Norm_is_square_root_of_inner_product_of_vector_with_itself()
    {
        var vector = U([(4, 3), (6, -4), (12, -7), (0, 13)]);

        var norm = Norm(vector);

        using var _ = new AssertionScope();

        norm.Should().Be(Sqrt(439));
        vector.Norm().Should().Be(Norm(vector));
    }

    [Fact]
    public void Vector_can_be_normalized_to_have_length_of_one_by_dividing_it_by_its_length()
    {
        var vector = U([(3, 1), (2, 5), (-1, 0)]);

        var normalized = Normalized(vector);

        using var _ = new AssertionScope();

        normalized.Should().Equal(1 / Sqrt(vector * vector) * vector);
        vector.Normalized().Should().Equal(Normalized(vector));
    }

    [Fact]
    public void Distance_of_the_two_vectors_is_the_norm_of_the_difference()
    {

        var a = U([(1, 2), (3, 5)]);
        var b = U([(7, 11), (13, 19)]);

        var distance = Distance(a, b);

        using var _ = new AssertionScope();

        distance.Should().Be(Sqrt(413));
        a.Distance(b).Should().Be(Distance(a, b));
    }

    [Fact]
    public void Tensor_product_of_vectors_contains_combinations_of_products_of_all_elements_of_both_vectors()
    {
        var a = U([(1, 2), (3, 5)]);
        var b = U([(7, 11), (13, 19)]);

        var tensorProduct = TensorProduct(a, b);

        tensorProduct.Should().Equal(U([(-15, 25), (-25, 45), (-34, 68), (-56, 122)]));
        a.TensorProduct(b).Should().Equal(TensorProduct(a, b));
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = U([(1, 2), (3, 5)]);
        var b = U([(7, 11), (13, 19)]);
        var c = U([(23, 29), (31, 37)]);

        TensorProduct(a, TensorProduct(b, c)).Should().Equal(TensorProduct(TensorProduct(a, b), c));
    }
}