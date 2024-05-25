namespace LearningLinearAlgebra.Numbers

open Xunit

module RealMatrixTests =

    open LearningLinearAlgebra
    open LearningLinearAlgebra.Matrices.Real

    [<Fact>]
    let ``Sum of two matrices is calculated as sum of the components`` () =
        let a = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let b = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        let sum = SquareMatrix.Add a b

        sum
        |> Should.BeEquivalentTo.Real.SquareMatrix(M [| [| 24.0; 34 |]; [| 48.0; 60 |] |])

        a + b |> Should.BeEquivalentTo.Real.SquareMatrix(SquareMatrix.Add a b)

    [<Fact>]
    let ``Sum of complex matrices is commutative`` () =
        let a = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let b = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        b + a |> Should.BeEquivalentTo.Real.SquareMatrix(a + b)

    [<Fact>]
    let ``Sum of complex matrices is associative`` () =
        let a = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let b = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        let c = M [| [| 59.0; 67 |]; [| 73.0; 83 |] |]

        a + (b + c) |> Should.BeEquivalentTo.Real.SquareMatrix((a + b) + c)

    [<Fact>]
    let ``Sum of matrix and it's the inverse is zero`` () =
        let matrix = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let zero = SquareMatrix.Zero 2

        matrix + (-matrix) |> Should.BeEquivalentTo.Real.SquareMatrix(zero)

    [<Fact>]
    let ``Zero is an additive identity`` () =
        let matrix = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let zero = SquareMatrix.Zero 2

        matrix + zero |> Should.BeEquivalentTo.Real.SquareMatrix(matrix)
        zero + matrix |> Should.BeEquivalentTo.Real.SquareMatrix(matrix)

    [<Fact>]
    let ``Difference of two matrices is calculated as difference of the components`` () =
        let a = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let b = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        let difference = SquareMatrix.Subtract a b

        difference
        |> Should.BeEquivalentTo.Real.SquareMatrix(M [| [| -22.0; -28 |]; [| -34.0; -34 |] |])

        a - b |> Should.BeEquivalentTo.Real.SquareMatrix(SquareMatrix.Subtract a b)

    [<Fact>]
    let ``When multiplying a matrix by scalar then each element of the matrix is multiplied by the scalar`` () =
        let scalar = 5.0

        let matrix = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let multiplied = SquareMatrix.Multiply scalar matrix

        multiplied
        |> Should.BeEquivalentTo.Real.SquareMatrix(M [| [| 5.0; 15.0 |]; [| 35.0; 65.0 |] |])

        scalar * matrix
        |> Should.BeEquivalentTo.Real.SquareMatrix(SquareMatrix.Multiply scalar matrix)

    [<Fact>]
    let ``Scalar multiplication respects complex multiplication`` () =
        let scalarA = 3.0

        let scalarB = 7.0

        let matrix = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        (scalarA * scalarB) * matrix
        |> Should.BeEquivalentTo.Real.SquareMatrix(scalarA * (scalarB * matrix))

    [<Fact>]
    let ``Scalar multiplication distributes over addition`` () =
        let scalar = 3.0

        let matrixA = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let matrixB = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        (scalar * matrixA) + (scalar * matrixB)
        |> Should.BeEquivalentTo.Real.SquareMatrix(scalar * (matrixA + matrixB))

    [<Fact>]
    let ``Scalar multiplication distributes over complex addition`` () =
        let scalarA = 3.0
        let scalarB = 7.0

        let matrix = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        (scalarA * matrix) + (scalarB * matrix)
        |> Should.BeEquivalentTo.Real.SquareMatrix((scalarA + scalarB) * matrix)

    [<Fact>]
    let ``Transposing a matrix flips the rows and columns`` () =
        let matrix = M [| [| 1.0; 2.0 |]; [| 3.0; 4.0 |] |]

        let transposed = SquareMatrix.Transpose matrix

        transposed
        |> Should.BeEquivalentTo.Real.SquareMatrix(M [| [| 1.0; 3.0 |]; [| 2.0; 4.0 |] |])

    [<Fact>]
    let ``Example of transposing a square matrix`` () =
        let matrix = M [| [| 1.0; 3.0 |]; [| 7.0; 13.0 |] |]

        let transposed = SquareMatrix.Transpose matrix

        transposed
        |> Should.BeEquivalentTo.Real.SquareMatrix(M [| [| 1.0; 7 |]; [| 3.0; 13 |] |])

    [<Fact>]
    let ``Transposing a row vector produces a column vector`` () =
        let matrix = U [| 1.0; 2.0; 3.0 |]

        let transposed = RowVector.Transpose matrix

        transposed |> Should.BeEquivalentTo.Real.ColumnVector(V [| 1.0; 2.0; 3.0 |])

    [<Fact>]
    let ``Transposing a column vector changes it to single row matrix`` () =
        let vector = V [| 1.0; 2.0; 3 |]

        let transposed = ColumnVector.Transpose vector

        transposed |> Should.BeEquivalentTo.Real.RowVector(U [| 1.0; 2.0; 3.0 |])

    [<Fact>]
    let ``SquareMatrix product is the result of multiplying rows by columns`` () =
        let a = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let b = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        let product = SquareMatrix.Product a b

        product
        |> Should.BeEquivalentTo.Real.SquareMatrix(M [| [| 146.0; 172.0 |]; [| 694.0; 828.0 |] |])

        a * b |> Should.BeEquivalentTo.Real.SquareMatrix(SquareMatrix.Product a b)

    [<Fact>]
    let ``On identity matrix diagonal entries has value one and everytinhg else is zeroes`` () =

        let identity = M [| [| 1.0; 0 |]; [| 0.0; 1 |] |]

        SquareMatrix.Identity 2 |> Should.BeEquivalentTo.Real.SquareMatrix(identity)

    [<Fact>]
    let ``Another example of identity matrix`` () =

        let identity = M [| [| 1.0; 0.0; 0 |]; [| 0.0; 1.0; 0 |]; [| 0.0; 0.0; 1 |] |]

        SquareMatrix.Identity 3 |> Should.BeEquivalentTo.Real.SquareMatrix(identity)

    [<Fact>]
    let ``Multiplying matrix by identity matrix does not change the matrix`` () =
        let matrix = M [| [| 1.0; 3.0; 7 |]; [| 7.0; 13.0; 23 |]; [| 31.0; 41.0; 47 |] |]

        let identity = M [| [| 1.0; 0.0; 0 |]; [| 0.0; 1.0; 0 |]; [| 0.0; 0.0; 1 |] |]

        let product = SquareMatrix.Product matrix identity

        product |> Should.BeEquivalentTo.Real.SquareMatrix(matrix)

    [<Fact>]
    let ``Multiplying orthogonal matrix by it's transpose produces an identity matrix`` () =

        let orthogonal = M [| [| 0.0; 1.0; 0 |]; [| 0.0; 0.0; 1 |]; [| 1.0; 0.0; 0 |] |]

        let transposeOfOrthogonal = SquareMatrix.Transpose orthogonal

        orthogonal * transposeOfOrthogonal
        |> Should.BeEquivalentTo.Real.SquareMatrix(SquareMatrix.Identity 3)

        transposeOfOrthogonal * orthogonal
        |> Should.BeEquivalentTo.Real.SquareMatrix(SquareMatrix.Identity 3)

    [<Fact>]
    let ``LearningLinearAlgebra.FSharp of matrices acts on vectors to yield new vectors`` () =

        let matrix = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let vector = V [| 23.0; 31 |]

        let resultOfAction = SquareMatrix.Act matrix vector

        resultOfAction |> Should.BeEquivalentTo.Real.ColumnVector(V [| 116.0; 564.0 |])

        matrix * vector
        |> Should.BeEquivalentTo.Real.ColumnVector(SquareMatrix.Act matrix vector)


    [<Fact>]
    let ``Tensor product of matrix contains combinations scalar products of all elements of both matrix`` () =

        let a = M [| [| 1.0; 2.0 |]; [| 3.0; 4.0 |] |]

        let b = M [| [| 5.0; 6.0 |]; [| 7.0; 8.0 |] |]

        let tensorProduct = SquareMatrix.TensorProduct a b

        tensorProduct
        |> Should.BeEquivalentTo.Real.SquareMatrix(
            M
                [| [| 5.0; 6.0; 10.0; 12.0 |]
                   [| 7.0; 8.0; 14.0; 16.0 |]
                   [| 15.0; 18.0; 20.0; 24.0 |]
                   [| 21.0; 24.0; 28.0; 32.0 |] |]
        )

    [<Fact>]
    let ``Another example of tensor product`` () =

        let a = M [| [| 1.0; 2 |]; [| 3.0; 4 |] |]

        let b = M [| [| 1.0; 2.0; 3 |]; [| 4.0; 5.0; 6 |]; [| 7.0; 8.0; 9 |] |]

        let tensorProduct = SquareMatrix.TensorProduct a b

        tensorProduct
        |> Should.BeEquivalentTo.Real.SquareMatrix(
            M
                [| [| 1.0; 2.0; 3.0; 2.0; 4.0; 6.0 |]
                   [| 4.0; 5.0; 6.0; 8.0; 10.0; 12.0 |]
                   [| 7.0; 8.0; 9.0; 14.0; 16.0; 18.0 |]
                   [| 3.0; 6.0; 9.0; 4.0; 8.0; 12.0 |]
                   [| 12.0; 15.0; 18.0; 16.0; 20.0; 24.0 |]
                   [| 21.0; 24.0; 27.0; 28.0; 32.0; 36.0 |] |]
        )

    [<Fact>]
    let ``Tensor product is associative`` () =
        let a = M [| [| 1.0; 3 |]; [| 7.0; 13 |] |]

        let b = M [| [| 23.0; 31 |]; [| 41.0; 47 |] |]

        let c = M [| [| 59.0; 67 |]; [| 73.0; 83 |] |]

        SquareMatrix.TensorProduct a (SquareMatrix.TensorProduct b c)
        |> Should.BeEquivalentTo.Real.SquareMatrix(SquareMatrix.TensorProduct (SquareMatrix.TensorProduct a b) c)


    [<Fact>]
    let ``SquareMatrix multiplied by it's eigen vector equals to eigen value multiplied by eigen vector`` () =
        let matrix = M [| [| 4.0; -1.0 |]; [| 2.0; 1.0 |] |]

        let eigenVector = V [| 1.0; 1.0 |]

        let eigenValue = 3.0

        eigenValue * eigenVector
        |> Should.BeEquivalentTo.Real.ColumnVector(matrix * eigenVector)
