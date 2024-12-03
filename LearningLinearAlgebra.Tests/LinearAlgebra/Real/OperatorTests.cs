using FluentAssertions;
using FluentAssertions.Execution;
using LearningLinearAlgebra.LinearAlgebra.RealVectorSpace;
using Xunit;
using static LearningLinearAlgebra.LinearAlgebra.RealVectorSpace.Bra<float>;
using static LearningLinearAlgebra.LinearAlgebra.RealVectorSpace.Ket<float>;
using static LearningLinearAlgebra.LinearAlgebra.RealVectorSpace.Operator<float>;
using static LearningLinearAlgebra.Numbers.RealNumber<float>;

namespace LearningLinearAlgebra.Tests.LinearAlgebra.Real;

public class OperatorTests
{
    [Fact]
    public void Dimension_of_the_operator_is_the_dimension_of_the_vector_space_it_is_acting_on()
    {
        var @operator = M(new float[,] {
            { 1, 3 },
            { 7, 13 }
        });

        var dimension = Dimension(@operator);

        using var _ = new AssertionScope();

        dimension.Should().Be(2);
        @operator.Dimension().Should().Be(Dimension(@operator));
    }

    [Fact]
    public void On_identity_operator_diagonal_entries_has_value_one_and_everything_else_is_zeroes()
    {
        var identity = M(new float[,] {
            { 1, 0 },
            { 0, 1 } });

        using var _ = new AssertionScope();

        Identity(2).Should().BeEquivalentTo(identity);
    }

    [Fact]
    public void Zero_operator_has_only_zero_entries()
    {
        var zero = Operator<float>.Zero(2);

        zero.Should().BeEquivalentTo(M(new float[,] {
          { 0, 0 },
          { 0, 0 } }));
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var @operator = M(new float[,] {
          { 1, 3 },
          { 7, 13 } });

        var zero = Operator<float>.Zero(2);

        using var _ = new AssertionScope();

        (@operator + zero).Should().BeEquivalentTo(@operator);
        (zero + @operator).Should().BeEquivalentTo(@operator);
    }

    [Fact]
    public void Sum_of_two_operators_is_calculated_as_sum_of_the_components()
    {
        var a = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        var sum = Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(M(new float[,] {
            { 24, 34 },
            { 48, 60 } }));
        (a + b).Should().BeEquivalentTo(Add(a, b));
        a.Add(b).Should().BeEquivalentTo(Add(a, b));
    }

    [Fact]
    public void Difference_of_two_operators_is_calculated_as_difference_of_the_components()
    {
        var a = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        var difference = Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(
            M(new float[,] {
                { -22, -28 },
                { -34, -34 }
            }
        ));
        (a - b).Should().BeEquivalentTo(Subtract(a, b));
        a.Subtract(b).Should().BeEquivalentTo(Subtract(a, b));
    }

    [Fact]
    public void Sum_of_operator_and_its_the_inverse_is_zero()
    {
        var @operator = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        (@operator + AdditiveInverse(@operator)).Should().BeEquivalentTo(Operator<float>.Zero(2));
        (@operator + -@operator).Should().BeEquivalentTo(Operator<float>.Zero(2));
    }

    [Fact]
    public void When_multiplying_an_operator_by_scalar_then_each_element_of_the_operator_is_multiplied_by_the_scalar()
    {
        const float scalar = 5.0f;
        var @operator = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var multiplied = Multiply(scalar, @operator);

        using var _ = new AssertionScope();

        multiplied.Should().BeEquivalentTo(M(new float[,] {
            { 5, 15 },
            { 35, 65 }
        }));
        (scalar * @operator).Should().BeEquivalentTo(Multiply(scalar, @operator));
        scalar.Multiply(@operator).Should().BeEquivalentTo(Multiply(scalar, @operator));
    }

    [Fact]
    public void Multiplying_operators_using_the_matrix_multiplication()
    {
        var a = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        var product = Multiply(a, b);

        using var _ = new AssertionScope();

        product.Should().BeEquivalentTo(M(new float[,] {
            { 146, 172 },
            { 694, 828 }
        }));
        (a * b).Should().BeEquivalentTo(Multiply(a, b));
        a.Multiply(b).Should().BeEquivalentTo(Multiply(a, b));
    }

    [Fact]
    public void Operator_is_a_linear_map_in_vector_space_that_can_be_seen_as_operator_acting_on_ket_components()
    {
        var @operator = M(new float[,] {
            { 1, 3 },
            { 7, 13 }
        });

        var ket = V([23, 31]);

        var result = Act(@operator, ket);

        using var _ = new AssertionScope();

        result.Should().Equal(V([116, 564]));
        (@operator * ket).Should().Equal(result);
        @operator.Act(ket).Should().Equal(Act(@operator, ket));
    }

    [Fact]
    public void Operator_is_a_linear_map_in_vector_space_that_can_be_seen_as_operator_acting_on_bra_components()
    {
        var @operator = M(new float[,] {
            { 1, 3 },
            { 7, 13 }
        });

        var bra = U([23, 31]);

        var result = Act(bra, @operator);

        using var _ = new AssertionScope();

        result.Should().Equal(V([116, 564]));
        (bra * @operator).Should().Equal(result);
        bra.Act(@operator).Should().Equal(Act(bra, @operator));
    }

