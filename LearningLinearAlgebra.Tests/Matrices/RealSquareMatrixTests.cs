using LearningLinearAlgebra.Matrices.Real;
using LearningLinearAlgebra.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

using static LearningLinearAlgebra.Matrices.Real.SquareMatrix<float>;
using static LearningLinearAlgebra.Matrices.Real.ColumnVector<float>;
using static LearningLinearAlgebra.Matrices.Real.RowVector<float>;
using static LearningLinearAlgebra.Numbers.RealNumber<float>;

namespace LearningLinearAlgebra.Tests.Matrices;

public class RealSquareMatrixTests
{
    [Fact]
    public void Sum_of_two_matrices_is_calculated_as_sum_of_the_components()
    {
        var a = M(new float[,] {
            { 1, 3 },
            { 7, 13 }
        });
        var b = M(new float[,] { { 23, 31 }, { 41, 47 } });

        var sum = Add(a, b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(M(new float[,] { { 24, 34 }, { 48, 60 } }));
        (a + b).Should().BeEquivalentTo(Add(a, b));
        a.Add(b).Should().BeEquivalentTo(Add(a, b));
    }

    [Fact]
    public void Sum_of_complex_matrices_is_commutative()
    {
        var a = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        (b + a).Should().BeEquivalentTo(a + b);
    }

    [Fact]
    public void Sum_of_complex_matrices_is_associative()
    {
        var a = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        var c = M(new float[,] {
            { 59, 67 },
            { 73, 83 } });

        (a + (b + c)).Should().BeEquivalentTo(a + b + c);
    }

    [Fact]
    public void Sum_of_matrix_and_its_the_inverse_is_zero()
    {
        var matrix = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var zero = SquareMatrix<float>.Zero(2);

        (matrix + -matrix).Should().BeEquivalentTo(zero);
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var matrix = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });
        var zero = SquareMatrix<float>.Zero(2);

        using var _ = new AssertionScope();

