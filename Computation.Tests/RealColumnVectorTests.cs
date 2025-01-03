﻿using System.Numerics;
using Computation.Matrices.Real;
using Computation.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Computation.Tests;

public class ManagedSinglePrecisionRealColumnVectorTests : RealColumnVectorTests<Managed.Real.Matrices<float>, float>;
public class ManagedDoublePrecisionRealColumnVectorTests : RealColumnVectorTests<Managed.Real.Matrices<double>, double>;
public class CudaSinglePrecisionRealColumnVectorTests : RealColumnVectorTests<Cuda.Real.Matrices<float>, float>;
public class CudaDoublePrecisionRealColumnVectorTests : RealColumnVectorTests<Cuda.Real.Matrices<double>, double>;

public abstract class RealColumnVectorTests<TMatrices, TRealNumber>
    where TMatrices : IMatrices<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    protected RealColumnVectorTests() =>
        Formatters<TRealNumber>.Register();

    [Fact]
    public void Sum_of_two_vectors_is_calculated_as_sum_of_the_components()
    {
        var a = TMatrices.V([1.0, 3.0]);
        var b = TMatrices.V([7.0, 13.0]);

        var sum = a.Add(b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(TMatrices.V([8.0, 16.0]));
        (a + b).Should().BeEquivalentTo(a.Add(b));
    }

    [Fact]
    public void Sum_of_complex_vectors_is_commutative()
    {
        var a = TMatrices.V([1.0, 3.0]);
        var b = TMatrices.V([7.0, 13.0]);

        (b + a).Should().BeEquivalentTo(a + b);
    }

    [Fact]
    public void Sum_of_complex_vectors_is_associative()
    {
        var a = TMatrices.V([1.0, 3.0]);
        var b = TMatrices.V([7.0, 13.0]);
        var c = TMatrices.V([23.0, 31.0]);

        (a + (b + c)).Should().BeEquivalentTo(a + b + c);
    }

    [Fact]
    public void Sum_of_vector_and_its_the_inverse_is_zero()
    {
        var vector = TMatrices.V([1.0, 3.0]);

        var zero = TMatrices.ZeroColumnVector(2);

        (vector + -vector).Should().BeEquivalentTo(zero);
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var vector = TMatrices.V([1.0, 3.0]);

        var zero = TMatrices.ZeroColumnVector(2);

        using var _ = new AssertionScope();

        (vector + zero).Should().BeEquivalentTo(vector);
        (zero + vector).Should().BeEquivalentTo(vector);
    }

    [Fact]
    public void Difference_of_two_vectors_is_calculated_as_difference_of_the_components()
    {
        var a = TMatrices.V([1.0, 3.0]);

        var b = TMatrices.V([7.0, 13.0]);

        var difference = a.Subtract(b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(TMatrices.V([-6.0, -10.0]));
        (a - b).Should().BeEquivalentTo(a.Subtract(b));
    }

    [Fact]
    public void When_multiplying_a_vector_by_scalar_then_each_element_of_the_vector_is_multiplied_by_the_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5);
        var vector = TMatrices.V([11.0, 19.0]);

        var multiplied = vector.Multiply(scalar);

        using var _ = new AssertionScope();

        multiplied.Should().BeEquivalentTo(TMatrices.V([55.0, 95.0]));
        (scalar * vector).Should().BeEquivalentTo(vector.Multiply(scalar));
    }

    [Fact]
    public void Real_vector_can_by_multiplied_by_a_real_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5.0);
        var vector = TMatrices.V([1.0, 2.0]);

        var multiplied = vector.Multiply(scalar);

        using var _ = new AssertionScope();

        multiplied.Should().BeEquivalentTo(TMatrices.V([5.0, 10.0]));
        (scalar * vector).Should().BeEquivalentTo(vector.Multiply(scalar));
    }

    [Fact]
    public void Scalar_multiplication_respects_complex_multiplication()
    {
        var scalarA = RealNumber<TRealNumber>.R(3);
        var scalarB = RealNumber<TRealNumber>.R(7);

        var vector = TMatrices.V([23.0, 31.0]);

        (scalarA * scalarB * vector).Should().BeEquivalentTo(scalarA * (scalarB * vector));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = RealNumber<TRealNumber>.R(3);
        var vectorA = TMatrices.V([7.0, 13.0]);

        var vectorB = TMatrices.V([23.0, 31.0]);

        (scalar * vectorA + scalar * vectorB).Should().BeEquivalentTo(scalar * (vectorA + vectorB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_complex_addition()
    {
        var scalarA = RealNumber<TRealNumber>.R(3);
        var scalarB = RealNumber<TRealNumber>.R(7);

        var vector = TMatrices.V([23.0, 31.0]);

        (scalarA * vector + scalarB * vector).Should().BeEquivalentTo((scalarA + scalarB) * vector);
    }

    [Fact]
    public void Transpose_of_a_column_vector_is_row_vector_with_same_entries_of_the_original_vector()
    {
        var vector = TMatrices.V([1.0, 3.0]);

        var transpose = vector.Transpose();

        using var _ = new AssertionScope();

        transpose.Should().BeEquivalentTo(TMatrices.U([1.0, 3.0]));
        vector.Transpose().Should().BeEquivalentTo(transpose);
    }

    [Fact]
    public void Inner_product_is_a_sum_of_products_of_left_vector_components_and_conjucates_of_right_vector_components()
    {
        var a = TMatrices.V([1.0, 3.0]);
        var b = TMatrices.V([7.0, 13.0]);

        var innerProduct = a.InnerProduct(b);

        using var _ = new AssertionScope();

        innerProduct.Should().Be(RealNumber<TRealNumber>.R(46));
        (a * b).Should().Be(a.InnerProduct(b));
    }

    [Fact]
    public void Inner_product_respects_addition()
    {
        var a = TMatrices.V([1.0, 3.0]);

        var b = TMatrices.V([7.0, 13.0]);

        var c = TMatrices.V([23.0, 31.0]);

        (a * c + b * c).Should().Be((a + b) * c);
    }

    [Fact]
    public void Inner_product_respects_scalar_multiplication()
    {
        var a = TMatrices.V([1.0, 3.0]);

        var b = TMatrices.V([7.0, 13.0]);

        var scalar = RealNumber<TRealNumber>.R(23.0);

        (scalar * (a * b)).Should().Be(scalar * a * b);
    }

    [Fact]
    public void Inner_product_of_a_complex_vector_with_itself_is_a_real_number()
    {
        var vector = TMatrices.V([1.0, 3.0]);

        var innerProduct = vector.InnerProduct(vector);

        innerProduct.Should().Be(RealNumber<TRealNumber>.R(10));
    }

    [Fact]
    public void Norm_is_square_root_of_inner_product_of_vector_with_itself()
    {
        var vector = TMatrices.V([4.0, 6.0, 12.0, 0.0]);

        var norm = vector.Norm();

        norm.Should().Be(RealNumber<TRealNumber>.Sqrt(196));
    }

    [Fact]
    public void Vector_can_be_normalized_to_have_length_of_one_by_dividing_it_by_its_length()
    {
        var vector = TMatrices.V([3.0, 2.0, -1.0]);

        var normalized = vector.Normalized();

        using var _ = new AssertionScope();

        normalized.Should().BeEquivalentTo(TRealNumber.One / RealNumber<TRealNumber>.Sqrt(vector * vector) * vector);
    }

    [Fact]
    public void Distance_of_the_two_vectors_is_the_norm_of_the_difference()
    {
        var a = TMatrices.V([1.0, 3.0]);
        var b = TMatrices.V([7.0, 13.0]);

        var distance = a.Distance(b);

        distance.Should().Be(RealNumber<TRealNumber>.Sqrt(136));
    }

    [Fact]
    public void Tensor_product_of_vectors_contains_combinations_of_products_of_all_elements_of_both_vectors()
    {
        var a = TMatrices.V([1.0, 3.0]);
        var b = TMatrices.V([7.0, 13.0]);

        var tensorProduct = a.TensorProduct(b);

        tensorProduct.Should().BeEquivalentTo(TMatrices.V([7.0, 13.0, 21.0, 39.0]));
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = TMatrices.V([1.0, 3.0]);
        var b = TMatrices.V([7.0, 13.0]);
        var c = TMatrices.V([23.0, 31.0]);

        a.TensorProduct(b.TensorProduct(c)).Should().BeEquivalentTo(a.TensorProduct(b).TensorProduct(c));
    }
}