    [Fact]
    public void Another_example_of_operator_multiplication()
    {
        var a = M(new float[,] {
            { 3, 0, 5 },
            { 1, 4, 0 },
            { 4, 0, 4 }
        });

        var b = M(new float[,] {
            { 5, 2, 6 },
            { 0, 4, 2 },
            { 7, 2, 0 }
        });

        var product = Multiply(a, b);

        product.Should().BeEquivalentTo(M(new float[,] {
            { 50, 16, 18 },
            { 5, 18, 14 },
            { 48, 16, 24 }
        }));
    }

    [Fact]
    public void Another_example_of_identity_operator()
    {
        var identity = M(new float[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 }
        });

        using var _ = new AssertionScope();

        Identity(3).Should().BeEquivalentTo(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Multiplying_operator_by_identity_operator_does_not_change_the_operator()
    {
        var matrix = M(new float[,] {
            { 1, 3, 7 },
            { 7, 13, 23 },
            { 31, 41, 47 }
        });

        var identity = M(new float[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 }
        });

        var product = Multiply(matrix, identity);

        product.Should().BeEquivalentTo(matrix);
    }

    [Fact]
    public void Unitary_matrices_preserve_inner_products()
    {
        var a = V([1, 3]);
        var b = V([7, 13]);

        var unitary = M(new float[,] {
            { 1, 0 },
            { 0, 1 }
        });

        InnerProduct(unitary * a, unitary * b).Should().Be(InnerProduct(a, b));
    }

    [Fact]
    public void Unitary_matrices_preserve_distance()
    {
        var a = V([1, 3]);
        var b = V([7, 13]);

        var unitary = M(new float[,] {
            { 1, 0 },
            { 0, 1 }
        });

        Distance(unitary * a, unitary * b).Should().Be(Distance(a, b));
    }

    [Fact]
    public void Tensor_product_of_operator_contains_combinations_of_scalar_products_of_all_elements_of_both_operator()
    {
        var a = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        var tensorProduct = TensorProduct(a, b);

        using var _ = new AssertionScope();

        tensorProduct.Should().BeEquivalentTo(M(new float[,]{
            {23, 31, 69, 93 },
            {41, 47, 123, 141 },
            {161, 217, 299, 403 },
            {287, 329, 533, 611 } }));
        a.TensorProduct(b).Should().BeEquivalentTo(TensorProduct(a, b));
    }

    [Fact]
    public void Another_example_of_tensor_product()
    {
        var a = M(new float[,] {
            { 3, 5, 0 },
            { 0, 12, 6 },
            { 2, 4, 9 }
        });

        var b = M(new float[,] {
            { 1, 3, 5 },
            { 10, 6, 2 },
            { 0, 1, 2 }
        });

        var tensorProduct = TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(M(new float[,] {
            {3, 9, 15, 5, 15, 25, 0, 0, 0 },
            {30, 18, 6, 50, 30, 10, 0, 0, 0 },
            {0, 3, 6, 0, 5, 10, 0, 0, 0 },
            {0, 0, 0, 12, 36, 60, 6, 18, 30 },
            {0, 0, 0, 120, 72, 24, 60, 36, 12 },
            {0, 0, 0, 0, 12, 24, 0, 6, 12 },
            {2, 6, 10, 4, 12, 20, 9, 27, 45 },
            {20, 12, 4, 40, 24, 8, 90, 54, 18 },
            {0, 2, 4, 0, 4, 8, 0, 9, 18 } }));
    }

    [Fact]
    public void Third_example_of_tensor_product()
    {

        var a = M(new float[,] {
            { 1, 2 },
            { 3, 4 }
        });

        var b = M(new float[,] {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        });

        var tensorProduct = TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(M(new float[,] {
            { 1, 2, 3, 2, 4, 6 },
            { 4, 5, 6, 8, 10, 12 },
            { 7, 8, 9, 14, 16, 18 },
            { 3, 6, 9, 4, 8, 12 },
            { 12, 15, 18, 16, 20, 24 },
            { 21, 24, 27, 28, 32, 36 } }));
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = M(new float[,] {
            { 1, 3 },
            { 7, 13 }
        });

        var b = M(new float[,] {
            { 23, 31 },
            { 41, 47 }
        });

        var c = M(new float[,] {
            { 59, 67 },
            { 73, 83 }
        });

        TensorProduct(a, TensorProduct(b, c)).Should().BeEquivalentTo(TensorProduct(TensorProduct(a, b), c));
    }

    [Fact]
    public void Matrix_multiplied_by_its_eigen_vector_equals_to_eigen_value_multiplied_by_eigen_vector()
    {
        var matrix = M(new float[,] {
            { 4, -1 },
            { 2, 1 } });

        var eigenVector = V([1, 1]);

        var eigenValue = R(3);

        (eigenValue * eigenVector).Should().Equal(matrix * eigenVector);
    }
}