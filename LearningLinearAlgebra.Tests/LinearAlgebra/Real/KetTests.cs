using System.Numerics;
using Computation.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;
using LearningLinearAlgebra.RealVectorSpace;
using LearningLinearAlgebra.Tests.Helpers;
using Xunit;

namespace LearningLinearAlgebra.Tests.LinearAlgebra.Real;

public class SinglePrecisionKetTests : KetTests<float>;
public class DoublePrecisionKetTests : KetTests<double>;

public abstract class KetTests<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    static KetTests() => Formatters<TRealNumber>.Register();

    [Fact]
    public void Dimension_of_the_ket_is_the_number_of_elements_in_basis_ket()
    {
        var ket = Ket<TRealNumber>.V([1.0, 3.0, 7.0]);

        var dimension = Ket<TRealNumber>.Dimension(ket);

        using var _ = new AssertionScope();

        dimension.Should().Be(3);
        ket.Dimension().Should().Be(Ket<TRealNumber>.Dimension(ket));
    }

    [Fact]
    public void Addition_is_calculated_as_addition_of_components()
    {
        var a = Ket<TRealNumber>.V([1.0, 3.0]);
        var b = Ket<TRealNumber>.V([7.0, 13.0]);

        var sum = Ket<TRealNumber>.Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().Be(Ket<TRealNumber>.V([8.0, 16.0]));
        (a + b).Should().Be(Ket<TRealNumber>.Add(a, b));
        a.Add(b).Should().Be(Ket<TRealNumber>.Add(a, b));
    }

    [Fact]
    public void Sum_of_two_kets_is_calculated_as_sum_of_the_components()
    {
        var a = Ket<TRealNumber>.V([1.0, 3.0]);
        var b = Ket<TRealNumber>.V([7.0, 13.0]);

        var sum = a.Add(b);

        using var _ = new AssertionScope();

        sum.Should().Be(Ket<TRealNumber>.V([8.0, 16.0]));
        (a + b).Should().Be(a.Add(b));
    }

    [Fact]
    public void Sum_of_complex_kets_is_commutative()
    {
        var a = Ket<TRealNumber>.V([1.0, 3.0]);
        var b = Ket<TRealNumber>.V([7.0, 13.0]);

        (b + a).Should().Be(a + b);
    }

    [Fact]
    public void Sum_of_complex_kets_is_associative()
    {
        var a = Ket<TRealNumber>.V([1.0, 3.0]);
        var b = Ket<TRealNumber>.V([7.0, 13.0]);
        var c = Ket<TRealNumber>.V([23.0, 31.0]);

        (a + (b + c)).Should().Be(a + b + c);
    }

    [Fact]
    public void Sum_of_ket_and_its_the_inverse_is_zero()
    {
        var ket = Ket<TRealNumber>.V([1.0, 3.0]);

        var zero = Ket<TRealNumber>.Zero(2);

        (ket + -ket).Should().Be(zero);
    }

    [Fact]
    public void Subtraction_is_calculated_as_subtraction_of_components()
    {
        var a = Ket<TRealNumber>.V([1.0, 3.0]);
        var b = Ket<TRealNumber>.V([7.0, 13.0]);

        var difference = Ket<TRealNumber>.Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().Be(Ket<TRealNumber>.V([-6.0, -10.0]));
        (a - b).Should().Be(Ket<TRealNumber>.Subtract(a, b));
        a.Subtract(b).Should().Be(Ket<TRealNumber>.Subtract(a, b));
    }

    [Fact]
    public void Difference_of_two_kets_is_calculated_as_difference_of_the_components()
    {
        var a = Ket<TRealNumber>.V([1.0, 3.0]);

        var b = Ket<TRealNumber>.V([7.0, 13.0]);

        var difference = a.Subtract(b);

        using var _ = new AssertionScope();

        difference.Should().Be(Ket<TRealNumber>.V([-6.0, -10.0]));
        (a - b).Should().Be(a.Subtract(b));
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var ket = Ket<TRealNumber>.V([1.0, 3.0]);

        var zero = Ket<TRealNumber>.Zero(2);

        using var _ = new AssertionScope();

        (ket + zero).Should().Be(ket);
        (zero + ket).Should().Be(ket);
    }

    [Fact]
    public void When_multiplying_a_ket_by_scalar_then_each_element_of_the_ket_is_multiplied_by_the_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5.0);
        var ket = Ket<TRealNumber>.V([11.0, 19.0]);

        var multiplied = ket.Multiply(scalar);

        using var _ = new AssertionScope();

        multiplied.Should().Be(Ket<TRealNumber>.V([55.0, 95.0]));
        (scalar * ket).Should().Be(ket.Multiply(scalar));
    }

    [Fact]
    public void Complex_ket_can_by_multiplied_by_a_real_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5.0);
        var ket = Ket<TRealNumber>.V([1.0, 2.0]);

        var multiplied = ket.Multiply(scalar);

        using var _ = new AssertionScope();

        multiplied.Should().Be(Ket<TRealNumber>.V([5.0, 10.0]));
        (scalar * ket).Should().Be(ket.Multiply(scalar));
        ket.Multiply(scalar).Should().Be(ket.Multiply(scalar));
    }

    [Fact]
    public void Scalar_multiplication_is_calculated_as_scalar_multiplication_of_components()
    {
        var scalar = RealNumber<TRealNumber>.R(6.0);
        var ket = Ket<TRealNumber>.V([1.0, 3.0]);

        var product = Ket<TRealNumber>.Multiply(scalar, ket);

        using var _ = new AssertionScope();

        product.Should().Be(Ket<TRealNumber>.V([6.0, 18.0]));
        (scalar * ket).Should().Be(Ket<TRealNumber>.Multiply(scalar, ket));
        scalar.Multiply(ket).Should().Be(Ket<TRealNumber>.Multiply(scalar, ket));
    }

    [Fact]
    public void Scalar_multiplication_respects_complex_multiplication()
    {
        var scalarA = RealNumber<TRealNumber>.R(3.0);
        var scalarB = RealNumber<TRealNumber>.R(7.0);

        var ket = Ket<TRealNumber>.V([23.0, 31.0]);

        (scalarA * scalarB * ket).Should().Be(scalarA * (scalarB * ket));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = RealNumber<TRealNumber>.R(3.0);
        var ketA = Ket<TRealNumber>.V([7.0, 13.0]);

        var ketB = Ket<TRealNumber>.V([23.0, 31.0]);

        (scalar * ketA + scalar * ketB).Should().Be(scalar * (ketA + ketB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_complex_addition()
    {
        var scalarA = RealNumber<TRealNumber>.R(3.0);
        var scalarB = RealNumber<TRealNumber>.R(7.0);

        var ket = Ket<TRealNumber>.V([23.0, 31.0]);

        (scalarA * ket + scalarB * ket).Should().Be((scalarA + scalarB) * ket);
    }

    [Fact]
    public void Additive_inverse_is_calculated_as_additive_inverse_of_components()
    {
        var ket = Ket<TRealNumber>.V([1.0, 3.0]);

        var additiveInverse = Ket<TRealNumber>.AdditiveInverse(ket);

        using var _ = new AssertionScope();

        additiveInverse.Should().Be(Ket<TRealNumber>.V([-1.0, -3.0]));
        (-ket).Should().Be(Ket<TRealNumber>.AdditiveInverse(ket));
        ket.AdditiveInverse().Should().Be(Ket<TRealNumber>.AdditiveInverse(ket));
    }

    [Fact]
    public void Transpose_of_a_ket_is_bra_with_same_entries_of_the_original_ket()
    {
        var ket = Ket<TRealNumber>.V([1.0, 3.0]);

        var transpose = ket.Transpose();

        transpose.Should().Be(Bra<TRealNumber>.U([1.0, 3.0]));
    }

    [Fact]
    public void Bra_can_be_multiplied_by_ket_applying_the_rules_of_matrix_multiplication()
    {
        var a = Bra<TRealNumber>.U([1.0, 3.0]);
        var b = Ket<TRealNumber>.V([7.0, 13.0]);

        var product = Ket<TRealNumber>.Multiply(a, b);

        using var _ = new AssertionScope();

        product.Should().Be(RealNumber<TRealNumber>.R(46));
        a.Multiply(b).Should().Be(Ket<TRealNumber>.Multiply(a, b));
        (a * b).Should().Be(Ket<TRealNumber>.Multiply(a, b));
    }

    [Fact]
    public void Product_of_ket_and_bra_is_sum_of_products_of_ket_components()
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
    public void Inner_product_is_a_sum_of_products_of_left_ket_components_and_conjucates_of_right_ket_components()
    {
        var a = Ket<TRealNumber>.V([1.0, 3.0]);
        var b = Ket<TRealNumber>.V([7.0, 13.0]);

        var innerProduct = Ket<TRealNumber>.InnerProduct(a, b);

        using var _ = new AssertionScope();

        innerProduct.Should().Be(RealNumber<TRealNumber>.R(46));
        (a * b).Should().Be(Ket<TRealNumber>.InnerProduct(a, b));
    }

    [Fact]
    public void Inner_product_respects_addition()
    {
        var a = Ket<TRealNumber>.V([1.0, 3.0]);

        var b = Ket<TRealNumber>.V([7.0, 13.0]);

        var c = Ket<TRealNumber>.V([23.0, 31.0]);

        (a * c + b * c).Should().Be((a + b) * c);
    }

    [Fact]
    public void Inner_product_respects_scalar_multiplication()
    {
        var a = Ket<TRealNumber>.V([1.0, 3.0]);

        var b = Ket<TRealNumber>.V([7.0, 13.0]);

        var scalar = RealNumber<TRealNumber>.R(23.0);

        (scalar * (a * b)).Should().Be(scalar * a * b);
    }

    [Fact]
    public void Inner_product_of_a_complex_ket_with_itself_is_a_real_number()
    {
        var ket = Ket<TRealNumber>.V([1.0, 3.0]);

        var innerProduct = ket.InnerProduct(ket);

        innerProduct.Should().Be(RealNumber<TRealNumber>.R(10));
    }

    [Fact]
    public void Tensor_product_of_kets_contains_combinations_of_products_of_all_elements_of_both_kets()
    {
        var a = Ket<TRealNumber>.V([1.0, 3.0]);
        var b = Ket<TRealNumber>.V([7.0, 13.0]);

        var tensorProduct = Ket<TRealNumber>.TensorProduct(a, b);

        tensorProduct.Should().Be(Ket<TRealNumber>.V([7.0, 13.0, 21.0, 39.0]));
        a.TensorProduct(b).Should().Be(Ket<TRealNumber>.TensorProduct(a, b));
    }

    [Fact]
    public void More_examples_of_tensor_product()
    {
        Ket<TRealNumber>.TensorProduct(Ket<TRealNumber>.V([1.0, 0.0]), Ket<TRealNumber>.V([1.0, 0.0])).Should().Be(Ket<TRealNumber>.V([1.0, 0.0, 0.0, 0.0]));
        Ket<TRealNumber>.TensorProduct(Ket<TRealNumber>.V([1.0, 0.0]), Ket<TRealNumber>.V([0.0, 1.0])).Should().Be(Ket<TRealNumber>.V([0.0, 1.0, 0.0, 0.0]));
        Ket<TRealNumber>.TensorProduct(Ket<TRealNumber>.V([0.0, 1.0]), Ket<TRealNumber>.V([1.0, 0.0])).Should().Be(Ket<TRealNumber>.V([0.0, 0.0, 1.0, 0.0]));
        Ket<TRealNumber>.TensorProduct(Ket<TRealNumber>.V([0.0, 1.0]), Ket<TRealNumber>.V([0.0, 1.0])).Should().Be(Ket<TRealNumber>.V([0.0, 0.0, 0.0, 1.0]));
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
    public void Norm_that_is_the_length_of_the_ket_is_calculated_as_square_root_of_the_inner_product_of_ket_with_itself()
    {
        var ket = Ket<TRealNumber>.V([4.0, 6.0, 12.0, 0.0]);

        var norm = Ket<TRealNumber>.Norm(ket);

        using var _ = new AssertionScope();

        norm.Should().Be(RealNumber<TRealNumber>.Sqrt(196));
        ket.Norm().Should().Be(Ket<TRealNumber>.Norm(ket));
    }

    [Fact]
    public void Norm_is_square_root_of_inner_product_of_ket_with_itself()
    {
        var ket = Ket<TRealNumber>.V([4.0, 6.0, 12.0, 0.0]);

        var norm = ket.Norm();

        using var _ = new AssertionScope();

        norm.Should().Be(RealNumber<TRealNumber>.Sqrt(196));
    }

    [Fact]
    public void Distance_of_the_two_kets_is_the_norm_of_the_difference()
    {
        var a = Ket<TRealNumber>.V([1.0, 3.0]);
        var b = Ket<TRealNumber>.V([7.0, 13.0]);

        var distance = Ket<TRealNumber>.Distance(a, b);

        using var _ = new AssertionScope();

        distance.Should().Be(RealNumber<TRealNumber>.Sqrt(136));
        a.Distance(b).Should().Be(Ket<TRealNumber>.Distance(a, b));
    }

    [Fact]
    public void ket_can_be_normalized_to_have_length_of_one_by_dividing_it_by_its_length()
    {
        var ket = Ket<TRealNumber>.V([3.0, 2.0, -1.0]);

        var normalized = ket.Normalized();

        using var _ = new AssertionScope();

        normalized.Should().Be(RealNumber<TRealNumber>.R(1.0) / RealNumber<TRealNumber>.Sqrt(ket * ket) * ket);
    }

    [Fact]
    public void Normalization_changes_norm_to_one_but_preserves_the_ratio_of_the_components()
    {
        var ket = Ket<TRealNumber>.V([1.0, 3.0]);

        var normalized = Ket<TRealNumber>.Normalized(ket);

        using var _ = new AssertionScope();

        normalized.Should().Be(RealNumber<TRealNumber>.R(1.0) / RealNumber<TRealNumber>.Sqrt(ket * ket) * ket);
        ket.Normalized().Should().Be(Ket<TRealNumber>.Normalized(ket));
        normalized.Norm().Round().Should().Be(TRealNumber.One);
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = Ket<TRealNumber>.V([1.0, 3.0]);
        var b = Ket<TRealNumber>.V([7.0, 13.0]);
        var c = Ket<TRealNumber>.V([23.0, 31.0]);

        a.TensorProduct(b.TensorProduct(c)).Should().Be(a.TensorProduct(b).TensorProduct(c));
    }
}