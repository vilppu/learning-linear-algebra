using System.Numerics;
using LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace;
using LearningLinearAlgebra.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace LearningLinearAlgebra.Tests.LinearAlgebra;

public class SinglePrecisionCpuOperatorTests : OperatorTests<Operator<float>, Bra<float>, Ket<float>, float> { }
public class DoublePrecisionCpuOperatorTests : OperatorTests<Operator<double>, Bra<double>, Ket<double>, double> { }

public abstract class OperatorTests<TOperator, TBra, TKet, TRealNumber>
    where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
    where TBra : IBra<TBra, TKet, TRealNumber>
    where TKet : IKet<TKet, TBra, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    [Fact]
    public void Dimension_of_the_operator_is_the_dimension_of_the_vector_space_it_is_acting_on()
    {
        var @operator = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var dimension = TOperator.Dimension(@operator);

        using var _ = new AssertionScope();

        dimension.Should().Be(2);
        @operator.Dimension().Should().Be(TOperator.Dimension(@operator));
    }

    [Fact]
    public void On_identity_operator_diagonal_entries_has_value_one_and_everytinhg_else_is_zeroes()
    {
        var identity = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (1, 0) } });

        using var _ = new AssertionScope();

        TOperator.Identity(2).Should().BeEquivalentTo(identity);
    }

    [Fact]
    public void Zero_operator_has_only_zero_entries()
    {
        var zero = TOperator.Zero(2);

        zero.Should().BeEquivalentTo(TOperator.M(new ComplexNumber<TRealNumber>[,] {
          { (0, 0), (0,0) },
          { (0, 0), (0,0) } }));
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var @operator = TOperator.M(new ComplexNumber<TRealNumber>[,] {
          { (1, 2), (3, 5) },
          { (7, 11), (13, 19) } });

        var zero = TOperator.Zero(2);

        using var _ = new AssertionScope();

        (@operator + zero).Should().BeEquivalentTo(@operator);
        (zero + @operator).Should().BeEquivalentTo(@operator);
    }

    [Fact]
    public void Sum_of_two_operators_is_calculated_as_sum_of_the_components()
    {
        var a = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var sum = TOperator.Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (24, 31), (34, 42) },
            { (48, 54), (60, 72) } }));
        (a + b).Should().BeEquivalentTo(TOperator.Add(a, b));
        a.Add(b).Should().BeEquivalentTo(TOperator.Add(a, b));
    }

    [Fact]
    public void Difference_of_two_operators_is_calculated_as_difference_of_the_components()
    {
        var a = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var difference = TOperator.Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(
            TOperator.M(new ComplexNumber<TRealNumber>[,] {
                { (-22, -27), (-28, -32) },
                { (-34, -32), (-34, -34) }
            }
        ));
        (a - b).Should().BeEquivalentTo(TOperator.Subtract(a, b));
        a.Subtract(b).Should().BeEquivalentTo(TOperator.Subtract(a, b));
    }

    [Fact]
    public void Sum_of_operator_and_its_the_inverse_is_zero()
    {
        var @operator = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var zero = TOperator.Zero(2);

        (@operator + TOperator.AdditiveInverse(@operator)).Should().BeEquivalentTo(TOperator.Zero(2));
        (@operator + -@operator).Should().BeEquivalentTo(TOperator.Zero(2));
    }

    [Fact]
    public void When_multiplying_an_operator_by_scalar_then_each_element_of_the_operator_is_multiplied_by_the_scalar()
    {
        var scalar = ComplexNumber<TRealNumber>.C(5, 7);
        var @operator = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var multiplied = TOperator.Multiply(scalar, @operator);

        using var _ = new AssertionScope();

        multiplied.Should().BeEquivalentTo(TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (-9, 17), (-20, 46) },
            { (-42, 104), (-68, 186) }
        }));
        (scalar * @operator).Should().BeEquivalentTo(TOperator.Multiply(scalar, @operator));
        scalar.Multiply(@operator).Should().BeEquivalentTo(TOperator.Multiply(scalar, @operator));
    }

    [Fact]
    public void Multiplying_operators_using_the_matrix_multiplication()
    {
        var a = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var product = TOperator.Multiply(a, b);

        using var _ = new AssertionScope();

        product.Should().BeEquivalentTo(TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (-127, 409), (-167, 493) },
            { (-442, 1794), (-586, 2182) }
        }));
        (a * b).Should().BeEquivalentTo(TOperator.Multiply(a, b));
        a.Multiply(b).Should().BeEquivalentTo(TOperator.Multiply(a, b));
    }

    [Fact]
    public void Operator_is_a_linear_map_in_vector_space_that_can_be_seen_as_operator_acting_on_ket_components()
    {
        var @operator = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var ket = TKet.V([(23, 29), (31, 37)]);

        var result = TOperator.Act(@operator, ket);

        using var _ = new AssertionScope();

        result.Should().BeEquivalentTo(TKet.V([(-127, 341), (-458, 1526)]));
        (@operator * ket).Should().BeEquivalentTo(result);
        @operator.Act(ket).Should().BeEquivalentTo(TOperator.Act(@operator, ket));
    }

    [Fact]
    public void Operator_is_a_linear_map_in_vector_space_that_can_be_seen_as_operator_acting_on_bra_components()
    {
        var @operator = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var bra = TBra.U([(23, 29), (31, 37)]);

        var result = TOperator.Act(bra, @operator);

        using var _ = new AssertionScope();

        result.Should().BeEquivalentTo(TKet.V([(-127, 341), (-458, 1526)]));
        (bra * @operator).Should().BeEquivalentTo(result);
        bra.Act(@operator).Should().BeEquivalentTo(TOperator.Act(bra, @operator));
    }

    [Fact]
    public void Conjucate_of_a_operator_is_a_operator_where_each_element_is_a_complex_conjucate_of_the_original_operator()
    {
        var matrix = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var conjucate = TOperator.Conjucate(matrix);

        using var _ = new AssertionScope();

        conjucate.Should().BeEquivalentTo(TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, -2), (3, -5) },
            { (7, -11), (13, -19) }
        }));
        matrix.Conjucate().Should().BeEquivalentTo(conjucate);
    }

    [Fact]
    public void Conjucate_of_a_vector_is_a_vector_where_each_element_is_a_complex_conjucate_of_the_original_vector()
    {
        var vector = TKet.V([(1, 2), (3, 5), (7, 11), (13, 19)]);

        var conjucate = TKet.Conjucate(vector);

        conjucate.Should().BeEquivalentTo(TKet.V([(1, -2), (3, -5), (7, -11), (13, -19)]));
    }

    [Fact]
    public void Adjoint_is_the_combination_of_transpose_and_conjucate()
    {
        var matrix = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var adjointed = TOperator.Adjoint(matrix);

        using var _ = new AssertionScope();

        adjointed.Should().BeEquivalentTo(TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, -2), (7, -11) },
            { (3, -5), (13, -19) }
        }));
        matrix.Adjoint().Should().BeEquivalentTo(adjointed);
    }

    [Fact]
    public void Another_example_of_operator_multiplication()
    {
        var a = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (3, 2), (0, 0), (5, -6) },
            { (1, 0), (4, 2), (0, 1) },
            { (4, -1), (0, 0), (4, 0) }
        });

        var b = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (5, 0), (2, -1), (6, -4) },
            { (0, 0), (4, 5), (2, 0) },
            { (7, -4), (2, 7), (0, 0) }
        });

        var product = TOperator.Multiply(a, b);

        product.Should().BeEquivalentTo(TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (26, -52), (60, 24), (26, 0) },
            { (9, 7), (1, 29), (14, 0) },
            { (48, -21), (15, 22), (20, -22) }
        }));
    }

    [Fact]
    public void Another_example_of_identity_operator()
    {
        var identity = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0), (0, 0) },
            { (0, 0), (1, 0), (0, 0) },
            { (0, 0), (0, 0), (1, 0) }
        });

        using var _ = new AssertionScope();

        TOperator.Identity(3).Should().BeEquivalentTo(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Multiplying_operator_by_identity_operator_does_not_change_the_operator()
    {
        var matrix = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5), (7, 11) },
            { (7, 11), (13, 19), (23, 29) },
            { (31, 37), (41, 43), (47, 53) }
        });

        var identity = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0), (0, 0) },
            { (0, 0), (1, 0), (0, 0) },
            { (0, 0), (0, 0), (1, 0) }
        });

        var product = TOperator.Multiply(matrix, identity);

        product.Should().BeEquivalentTo(matrix);
    }
    
    [Fact]
    public void Matrix_is_hermitian_if_adjoint_of_operator_does_not_change_the_operator()
    {
        var matrix = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (3, 4), (5, 6) },
            { (3, -4), (7, 0), (10, 0) },
            { (5, -6), (10, 0), (9, 0) }
        });

        using var _ = new AssertionScope();

        TOperator.IsHermitian(matrix).Should().BeTrue();
        matrix.IsHermitian().Should().BeTrue();
    }

    [Fact]
    public void Another_example_of_hermitian_operator()
    {
        var matrix = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (5, 0), (4, 5), (6, -16) },
            { (4, -5), (13, 0), (7, 0) },
            { (6, 16), (7, 0),(-2.1f, 0) }
        });

        TOperator.IsHermitian(matrix).Should().BeTrue();
    }

    [Fact]
    public void Third_example_of_hermitian_operator()
    {
        var matrix = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (7, 0), (6, 5) },
            { (6, -5), (-3, 0) }
    });

        TOperator.IsHermitian(matrix).Should().BeTrue();
    }

    [Fact]
    public void Fourth_example_of_hermitian_operator()
    {
        var matrix = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (2, 0), (3, 0) },
            { (2, 0), (2, 0), (3, 0) },
            { (3, 0), (3, 0), (9, 0) }
        });

        TOperator.IsHermitian(matrix).Should().BeTrue();
    }


    [Fact]
    public void Example_of_nonhermitian_operator()
    {
        var matrix = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (7, 0), (6, 5) },
            { (6, 5), (3, 0) } });

        TOperator.IsHermitian(matrix).Should().BeFalse();
    }

    [Fact]
    public void Inner_product_when_applying_hermitian_operator()
    // If A is hermitian matrix then inner product of A*TKet.V and TKet.V' is equal to inner product of TKet.V and A*TKet.V
    {
        var a = TKet.V([(1, 2), (3, 5)]);
        var b = TKet.V([(7, 11), (13, 19)]);

        var hermitian = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (7, 0), (6, 5) },
            { (6, -5), (-3, 0) }
        });

        TKet.InnerProduct(a, hermitian * b).Should().BeEquivalentTo(TKet.InnerProduct(hermitian * a, b));
    }

    [Fact]
    public void Matrix_is_unitary_if_product_of_operator_and_its_adjoint_is_equal_to_product_of_adjoint_and_operator_is_equal_to_identity_operator()
    {
        var a = RealNumber<TRealNumber>.R(10);

        var matrix = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (TRealNumber.Cos(a), 0), (-TRealNumber.Sin(a), 0), (0, 0) },
            { (TRealNumber.Sin(a), 0), (TRealNumber.Cos(a), 0), (0, 0) },
            { (0, 0), (0, 0), (1, 0) }
        });

        using var _ = new AssertionScope();

        TOperator.IsUnitary(matrix).Should().BeTrue();
        matrix.IsUnitary().Should().BeTrue();
    }

    [Fact]
    public void Another_example_of_unitary_operator()
    {
        var matrix = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1 / Math.Sqrt(2), 0), (1 / Math.Sqrt(2), 0) },
            { (0, 1 / Math.Sqrt(2)), (0, -1 / Math.Sqrt(2)) }
        });

        TOperator.IsUnitary(matrix).Should().BeTrue();
    }

    [Fact]
    public void Third_example_of_unitary_operator()
    {
        var matrix = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (0, 1) } });

        TOperator.IsUnitary(matrix).Should().BeTrue();
    }

    [Fact]
    public void Example_of_nonunitary_operator()
    {
        var matrix = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (1, 0) },
            { (0, 1), (0, -1) }
        });

        using var _ = new AssertionScope();

        TOperator.IsUnitary(matrix).Should().BeFalse();
        matrix.IsUnitary().Should().BeFalse();
    }

    [Fact]
    public void Product_of_unitary_matrices_is_also_unitary_operator()
    {
        var unitary = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1 / Math.Sqrt(2), 0), (1 / Math.Sqrt(2), 0) },
            { (0, 1 / Math.Sqrt(2)), (0, -1 / Math.Sqrt(2)) }
        });

        var anotherUnitary = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (0, 1) }
        });

        TOperator.IsUnitary(unitary).Should().BeTrue();
        TOperator.IsUnitary(anotherUnitary).Should().BeTrue();
        TOperator.IsUnitary(unitary * anotherUnitary).Should().BeTrue();
    }

    [Fact]
    public void Unitary_matrices_preserve_inner_products()
    {
        var a = TKet.V([(1, 2), (3, 5)]);
        var b = TKet.V([(7, 11), (13, 19)]);

        var unitary = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (0, 1) }
        });

        TKet.InnerProduct(unitary * a, unitary * b).Should().BeEquivalentTo(TKet.InnerProduct(a, b));
    }

    [Fact]
    public void Unitary_matrices_preserve_distance()
    {
        var a = TKet.V([(1, 2), (3, 5)]);
        var b = TKet.V([(7, 11), (13, 19)]);

        var unitary = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (0, 1) }
        });

        TKet.Distance(unitary * a, unitary * b).Should().Be(TKet.Distance(a, b));
    }

    [Fact]
    public void Multiplying_unitary_operator_by_its_adjoint_produces_an_identity_operator()
    {
        var unitary = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1 / Math.Sqrt(2), 0), (1 / Math.Sqrt(2), 0) },
            { (0, 1 / Math.Sqrt(2)), (0, -1 / Math.Sqrt(2)) }
        });

        (unitary * TOperator.Adjoint(unitary)).Round().Should().BeEquivalentTo(TOperator.Identity(2));
    }

    [Fact]
    public void Another_example_of_multiplying_unitary_operator_by_its_adjoint()
    {
        var unitary = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1 / Math.Sqrt(2), 0), (1 / Math.Sqrt(2), 0), (0, 0) },
            { (0, -1 / Math.Sqrt(2)), (0, 1 / Math.Sqrt(2)), (0, 0) },
            { (0, 0), (0, 0), (0, 1) }
        });

        var transposeOfunitary = TOperator.Adjoint(unitary);

        (unitary * transposeOfunitary).Round().Should().BeEquivalentTo(TOperator.Identity(3));
    }

    [Fact]
    public void Commutator_of_hermitians()
    // Commutator of hermitians a and b is (a * b) - (b * a)
    {
        var a = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (0, 0), (1, 0) },
            { (1, 0), (0, 0) } });

        var b = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (0, 0), (0, -1) },
            { (0, 1), (0, 0) } });

        var commutator = TOperator.Commutator(a, b);

        using var _ = new AssertionScope();

        commutator.Should().BeEquivalentTo(TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (0, 2), (0, 0) },
            { (0, 0), (0, -2) } }));
        a.Commutator(b).Should().BeEquivalentTo(TOperator.Commutator(a, b));
    }

    [Fact]
    public void Commutator_of_hermitians_is_zero_if_hermitians_are_commutable()
    {

        var a = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (1, 0) } });

        var b = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (0, 0), (1, 0) },
            { (1, 0), (0, 0) } });

        var commutator = TOperator.Commutator(a, b);

        commutator.Should().BeEquivalentTo(TOperator.Zero(2));
    }

    [Fact]
    public void Tensor_product_of_operator_contains_combinations_of_scalar_products_of_all_elements_of_both_operator()
    {
        var a = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var tensorProduct = TOperator.TensorProduct(a, b);

        using var _ = new AssertionScope();

        tensorProduct.Should().BeEquivalentTo(TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (-35, 75), (-43, 99), (-76, 202), (-92, 266) },
            { (-45, 125), (-59, 147), (-92, 334), (-124, 394) },
            { (-158, 456), (-190, 600), (-252, 814), (-300, 1070) },
            { (-186, 752), (-254, 888), (-284, 1338), (-396, 1582) }
        }));
        a.TensorProduct(b).Should().BeEquivalentTo(TOperator.TensorProduct(a, b));
    }

    [Fact]
    public void Another_example_of_tensor_product()
    {
        var a = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (3, 2), (5, -1), (0, 2) },
            { (0, 0), (12, 0), (6, -3) },
            { (2, 0), (4, 4), (9, 3) }
        });

        var b = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (3, 4), (5, -7) },
            { (10, 2), (6, 0), (2, 5) },
            { (0, 0), (1, 0), (2, 9) }
        });

        var tensorProduct = TOperator.TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(TOperator.M(new ComplexNumber<TRealNumber>[,] {
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

        var a = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 1), (2, 2) },
            { (3, 3), (4, 4) }
        });

        var b = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 1), (2, 2), (3, 3) },
            { (4, 4), (5, 5), (6, 6) },
            { (7, 7), (8, 8), (9, 9) }
        });

        var tensorProduct = TOperator.TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(TOperator.M(new ComplexNumber<TRealNumber>[,] {
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
        var a = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var b = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) }
        });

        var c = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (59, 61), (67, 71) },
            { (73, 79), (83, 89) }
        });

        TOperator.TensorProduct(a, TOperator.TensorProduct(b, c)).Should().BeEquivalentTo(TOperator.TensorProduct(TOperator.TensorProduct(a, b), c));
    }

    [Fact]
    public void Matrix_multiplied_by_its_eigen_vector_equals_to_eigen_value_multiplied_by_eigen_vector()
    {
        var matrix = TOperator.M(new ComplexNumber<TRealNumber>[,] {
            { (4, 0), (-1, 0) },
            { (2, 0), (1, 0) } });

        var eigenVector = TKet.V([(1, 0), (1, 0)]);

        var eigenValue = RealNumber<TRealNumber>.R(3);

        (eigenValue * eigenVector).Should().BeEquivalentTo(matrix * eigenVector);
    }
}