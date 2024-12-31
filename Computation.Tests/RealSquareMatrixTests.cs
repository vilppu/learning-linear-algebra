using System.Numerics;
using Computation.Matrices.Real;
using Computation.Numbers;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Computation.Tests;

public class ManagedSinglePrecisionRealSquareMatrixTests : RealSquareMatrixTests<Managed.Real.Matrices<float>, float>;
public class ManagedDoublePrecisionRealSquareMatrixTests : RealSquareMatrixTests<Managed.Real.Matrices<double>, double>; 
public class CudaSinglePrecisionRealSquareMatrixTests : RealSquareMatrixTests<Cuda.Real.Matrices<float>, float>;
public class CudaDoublePrecisionRealSquareMatrixTests : RealSquareMatrixTests<Cuda.Real.Matrices<double>, double>;

public abstract class RealSquareMatrixTests<TMatrices, TRealNumber>
    where TMatrices : IMatrices<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    protected RealSquareMatrixTests() =>
        Formatters<TRealNumber>.Register();

    [Fact]
    public void Sum_of_two_matrices_is_calculated_as_sum_of_the_components()
    {
        var a = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 } });

        var b = TMatrices.M(new[,] {
            { 23.0, 31.0 },
            { 41.0, 47.0 } });

        var sum = a.Add(b);

        using var _ = new AssertionScope();

        sum.Should().BeEquivalentTo(TMatrices.M(new[,] {
            { 24.0, 34.0 },
            { 48.0, 60.0 } }));
        (a + b).Should().BeEquivalentTo(a.Add(b));
    }

    [Fact]
    public void Sum_of_complex_matrices_is_commutative()
    {
        var a = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 } });

        var b = TMatrices.M(new[,] {
            { 23.0, 31.0 },
            { 41.0, 47.0 } });

        (b + a).Should().BeEquivalentTo(a + b);
    }

    [Fact]
    public void Sum_of_complex_matrices_is_associative()
    {
        var a = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 } });

        var b = TMatrices.M(new[,] {
            { 23.0, 31.0 },
            { 41.0, 47.0 } });

        var c = TMatrices.M(new[,] {
            { 59.0, 67.0 },
            { 73.0, 83.0 } });


        (a + (b + c)).Should().BeEquivalentTo(a + b + c);
    }

    [Fact]
    public void Sum_of_matrix_and_its_the_inverse_is_zero()
    {
        var matrix = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 } });

        var zero = TMatrices.Zero(2);

        (matrix + -matrix).Should().BeEquivalentTo(zero);
    }

    [Fact]
    public void Zero_is_an_additive_identity()
    {
        var matrix = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 } });

        var zero = TMatrices.Zero(2);

        using var _ = new AssertionScope();

        (matrix + zero).Should().BeEquivalentTo(matrix);
        (zero + matrix).Should().BeEquivalentTo(matrix);
    }

    [Fact]
    public void Difference_of_two_matrices_is_calculated_as_difference_of_the_components()
    {
        var a = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 } });

        var b = TMatrices.M(new[,] {
            { 23.0, 31.0 },
            { 41.0, 47.0 } });

        var difference = a.Subtract(b);

        using var _ = new AssertionScope();

        difference.Should().BeEquivalentTo(
            TMatrices.M(new[,] {
                { -22.0, -28.0 },
                { -34.0, -34.0 }
            }
        ));
        (a - b).Should().BeEquivalentTo(a.Subtract(b));
    }

    [Fact]
    public void When_multiplying_a_matrix_by_scalar_then_each_element_of_the_matrix_is_multiplied_by_the_scalar()
    {
        var scalar = RealNumber<TRealNumber>.R(5);
        var matrix = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 } });

        var multiplied = matrix.Multiply(scalar);

        using var _ = new AssertionScope();

        multiplied.Should().BeEquivalentTo(TMatrices.M(new[,] {
            { 5.0, 15.0 },
            { 35.0, 65.0 }
        }));
        (scalar * matrix).Should().BeEquivalentTo(matrix.Multiply(scalar));
        matrix.Multiply(scalar).Should().BeEquivalentTo(matrix.Multiply(scalar));
    }

    [Fact]
    public void Scalar_multiplication_respects_complex_multiplication()
    {
        var scalarA = RealNumber<TRealNumber>.R(3);
        var scalarB = RealNumber<TRealNumber>.R(7);

        var matrix = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 }
        });

        (scalarA * scalarB * matrix).Should().BeEquivalentTo(scalarA * (scalarB * matrix));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_addition()
    {
        var scalar = RealNumber<TRealNumber>.R(3);

        var matrixA = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 }
        });

        var matrixB = TMatrices.M(new[,] {
            { 23.0, 31.0 },
            { 41.0, 47.0 }
        });

        (scalar * matrixA + scalar * matrixB).Should().BeEquivalentTo(scalar * (matrixA + matrixB));
    }

    [Fact]
    public void Scalar_multiplication_distributes_over_complex_addition()
    {
        var scalarA = RealNumber<TRealNumber>.R(3);
        var scalarB = RealNumber<TRealNumber>.R(7);

        var matrix = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 }
        });

        (scalarA * matrix + scalarB * matrix).Should().BeEquivalentTo((scalarA + scalarB) * matrix);
    }

    [Fact]
    public void Transposing_a_matrix_flips_the_rows_and_columns()
    {
        var matrix = TMatrices.M(new[,] {
            { 1.0, 2.0 },
            { 3.0, 4.0 }
        });

        var transposed = matrix.Transpose();

        using var _ = new AssertionScope();

        transposed.Should().BeEquivalentTo(TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 2.0, 4.0 } }));
        matrix.Transpose().Should().BeEquivalentTo(transposed);
    }

    [Fact]
    public void Example_of_transposing_a_square_matrix()
    {
        var matrix = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 }
        });

        var transposed = matrix.Transpose();

        transposed.Should().BeEquivalentTo(TMatrices.M(new[,] {
            { 1.0, 7.0 },
            { 3.0, 13.0 }
        }));
    }


    [Fact]
    public void Matrix_product_is_the_result_of_multiplying_rows_by_columns()
    {
        var a = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 } });

        var b = TMatrices.M(new[,] {
            { 23.0, 31.0 },
            { 41.0, 47.0 } });

        var product = a.Multiply(b);

        using var _ = new AssertionScope();

        product.Should().BeEquivalentTo(TMatrices.M(new[,] {
            { 146.0, 172.0 },
            { 694.0, 828.0 }
        }));
        (a * b).Should().BeEquivalentTo(a.Multiply(b));
    }


    [Fact]
    public void Another_example_of_matrix_multiplication()
    {
        var a = TMatrices.M(new[,] {
            { 3.0, 0.0, 5.0 },
            { 1.0, 4.0, 0.0 },
            { 4.0, 0.0, 4.0 }
        });

        var b = TMatrices.M(new[,] {
            { 5.0, 2.0, 6.0 },
            { 0.0, 4.0, 2.0 },
            { 7.0, 2.0, 0.0 }
        });

        var product = a.Multiply(b);

        product.Should().BeEquivalentTo(TMatrices.M(new[,] {
            { 50.0, 16.0, 18.0 },
            { 5.0, 18.0, 14.0 },
            { 48.0, 16.0, 24.0 }
        }));
    }

    [Fact]
    public void On_identity_matrix_diagonal_entries_has_value_one_and_everything_else_is_zeroes()
    {
        var identity = TMatrices.M(new[,] {
            { 1.0, 0.0 },
            { 0.0, 1.0 } });

        using var _ = new AssertionScope();

        TMatrices.Identity(2).Should().BeEquivalentTo(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Another_example_of_identity_matrix()
    {
        var identity = TMatrices.M(new[,] {
            { 1.0, 0.0, 0.0 },
            { 0.0, 1.0, 0.0 },
            { 0.0, 0.0, 1.0 }
        });

        using var _ = new AssertionScope();

        TMatrices.Identity(3).Should().BeEquivalentTo(identity);
        identity.IsIdentity().Should().BeTrue();
    }

    [Fact]
    public void Matrix_can_be_rounded_to_identity_if_it_is_close_to_identity()
    {
        var almostIdentity = TMatrices.M(new[,] {
            { RealNumber<TRealNumber>.R(0.9999999f), RealNumber<TRealNumber>.R(0.0000001f), RealNumber<TRealNumber>.R(0.0000001f) },
            { RealNumber<TRealNumber>.R(0.0000001f), RealNumber<TRealNumber>.R(0.9999999f), RealNumber<TRealNumber>.R(0.0000001f) },
            { RealNumber<TRealNumber>.R(0.0000001f), RealNumber<TRealNumber>.R(0.0000001f), RealNumber<TRealNumber>.R(0.9999999f) } });

        using var _ = new AssertionScope();

        almostIdentity.Round().Should().BeEquivalentTo(TMatrices.M(new[,] {
            { 1.0, 0.0, 0.0 },
            { 0.0, 1.0, 0.0 },
            { 0.0, 0.0, 1.0 } }));
        almostIdentity.Round().Should().BeEquivalentTo(almostIdentity.Round());
    }

    [Fact]
    public void Matrix_cannot_be_rounded_to_identity_if_it_is_not_close_enough_to_identity()
    {
        var almostIdentity = TMatrices.M(new[,] {
            { RealNumber<TRealNumber>.R(0.999999f), RealNumber<TRealNumber>.R(0.0000001f), RealNumber<TRealNumber>.R(0.0000001f) },
            { RealNumber<TRealNumber>.R(0.0000001f), RealNumber<TRealNumber>.R(0.999999f), RealNumber<TRealNumber>.R(0.0000001f) },
            { RealNumber<TRealNumber>.R(0.0000001f), RealNumber<TRealNumber>.R(0.0000001f), RealNumber<TRealNumber>.R(0.999999f) } });

        almostIdentity.Round().Should().NotBeEquivalentTo(TMatrices.M(new[,] {
            { 1.0, 0.0, 0.0 },
            { 0.0, 1.0, 0.0 },
            { 0.0, 0.0, 1.0 } }));
    }

    [Fact]
    public void Multiplying_matrix_by_identity_matrix_does_not_change_the_matrix()
    {
        var matrix = TMatrices.M(new[,] {
            { 1.0, 3.0, 7.0 },
            { 7.0, 13.0, 23.0 },
            { 31.0, 41.0, 47.0 }
        });

        var identity = TMatrices.M(new[,] {
            { 1.0, 0.0, 0.0 },
            { 0.0, 1.0, 0.0 },
            { 0.0, 0.0, 1.0 }
        });

        var product = matrix.Multiply(identity);

        product.Should().BeEquivalentTo(matrix);
    }

    [Fact]
    public void Algebra_of_matrices_acts_on_vectors_to_yield_new_vectors()
    {

        var matrix = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 }
        });

        var vector = TMatrices.V([23.0, 31.0]);

        var resultOfAction = matrix.Act(vector);

        using var _ = new AssertionScope();

        resultOfAction.Should().BeEquivalentTo(TMatrices.V([116.0, 564.0]));
        (matrix * vector).Should().BeEquivalentTo(resultOfAction);
        matrix.Act(vector).Should().BeEquivalentTo(matrix.Act(vector));
    }

    [Fact]
    public void Algebra_of_matrices_acts_on_row_vectors_to_yield_new_vectors()
    {
        var matrix = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 }
        });

        var vector = TMatrices.U([23.0, 31.0]);

        var resultOfAction = matrix.Act(vector);

        using var _ = new AssertionScope();

        resultOfAction.Should().BeEquivalentTo(TMatrices.U([116.0, 564.0]));
        (vector * matrix).Should().BeEquivalentTo(resultOfAction);
    }

    [Fact]
    public void Tensor_product_of_matrix_contains_combinations_of_scalar_products_of_all_elements_of_both_matrix()
    {
        var a = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 } });

        var b = TMatrices.M(new[,] {
            { 23.0, 31.0 },
            { 41.0, 47.0 } });

        var tensorProduct = a.TensorProduct(b);

        using var _ = new AssertionScope();

        tensorProduct.Should().BeEquivalentTo(TMatrices.M(new float[,] {
            { 23, 31, 69, 93 },
            { 41, 47, 123, 141 },
            { 161, 217, 299, 403 },
            { 287, 329, 533, 611 }
        }));
    }

    [Fact]
    public void Another_example_of_tensor_product()
    {
        var a = TMatrices.M(new[,] {
            { 3.0, 5.0, 0.0 },
            { 0.0, 12.0, 6.0 },
            { 2.0, 4.0, 9.0 }
        });

        var b = TMatrices.M(new[,] {
            { 1.0, 3.0, 5.0 },
            { 10.0, 6.0, 2.0 },
            { 0.0, 1.0, 2.0 }
        });

        var tensorProduct = a.TensorProduct(b);

        tensorProduct.Should().BeEquivalentTo(TMatrices.M(new[,] {
            {3.0, 9.0, 15.0, 5.0, 15.0, 25.0, 0.0, 0.0, 0},
            {30.0, 18.0, 6.0, 50.0, 30.0, 10.0, 0.0, 0.0, 0},
            {0.0, 3.0, 6.0, 0.0, 5.0, 10.0, 0.0, 0.0, 0},
            {0.0, 0.0, 0.0, 12.0, 36.0, 60.0, 6.0, 18.0, 30},
            {0.0, 0.0, 0.0, 120.0, 72.0, 24.0, 60.0, 36.0, 12},
            {0.0, 0.0, 0.0, 0.0, 12.0, 24.0, 0.0, 6.0, 12},
            {2.0, 6.0, 10.0, 4.0, 12.0, 20.0, 9.0, 27.0, 45},
            {20.0, 12.0, 4.0, 40.0, 24.0, 8.0, 90.0, 54.0, 18},
            {0.0, 2.0, 4.0, 0.0, 4.0, 8.0, 0.0, 9.0, 18.0}
        }));
    }

    [Fact]
    public void Third_example_of_tensor_product()
    {

        var a = TMatrices.M(new[,] {
            { 1.0, 2.0 },
            { 3.0, 4.0 }
        });

        var b = TMatrices.M(new[,] {
            { 1.0, 2.0, 3.0 },
            { 4.0, 5.0, 6.0 },
            { 7.0, 8.0, 9.0 }
        });

        var tensorProduct = a.TensorProduct(b);

        tensorProduct.Should().BeEquivalentTo(TMatrices.M(new float[,] {
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
        var a = TMatrices.M(new[,] {
            { 1.0, 3.0 },
            { 7.0, 13.0 }
        });

        var b = TMatrices.M(new[,] {
            { 23.0, 31.0 },
            { 41.0, 47.0 }
        });

        var c = TMatrices.M(new[,] {
            { 59.0, 67.0 },
            { 73.0, 83.0 }
        });

        a.TensorProduct(b.TensorProduct(c)).Should().BeEquivalentTo(a.TensorProduct(b).TensorProduct(c));
    }

    [Fact]
    public void Matrix_multiplied_by_its_eigen_vector_equals_to_eigen_value_multiplied_by_eigen_vector()
    {
        var matrix = TMatrices.M(new[,] {
            { 4.0, -1.0 },
            { 2.0, 1.0 } });

        var eigenVector = TMatrices.V([1.0, 1.0]);

        var eigenValue = RealNumber<TRealNumber>.R(3.0);

        (eigenValue * eigenVector).Should().BeEquivalentTo(matrix * eigenVector);
    }
}