namespace LearningLinearAlgebra.Numbers

open Xunit

module RealColumnVectorTests =

    open LearningLinearAlgebra
    open LearningLinearAlgebra.Matrices.Real

    [<Fact>]
    let ``Sum of two vectors is calculated as sum of the components`` () =
        let a = V [| -1.0; -3 |]
        let b = V [| -7.0; -13 |]

        let sum = ColumnVector.Add a b

        Assert.Equal<ColumnVector<float>>(a, a)

        sum |> Should.BeEquivalentTo.Real.ColumnVector(V [| -8.0; -16 |])
        a + b |> Should.BeEquivalentTo.Real.ColumnVector(ColumnVector.Add a b)

    [<Fact>]
    let ``Sum of vectors is commutative`` () =
        let a = V [| -1.0; -3 |]
        let b = V [| -7.0; -13 |]

        b + a |> Should.BeEquivalentTo.Real.ColumnVector(a + b)

    [<Fact>]
    let ``Sum of vectors is associative`` () =
        let a = V [| -1.0; -3 |]
        let b = V [| -7.0; -13 |]
        let c = V [| -23.0; -31 |]

        a + (b + c) |> Should.BeEquivalentTo.Real.ColumnVector((a + b) + c)

    [<Fact>]
    let ``Sum of vector and it's the inverse is zero`` () =
        let vector = V [| -1.0; -3 |]

        let zero = ColumnVector.Zero 2

        vector + (-vector) |> Should.BeEquivalentTo.Real.ColumnVector(zero)

    [<Fact>]
    let ``Zero is an additive identity`` () =
        let vector = V [| -1.0; -3 |]
        let zero = ColumnVector.Zero 2

        vector + zero |> Should.BeEquivalentTo.Real.ColumnVector(vector)
        zero + vector |> Should.BeEquivalentTo.Real.ColumnVector(vector)

    [<Fact>]
    let ``Difference of two vectors is calculated as difference of the components`` () =
        let a = V [| 1.0; 3 |]
        let b = V [| 7.0; 13 |]

        let difference = ColumnVector.Subtract a b

        difference |> Should.BeEquivalentTo.Real.ColumnVector(V [| -6.0; -10 |])
        a - b |> Should.BeEquivalentTo.Real.ColumnVector(ColumnVector.Subtract a b)

    [<Fact>]
    let ``When multiplying a vector by scalar then each element of the vector is multiplied by the scalar`` () =
        let scalar = 5.0
        let vector = V [| 11.0; 19.0 |]

        let multiplied = ColumnVector.Multiply scalar vector

        multiplied |> Should.BeEquivalentTo.Real.ColumnVector(V [| 55.0; 95.0 |])

        scalar * vector
        |> Should.BeEquivalentTo.Real.ColumnVector(ColumnVector.Multiply scalar vector)

    [<Fact>]
    let ``Scalar multiplication respects vector multiplication`` () =
        let scalarA = -3.0
        let scalarB = -7.0
        let vector = V [| -23.0; -31.0 |]

        (scalarA * scalarB) * vector
        |> Should.BeEquivalentTo.Real.ColumnVector(scalarA * (scalarB * vector))

    [<Fact>]
    let ``Scalar multiplication distributes over addition`` () =
        let scalar = -3.0
        let vectorA = V [| -7.0; -13.0 |]
        let vectorB = V [| -23.0; -31.0 |]

        (scalar * vectorA) + (scalar * vectorB)
        |> Should.BeEquivalentTo.Real.ColumnVector(scalar * (vectorA + vectorB))

    [<Fact>]
    let ``Scalar multiplication distributes over vector addition`` () =
        let scalarA = -3.0
        let scalarB = -7.0
        let vector = V [| -23.0; -31.0 |]

        (scalarA * vector) + (scalarB * vector)
        |> Should.BeEquivalentTo.Real.ColumnVector((scalarA + scalarB) * vector)

    [<Fact>]
    let ``Inner product is a sum of products of vector components`` () =
        let a = V [| 5.0; 3.0; -7.0 |]
        let b = V [| 6.0; 2.0; 0.0 |]

        let innerProduct = ColumnVector.InnerProduct a b

        innerProduct |> Should.BeEquivalentTo.Real.Number(36.0)
        a * b |> Should.BeEquivalentTo.Real.Number(ColumnVector.InnerProduct a b)

    [<Fact>]
    let ``Inner product respects addition`` () =
        let a = V [| -1.0; -3.0 |]
        let b = V [| -7.0; -13.0 |]
        let c = V [| -23.0; -31.0 |]

        (a * c) + (b * c) |> Should.BeEquivalentTo.Real.Number((a + b) * c)

    [<Fact>]
    let ``Inner product respects scalar multiplication`` () =
        let a = V [| -1.0; -3.0 |]
        let b = V [| -7.0; -13.0 |]
        let scalar = 23.0

        scalar * (a * b) |> Should.BeEquivalentTo.Real.Number((scalar * a) * b)

    [<Fact>]
    let ``Norm is square root of inner product of vector with itself`` () =
        let vector = V [| 3.0; -6.0; 2 |]

        let norm = ColumnVector.Norm vector

        norm |> Should.BeEquivalentTo.Real.Number(7.0)

    [<Fact>]
    let ``Distance of the two vectors is the norm of the difference`` () =
        let a = V [| 3.0; 1.0; 2 |]
        let b = V [| 2.0; 2.0; -1 |]

        let distance = ColumnVector.Distance a b

        distance |> Should.BeEquivalentTo.Real.Number(sqrt (11.0))

    [<Fact>]
    let ``Tensor product of vectors contains combinations scalar products of all elements of both vectors`` () =

        let a = V [| 3.0; 4.0; 7 |]
        let b = V [| -1.0; 2 |]

        let tensorProduct = ColumnVector.TensorProduct a b

        tensorProduct
        |> Should.BeEquivalentTo.Real.ColumnVector(V [| -3.0; 6.0; -4.0; 8.0; -7.0; 14 |])

    [<Fact>]
    let ``Another example of tensor product`` () =

        let a = V [| -1.0; 2 |]
        let b = V [| 3.0; 4.0; 7 |]

        let tensorProduct = ColumnVector.TensorProduct a b

        tensorProduct
        |> Should.BeEquivalentTo.Real.ColumnVector(V [| -3.0; -4.0; -7.0; 6.0; 8.0; 14.0 |])

    [<Fact>]
    let ``Tensor product is associative`` () =
        let a = V [| -1.0; -3 |]
        let b = V [| -7.0; -13 |]
        let c = V [| -23.0; -31 |]

        ColumnVector.TensorProduct a (ColumnVector.TensorProduct b c)
        |> Should.BeEquivalentTo.Real.ColumnVector(ColumnVector.TensorProduct (ColumnVector.TensorProduct a b) c)

    [<Fact>]
    let ``Convert from linearly independenr base to orthonormal base`` () =
        let I = V [| 3.0; 0.0; 0.0 |]
        let II = V [| 0.0; 1.0; 2.0 |]
        let III = V [| 0.0; 25.0 |]

        let norm = (ColumnVector.Norm I)
        let a = (1.0 / norm) * I
        V [| 1.0; 0.0; 0.0 |] |> Should.BeEquivalentTo.Real.ColumnVector(a)
