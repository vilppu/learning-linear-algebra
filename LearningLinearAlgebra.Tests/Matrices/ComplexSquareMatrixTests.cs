using LearningLinearAlgebra.Matrices.Complex;
using LearningLinearAlgebra.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Numerics;
using Xunit;

namespace LearningLinearAlgebra.Tests.Matrices;

public class SinglePrecisionCpuComplexSquareMatrixTests : ComplexSquareMatrixTests<SquareMatrix<float>, RowVector<float>, ColumnVector<float>, float> { }
public class DoublePrecisionCpuComplexSquareMatrixTests : ComplexSquareMatrixTests<SquareMatrix<double>, RowVector<double>, ColumnVector<double>, double> { }

public abstract class ComplexSquareMatrixTests<TMatrix, TRowVector, TColumnVector, TRealNumber>
    where TMatrix : ISquareMatrix<TMatrix, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    [Fact]
    public void Sum_of_two_matrices_is_calculated_as_sum_of_the_components()
    {
        var a = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var sum = TMatrix.Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (24, 31), (34, 42) },
            { (48, 54), (60, 72) } }));
        (a + b).Should().BeEquivalentTo(TMatrix.Add(a, b));
        a.Add(b).Should().BeEquivalentTo(TMatrix.Add(a, b));
    }

    [Fact]
    public void Sum_of_complex_matrices_is_commutative()
    {
        var a = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        (b + a).Should().BeEquivalentTo(a + b);
    }

    [Fact]
    public void Sum_of_complex_matrices_is_associative()
    {
        var a = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var c = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (59, 61), (67, 71) },
            { (73, 79), (83, 89) } });


        (a + (b + c)).Should().BeEquivalentTo(a + b + c);
    }

    [Fact]
    public void Sum_of_matrix_and_its_the_inverse_is_zero()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var zero = TMatrix.Zero(2);

        (matrix + -matrix).Should().BeEquivalentTo(zero);
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var zero = TMatrix.Zero(2);

        using var _ = new AssertionScope();

        (matrix + zero).Should().BeEquivalentTo(matrix);
        (zero + matrix).Should().BeEquivalentTo(matrix);
    }

    [Fact]
    public void Difference_of_two_matrices_is_calculated_as_difference_of_the_components()
    {
        var a = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var difference = TMatrix.Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(
            TMatrix.M(new ComplexNumber<TRealNumber>[,] {
                { (-22, -27), (-28, -32) },
                { (-34, -32), (-34, -34) }
            }
        ));
        (a - b).Should().BeEquivalentTo(TMatrix.Subtract(a, b));
        a.Subtract(b).Should().BeEquivalentTo(TMatrix.Subtract(a, b));
    }

    [Fact]
    public void When_multiplying_a_matrix_by_scalar_then_each_element_of_the_matrix_is_multiplied_by_the_scalar()
    {
        var scalar = ComplexNumber<TRealNumber>.C(5, 7);
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var multiplied = TMatrix.Multiply(scalar, matrix);

        using var _ = new AssertionScope();

        multiplied.Should().BeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (-9, 17), (-20, 46) },
            { (-42, 104), (-68, 186) }
        }));
        (scalar * matrix).Should().BeEquivalentTo(TMatrix.Multiply(scalar, matrix));
        scalar.Multiply(matrix).Should().BeEquivalentTo(TMatrix.Multiply(scalar, matrix));
    }

    [Fact]
    public void Scalar_multiplication_respects_complex_multiplication()
    {
        var scalarA = ComplexNumber<TRealNumber>.C(3, 5);
        var scalarB = ComplexNumber<TRealNumber>.C(7, 11);

        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        (scalarA * scalarB * matrix).Should().BeEquivalentTo(scalarA * (scalarB * matrix));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = ComplexNumber<TRealNumber>.C(3, 5);

        var matrixA = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var matrixB = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) }
        });

        (scalar * matrixA + scalar * matrixB).Should().BeEquivalentTo(scalar * (matrixA + matrixB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_complex_addition()
    {
        var scalarA = ComplexNumber<TRealNumber>.C(3, 5);
        var scalarB = ComplexNumber<TRealNumber>.C(7, 11);

        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        (scalarA * matrix + scalarB * matrix).Should().BeEquivalentTo((scalarA + scalarB) * matrix);
    }

    [Fact]
    public void Transposing_a_matrix_flips_the_rows_and_columns()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 1), (2, 2) },
            { (3, 3), (4, 4) }
        });

        var transposed = TMatrix.Transpose(matrix);

        using var _ = new AssertionScope();

        transposed.Should().BeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 1), (3, 3) },
            { (2, 2), (4, 4) } }));
        matrix.Transpose().Should().BeEquivalentTo(transposed);
    }

    [Fact]
    public void Example_of_transposing_a_square_matrix()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var transposed = TMatrix.Transpose(matrix);

        transposed.Should().BeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (7, 11) },
            { (3, 5), (13, 19) }
        }));
    }

    [Fact]
    public void Conjucate_of_a_matrix_is_a_matrix_where_each_element_is_a_complex_conjucate_of_the_original_matrix()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var conjucate = TMatrix.Conjucate(matrix);

        using var _ = new AssertionScope();

        conjucate.Should().BeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, -2), (3, -5) },
            { (7, -11), (13, -19) }
        }));
        matrix.Conjucate().Should().BeEquivalentTo(conjucate);
    }

    [Fact]
    public void Conjucate_of_a_vector_is_a_vector_where_each_element_is_a_complex_conjucate_of_the_original_vector()
    {
        var vector = TColumnVector.V([(1, 2), (3, 5), (7, 11), (13, 19)]);

        var conjucate = TColumnVector.Conjucate(vector);

        conjucate.Should().BeEquivalentTo(TColumnVector.V([(1, -2), (3, -5), (7, -11), (13, -19)]));
    }

    [Fact]
    public void Adjoint_is_the_combination_of_transpose_and_conjucate()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var adjointed = TMatrix.Adjoint(matrix);

        using var _ = new AssertionScope();

        adjointed.Should().BeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, -2), (7, -11) },
            { (3, -5), (13, -19) }
        }));
        matrix.Adjoint().Should().BeEquivalentTo(adjointed);
    }

    [Fact]
    public void Matrix_product_is_the_result_of_multiplying_rows_by_columns()
    {
        var a = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var product = TMatrix.Multiply(a, b);

        using var _ = new AssertionScope();

        product.Should().BeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (-127, 409), (-167, 493) },
            { (-442, 1794), (-586, 2182) }
        }));
        (a * b).Should().BeEquivalentTo(TMatrix.Multiply(a, b));
        a.Multiply(b).Should().BeEquivalentTo(TMatrix.Multiply(a, b));
    }


    [Fact]
    public void Another_example_of_matrix_multiplication()
    {
        var a = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (3, 2), (0, 0), (5, -6) },
            { (1, 0), (4, 2), (0, 1) },
            { (4, -1), (0, 0), (4, 0) }
        });

        var b = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (5, 0), (2, -1), (6, -4) },
            { (0, 0), (4, 5), (2, 0) },
            { (7, -4), (2, 7), (0, 0) }
        });

        var product = TMatrix.Multiply(a, b);

        product.Should().BeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (26, -52), (60, 24), (26, 0) },
            { (9, 7), (1, 29), (14, 0) },
            { (48, -21), (15, 22), (20, -22) }
        }));
    }

    [Fact]
    public void On_identity_matrix_diagonal_entries_has_value_one_and_everytinhg_else_is_zeroes()
    {
        var identity = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (1, 0) } });

        using var _ = new AssertionScope();

        TMatrix.Identity(2).Should().BeEquivalentTo(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Another_example_of_identity_matrix()
    {
        var identity = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0), (0, 0) },
            { (0, 0), (1, 0), (0, 0) },
            { (0, 0), (0, 0), (1, 0) }
        });

        using var _ = new AssertionScope();

        TMatrix.Identity(3).Should().BeEquivalentTo(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Matrix_can_be_rounded_to_identity_if_it_is_close_to_identity()
    {
        var almostIdentity = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { ComplexNumber<TRealNumber>.C(0.9999999f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f) },
            { ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.9999999f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f) },
            { ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.9999999f, 0.0000001f) } });

        using var _ = new AssertionScope();

        TMatrix.Round(almostIdentity).Should().BeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 } }));
        almostIdentity.Round().Should().BeEquivalentTo(TMatrix.Round(almostIdentity));
    }

    [Fact]
    public void Matrix_cannot_be_rounded_to_identity_if_it_is_not_close_enought_to_identity()
    {
        var almostIdentity = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { ComplexNumber<TRealNumber>.C(0.999999f, 0.000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f) },
            { ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.999999f, 0.000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f) },
            { ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.0000001f, 0.0000001f), ComplexNumber<TRealNumber>.C(0.999999f, 0.000001f) } });

        TMatrix.Round(almostIdentity).Should().NotBeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 } }));
    }

    [Fact]
    public void Multiplying_matrix_by_identity_matrix_does_not_change_the_matrix()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5), (7, 11) },
            { (7, 11), (13, 19), (23, 29) },
            { (31, 37), (41, 43), (47, 53) }
        });

        var identity = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0), (0, 0) },
            { (0, 0), (1, 0), (0, 0) },
            { (0, 0), (0, 0), (1, 0) }
        });

        var product = TMatrix.Multiply(matrix, identity);

        product.Should().BeEquivalentTo(matrix);
    }

    [Fact]
    public void Algebra_of_matrices_acts_on_vectors_to_yield_new_vectors()
    {

        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var vector = TColumnVector.V([(23, 29), (31, 37)]);

        var resultOfAction = TMatrix.Act(matrix, vector);

        using var _ = new AssertionScope();

        resultOfAction.Should().BeEquivalentTo(TColumnVector.V([(-127, 341), (-458, 1526)]));
        (matrix * vector).Should().BeEquivalentTo(resultOfAction);
        matrix.Act(vector).Should().BeEquivalentTo(TMatrix.Act(matrix, vector));
    }

    [Fact]
    public void Algebra_of_matrices_acts_on_row_vectors_to_yield_new_vectors()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var vector = TRowVector.U([(23, 29), (31, 37)]);

        var resultOfAction = TMatrix.Act(vector, matrix);

        using var _ = new AssertionScope();

        resultOfAction.Should().BeEquivalentTo(TColumnVector.V([(-127, 341), (-458, 1526)]));
        (vector * matrix).Should().BeEquivalentTo(resultOfAction);
        matrix.Act(vector).Should().BeEquivalentTo(TMatrix.Act(vector, matrix));
    }

    [Fact]
    public void Matrix_is_hermitian_if_adjoint_of_matrix_does_not_change_the_matrix()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (3, 4), (5, 6) },
            { (3, -4), (7, 0), (10, 0) },
            { (5, -6), (10, 0), (9, 0) }
        });

        using var _ = new AssertionScope();

        TMatrix.IsHermitian(matrix).Should().BeTrue();
        matrix.IsHermitian().Should().BeTrue();
    }

    [Fact]
    public void Another_example_of_hermitian_matrix()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (5, 0), (4, 5), (6, -16) },
            { (4, -5), (13, 0), (7, 0) },
            { (6, 16), (7, 0),(-2.1f, 0) }
        });

        TMatrix.IsHermitian(matrix).Should().BeTrue();
    }

    [Fact]
    public void Third_example_of_hermitian_matrix()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (7, 0), (6, 5) },
            { (6, -5), (-3, 0) }
    });

        TMatrix.IsHermitian(matrix).Should().BeTrue();
    }

    [Fact]
    public void Fourth_example_of_hermitian_matrix()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (2, 0), (3, 0) },
            { (2, 0), (2, 0), (3, 0) },
            { (3, 0), (3, 0), (9, 0) }
        });

        TMatrix.IsHermitian(matrix).Should().BeTrue();
    }


    [Fact]
    public void Example_of_nonhermitian_matrix()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (7, 0), (6, 5) },
            { (6, 5), (3, 0) } });

        TMatrix.IsHermitian(matrix).Should().BeFalse();
    }

    [Fact]
    public void Inner_product_when_applying_hermitian_matrix()
    // If A is hermitian matrix then inner product of A*TColumnVector.V and TColumnVector.V' is equal to inner product of TColumnVector.V and A*TColumnVector.V
    {
        var a = TColumnVector.V([(1, 2), (3, 5)]);
        var b = TColumnVector.V([(7, 11), (13, 19)]);

        var hermitian = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (7, 0), (6, 5) },
            { (6, -5), (-3, 0) }
        });

        TColumnVector.InnerProduct(a, hermitian * b).Should().BeEquivalentTo(TColumnVector.InnerProduct(hermitian * a, b));
    }

    [Fact]
    public void Matrix_is_unitary_if_product_of_matrix_and_its_adjoint_is_equal_to_product_of_adjoint_and_matrix_is_equal_to_identity_matrix()
    {
        var a = RealNumber<TRealNumber>.R(10);

        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (TRealNumber.Cos(a), 0), (-TRealNumber.Sin(a), 0), (0, 0) },
            { (TRealNumber.Sin(a), 0), (TRealNumber.Cos(a), 0), (0, 0) },
            { (0, 0), (0, 0), (1, 0) }
        });

        using var _ = new AssertionScope();

        TMatrix.IsUnitary(matrix).Should().BeTrue();
        matrix.IsUnitary().Should().BeTrue();
    }

    [Fact]
    public void Another_example_of_unitary_matrix()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1 / Math.Sqrt(2), 0), (1 / Math.Sqrt(2), 0) },
            { (0, 1 / Math.Sqrt(2)), (0, -1 / Math.Sqrt(2)) }
        });

        TMatrix.IsUnitary(matrix).Should().BeTrue();
    }

    [Fact]
    public void Third_example_of_unitary_matrix()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (0, 1) } });

        TMatrix.IsUnitary(matrix).Should().BeTrue();
    }

    [Fact]
    public void Example_of_nonunitary_matrix()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (1, 0) },
            { (0, 1), (0, -1) }
        });

        using var _ = new AssertionScope();

        TMatrix.IsUnitary(matrix).Should().BeFalse();
        matrix.IsUnitary().Should().BeFalse();
    }

    [Fact]
    public void Product_of_unitary_matrices_is_also_unitary_matrix()
    {
        var unitary = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1 / Math.Sqrt(2), 0), (1 / Math.Sqrt(2), 0) },
            { (0, 1 / Math.Sqrt(2)), (0, -1 / Math.Sqrt(2)) }
        });

        var anotherUnitary = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (0, 1) }
        });

        TMatrix.IsUnitary(unitary).Should().BeTrue();
        TMatrix.IsUnitary(anotherUnitary).Should().BeTrue();
        TMatrix.IsUnitary(unitary * anotherUnitary).Should().BeTrue();
    }

    [Fact]
    public void Unitary_matrices_preserve_inner_products()
    {
        var a = TColumnVector.V([(1, 2), (3, 5)]);
        var b = TColumnVector.V([(7, 11), (13, 19)]);

        var unitary = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (0, 1) }
        });

        TColumnVector.InnerProduct(unitary * a, unitary * b).Should().BeEquivalentTo(TColumnVector.InnerProduct(a, b));
    }

    [Fact]
    public void Unitary_matrices_preserve_distance()
    {
        var a = TColumnVector.V([(1, 2), (3, 5)]);
        var b = TColumnVector.V([(7, 11), (13, 19)]);

        var unitary = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (0, 1) }
        });

        TColumnVector.Distance(unitary * a, unitary * b).Should().Be(TColumnVector.Distance(a, b));
    }

    [Fact]
    public void Multiplying_unitary_matrix_by_its_adjoint_produces_an_identity_matrix()
    {
        var unitary = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1 / Math.Sqrt(2), 0), (1 / Math.Sqrt(2), 0) },
            { (0, 1 / Math.Sqrt(2)), (0, -1 / Math.Sqrt(2)) }
        });

        (unitary * TMatrix.Adjoint(unitary)).Round().Should().BeEquivalentTo(TMatrix.Identity(2));
    }

    [Fact]
    public void Another_example_of_multiplying_unitary_matrix_by_its_adjoint()
    {
        var unitary = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1 / Math.Sqrt(2), 0), (1 / Math.Sqrt(2), 0), (0, 0) },
            { (0, -1 / Math.Sqrt(2)), (0, 1 / Math.Sqrt(2)), (0, 0) },
            { (0, 0), (0, 0), (0, 1) }
        });

        var transposeOfunitary = TMatrix.Adjoint(unitary);

        (unitary * transposeOfunitary).Round().Should().BeEquivalentTo(TMatrix.Identity(3));
    }

    [Fact]
    public void Commutator_of_hermitians()
    // Commutator of hermitians a and b is (a * b) - (b * a)
    {
        var a = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (0, 0), (1, 0) },
            { (1, 0), (0, 0) } });

        var b = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (0, 0), (0, -1) },
            { (0, 1), (0, 0) } });

        var commutator = TMatrix.Commutator(a, b);

        using var _ = new AssertionScope();

        commutator.Should().BeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (0, 2), (0, 0) },
            { (0, 0), (0, -2) } }));
        a.Commutator(b).Should().BeEquivalentTo(TMatrix.Commutator(a, b));
    }

    [Fact]
    public void Commutator_of_hermitians_is_zero_if_hermitians_are_commutable()
    {

        var a = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (0, 0) },
            { (0, 0), (1, 0) } });

        var b = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (0, 0), (1, 0) },
            { (1, 0), (0, 0) } });

        var commutator = TMatrix.Commutator(a, b);

        commutator.Should().BeEquivalentTo(TMatrix.Zero(2));
    }

    [Fact]
    public void Tensor_product_of_matrix_contains_combinations_of_scalar_products_of_all_elements_of_both_matrix()
    {
        var a = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) } });

        var b = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) } });

        var tensorProduct = TMatrix.TensorProduct(a, b);

        using var _ = new AssertionScope();

        tensorProduct.Should().BeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (-35, 75), (-43, 99), (-76, 202), (-92, 266) },
            { (-45, 125), (-59, 147), (-92, 334), (-124, 394) },
            { (-158, 456), (-190, 600), (-252, 814), (-300, 1070) },
            { (-186, 752), (-254, 888), (-284, 1338), (-396, 1582) }
        }));
        a.TensorProduct(b).Should().BeEquivalentTo(TMatrix.TensorProduct(a, b));
    }

    [Fact]
    public void Another_example_of_tensor_product()
    {
        var a = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (3, 2), (5, -1), (0, 2) },
            { (0, 0), (12, 0), (6, -3) },
            { (2, 0), (4, 4), (9, 3) }
        });

        var b = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 0), (3, 4), (5, -7) },
            { (10, 2), (6, 0), (2, 5) },
            { (0, 0), (1, 0), (2, 9) }
        });

        var tensorProduct = TMatrix.TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
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

        var a = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 1), (2, 2) },
            { (3, 3), (4, 4) }
        });

        var b = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 1), (2, 2), (3, 3) },
            { (4, 4), (5, 5), (6, 6) },
            { (7, 7), (8, 8), (9, 9) }
        });

        var tensorProduct = TMatrix.TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(TMatrix.M(new ComplexNumber<TRealNumber>[,] {
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
        var a = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (1, 2), (3, 5) },
            { (7, 11), (13, 19) }
        });

        var b = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (23, 29), (31, 37) },
            { (41, 43), (47, 53) }
        });

        var c = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (59, 61), (67, 71) },
            { (73, 79), (83, 89) }
        });

        TMatrix.TensorProduct(a, TMatrix.TensorProduct(b, c)).Should().BeEquivalentTo(TMatrix.TensorProduct(TMatrix.TensorProduct(a, b), c));
    }

    [Fact]
    public void Matrix_multiplied_by_its_eigen_vector_equals_to_eigen_value_multiplied_by_eigen_vector()
    {
        var matrix = TMatrix.M(new ComplexNumber<TRealNumber>[,] {
            { (4, 0), (-1, 0) },
            { (2, 0), (1, 0) } });

        var eigenVector = TColumnVector.V([(1, 0), (1, 0)]);

        var eigenValue = RealNumber<TRealNumber>.R(3);

        (eigenValue * eigenVector).Should().BeEquivalentTo(matrix * eigenVector);
    }
}