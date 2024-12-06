using System.Numerics;
using FluentAssertions;
using FluentAssertions.Execution;
using LearningLinearAlgebra.Matrices.Real.Abstract;
using LearningLinearAlgebra.Numbers;
using Xunit;
using Cuda2 = LearningLinearAlgebra.Matrices.Real.Cuda;
using Managed = LearningLinearAlgebra.Matrices.Real.Managed;

namespace LearningLinearAlgebra.Tests.Matrices;

public class CudaSinglePrecisionRowVectorTests : RealRowVectorTests<Cuda2.RowVector<float>, Cuda2.ColumnVector<float>, float>;
public class ManagedSinglePrecisionRowVectorTests : RealRowVectorTests<Managed.RowVector<float>, Managed.ColumnVector<float>, float>;
public class ManagedDoublePrecisionRowVectorTests : RealRowVectorTests<Managed.RowVector<double>, Managed.ColumnVector<double>, double>;

public abstract class RealRowVectorTests<TRowVector, TColumnVector, TRealNumber>
     where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
     where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
     where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    [Fact]
    public void Sum_of_two_vectors_is_calculated_as_sum_of_the_components()
    {
        var a = TRowVector.U([-1, -3]);
        var b = TRowVector.U([-7, -13]);

        var sum = TRowVector.Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().Equal(TRowVector.U([-8, -16]));
        (a + b).Should().Equal(TRowVector.Add(a, b));
        a.Add(b).Should().Equal(TRowVector.Add(a, b));
    }

    [Fact]
    public void Sum_of_vectors_is_commutative()
    {
        var a = TRowVector.U([-1, -3]);
        var b = TRowVector.U([-7, -13]);

        (a + b).Should().Equal(b + a);
    }

    [Fact]
    public void Sum_of_vectors_is_associative()
    {
        var a = TRowVector.U([-1, -3]);
        var b = TRowVector.U([-7, -13]);
        var c = TRowVector.U([-23, -31]);

        (a + (b + c)).Should().Equal(a + b + c);
    }

    [Fact]
    public void Sum_of_vector_and_its_the_inverse_is_zero()
    {
        var vector = TRowVector.U([-1, -3]);

        var zero = TRowVector.Zero(2);

        (vector + -vector).Should().Equal(zero);
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var vector = TRowVector.U([-1, -3]);
        var zero = TRowVector.Zero(2);

        using var _ = new AssertionScope();

        (vector + zero).Should().Equal(vector);
        (zero + vector).Should().Equal(vector);
    }

    [Fact]
    public void Difference_of_two_vectors_is_calculated_as_difference_of_the_components()
    {
        var a = TRowVector.U([1, 3]);
        var b = TRowVector.U([7, 13]);

        var difference = TRowVector.Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().Equal(TRowVector.U([-6, -10]));
        (a - b).Should().Equal(TRowVector.Subtract(a, b));
    }

    [Fact]
    public void When_multiplying_a_vector_by_scalar_then_each_element_of_the_vector_is_multiplied_by_the_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5);
        var vector = TRowVector.U([11, 19]);

        var multiplied = TRowVector.Multiply(scalar, vector);

        using var _ = new AssertionScope();

        multiplied.Should().Equal(TRowVector.U([55, 95]));
        (scalar * vector).Should().Equal(TRowVector.Multiply(scalar, vector));
    }

    [Fact]
    public void Scalar_multiplication_respects_vector_multiplication()
    {
        var scalarA = RealNumber<TRealNumber>.R(-3);
        var scalarB = RealNumber<TRealNumber>.R(-7);
        var vector = TRowVector.U([-23, -31]);

        (scalarA * scalarB * vector).Should().Equal(scalarA * (scalarB * vector));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = RealNumber<TRealNumber>.R(-3);
        var vectorA = TRowVector.U([-7, -13]);
        var vectorB = TRowVector.U([-23, -31]);

        (scalar * vectorA + scalar * vectorB).Should().Equal(scalar * (vectorA + vectorB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_vector_addition()
    {
        var scalarA = RealNumber<TRealNumber>.R(-3);
        var scalarB = RealNumber<TRealNumber>.R(-7);
        var vector = TRowVector.U([-23, -31]);

        (scalarA * vector + scalarB * vector).Should().Equal((scalarA + scalarB) * vector);
    }

    [Fact]
    public void Transpose_of_a_row_vector_is_column_vector_with_same_entries_of_the_original_vector()
    {
        var vector = TRowVector.U([5, 3, -7]);

        var transpose = TRowVector.Transpose(vector);

        using var _ = new AssertionScope();

        transpose.Should().Equal(TColumnVector.V([5, 3, -7]));
        vector.Transpose().Should().Equal(transpose);
    }

    [Fact]
    public void Product_of_row_vector_and_column_vector_is_sum_of_products_of_vector_components()
    {
        var a = TRowVector.U([5, 3, -7]);
        var b = TColumnVector.V([6, 2, 0]);

        var innerProduct = TRowVector.Multiply(a, b);

        using var _ = new AssertionScope();

        innerProduct.Should().Be(RealNumber<TRealNumber>.R(36));
        a.Multiply(b).Should().Be(TRowVector.Multiply(a, b));
        (a * b).Should().Be(TRowVector.Multiply(a, b));
    }

    [Fact]
    public void Inner_product_is_a_sum_of_products_of_vector_components()
    {
        var a = TRowVector.U([5, 3, -7]);
        var b = TRowVector.U([6, 2, 0]);

        var innerProduct = TRowVector.InnerProduct(a, b);

        using var _ = new AssertionScope();

        innerProduct.Should().Be(RealNumber<TRealNumber>.R(36));
        a.InnerProduct(b).Should().Be(TRowVector.InnerProduct(a, b));
        (a * b).Should().Be(TRowVector.InnerProduct(a, b));
    }

    [Fact]
    public void Inner_product_respects_addition()
    {
        var a = TRowVector.U([-1, -3]);
        var b = TRowVector.U([-7, -13]);
        var c = TRowVector.U([-23, -31]);

        (a * c + b * c).Should().Be((a + b) * c);
    }

    [Fact]
    public void Inner_product_respects_scalar_multiplication()
    {
        var a = TRowVector.U([-1, -3]);
        var b = TRowVector.U([-7, -13]);
        var scalar = RealNumber<TRealNumber>.R(23);

        (scalar * (a * b)).Should().Be(scalar * a * b);
    }

    [Fact]
    public void Norm_is_square_root_of_inner_product_of_vector_with_itself()
    {
        var vector = TRowVector.U([3, -6, 2]);

        var norm = TRowVector.Norm(vector);

        using var _ = new AssertionScope();

        norm.Should().Be(RealNumber<TRealNumber>.R(7));
        vector.Norm().Should().Be(TRowVector.Norm(vector));
    }

    [Fact]
    public void Vector_can_be_normalized_to_have_length_of_one_by_dividing_it_by_its_length()
    {
        var vector = TRowVector.U([3, -6, 2]);

        var normalized = TRowVector.Normalized(vector);

        using var _ = new AssertionScope();

        normalized.Should().Equal(TRealNumber.One / RealNumber<TRealNumber>.Sqrt(vector * vector) * vector);
        vector.Normalized().Should().Equal(TRowVector.Normalized(vector));
    }

    [Fact]
    public void Distance_of_the_two_vectors_is_the_norm_of_the_difference()
    {
        var a = TRowVector.U([3, 1, 2]);
        var b = TRowVector.U([2, 2, -1]);

        var distance = TRowVector.Distance(a, b);

        using var _ = new AssertionScope();

        distance.Should().Be(RealNumber<TRealNumber>.Sqrt(11));
        a.Distance(b).Should().Be(TRowVector.Distance(a, b));
    }

    [Fact]
    public void Tensor_product_of_vectors_contains_combinations_of_products_of_all_elements_of_both_vectors()
    {

        var a = TRowVector.U([3, 4, 7]);
        var b = TRowVector.U([-1, 2]);

        var tensorProduct = TRowVector.TensorProduct(a, b);

        using var _ = new AssertionScope();

        tensorProduct.Should().Equal(TRowVector.U([-3, -4, -7, 6, 8, 14]));
        a.TensorProduct(b).Should().Equal(TRowVector.TensorProduct(a, b));
    }

    [Fact]
    public void Another_example_of_tensor_product()
    {

        var a = TRowVector.U([-1, 2]);
        var b = TRowVector.U([3, 4, 7]);

        var tensorProduct = TRowVector.TensorProduct(a, b);

        tensorProduct.Should().Equal(TRowVector.U([-3, 6, -4, 8, -7, 14]));
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = TRowVector.U([-1, -3]);
        var b = TRowVector.U([-7, -13]);
        var c = TRowVector.U([-23, -31]);

        TRowVector.TensorProduct(TRowVector.TensorProduct(a, b), c).Should().Equal(TRowVector.TensorProduct(a, TRowVector.TensorProduct(b, c)));
    }

    [Fact]
    public void Vector_is_converted_from_linearly_independent_base_to_orthonormal_base_by_dividing_it_by_its_norm()
    {
        var I = TRowVector.U([3, 0, 0]);
        var II = TRowVector.U([0, 1, 2]);
        var III = TRowVector.U([0, 25]);

        using var _ = new AssertionScope();

        TRowVector.Orthonormal(I).Should().Equal(TRealNumber.One / RealNumber<TRealNumber>.Sqrt(I * I) * I);
        TRowVector.Orthonormal(II).Should().Equal(TRealNumber.One / RealNumber<TRealNumber>.Sqrt(II * II) * II);
        TRowVector.Orthonormal(III).Should().Equal(TRealNumber.One / RealNumber<TRealNumber>.Sqrt(III * III) * III);

        I.Orthonormal().Should().Equal(TRowVector.Orthonormal(I));
    }
}