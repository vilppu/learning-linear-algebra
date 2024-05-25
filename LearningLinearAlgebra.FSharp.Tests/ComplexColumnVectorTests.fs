namespace LearningLinearAlgebra.Numbers

open Xunit

module ComplexColumnVectorTests =

    open LearningLinearAlgebra
    open LearningLinearAlgebra.Matrices.Complex
    open LearningLinearAlgebra.Numbers.Complex

    [<Fact>]
    let ``Sum of two vectors is calculated as sum of the components`` () =
        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let sum = ColumnVector.Add a b

        sum |> Should.BeEquivalentTo.Complex.ColumnVector(V [| C(8, 13); C(16, 24) |])

        a + b |> Should.BeEquivalentTo.Complex.ColumnVector(ColumnVector.Add a b)

    [<Fact>]
    let ``Sum of complex vectors is commutative`` () =
        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        b + a |> Should.BeEquivalentTo.Complex.ColumnVector(a + b)

    [<Fact>]
    let ``Sum of complex vectors is associative`` () =
        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let c = V [| C(23, 29); C(31, 37) |]

        a + (b + c) |> Should.BeEquivalentTo.Complex.ColumnVector((a + b) + c)

    [<Fact>]
    let ``Sum of vector and it's the inverse is zero`` () =
        let vector = V [| C(1, 2); C(3, 5) |]

        let zero = ColumnVector.Zero 2

        vector + (-vector) |> Should.BeEquivalentTo.Complex.ColumnVector(zero)

    [<Fact>]
    let ``Zero is an additive identity`` () =
        let vector = V [| C(1, 2); C(3, 5) |]

        let zero = ColumnVector.Zero 2

        vector + zero |> Should.BeEquivalentTo.Complex.ColumnVector(vector)
        zero + vector |> Should.BeEquivalentTo.Complex.ColumnVector(vector)

    [<Fact>]
    let ``Difference of two vectors is calculated as difference of the components`` () =
        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let difference = ColumnVector.Subtract a b

        difference
        |> Should.BeEquivalentTo.Complex.ColumnVector(V [| C(-6, -9); C(-10, -14) |])

        a - b |> Should.BeEquivalentTo.Complex.ColumnVector(ColumnVector.Subtract a b)

    [<Fact>]
    let ``When multiplying a vector by scalar then each element of the vector is multiplied by the scalar`` () =
        let scalar = C(5, 7)

        let vector = V [| C(11, 13); C(19, 21) |]

        let multiplied = ColumnVector.Multiply scalar vector

        multiplied
        |> Should.BeEquivalentTo.Complex.ColumnVector(V [| C(-36.0, 142.0); C(-52.0, 238.0) |])

        scalar * vector
        |> Should.BeEquivalentTo.Complex.ColumnVector(ColumnVector.Multiply scalar vector)

    [<Fact>]
    let ``Complex vector can by multiplied by a real scalar`` () =
        let scalar = 5.0

        let vector = V [| C(1, 0); C(2, 0) |]

        let multiplied = ColumnVector.MultiplyByReal scalar vector

        multiplied
        |> Should.BeEquivalentTo.Complex.ColumnVector(V [| C(5.0, 0.0); C(10.0, 0.0) |])

        scalar * vector
        |> Should.BeEquivalentTo.Complex.ColumnVector(ColumnVector.MultiplyByReal scalar vector)

    [<Fact>]
    let ``Scalar multiplication respects complex multiplication`` () =
        let scalarA = C(3, 5)
        let scalarB = C(7, 11)

        let vector = V [| C(23, 29); C(31, 37) |]

        (scalarA * scalarB) * vector
        |> Should.BeEquivalentTo.Complex.ColumnVector(scalarA * (scalarB * vector))

    [<Fact>]
    let ``Scalar multiplication distributes over addition`` () =
        let scalar = C(3, 5)

        let vectorA = V [| C(7, 11); C(13, 19) |]

        let vectorB = V [| C(23, 29); C(31, 37) |]

        (scalar * vectorA) + (scalar * vectorB)
        |> Should.BeEquivalentTo.Complex.ColumnVector(scalar * (vectorA + vectorB))

    [<Fact>]
    let ``Scalar multiplication distributes over complex addition`` () =
        let scalarA = C(3, 5)
        let scalarB = C(7, 11)

        let vector = V [| C(23, 29); C(31, 37) |]

        (scalarA * vector) + (scalarB * vector)
        |> Should.BeEquivalentTo.Complex.ColumnVector((scalarA + scalarB) * vector)

    [<Fact>]
    let ``Conjucate of a vector is where each element is a complex conjucate of the original vector`` () =
        let vector = V [| C(1, 2); C(3, 5) |]

        let conjucate = ColumnVector.Conjucate vector

        conjucate
        |> Should.BeEquivalentTo.Complex.ColumnVector(V [| C(1, -2); C(3, -5) |])

    [<Fact>]
    let ``Inner product is a sum of products of left vector components and conjucates of right vector components`` () =
        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let innerProduct = ColumnVector.InnerProduct a b

        innerProduct |> Should.BeEquivalentTo.Complex.Number(C(163.0, 11.0))

        a * b |> Should.BeEquivalentTo.Complex.Number(ColumnVector.InnerProduct a b)

    [<Fact>]
    let ``Inner product respects addition`` () =
        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let c = V [| C(23, 29); C(31, 37) |]

        (a * c) + (b * c) |> Should.BeEquivalentTo.Complex.Number((a + b) * c)

    [<Fact>]
    let ``Inner product respects scalar multiplication`` () =
        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let scalar = C(23, 29)

        scalar * (a * b) |> Should.BeEquivalentTo.Complex.Number((scalar * a) * b)

    [<Fact>]
    let ``Inner product of a complex vector with itself is a real number`` () =
        let vector = V [| C(1, 2); C(3, 5) |]

        let innerProduct = ColumnVector.InnerProduct vector vector

        innerProduct |> Should.BeEquivalentTo.Complex.Number(C(39.0, 0.0))

    [<Fact>]
    let ``Norm is square root of inner product of vector with itself`` () =
        let vector = V [| C(4, 3); C(6, -4); C(12, -7); C(0, 13) |]

        let norm = ColumnVector.Norm vector

        norm |> Should.BeEquivalentTo.Real.Number(sqrt 439.0)

    [<Fact>]
    let ``ColumnVector can be normalized to have length of one by dividing it by it's length`` () =
        let vector = V [| C(3.0, 1.0); C(2.0, 5.0); C(-1.0, 0) |]

        let normalized = ColumnVector.Normalized vector |> ColumnVector.Round

        normalized
        |> Should.BeEquivalentTo.Complex.ColumnVector(
            V
                [| C(0.474341649, 0.158113883)
                   C(0.316227766, 0.790569415)
                   C(-0.158113883, 0.0) |]
            |> ColumnVector.Round
        )

    [<Fact>]
    let ``Distance of the two vectors is the norm of the difference`` () =

        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let distance = ColumnVector.Distance a b

        distance |> Should.BeEquivalentTo.Real.Number(sqrt 413.0)

    [<Fact>]
    let ``Tensor product of vectors contains combinations scalar products of all elements of both vectors`` () =

        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let tensorProduct = ColumnVector.TensorProduct a b

        tensorProduct
        |> Should.BeEquivalentTo.Complex.ColumnVector(
            V [| C(-15.0, 25.0); C(-25.0, 45.0); C(-34.0, 68.0); C(-56.0, 122.0) |]
        )

    [<Fact>]
    let ``Tensor product is associative`` () =

        let a = V [| C(1, 2); C(3, 5) |]

        let b = V [| C(7, 11); C(13, 19) |]

        let c = V [| C(23, 29); C(31, 37) |]

        ColumnVector.TensorProduct a (ColumnVector.TensorProduct b c)
        |> Should.BeEquivalentTo.Complex.ColumnVector(ColumnVector.TensorProduct (ColumnVector.TensorProduct a b) c)
