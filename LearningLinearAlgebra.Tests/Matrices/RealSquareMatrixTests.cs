using LearningLinearAlgebra.Matrices.Real;
using LearningLinearAlgebra.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Numerics;
using Xunit;

namespace LearningLinearAlgebra.Tests.Matrices;

public class SinglePrecisionCpuRealSquareMatrixTests : RealSquareMatrixTests<SquareMatrix<float>, RowVector<float>, ColumnVector<float>, float> { }
public class DoublePrecisionCpuRealSquareMatrixTests : RealSquareMatrixTests<SquareMatrix<double>, RowVector<double>, ColumnVector<double>, double> { }

public abstract class RealSquareMatrixTests<TMatrix, TRowVector, TColumnVector, TRealNumber>
    where TMatrix : ISquareMatrix<TMatrix, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    [Fact]
    public void Sum_of_two_matrices_is_calculated_as_sum_of_the_components()
    {
        var a = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 }
        });
        var b = TMatrix.M(new float[,] { { 23, 31 }, { 41, 47 } });

        var sum = TMatrix.Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(TMatrix.M(new float[,] { { 24, 34 }, { 48, 60 } }));
        (a + b).Should().BeEquivalentTo(TMatrix.Add(a, b));
        a.Add(b).Should().BeEquivalentTo(TMatrix.Add(a, b));
    }

    [Fact]
    public void Sum_of_complex_matrices_is_commutative()
    {
        var a = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = TMatrix.M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        (b + a).Should().BeEquivalentTo(a + b);
    }

    [Fact]
    public void Sum_of_complex_matrices_is_associative()
    {
        var a = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = TMatrix.M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        var c = TMatrix.M(new float[,] {
            { 59, 67 },
            { 73, 83 } });

        (a + (b + c)).Should().BeEquivalentTo(a + b + c);
    }

    [Fact]
    public void Sum_of_matrix_and_its_the_inverse_is_zero()
    {
        var matrix = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var zero = TMatrix.Zero(2);

        (matrix + -matrix).Should().BeEquivalentTo(zero);
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var matrix = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 } });
        var zero = TMatrix.Zero(2);

        using var _ = new AssertionScope();

        (matrix + zero).Should().BeEquivalentTo(matrix);
        (zero + matrix).Should().BeEquivalentTo(matrix);
    }

    [Fact]
    public void Difference_of_two_matrices_is_calculated_as_difference_of_the_components()
    {
        var a = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = TMatrix.M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        var difference = TMatrix.Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(TMatrix.M(new float[,] {
            { -22, -28 },
            { -34, -34 } }));
        (a - b).Should().BeEquivalentTo(TMatrix.Subtract(a, b));
        a.Subtract(b).Should().BeEquivalentTo(TMatrix.Subtract(a, b));
    }

    [Fact]
    public void When_multiplying_a_matrix_by_scalar_then_each_element_of_the_matrix_is_multiplied_by_the_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5.0f);
        var matrix = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 } });
        var multiplied = TMatrix.Multiply(scalar, matrix);

        using var _ = new AssertionScope();

        multiplied.Should().BeEquivalentTo(TMatrix.M(new float[,] {
            { 5, 15 },
            { 35, 65 } }));
        (scalar * matrix).Should().BeEquivalentTo(TMatrix.Multiply(scalar, matrix));
        scalar.Multiply(matrix).Should().BeEquivalentTo(TMatrix.Multiply(scalar, matrix));
    }

    [Fact]
    public void Scalar_multiplication_respects_complex_multiplication()
    {
        var scalarA = RealNumber<TRealNumber>.R(3);
        var scalarB = RealNumber<TRealNumber>.R(7);
        var matrix = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        (scalarA * scalarB * matrix).Should().BeEquivalentTo(scalarA * (scalarB * matrix));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = RealNumber<TRealNumber>.R(3);

        var matrixA = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var matrixB = TMatrix.M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        (scalar * matrixA + scalar * matrixB).Should().BeEquivalentTo(scalar * (matrixA + matrixB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_complex_addition()
    {
        var scalarA = RealNumber<TRealNumber>.R(3);
        var scalarB = RealNumber<TRealNumber>.R(7);

        var matrix = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 }
        });

        (scalarA * matrix + scalarB * matrix).Should().BeEquivalentTo((scalarA + scalarB) * matrix);
    }

    [Fact]
    public void Transposing_a_matrix_flips_the_rows_and_columns()
    {
        var matrix = TMatrix.M(new float[,] {
            { 1, 2 },
            { 3, 4 }
        });

        var transposed = TMatrix.Transpose(matrix);

        using var _ = new AssertionScope();

        transposed.Should().BeEquivalentTo(TMatrix.M(new float[,] {
            { 1, 3 },
            { 2, 4 }
        }));
        matrix.Transpose().Should().BeEquivalentTo(transposed);
    }

    [Fact]
    public void Example_of_transposing_a_square_matrix()
    {
        var matrix = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var transposed = TMatrix.Transpose(matrix);

        transposed.Should().BeEquivalentTo(TMatrix.M(new float[,] {
            { 1, 7 },
            { 3, 13 }
        }));
    }

    [Fact]
    public void Matrix_product_is_the_result_of_multiplying_rows_by_columns()
    {
        var a = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 }
        });

        var b = TMatrix.M(new float[,] {
            { 23, 31 },
            { 41, 47 }
        });

        var product = TMatrix.Multiply(a, b);

        using var _ = new AssertionScope();

        product.Should().BeEquivalentTo(TMatrix.M(new float[,] { { 146, 172 }, { 694, 828 } }));
        (a * b).Should().BeEquivalentTo(TMatrix.Multiply(a, b));
        a.Multiply(b).Should().BeEquivalentTo(TMatrix.Multiply(a, b));
    }

    [Fact]
    public void On_identity_matrix_diagonal_entries_has_value_one_and_everytinhg_else_is_zeroes()
    {
        var identity = TMatrix.M(new float[,] {
            { 1, 0 },
            { 0, 1 }
        });

        using var _ = new AssertionScope();

        TMatrix.Identity(2).Should().BeEquivalentTo(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Another_example_of_identity_matrix()
    {
        var identity = TMatrix.M(new float[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 } });

        using var _ = new AssertionScope();

        TMatrix.Identity(3).Should().BeEquivalentTo(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Matrix_can_be_rounded_to_identity_if_it_is_close_to_identity()
    {
        var almostIdentity = TMatrix.M(new float[,] {
            { 0.9999999f, 0.0000001f, 0.0000001f },
            { 0.0000001f, 0.9999999f, 0.0000001f },
            { 0.0000001f, 0.0000001f, 0.9999999f } });

        using var _ = new AssertionScope();

        TMatrix.Round(almostIdentity).Should().BeEquivalentTo(TMatrix.M(new float[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 } }));
        almostIdentity.Round().Should().BeEquivalentTo(TMatrix.Round(almostIdentity));
    }

    [Fact]
    public void Matrix_cannot_be_rounded_to_identity_if_it_is_not_close_enought_to_identity()
    {
        var almostIdentity = TMatrix.M(new float[,] {
            { 0.999999f, 0.000001f, 0.000001f },
            { 0.000001f, 0.999999f, 0.000001f },
            { 0.000001f, 0.000001f, 0.999999f } });

        using var _ = new AssertionScope();

        TMatrix.Round(almostIdentity).Should().NotBeEquivalentTo(TMatrix.M(new float[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 } }));
    }

    [Fact]
    public void Multiplying_matrix_by_identity_matrix_does_not_change_the_matrix()
    {
        var matrix = TMatrix.M(new float[,] {
            { 1, 3, 7 },
            { 7, 13, 23 },
            { 31, 41, 47 }
        });

        var identity = TMatrix.M(new float[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 }
        });

        var product = TMatrix.Multiply(matrix, identity);

        product.Should().BeEquivalentTo(matrix);
    }

    [Fact]
    public void Multiplying_orthogonal_matrix_by_its_transpose_produces_an_identity_matrix()
    {
        var orthogonal = TMatrix.M(new float[,] {
            { 0, 1, 0 },
            { 0, 0, 1 },
            { 1, 0, 0 }
        });

        var transposeOfOrthogonal = TMatrix.Transpose(orthogonal);

        using var _ = new AssertionScope();

        (orthogonal * transposeOfOrthogonal).Should().BeEquivalentTo(TMatrix.Identity(3));
        (transposeOfOrthogonal * orthogonal).Should().BeEquivalentTo(TMatrix.Identity(3));
    }

    [Fact]
    public void Algebra_of_matrices_acts_on_vectors_to_yield_new_vectors()
    {
        var matrix = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var vector = TColumnVector.V([23, 31]);

        var resultOfAction = TMatrix.Act(matrix, vector);

        using var _ = new AssertionScope();

        resultOfAction.Should().BeEquivalentTo(TColumnVector.V([116, 564]));
        (matrix * vector).Should().BeEquivalentTo(TMatrix.Act(matrix, vector));
        matrix.Act(vector).Should().BeEquivalentTo(TMatrix.Act(matrix, vector));
    }

    [Fact]
    public void Commutator_of_matrices()
    // Commutator of matrices a and b is (a * b) - (b * a)
    {
        var a = TMatrix.M(new float[,] {
            { 1, 2 },
            { 2, 1}
        });

        var b = TMatrix.M(new float[,] {
            { 3, 4 },
            { 4, 3}
        });

        var commutator = TMatrix.Commutator(a, b);

        using var _ = new AssertionScope();

        commutator.Should().BeEquivalentTo(a * b - b * a);
        a.Commutator(b).Should().BeEquivalentTo(TMatrix.Commutator(a, b));
    }

    [Fact]
    public void Tensor_product_of_matrix_contains_combinations_of_scalar_products_of_all_elements_of_both_matrix()
    {
        var a = TMatrix.M(new float[,] {
            { 1, 2 },
            { 3, 4 } });

        var b = TMatrix.M(new float[,] {
            { 5, 6 },
            { 7, 8 } });

        var tensorProduct = TMatrix.TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(TMatrix.M(new float[,] {
                { 5, 6, 10, 12 },
                { 7, 8, 14, 16 },
                { 15, 18, 20, 24 },
                { 21, 24, 28, 32 } })
        );
    }

    [Fact]
    public void Another_example_of_tensor_product()
    {
        var a = TMatrix.M(new float[,] {
            { 1, 2 },
            { 3, 4 } });

        var b = TMatrix.M(new float[,] {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 } });

        var tensorProduct = TMatrix.TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(TMatrix.M(new float[,] {
            { 1, 2, 3, 2, 4, 6 },
            { 4, 5, 6, 8, 10, 12 },
            { 7, 8, 9, 14, 16, 18 },
            { 3, 6, 9, 4, 8, 12 },
            { 12, 15, 18, 16, 20, 24 },
            { 21, 24, 27, 28, 32, 36 } })
            );
    }

    [Fact]
    public void Tensor_product_is_associative()
    {
        var a = TMatrix.M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = TMatrix.M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        var c = TMatrix.M(new float[,] {
            { 59, 67 },
            { 73, 83 } });


        TMatrix.TensorProduct(a, TMatrix.TensorProduct(b, c)).Should().BeEquivalentTo(TMatrix.TensorProduct(TMatrix.TensorProduct(a, b), c));
    }

    [Fact]
    public void Matrix_multiplied_by_its_eigen_vector_equals_to_eigen_value_multiplied_by_eigen_vector()
    {
        var matrix = TMatrix.M(new float[,] {
            { 4, -1 },
            { 2, 1 } });

        var eigenVector = TColumnVector.V([1, 1]);
        var eigenValue = RealNumber<TRealNumber>.R(3);

        (eigenValue * eigenVector).Should().BeEquivalentTo(matrix * eigenVector);
    }
}