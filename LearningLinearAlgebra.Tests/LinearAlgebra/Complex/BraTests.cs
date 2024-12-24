using FluentAssertions;
using FluentAssertions.Execution;
using LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace;
using LearningLinearAlgebra.Numbers;
using Xunit;
using static LearningLinearAlgebra.Numbers.ComplexNumber<float>;
using static LearningLinearAlgebra.ComplexVectorSpace.Bra<float>;
using static LearningLinearAlgebra.ComplexVectorSpace.Ket<float>;
using static LearningLinearAlgebra.Numbers.RealNumber<float>;

namespace LearningLinearAlgebra.Tests.LinearAlgebra.Complex;

public class BraTests
{
    [Fact]
    public void Dimension_of_the_vector_is_the_number_of_elements_in_basis_vector()
    {
        var bra = U([(1, 2), (3, 5), (7, 9)]);

        var dimension = Dimension(bra);

        using var _ = new AssertionScope();

        dimension.Should().Be(3);
        bra.Dimension().Should().Be(Dimension(bra));
    }

    [Fact]
    public void Addition_is_calculated_as_addition_of_components()
    {
        var a = U([(1, 2), (3, 5)]);
        var b = U([(7, 11), (13, 17)]);

        var sum = Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().Equal(U([(8, 13), (16, 22)]));
        (a + b).Should().Equal(Add(a, b));
        a.Add(b).Should().Equal(Add(a, b));
    }

    [Fact]
    public void Subtraction_is_calculated_as_subtraction_of_components()
    {
        var a = U([(1, 2), (3, 5)]);
        var b = U([(7, 11), (13, 17)]);

        var difference = Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().Equal(U([(-6, -9), (-10, -12)]));
        (a - b).Should().Equal(Subtract(a, b));
        a.Subtract(b).Should().Equal(Subtract(a, b));
    }

    [Fact]
    public void Scalar_multiplication_is_calculated_as_scalar_multiplication_of_components()
    {
        var scalar = C(6, 7);
        var bra = U([(1, 2), (3, 5)]);

        var product = Multiply(scalar, bra);

        using var _ = new AssertionScope();

        product.Should().Equal(U([(-8, 19), (-17, 51)]));
        (scalar * bra).Should().Equal(Multiply(scalar, bra));
        scalar.Multiply(bra).Should().Equal(Multiply(scalar, bra));
    }

    [Fact]
    public void Additive_inverse_is_calculated_as_additive_inverse_of_components()
    {
        var bra = U([(1, 2), (3, 5)]);

        var additiveInverse = AdditiveInverse(bra);

        using var _ = new AssertionScope();

        additiveInverse.Should().Equal(U([(-1, -2), (-3, -5)]));
        (-bra).Should().Equal(AdditiveInverse(bra));
        bra.AdditiveInverse().Should().Equal(AdditiveInverse(bra));
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
    public void Tensor_product_of_vectors_contains_combinations_of_products_of_all_elements_of_both_vectors()
    {
        var a = U([(1, 2), (3, 5)]);
        var b = U([(7, 11), (13, 19)]);

        var tensorProduct = TensorProduct(a, b);

        tensorProduct.Should().Equal(U([(-15, 25), (-25, 45), (-34, 68), (-56, 122)]));
        a.TensorProduct(b).Should().Equal(TensorProduct(a, b));
    }

    [Fact]
    public void More_examples_of_tensor_product()
    {
        TensorProduct(U([(1, 0), (0, 0)]), U([(1, 0), (0, 0)])).Should().Equal(U([(1, 0), (0, 0), (0, 0), (0, 0)]));
        TensorProduct(U([(1, 0), (0, 0)]), U([(0, 0), (1, 0)])).Should().Equal(U([(0, 0), (1, 0), (0, 0), (0, 0)]));
        TensorProduct(U([(0, 0), (1, 0)]), U([(1, 0), (0, 0)])).Should().Equal(U([(0, 0), (0, 0), (1, 0), (0, 0)]));
        TensorProduct(U([(0, 0), (1, 0)]), U([(0, 0), (1, 0)])).Should().Equal(U([(0, 0), (0, 0), (0, 0), (1, 0)]));
    }

    [Fact]
    public void Conjucate_of_a_vector_is_where_each_element_is_a_complex_conjucate_of_the_original_vector()
    {
        var bra = U([(1, 2), (3, 5)]);

        var conjucate = Conjucate(bra);

        using var _ = new AssertionScope();

        conjucate.Should().Equal(U([(1, -2), (3, -5)]));
        bra.Conjucate().Should().Equal(conjucate);
    }

    [Fact]
    public void Ket_is_a_complex_adjoint_of_bra()
    {
        var bra = U([(1, 2), (3, 5)]);

        var ket = Ket(bra);

        using var _ = new AssertionScope();

        ket.Should().BeEquivalentTo(V([(1, -2), (3, -5)]));
        bra.Ket().Should().BeEquivalentTo(Ket(bra));
    }

    [Fact]
    public void Norm_that_is_the_length_of_the_vector_is_calculated_as_square_root_of_the_inner_product_of_vector_with_itself()
    {
        var bra = U([(4, 3), (6, -4), (12, -7), (0, 13)]);

        var norm = Norm(bra);

        using var _ = new AssertionScope();

        norm.Should().Be(Sqrt(439));
        bra.Norm().Should().Be(Norm(bra));
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
    public void Normalization_changes_norm_to_one_but_preserves_the_ratio_of_the_components()
    {
        var bra = U([(1, 2), (3, 5)]);

        var normalized = Normalized(bra);

        using var _ = new AssertionScope();

        normalized.Should().Equal(1 / Sqrt(bra * bra) * bra);
        bra.Normalized().Should().Equal(Normalized(bra));
        normalized.Norm().Round().Should().Be(R(1));
    }
}