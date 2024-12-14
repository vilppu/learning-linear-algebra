using FluentAssertions;
using FluentAssertions.Execution;
using LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace;
using LearningLinearAlgebra.Numbers;
using Xunit;
using static LearningLinearAlgebra.Numbers.ComplexNumber<float>;
using static LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace.Bra<float>;
using static LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace.Ket<float>;
using static LearningLinearAlgebra.Numbers.RealNumber<float>;

namespace LearningLinearAlgebra.Tests.LinearAlgebra.Complex;

public class KetTests
{
    [Fact]
    public void Dimension_of_the_vector_is_the_number_of_elements_in_basis_vector()
    {
        var ket = V([(1, 2), (3, 5), (7, 9)]);

        var dimension = Dimension(ket);

        using var _ = new AssertionScope();

        dimension.Should().Be(3);
        ket.Dimension().Should().Be(Dimension(ket));
    }

    [Fact]
    public void Addition_is_calculated_as_addition_of_components()
    {
        var a = V([(1, 2), (3, 5)]);
        var b = V([(7, 11), (13, 17)]);

        var sum = Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(V([(8, 13), (16, 22)]));
        (a + b).Should().BeEquivalentTo(Add(a, b));
        a.Add(b).Should().BeEquivalentTo(Add(a, b));
    }

    [Fact]
    public void Subtraction_is_calculated_as_subtraction_of_components()
    {
        var a = V([(1, 2), (3, 5)]);
        var b = V([(7, 11), (13, 17)]);

        var difference = Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(V([(-6, -9), (-10, -12)]));
        (a - b).Should().BeEquivalentTo(Subtract(a, b));
        a.Subtract(b).Should().BeEquivalentTo(Subtract(a, b));
    }

    [Fact]
    public void Scalar_multiplication_is_calculated_as_scalar_multiplication_of_components()
    {
        var scalar = C(6, 7);
        var ket = V([(1, 2), (3, 5)]);

        var product = Multiply(scalar, ket);

        using var _ = new AssertionScope();

        product.Should().BeEquivalentTo(V([(-8, 19), (-17, 51)]));
        (scalar * ket).Should().BeEquivalentTo(Multiply(scalar, ket));
        scalar.Multiply(ket).Should().BeEquivalentTo(Multiply(scalar, ket));
    }

    [Fact]
    public void Additive_inverse_is_calculated_as_additive_inverse_of_components()
    {
        var ket = V([(1, 2), (3, 5)]);

        var additiveInverse = AdditiveInverse(ket);

        using var _ = new AssertionScope();

        additiveInverse.Should().BeEquivalentTo(V([(-1, -2), (-3, -5)]));
        (-ket).Should().BeEquivalentTo(AdditiveInverse(ket));
        ket.AdditiveInverse().Should().BeEquivalentTo(AdditiveInverse(ket));
    }

    [Fact]
    public void Ket_can_be_multiplied_bra_by_applying_the_rules_of_matrix_multiplication()
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
        var a = V([(1, 2), (3, 5)]);
        var b = V([(7, 11), (13, 19)]);

        var innerProduct = InnerProduct(a, b);

        using var _ = new AssertionScope();

        innerProduct.Should().BeEquivalentTo(C(163, 11));
        (a * b).Should().BeEquivalentTo(InnerProduct(a, b));
    }

    [Fact]
    public void Tensor_product_of_vectors_contains_combinations_of_products_of_all_elements_of_both_vectors()
    {
        var a = V([(1, 2), (3, 5)]);
        var b = V([(7, 11), (13, 19)]);

        var tensorProduct = TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(V([(-15, 25), (-25, 45), (-34, 68), (-56, 122)]));
        a.TensorProduct(b).Should().BeEquivalentTo(TensorProduct(a, b));
    }

    [Fact]
    public void More_examples_of_tensor_product()
    {
        TensorProduct(V([(1, 0), (0, 0)]), V([(1, 0), (0, 0)])).Should().BeEquivalentTo(V([(1, 0), (0, 0), (0, 0), (0, 0)]));
        TensorProduct(V([(1, 0), (0, 0)]), V([(0, 0), (1, 0)])).Should().BeEquivalentTo(V([(0, 0), (1, 0), (0, 0), (0, 0)]));
        TensorProduct(V([(0, 0), (1, 0)]), V([(1, 0), (0, 0)])).Should().BeEquivalentTo(V([(0, 0), (0, 0), (1, 0), (0, 0)]));
        TensorProduct(V([(0, 0), (1, 0)]), V([(0, 0), (1, 0)])).Should().BeEquivalentTo(V([(0, 0), (0, 0), (0, 0), (1, 0)]));
    }

    [Fact]
    public void Conjucate_of_a_vector_is_where_each_element_is_a_complex_conjucate_of_the_original_vector()
    {
        var ket = V([(1, 2), (3, 5)]);

        var conjucate = Conjucate(ket);

        using var _ = new AssertionScope();

        conjucate.Should().BeEquivalentTo(V([(1, -2), (3, -5)]));
        ket.Conjucate().Should().BeEquivalentTo(conjucate);
    }

    [Fact]
    public void Bra_is_a_complex_adjoint_of_ket()
    {
        var a = V([(1, 2), (3, 5)]);

        var bra = Bra(a);

        using var _ = new AssertionScope();

        bra.Should().Equal(U([(1, -2), (3, -5)]));
        a.Bra().Should().Equal(Bra(a));
    }

    [Fact]
    public void Norm_that_is_the_length_of_the_vector_is_calculated_as_square_root_of_the_inner_product_of_vector_with_itself()
    {
        var ket = V([(4, 3), (6, -4), (12, -7), (0, 13)]);

        var norm = Norm(ket);

        using var _ = new AssertionScope();

        norm.Should().Be(Sqrt(439));
        ket.Norm().Should().Be(Norm(ket));
    }

    [Fact]
    public void Distance_of_the_two_vectors_is_the_norm_of_the_difference()
    {

        var a = V([(1, 2), (3, 5)]);
        var b = V([(7, 11), (13, 19)]);

        var distance = Distance(a, b);

        using var _ = new AssertionScope();

        distance.Should().Be(Sqrt(413));
        a.Distance(b).Should().Be(Distance(a, b));
    }

    [Fact]
    public void Normalization_changes_norm_to_one_but_preserves_the_ratio_of_the_components()
    {
        var ket = V([(1, 2), (3, 5)]);

        var normalized = Normalized(ket);

        using var _ = new AssertionScope();

        normalized.Should().BeEquivalentTo(1 / Sqrt(ket * ket) * ket);
        ket.Normalized().Should().BeEquivalentTo(Normalized(ket));
        normalized.Norm().Round().Should().Be(R(1));
    }
}