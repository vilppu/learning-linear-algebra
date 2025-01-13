using System.Numerics;
using FluentAssertions;
using FluentAssertions.Execution;
using LearningLinearAlgebra.Numbers;
using LearningLinearAlgebra.RealVectorSpace;
using LearningLinearAlgebra.Tests.Helpers;
using Xunit;

namespace LearningLinearAlgebra.Tests.LinearAlgebra.Real;

public class SinglePrecisionOperatorTests : OperatorTests<float>;
public class DoublePrecisionOperatorTests : OperatorTests<double>;

public abstract class OperatorTests<TRealNumber> 
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    static OperatorTests() => Formatters<TRealNumber>.Register();

    [Fact]
    public void Dimension_of_the_operator_is_the_dimension_of_the_vector_space_it_is_acting_on()
    {
        var @operator = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 }
        });

        var dimension = @operator.Dimension<TRealNumber>();

        using var _ = new AssertionScope();

        dimension.Should().Be(2);
        @operator.Dimension().Should().Be(@operator.Dimension<TRealNumber>());
    }

    [Fact]
    public void Sum_of_two_operators_is_calculated_as_sum_of_the_components()
    {
        var a = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = Operator.M<TRealNumber>(new[,] {
            { 23, 31 },
            { 41, 47 } });

        var sum = a.Add<TRealNumber>(b);

        using var _ = new AssertionScope();

        sum.Should().Be(Operator.M<TRealNumber>(new[,] {
            { 24, 34 },
            { 48, 60 } }));
        (a + b).Should().Be(a.Add<TRealNumber>(b));
        a.Add(b).Should().Be(a.Add<TRealNumber>(b));
    }

    [Fact]
    public void Sum_of_complex_operators_is_commutative()
    {
        var a = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = Operator.M<TRealNumber>(new[,] {
            { 23, 31 },
            { 41, 47 } });

        (b + a).Should().Be(a + b);
    }

    [Fact]
    public void Sum_of_complex_operators_is_associative()
    {
        var a = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = Operator.M<TRealNumber>(new[,] {
            { 23, 31 },
            { 41, 47 } });

        var c = Operator.M<TRealNumber>(new[,] {
            { 59, 67 },
            { 73, 83 } });


        (a + (b + c)).Should().Be(a + b + c);
    }

    [Fact]
    public void Sum_of_operator_and_its_the_inverse_is_zero()
    {
        var @operator = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 } });

        var zero = Operator.Zero<TRealNumber>(2);

        (@operator + -@operator).Should().Be(zero);
    }

    [Fact]
    public void Difference_of_two_operators_is_calculated_as_difference_of_the_components()
    {
        var a = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = Operator.M<TRealNumber>(new[,] {
            { 23, 31 },
            { 41, 47 } });

        var difference = a.Subtract<TRealNumber>(b);

        using var _ = new AssertionScope();

        difference.Should().Be(
            Operator.M<TRealNumber>(new[,] {
                { -22, -28 },
                { -34, -34 }
            }
        ));
        (a - b).Should().Be(a.Subtract<TRealNumber>(b));
        a.Subtract(b).Should().Be(a.Subtract<TRealNumber>(b));
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var @operator = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 } });

        var zero = Operator.Zero<TRealNumber>(2);

        using var _ = new AssertionScope();

        (@operator + zero).Should().Be(@operator);
        (zero + @operator).Should().Be(@operator);
    }

    [Fact]
    public void Zero_operator_has_only_zero_entries()
    {
        var zero = Operator.Zero<TRealNumber>(2);

        zero.Should().Be(Operator.M<TRealNumber>(new[,] {
            { 0, 0 },
            { 0, 0 } }));
    }

    [Fact]
    public void When_multiplying_an_operator_by_scalar_then_each_element_of_the_operator_is_multiplied_by_the_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5);
        var @operator = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 } });

        var multiplied = scalar.Multiply<TRealNumber>(@operator);

        using var _ = new AssertionScope();

        multiplied.Should().Be(Operator.M<TRealNumber>(new[,] {
            { 5, 15 },
            { 35, 65 }
        }));
        (scalar * @operator).Should().Be(scalar.Multiply<TRealNumber>(@operator));
        scalar.Multiply(@operator).Should().Be(scalar.Multiply<TRealNumber>(@operator));
    }
    [Fact]
    public void When_multiplying_a_operator_by_scalar_then_each_element_of_the_operator_is_multiplied_by_the_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5);
        var @operator = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 } });

        var multiplied = @operator.Multiply(scalar);

        using var _ = new AssertionScope();

        multiplied.Should().Be(Operator.M<TRealNumber>(new[,] {
            { 5, 15 },
            { 35, 65 }
        }));
        (scalar * @operator).Should().Be(@operator.Multiply(scalar));
        @operator.Multiply(scalar).Should().Be(@operator.Multiply(scalar));
    }

    [Fact]
    public void Scalar_multiplication_respects_complex_multiplication()
    {
        var scalarA = RealNumber<TRealNumber>.R(3);
        var scalarB = RealNumber<TRealNumber>.R(7);

        var @operator = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 }
        });

        (scalarA * scalarB * @operator).Should().Be(scalarA * (scalarB * @operator));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = RealNumber<TRealNumber>.R(3);

        var operatorA = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 }
        });

        var operatorB = Operator.M<TRealNumber>(new[,] {
            { 23, 31 },
            { 41, 47 }
        });

        (scalar * operatorA + scalar * operatorB).Should().Be(scalar * (operatorA + operatorB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_complex_addition()
    {
        var scalarA = RealNumber<TRealNumber>.R(3);
        var scalarB = RealNumber<TRealNumber>.R(7);

        var @operator = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 }
        });

        (scalarA * @operator + scalarB * @operator).Should().Be((scalarA + scalarB) * @operator);
    }

    [Fact]
    public void Multiplying_operators_using_the_operator_multiplication()
    {
        var a = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = Operator.M<TRealNumber>(new[,] {
            { 23, 31 },
            { 41, 47 } });

        var product = a.Multiply<TRealNumber>(b);

        using var _ = new AssertionScope();

        product.Should().Be(Operator.M<TRealNumber>(new[,] {
            { 146, 172 },
            { 694, 828 }
        }));
        (a * b).Should().Be(a.Multiply<TRealNumber>(b));
        a.Multiply(b).Should().Be(a.Multiply<TRealNumber>(b));
    }

    [Fact]
    public void Operator_is_a_linear_map_in_vector_space_that_can_be_seen_as_operator_acting_on_ket_components()
    {
        var @operator = Operator.M<TRealNumber>(new[,] {
            { 146, 172 },
            { 694, 828 }
        });

        var ket = Ket.V<TRealNumber>([23.0, 31.0]);

        var result = @operator.Act<TRealNumber>(ket);

        using var _ = new AssertionScope();

        result.Should().Be(Ket.V<TRealNumber>([8690.0, 41630.0]));
        (@operator * ket).Should().Be(result);
        @operator.Act(ket).Should().Be(@operator.Act<TRealNumber>(ket));
    }

    [Fact]
    public void Operator_is_a_linear_map_in_vector_space_that_can_be_seen_as_operator_acting_on_bra_components()
    {
        var @operator = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 }
        });

        var bra = Bra.U<TRealNumber>([23.0, 31.0]);

        var result = bra.Act<TRealNumber>(@operator);

        using var _ = new AssertionScope();

        result.Should().Be(Bra.U<TRealNumber>([116.0, 564.0]));
        (bra * @operator).Should().Be(result);
        bra.Act(@operator).Should().Be(bra.Act<TRealNumber>(@operator));
    }

    [Fact]
    public void Transposing_a_operator_flips_the_rows_and_columns()
    {
        var @operator = Operator.M<TRealNumber>(new[,] {
            { 1, 2 },
            { 3, 4 }
        });

        var transposed = @operator.Transpose();

        using var _ = new AssertionScope();

        transposed.Should().Be(Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 2, 4 } }));
        @operator.Transpose().Should().Be(transposed);
    }

    [Fact]
    public void Example_of_transposing_a_square_operator()
    {
        var @operator = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 }
        });

        var transposed = @operator.Transpose();

        transposed.Should().Be(Operator.M<TRealNumber>(new[,] {
            { 1, 7 },
            { 3, 13 }
        }));
    }

    [Fact]
    public void Another_example_of_operator_multiplication()
    {
        var a = Operator.M<TRealNumber>(new[,] {
            { 3, 0, 5 },
            { 1, 4, 0 },
            { 4, 0, 4 }
        });

        var b = Operator.M<TRealNumber>(new[,] {
            { 5, 2, 6 },
            { 0, 4, 2 },
            { 7, 2, 0 }
        });

        var product = a.Multiply<TRealNumber>(b);

        product.Should().Be(Operator.M<TRealNumber>(new[,] {
            { 50, 16, 18 },
            { 5, 18, 14 },
            { 48, 16, 24 }
        }));
    }

    [Fact]
    public void Another_example_of_identity_operator()
    {
        var identity = Operator.M<TRealNumber>(new[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 }
        });

        using var _ = new AssertionScope();

        Operator.Identity<TRealNumber>(3).Should().Be(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Multiplying_operator_by_identity_operator_does_not_change_the_operator()
    {
        var @operator = Operator.M<TRealNumber>(new[,] {
            { 1, 3, 7 },
            { 7, 13, 23 },
            { 31, 41, 47 }
        });

        var identity = Operator.M<TRealNumber>(new[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 }
        });

        var product = @operator.Multiply<TRealNumber>(identity);

        product.Should().Be(@operator);
    }

    [Fact]
    public void Operator_product_is_the_result_of_multiplying_rows_by_columns()
    {
        var a = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = Operator.M<TRealNumber>(new[,] {
            { 23, 31 },
            { 41, 47 } });

        var product = a.Multiply(b);

        using var _ = new AssertionScope();

        product.Should().Be(Operator.M<TRealNumber>(new[,]{
            { 146, 172 },
            { 694, 828 }
        }));
        (a * b).Should().Be(a.Multiply(b));
    }

    [Fact]
    public void On_identity_operator_diagonal_entries_has_value_one_and_everything_else_is_zeroes()
    {
        var identity = Operator.M<TRealNumber>(new[,] {
            { 1, 0 },
            { 0, 1 } });

        using var _ = new AssertionScope();

        Operator.Identity<TRealNumber>(2).Should().Be(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Operator_can_be_rounded_to_identity_if_it_is_close_to_identity()
    {
        var almostIdentity = Operator.M<TRealNumber>(new[,] {
            { RealNumber<TRealNumber>.R(0.9999999), RealNumber<TRealNumber>.R(0.0000001), RealNumber<TRealNumber>.R(0.0000001) },
            { RealNumber<TRealNumber>.R(0.0000001), RealNumber<TRealNumber>.R(0.9999999), RealNumber<TRealNumber>.R(0.0000001) },
            { RealNumber<TRealNumber>.R(0.0000001), RealNumber<TRealNumber>.R(0.0000001), RealNumber<TRealNumber>.R(0.9999999) } });

        using var _ = new AssertionScope();

        almostIdentity.Round().Should().Be(Operator.M<TRealNumber>(new[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 } }));
        almostIdentity.Round().Should().Be(almostIdentity.Round());
    }

    [Fact]
    public void Operator_cannot_be_rounded_to_identity_if_it_is_not_close_enough_to_identity()
    {
        var almostIdentity = Operator.M<TRealNumber>(new[,] {
            { RealNumber<TRealNumber>.R(0.999999), RealNumber<TRealNumber>.R(0.0000001), RealNumber<TRealNumber>.R(0.0000001) },
            { RealNumber<TRealNumber>.R(0.0000001), RealNumber<TRealNumber>.R(0.999999), RealNumber<TRealNumber>.R(0.0000001) },
            { RealNumber<TRealNumber>.R(0.0000001), RealNumber<TRealNumber>.R(0.0000001), RealNumber<TRealNumber>.R(0.999999) } });

        almostIdentity.Round().Should().NotBeEquivalentTo(Operator.M<TRealNumber>(new[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 } }));
    }

    [Fact]
    public void Algebra_of_operators_acts_on_vectors_to_yield_new_vectors()
    {

        var @operator = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 }
        });

        var vector = Ket.V<TRealNumber>([23.0, 31.0]);

        var resultOfAction = @operator.Act(vector);

        using var _ = new AssertionScope();

        resultOfAction.Should().Be(Ket.V<TRealNumber>([116.0, 564.0]));
        (@operator * vector).Should().Be(resultOfAction);
        @operator.Act(vector).Should().Be(@operator.Act(vector));
    }

    [Fact]
    public void Algebra_of_operators_acts_on_row_vectors_to_yield_new_vectors()
    {
        var @operator = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 }
        });

        var vector = Bra.U<TRealNumber>([23.0, 31.0]);

        var resultOfAction = vector.Act(@operator);

        using var _ = new AssertionScope();

        resultOfAction.Should().Be(Bra.U<TRealNumber>([116.0, 564.0]));
        (vector * @operator).Should().Be(resultOfAction);
    }

    [Fact]
    public void Tensor_product_of_operator_contains_combinations_of_scalar_products_of_all_elements_of_both_operator()
    {
        var a = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = Operator.M<TRealNumber>(new[,] {
            { 23, 31 },
            { 41, 47 } });

        var tensorProduct = a.TensorProduct<TRealNumber>(b);

        using var _ = new AssertionScope();

        tensorProduct.Should().Be(Operator.M<TRealNumber>(new[,] {
            { 23, 31, 69, 93 },
            { 41, 47, 123, 141 },
            { 161, 217, 299, 403 },
            { 287, 329, 533, 611 }
        }));
        a.TensorProduct(b).Should().Be(a.TensorProduct<TRealNumber>(b));
    }

    [Fact]
    public void Another_example_of_tensor_product()
    {
        var a = Operator.M<TRealNumber>(new[,] {
            { 3, 5, 0 },
            { 0, 12, 6 },
            { 2, 4, 9 }
        });

        var b = Operator.M<TRealNumber>(new[,] {
            { 1, 3, 5 },
            { 10, 6, 2 },
            { 0, 1, 2 }
        });

        var tensorProduct = a.TensorProduct<TRealNumber>(b);

        tensorProduct.Should().Be(Operator.M<TRealNumber>(new[,] {
            { 3, 9, 15, 5, 15, 25, 0, 0, 0 },
            { 30, 18, 6, 50, 30, 10, 0, 0, 0 },
            { 0, 3, 6, 0, 5, 10, 0, 0, 0 },
            { 0, 0, 0, 12, 36, 60, 6, 18, 30 },
            { 0, 0, 0, 120, 72, 24, 60, 36, 12 },
            { 0, 0, 0, 0, 12, 24, 0, 6, 12 },
            { 2, 6, 10, 4, 12, 20, 9, 27, 45 },
            { 20, 12, 4, 40, 24, 8, 90, 54, 18 },
            { 0, 2, 4, 0, 4, 8, 0, 9, 18 }
        }));
    }

    [Fact]
    public void Third_example_of_tensor_product()
    {

        var a = Operator.M<TRealNumber>(new[,] {
            { 1, 2 },
            { 3, 4 }
        });

        var b = Operator.M<TRealNumber>(new[,] {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        });

        var tensorProduct = a.TensorProduct<TRealNumber>(b);

        tensorProduct.Should().Be(Operator.M<TRealNumber>(new[,] {
            { 1, 2, 3, 2, 4, 6 },
            { 4, 5, 6, 8, 10, 12 },
            { 7, 8, 9, 14, 16, 18 },
            { 3, 6, 9, 4, 8, 12 },
            { 12, 15, 18, 16, 20, 24 },
            { 21, 24, 27, 28, 32, 36 }
        }));
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = Operator.M<TRealNumber>(new[,] {
            { 1, 3 },
            { 7, 13 }
        });

        var b = Operator.M<TRealNumber>(new[,] {
            { 23, 31 },
            { 41, 47 }
        });

        var c = Operator.M<TRealNumber>(new[,] {
            { 59, 67 },
            { 73, 83 }
        });

        a.TensorProduct<TRealNumber>(b.TensorProduct<TRealNumber>(c)).Should().Be(a.TensorProduct<TRealNumber>(b).TensorProduct<TRealNumber>(c));
    }
}