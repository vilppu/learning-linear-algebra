﻿using System.Numerics;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using LearningLinearAlgebra.ComplexVectorSpace;
using LearningLinearAlgebra.Numbers;
using LearningLinearAlgebra.Tests.Helpers;

namespace LearningLinearAlgebra.Tests.LinearAlgebra.Complex;

public class SinglePrecisionBraTests : BraTests<float>;
public class DoublePrecisionBraTests : BraTests<double>;

public abstract class BraTests<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    static BraTests() => Formatters<TRealNumber>.Register();

    [Fact]
    public void Dimension_of_the_bra_is_the_number_of_elements_in_basis_bra()
    {
        var bra = Bra.U<TRealNumber>([(1, 2), (3, 5), (7, 9)]);

        var dimension = bra.Dimension();

        using var _ = new AssertionScope();

        dimension.Should().Be(3);
        bra.Dimension().Should().Be(bra.Dimension());
    }

    [Fact]
    public void Addition_is_calculated_as_addition_of_components()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);
        var b = Bra.U<TRealNumber>([(7, 11), (13, 17)]);

        var sum = a.Add(b);

        using var _ = new AssertionScope();

        sum.Should().Be(Bra.U<TRealNumber>([(8, 13), (16, 22)]));
        (a + b).Should().Be(a.Add(b));
    }

    [Fact]
    public void Sum_of_two_bras_is_calculated_as_sum_of_the_components()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);
        var b = Bra.U<TRealNumber>([(7, 11), (13, 19)]);

        var sum = a.Add(b);

        using var _ = new AssertionScope();

        sum.Should().Be(Bra.U<TRealNumber>([(8, 13), (16, 24)]));
        (a + b).Should().Be(a.Add(b));
    }

    [Fact]
    public void Sum_of_complex_bras_is_commutative()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);
        var b = Bra.U<TRealNumber>([(7, 11), (13, 19)]);

        (b + a).Should().Be(a + b);
    }

    [Fact]
    public void Sum_of_complex_bras_is_associative()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);
        var b = Bra.U<TRealNumber>([(7, 11), (13, 19)]);
        var c = Bra.U<TRealNumber>([(23, 29), (31, 37)]);

        (a + (b + c)).Should().Be(a + b + c);
    }

    [Fact]
    public void Sum_of_bra_and_its_the_inverse_is_zero()
    {
        var bra = Bra.U<TRealNumber>([(1, 2), (3, 5)]);

        var zero = Bra.Zero<TRealNumber>(2);

        (bra + -bra).Should().Be(zero);
    }

    [Fact]
    public void Subtraction_is_calculated_as_subtraction_of_components()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);
        var b = Bra.U<TRealNumber>([(7, 11), (13, 17)]);

        var difference = a.Subtract(b);

        using var _ = new AssertionScope();

        difference.Should().Be(Bra.U<TRealNumber>([(-6, -9), (-10, -12)]));
        (a - b).Should().Be(a.Subtract(b));
    }

    [Fact]
    public void Difference_of_two_bras_is_calculated_as_difference_of_the_components()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);

        var b = Bra.U<TRealNumber>([(7, 11), (13, 19)]);

        var difference = a.Subtract(b);

        using var _ = new AssertionScope();

        difference.Should().Be(Bra.U<TRealNumber>([(-6, -9), (-10, -14)]));
        (a - b).Should().Be(a.Subtract(b));
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var bra = Bra.U<TRealNumber>([(1, 2), (3, 5)]);

        var zero = Bra.Zero<TRealNumber>(2);

        using var _ = new AssertionScope();

        (bra + zero).Should().Be(bra);
        (zero + bra).Should().Be(bra);
    }

    [Fact]
    public void When_multiplying_a_bra_by_scalar_then_each_element_of_the_bra_is_multiplied_by_the_scalar()
    {
        var scalar = ComplexNumber<TRealNumber>.C(5, 7);
        var bra = Bra.U<TRealNumber>([(11, 13), (19, 21)]);

        var multiplied = bra.Multiply(scalar);

        using var _ = new AssertionScope();

        multiplied.Should().Be(Bra.U<TRealNumber>([(-36, 142), (-52, 238)]));
        (scalar * bra).Should().Be(bra.Multiply(scalar));
    }

    [Fact]
    public void Complex_bra_can_by_multiplied_by_a_real_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5);
        var bra = Bra.U<TRealNumber>([(1, 0), (2, 0)]);

        var multiplied = bra.Multiply(scalar);

        using var _ = new AssertionScope();

        multiplied.Should().Be(Bra.U<TRealNumber>([(5, 0), (10, 0)]));
        (scalar * bra).Should().Be(bra.Multiply(scalar));
    }

    [Fact]
    public void Scalar_multiplication_is_calculated_as_scalar_multiplication_of_components()
    {
        var scalar = ComplexNumber<TRealNumber>.C(6, 7);
        var bra = Bra.U<TRealNumber>([(1, 2), (3, 5)]);

        var product = scalar.Multiply(bra);

        using var _ = new AssertionScope();

        product.Should().Be(Bra.U<TRealNumber>([(-8, 19), (-17, 51)]));
        (scalar * bra).Should().Be(scalar.Multiply(bra));
    }

    [Fact]
    public void Scalar_multiplication_respects_complex_multiplication()
    {
        var scalarA = ComplexNumber<TRealNumber>.C(3, 5);
        var scalarB = ComplexNumber<TRealNumber>.C(7, 11);

        var bra = Bra.U<TRealNumber>([(23, 29), (31, 37)]);

        (scalarA * scalarB * bra).Should().Be(scalarA * (scalarB * bra));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = ComplexNumber<TRealNumber>.C(3, 5);
        var braA = Bra.U<TRealNumber>([(7, 11), (13, 19)]);

        var braB = Bra.U<TRealNumber>([(23, 29), (31, 37)]);

        (scalar * braA + scalar * braB).Should().Be(scalar * (braA + braB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_complex_addition()
    {
        var scalarA = ComplexNumber<TRealNumber>.C(3, 5);
        var scalarB = ComplexNumber<TRealNumber>.C(7, 11);

        var bra = Bra.U<TRealNumber>([(23, 29), (31, 37)]);

        (scalarA * bra + scalarB * bra).Should().Be((scalarA + scalarB) * bra);
    }

    [Fact]
    public void Additive_inverse_is_calculated_as_additive_inverse_of_components()
    {
        var bra = Bra.U<TRealNumber>([(1, 2), (3, 5)]);

        var additiveInverse = bra.AdditiveInverse();

        using var _ = new AssertionScope();

        additiveInverse.Should().Be(Bra.U<TRealNumber>([(-1, -2), (-3, -5)]));
        (-bra).Should().Be(bra.AdditiveInverse());
    }

    [Fact]
    public void Conjucate_of_a_bra_is_where_each_element_is_a_complex_conjucate_of_the_original_bra()
    {
        var bra = Bra.U<TRealNumber>([(1, 2), (3, 5)]);

        var conjucate = bra.Conjucate();

        conjucate.Should().Be(Bra.U<TRealNumber>([(1, -2), (3, -5)]));
    }

    [Fact]
    public void Transpose_of_a_bra_is_ket_with_same_entries_of_the_original_bra()
    {
        var bra = Bra.U<TRealNumber>([(1, 2), (3, 5)]);

        var transpose = bra.Transpose();

        transpose.Should().Be(Ket.V<TRealNumber>([(1, 2), (3, 5)]));
    }

    [Fact]
    public void Adjoint_of_a_bra_is_ket_where_each_entry_is_a_complex_conjucate_of_the_original_bra()
    {
        var bra = Bra.U<TRealNumber>([(1, 2), (3, 5)]);

        var adjoint = bra.Adjoint();

        adjoint.Should().Be(Ket.V<TRealNumber>([(1, -2), (3, -5)]));
    }

    [Fact]
    public void Bra_can_be_multiplied_by_ket_applying_the_rules_of_matrix_multiplication()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);
        var b = Ket.V<TRealNumber>([(7, 11), (13, 19)]);

        var product = a.Multiply(b);

        using var _ = new AssertionScope();

        product.Should().Be(ComplexNumber<TRealNumber>.C(-71, 147));
        (a * b).Should().Be(a.Multiply(b));
    }

    [Fact]
    public void Product_of_bra_and_ket_is_sum_of_products_of_bra_components()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);
        var b = Ket.V<TRealNumber>([(7, 11), (13, 19)]);

        var product = a.Multiply(b);

        using var _ = new AssertionScope();

        product.Should().Be(ComplexNumber<TRealNumber>.C(-71, 147));
        (a * b).Should().Be(a.Multiply(b));
    }

    [Fact]
    public void Inner_product_is_a_sum_of_products_of_left_bra_components_and_conjucates_of_right_bra_components()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);
        var b = Bra.U<TRealNumber>([(7, 11), (13, 19)]);

        var innerProduct = a.InnerProduct(b);

        using var _ = new AssertionScope();

        innerProduct.Should().Be(ComplexNumber<TRealNumber>.C(163, 11));
        (a * b).Should().Be(a.InnerProduct(b));
    }

    [Fact]
    public void Inner_product_respects_addition()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);
        var b = Bra.U<TRealNumber>([(7, 11), (13, 19)]);
        var c = Bra.U<TRealNumber>([(23, 29), (31, 37)]);

        (a * c + b * c).Should().Be((a + b) * c);
    }

    [Fact]
    public void Inner_product_respects_scalar_multiplication()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);
        var b = Bra.U<TRealNumber>([(7, 11), (13, 19)]);

        var scalar = (23, 29);

        (scalar * (a * b)).Should().Be(scalar * a * b);
    }

    [Fact]
    public void Inner_product_of_a_complex_bra_with_itself_is_a_real_number()
    {
        var bra = Bra.U<TRealNumber>([(1, 2), (3, 5)]);

        var innerProduct = bra.InnerProduct(bra);

        innerProduct.Should().Be(ComplexNumber<TRealNumber>.C(39, 0));
    }

    [Fact]
    public void Tensor_product_of_bras_contains_combinations_of_products_of_all_elements_of_both_bras()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);
        var b = Bra.U<TRealNumber>([(7, 11), (13, 19)]);

        var tensorProduct = a.TensorProduct(b);

        tensorProduct.Should().Be(Bra.U<TRealNumber>([(-15, 25), (-25, 45), (-34, 68), (-56, 122)]));
        a.TensorProduct(b).Should().Be(a.TensorProduct(b));
    }

    [Fact]
    public void More_examples_of_tensor_product()
    {
        Bra.U<TRealNumber>([(1, 0), (0, 0)]).TensorProduct(Bra.U<TRealNumber>([(1, 0), (0, 0)])).Should().Be(Bra.U<TRealNumber>([(1, 0), (0, 0), (0, 0), (0, 0)]));
        Bra.U<TRealNumber>([(1, 0), (0, 0)]).TensorProduct(Bra.U<TRealNumber>([(0, 0), (1, 0)])).Should().Be(Bra.U<TRealNumber>([(0, 0), (1, 0), (0, 0), (0, 0)]));
        Bra.U<TRealNumber>([(0, 0), (1, 0)]).TensorProduct(Bra.U<TRealNumber>([(1, 0), (0, 0)])).Should().Be(Bra.U<TRealNumber>([(0, 0), (0, 0), (1, 0), (0, 0)]));
        Bra.U<TRealNumber>([(0, 0), (1, 0)]).TensorProduct(Bra.U<TRealNumber>([(0, 0), (1, 0)])).Should().Be(Bra.U<TRealNumber>([(0, 0), (0, 0), (0, 0), (1, 0)]));
    }

    [Fact]
    public void Bra_is_a_complex_adjoint_of_ket()
    {
        var a = Ket.V<TRealNumber>([(1, 2), (3, 5)]);

        var bra = a.Bra();

        using var _ = new AssertionScope();

        bra.Should().Be(Bra.U<TRealNumber>([(1, -2), (3, -5)]));
        a.Bra().Should().Be(bra);
    }

    [Fact]
    public void Norm_that_is_the_length_of_the_bra_is_calculated_as_square_root_of_the_inner_product_of_bra_with_itself()
    {
        var bra = Bra.U<TRealNumber>([(4, 3), (6, -4), (12, -7), (0, 13)]);

        var norm = bra.Norm();

        using var _ = new AssertionScope();

        norm.Should().Be(RealNumber<TRealNumber>.Sqrt(439));
    }

    [Fact]
    public void Norm_is_square_root_of_inner_product_of_bra_with_itself()
    {
        var bra = Bra.U<TRealNumber>([(4, 3), (6, -4), (12, -7), (0, 13)]);

        var norm = bra.Norm();

        using var _ = new AssertionScope();

        norm.Should().Be(RealNumber<TRealNumber>.Sqrt(439));
    }

    [Fact]
    public void Distance_of_the_two_bras_is_the_norm_of_the_difference()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);
        var b = Bra.U<TRealNumber>([(7, 11), (13, 19)]);

        var distance = a.Distance(b);

        using var _ = new AssertionScope();

        distance.Should().Be(RealNumber<TRealNumber>.Sqrt(413));
    }

    [Fact]
    public void bra_can_be_normalized_to_have_length_of_one_by_dividing_it_by_its_length()
    {
        var bra = Bra.U<TRealNumber>([(3, 1), (2, 5), (-1, 0)]);

        var normalized = bra.Normalized();

        using var _ = new AssertionScope();

        normalized.Should().Be(1 / ComplexNumber<TRealNumber>.Sqrt(bra * bra) * bra);
    }

    [Fact]
    public void Normalization_changes_norm_to_one_but_preserves_the_ratio_of_the_components()
    {
        var bra = Bra.U<TRealNumber>([(1, 2), (3, 5)]);

        var normalized = bra.Normalized();

        using var _ = new AssertionScope();

        normalized.Should().Be(1 / ComplexNumber<TRealNumber>.Sqrt(bra * bra) * bra);
        normalized.Norm().Round().Should().Be(TRealNumber.One);
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = Bra.U<TRealNumber>([(1, 2), (3, 5)]);
        var b = Bra.U<TRealNumber>([(7, 11), (13, 19)]);
        var c = Bra.U<TRealNumber>([(23, 29), (31, 37)]);

        a.TensorProduct(b.TensorProduct(c)).Should().Be(a.TensorProduct(b).TensorProduct(c));
    }
}