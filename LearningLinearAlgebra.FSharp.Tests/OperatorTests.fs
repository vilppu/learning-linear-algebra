namespace LearningLinearAlgebra.LinearAlgebra

open Xunit

module OperatorTests =

    open LearningLinearAlgebra
    open LearningLinearAlgebra.Numbers.Complex
    open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace


    [<Fact>]
    let ``On identity operator diagonal entries has value one and everytinhg else is zeroes`` () =

        let identity = M [| [| C(1, 0); C(0, 0) |]; [| C(0, 0); C(1, 0) |] |]

        Operator.Identity 2 |> Should.BeEquivalentTo.Complex.Operator(identity)


    [<Fact>]
    let ``Zero has only zero entries`` () =
        let operator = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]


        let zero = Operator.Zero 2

        operator + zero |> Should.BeEquivalentTo.Complex.Operator(operator)
        zero + operator |> Should.BeEquivalentTo.Complex.Operator(operator)


    [<Fact>]
    let ``Zero is an additive identity`` () =
        let operator = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]


        let zero = Operator.Zero 2

        operator + zero |> Should.BeEquivalentTo.Complex.Operator(operator)
        zero + operator |> Should.BeEquivalentTo.Complex.Operator(operator)

    [<Fact>]
    let ``Operator is a linear map in vector space that can be seen as operator acting on ket components`` () =
        let a = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]
        let operator = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let ket = V [| C(23, 29); C(31, 37) |]

        let result = Operator.Act operator ket

        result |> Should.BeEquivalentTo.Complex.Ket(V [| C(-127, 341); C(-458, 1526) |])

    [<Fact>]
    let ``Operator is a linear map in vector space that can be seen as operator acting on bra components`` () =
        let a = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]
        let operator = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let bra = U [| C(23, 29); C(31, 37) |]

        let result = Operator.ActToLeft bra operator

        result |> Should.BeEquivalentTo.Complex.Bra(U [| C(-127, 341); C(-458, 1526) |])

    [<Fact>]
    let ``When multiplying an operator by scalar then each element of the operator is multiplied by the scalar`` () =
        let scalar = C(5, 7)
        let operator = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let multiplied = Operator.Multiply scalar operator

        multiplied
        |> Should.BeEquivalentTo.Complex.Operator(M [| [| C(-9, 17); C(-20, 46) |]; [| C(-42, 104); C(-68, 186) |] |])

    [<Fact>]
    let ``Adjoint is the combination of transpose and conjucate`` () =
        let operator = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let adjointed = Operator.Adjoint operator

        adjointed
        |> Should.BeEquivalentTo.Complex.Operator(M [| [| C(1, -2); C(7, -11) |]; [| C(3, -5); C(13, -19) |] |])


    [<Fact>]
    let ``Multiplying operators using the matrix multiplication`` () =
        let a = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let b = M [| [| C(23, 29); C(31, 37) |]; [| C(41, 43); C(47, 53) |] |]

        let product = Operator.MultiplyOperators a b

        product
        |> Should.BeEquivalentTo.Complex.Operator(
            M
                [| [| C(-127.0, 409.0); C(-167.0, 493.0) |]
                   [| C(-442.0, 1794.0); C(-586.0, 2182.0) |] |]
        )

        a * b |> Should.BeEquivalentTo.Complex.Operator(Operator.MultiplyOperators a b)


    [<Fact>]
    let ``Another example of operator multiplication`` () =
        let a =
            M
                [| [| C(3, 2); C(0, 0); C(5, -6) |]
                   [| C(1, 0); C(4, 2); C(0, 1) |]
                   [| C(4, -1); C(0, 0); C(4, 0) |] |]

        let b =
            M
                [| [| C(5, 0); C(2, -1); C(6, -4) |]
                   [| C(0, 0); C(4, 5); C(2, 0) |]
                   [| C(7, -4); C(2, 7); C(0, 0) |] |]


        let product = Operator.MultiplyOperators a b

        product
        |> Should.BeEquivalentTo.Complex.Operator(
            M
                [| [| C(26, -52); C(60, 24); C(26, 0) |]
                   [| C(9, 7); C(1, 29); C(14, 0) |]
                   [| C(48, -21); C(15, 22); C(20, -22) |] |]
        )

        a * b |> Should.BeEquivalentTo.Complex.Operator(Operator.MultiplyOperators a b)

    [<Fact>]
    let ``On identity operator diagonal entries has value one and everything else is zeroes`` () =

        let identity = M [| [| C(1, 0); C(0, 0) |]; [| C(0, 0); C(1, 0) |] |]


        Operator.Identity 2 |> Should.BeEquivalentTo.Complex.Operator(identity)

    [<Fact>]
    let ``Another example of identity operator`` () =

        let identity =
            M
                [| [| C(1, 0); C(0, 0); C(0, 0) |]
                   [| C(0, 0); C(1, 0); C(0, 0) |]
                   [| C(0, 0); C(0, 0); C(1, 0) |] |]

        Operator.Identity 3 |> Should.BeEquivalentTo.Complex.Operator(identity)


    [<Fact>]
    let ``Multiplying operator by identity operator does not change the operator`` () =
        let operator =
            M
                [| [| C(1, 2); C(3, 5); C(7, 11) |]
                   [| C(7, 11); C(13, 19); C(23, 29) |]
                   [| C(31, 37); C(41, 43); C(47, 53) |] |]

        let identity =
            M
                [| [| C(1, 0); C(0, 0); C(0, 0) |]
                   [| C(0, 0); C(1, 0); C(0, 0) |]
                   [| C(0, 0); C(0, 0); C(1, 0) |] |]

        let product = Operator.MultiplyOperators operator identity

        product |> Should.BeEquivalentTo.Complex.Operator(operator)

    [<Fact>]
    let ``LearningLinearAlgebra.FSharp of operators acts on vectors to yield new vectors`` () =

        let operator = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let vector = V [| C(23, 29); C(31, 37) |]

        let vectorAsMatrix = M [| [| C(23, 29) |]; [| C(31, 37) |] |]

        let resultOfAction = Operator.Act operator vector

        resultOfAction
        |> Should.BeEquivalentTo.Complex.Ket(V [| C(-127.0, 341.0); C(-458.0, 1526.0) |])

        operator * vector
        |> Should.BeEquivalentTo.Complex.Ket(Operator.Act operator vector)

    [<Fact>]
    let ``Operator is hermitian if adjoint of operator does not change the operator`` () =
        let operator =
            M
                [| [| C(1, 0); C(3, 4); C(5, 6) |]
                   [| C(3, -4); C(7, 0); C(10, 0) |]
                   [| C(5, -6); C(10, 0); C(9, 0) |] |]

        Assert.True(Operator.IsHermitian operator)

    [<Fact>]
    let ``Another example of hermitian operator`` () =
        let operator =
            M
                [| [| C(5, 0); C(4, 5); C(6, -16) |]
                   [| C(4, -5); C(13, 0); C(7, 0) |]
                   [| C(6, 16); C(7, 0); C((-2.1, 0)) |] |]


        Assert.True(Operator.IsHermitian operator)

    [<Fact>]
    let ``Third example of hermitian operator`` () =
        let operator = M [| [| C(7, 0); C(6, 5) |]; [| C(6, -5); C(-3, 0) |] |]

        Assert.True(Operator.IsHermitian operator)

    [<Fact>]
    let ``Fourth example of hermitian operator`` () =
        let operator =
            M
                [| [| C(1, 0); C(2, 0); C(3, 0) |]
                   [| C(2, 0); C(2, 0); C(3, 0) |]
                   [| C(3, 0); C(3, 0); C(9, 0) |] |]

        Assert.True(Operator.IsHermitian operator)


    [<Fact>]
    let ``Example of non-hermitian operator`` () =
        let operator = M [| [| C(7, 0); C(6, 5) |]; [| C(6, 5); C(3, 0) |] |]


        Assert.False(Operator.IsHermitian operator)

    [<Fact>]
    let ``If A is hermitian operator then inner product of A*V and V' is equal to inner product of V and A*V'`` () =

        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let hermitian = M [| [| C(7, 0); C(6, 5) |]; [| C(6, -5); C(-3, 0) |] |]

        Ket.InnerProduct a (hermitian * b)
        |> Should.BeEquivalentTo.Complex.Number(Ket.InnerProduct (hermitian * a) b)

    [<Fact>]
    let ``Operator is unitary if product of operator and it's adjoint is equal to product of adjoint and operator is equal to identity operator``
        ()
        =
        let cos = cos
        let sin = sin
        let a = 10.0

        let operator =
            M
                [| [| C(cos a, 0); C(-1.0 * (sin a), 0); C(0, 0) |]
                   [| C(sin a, 0); C(cos a, 0); C(0, 0) |]
                   [| C(0, 0); C(0, 0); C(1, 0) |] |]

        Assert.True(Operator.IsUnitary operator)

    [<Fact>]
    let ``Another example of unitary operator`` () =

        let operator =
            M
                [| [| C(1.0 / sqrt 2.0, 0); C(1.0 / sqrt 2.0, 0) |]
                   [| C(0, 1.0 / sqrt 2.0); C(0, -1.0 / sqrt 2.0) |] |]


        Assert.True(Operator.IsUnitary operator)

    [<Fact>]
    let ``Third example of unitary operator`` () =

        let operator = M [| [| C(1, 0); C(0, 0) |]; [| C(0, 0); C(0, 1) |] |]

        Assert.True(Operator.IsUnitary operator)

    [<Fact>]
    let ``Example of non-unitary operator`` () =

        let operator = M [| [| C(1.0, 0); C(1.0, 0) |]; [| C(0, 1.0); C(0, -1.0) |] |]

        Assert.False(Operator.IsUnitary operator)

    [<Fact>]
    let ``Product of unitary operators is also unitary operator`` () =

        let unitary =
            M
                [| [| C(1.0 / sqrt 2.0, 0); C(1.0 / sqrt 2.0, 0) |]
                   [| C(0, 1.0 / sqrt 2.0); C(0, -1.0 / sqrt 2.0) |] |]

        let anotherUnitary = M [| [| C(1, 0); C(0, 0) |]; [| C(0, 0); C(0, 1) |] |]


        Assert.True(Operator.IsUnitary(unitary * anotherUnitary))


    [<Fact>]
    let ``Unitary operators preserve inner products`` () =

        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let unitary = M [| [| C(1, 0); C(0, 0) |]; [| C(0, 0); C(0, 1) |] |]


        Ket.InnerProduct a b
        |> Should.BeEquivalentTo.Complex.Number(Ket.InnerProduct (unitary * a) (unitary * b))


    [<Fact>]
    let ``Unitary operators preserve distance`` () =

        let a: Ket<float> = V [| C(1, 2); C(3, 5) |]

        let b: Ket<float> = V [| C(7, 11); C(13, 19) |]

        let unitary = M [| [| C(1, 0); C(0, 0) |]; [| C(0, 0); C(0, 1) |] |]

        Ket.Distance a b
        |> Should.BeEquivalentTo.Real.Number(Ket.Distance (unitary * a) (unitary * b))

    [<Fact>]
    let ``Multiplying unitary operator by it's adjoint produces an identity operator`` () =

        let unitary =
            M
                [| [| C(1.0 / sqrt 2.0, 0); C(1.0 / sqrt 2.0, 0) |]
                   [| C(0, 1.0 / sqrt 2.0); C(0, -1.0 / sqrt 2.0) |] |]

        unitary * Operator.Adjoint unitary
        |> Operator.Round
        |> Should.BeEquivalentTo.Complex.Operator(Operator.Identity 2)

    [<Fact>]
    let ``Another example of multiplying unitary operator by it's adjoint`` () =

        let unitary =
            M
                [| [| C(1.0 / sqrt 2.0, 0); C(1.0 / sqrt 2.0, 0); C(0, 0) |]
                   [| C(0, -1.0 / sqrt 2.0); C(0, 1.0 / sqrt 2.0); C(0, 0) |]
                   [| C(0, 0); C(0, 0); C(0, 1) |] |]

        let transposeOfunitary = Operator.Adjoint unitary

        unitary * transposeOfunitary
        |> Operator.Round
        |> Should.BeEquivalentTo.Complex.Operator(Operator.Identity 3)

    [<Fact>]
    let ``Commutator of hermitians a and b is (a * b) - (b * a)`` () =

        let a = M [| [| C(0, 0); C(1, 0) |]; [| C(1, 0); C(0, 0) |] |]

        let b = M [| [| C(0, 0); C(0, -1) |]; [| C(0, 1); C(0, 0) |] |]

        let commutator = Operator.Commutator a b

        commutator
        |> Should.BeEquivalentTo.Complex.Operator(M [| [| C(0, 2); C(0, 0) |]; [| C(0, 0); C(0, -2) |] |])

    [<Fact>]
    let ``Commutator of hermitians a and b is zero if hermitians are commutable`` () =

        let a = M [| [| C(1, 0); C(0, 0) |]; [| C(0, 0); C(1, 0) |] |]

        let b = M [| [| C(0, 0); C(1, 0) |]; [| C(1, 0); C(0, 0) |] |]

        let commutator = Operator.Commutator a b

        commutator |> Should.BeEquivalentTo.Complex.Operator(Operator.Zero 2)

    [<Fact>]
    let ``Tensor product of operator contains combinations scalar products of all elements of both operator`` () =

        let a = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let b = M [| [| C(23, 29); C(31, 37) |]; [| C(41, 43); C(47, 53) |] |]

        let tensorProduct = Operator.TensorProduct a b

        tensorProduct
        |> Should.BeEquivalentTo.Complex.Operator(
            M
                [| [| C(-35.0, 75.0); C(-43.0, 99.0); C(-76.0, 202.0); C(-92.0, 266.0) |]
                   [| C(-45.0, 125.0); C(-59.0, 147.0); C(-92.0, 334.0); C(-124.0, 394.0) |]
                   [| C(-158.0, 456.0); C(-190.0, 600.0); C(-252.0, 814.0); C(-300.0, 1070.0) |]
                   [| C(-186.0, 752.0); C(-254.0, 888.0); C(-284.0, 1338.0); C(-396.0, 1582.0) |] |]
        )

    [<Fact>]
    let ``Another example of tensor product`` () =

        let a =
            M
                [| [| C(3, 2); C(5, -1); C(0, 2) |]
                   [| C(0, 0); C(12, 0); C(6, -3) |]
                   [| C(2, 0); C(4, 4); C(9, 3) |] |]

        let b =
            M
                [| [| C(1, 0); C(3, 4); C(5, -7) |]
                   [| C(10, 2); C(6, 0); C(2, 5) |]
                   [| C(0, 0); C(1, 0); C(2, 9) |] |]

        let tensorProduct = Operator.TensorProduct a b

        tensorProduct
        |> Should.BeEquivalentTo.Complex.Operator(
            M
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
                      C(-9.0, 87.0) |] |]
        )

    [<Fact>]
    let ``Third example of tensor product`` () =

        let a = M [| [| C(1, 1); C(2, 2) |]; [| C(3, 3); C(4, 4) |] |]

        let b =
            M
                [| [| C(1, 1); C(2, 2); C(3, 3) |]
                   [| C(4, 4); C(5, 5); C(6, 6) |]
                   [| C(7, 7); C(8, 8); C(9, 9) |] |]

        let tensorProduct = Operator.TensorProduct a b

        tensorProduct
        |> Should.BeEquivalentTo.Complex.Operator(
            M
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
                      C(0.0, 72.0) |] |]
        )

    [<Fact>]
    let ``Tensor product is associative`` () =
        let a = M [| [| C(1, 2); C(3, 5) |]; [| C(7, 11); C(13, 19) |] |]

        let b = M [| [| C(23, 29); C(31, 37) |]; [| C(41, 43); C(47, 53) |] |]

        let c = M [| [| C(59, 61); C(67, 71) |]; [| C(73, 79); C(83, 89) |] |]

        Operator.TensorProduct a (Operator.TensorProduct b c)
        |> Should.BeEquivalentTo.Complex.Operator(Operator.TensorProduct (Operator.TensorProduct a b) c)

    [<Fact>]
    let ``Operator multiplied by it's eigen vector equals to eigen value multiplied by eigen vector`` () =
        let operator = M [| [| C(4, 0); C(-1, 0) |]; [| C(2, 0); C(1, 0) |] |]

        let eigenVector = V [| C(1, 0); C(1, 0) |]

        let eigenValue = C(3.0, 0)

        eigenValue * eigenVector
        |> Should.BeEquivalentTo.Complex.Ket(operator * eigenVector)
