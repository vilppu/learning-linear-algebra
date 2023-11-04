namespace Algebra

open Xunit

module RealMatrixTests =

    open RealNumbers

    [<Fact>]
    let ``Matrix with one element can be presented as scalar`` () =
        let matrix = M [| [| 123 |] |]

        let vector = Matrix<_>.AsScalar matrix

        Assert.Equal(123.0, vector)

    [<Fact>]
    let ``Matrix with one column can be presented as a vector`` () =
        let matrix = M [| [| 1.0 |]; [| 2.0 |]; [| 3.0 |] |]

        let vector = Matrix<_>.AsVector matrix

        Assert.Equal(V [| 1.0; 2.0; 3.0 |], vector)

    [<Fact>]
    let ``Sum of two matrices is calculated as sum of the components`` () =
        let a = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let b = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        let sum = Matrix<_>.Add a b

        Assert.Equal(M [| [| 24.0; 34 |]; [| 48.0; 60 |] |], sum)

        Assert.Equal(Matrix<_>.Add a b, a + b)

    [<Fact>]
    let ``Sum of complex matrices is commutative`` () =
        let a = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let b = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        Assert.Equal(a + b, b + a)

    [<Fact>]
    let ``Sum of complex matrices is associative`` () =
        let a = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let b = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        let c = M [| [| 59.0; 67 |]; [| 73.0; 83 |] |]

        Assert.Equal((a + b) + c, a + (b + c))

    [<Fact>]
    let ``Sum of matrix and it's the inverse is zero`` () =
        let matrix = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let zero = Matrix<_>.Zero 2 2

        Assert.Equal(zero, matrix + (-matrix))

    [<Fact>]
    let ``Zero is an additive identity`` () =
        let matrix = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let zero = Matrix<_>.Zero 2 2

        Assert.Equal(matrix, matrix + zero)
        Assert.Equal(matrix, zero + matrix)

    [<Fact>]
    let ``Difference of two matrices is calculated as difference of the components`` () =
        let a = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let b = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        let difference = Matrix<_>.Subtract a b

        Assert.Equal(M [| [| -22.0; -28 |]; [| -34.0; -34 |] |], difference)

        Assert.Equal(Matrix<_>.Subtract a b, a - b)

    [<Fact>]
    let ``When multiplying a matrix by scalar then each element of the matrix is multiplied by the scalar`` () =
        let scalar = 5.0

        let matrix = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let multiplied = Matrix<_>.Multiply scalar matrix

        Assert.Equal(M [| [| 5.0; 15.0 |]; [| 35.0; 65.0 |] |], multiplied)

        Assert.Equal(Matrix<_>.Multiply scalar matrix, scalar * matrix)

    [<Fact>]
    let ``Scalar multiplication respects complex multiplication`` () =
        let scalarA = 3.0

        let scalarB = 7.0

        let matrix = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        Assert.Equal(scalarA * (scalarB * matrix), (scalarA * scalarB) * matrix)

    [<Fact>]
    let ``Scalar multiplication distributes over addition`` () =
        let scalar = 3.0

        let matrixA = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let matrixB = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        Assert.Equal(scalar * (matrixA + matrixB), (scalar * matrixA) + (scalar * matrixB))

    [<Fact>]
    let ``Scalar multiplication distributes over complex addition`` () =
        let scalarA = 3.0
        let scalarB = 7.0

        let matrix = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        Assert.Equal((scalarA + scalarB) * matrix, (scalarA * matrix) + (scalarB * matrix))

    [<Fact>]
    let ``Transposing a matrix flips the rows and columns`` () =
        let matrix = M [| [| 1.0; 2.0; 3.0 |]; [| 4.0; 5.0; 6.0 |] |]

        let transposed = Matrix<_>.Transpose matrix

        Assert.Equal(M [| [| 1.0; 4.0 |]; [| 2.0; 5.0 |]; [| 3.0; 6.0 |] |], transposed)

    [<Fact>]
    let ``Example of transposing a square matrix`` () =
        let matrix = M [| [| 1.0; 3.0 |]; [| 7.0; 13.0 |] |]

        let transposed = Matrix<_>.Transpose matrix

        Assert.Equal(M [| [| 1.0; 7 |]; [| 3.0; 13 |] |], transposed)

    [<Fact>]
    let ``Transposing a row vector produces a column vector`` () =
        let matrix = M [| [| 1.0; 2.0; 3.0 |] |]

        let transposed = Matrix<_>.Transpose matrix

        Assert.Equal(M [| [| 1.0 |]; [| 2.0 |]; [| 3.0 |] |], transposed)

    [<Fact>]
    let ``Transposing a vector changes it to single column matrix`` () =
        let vector = V [| 1.0; 2.0; 3 |]

        let multiplied = Matrix<_>.Transpose vector

        Assert.Equal(M [| [| 1.0 |]; [| 2.0 |]; [| 3.0 |] |], multiplied)

    [<Fact>]
    let ``Matrix product is the result of multiplying rows by columns`` () =
        let a = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let b = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        let product = Matrix<_>.Product a b

        Assert.Equal(M [| [| 146.0; 172.0 |]; [| 694.0; 828.0 |] |], product)

        Assert.Equal(Matrix<_>.Product a b, a * b)

    [<Fact>]
    let ``On identity matrix diagonal entries has value one and everytinhg else is zeroes`` () =

        let identity = M [| [| 1.0; 0 |]; [| 0.0; 1 |] |]

        Assert.Equal(identity, Matrix<_>.Identity 2)

    [<Fact>]
    let ``Another example of identity matrix`` () =

        let identity = M [| [| 1.0; 0.0; 0 |]; [| 0.0; 1.0; 0 |]; [| 0.0; 0.0; 1 |] |]

        Assert.Equal(identity, Matrix<_>.Identity 3)

    [<Fact>]
    let ``Multiplying matrix by identity matrix does not change the matrix`` () =
        let matrix = M [| [| 1.0; 3.0; 7 |]; [| 7.0; 13.0; 23 |]; [| 31.0; 41.0; 47 |] |]

        let identity = M [| [| 1.0; 0.0; 0 |]; [| 0.0; 1.0; 0 |]; [| 0.0; 0.0; 1 |] |]

        let product = Matrix<_>.Product matrix identity

        Assert.Equal(matrix, product)

    [<Fact>]
    let ``Multiplying orthogonal matrix by it's transpose produces an identity matrix`` () =

        let orthogonal = M [| [| 0.0; 1.0; 0 |]; [| 0.0; 0.0; 1 |]; [| 1.0; 0.0; 0 |] |]

        let transposeOfOrthogonal = Matrix<_>.Transpose orthogonal

        Assert.Equal(Matrix<_>.Identity 3, orthogonal * transposeOfOrthogonal)
        Assert.Equal(Matrix<_>.Identity 3, transposeOfOrthogonal * orthogonal)

    [<Fact>]
    let ``Algebra of matrices acts on vectors to yield new vectors`` () =

        let matrix = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let vector = V [| 23.0; 31 |]

        let vectorAsMatrix = M [| [| 23 |]; [| 31 |] |]

        let resultOfAction = Matrix<_>.Act matrix vector

        Assert.Equal(V [| 116.0; 564.0 |], resultOfAction)

        Assert.Equal(Matrix<_>.Act matrix vector, matrix * vector)


    [<Fact>]
    let ``Tensor product of matrix contains combinations scalar products of all elements of both matrix`` () =

        let a = M [| [| 1.0; 2.0 |]; [| 3.0; 4.0 |] |]

        let b = M [| [| 5.0; 6.0 |]; [| 7.0; 8.0 |] |]

        let tensorProduct = Matrix<_>.TensorProduct a b

        Assert.Equal(
            M
                [| [| 5.0; 6.0; 10.0; 12.0 |]
                   [| 7.0; 8.0; 14.0; 16.0 |]
                   [| 15.0; 18.0; 20.0; 24.0 |]
                   [| 21.0; 24.0; 28.0; 32.0 |] |],
            tensorProduct
        )

    [<Fact>]
    let ``Another example of tensor product`` () =

        let a = M [| [| 1.0; 2 |]; [| 3.0; 4 |] |]

        let b = M [| [| 1.0; 2.0; 3 |]; [| 4.0; 5.0; 6 |]; [| 7.0; 8.0; 9 |] |]

        let tensorProduct = Matrix<_>.TensorProduct a b

        Assert.Equal(
            M
                [| [| 1.0; 2.0; 3.0; 2.0; 4.0; 6.0 |]
                   [| 4.0; 5.0; 6.0; 8.0; 10.0; 12.0 |]
                   [| 7.0; 8.0; 9.0; 14.0; 16.0; 18.0 |]
                   [| 3.0; 6.0; 9.0; 4.0; 8.0; 12.0 |]
                   [| 12.0; 15.0; 18.0; 16.0; 20.0; 24.0 |]
                   [| 21.0; 24.0; 27.0; 28.0; 32.0; 36.0 |] |],
            tensorProduct
        )

    [<Fact>]
    let ``Tensor product is associative`` () =
        let a = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let b = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        let c = M [| [| 59.0; 67 |]; [| 73.0; 83 |] |]

        Assert.Equal(
            Matrix<_>.TensorProduct (Matrix<_>.TensorProduct a b) c,
            Matrix<_>.TensorProduct a (Matrix<_>.TensorProduct b c)
        )

    [<Fact>]
    let ``Matrix multiplied by it's eigen vector equals to eigen value multiplied by eigen vector`` () =
        let matrix = M [| [| 4.0; -1.0 |]; [| 2.0; 1.0 |] |]

        let eigenVector = V [| 1.0; 1.0 |]

        let eigenValue = 3.0

        Assert.Equal(matrix * eigenVector, eigenValue * eigenVector)
