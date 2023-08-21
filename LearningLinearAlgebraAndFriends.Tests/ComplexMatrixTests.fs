namespace Algebra

open Xunit

module ComplexMatrixTests =

    open ComplexVectorSpace
    open Algebra.ComplexNumbers.CartesianPresentation

    [<Fact>]
    let ``m x 1 matrix can be presentented as n vector and vice versa`` () =
        let matrix =
            Matrix(
                [| [| Complex(1, 2) |]
                   [| Complex(7, 11) |] |]
            )

        let vector = Matrix.ToVector matrix

        Assert.Equal(Vector([| Complex(1, 2); Complex(7, 11) |]), vector)
        Assert.Equal(matrix, Matrix.FromVector vector)

    [<Fact>]
    let ``Sum of two matrices is calculated as sum of the components`` () =
        let a =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        let b =
            Matrix(
                [| [| Complex(23, 29); Complex(31, 37) |]
                   [| Complex(41, 43); Complex(47, 53) |] |]
            )

        let sum = Matrix.Add a b

        Assert.Equal(
            Matrix(
                [| [| Complex(24, 31); Complex(34, 42) |]
                   [| Complex(48, 54); Complex(60, 72) |] |]
            ),
            sum
        )

        Assert.Equal(Matrix.Add a b, a + b)

    [<Fact>]
    let ``Sum of complex matrices is commutative`` () =
        let a =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        let b =
            Matrix(
                [| [| Complex(23, 29); Complex(31, 37) |]
                   [| Complex(41, 43); Complex(47, 53) |] |]
            )

        Assert.Equal(a + b, b + a)

    [<Fact>]
    let ``Sum of complex matrices is associative`` () =
        let a =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        let b =
            Matrix(
                [| [| Complex(23, 29); Complex(31, 37) |]
                   [| Complex(41, 43); Complex(47, 53) |] |]
            )

        let c =
            Matrix(
                [| [| Complex(59, 61); Complex(67, 71) |]
                   [| Complex(73, 79); Complex(83, 89) |] |]
            )

        Assert.Equal((a + b) + c, a + (b + c))

    [<Fact>]
    let ``Sum of matrix and it's the inverse is zero`` () =
        let matrix =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        let zero = Matrix.Zero 2 2

        Assert.Equal(zero, matrix + (-matrix))

    [<Fact>]
    let ``Zero is an additive identity`` () =
        let matrix =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        let zero = Matrix.Zero 2 2

        Assert.Equal(matrix, matrix + zero)
        Assert.Equal(matrix, zero + matrix)

    [<Fact>]
    let ``Difference of two matrices is calculated as difference of the components`` () =
        let a =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        let b =
            Matrix(
                [| [| Complex(23, 29); Complex(31, 37) |]
                   [| Complex(41, 43); Complex(47, 53) |] |]
            )

        let difference = Matrix.Subtract a b

        Assert.Equal(
            Matrix(
                [| [| Complex(-22.0, -27.0)
                      Complex(-28.0, -32.0) |]
                   [| Complex(-34.0, -32.0)
                      Complex(-34.0, -34.0) |] |]
            ),
            difference
        )

        Assert.Equal(Matrix.Subtract a b, a - b)

    [<Fact>]
    let ``When multiplying a matrix by scalar then each element of the matrix is multiplied by the scalar`` () =
        let scalar = Complex(5, 7)

        let matrix =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        let multiplied = Matrix.Multiply scalar matrix

        Assert.Equal(
            Matrix(
                [| [| Complex(-9.0, 17.0)
                      Complex(-20.0, 46.0) |]
                   [| Complex(-42.0, 104.0)
                      Complex(-68.0, 186.0) |] |]
            ),
            multiplied
        )

        Assert.Equal(Matrix.Multiply scalar matrix, scalar * matrix)

    [<Fact>]
    let ``Scalar multiplication respects complex multiplication`` () =
        let scalarA = Complex(3, 5)

        let scalarB = Complex(7, 11)

        let matrix =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        Assert.Equal(scalarA * (scalarB * matrix), (scalarA * scalarB) * matrix)

    [<Fact>]
    let ``Scalar multiplication distributes over addition`` () =
        let scalar = Complex(3, 5)

        let matrixA =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        let matrixB =
            Matrix(
                [| [| Complex(23, 29); Complex(31, 37) |]
                   [| Complex(41, 43); Complex(47, 53) |] |]
            )

        Assert.Equal(scalar * (matrixA + matrixB), (scalar * matrixA) + (scalar * matrixB))

    [<Fact>]
    let ``Scalar multiplication distributes over complex addition`` () =
        let scalarA = Complex(3, 5)
        let scalarB = Complex(7, 11)

        let matrix =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        Assert.Equal((scalarA + scalarB) * matrix, (scalarA * matrix) + (scalarB * matrix))

    [<Fact>]
    let ``Transposing a matrix flips the rows and columns`` () =
        let matrix =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        let transposed = Matrix.Transpose matrix

        Assert.Equal(
            Matrix(
                [| [| Complex(1, 2); Complex(7, 11) |]
                   [| Complex(3, 5); Complex(13, 19) |] |]
            ),
            transposed
        )

    [<Fact>]
    let ``Conjucate of a matrix is where each element is a complex conjucate of the original matrix`` () =
        let matrix =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        let conjucate = Matrix.Conjucate matrix

        Assert.Equal(
            Matrix(
                [| [| Complex(1, -2); Complex(3, -5) |]
                   [| Complex(7, -11); Complex(13, -19) |] |]
            ),
            conjucate
        )

    [<Fact>]
    let ``Adjoin is the combination of transpose and conjucate`` () =
        let matrix =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        let adjointed = Matrix.Adjoint matrix

        Assert.Equal(
            Matrix(
                [| [| Complex(1, -2); Complex(7, -11) |]
                   [| Complex(3, -5); Complex(13, -19) |] |]
            ),
            adjointed
        )

    [<Fact>]
    let ``Matrix product is the result of multiplying rows by columns`` () =
        let a =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        let b =
            Matrix(
                [| [| Complex(23, 29); Complex(31, 37) |]
                   [| Complex(41, 43); Complex(47, 53) |] |]
            )

        let product = Matrix.Product a b

        Assert.Equal(
            Matrix(
                [| [| Complex(-127.0, 409.0)
                      Complex(-167.0, 493.0) |]
                   [| Complex(-442.0, 1794.0)
                      Complex(-586.0, 2182.0) |] |]
            ),
            product
        )

        Assert.Equal(Matrix.Product a b, a * b)

    [<Fact>]
    let ``Another example of matrix multiplication`` () =
        let a =
            Matrix(
                [| [| Complex(3, 2)
                      Complex(0, 0)
                      Complex(5, -6) |]
                   [| Complex(1, 0)
                      Complex(4, 2)
                      Complex(0, 1) |]
                   [| Complex(4, -1)
                      Complex(0, 0)
                      Complex(4, 0) |] |]
            )

        let b =
            Matrix(
                [| [| Complex(5, 0)
                      Complex(2, -1)
                      Complex(6, -4) |]
                   [| Complex(0, 0)
                      Complex(4, 5)
                      Complex(2, 0) |]
                   [| Complex(7, -4)
                      Complex(2, 7)
                      Complex(0, 0) |] |]
            )

        let product = Matrix.Product a b

        Assert.Equal(
            Matrix(
                [| [| Complex(26, -52)
                      Complex(60, 24)
                      Complex(26, 0) |]
                   [| Complex(9, 7)
                      Complex(1, 29)
                      Complex(14, 0) |]
                   [| Complex(48, -21)
                      Complex(15, 22)
                      Complex(20, -22) |] |]
            ),
            product
        )

        Assert.Equal(Matrix.Product a b, a * b)

    [<Fact>]
    let ``On identity matrix diagonal entries has value one and everytinhg else is zeroes`` () =

        let identity =
            Matrix(
                [| [| Complex(1, 0); Complex(0, 0) |]
                   [| Complex(0, 0); Complex(1, 0) |] |]
            )

        Assert.Equal(identity, Matrix.Identity 2)

    [<Fact>]
    let ``Another example of identity matrix`` () =

        let identity =
            Matrix(
                [| [| Complex(1, 0)
                      Complex(0, 0)
                      Complex(0, 0) |]
                   [| Complex(0, 0)
                      Complex(1, 0)
                      Complex(0, 0) |]
                   [| Complex(0, 0)
                      Complex(0, 0)
                      Complex(1, 0) |] |]
            )

        Assert.Equal(identity, Matrix.Identity 3)


    [<Fact>]
    let ``Multiplying matrix by identity matrix does not change the matrix`` () =
        let matrix =
            Matrix(
                [| [| Complex(1, 2)
                      Complex(3, 5)
                      Complex(7, 11) |]
                   [| Complex(7, 11)
                      Complex(13, 19)
                      Complex(23, 29) |]
                   [| Complex(31, 37)
                      Complex(41, 43)
                      Complex(47, 53) |] |]
            )

        let identity =
            Matrix(
                [| [| Complex(1, 0)
                      Complex(0, 0)
                      Complex(0, 0) |]
                   [| Complex(0, 0)
                      Complex(1, 0)
                      Complex(0, 0) |]
                   [| Complex(0, 0)
                      Complex(0, 0)
                      Complex(1, 0) |] |]
            )

        let product = Matrix.Product matrix identity

        Assert.Equal(matrix, product)

    [<Fact>]
    let ``Algebra of matrices acts on vectors to yield new vectors`` () =

        let matrix =
            Matrix(
                [| [| Complex(1, 2); Complex(3, 5) |]
                   [| Complex(7, 11); Complex(13, 19) |] |]
            )

        let vector = Vector([| Complex(23, 29); Complex(31, 37) |])

        let vectorAsMatrix =
            Matrix(
                [| [| Complex(23, 29) |]
                   [| Complex(31, 37) |] |]
            )

        let resultOfAction = Matrix.Act matrix vector

        Assert.Equal(
            Vector(
                [| Complex(-127.0, 341.0)
                   Complex(-458.0, 1526.0) |]
            ),
            resultOfAction
        )

        Assert.Equal(Matrix.Act matrix vector, matrix * vector)
        Assert.Equal(matrix * vector, matrix * vectorAsMatrix |> Matrix.ToVector)

    [<Fact>]
    let ``Matrix is hermitian if adjoint of matrix does not change the matrix`` () =
        let matrix =
            Matrix(
                [| [| Complex(1, 0)
                      Complex(3, 4)
                      Complex(5, 6) |]
                   [| Complex(3, -4)
                      Complex(7, 0)
                      Complex(10, 0) |]
                   [| Complex(5, -6)
                      Complex(10, 0)
                      Complex(9, 0) |] |]
            )

        Assert.True(Matrix.IsHermitian matrix)

    [<Fact>]
    let ``Another example of hermitian matrix`` () =
        let matrix =
            Matrix(
                [| [| Complex(5, 0)
                      Complex(4, 5)
                      Complex(6, -16) |]
                   [| Complex(4, -5)
                      Complex(13, 0)
                      Complex(7, 0) |]
                   [| Complex(6, 16)
                      Complex(7, 0)
                      Complex((-2.1, 0)) |]

                   |]
            )

        Assert.True(Matrix.IsHermitian matrix)

    [<Fact>]
    let ``Third example of hermitian matrix`` () =
        let matrix =
            Matrix(
                [| [| Complex(7, 0); Complex(6, 5) |]
                   [| Complex(6, -5); Complex(-3, 0) |] |]
            )

        Assert.True(Matrix.IsHermitian matrix)

    [<Fact>]
    let ``Fourth example of hermitian matrix`` () =
        let matrix =
            Matrix(
                [| [| Complex(1, 0)
                      Complex(2, 0)
                      Complex(3, 0) |]
                   [| Complex(2, 0)
                      Complex(2, 0)
                      Complex(3, 0) |]
                   [| Complex(3, 0)
                      Complex(3, 0)
                      Complex(9, 0) |] |]
            )

        Assert.True(Matrix.IsHermitian matrix)


    [<Fact>]
    let ``Third example of non-hermitian matrix`` () =
        let matrix =
            Matrix(
                [| [| Complex(7, 0); Complex(6, 5) |]
                   [| Complex(6, 5); Complex(3, 0) |] |]
            )

        Assert.False(Matrix.IsHermitian matrix)

    [<Fact>]
    let ``If A is hermitian matrix then inner product of A*V and V' is equal to inner product of V and A*V'`` () =

        let a = Vector([| Complex(1, 2); Complex(3, 5) |])
        let b = Vector([| Complex(7, 11); Complex(13, 19) |])

        let hermitian =
            Matrix(
                [| [| Complex(7, 0); Complex(6, 5) |]
                   [| Complex(6, -5); Complex(-3, 0) |] |]
            )

        Assert.Equal(Vector.InnerProduct (hermitian * a) b, Vector.InnerProduct a (hermitian * b))

    [<Fact>]
    let ``Matrix is unitary if product of matrix and it's adjoint is equal to product of adjoint and matrix is equal to identity matrix``
        ()
        =
        let cos = System.Math.Cos
        let sin = System.Math.Sin
        let a = 10

        let matrix =
            Matrix(
                [| [| Complex(cos a, 0)
                      Complex(-1.0 * (sin a), 0)
                      Complex(0, 0) |]
                   [| Complex(sin a, 0)
                      Complex(cos a, 0)
                      Complex(0, 0) |]
                   [| Complex(0, 0)
                      Complex(0, 0)
                      Complex(1, 0) |] |]
            )

        Assert.True(Matrix.IsUnitary matrix)

    [<Fact>]
    let ``Another example of unitary matrix`` () =

        let matrix =
            Matrix(
                [| [| Complex(1.0 / Constants.SquareRootOfTwo, 0)
                      Complex(1.0 / Constants.SquareRootOfTwo, 0) |]
                   [| Complex(0, 1.0 / Constants.SquareRootOfTwo)
                      Complex(0, -1.0 / Constants.SquareRootOfTwo) |] |]
            )

        Assert.True(Matrix.IsUnitary matrix)

    [<Fact>]
    let ``Example of non-unitary matrix`` () =

        let matrix =
            Matrix(
                [| [| Complex(1.0, 0); Complex(1.0, 0) |]
                   [| Complex(0, 1.0); Complex(0, -1.0) |] |]
            )

        Assert.False(Matrix.IsUnitary matrix)
