using System.Numerics;
using LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace;
using LearningLinearAlgebra.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace LearningLinearAlgebra.Tests.LinearAlgebra;

public class SinglePrecisionCpuKetTests : KetTests<Operator<float>, Bra<float>, Ket<float>, float> { }
public class DoublePrecisionCpuKetTests : KetTests<Operator<double>, Bra<double>, Ket<double>, double> { }

public abstract class KetTests<TOperator, TBra, TKet, TRealNumber>
    where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
    where TBra : IBra<TBra, TKet, TRealNumber>
    where TKet : IKet<TKet, TBra, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    [Fact]
    public void Dimension_of_the_vector_is_the_number_of_elements_in_basis_vector()
    {
        var ket = TKet.V([(1, 2), (3, 5), (7, 9)]);

        var dimension = TKet.Dimension(ket);

        using var _ = new AssertionScope();

        dimension.Should().Be(3);
        ket.Dimension().Should().Be(TKet.Dimension(ket));
    }

    [Fact]
    public void Addition_is_calculated_as_addition_of_components()
    {
        var a = TKet.V([(1, 2), (3, 5)]);
        var b = TKet.V([(7, 11), (13, 17)]);

        var sum = TKet.Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(TKet.V([(8, 13), (16, 22)]));
        (a + b).Should().BeEquivalentTo(TKet.Add(a, b));
        a.Add(b).Should().BeEquivalentTo(TKet.Add(a, b));
    }

    [Fact]
    public void Subtraction_is_calculated_as_subtraction_of_components()
    {
        var a = TKet.V([(1, 2), (3, 5)]);
        var b = TKet.V([(7, 11), (13, 17)]);

        var difference = TKet.Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(TKet.V([(-6, -9), (-10, -12)]));
        (a - b).Should().BeEquivalentTo(TKet.Subtract(a, b));
        a.Subtract(b).Should().BeEquivalentTo(TKet.Subtract(a, b));
    }

    [Fact]
    public void Scalar_multiplication_is_calculated_as_scalar_multiplication_of_components()
    {
        var scalar = ComplexNumber<TRealNumber>.C(6, 7);
        var ket = TKet.V([(1, 2), (3, 5)]);

        var product = TKet.Multiply(scalar, ket);

        using var _ = new AssertionScope();

        product.Should().BeEquivalentTo(TKet.V([(-8, 19), (-17, 51)]));
        (scalar * ket).Should().BeEquivalentTo(TKet.Multiply(scalar, ket));
        scalar.Multiply(ket).Should().BeEquivalentTo(TKet.Multiply(scalar, ket));
    }

    [Fact]
    public void Additive_inverse_is_calculated_as_additive_inverse_of_components()
    {
        var ket = TKet.V([(1, 2), (3, 5)]);

        var additiveInverse = TKet.AdditiveInverse(ket);

        using var _ = new AssertionScope();

        additiveInverse.Should().BeEquivalentTo(TKet.V([(-1, -2), (-3, -5)]));
        (-ket).Should().BeEquivalentTo(TKet.AdditiveInverse(ket));
        ket.AdditiveInverse().Should().BeEquivalentTo(TKet.AdditiveInverse(ket));
    }

    [Fact]
    public void Ket_can_be_multiplied_bra_by_applying_the_rules_of_matrix_multiplication()
    {
        var a = TBra.U([(1, 2), (3, 5)]);
        var b = TKet.V([(7, 11), (13, 19)]);

        var product = TKet.Multiply(a, b);

        using var _ = new AssertionScope();

        product.Should().BeEquivalentTo(ComplexNumber<TRealNumber>.C(-71, 147));
        a.Multiply(b).Should().Be(TKet.Multiply(a, b));
        (a * b).Should().BeEquivalentTo(TKet.Multiply(a, b));
    }

    [Fact]
    public void Inner_product_is_a_sum_of_products_of_left_vector_components_and_conjucates_of_right_vector_components()
    {
        var a = TKet.V([(1, 2), (3, 5)]);
        var b = TKet.V([(7, 11), (13, 19)]);

        var innerProduct = TKet.InnerProduct(a, b);

        using var _ = new AssertionScope();

        innerProduct.Should().BeEquivalentTo(ComplexNumber<TRealNumber>.C(163, 11));
        (a * b).Should().BeEquivalentTo(TKet.InnerProduct(a, b));
    }

    [Fact]
    public void Tensor_product_of_vectors_contains_combinations_of_products_of_all_elements_of_both_vectors()
    {
        var a = TKet.V([(1, 2), (3, 5)]);
        var b = TKet.V([(7, 11), (13, 19)]);

        var tensorProduct = TKet.TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(TKet.V([(-15, 25), (-25, 45), (-34, 68), (-56, 122)]));
        a.TensorProduct(b).Should().BeEquivalentTo(TKet.TensorProduct(a, b));
    }

    [Fact]
    public void Conjucate_of_a_vector_is_where_each_element_is_a_complex_conjucate_of_the_original_vector()
    {
        var ket = TKet.V([(1, 2), (3, 5)]);

        var conjucate = TKet.Conjucate(ket);

        using var _ = new AssertionScope();

        conjucate.Should().BeEquivalentTo(TKet.V([(1, -2), (3, -5)]));
        ket.Conjucate().Should().BeEquivalentTo(conjucate);
    }

    [Fact]
    public void Bra_is_a_complex_adjoint_of_ket()
    {
        var a = TKet.V([(1, 2), (3, 5)]);

        var bra = TKet.Bra(a);

        using var _ = new AssertionScope();

        bra.Should().BeEquivalentTo(TBra.U([(1, -2), (3, -5)]));
        a.Bra().Should().BeEquivalentTo(TKet.Bra(a));
    }

    [Fact]
    public void Norm_that_is_the_length_of_the_vector_is_calculated_as_square_root_of_the_inner_product_of_vector_with_itself()
    {
        var ket = TKet.V([(4, 3), (6, -4), (12, -7), (0, 13)]);

        var norm = TKet.Norm(ket);

        using var _ = new AssertionScope();

        norm.Should().Be(RealNumber<TRealNumber>.Sqrt(439));
        ket.Norm().Should().Be(TKet.Norm(ket));
    }

    [Fact]
    public void Distance_of_the_two_vectors_is_the_norm_of_the_difference()
    {

        var a = TKet.V([(1, 2), (3, 5)]);
        var b = TKet.V([(7, 11), (13, 19)]);

        var distance = TKet.Distance(a, b);

        using var _ = new AssertionScope();

        distance.Should().Be(RealNumber<TRealNumber>.Sqrt(413));
        a.Distance(b).Should().Be(TKet.Distance(a, b));
    }

    [Fact]
    public void Normalization_changes_norm_to_one_but_preserves_the_ratio_of_the_components()
    {
        var ket = TKet.V([(1, 2), (3, 5)]);

        var normalized = TKet.Normalized(ket);

        using var _ = new AssertionScope();

        normalized.Should().BeEquivalentTo(TRealNumber.One / ComplexNumber<TRealNumber>.Sqrt(ket * ket) * ket);
        ket.Normalized().Should().BeEquivalentTo(TKet.Normalized(ket));
        normalized.Norm().Round().Should().Be(RealNumber<TRealNumber>.R(1));
    }
}