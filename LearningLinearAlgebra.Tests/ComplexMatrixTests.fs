namespace Algebra

open Xunit

module ComplexMatrixTests =

    open ComplexNumbers

    [<Fact>]
    let ``Matrix with one element can be presented as scalar`` () =
        let matrix = M [| [| C(1, 2) |] |]

        let vector = Matrix.AsScalar matrix

        Assert.Equal(Complex(1, 2), vector)

    [<Fact>]
    let ``Matrix with one column can be presented as a vector`` () =
        let matrix = M [| [| C(1, 1) |]; [| C(2, 2) |]; [| C(3, 2) |] |]


        let vector = Matrix.AsVector matrix

        Assert.Equal(V [| C(1, 1); C(2, 2); C(3, 2) |], vector)

    [<Fact>]
    let ``Sum of two matrices is calculated as sum of the components`` () =
        let a = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let b = M [| [| C(23, 29); C(31, 37) |]; [| C(41, 43); C(47, 53) |] |]

        let sum = Matrix.Add a b

        Assert.Equal(M [| [| C(24, 31); C(34, 42) |]; [| C(48, 54); C(60, 72) |] |], sum)

        Assert.Equal(Matrix.Add a b, a + b)

    [<Fact>]
    let ``Sum of complex matrices is commutative`` () =
        let a = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let b = M [| [| C(23, 29); C(31, 37) |]; [| C(41, 43); C(47, 53) |] |]

        Assert.Equal(a + b, b + a)

    [<Fact>]
    let ``Sum of complex matrices is associative`` () =
        let a = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let b = M [| [| C(23, 29); C(31, 37) |]; [| C(41, 43); C(47, 53) |] |]

        let c = M [| [| C(59, 61); C(67, 71) |]; [| C(73, 79); C(83, 89) |] |]


        Assert.Equal((a + b) + c, a + (b + c))

    [<Fact>]
    let ``Sum of matrix and it's the inverse is zero`` () =
        let matrix = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let zero = Matrix.Zero 2 2

        Assert.Equal(zero, matrix + (-matrix))

    [<Fact>]
    let ``Zero is an additive identity`` () =
        let matrix = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]


        let zero = Matrix.Zero 2 2

        Assert.Equal(matrix, matrix + zero)
        Assert.Equal(matrix, zero + matrix)

    [<Fact>]
    let ``Difference of two matrices is calculated as difference of the components`` () =
        let a = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let b = M [| [| C(23, 29); C(31, 37) |]; [| C(41, 43); C(47, 53) |] |]

        let difference = Matrix.Subtract a b

        Assert.Equal(
            Matrix
                [| [| C(-22.0, -27.0); C(-28.0, -32.0) |]
                   [| C(-34.0, -32.0); C(-34.0, -34.0) |] |],
            difference
        )

        Assert.Equal(Matrix.Subtract a b, a - b)

    [<Fact>]
    let ``When multiplying a matrix by scalar then each element of the matrix is multiplied by the scalar`` () =
        let scalar = C(5, 7)

        let matrix = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let multiplied = Matrix.Multiply scalar matrix

        Assert.Equal(
            Matrix
                [| [| C(-9.0, 17.0); C(-20.0, 46.0) |]
                   [| C(-42.0, 104.0); C(-68.0, 186.0) |] |],
            multiplied
        )

        Assert.Equal(Matrix.Multiply scalar matrix, scalar * matrix)

    [<Fact>]
    let ``Scalar multiplication respects complex multiplication`` () =
        let scalarA = C(3, 5)

        let scalarB = C(7, 11)

        let matrix = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]


        Assert.Equal(scalarA * (scalarB * matrix), (scalarA * scalarB) * matrix)

    [<Fact>]
    let ``Scalar multiplication distributes over addition`` () =
        let scalar = C(3, 5)

        let matrixA = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let matrixB = M [| [| C(23, 29); C(31, 37) |]; [| C(41, 43); C(47, 53) |] |]

        Assert.Equal(scalar * (matrixA + matrixB), (scalar * matrixA) + (scalar * matrixB))

    [<Fact>]
    let ``Scalar multiplication distributes over complex addition`` () =
        let scalarA = C(3, 5)
        let scalarB = C(7, 11)

        let matrix = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        Assert.Equal((scalarA + scalarB) * matrix, (scalarA * matrix) + (scalarB * matrix))

    [<Fact>]
    let ``Transposing a matrix flips the rows and columns`` () =
        let matrix =
            M [| [| C(1, 1); C(2, 2); C(3, 3) |]; [| C(4, 4); C(5, 5); C(6, 6) |] |]

        let transposed = Matrix.Transpose matrix

        Assert.Equal(M [| [| C(1, 1); C(4, 4) |]; [| C(2, 2); C(5, 5) |]; [| C(3, 3); C(6, 6) |] |], transposed)

    [<Fact>]
    let ``Example of transposing a square matrix`` () =
        let matrix = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let transposed = Matrix.Transpose matrix

        Assert.Equal(M [| [| C(1, 2); C(7, 11) |]; [| C(3, 5); C(13, 19) |] |], transposed)

    [<Fact>]
    let ``Transposing a row vector produces a column vector`` () =
        let matrix = M [| [| C(1, 1); C(2, 2); C(3, 3) |] |]

        let transposed = Matrix.Transpose matrix

        Assert.Equal(M [| [| C(1, 1) |]; [| C(2, 2) |]; [| C(3, 3) |] |], transposed)

    [<Fact>]
    let ``Transposing a vector changes it to single row matrix`` () =
        let vector = V [| C(1, 2); C(3, 4); C(5, 6) |]

        let multiplied = Matrix.Transpose vector

        Assert.Equal(M [| [| C(1, 2); C(3, 4); C(5, 6) |] |], multiplied)

    [<Fact>]
    let ``Conjucate of a matrix is a matrix where each element is a complex conjucate of the original matrix`` () =
        let matrix = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let conjucate = Matrix.Conjucate matrix

        Assert.Equal(M [| [| C(1, -2); C(3, -5) |]; [| C(7, -11); C(13, -19) |] |], conjucate)

    [<Fact>]
    let ``Conjucate of a vector is a vector where each element is a complex conjucate of the original vector`` () =
        let vector = V [| C(1, 2); C(3, 5); C(7, 11); C(13, 19) |]

        let conjucate = Matrix.Conjucate vector

        Assert.Equal(V [| C(1, -2); C(3, -5); C(7, -11); C(13, -19) |], conjucate)

    [<Fact>]
    let ``Adjoint is the combination of transpose and conjucate`` () =
        let matrix = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let adjointed = Matrix.Adjoint matrix

        Assert.Equal(M [| [| C(1, -2); C(7, -11) |]; [| C(3, -5); C(13, -19) |] |], adjointed)

    [<Fact>]
    let ``Adjoint of vector is the combination of transpose and conjucate`` () =
        let vector = V [| C(1, 2); C(3, 5) |]

        let adjointed = Matrix.Adjoint vector

        Assert.Equal(M [| [| C(1, -2); C(3, -5) |] |], adjointed)

    [<Fact>]
    let ``Matrix product is the result of multiplying rows by columns`` () =
        let a = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let b = M [| [| C(23, 29); C(31, 37) |]; [| C(41, 43); C(47, 53) |] |]

        let product = Matrix.Product a b

        Assert.Equal(
            Matrix
                [| [| C(-127.0, 409.0); C(-167.0, 493.0) |]
                   [| C(-442.0, 1794.0); C(-586.0, 2182.0) |] |],
            product
        )

        Assert.Equal(Matrix.Product a b, a * b)


    [<Fact>]
    let ``Another example of matrix multiplication`` () =
        let a =
            Matrix
                [| [| C(3, 2); C(0, 0); C(5, -6) |]
                   [| C(1, 0); C(4, 2); C(0, 1) |]
                   [| C(4, -1); C(0, 0); C(4, 0) |] |]

        let b =
            Matrix
                [| [| C(5, 0); C(2, -1); C(6, -4) |]
                   [| C(0, 0); C(4, 5); C(2, 0) |]
                   [| C(7, -4); C(2, 7); C(0, 0) |] |]


        let product = Matrix.Product a b

        Assert.Equal(
            Matrix
                [| [| C(26, -52); C(60, 24); C(26, 0) |]
                   [| C(9, 7); C(1, 29); C(14, 0) |]
                   [| C(48, -21); C(15, 22); C(20, -22) |] |],
            product
        )

        Assert.Equal(Matrix.Product a b, a * b)

    [<Fact>]
    let ``On identity matrix diagonal entries has value one and everytinhg else is zeroes`` () =

        let identity = M [| [| C(1, 0); C(0, 0) |]; [| C(0, 0); C(1, 0) |] |]


        Assert.Equal(identity, Matrix.Identity 2)

    [<Fact>]
    let ``Another example of identity matrix`` () =

        let identity =
            Matrix
                [| [| C(1, 0); C(0, 0); C(0, 0) |]
                   [| C(0, 0); C(1, 0); C(0, 0) |]
                   [| C(0, 0); C(0, 0); C(1, 0) |] |]

        Assert.Equal(identity, Matrix.Identity 3)


    [<Fact>]
    let ``Multiplying matrix by identity matrix does not change the matrix`` () =
        let matrix =
            Matrix
                [| [| C(1, 2); C(3, 5); C(7, 11) |]
                   [| C(7, 11); C(13, 19); C(23, 29) |]
                   [| C(31, 37); C(41, 43); C(47, 53) |] |]

        let identity =
            Matrix
                [| [| C(1, 0); C(0, 0); C(0, 0) |]
                   [| C(0, 0); C(1, 0); C(0, 0) |]
                   [| C(0, 0); C(0, 0); C(1, 0) |] |]

        let product = Matrix.Product matrix identity

        Assert.Equal(matrix, product)

    [<Fact>]
    let ``Algebra of matrices acts on vectors to yield new vectors`` () =

        let matrix = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let vector = V [| C(23, 29); C(31, 37) |]

        let vectorAsMatrix = M [| [| C(23, 29) |]; [| C(31, 37) |] |]

        let resultOfAction = Matrix.Act matrix vector

        Assert.Equal(V [| C(-127.0, 341.0); C(-458.0, 1526.0) |], resultOfAction)

        Assert.Equal(Matrix.Act matrix vector, matrix * vector)

    [<Fact>]
    let ``Matrix is hermitian if adjoint of matrix does not change the matrix`` () =
        let matrix =
            Matrix
                [| [| C(1, 0); C(3, 4); C(5, 6) |]
                   [| C(3, -4); C(7, 0); C(10, 0) |]
                   [| C(5, -6); C(10, 0); C(9, 0) |] |]

        Assert.True(Matrix.IsHermitian matrix)

    [<Fact>]
    let ``Another example of hermitian matrix`` () =
        let matrix =
            Matrix
                [| [| C(5, 0); C(4, 5); C(6, -16) |]
                   [| C(4, -5); C(13, 0); C(7, 0) |]
                   [| C(6, 16); C(7, 0); C((-2.1, 0)) |] |]


        Assert.True(Matrix.IsHermitian matrix)

    [<Fact>]
    let ``Third example of hermitian matrix`` () =
        let matrix = M [| [| C(7, 0); C(6, 5) |]; [| C(6, -5); C(-3, 0) |] |]

        Assert.True(Matrix.IsHermitian matrix)

    [<Fact>]
    let ``Fourth example of hermitian matrix`` () =
        let matrix =
            Matrix
                [| [| C(1, 0); C(2, 0); C(3, 0) |]
                   [| C(2, 0); C(2, 0); C(3, 0) |]
                   [| C(3, 0); C(3, 0); C(9, 0) |] |]

        Assert.True(Matrix.IsHermitian matrix)


    [<Fact>]
    let ``Example of non-hermitian matrix`` () =
        let matrix = M [| [| C(7, 0); C(6, 5) |]; [| C(6, 5); C(3, 0) |] |]


        Assert.False(Matrix.IsHermitian matrix)

    [<Fact>]
    let ``If A is hermitian matrix then inner product of A*V and V' is equal to inner product of V and A*V'`` () =

        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let hermitian = M [| [| C(7, 0); C(6, 5) |]; [| C(6, -5); C(-3, 0) |] |]


        Assert.Equal(Vector.InnerProduct (hermitian * a) b, Vector.InnerProduct a (hermitian * b))

    [<Fact>]
    let ``Matrix is unitary if product of matrix and it's adjoint is equal to product of adjoint and matrix is equal to identity matrix``
        ()
        =
        let cos = cos
        let sin = sin
        let a = 10.0

        let matrix =
            Matrix
                [| [| C(cos a, 0); C(-1.0 * (sin a), 0); C(0, 0) |]
                   [| C(sin a, 0); C(cos a, 0); C(0, 0) |]
                   [| C(0, 0); C(0, 0); C(1, 0) |] |]


        Assert.True(Matrix.IsUnitary matrix)

    [<Fact>]
    let ``Another example of unitary matrix`` () =

        let matrix =
            Matrix
                [| [| C(1.0 / sqrt 2.0, 0); C(1.0 / sqrt 2.0, 0) |]
                   [| C(0, 1.0 / sqrt 2.0); C(0, -1.0 / sqrt 2.0) |] |]


        Assert.True(Matrix.IsUnitary matrix)

    [<Fact>]
    let ``Third example of unitary matrix`` () =

        let matrix = M [| [| C(1, 0); C(0, 0) |]; [| C(0, 0); C(0, 1) |] |]

        Assert.True(Matrix.IsUnitary matrix)

    [<Fact>]
    let ``Example of non-unitary matrix`` () =

        let matrix = M [| [| C(1.0, 0); C(1.0, 0) |]; [| C(0, 1.0); C(0, -1.0) |] |]

        Assert.False(Matrix.IsUnitary matrix)

    [<Fact>]
    let ``Product of unitary matrices is also unitary matrix`` () =

        let unitary =
            Matrix
                [| [| C(1.0 / sqrt 2.0, 0); C(1.0 / sqrt 2.0, 0) |]
                   [| C(0, 1.0 / sqrt 2.0); C(0, -1.0 / sqrt 2.0) |] |]

        let anotherUnitary = M [| [| C(1, 0); C(0, 0) |]; [| C(0, 0); C(0, 1) |] |]


        Assert.True(Matrix.IsUnitary(unitary * anotherUnitary))


    [<Fact>]
    let ``Unitary matrices preserve inner products`` () =

        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let unitary = M [| [| C(1, 0); C(0, 0) |]; [| C(0, 0); C(0, 1) |] |]


        Assert.Equal(Vector.InnerProduct (unitary * a) (unitary * b), Vector.InnerProduct a b)


    [<Fact>]
    let ``Unitary matrices preserve distance`` () =

        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let unitary = M [| [| C(1, 0); C(0, 0) |]; [| C(0, 0); C(0, 1) |] |]


        Assert.Equal(Vector.Distance (unitary * a) (unitary * b), Vector.Distance a b)

    [<Fact>]
    let ``Multiplying unitary matrix by it's adjoint produces an identity matrix`` () =

        let unitary =
            Matrix
                [| [| C(1.0 / sqrt 2.0, 0); C(1.0 / sqrt 2.0, 0) |]
                   [| C(0, 1.0 / sqrt 2.0); C(0, -1.0 / sqrt 2.0) |] |]

        Assert.Equal(Matrix.Identity 2, unitary * Matrix.Adjoint unitary |> Matrix.Round)

    [<Fact>]
    let ``Another example of multiplying unitary matrix by it's adjoint`` () =

        let identity =
            Matrix
                [| [| C(1.0, 0.0); C(0.0, 0.0); C(0.0, 0.0) |]
                   [| C(0.0, 0.0); C(-1.0, 0.0); C(0.0, 0.0) |]
                   [| C(0.0, 0.0); C(0.0, 0.0); C(-1.0, 0.0) |] |]

        let unitary =
            Matrix
                [| [| C(1.0 / sqrt 2.0, 0); C(1.0 / sqrt 2.0, 0); C(0, 0) |]
                   [| C(0, -1.0 / sqrt 2.0); C(0, 1.0 / sqrt 2.0); C(0, 0) |]
                   [| C(0, 0); C(0, 0); C(0, 1) |] |]

        let transposeOfunitary = Matrix.Adjoint unitary

        Assert.Equal(Matrix.Identity 3, unitary * transposeOfunitary |> Matrix.Round)

    [<Fact>]
    let ``Commutator of hermitians a and b is (a * b) - (b * a)`` () =

        let a = M [| [| C(0, 0); C(1, 0) |]; [| C(1, 0); C(0, 0) |] |]

        let b = M [| [| C(0, 0); C(0, -1) |]; [| C(0, 1); C(0, 0) |] |]

        let commutator = Matrix.Commutator a b

        Assert.Equal(M [| [| C(0, 2); C(0, 0) |]; [| C(0, 0); C(0, -2) |] |], commutator)

    [<Fact>]
    let ``Commutator of hermitians a and b is zero if hermitians are commutable`` () =

        let a = M [| [| C(1, 0); C(0, 0) |]; [| C(0, 0); C(1, 0) |] |]

        let b = M [| [| C(0, 0); C(1, 0) |]; [| C(1, 0); C(0, 0) |] |]

        let commutator = Matrix.Commutator a b

        Assert.Equal(Matrix.Zero 2 2, commutator)

    [<Fact>]
    let ``Tensor product of matrix contains combinations scalar products of all elements of both matrix`` () =

        let a = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let b = M [| [| C(23, 29); C(31, 37) |]; [| C(41, 43); C(47, 53) |] |]

        let tensorProduct = Matrix.TensorProduct a b

        Assert.Equal(
            Matrix
                [| [| C(-35.0, 75.0); C(-43.0, 99.0); C(-76.0, 202.0); C(-92.0, 266.0) |]
                   [| C(-45.0, 125.0); C(-59.0, 147.0); C(-92.0, 334.0); C(-124.0, 394.0) |]
                   [| C(-158.0, 456.0); C(-190.0, 600.0); C(-252.0, 814.0); C(-300.0, 1070.0) |]
                   [| C(-186.0, 752.0); C(-254.0, 888.0); C(-284.0, 1338.0); C(-396.0, 1582.0) |] |],
            tensorProduct
        )

    [<Fact>]
    let ``Another example of tensor product`` () =

        let a =
            Matrix
                [| [| C(3, 2); C(5, -1); C(0, 2) |]
                   [| C(0, 0); C(12, 0); C(6, -3) |]
                   [| C(2, 0); C(4, 4); C(9, 3) |] |]

        let b =
            Matrix
                [| [| C(1, 0); C(3, 4); C(5, -7) |]
                   [| C(10, 2); C(6, 0); C(2, 5) |]
                   [| C(0, 0); C(1, 0); C(2, 9) |] |]

        let tensorProduct = Matrix.TensorProduct a b

        Assert.Equal(
            Matrix
                [| [| C(3.0, 2.0)
                      C(1.0, 18.0)
                      C(29.0, -11.0)
                      C(5.0, -1.0)
                      C(19.0, 17.0)
                      C(18.0, -40.0)
                      C(0.0, 2.0)
                      C(-8.0, 6.0)
                      C(14.0, 10.0) |]
                   [| C(26.0, 26.0)
                      C(18.0, 12.0)
                      C(-4.0, 19.0)
                      C(52.0, 0.0)
                      C(30.0, -6.0)
                      C(15.0, 23.0)
                      C(-4.0, 20.0)
                      C(0.0, 12.0)
                      C(-10.0, 4.0) |]
                   [| C(0.0, 0.0)
                      C(3.0, 2.0)
                      C(-12.0, 31.0)
                      C(0.0, 0.0)
                      C(5.0, -1.0)
                      C(19.0, 43.0)
                      C(0.0, 0.0)
                      C(0.0, 2.0)
                      C(-18.0, 4.0) |]
                   [| C(0.0, 0.0)
                      C(0.0, 0.0)
                      C(0.0, 0.0)
                      C(12.0, 0.0)
                      C(36.0, 48.0)
                      C(60.0, -84.0)
                      C(6.0, -3.0)
                      C(30.0, 15.0)
                      C(9.0, -57.0) |]
                   [| C(0.0, 0.0)
                      C(0.0, 0.0)
                      C(0.0, 0.0)
                      C(120.0, 24.0)
                      C(72.0, 0.0)
                      C(24.0, 60.0)
                      C(66.0, -18.0)
                      C(36.0, -18.0)
                      C(27.0, 24.0) |]
                   [| C(0.0, 0.0)
                      C(0.0, 0.0)
                      C(0.0, 0.0)
                      C(0.0, 0.0)
                      C(12.0, 0.0)
                      C(24.0, 108.0)
                      C(0.0, 0.0)
                      C(6.0, -3.0)
                      C(39.0, 48.0) |]
                   [| C(2.0, 0.0)
                      C(6.0, 8.0)
                      C(10.0, -14.0)
                      C(4.0, 4.0)
                      C(-4.0, 28.0)
                      C(48.0, -8.0)
                      C(9.0, 3.0)
                      C(15.0, 45.0)
                      C(66.0, -48.0) |]
                   [| C(20.0, 4.0)
                      C(12.0, 0.0)
                      C(4.0, 10.0)
                      C(32.0, 48.0)
                      C(24.0, 24.0)
                      C(-12.0, 28.0)
                      C(84.0, 48.0)
                      C(54.0, 18.0)
                      C(3.0, 51.0) |]
                   [| C(0.0, 0.0)
                      C(2.0, 0.0)
                      C(4.0, 18.0)
                      C(0.0, 0.0)
                      C(4.0, 4.0)
                      C(-28.0, 44.0)
                      C(0.0, 0.0)
                      C(9.0, 3.0)
                      C(-9.0, 87.0) |] |],
            tensorProduct
        )

    [<Fact>]
    let ``Third example of tensor product`` () =

        let a = M [| [| C(1, 1); C(2, 2) |]; [| C(3, 3); C(4, 4) |] |]

        let b =
            Matrix
                [| [| C(1, 1); C(2, 2); C(3, 3) |]
                   [| C(4, 4); C(5, 5); C(6, 6) |]
                   [| C(7, 7); C(8, 8); C(9, 9) |] |]

        let tensorProduct = Matrix.TensorProduct a b

        Assert.Equal(
            Matrix
                [| [| C(0.0, 2.0)
                      C(0.0, 4.0)
                      C(0.0, 6.0)
                      C(0.0, 4.0)
                      C(0.0, 8.0)
                      C(0.0, 12.0) |]
                   [| C(0.0, 8.0)
                      C(0.0, 10.0)
                      C(0.0, 12.0)
                      C(0.0, 16.0)
                      C(0.0, 20.0)
                      C(0.0, 24.0) |]
                   [| C(0.0, 14.0)
                      C(0.0, 16.0)
                      C(0.0, 18.0)
                      C(0.0, 28.0)
                      C(0.0, 32.0)
                      C(0.0, 36.0) |]
                   [| C(0.0, 6.0)
                      C(0.0, 12.0)
                      C(0.0, 18.0)
                      C(0.0, 8.0)
                      C(0.0, 16.0)
                      C(0.0, 24.0) |]
                   [| C(0.0, 24.0)
                      C(0.0, 30.0)
                      C(0.0, 36.0)
                      C(0.0, 32.0)
                      C(0.0, 40.0)
                      C(0.0, 48.0) |]
                   [| C(0.0, 42.0)
                      C(0.0, 48.0)
                      C(0.0, 54.0)
                      C(0.0, 56.0)
                      C(0.0, 64.0)
                      C(0.0, 72.0) |] |],
            tensorProduct
        )

    [<Fact>]
    let ``Tensor product is associative`` () =
        let a = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let b = M [| [| C(23, 29); C(31, 37) |]; [| C(41, 43); C(47, 53) |] |]

        let c = M [| [| C(59, 61); C(67, 71) |]; [| C(73, 79); C(83, 89) |] |]

        Assert.Equal(
            Matrix.TensorProduct (Matrix.TensorProduct a b) c,
            Matrix.TensorProduct a (Matrix.TensorProduct b c)
        )

    [<Fact>]
    let ``Matrix multiplied by it's eigen vector equals to eigen value multiplied by eigen vector`` () =
        let matrix = M [| [| C(4, 0); C(-1, 0) |]; [| C(2, 0); C(1, 0) |] |]

        let eigenVector = V [| C(1, 0); C(1, 0) |]

        let eigenValue = 3.0

        Assert.Equal(matrix * eigenVector, eigenValue * eigenVector)
