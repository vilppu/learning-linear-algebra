using System.Numerics;
using LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace;
using LearningLinearAlgebra.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using LearningLinearAlgebra.Matrices.Complex;

namespace LearningLinearAlgebra.Tests.LinearAlgebra;

public class SinglePrecisionCpuBraTests : BraTests<Operator<float>, Bra<float>, Ket<float>, float> { }
public class DoublePrecisionCpuBraTests : BraTests<Operator<double>, Bra<double>, Ket<double>, double> { }

public abstract class BraTests<TOperator, TBra, TKet, TRealNumber>
    where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
    where TBra : IBra<TBra, TKet, TRealNumber>
    where TKet : IKet<TKet, TBra, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    [Fact]
    public void Dimension_of_the_vector_is_the_number_of_elements_in_basis_vector()
    {
        var bra = Bra<float>.U([(1, 2), (3, 5), (7, 9)]);

        var dimension = Bra<float>.Dimension(bra);

        using var _ = new AssertionScope();

        dimension.Should().Be(3);
        bra.Dimension().Should().Be(Bra<float>.Dimension(bra));
    }

    [Fact]
    public void Addition_is_calculated_as_addition_of_components()
    {
        var a = TBra.U([(1, 2), (3, 5)]);
        var b = TBra.U([(7, 11), (13, 17)]);

        var sum = TBra.Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(TBra.U([(8, 13), (16, 22)]));
        (a + b).Should().BeEquivalentTo(TBra.Add(a, b));
        a.Add(b).Should().BeEquivalentTo(TBra.Add(a, b));
    }

    [Fact]
    public void Subtraction_is_calculated_as_subtraction_of_components()
    {
        var a = TBra.U([(1, 2), (3, 5)]);
        var b = TBra.U([(7, 11), (13, 17)]);

        var difference = TBra.Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(TBra.U([(-6, -9), (-10, -12)]));
        (a - b).Should().BeEquivalentTo(TBra.Subtract(a, b));
        a.Subtract(b).Should().BeEquivalentTo(TBra.Subtract(a, b));
    }

    [Fact]
    public void Scalar_multiplication_is_calculated_as_scalar_multiplication_of_components()
    {
        var scalar = ComplexNumber<TRealNumber>.C(6, 7);
        var bra = TBra.U([(1, 2), (3, 5)]);

        var product = TBra.Multiply(scalar, bra);

        using var _ = new AssertionScope();

        product.Should().BeEquivalentTo(TBra.U([(-8, 19), (-17, 51)]));
        (scalar * bra).Should().BeEquivalentTo(TBra.Multiply(scalar, bra));
        scalar.Multiply(bra).Should().BeEquivalentTo(TBra.Multiply(scalar, bra));
    }

    [Fact]
    public void Additive_inverse_is_calculated_as_additive_inverse_of_components()
    {
        var bra = TBra.U([(1, 2), (3, 5)]);

        var additiveInverse = TBra.AdditiveInverse(bra);

        using var _ = new AssertionScope();

        additiveInverse.Should().BeEquivalentTo(TBra.U([(-1, -2), (-3, -5)]));
        (-bra).Should().BeEquivalentTo(TBra.AdditiveInverse(bra));
        bra.AdditiveInverse().Should().BeEquivalentTo(TBra.AdditiveInverse(bra));
    }

    [Fact]
    public void Inner_product_is_a_sum_of_products_of_left_vector_components_and_conjucates_of_right_vector_components()
    {
        var a = TBra.U([(1, 2), (3, 5)]);
        var b = TBra.U([(7, 11), (13, 19)]);

        var innerProduct = TBra.InnerProduct(a, b);

        using var _ = new AssertionScope();

        innerProduct.Should().BeEquivalentTo(ComplexNumber<TRealNumber>.C(163, 11));
        (a * b).Should().BeEquivalentTo(TBra.InnerProduct(a, b));
    }

    [Fact]
    public void Tensor_product_of_vectors_contains_combinations_of_products_of_all_elements_of_both_vectors()
    {
        var a = TBra.U([(1, 2), (3, 5)]);
        var b = TBra.U([(7, 11), (13, 19)]);

        var tensorProduct = TBra.TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(TBra.U([(-15, 25), (-25, 45), (-34, 68), (-56, 122)]));
        a.TensorProduct(b).Should().BeEquivalentTo(TBra.TensorProduct(a, b));
    }

    [Fact]
    public void Conjucate_of_a_vector_is_where_each_element_is_a_complex_conjucate_of_the_original_vector()
    {
        var bra = TBra.U([(1, 2), (3, 5)]);

        var conjucate = TBra.Conjucate(bra);

        using var _ = new AssertionScope();

        conjucate.Should().BeEquivalentTo(TBra.U([(1, -2), (3, -5)]));
        bra.Conjucate().Should().BeEquivalentTo(conjucate);
    }

    [Fact]
    public void Ket_is_a_complex_adjoint_of_bra()
    {
        var bra = TBra.U([(1, 2), (3, 5)]);

        var ket = TBra.Ket(bra);

        using var _ = new AssertionScope();

        ket.Should().BeEquivalentTo(TBra.U([(1, -2), (3, -5)]));
        bra.Ket().Should().BeEquivalentTo(TBra.Ket(bra));
    }

    [Fact]
    public void Norm_that_is_the_length_of_the_vector_is_calculated_as_square_root_of_the_inner_product_of_vector_with_itself()
    {
        var bra = TBra.U([(4, 3), (6, -4), (12, -7), (0, 13)]);

        var norm = TBra.Norm(bra);

        using var _ = new AssertionScope();

        norm.Should().Be(RealNumber<TRealNumber>.Sqrt(439));
        bra.Norm().Should().Be(TBra.Norm(bra));
    }

    [Fact]
    public void Distance_of_the_two_vectors_is_the_norm_of_the_difference()
    {
        var a = TBra.U([(1, 2), (3, 5)]);
        var b = TBra.U([(7, 11), (13, 19)]);

        var distance = TBra.Distance(a, b);

        using var _ = new AssertionScope();

        distance.Should().Be(RealNumber<TRealNumber>.Sqrt(413));
        a.Distance(b).Should().Be(TBra.Distance(a, b));
    }

    [Fact]
    public void Normalization_changes_norm_to_one_but_preserves_the_ratio_of_the_components()
    {
        var bra = TBra.U([(1, 2), (3, 5)]);

        var normalized = TBra.Normalized(bra);

        using var _ = new AssertionScope();

        normalized.Should().BeEquivalentTo(TRealNumber.One / ComplexNumber<TRealNumber>.Sqrt(bra * bra) * bra);
        bra.Normalized().Should().BeEquivalentTo(TBra.Normalized(bra));
        normalized.Norm().Round().Should().Be(RealNumber<TRealNumber>.R(1));
    }
}