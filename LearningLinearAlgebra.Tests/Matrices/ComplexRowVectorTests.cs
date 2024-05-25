using LearningLinearAlgebra.Matrices.Complex;
using LearningLinearAlgebra.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Numerics;
using Xunit;

namespace LearningLinearAlgebra.Tests.Matrices;

public class SinglePrecisionCpuComplexRowVectorTests : ComplexRowVectorTests<SquareMatrix<float>, RowVector<float>, ColumnVector<float>, float> { }
public class DoublePrecisionCpuComplexRowVectorTests : ComplexRowVectorTests<SquareMatrix<double>, RowVector<double>, ColumnVector<double>, double> { }

public abstract class ComplexRowVectorTests<TMatrix, TRowVector, TColumnVector, TRealNumber>
    where TMatrix : ISquareMatrix<TMatrix, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    [Fact]
    public void Sum_of_two_vectors_is_calculated_as_sum_of_the_components()
    {
        var a = TRowVector.U([(1, 2), (3, 5)]);
        var b = TRowVector.U([(7, 11), (13, 19)]);

        var sum = TRowVector.Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(TRowVector.U([(8, 13), (16, 24)]));
        (a + b).Should().BeEquivalentTo(TRowVector.Add(a, b));
        a.Add(b).Should().BeEquivalentTo(TRowVector.Add(a, b));
    }

    [Fact]
    public void Sum_of_complex_vectors_is_commutative()
    {
        var a = TRowVector.U([(1, 2), (3, 5)]);
        var b = TRowVector.U([(7, 11), (13, 19)]);

        (b + a).Should().BeEquivalentTo(a + b);
    }

    [Fact]
    public void Sum_of_complex_vectors_is_associative()
    {
        var a = TRowVector.U([(1, 2), (3, 5)]);
        var b = TRowVector.U([(7, 11), (13, 19)]);
        var c = TRowVector.U([(23, 29), (31, 37)]);

        (a + (b + c)).Should().BeEquivalentTo(a + b + c);
    }

    [Fact]
    public void Sum_of_vector_and_its_the_inverse_is_zero()
    {
        var vector = TRowVector.U([(1, 2), (3, 5)]);

        var zero = TRowVector.Zero(2);

        (vector + -vector).Should().BeEquivalentTo(zero);
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var vector = TRowVector.U([(1, 2), (3, 5)]);

        var zero = TRowVector.Zero(2);

        using var _ = new AssertionScope();

        (vector + zero).Should().BeEquivalentTo(vector);
        (zero + vector).Should().BeEquivalentTo(vector);
    }

    [Fact]
    public void Difference_of_two_vectors_is_calculated_as_difference_of_the_components()
    {
        var a = TRowVector.U([(1, 2), (3, 5)]);

        var b = TRowVector.U([(7, 11), (13, 19)]);

        var difference = TRowVector.Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(TRowVector.U([(-6, -9), (-10, -14)]));
        (a - b).Should().BeEquivalentTo(TRowVector.Subtract(a, b));
        a.Subtract(b).Should().BeEquivalentTo(TRowVector.Subtract(a, b));
    }

    [Fact]
    public void When_multiplying_a_vector_by_scalar_then_each_element_of_the_vector_is_multiplied_by_the_scalar()
    {
        var scalar = ComplexNumber<TRealNumber>.C(5, 7);
        var vector = TRowVector.U([(11, 13), (19, 21)]);

        var multiplied = TRowVector.Multiply(scalar, vector);

        using var _ = new AssertionScope();

        multiplied.Should().BeEquivalentTo(TRowVector.U([(-36, 142), (-52, 238)]));
        (scalar * vector).Should().BeEquivalentTo(TRowVector.Multiply(scalar, vector));
        scalar.Multiply(vector).Should().BeEquivalentTo(TRowVector.Multiply(scalar, vector));
    }

    [Fact]
    public void Complex_vector_can_by_multiplied_by_a_real_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5);
        var vector = TRowVector.U([(1, 0), (2, 0)]);

        var multiplied = TRowVector.Multiply(scalar, vector);

        using var _ = new AssertionScope();

        multiplied.Should().BeEquivalentTo(TRowVector.U([(5, 0), (10, 0)]));
        (scalar * vector).Should().BeEquivalentTo(TRowVector.Multiply(scalar, vector));
        scalar.Multiply(vector).Should().BeEquivalentTo(TRowVector.Multiply(scalar, vector));
    }

    [Fact]
    public void Scalar_multiplication_respects_complex_multiplication()
    {
        var scalarA = ComplexNumber<TRealNumber>.C(3, 5);
        var scalarB = ComplexNumber<TRealNumber>.C(7, 11);

        var vector = TRowVector.U([(23, 29), (31, 37)]);

        (scalarA * scalarB * vector).Should().BeEquivalentTo(scalarA * (scalarB * vector));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = ComplexNumber<TRealNumber>.C(3, 5);
        var vectorA = TRowVector.U([(7, 11), (13, 19)]);

        var vectorB = TRowVector.U([(23, 29), (31, 37)]);

        (scalar * vectorA + scalar * vectorB).Should().BeEquivalentTo(scalar * (vectorA + vectorB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_complex_addition()
    {
        var scalarA = ComplexNumber<TRealNumber>.C(3, 5);
        var scalarB = ComplexNumber<TRealNumber>.C(7, 11);

        var vector = TRowVector.U([(23, 29), (31, 37)]);

        (scalarA * vector + scalarB * vector).Should().BeEquivalentTo((scalarA + scalarB) * vector);
    }

    [Fact]
    public void Conjucate_of_a_vector_is_where_each_element_is_a_complex_conjucate_of_the_original_vector()
    {
        var vector = TRowVector.U([(1, 2), (3, 5)]);

        var conjucate = TRowVector.Conjucate(vector);

        using var _ = new AssertionScope();

        conjucate.Should().BeEquivalentTo(TRowVector.U([(1, -2), (3, -5)]));
        vector.Conjucate().Should().BeEquivalentTo(conjucate);
    }

    [Fact]
    public void Transpose_of_a_row_vector_is_column_vector_with_same_entries_of_the_original_vector()
    {
        var vector = TRowVector.U([(1, 2), (3, 5)]);

        var transpose = TRowVector.Transpose(vector);

        using var _ = new AssertionScope();

        transpose.Should().BeEquivalentTo(TColumnVector.V([(1, 2), (3, 5)]));
        vector.Transpose().Should().BeEquivalentTo(transpose);
    }

    [Fact]
    public void Adjoint_of_a_row_vector_is_column_vector_where_each_entry_is_a_complex_conjucate_of_the_original_vector()
    {
        var vector = TRowVector.U([(1, 2), (3, 5)]);

        var adjoint = TRowVector.Adjoint(vector);

        using var _ = new AssertionScope();

        adjoint.Should().BeEquivalentTo(TColumnVector.V([(1, -2), (3, -5)]));
        vector.Adjoint().Should().BeEquivalentTo(adjoint);
    }

    [Fact]
    public void Product_of_row_vector_and_column_vector_is_sum_of_products_of_vector_components()
    {
        var a = TRowVector.U([(1, 2), (3, 5)]);
        var b = TColumnVector.V([(7, 11), (13, 19)]);

        var product = TRowVector.Multiply(a, b);

        using var _ = new AssertionScope();

        product.Should().BeEquivalentTo(ComplexNumber<TRealNumber>.C(-71, 147));
        a.Multiply(b).Should().Be(TRowVector.Multiply(a, b));
        (a * b).Should().BeEquivalentTo(TRowVector.Multiply(a, b));
    }

    [Fact]
    public void Inner_product_is_a_sum_of_products_of_left_vector_components_and_conjucates_of_right_vector_components()
    {
        var a = TRowVector.U([(1, 2), (3, 5)]);
        var b = TRowVector.U([(7, 11), (13, 19)]);

        var innerProduct = TRowVector.InnerProduct(a, b);

        using var _ = new AssertionScope();

        innerProduct.Should().BeEquivalentTo(ComplexNumber<TRealNumber>.C(163, 11));
        (a * b).Should().BeEquivalentTo(TRowVector.InnerProduct(a, b));
    }

    [Fact]
    public void Inner_product_respects_addition()
    {
        var a = TRowVector.U([(1, 2), (3, 5)]);

        var b = TRowVector.U([(7, 11), (13, 19)]);

        var c = TRowVector.U([(23, 29), (31, 37)]);

        (a * c + b * c).Should().BeEquivalentTo((a + b) * c);
    }

    [Fact]
    public void Inner_product_respects_scalar_multiplication()
    {
        var a = TRowVector.U([(1, 2), (3, 5)]);

        var b = TRowVector.U([(7, 11), (13, 19)]);

        var scalar = (23, 29);

        (scalar * (a * b)).Should().BeEquivalentTo(scalar * a * b);
    }

    [Fact]
    public void Inner_product_of_a_complex_vector_with_itself_is_a_real_number()
    {
        var vector = TRowVector.U([(1, 2), (3, 5)]);

        var innerProduct = TRowVector.InnerProduct(vector, vector);

        innerProduct.Should().BeEquivalentTo(ComplexNumber<TRealNumber>.C(39, 0));
    }

    [Fact]
    public void Norm_is_square_root_of_inner_product_of_vector_with_itself()
    {
        var vector = TRowVector.U([(4, 3), (6, -4), (12, -7), (0, 13)]);

        var norm = TRowVector.Norm(vector);

        using var _ = new AssertionScope();

        norm.Should().Be(RealNumber<TRealNumber>.Sqrt(439));
        vector.Norm().Should().Be(TRowVector.Norm(vector));
    }

    [Fact]
    public void Vector_can_be_normalized_to_have_length_of_one_by_dividing_it_by_its_length()
    {
        var vector = TRowVector.U([(3, 1), (2, 5), (-1, 0)]);

        var normalized = TRowVector.Normalized(vector);

        using var _ = new AssertionScope();

        normalized.Should().BeEquivalentTo(TRealNumber.One / ComplexNumber<TRealNumber>.Sqrt(vector * vector) * vector);
        vector.Normalized().Should().BeEquivalentTo(TRowVector.Normalized(vector));
    }

    [Fact]
    public void Distance_of_the_two_vectors_is_the_norm_of_the_difference()
    {

        var a = TRowVector.U([(1, 2), (3, 5)]);
        var b = TRowVector.U([(7, 11), (13, 19)]);

        var distance = TRowVector.Distance(a, b);

        using var _ = new AssertionScope();

        distance.Should().Be(RealNumber<TRealNumber>.Sqrt(413));
        a.Distance(b).Should().Be(TRowVector.Distance(a, b));
    }

    [Fact]
    public void Tensor_product_of_vectors_contains_combinations_of_products_of_all_elements_of_both_vectors()
    {
        var a = TRowVector.U([(1, 2), (3, 5)]);
        var b = TRowVector.U([(7, 11), (13, 19)]);

        var tensorProduct = TRowVector.TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(TRowVector.U([(-15, 25), (-25, 45), (-34, 68), (-56, 122)]));
        a.TensorProduct(b).Should().BeEquivalentTo(TRowVector.TensorProduct(a, b));
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = TRowVector.U([(1, 2), (3, 5)]);
        var b = TRowVector.U([(7, 11), (13, 19)]);
        var c = TRowVector.U([(23, 29), (31, 37)]);

        TRowVector.TensorProduct(a, TRowVector.TensorProduct(b, c)).Should().BeEquivalentTo(TRowVector.TensorProduct(TRowVector.TensorProduct(a, b), c));
    }
}