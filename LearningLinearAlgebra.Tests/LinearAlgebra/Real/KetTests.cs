using FluentAssertions;
using FluentAssertions.Execution;
using LearningLinearAlgebra.LinearAlgebra.RealVectorSpace;
using LearningLinearAlgebra.Numbers;
using Xunit;
using static LearningLinearAlgebra.LinearAlgebra.RealVectorSpace.Bra<float>;
using static LearningLinearAlgebra.LinearAlgebra.RealVectorSpace.Ket<float>;
using static LearningLinearAlgebra.Numbers.RealNumber<float>;

namespace LearningLinearAlgebra.Tests.LinearAlgebra.Real;

public class KetTests
{
    [Fact]
    public void Dimension_of_the_vector_is_the_number_of_elements_in_basis_vector()
    {
        var ket = V([1, 3, 7]);

        var dimension = Dimension(ket);

        using var _ = new AssertionScope();

        dimension.Should().Be(3);
        ket.Dimension().Should().Be(Dimension(ket));
    }

    [Fact]
    public void Addition_is_calculated_as_addition_of_components()
    {
        var a = V([1, 3]);
        var b = V([7, 13]);

        var sum = Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().Equal(V([8, 16]));
        (a + b).Should().Equal(Add(a, b));
        a.Add(b).Should().Equal(Add(a, b));
    }

    [Fact]
    public void Subtraction_is_calculated_as_subtraction_of_components()
    {
        var a = V([1, 3]);
        var b = V([7, 13]);

        var difference = Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().Equal(V([-6, -10]));
        (a - b).Should().Equal(Subtract(a, b));
        a.Subtract(b).Should().Equal(Subtract(a, b));
    }

    [Fact]
    public void Scalar_multiplication_is_calculated_as_scalar_multiplication_of_components()
    {
        const float scalar = 6.0f;
        var ket = V([1, 3]);

        var product = Multiply(scalar, ket);

        using var _ = new AssertionScope();

        product.Should().Equal(V([6, 18]));
        (scalar * ket).Should().Equal(Multiply(scalar, ket));
        scalar.Multiply(ket).Should().Equal(Multiply(scalar, ket));
    }

    [Fact]
    public void Additive_inverse_is_calculated_as_additive_inverse_of_components()
    {
        var ket = V([1, 3]);

        var additiveInverse = AdditiveInverse(ket);

        using var _ = new AssertionScope();

        additiveInverse.Should().Equal(V([-1, -3]));
        (-ket).Should().Equal(AdditiveInverse(ket));
        ket.AdditiveInverse().Should().Equal(AdditiveInverse(ket));
    }

    [Fact]
    public void Ket_can_be_multiplied_bra_by_applying_the_rules_of_matrix_multiplication()
    {
        var a = U([1, 3]);
        var b = V([7, 13]);

        var product = Multiply(a, b);

        using var _ = new AssertionScope();

        product.Should().Be(46);
        a.Multiply(b).Should().Be(Multiply(a, b));
        (a * b).Should().Be(Multiply(a, b));
    }

    [Fact]
    public void Tensor_product_of_vectors_contains_combinations_of_products_of_all_elements_of_both_vectors()
    {
        var a = V([1, 3]);
        var b = V([7, 13]);

        var tensorProduct = TensorProduct(a, b);

        tensorProduct.Should().Equal(V([7, 21, 13, 39]));
        a.TensorProduct(b).Should().Equal(TensorProduct(a, b));
    }

    [Fact]
    public void More_examples_of_tensor_product()
    {
        TensorProduct(V([1, 0]), V([1, 0])).Should().Equal(V([1, 0, 0, 0]));
        TensorProduct(V([1, 0]), V([0, 1])).Should().Equal(V([0, 0, 1, 0]));
        TensorProduct(V([0, 1]), V([1, 0])).Should().Equal(V([0, 1, 0, 0]));
        TensorProduct(V([0, 1]), V([0, 1])).Should().Equal(V([0, 0, 0, 1]));
    }

    [Fact]
    public void Bra_is_a_complex_adjoint_of_ket()
    {
        var a = V([1, 3]);

        var bra = Bra(a);

        using var _ = new AssertionScope();

        bra.Should().Equal(U([1, 3]));
        a.Bra().Should().Equal(Bra(a));
    }

    [Fact]
    public void Norm_that_is_the_length_of_the_vector_is_calculated_as_square_root_of_the_inner_product_of_vector_with_itself()
    {
        var ket = V([4, 6, 12, 0]);

        var norm = Norm(ket);

        using var _ = new AssertionScope();

        norm.Should().Be(Sqrt(196));
        ket.Norm().Should().Be(Norm(ket));
    }

    [Fact]
    public void Distance_of_the_two_vectors_is_the_norm_of_the_difference()
    {

        var a = V([1, 3]);
        var b = V([7, 13]);

        var distance = Distance(a, b);

        using var _ = new AssertionScope();

        distance.Should().Be(Sqrt(136));
        a.Distance(b).Should().Be(Distance(a, b));
    }

    [Fact]
    public void Normalization_changes_norm_to_one_but_preserves_the_ratio_of_the_components()
    {
        var ket = V([1, 3]);

        var normalized = Normalized(ket);

        using var _ = new AssertionScope();

        normalized.Should().Equal(1 / Sqrt(ket * ket) * ket);
        ket.Normalized().Should().Equal(Normalized(ket));
        normalized.Norm().Round().Should().Be(R(1));
    }
}