namespace LearningLinearAlgebra.LinearAlgebra

open Xunit

module KetTests =

    open LearningLinearAlgebra
    open LearningLinearAlgebra.Numbers.Complex
    open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace

    [<Fact>]
    let ``Addition is calculated as addition of components`` () =
        let a = V [| C(1, 2); C(3, 5) |]
        let b = V [| C(7, 11); C(13, 17) |]

        let sum = Ket.Add a b

        sum |> Should.BeEquivalentTo.Complex.Ket(V [| C(8, 13); C(16, 22) |])

    [<Fact>]
    let ``Subtraction is calculated as subtraction of components`` () =
        let a = V [| C(1, 2); C(3, 5) |]
        let b = V [| C(7, 11); C(13, 17) |]

        let sum = Ket.Subtract a b

        sum |> Should.BeEquivalentTo.Complex.Ket(V [| C(-6, -9); C(-10, -12) |])

    [<Fact>]
    let ``Additive inverse is calculated as additive inverse of components`` () =
        let a = V [| C(1, 2); C(3, 5) |]

        let additiveInverse = Ket.AdditiveInverse a

        additiveInverse
        |> Should.BeEquivalentTo.Complex.Ket(V [| C(-1, -2); C(-3, -5) |])

    [<Fact>]
    let ``Scalar multiplication is calculated as scalar multiplication of components`` () =
        let scalar = C(6, 7)
        let ket = V [| C(1, 2); C(3, 5) |]

        let sum = Ket.Multiply scalar ket

        sum |> Should.BeEquivalentTo.Complex.Ket(V [| C(-8, 19); C(-17, 51) |])

    [<Fact>]
    let ``Scalar multiplications by real number is calculated as scalar multiplication of components`` () =
        let scalar = 6.0
        let ket = V [| C(1, 2); C(3, 5) |]

        let sum = Ket.MultiplyByReal scalar ket

        sum |> Should.BeEquivalentTo.Complex.Ket(V [| C(6, 12); C(18, 30) |])

    [<Fact>]
    let ``Ket can be multiplied bra by applying the rules of matrix multiplication`` () =
        let bra = U [| C(1, 2); C(3, 5) |]
        let ket = V [| C(7, 11); C(13, 19) |]

        let product = Ket.MultiplyByBra bra ket

        product |> Should.BeEquivalentTo.Complex.Number(C(-71.0, 147))

    [<Fact>]
    let ``Inner product is a sum of products of left vector components and conjucates of right vector components`` () =
        let a = V [| C(1, 2); C(3, 5) |]
        let b = V [| C(7, 11); C(13, 19) |]

        let innerProduct = Ket.InnerProduct a b

        innerProduct |> Should.BeEquivalentTo.Complex.Number(C(163, 11))

    [<Fact>]
    let ``Conjucate of a vector is where each element is a complex conjucate of the original vector`` () =
        let vector = V [| C(1, 2); C(3, 5) |]

        let conjucate = Ket.Conjucate vector

        conjucate |> Should.BeEquivalentTo.Complex.Ket(V [| C(1, -2); C(3, -5) |])

    [<Fact>]
    let ``Bra is a complex adjoint of ket`` () =
        let a = V([| C(1, 2); C(3, 5) |])

        let bra = Ket.Bra(a)

        bra |> Should.BeEquivalentTo.Complex.Bra(U([| C(1, -2); C(3, -5) |]))

    [<Fact>]
    let ``Norm that is the length of the vector is calculated as square root of the inner product of vector with itself``
        ()
        =
        let ket = V([| C(4, 3); C(6, -4); C(12, -7); C(0, 13) |])

        let norm = Ket.Norm ket

        norm |> Should.BeEquivalentTo.Real.Number(sqrt (439.0))

    [<Fact>]
    let ``Distance of the two vectors is the norm of the difference`` () =
        let a = V([| C(1, 2); C(3, 5) |])
        let b = V([| C(7, 11); C(13, 19) |])

        let distance = Ket.Distance a b

        distance |> Should.BeEquivalentTo.Real.Number(sqrt (413.0))

    [<Fact>]
    let ``Normalization changes norm to one but preserves the ratio of the components`` () =
        let ket = V([| C(1, 2); C(3, 5) |])

        let normalized = Ket.Normalized ket

        normalized
        |> Should.BeEquivalentTo.Complex.Ket(
            V(
                [| C(0.16012815380508713, 0.32025630761017426)
                   C(0.48038446141526137, 0.8006407690254357) |]
            )
        )

        Ket.Norm(Ket.Normalized(ket)) |> Should.BeEquivalentTo.Real.Number(1)
