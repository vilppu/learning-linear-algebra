namespace Algebra

open Xunit

module RealMatrixTests =

    open RealNumbers

    [<Fact>]
    let ``Matrix with one element can be presented as scalar`` () =
        let matrix = Matrix [| [| 123 |] |]

        let vector = Matrix.AsScalar matrix

        Assert.Equal(123.0, vector)

    [<Fact>]
    let ``Matrix with one column can be presented as a vector`` () =
        let matrix = Matrix [| [| 1 |]; [| 2 |]; [| 3 |] |]

        let vector = Matrix.AsVector matrix

        Assert.Equal(Vector [| 1; 2; 3 |], vector)

    [<Fact>]
    let ``Sum of two matrices is calculated as sum of the components`` () =
        let a = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        let b = Matrix [| [| 23; 31 |]; [| 41; 47 |] |]

        let sum = Matrix.Add a b

        Assert.Equal(Matrix [| [| 24; 34 |]; [| 48; 60 |] |], sum)

        Assert.Equal(Matrix.Add a b, a + b)

    [<Fact>]
    let ``Sum of complex matrices is commutative`` () =
        let a = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        let b = Matrix [| [| 23; 31 |]; [| 41; 47 |] |]

        Assert.Equal(a + b, b + a)

    [<Fact>]
    let ``Sum of complex matrices is associative`` () =
        let a = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        let b = Matrix [| [| 23; 31 |]; [| 41; 47 |] |]

        let c = Matrix [| [| 59; 67 |]; [| 73; 83 |] |]

        Assert.Equal((a + b) + c, a + (b + c))

    [<Fact>]
    let ``Sum of matrix and it's the inverse is zero`` () =
        let matrix = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        let zero = Matrix.Zero 2 2

        Assert.Equal(zero, matrix + (-matrix))

    [<Fact>]
    let ``Zero is an additive identity`` () =
        let matrix = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        let zero = Matrix.Zero 2 2

        Assert.Equal(matrix, matrix + zero)
        Assert.Equal(matrix, zero + matrix)

    [<Fact>]
    let ``Difference of two matrices is calculated as difference of the components`` () =
        let a = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        let b = Matrix [| [| 23; 31 |]; [| 41; 47 |] |]

        let difference = Matrix.Subtract a b

        Assert.Equal(
            Matrix [| [| -22; -28 |]
                      [| -34; -34 |] |],
            difference
        )

        Assert.Equal(Matrix.Subtract a b, a - b)

    [<Fact>]
    let ``When multiplying a matrix by scalar then each element of the matrix is multiplied by the scalar`` () =
        let scalar = 5.0

        let matrix = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        let multiplied = Matrix.Multiply scalar matrix

        Assert.Equal(
            Matrix [| [| 5.0; 15.0 |]
                      [| 35.0; 65.0 |] |],
            multiplied
        )

        Assert.Equal(Matrix.Multiply scalar matrix, scalar * matrix)

    [<Fact>]
    let ``Scalar multiplication respects complex multiplication`` () =
        let scalarA = 3.0

        let scalarB = 7.0

        let matrix = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        Assert.Equal(scalarA * (scalarB * matrix), (scalarA * scalarB) * matrix)

    [<Fact>]
    let ``Scalar multiplication distributes over addition`` () =
        let scalar = 3.0

        let matrixA = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        let matrixB = Matrix [| [| 23; 31 |]; [| 41; 47 |] |]

        Assert.Equal(scalar * (matrixA + matrixB), (scalar * matrixA) + (scalar * matrixB))

    [<Fact>]
    let ``Scalar multiplication distributes over complex addition`` () =
        let scalarA = 3.0
        let scalarB = 7.0

        let matrix = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        Assert.Equal((scalarA + scalarB) * matrix, (scalarA * matrix) + (scalarB * matrix))

    [<Fact>]
    let ``Transposing a matrix flips the rows and columns`` () =
        let matrix =
            Matrix [| [| 1; 2; 3 |]
                      [| 4; 5; 6 |] |]

        let transposed = Matrix.Transpose matrix

        Assert.Equal(
            Matrix [| [| 1; 4 |]
                      [| 2; 5 |]
                      [| 3; 6 |] |],
            transposed
        )

    [<Fact>]
    let ``Example of transposing a square matrix`` () =
        let matrix = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        let transposed = Matrix.Transpose matrix

        Assert.Equal(Matrix [| [| 1; 7 |]; [| 3; 13 |] |], transposed)

    [<Fact>]
    let ``Transposing a row vector produces a column vector`` () =
        let matrix = Matrix [| [| 1; 2; 3 |] |]

        let transposed = Matrix.Transpose matrix

        Assert.Equal(Matrix [| [| 1 |]; [| 2 |]; [| 3 |] |], transposed)

    [<Fact>]
    let ``Transposing a vector changes it to single column matrix`` () =
        let vector = Vector [| 1; 2; 3 |]

        let multiplied = Matrix.Transpose vector

        Assert.Equal(Matrix [| [| 1 |]; [| 2 |]; [| 3 |] |], multiplied)

    [<Fact>]
    let ``Matrix product is the result of multiplying rows by columns`` () =
        let a = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        let b = Matrix [| [| 23; 31 |]; [| 41; 47 |] |]

        let product = Matrix.Product a b

        Assert.Equal(
            Matrix [| [| 146.0; 172.0 |]
                      [| 694.0; 828.0 |] |],
            product
        )

        Assert.Equal(Matrix.Product a b, a * b)

    [<Fact>]
    let ``On identity matrix diagonal entries has value one and everytinhg else is zeroes`` () =

        let identity = Matrix [| [| 1; 0 |]; [| 0; 1 |] |]

        Assert.Equal(identity, Matrix.Identity 2)

    [<Fact>]
    let ``Another example of identity matrix`` () =

        let identity =
            Matrix [| [| 1; 0; 0 |]
                      [| 0; 1; 0 |]
                      [| 0; 0; 1 |] |]

        Assert.Equal(identity, Matrix.Identity 3)

    [<Fact>]
    let ``Multiplying matrix by identity matrix does not change the matrix`` () =
        let matrix =
            Matrix [| [| 1; 3; 7 |]
                      [| 7; 13; 23 |]
                      [| 31; 41; 47 |] |]

        let identity =
            Matrix [| [| 1; 0; 0 |]
                      [| 0; 1; 0 |]
                      [| 0; 0; 1 |] |]

        let product = Matrix.Product matrix identity

        Assert.Equal(matrix, product)

    [<Fact>]
    let ``Multiplying orthogonal matrix by it's transpose produces an identity matrix`` () =

        let orthogonal =
            Matrix [| [| 0; 1; 0 |]
                      [| 0; 0; 1 |]
                      [| 1; 0; 0 |] |]

        let transposeOfOrthogonal = Matrix.Transpose orthogonal

        Assert.Equal(Matrix.Identity 3, orthogonal * transposeOfOrthogonal)
        Assert.Equal(Matrix.Identity 3, transposeOfOrthogonal * orthogonal)

    [<Fact>]
    let ``Algebra of matrices acts on vectors to yield new vectors`` () =

        let matrix = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        let vector = Vector [| 23; 31 |]

        let vectorAsMatrix = Matrix [| [| 23 |]; [| 31 |] |]

        let resultOfAction = Matrix.Act matrix vector

        Assert.Equal(Vector [| 116.0; 564.0 |], resultOfAction)

        Assert.Equal(Matrix.Act matrix vector, matrix * vector)


    [<Fact>]
    let ``Tensor product of matrix contains combinations scalar products of all elements of both matrix`` () =

        let a = Matrix [| [| 1; 2 |]; [| 3; 4 |] |]

        let b = Matrix [| [| 5; 6 |]; [| 7; 8 |] |]

        let tensorProduct = Matrix.TensorProduct a b

        Assert.Equal(
            Matrix [| [| 5.0; 6.0; 10.0; 12.0 |]
                      [| 7.0; 8.0; 14.0; 16.0 |]
                      [| 15.0; 18.0; 20.0; 24.0 |]
                      [| 21.0; 24.0; 28.0; 32.0 |] |],
            tensorProduct
        )

    [<Fact>]
    let ``Another example of tensor product`` () =

        let a = Matrix [| [| 1; 2 |]; [| 3; 4 |] |]

        let b =
            Matrix [| [| 1; 2; 3 |]
                      [| 4; 5; 6 |]
                      [| 7; 8; 9 |] |]

        let tensorProduct = Matrix.TensorProduct a b

        Assert.Equal(
            Matrix [| [| 1.0; 2.0; 3.0; 2.0; 4.0; 6.0 |]
                      [| 4.0; 5.0; 6.0; 8.0; 10.0; 12.0 |]
                      [| 7.0; 8.0; 9.0; 14.0; 16.0; 18.0 |]
                      [| 3.0; 6.0; 9.0; 4.0; 8.0; 12.0 |]
                      [| 12.0; 15.0; 18.0; 16.0; 20.0; 24.0 |]
                      [| 21.0; 24.0; 27.0; 28.0; 32.0; 36.0 |] |],
            tensorProduct
        )

    [<Fact>]
    let ``Tensor product is associative`` () =
        let a = Matrix [| [| 1; 3 |]; [| 7; 13 |] |]

        let b = Matrix [| [| 23; 31 |]; [| 41; 47 |] |]

        let c = Matrix [| [| 59; 67 |]; [| 73; 83 |] |]

        Assert.Equal(
            Matrix.TensorProduct (Matrix.TensorProduct a b) c,
            Matrix.TensorProduct a (Matrix.TensorProduct b c)
        )

    [<Fact>]
    let ``Matrix multiplied by it's eigen vector equals to eigen value multiplied by eigen vector`` () =
        let matrix = Matrix [| [| 4; -1 |]; [| 2; 1 |] |]

        let eigenVector = Vector [| 1; 1 |]

        let eigenValue = 3.0

        Assert.Equal(matrix * eigenVector, eigenValue * eigenVector)
