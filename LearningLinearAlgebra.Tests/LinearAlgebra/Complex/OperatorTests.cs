using System.Numerics;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using LearningLinearAlgebra.ComplexVectorSpace;
using LearningLinearAlgebra.Numbers;
using LearningLinearAlgebra.Tests.Helpers;

namespace LearningLinearAlgebra.Tests.LinearAlgebra.Complex;

public class SinglePrecisionOperatorTests : OperatorTests<float>;
public class DoublePrecisionOperatorTests : OperatorTests<double>;

public abstract class OperatorTests<TRealNumber> 
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    static OperatorTests() => Formatters<TRealNumber>.Register();

    [Fact]
    public void Dimension_of_the_operator_is_the_dimension_of_the_vector_space_it_is_acting_on()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var dimension = @operator.Dimension<TRealNumber>();

        using var _ = new AssertionScope();

        dimension.Should().Be(2);
        @operator.Dimension().Should().Be(@operator.Dimension<TRealNumber>());
    }

    [Fact]
    public void Sum_of_two_operators_is_calculated_as_sum_of_the_components()
    {
        var a = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var sum = a.Add<TRealNumber>(b);

        using var _ = new AssertionScope();

        sum.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (24, 31), (34, 42) },
            { (48, 54), (60, 72) } }));
        (a + b).Should().Be(a.Add<TRealNumber>(b));
        a.Add(b).Should().Be(a.Add<TRealNumber>(b));
    }

    [Fact]
    public void Sum_of_complex_operators_is_commutative()
    {
        var a = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        (b + a).Should().Be(a + b);
    }

    [Fact]
    public void Sum_of_complex_operators_is_associative()
    {
        var a = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var c = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (59, 61), (67, 71) },
            { (73, 79), (83, 89) } });


        (a + (b + c)).Should().Be(a + b + c);
    }

    [Fact]
    public void Sum_of_operator_and_its_the_inverse_is_zero()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var zero = Operator.Zero<TRealNumber>(2);

        (@operator + -@operator).Should().Be(zero);
    }

    [Fact]
    public void Difference_of_two_operators_is_calculated_as_difference_of_the_components()
    {
        var a = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var difference = a.Subtract<TRealNumber>(b);

        using var _ = new AssertionScope();

        difference.Should().Be(
            Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
                { (-22, -27), (-28, -32) },
                { (-34, -32), (-34, -34) }
            }
        ));
        (a - b).Should().Be(a.Subtract<TRealNumber>(b));
        a.Subtract(b).Should().Be(a.Subtract<TRealNumber>(b));
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var zero = Operator.Zero<TRealNumber>(2);

        using var _ = new AssertionScope();

        (@operator + zero).Should().Be(@operator);
        (zero + @operator).Should().Be(@operator);
    }

    [Fact]
    public void Zero_operator_has_only_zero_entries()
    {
        var zero = Operator.Zero<TRealNumber>(2);

        zero.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (0, 0), (0, 0) },
            { (0, 0), (0, 0) } }));
    }

    [Fact]
    public void When_multiplying_an_operator_by_scalar_then_each_element_of_the_operator_is_multiplied_by_the_scalar()
    {
        var scalar = ComplexNumber<TRealNumber>.C(5, 7);
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var multiplied = scalar.Multiply<TRealNumber>(@operator);

        using var _ = new AssertionScope();

        multiplied.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (-9, 17), (-20, 46) },
            { (-42, 104), (-68, 186) }
        }));
        (scalar * @operator).Should().Be(scalar.Multiply<TRealNumber>(@operator));
        scalar.Multiply(@operator).Should().Be(scalar.Multiply<TRealNumber>(@operator));
    }
    [Fact]
    public void When_multiplying_a_operator_by_scalar_then_each_element_of_the_operator_is_multiplied_by_the_scalar()
    {
        var scalar = ComplexNumber<TRealNumber>.C(5, 7);
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var multiplied = @operator.Multiply(scalar);

        using var _ = new AssertionScope();

        multiplied.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (-9, 17), (-20, 46) },
            { (-42, 104), (-68, 186) }
        }));
        (scalar * @operator).Should().Be(@operator.Multiply(scalar));
        @operator.Multiply(scalar).Should().Be(@operator.Multiply(scalar));
    }

    [Fact]
    public void Scalar_multiplication_respects_complex_multiplication()
    {
        var scalarA = ComplexNumber<TRealNumber>.C(3, 5);
        var scalarB = ComplexNumber<TRealNumber>.C(7, 11);

        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        (scalarA * scalarB * @operator).Should().Be(scalarA * (scalarB * @operator));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = ComplexNumber<TRealNumber>.C(3, 5);

        var operatorA = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var operatorB = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) }
        });

        (scalar * operatorA + scalar * operatorB).Should().Be(scalar * (operatorA + operatorB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_complex_addition()
    {
        var scalarA = ComplexNumber<TRealNumber>.C(3, 5);
        var scalarB = ComplexNumber<TRealNumber>.C(7, 11);

        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        (scalarA * @operator + scalarB * @operator).Should().Be((scalarA + scalarB) * @operator);
    }

    [Fact]
    public void Multiplying_operators_using_the_operator_multiplication()
    {
        var a = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var product = a.Multiply<TRealNumber>(b);

        using var _ = new AssertionScope();

        product.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (-127, 409), (-167, 493) },
            { (-442, 1794), (-586, 2182) }
        }));
        (a * b).Should().Be(a.Multiply<TRealNumber>(b));
        a.Multiply(b).Should().Be(a.Multiply<TRealNumber>(b));
    }

    [Fact]
    public void Operator_is_a_linear_map_in_vector_space_that_can_be_seen_as_operator_acting_on_ket_components()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var ket = Ket.V<TRealNumber>([(23, 29), (31, 37)]);

        var result = @operator.Act<TRealNumber>(ket);

        using var _ = new AssertionScope();

        result.Should().Be(Ket.V<TRealNumber>([(-127, 341), (-458, 1526)]));
        (@operator * ket).Should().Be(result);
        @operator.Act(ket).Should().Be(@operator.Act<TRealNumber>(ket));
    }

    [Fact]
    public void Operator_is_a_linear_map_in_vector_space_that_can_be_seen_as_operator_acting_on_bra_components()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var bra = Bra.U<TRealNumber>([(23, 29), (31, 37)]);

        var result = bra.Act<TRealNumber>(@operator);

        using var _ = new AssertionScope();

        result.Should().Be(Bra.U<TRealNumber>([(-127, 341), (-458, 1526)]));
        (bra * @operator).Should().Be(result);
        bra.Act(@operator).Should().Be(bra.Act<TRealNumber>(@operator));
    }

    [Fact]
    public void Transposing_a_operator_flips_the_rows_and_columns()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 1), (2, 2) },
            { (3, 3), (4, 4) }
        });

        var transposed = @operator.Transpose();

        using var _ = new AssertionScope();

        transposed.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 1), (3, 3) },
            { (2, 2), (4, 4) } }));
        @operator.Transpose().Should().Be(transposed);
    }

    [Fact]
    public void Example_of_transposing_a_square_operator()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var transposed = @operator.Transpose();

        transposed.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (7, 11) },
            { (3, 5), (13, 19) }
        }));
    }

    [Fact]
    public void Conjucate_of_a_operator_is_a_operator_where_each_element_is_a_complex_conjucate_of_the_original_operator()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var conjucate = @operator.Conjucate<TRealNumber>();

        using var _ = new AssertionScope();

        conjucate.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, -2), (3, -5) },
            { (7, -11), (13, -19) }
        }));
        @operator.Conjucate().Should().Be(conjucate);
    }

    [Fact]
    public void Conjucate_of_a_vector_is_a_vector_where_each_element_is_a_complex_conjucate_of_the_original_vector()
    {
        var vector = Ket.V<TRealNumber>([(1, 2), (3, 5), (7, 11), (13, 19)]);

        var conjucate = vector.Conjucate();

        conjucate.Should().Be(Ket.V<TRealNumber>([(1, -2), (3, -5), (7, -11), (13, -19)]));
    }

    [Fact]
    public void Adjoint_is_the_combination_of_transpose_and_conjucate()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var adjointed = @operator.Adjoint<TRealNumber>();

        using var _ = new AssertionScope();

        adjointed.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, -2), (7, -11) },
            { (3, -5), (13, -19) }
        }));
        @operator.Adjoint().Should().Be(adjointed);
    }

    [Fact]
    public void Another_example_of_operator_multiplication()
    {
        var a = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (3, 2), (0, 0), (5, -6) },
            { (1, 0), (4, 2), (0, 1) },
            { (4, -1), (0, 0), (4, 0) }
        });

        var b = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (5, 0), (2, -1), (6, -4) },
            { (0, 0), (4, 5), (2, 0) },
            { (7, -4), (2, 7), (0, 0) }
        });

        var product = a.Multiply<TRealNumber>(b);

        product.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (26, -52), (60, 24), (26, 0) },
            { (9, 7), (1, 29), (14, 0) },
            { (48, -21), (15, 22), (20, -22) }
        }));
    }

    [Fact]
    public void Another_example_of_identity_operator()
    {
        var identity = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0), (0, 0) },
            { (0, 0), (1, 0), (0, 0) },
            { (0, 0), (0, 0), (1, 0) }
        });

        using var _ = new AssertionScope();

        Operator.Identity<TRealNumber>(3).Should().Be(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Multiplying_operator_by_identity_operator_does_not_change_the_operator()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5), (7, 11) },
            { (7, 11), (13, 19), (23, 29) },
            { (31, 37), (41, 43), (47, 53) }
        });

        var identity = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0), (0, 0) },
            { (0, 0), (1, 0), (0, 0) },
            { (0, 0), (0, 0), (1, 0) }
        });

        var product = @operator.Multiply<TRealNumber>(identity);

        product.Should().Be(@operator);
    }

    [Fact]
    public void Operator_product_is_the_result_of_multiplying_rows_by_columns()
    {
        var a = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var product = a.Multiply(b);

        using var _ = new AssertionScope();

        product.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (-127, 409), (-167, 493) },
            { (-442, 1794), (-586, 2182) }
        }));
        (a * b).Should().Be(a.Multiply(b));
    }

    [Fact]
    public void On_identity_operator_diagonal_entries_has_value_one_and_everything_else_is_zeroes()
    {
        var identity = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (1, 0) } });

        using var _ = new AssertionScope();

        Operator.Identity<TRealNumber>(2).Should().Be(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Operator_can_be_rounded_to_identity_if_it_is_close_to_identity()
    {
        var almostIdentity = Operator.M<TRealNumber>(new[,] {
            { ComplexNumber<TRealNumber>.C(0.9999999f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f) },
            { ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.9999999f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f) },
            { ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.9999999f, 0.0000001f) } });

        using var _ = new AssertionScope();

        almostIdentity.Round().Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 } }));
        almostIdentity.Round().Should().Be(almostIdentity.Round());
    }

    [Fact]
    public void Operator_cannot_be_rounded_to_identity_if_it_is_not_close_enough_to_identity()
    {
        var almostIdentity = Operator.M<TRealNumber>(new[,] {
            { ComplexNumber<TRealNumber>.C(0.999999f, 0.000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f) },
            { ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.999999f, 0.000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f) },
            { ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.999999f, 0.000001f) } });

        almostIdentity.Round().Should().NotBeEquivalentTo(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 } }));
    }

    [Fact]
    public void Algebra_of_operators_acts_on_vectors_to_yield_new_vectors()
    {

        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var vector = Ket.V<TRealNumber>([(23, 29), (31, 37)]);

        var resultOfAction = @operator.Act(vector);

        using var _ = new AssertionScope();

        resultOfAction.Should().Be(Ket.V<TRealNumber>([(-127, 341), (-458, 1526)]));
        (@operator * vector).Should().Be(resultOfAction);
        @operator.Act(vector).Should().Be(@operator.Act(vector));
    }

    [Fact]
    public void Algebra_of_operators_acts_on_row_vectors_to_yield_new_vectors()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var vector = Bra.U<TRealNumber>([(23, 29), (31, 37)]);

        var resultOfAction = vector.Act(@operator);

        using var _ = new AssertionScope();

        resultOfAction.Should().Be(Bra.U<TRealNumber>([(-127, 341), (-458, 1526)]));
        (vector * @operator).Should().Be(resultOfAction);
    }

    [Fact]
    public void Operator_is_hermitian_if_adjoint_of_operator_does_not_change_the_operator()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (3, 4), (5, 6) },
            { (3, -4), (7, 0), (10, 0) },
            { (5, -6), (10, 0), (9, 0) }
        });

        using var _ = new AssertionScope();

        @operator.IsHermitian<TRealNumber>().Should().BeTrue();
        @operator.IsHermitian().Should().BeTrue();
    }

    [Fact]
    public void Another_example_of_hermitian_operator()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (5, 0), (4, 5), (6, -16) },
            { (4, -5), (13, 0), (7, 0) },
            { (6, 16), (7, 0),(-2.1f, 0) }
        });

        @operator.IsHermitian<TRealNumber>().Should().BeTrue();
    }

    [Fact]
    public void Third_example_of_hermitian_operator()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (7, 0), (6, 5) },
            { (6, -5), (-3, 0) }
    });

        @operator.IsHermitian<TRealNumber>().Should().BeTrue();
    }

    [Fact]
    public void Fourth_example_of_hermitian_operator()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (2, 0), (3, 0) },
            { (2, 0), (2, 0), (3, 0) },
            { (3, 0), (3, 0), (9, 0) }
        });

        @operator.IsHermitian<TRealNumber>().Should().BeTrue();
    }


    [Fact]
    public void Example_of_nonhermitian_operator()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (7, 0), (6, 5) },
            { (6, 5), (3, 0) } });

        @operator.IsHermitian<TRealNumber>().Should().BeFalse();
    }

    [Fact]
    public void Inner_product_when_applying_hermitian_operator()
    // If A is hermitian @operator then inner product of A*Ket.LearningLinearAlgebra.ComplexVectorSpace.Bra.U<TRealNumber> and Ket.LearningLinearAlgebra.ComplexVectorSpace.Bra.U<TRealNumber>' is equal to inner product of Ket.LearningLinearAlgebra.ComplexVectorSpace.Bra.U<TRealNumber> and A*Ket.LearningLinearAlgebra.ComplexVectorSpace.Bra.U<TRealNumber>
    {
        var a = Ket.V<TRealNumber>([(1, 2), (3, 5)]);
        var b = Ket.V<TRealNumber>([(7, 11), (13, 19)]);

        var hermitian = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (7, 0), (6, 5) },
            { (6, -5), (-3, 0) }
        });

        a.InnerProduct(hermitian * b).Should().Be((hermitian * a).InnerProduct(b));
    }

    [Fact]
    public void Operator_is_unitary_if_product_of_operator_and_its_adjoint_is_equal_to_product_of_adjoint_and_operator_is_equal_to_identity_operator()
    {
        var a = RealNumber<TRealNumber>.R(10);

        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (TRealNumber.Cos(a), 0), (-TRealNumber.Sin(a), 0), (0, 0) },
            { (TRealNumber.Sin(a), 0), (TRealNumber.Cos(a), 0), (0, 0) },
            { (0, 0), (0, 0), (1, 0) }
        });

        using var _ = new AssertionScope();

        @operator.IsUnitary<TRealNumber>().Should().BeTrue();
        @operator.IsUnitary().Should().BeTrue();
    }

    [Fact]
    public void Another_example_of_unitary_operator()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1 / Math.Sqrt(2), 0), (1 / Math.Sqrt(2), 0) },
            { (0, 1 / Math.Sqrt(2)), (0, -1 / Math.Sqrt(2)) }
        });

        @operator.IsUnitary<TRealNumber>().Should().BeTrue();
    }

    [Fact]
    public void Third_example_of_unitary_operator()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (0, 1) } });

        @operator.IsUnitary<TRealNumber>().Should().BeTrue();
    }

    [Fact]
    public void Example_of_nonunitary_operator()
    {
        var @operator = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (1, 0) },
            { (0, 1), (0, -1) }
        });

        using var _ = new AssertionScope();

        @operator.IsUnitary<TRealNumber>().Should().BeFalse();
        @operator.IsUnitary().Should().BeFalse();
    }

    [Fact]
    public void Product_of_unitary_operators_is_also_unitary_operator()
    {
        var unitary = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1 / Math.Sqrt(2), 0), (1 / Math.Sqrt(2), 0) },
            { (0, 1 / Math.Sqrt(2)), (0, -1 / Math.Sqrt(2)) }
        });

        var anotherUnitary = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (0, 1) }
        });

        unitary.IsUnitary<TRealNumber>().Should().BeTrue();
        anotherUnitary.IsUnitary<TRealNumber>().Should().BeTrue();
        (unitary * anotherUnitary).IsUnitary<TRealNumber>().Should().BeTrue();
    }

    [Fact]
    public void Unitary_operators_preserve_inner_products()
    {
        var a = Ket.V<TRealNumber>([(1, 2), (3, 5)]);
        var b = Ket.V<TRealNumber>([(7, 11), (13, 19)]);

        var unitary = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (0, 1) }
        });

        (unitary * a).InnerProduct(unitary * b).Should().Be(a.InnerProduct(b));
    }

    [Fact]
    public void Unitary_operators_preserve_distance()
    {
        var a = Ket.V<TRealNumber>([(1, 2), (3, 5)]);
        var b = Ket.V<TRealNumber>([(7, 11), (13, 19)]);

        var unitary = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (0, 1) }
        });

        (unitary * a).Distance(unitary * b).Should().Be(a.Distance(b));
    }

    [Fact]
    public void Multiplying_unitary_operator_by_its_adjoint_produces_an_identity_operator()
    {
        var unitary = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1 / Math.Sqrt(2), 0), (1 / Math.Sqrt(2), 0) },
            { (0, 1 / Math.Sqrt(2)), (0, -1 / Math.Sqrt(2)) }
        });

        (unitary * unitary.Adjoint<TRealNumber>()).Round().Should().Be(Operator.Identity<TRealNumber>(2));
    }

    [Fact]
    public void Another_example_of_multiplying_unitary_operator_by_its_adjoint()
    {
        var unitary = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1 / Math.Sqrt(2), 0), (1 / Math.Sqrt(2), 0), (0, 0) },
            { (0, -1 / Math.Sqrt(2)), (0, 1 / Math.Sqrt(2)), (0, 0) },
            { (0, 0), (0, 0), (0, 1) }
        });

        var transposeOfUnitary = unitary.Adjoint<TRealNumber>();

        (unitary * transposeOfUnitary).Round().Should().Be(Operator.Identity<TRealNumber>(3));
    }

    [Fact]
    public void Commutator_of_hermitians()
    // Commutator of hermitians a and b is (a * b) - (b * a)
    {
        var a = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (0, 0), (1, 0) },
            { (1, 0), (0, 0) } });

        var b = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (0, 0), (0, -1) },
            { (0, 1), (0, 0) } });

        var commutator = a.Commutator<TRealNumber>(b);

        using var _ = new AssertionScope();

        commutator.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (0, 2), (0, 0) },
            { (0, 0), (0, -2) } }));
        a.Commutator(b).Should().Be(a.Commutator<TRealNumber>(b));
    }

    [Fact]
    public void Commutator_of_hermitians_is_zero_if_hermitians_are_commutable()
    {

        var a = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (1, 0) } });

        var b = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (0, 0), (1, 0) },
            { (1, 0), (0, 0) } });

        var commutator = a.Commutator<TRealNumber>(b);

        commutator.Should().Be(Operator.Zero<TRealNumber>(2));
    }

    [Fact]
    public void Tensor_product_of_operator_contains_combinations_of_scalar_products_of_all_elements_of_both_operator()
    {
        var a = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var tensorProduct = a.TensorProduct<TRealNumber>(b);

        using var _ = new AssertionScope();

        tensorProduct.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (-35, 75), (-43, 99), (-76, 202), (-92, 266) },
            { (-45, 125), (-59, 147), (-92, 334), (-124, 394) },
            { (-158, 456), (-190, 600), (-252, 814), (-300, 1070) },
            { (-186, 752), (-254, 888), (-284, 1338), (-396, 1582) }
        }));
        a.TensorProduct(b).Should().Be(a.TensorProduct<TRealNumber>(b));
    }

    [Fact]
    public void Another_example_of_tensor_product()
    {
        var a = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (3, 2), (5, -1), (0, 2) },
            { (0, 0), (12, 0), (6, -3) },
            { (2, 0), (4, 4), (9, 3) }
        });

        var b = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (3, 4), (5, -7) },
            { (10, 2), (6, 0), (2, 5) },
            { (0, 0), (1, 0), (2, 9) }
        });

        var tensorProduct = a.TensorProduct<TRealNumber>(b);

        tensorProduct.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (3, 2),(1, 18),(29, -11),(5, -1),(19, 17),(18, -40),(0, 2),(-8, 6),(14, 10) },
            { (26, 26),(18, 12),(-4, 19),(52, 0),(30, -6),(15, 23),(-4, 20),(0, 12),(-10, 4) },
            { (0, 0),(3, 2),(-12, 31),(0, 0),(5, -1),(19, 43),(0, 0),(0, 2),(-18, 4) },
            { (0, 0),(0, 0),(0, 0),(12, 0),(36, 48),(60, -84),(6, -3),(30, 15),(9, -57) },
            { (0, 0),(0, 0),(0, 0),(120, 24),(72, 0),(24, 60),(66, -18),(36, -18),(27, 24) },
            { (0, 0),(0, 0),(0, 0),(0, 0),(12, 0),(24, 108),(0, 0),(6, -3),(39, 48) },
            { (2, 0),(6, 8),(10, -14),(4, 4),(-4, 28),(48, -8),(9, 3),(15, 45),(66, -48) },
            { (20, 4),(12, 0),(4, 10),(32, 48),(24, 24),(-12, 28),(84, 48),(54, 18),(3, 51) },
            { (0, 0),(2, 0),(4, 18),(0, 0),(4, 4),(-28, 44),(0, 0),(9, 3),(-9, 87) }
        }));
    }

    [Fact]
    public void Third_example_of_tensor_product()
    {

        var a = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 1), (2, 2) },
            { (3, 3), (4, 4) }
        });

        var b = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 1), (2, 2), (3, 3) },
            { (4, 4), (5, 5), (6, 6) },
            { (7, 7), (8, 8), (9, 9) }
        });

        var tensorProduct = a.TensorProduct<TRealNumber>(b);

        tensorProduct.Should().Be(Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            {(0, 2), (0, 4), (0, 6), (0, 4), (0, 8), (0, 12) },
            {(0, 8), (0, 10), (0, 12),(0, 16),(0, 20),(0, 24) },
            {(0, 14),(0, 16) , (0, 18),(0, 28),(0, 32),(0, 36) },
            {(0, 6),(0, 12),(0, 18),(0, 8),(0, 16),(0, 24) },
            {(0, 24),(0, 30),(0, 36),(0, 32),(0, 40),(0, 48) },
            {(0, 42),(0, 48),(0, 54),(0, 56),(0, 64),(0, 72) }
        }));
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var b = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) }
        });

        var c = Operator.M<TRealNumber>(new ComplexNumber<TRealNumber>[,] {
            { (59, 61), (67, 71) },
            { (73, 79), (83, 89) }
        });

        a.TensorProduct<TRealNumber>(b.TensorProduct<TRealNumber>(c)).Should().Be(a.TensorProduct<TRealNumber>(b).TensorProduct<TRealNumber>(c));
    }
}