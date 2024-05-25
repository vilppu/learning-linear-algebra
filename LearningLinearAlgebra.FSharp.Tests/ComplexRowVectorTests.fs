namespace LearningLinearAlgebra.Numbers

open Xunit

module ComplexRowVectorTests =

    open LearningLinearAlgebra
    open LearningLinearAlgebra.Matrices.Complex
    open LearningLinearAlgebra.Numbers.Complex

    [<Fact>]
    let ``Sum of two vectors is calculated as sum of the components`` () =
        let a = U [| C(1, 2); C(3, 5) |]

        let b = U [| C(7, 11); C(13, 19) |]

        let sum = RowVector.Add a b

        sum |> Should.BeEquivalentTo.Complex.RowVector(U [| C(8, 13); C(16, 24) |])

        a + b |> Should.BeEquivalentTo.Complex.RowVector(RowVector.Add a b)

    [<Fact>]
    let ``Sum of complex vectors is commutative`` () =
        let a = U [| C(1, 2); C(3, 5) |]

        let b = U [| C(7, 11); C(13, 19) |]

        b + a |> Should.BeEquivalentTo.Complex.RowVector(a + b)

    [<Fact>]
    let ``Sum of complex vectors is associative`` () =
        let a = U [| C(1, 2); C(3, 5) |]

        let b = U [| C(7, 11); C(13, 19) |]

        let c = U [| C(23, 29); C(31, 37) |]

        a + (b + c) |> Should.BeEquivalentTo.Complex.RowVector((a + b) + c)

    [<Fact>]
    let ``Sum of vector and it's the inverse is zero`` () =
        let vector = U [| C(1, 2); C(3, 5) |]

        let zero = RowVector.Zero 2

        vector + (-vector) |> Should.BeEquivalentTo.Complex.RowVector(zero)

    [<Fact>]
    let ``Zero is an additive identity`` () =
        let vector = U [| C(1, 2); C(3, 5) |]

        let zero = RowVector.Zero 2

        vector + zero |> Should.BeEquivalentTo.Complex.RowVector(vector)
        zero + vector |> Should.BeEquivalentTo.Complex.RowVector(vector)

    [<Fact>]
    let ``Difference of two vectors is calculated as difference of the components`` () =
        let a = U [| C(1, 2); C(3, 5) |]

        let b = U [| C(7, 11); C(13, 19) |]

        let difference = RowVector.Subtract a b

        difference
        |> Should.BeEquivalentTo.Complex.RowVector(U [| C(-6, -9); C(-10, -14) |])

        a - b |> Should.BeEquivalentTo.Complex.RowVector(RowVector.Subtract a b)

    [<Fact>]
    let ``When multiplying a vector by scalar then each element of the vector is multiplied by the scalar`` () =
        let scalar = C(5, 7)

        let vector = U [| C(11, 13); C(19, 21) |]

        let multiplied = RowVector.Multiply scalar vector

        multiplied
        |> Should.BeEquivalentTo.Complex.RowVector(U [| C(-36.0, 142.0); C(-52.0, 238.0) |])

        scalar * vector
        |> Should.BeEquivalentTo.Complex.RowVector(RowVector.Multiply scalar vector)

    [<Fact>]
    let ``Complex vector can by multiplied by a real scalar`` () =
        let scalar = 5.0

        let vector = U [| C(1, 0); C(2, 0) |]

        let multiplied = RowVector.MultiplyByReal scalar vector

        multiplied
        |> Should.BeEquivalentTo.Complex.RowVector(U [| C(5.0, 0.0); C(10.0, 0.0) |])

        scalar * vector
        |> Should.BeEquivalentTo.Complex.RowVector(RowVector.MultiplyByReal scalar vector)

    [<Fact>]
    let ``Scalar multiplication respects complex multiplication`` () =
        let scalarA = C(3, 5)
        let scalarB = C(7, 11)

        let vector = U [| C(23, 29); C(31, 37) |]

        (scalarA * scalarB) * vector
        |> Should.BeEquivalentTo.Complex.RowVector(scalarA * (scalarB * vector))

    [<Fact>]
    let ``Scalar multiplication distributes over addition`` () =
        let scalar = C(3, 5)

        let vectorA = U [| C(7, 11); C(13, 19) |]

        let vectorB = U [| C(23, 29); C(31, 37) |]

        (scalar * vectorA) + (scalar * vectorB)
        |> Should.BeEquivalentTo.Complex.RowVector(scalar * (vectorA + vectorB))

    [<Fact>]
    let ``Scalar multiplication distributes over complex addition`` () =
        let scalarA = C(3, 5)
        let scalarB = C(7, 11)

        let vector = U [| C(23, 29); C(31, 37) |]

        (scalarA * vector) + (scalarB * vector)
        |> Should.BeEquivalentTo.Complex.RowVector((scalarA + scalarB) * vector)

    [<Fact>]
    let ``Conjucate of a vector is where each element is a complex conjucate of the original vector`` () =
        let vector = U [| C(1, 2); C(3, 5) |]

        let conjucate = RowVector.Conjucate vector

        conjucate |> Should.BeEquivalentTo.Complex.RowVector(U [| C(1, -2); C(3, -5) |])

    [<Fact>]
    let ``Product of row vector and column vector is sum of products of vector components`` () =
        let a = U [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let product = RowVector.MultiplyVectors a b

        product |> Should.BeEquivalentTo.Complex.Number(C(-71.0, 147))

    [<Fact>]
    let ``Inner product is a sum of products of left vector components and conjucates of right vector components`` () =
        let a = U [| C(1, 2); C(3, 5) |]

        let b = U [| C(7, 11); C(13, 19) |]

        let innerProduct = RowVector.InnerProduct a b

        innerProduct |> Should.BeEquivalentTo.Complex.Number(C(163.0, 11.0))

        a * b |> Should.BeEquivalentTo.Complex.Number(RowVector.InnerProduct a b)

    [<Fact>]
    let ``Inner product respects addition`` () =
        let a = U [| C(1, 2); C(3, 5) |]

        let b = U [| C(7, 11); C(13, 19) |]

        let c = U [| C(23, 29); C(31, 37) |]

        (a * c) + (b * c) |> Should.BeEquivalentTo.Complex.Number((a + b) * c)

    [<Fact>]
    let ``Inner product respects scalar multiplication`` () =
        let a = U [| C(1, 2); C(3, 5) |]

        let b = U [| C(7, 11); C(13, 19) |]

        let scalar = C(23, 29)

        scalar * (a * b) |> Should.BeEquivalentTo.Complex.Number((scalar * a) * b)

    [<Fact>]
    let ``Inner product of a complex vector with itself is a real number`` () =
        let vector = U [| C(1, 2); C(3, 5) |]

        let innerProduct = RowVector.InnerProduct vector vector

        innerProduct |> Should.BeEquivalentTo.Complex.Number(C(39.0, 0.0))

    [<Fact>]
    let ``Norm is square root of inner product of vector with itself`` () =
        let vector = U [| C(4, 3); C(6, -4); C(12, -7); C(0, 13) |]

        let norm = RowVector.Norm vector

        norm |> Should.BeEquivalentTo.Real.Number(sqrt 439.0)

    [<Fact>]
    let ``RowVector can be normalized to have length of one by dividing it by it's length`` () =
        let vector = U [| C(3.0, 1.0); C(2.0, 5.0); C(-1.0, 0) |]

        let normalized = RowVector.Normalized vector |> RowVector.Round

        normalized
        |> Should.BeEquivalentTo.Complex.RowVector(
            U
                [| C(0.474341649, 0.158113883)
                   C(0.316227766, 0.790569415)
                   C(-0.158113883, 0.0) |]
            |> RowVector.Round
        )

    [<Fact>]
    let ``Distance of the two vectors is the norm of the difference`` () =

        let a = U [| C(1, 2); C(3, 5) |]

        let b = U [| C(7, 11); C(13, 19) |]

        let distance = RowVector.Distance a b

        distance |> Should.BeEquivalentTo.Real.Number(sqrt 413.0)

    [<Fact>]
    let ``Tensor product of vectors contains combinations scalar products of all elements of both vectors`` () =

        let a = U [| C(1, 2); C(3, 5) |]

        let b = U [| C(7, 11); C(13, 19) |]

        let tensorProduct = RowVector.TensorProduct a b

        tensorProduct
        |> Should.BeEquivalentTo.Complex.RowVector(
            U [| C(-15.0, 25.0); C(-25.0, 45.0); C(-34.0, 68.0); C(-56.0, 122.0) |]
        )

    [<Fact>]
    let ``Tensor product is associative`` () =

        let a = U [| C(1, 2); C(3, 5) |]

        let b = U [| C(7, 11); C(13, 19) |]

        let c = U [| C(23, 29); C(31, 37) |]

        RowVector.TensorProduct a (RowVector.TensorProduct b c)
        |> Should.BeEquivalentTo.Complex.RowVector(RowVector.TensorProduct (RowVector.TensorProduct a b) c)
