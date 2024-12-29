using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Computation.Numbers;
using LearningLinearAlgebra.RealVectorSpace;
using static Computation.Numbers.RealNumber<float>;
using static LearningLinearAlgebra.RealVectorSpace.Bra<float>;
using static LearningLinearAlgebra.RealVectorSpace.Ket<float>;

namespace LearningLinearAlgebra.Tests.LinearAlgebra.Real;

public class BraTests
{
    [Fact]
    public void Dimension_of_the_vector_is_the_number_of_elements_in_basis_vector()
    {
        var bra = U([1, 3, 7]);

        var dimension = Dimension(bra);

        using var _ = new AssertionScope();

        dimension.Should().Be(3);
        bra.Dimension().Should().Be(Dimension(bra));
    }

    [Fact]
    public void Addition_is_calculated_as_addition_of_components()
    {
        var a = U([1, 3]);
        var b = U([7, 13]);

        var sum = Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(U([8, 16]));
        (a + b).Should().BeEquivalentTo(Add(a, b));
        a.Add(b).Should().BeEquivalentTo(Add(a, b));
    }

    [Fact]
    public void Subtraction_is_calculated_as_subtraction_of_components()
    {
        var a = U([1, 3]);
        var b = U([7, 13]);

        var difference = Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(U([-6, -10]));
        (a - b).Should().BeEquivalentTo(Subtract(a, b));
        a.Subtract(b).Should().BeEquivalentTo(Subtract(a, b));
    }

    [Fact]
    public void Scalar_multiplication_is_calculated_as_scalar_multiplication_of_components()
    {
        const float scalar = 6.0f;
        var bra = U([1, 3]);

        var product = Multiply(scalar, bra);

        using var _ = new AssertionScope();

        product.Should().BeEquivalentTo(U([6, 18]));
        (scalar * bra).Should().BeEquivalentTo(Multiply(scalar, bra));
        scalar.Multiply(bra).Should().BeEquivalentTo(Multiply(scalar, bra));
    }

    [Fact]
    public void Additive_inverse_is_calculated_as_additive_inverse_of_components()
    {
        var bra = U([1, 3]);

        var additiveInverse = AdditiveInverse(bra);

        using var _ = new AssertionScope();

        additiveInverse.Should().BeEquivalentTo(U([-1, -3]));
        (-bra).Should().BeEquivalentTo(AdditiveInverse(bra));
        bra.AdditiveInverse().Should().BeEquivalentTo(AdditiveInverse(bra));
    }

    [Fact]
    public void Tensor_product_of_vectors_contains_combinations_of_products_of_all_elements_of_both_vectors()
    {
        var a = U([1, 3]);
        var b = U([7, 13]);

        var tensorProduct = TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(U([7, 21, 13, 39]));
        a.TensorProduct(b).Should().BeEquivalentTo(TensorProduct(a, b));
    }

    [Fact]
    public void More_examples_of_tensor_product()
    {
        using var _ = new AssertionScope();

        TensorProduct(U([1, 0]), U([1, 0])).Should().BeEquivalentTo(U([1, 0, 0, 0]));
        TensorProduct(U([1, 0]), U([0, 1])).Should().BeEquivalentTo(U([0, 0, 1, 0]));
        TensorProduct(U([0, 1]), U([1, 0])).Should().BeEquivalentTo(U([0, 1, 0, 0]));
        TensorProduct(U([0, 1]), U([0, 1])).Should().BeEquivalentTo(U([0, 0, 0, 1]));
    }


    [Fact]
    public void Ket_is_a_complex_adjoint_of_bra()
    {
        var bra = U([1, 3]);

        var ket = Ket(bra);

        using var _ = new AssertionScope();

        ket.Should().BeEquivalentTo(V([1, 3]));
        bra.Ket().Should().BeEquivalentTo(Ket(bra));
    }

    [Fact]
    public void Norm_that_is_the_length_of_the_vector_is_calculated_as_square_root_of_the_inner_product_of_vector_with_itself()
    {
        var bra = U([4, 6, 12, 0]);

        var norm = Norm(bra);

        using var _ = new AssertionScope();

        norm.Should().Be(Sqrt(196));
        bra.Norm().Should().Be(Norm(bra));
    }

    [Fact]
    public void Distance_of_the_two_vectors_is_the_norm_of_the_difference()
    {
        var a = U([1, 3]);
        var b = U([7, 13]);

        var distance = Distance(a, b);

        using var _ = new AssertionScope();

        distance.Should().Be(Sqrt(136));
        a.Distance(b).Should().Be(Distance(a, b));
    }

    [Fact]
    public void Normalization_changes_norm_to_one_but_preserves_the_ratio_of_the_components()
    {
        var bra = U([1, 3]);

        var normalized = Normalized(bra);

        using var _ = new AssertionScope();

        normalized.Should().BeEquivalentTo(1 / Sqrt(bra * bra) * bra);
        bra.Normalized().Should().BeEquivalentTo(Normalized(bra));
        normalized.Norm().Round().Should().Be(R(1));
    }
}