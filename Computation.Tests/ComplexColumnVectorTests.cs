using System.Numerics;
using Computation.Matrices.Complex;
using Computation.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Computation.Tests;

public class ManagedSinglePrecisionComplexColumnVectorTests : ComplexColumnVectorTests<Managed.Complex.Matrices<float>, float>;
public class ManagedDoublePrecisionComplexColumnVectorTests : ComplexColumnVectorTests<Managed.Complex.Matrices<double>, double>;
public class CudaSinglePrecisionComplexColumnVectorTests : ComplexColumnVectorTests<Cuda.Complex.Matrices<float>, float>;
public class CudaDoublePrecisionComplexColumnVectorTests : ComplexColumnVectorTests<Cuda.Complex.Matrices<double>, double>;


public abstract class ComplexColumnVectorTests<TMatrices, TRealNumber>
    where TMatrices : IMatrices<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    protected ComplexColumnVectorTests() =>
        Formatters<TRealNumber>.Register();

    [Fact]
    public void Sum_of_two_vectors_is_calculated_as_sum_of_the_components()
    {
        var a = TMatrices.V([(1, 2), (3, 5)]);
        var b = TMatrices.V([(7, 11), (13, 19)]);

        var sum = a.Add(b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(TMatrices.V([(8, 13), (16, 24)]));
        (a + b).Should().BeEquivalentTo(a.Add(b));
    }

    [Fact]
    public void Sum_of_complex_vectors_is_commutative()
    {
        var a = TMatrices.V([(1, 2), (3, 5)]);
        var b = TMatrices.V([(7, 11), (13, 19)]);

        (b + a).Should().BeEquivalentTo(a + b);
    }

    [Fact]
    public void Sum_of_complex_vectors_is_associative()
    {
        var a = TMatrices.V([(1, 2), (3, 5)]);
        var b = TMatrices.V([(7, 11), (13, 19)]);
        var c = TMatrices.V([(23, 29), (31, 37)]);

        (a + (b + c)).Should().BeEquivalentTo(a + b + c);
    }

    [Fact]
    public void Sum_of_vector_and_its_the_inverse_is_zero()
    {
        var vector = TMatrices.V([(1, 2), (3, 5)]);

        var zero = TMatrices.ZeroColumnVector(2);

        (vector + -vector).Should().BeEquivalentTo(zero);
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var vector = TMatrices.V([(1, 2), (3, 5)]);

        var zero = TMatrices.ZeroColumnVector(2);

        using var _ = new AssertionScope();

        (vector + zero).Should().BeEquivalentTo(vector);
        (zero + vector).Should().BeEquivalentTo(vector);
    }

    [Fact]
    public void Difference_of_two_vectors_is_calculated_as_difference_of_the_components()
    {
        var a = TMatrices.V([(1, 2), (3, 5)]);

        var b = TMatrices.V([(7, 11), (13, 19)]);

        var difference = a.Subtract(b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(TMatrices.V([(-6, -9), (-10, -14)]));
        (a - b).Should().BeEquivalentTo(a.Subtract(b));
    }

    [Fact]
    public void When_multiplying_a_vector_by_scalar_then_each_element_of_the_vector_is_multiplied_by_the_scalar()
    {
        var scalar = ComplexNumber<TRealNumber>.C(5, 7);
        var vector = TMatrices.V([(11, 13), (19, 21)]);

        var multiplied = vector.Multiply(scalar);

        using var _ = new AssertionScope();

        multiplied.Should().BeEquivalentTo(TMatrices.V([(-36, 142), (-52, 238)]));
        (scalar * vector).Should().BeEquivalentTo(vector.Multiply(scalar));
    }

    [Fact]
    public void Complex_vector_can_by_multiplied_by_a_real_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5);
        var vector = TMatrices.V([(1, 0), (2, 0)]);

        var multiplied = vector.Multiply(scalar);

        using var _ = new AssertionScope();

        multiplied.Should().BeEquivalentTo(TMatrices.V([(5, 0), (10, 0)]));
        (scalar * vector).Should().BeEquivalentTo(vector.Multiply(scalar));
    }

    [Fact]
    public void Scalar_multiplication_respects_complex_multiplication()
    {
        var scalarA = ComplexNumber<TRealNumber>.C(3, 5);
        var scalarB = ComplexNumber<TRealNumber>.C(7, 11);

        var vector = TMatrices.V([(23, 29), (31, 37)]);

        (scalarA * scalarB * vector).Should().BeEquivalentTo(scalarA * (scalarB * vector));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = ComplexNumber<TRealNumber>.C(3, 5);
        var vectorA = TMatrices.V([(7, 11), (13, 19)]);

        var vectorB = TMatrices.V([(23, 29), (31, 37)]);

        (scalar * vectorA + scalar * vectorB).Should().BeEquivalentTo(scalar * (vectorA + vectorB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_complex_addition()
    {
        var scalarA = ComplexNumber<TRealNumber>.C(3, 5);
        var scalarB = ComplexNumber<TRealNumber>.C(7, 11);

        var vector = TMatrices.V([(23, 29), (31, 37)]);

        (scalarA * vector + scalarB * vector).Should().BeEquivalentTo((scalarA + scalarB) * vector);
    }

    [Fact]
    public void Conjucate_of_a_vector_is_where_each_element_is_a_complex_conjucate_of_the_original_vector()
    {
        var vector = TMatrices.V([(1, 2), (3, 5)]);

        var conjucate = vector.Conjucate();

        conjucate.Should().BeEquivalentTo(TMatrices.V([(1, -2), (3, -5)]));
    }

    [Fact]
    public void Transpose_of_a_column_vector_is_row_vector_with_same_entries_of_the_original_vector()
    {
        var vector = TMatrices.V([(1, 2), (3, 5)]);

        var transpose = vector.Transpose();

        transpose.Should().BeEquivalentTo(TMatrices.U([(1, 2), (3, 5)]));
    }

    [Fact]
    public void Adjoint_of_a_column_vector_is_row_vector_where_each_entry_is_a_complex_conjucate_of_the_original_vector()
    {
        var vector = TMatrices.V([(1, 2), (3, 5)]);

        var adjoint = vector.Adjoint();

        adjoint.Should().BeEquivalentTo(TMatrices.U([(1, -2), (3, -5)]));
    }

    [Fact]
    public void Inner_product_is_a_sum_of_products_of_left_vector_components_and_conjucates_of_right_vector_components()
    {
        var a = TMatrices.V([(1, 2), (3, 5)]);
        var b = TMatrices.V([(7, 11), (13, 19)]);

        var innerProduct = a.InnerProduct(b);

        using var _ = new AssertionScope();

        innerProduct.Should().Be(ComplexNumber<TRealNumber>.C(163, 11));
        (a * b).Should().Be(a.InnerProduct(b));
    }

    [Fact]
    public void Inner_product_respects_addition()
    {
        var a = TMatrices.V([(1, 2), (3, 5)]);

        var b = TMatrices.V([(7, 11), (13, 19)]);

        var c = TMatrices.V([(23, 29), (31, 37)]);

        (a * c + b * c).Should().Be((a + b) * c);
    }

    [Fact]
    public void Inner_product_respects_scalar_multiplication()
    {
        var a = TMatrices.V([(1, 2), (3, 5)]);

        var b = TMatrices.V([(7, 11), (13, 19)]);

        var scalar = (23, 29);

        (scalar * (a * b)).Should().Be(scalar * a * b);
    }

    [Fact]
    public void Inner_product_of_a_complex_vector_with_itself_is_a_real_number()
    {
        var vector = TMatrices.V([(1, 2), (3, 5)]);

        var innerProduct = vector.InnerProduct(vector);

        innerProduct.Should().Be(ComplexNumber<TRealNumber>.C(39, 0));
    }

    [Fact]
    public void Norm_is_square_root_of_inner_product_of_vector_with_itself()
    {
        var vector = TMatrices.V([(4, 3), (6, -4), (12, -7), (0, 13)]);

        var norm = vector.Norm();

        using var _ = new AssertionScope();

        norm.Should().Be(RealNumber<TRealNumber>.Sqrt(439));
        vector.Norm().Should().Be(vector.Norm());
    }

    [Fact]
    public void Vector_can_be_normalized_to_have_length_of_one_by_dividing_it_by_its_length()
    {
        var vector = TMatrices.V([(3, 1), (2, 5), (-1, 0)]);

        var normalized = vector.Normalized();

        using var _ = new AssertionScope();

        normalized.Should().BeEquivalentTo(1 / ComplexNumber<TRealNumber>.Sqrt(vector * vector) * vector);
        vector.Normalized().Should().BeEquivalentTo(vector.Normalized());
    }

    [Fact]
    public void Distance_of_the_two_vectors_is_the_norm_of_the_difference()
    {
        var a = TMatrices.V([(1, 2), (3, 5)]);
        var b = TMatrices.V([(7, 11), (13, 19)]);

        var distance = a.Distance(b);

        using var _ = new AssertionScope();

        distance.Should().Be(RealNumber<TRealNumber>.Sqrt(413));
        a.Distance(b).Should().Be(a.Distance(b));
    }

    [Fact]
    public void Tensor_product_of_vectors_contains_combinations_of_products_of_all_elements_of_both_vectors()
    {
        var a = TMatrices.V([(1, 2), (3, 5)]);
        var b = TMatrices.V([(7, 11), (13, 19)]);

        var tensorProduct = a.TensorProduct(b);

        tensorProduct.Should().BeEquivalentTo(TMatrices.V([(-15, 25), (-25, 45), (-34, 68), (-56, 122)]));
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = TMatrices.V([(1, 2), (3, 5)]);
        var b = TMatrices.V([(7, 11), (13, 19)]);
        var c = TMatrices.V([(23, 29), (31, 37)]);

        a.TensorProduct(b.TensorProduct(c)).Should().BeEquivalentTo(a.TensorProduct(b).TensorProduct(c));
    }
}