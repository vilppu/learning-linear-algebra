using System.Numerics;
using Computation.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;
using LearningLinearAlgebra.RealVectorSpace;
using LearningLinearAlgebra.Tests.Helpers;
using Xunit;

namespace LearningLinearAlgebra.Tests.LinearAlgebra.Real;

public class SinglePrecisionBraTests : BraTests<float>;
public class DoublePrecisionBraTests : BraTests<double>;

public abstract class BraTests<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    static BraTests() => Formatters<TRealNumber>.Register();

    [Fact]
    public void Dimension_of_the_bra_is_the_number_of_elements_in_basis_bra()
    {
        var bra = Bra<TRealNumber>.U([1.0, 3.0, 7.0]);

        var dimension = Bra<TRealNumber>.Dimension(bra);

        using var _ = new AssertionScope();

        dimension.Should().Be(3);
        bra.Dimension().Should().Be(Bra<TRealNumber>.Dimension(bra));
    }

    [Fact]
    public void Addition_is_calculated_as_addition_of_components()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);
        var b = Bra<TRealNumber>.U([7.0, 13.0]);

        var sum = Bra<TRealNumber>.Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().Be(Bra<TRealNumber>.U([8.0, 16.0]));
        (a + b).Should().Be(Bra<TRealNumber>.Add(a, b));
        a.Add(b).Should().Be(Bra<TRealNumber>.Add(a, b));
    }

    [Fact]
    public void Sum_of_two_bras_is_calculated_as_sum_of_the_components()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);
        var b = Bra<TRealNumber>.U([7.0, 13.0]);

        var sum = a.Add(b);

        using var _ = new AssertionScope();

        sum.Should().Be(Bra<TRealNumber>.U([8.0, 16.0]));
        (a + b).Should().Be(a.Add(b));
    }

    [Fact]
    public void Sum_of_complex_bras_is_commutative()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);
        var b = Bra<TRealNumber>.U([7.0, 13.0]);

        (b + a).Should().Be(a + b);
    }

    [Fact]
    public void Sum_of_complex_bras_is_associative()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);
        var b = Bra<TRealNumber>.U([7.0, 13.0]);
        var c = Bra<TRealNumber>.U([23.0, 31.0]);

        (a + (b + c)).Should().Be(a + b + c);
    }

    [Fact]
    public void Sum_of_bra_and_its_the_inverse_is_zero()
    {
        var bra = Bra<TRealNumber>.U([1.0, 3.0]);

        var zero = Bra<TRealNumber>.Zero(2);

        (bra + -bra).Should().Be(zero);
    }

    [Fact]
    public void Subtraction_is_calculated_as_subtraction_of_components()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);
        var b = Bra<TRealNumber>.U([7.0, 13.0]);

        var difference = Bra<TRealNumber>.Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().Be(Bra<TRealNumber>.U([-6.0, -10.0]));
        (a - b).Should().Be(Bra<TRealNumber>.Subtract(a, b));
        a.Subtract(b).Should().Be(Bra<TRealNumber>.Subtract(a, b));
    }

    [Fact]
    public void Difference_of_two_bras_is_calculated_as_difference_of_the_components()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);

        var b = Bra<TRealNumber>.U([7.0, 13.0]);

        var difference = a.Subtract(b);

        using var _ = new AssertionScope();

        difference.Should().Be(Bra<TRealNumber>.U([-6.0, -10.0]));
        (a - b).Should().Be(a.Subtract(b));
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var bra = Bra<TRealNumber>.U([1.0, 3.0]);

        var zero = Bra<TRealNumber>.Zero(2);

        using var _ = new AssertionScope();

        (bra + zero).Should().Be(bra);
        (zero + bra).Should().Be(bra);
    }

    [Fact]
    public void When_multiplying_a_bra_by_scalar_then_each_element_of_the_bra_is_multiplied_by_the_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5.0);
        var bra = Bra<TRealNumber>.U([11.0, 19.0]);

        var multiplied = bra.Multiply(scalar);

        using var _ = new AssertionScope();

        multiplied.Should().Be(Bra<TRealNumber>.U([55.0, 95.0]));
        (scalar * bra).Should().Be(bra.Multiply(scalar));
    }

    [Fact]
    public void Complex_bra_can_by_multiplied_by_a_real_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5.0);
        var bra = Bra<TRealNumber>.U([1.0, 2.0]);

        var multiplied = bra.Multiply(scalar);

        using var _ = new AssertionScope();

        multiplied.Should().Be(Bra<TRealNumber>.U([5.0, 10.0]));
        (scalar * bra).Should().Be(bra.Multiply(scalar));
        bra.Multiply(scalar).Should().Be(bra.Multiply(scalar));
    }

    [Fact]
    public void Scalar_multiplication_is_calculated_as_scalar_multiplication_of_components()
    {
        var scalar = RealNumber<TRealNumber>.R(6.0);
        var bra = Bra<TRealNumber>.U([1.0, 3.0]);

        var product = Bra<TRealNumber>.Multiply(scalar, bra);

        using var _ = new AssertionScope();

        product.Should().Be(Bra<TRealNumber>.U([6.0, 18.0]));
        (scalar * bra).Should().Be(Bra<TRealNumber>.Multiply(scalar, bra));
        scalar.Multiply(bra).Should().Be(Bra<TRealNumber>.Multiply(scalar, bra));
    }

    [Fact]
    public void Scalar_multiplication_respects_complex_multiplication()
    {
        var scalarA = RealNumber<TRealNumber>.R(3.0);
        var scalarB = RealNumber<TRealNumber>.R(7.0);

        var bra = Bra<TRealNumber>.U([23.0, 31.0]);

        (scalarA * scalarB * bra).Should().Be(scalarA * (scalarB * bra));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = RealNumber<TRealNumber>.R(3.0);
        var braA = Bra<TRealNumber>.U([7.0, 13.0]);

        var braB = Bra<TRealNumber>.U([23.0, 31.0]);

        (scalar * braA + scalar * braB).Should().Be(scalar * (braA + braB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_complex_addition()
    {
        var scalarA = RealNumber<TRealNumber>.R(3.0);
        var scalarB = RealNumber<TRealNumber>.R(7.0);

        var bra = Bra<TRealNumber>.U([23.0, 31.0]);

        (scalarA * bra + scalarB * bra).Should().Be((scalarA + scalarB) * bra);
    }

    [Fact]
    public void Additive_inverse_is_calculated_as_additive_inverse_of_components()
    {
        var bra = Bra<TRealNumber>.U([1.0, 3.0]);

        var additiveInverse = Bra<TRealNumber>.AdditiveInverse(bra);

        using var _ = new AssertionScope();

        additiveInverse.Should().Be(Bra<TRealNumber>.U([-1.0, -3.0]));
        (-bra).Should().Be(Bra<TRealNumber>.AdditiveInverse(bra));
        bra.AdditiveInverse().Should().Be(Bra<TRealNumber>.AdditiveInverse(bra));
    }

    [Fact]
    public void Transpose_of_a_bra_is_ket_with_same_entries_of_the_original_bra()
    {
        var bra = Bra<TRealNumber>.U([1.0, 3.0]);

        var transpose = bra.Transpose();

        transpose.Should().Be(Ket<TRealNumber>.V([1.0, 3.0]));
    }

    [Fact]
    public void Product_of_bra_and_ket_is_sum_of_products_of_bra_components()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);
        var b = Ket<TRealNumber>.V([7.0, 13.0]);

        var product = a.Multiply(b);

        using var _ = new AssertionScope();

        product.Should().Be(RealNumber<TRealNumber>.R(46));
        a.Multiply(b).Should().Be(a.Multiply(b));
        (a * b).Should().Be(a.Multiply(b));
    }

    [Fact]
    public void Inner_product_is_a_sum_of_products_of_left_bra_components_and_conjucates_of_right_bra_components()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);
        var b = Bra<TRealNumber>.U([7.0, 13.0]);

        var innerProduct = Bra<TRealNumber>.InnerProduct(a, b);

        using var _ = new AssertionScope();

        innerProduct.Should().Be(RealNumber<TRealNumber>.R(46));
        (a * b).Should().Be(Bra<TRealNumber>.InnerProduct(a, b));
    }

    [Fact]
    public void Inner_product_respects_addition()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);
        var b = Bra<TRealNumber>.U([7.0, 13.0]);
        var c = Bra<TRealNumber>.U([23.0, 31.0]);

        (a * c + b * c).Should().Be((a + b) * c);
    }

    [Fact]
    public void Inner_product_respects_scalar_multiplication()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);
        var b = Bra<TRealNumber>.U([7.0, 13.0]);

        var scalar = RealNumber<TRealNumber>.R(23.0);

        (scalar * (a * b)).Should().Be(scalar * a * b);
    }

    [Fact]
    public void Inner_product_of_a_complex_bra_with_itself_is_a_real_number()
    {
        var bra = Bra<TRealNumber>.U([1.0, 3.0]);

        var innerProduct = bra.InnerProduct(bra);

        innerProduct.Should().Be(RealNumber<TRealNumber>.R(10));
    }

    [Fact]
    public void Tensor_product_of_bras_contains_combinations_of_products_of_all_elements_of_both_bras()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);
        var b = Bra<TRealNumber>.U([7.0, 13.0]);

        var tensorProduct = Bra<TRealNumber>.TensorProduct(a, b);

        tensorProduct.Should().Be(Bra<TRealNumber>.U([7.0, 13.0, 21.0, 39.0]));
        a.TensorProduct(b).Should().Be(Bra<TRealNumber>.TensorProduct(a, b));
    }

    [Fact]
    public void More_examples_of_tensor_product()
    {
        Bra<TRealNumber>.TensorProduct(Bra<TRealNumber>.U([1.0, 0.0]), Bra<TRealNumber>.U([1.0, 0.0])).Should().Be(Bra<TRealNumber>.U([1.0, 0.0, 0.0, 0.0]));
        Bra<TRealNumber>.TensorProduct(Bra<TRealNumber>.U([1.0, 0.0]), Bra<TRealNumber>.U([0.0, 1.0])).Should().Be(Bra<TRealNumber>.U([0.0, 1.0, 0.0, 0.0]));
        Bra<TRealNumber>.TensorProduct(Bra<TRealNumber>.U([0.0, 1.0]), Bra<TRealNumber>.U([1.0, 0.0])).Should().Be(Bra<TRealNumber>.U([0.0, 0.0, 1.0, 0.0]));
        Bra<TRealNumber>.TensorProduct(Bra<TRealNumber>.U([0.0, 1.0]), Bra<TRealNumber>.U([0.0, 1.0])).Should().Be(Bra<TRealNumber>.U([0.0, 0.0, 0.0, 1.0]));
    }

    [Fact]
    public void Bra_is_a_complex_adjoint_of_ket()
    {
        var a = Ket<TRealNumber>.V([1.0, 3.0]);

        var bra = Ket<TRealNumber>.Bra(a);

        using var _ = new AssertionScope();

        bra.Should().Be(Bra<TRealNumber>.U([1.0, 3.0]));
        a.Bra().Should().Be(bra);
    }

    [Fact]
    public void Norm_that_is_the_length_of_the_bra_is_calculated_as_square_root_of_the_inner_product_of_bra_with_itself()
    {
        var bra = Bra<TRealNumber>.U([4.0, 6.0, 12.0, 0.0]);

        var norm = Bra<TRealNumber>.Norm(bra);

        using var _ = new AssertionScope();

        norm.Should().Be(RealNumber<TRealNumber>.Sqrt(196));
        bra.Norm().Should().Be(Bra<TRealNumber>.Norm(bra));
    }

    [Fact]
    public void Norm_is_square_root_of_inner_product_of_bra_with_itself()
    {
        var bra = Bra<TRealNumber>.U([4.0, 6.0, 12.0, 0.0]);

        var norm = bra.Norm();

        using var _ = new AssertionScope();

        norm.Should().Be(RealNumber<TRealNumber>.Sqrt(196));
    }

    [Fact]
    public void Distance_of_the_two_bras_is_the_norm_of_the_difference()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);
        var b = Bra<TRealNumber>.U([7.0, 13.0]);

        var distance = Bra<TRealNumber>.Distance(a, b);

        using var _ = new AssertionScope();

        distance.Should().Be(RealNumber<TRealNumber>.Sqrt(136));
        a.Distance(b).Should().Be(Bra<TRealNumber>.Distance(a, b));
    }

    [Fact]
    public void bra_can_be_normalized_to_have_length_of_one_by_dividing_it_by_its_length()
    {
        var bra = Bra<TRealNumber>.U([3.0, 2.0, -1.0]);

        var normalized = bra.Normalized();

        using var _ = new AssertionScope();

        normalized.Should().Be(RealNumber<TRealNumber>.R(1.0) / RealNumber<TRealNumber>.Sqrt(bra * bra) * bra);
    }

    [Fact]
    public void Normalization_changes_norm_to_one_but_preserves_the_ratio_of_the_components()
    {
        var bra = Bra<TRealNumber>.U([1.0, 3.0]);

        var normalized = Bra<TRealNumber>.Normalized(bra);

        using var _ = new AssertionScope();

        normalized.Should().Be(RealNumber<TRealNumber>.R(1.0) / RealNumber<TRealNumber>.Sqrt(bra * bra) * bra);
        bra.Normalized().Should().Be(Bra<TRealNumber>.Normalized(bra));
        normalized.Norm().Round().Should().Be(TRealNumber.One);
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);
        var b = Bra<TRealNumber>.U([7.0, 13.0]);
        var c = Bra<TRealNumber>.U([23.0, 31.0]);

        a.TensorProduct(b.TensorProduct(c)).Should().Be(a.TensorProduct(b).TensorProduct(c));
    }
}