namespace LearningLinearAlgebra.LinearAlgebra

open Xunit

module BraTests =

    open LearningLinearAlgebra
    open LearningLinearAlgebra.Numbers.Complex
    open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace

    [<Fact>]
    let ``Addition is calculated as addition of components`` () =
        let a = U [| C(1, 2); C(3, 5) |]
        let b = U [| C(7, 11); C(13, 17) |]

        let sum = Bra.Add a b

        sum |> Should.BeEquivalentTo.Complex.Bra(U [| C(8, 13); C(16, 22) |])

    [<Fact>]
    let ``Subtraction is calculated as subtraction of components`` () =
        let a = U [| C(1, 2); C(3, 5) |]
        let b = U [| C(7, 11); C(13, 17) |]

        let sum = Bra.Subtract a b

        sum |> Should.BeEquivalentTo.Complex.Bra(U [| C(-6, -9); C(-10, -12) |])

    [<Fact>]
    let ``Additive inverse is calculated as additive inverse of components`` () =
        let a = U [| C(1, 2); C(3, 5) |]

        let additiveInverse = Bra.AdditiveInverse a

        additiveInverse
        |> Should.BeEquivalentTo.Complex.Bra(U [| C(-1, -2); C(-3, -5) |])

    [<Fact>]
    let ``Scalar multiplication is calculated as scalar multiplication of components`` () =
        let scalar = C(6, 7)
        let ket = U [| C(1, 2); C(3, 5) |]

        let sum = Bra.Multiply scalar ket

        sum |> Should.BeEquivalentTo.Complex.Bra(U [| C(-8, 19); C(-17, 51) |])

    [<Fact>]
    let ``Scalar multiplications by real number is calculated as scalar multiplication of components`` () =
        let scalar = 6.0
        let ket = U [| C(1, 2); C(3, 5) |]

        let sum = Bra.MultiplyByReal scalar ket

        sum |> Should.BeEquivalentTo.Complex.Bra(U [| C(6, 12); C(18, 30) |])

    [<Fact>]
    let ``Inner product is a sum of products of left vector components and conjucates of right vector components`` () =
        let a = U [| C(1, 2); C(3, 5) |]
        let b = U [| C(7, 11); C(13, 19) |]

        let innerProduct = Bra.InnerProduct a b

        innerProduct |> Should.BeEquivalentTo.Complex.Number(C(163, 11))

    [<Fact>]
    let ``Conjucate of a vector is where each element is a complex conjucate of the original vector`` () =
        let vector = U [| C(1, 2); C(3, 5) |]

        let conjucate = Bra.Conjucate vector

        conjucate |> Should.BeEquivalentTo.Complex.Bra(U [| C(1, -2); C(3, -5) |])

    [<Fact>]
    let ``Ket is a complex adjoint of bra`` () =
        let a = U([| C(1, 2); C(3, 5) |])

        let bra = Bra.Ket a

        bra |> Should.BeEquivalentTo.Complex.Ket(V([| C(1, -2); C(3, -5) |]))

    [<Fact>]
    let ``Norm that is the length of the vector is calculated as square root of the inner product of vector with itself``
        ()
        =
        let vector = U([| C(4, 3); C(6, -4); C(12, -7); C(0, 13) |])

        let norm = Bra.Norm vector

        norm |> Should.BeEquivalentTo.Real.Number(sqrt (439.0))

    [<Fact>]
    let ``Distance of the two vectors is the norm of the difference`` () =
        let a = U([| C(1, 2); C(3, 5) |])
        let b = U([| C(7, 11); C(13, 19) |])

        let distance = Bra.Distance a b

        distance |> Should.BeEquivalentTo.Real.Number(sqrt (413.0))

    [<Fact>]
    let ``Normalization changes norm to one but preserves the ratio of the components`` () =
        let bra = U([| C(1, 2); C(3, 5) |])

        let normalized = Bra.Normalized bra

        normalized
        |> Should.BeEquivalentTo.Complex.Bra(
            U(
                [| C(0.16012815380508713, 0.32025630761017426)
                   C(0.48038446141526137, 0.8006407690254357) |]
            )
        )

        Bra.Norm(Bra.Normalized(bra)) |> Should.BeEquivalentTo.Real.Number(1)