        (matrix + zero).Should().BeEquivalentTo(matrix);
        (zero + matrix).Should().BeEquivalentTo(matrix);
    }

    [Fact]
    public void Difference_of_two_matrices_is_calculated_as_difference_of_the_components()
    {
        var a = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        var difference = Subtract(a, b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(M(new float[,] {
            { -22, -28 },
            { -34, -34 } }));
        (a - b).Should().BeEquivalentTo(Subtract(a, b));
        a.Subtract(b).Should().BeEquivalentTo(Subtract(a, b));
    }

    [Fact]
    public void When_multiplying_a_matrix_by_scalar_then_each_element_of_the_matrix_is_multiplied_by_the_scalar()
    {
        var scalar = R(5.0f);
        var matrix = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });
        var multiplied = Multiply(scalar, matrix);

        using var _ = new AssertionScope();

        multiplied.Should().BeEquivalentTo(M(new float[,] {
            { 5, 15 },
            { 35, 65 } }));
        (scalar * matrix).Should().BeEquivalentTo(Multiply(scalar, matrix));
        scalar.Multiply(matrix).Should().BeEquivalentTo(Multiply(scalar, matrix));
    }

    [Fact]
    public void Scalar_multiplication_respects_complex_multiplication()
    {
        var scalarA = R(3);
        var scalarB = R(7);
        var matrix = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        (scalarA * scalarB * matrix).Should().BeEquivalentTo(scalarA * (scalarB * matrix));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = R(3);

        var matrixA = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var matrixB = M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        (scalar * matrixA + scalar * matrixB).Should().BeEquivalentTo(scalar * (matrixA + matrixB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_complex_addition()
    {
        var scalarA = R(3);
        var scalarB = R(7);

        var matrix = M(new float[,] {
            { 1, 3 },
            { 7, 13 }
        });

        (scalarA * matrix + scalarB * matrix).Should().BeEquivalentTo((scalarA + scalarB) * matrix);
    }

    [Fact]
    public void Transposing_a_matrix_flips_the_rows_and_columns()
    {
        var matrix = M(new float[,] {
            { 1, 2 },
            { 3, 4 }
        });

        var transposed = Transpose(matrix);

        using var _ = new AssertionScope();

        transposed.Should().BeEquivalentTo(M(new float[,] {
            { 1, 3 },
            { 2, 4 }
        }));
        matrix.Transpose().Should().BeEquivalentTo(transposed);
    }

    [Fact]
    public void Example_of_transposing_a_square_matrix()
    {
        var matrix = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var transposed = Transpose(matrix);

        transposed.Should().BeEquivalentTo(M(new float[,] {
            { 1, 7 },
            { 3, 13 }
        }));
    }

    [Fact]
    public void Matrix_product_is_the_result_of_multiplying_rows_by_columns()
    {
        var a = M(new float[,] {
            { 1, 3 },
            { 7, 13 }
        });

        var b = M(new float[,] {
            { 23, 31 },
            { 41, 47 }
        });

        var product = Multiply(a, b);

        using var _ = new AssertionScope();

        product.Should().BeEquivalentTo(M(new float[,] { { 146, 172 }, { 694, 828 } }));
        (a * b).Should().BeEquivalentTo(Multiply(a, b));
        a.Multiply(b).Should().BeEquivalentTo(Multiply(a, b));
    }

    [Fact]
    public void On_identity_matrix_diagonal_entries_has_value_one_and_everytinhg_else_is_zeroes()
    {
        var identity = M(new float[,] {
            { 1, 0 },
            { 0, 1 }
        });

        using var _ = new AssertionScope();

        Identity(2).Should().BeEquivalentTo(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Another_example_of_identity_matrix()
    {
        var identity = M(new float[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 } });

        using var _ = new AssertionScope();

        Identity(3).Should().BeEquivalentTo(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Matrix_can_be_rounded_to_identity_if_it_is_close_to_identity()
    {
        var almostIdentity = M(new float[,] {
            { 0.9999999f, 0.0000001f, 0.0000001f },
            { 0.0000001f, 0.9999999f, 0.0000001f },
            { 0.0000001f, 0.0000001f, 0.9999999f } });

        using var _ = new AssertionScope();

        Round(almostIdentity).Should().BeEquivalentTo(M(new float[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 } }));
        almostIdentity.Round().Should().BeEquivalentTo(Round(almostIdentity));
    }

    [Fact]
    public void Matrix_cannot_be_rounded_to_identity_if_it_is_not_close_enought_to_identity()
    {
        var almostIdentity = M(new float[,] {
            { 0.999999f, 0.000001f, 0.000001f },
            { 0.000001f, 0.999999f, 0.000001f },
            { 0.000001f, 0.000001f, 0.999999f } });

        using var _ = new AssertionScope();

        Round(almostIdentity).Should().NotBeEquivalentTo(M(new float[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 } }));
    }

    [Fact]
    public void Multiplying_matrix_by_identity_matrix_does_not_change_the_matrix()
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
    public void Multiplying_orthogonal_matrix_by_its_transpose_produces_an_identity_matrix()
    {
        var orthogonal = M(new float[,] {
            { 0, 1, 0 },
            { 0, 0, 1 },
            { 1, 0, 0 }
        });

        var transposeOfOrthogonal = Transpose(orthogonal);

        using var _ = new AssertionScope();

        (orthogonal * transposeOfOrthogonal).Should().BeEquivalentTo(Identity(3));
        (transposeOfOrthogonal * orthogonal).Should().BeEquivalentTo(Identity(3));
    }

    [Fact]
    public void Algebra_of_matrices_acts_on_vectors_to_yield_new_vectors()
    {
        var matrix = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var vector = ColumnVector<float>.V([23, 31]);

        var resultOfAction = Act(matrix, vector);

        using var _ = new AssertionScope();

        resultOfAction.Should().Equal(ColumnVector<float>.V([116, 564]));
        (matrix * vector).Should().Equal(Act(matrix, vector));
        matrix.Act(vector).Should().Equal(Act(matrix, vector));
    }

    [Fact]
    public void Commutator_of_matrices()
    // Commutator of matrices a and b is (a * b) - (b * a)
    {
        var a = M(new float[,] {
            { 1, 2 },
            { 2, 1}
        });

        var b = M(new float[,] {
            { 3, 4 },
            { 4, 3}
        });

        var commutator = Commutator(a, b);

        using var _ = new AssertionScope();

        commutator.Should().BeEquivalentTo(a * b - b * a);
        a.Commutator(b).Should().BeEquivalentTo(Commutator(a, b));
    }

    [Fact]
    public void Tensor_product_of_matrix_contains_combinations_of_scalar_products_of_all_elements_of_both_matrix()
    {
        var a = M(new float[,] {
            { 1, 2 },
            { 3, 4 } });

        var b = M(new float[,] {
            { 5, 6 },
            { 7, 8 } });

        var tensorProduct = TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(M(new float[,] {
                { 5, 6, 10, 12 },
                { 7, 8, 14, 16 },
                { 15, 18, 20, 24 },
                { 21, 24, 28, 32 } })
        );
    }

    [Fact]
    public void Another_example_of_tensor_product()
    {
        var a = M(new float[,] {
            { 1, 2 },
            { 3, 4 } });

        var b = M(new float[,] {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 } });

        var tensorProduct = TensorProduct(a, b);

        tensorProduct.Should().BeEquivalentTo(M(new float[,] {
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
        var a = M(new float[,] {
            { 1, 3 },
            { 7, 13 } });

        var b = M(new float[,] {
            { 23, 31 },
            { 41, 47 } });

        var c = M(new float[,] {
            { 59, 67 },
            { 73, 83 } });


        TensorProduct(a, TensorProduct(b, c)).Should().BeEquivalentTo(TensorProduct(TensorProduct(a, b), c));
    }

    [Fact]
    public void Matrix_multiplied_by_its_eigen_vector_equals_to_eigen_value_multiplied_by_eigen_vector()
    {
        var matrix = M(new float[,] {
            { 4, -1 },
            { 2, 1 } });

        var eigenVector = ColumnVector<float>.V([1, 1]);
        var eigenValue = R(3);

        (eigenValue * eigenVector).Should().Equal(matrix * eigenVector);
    }
}