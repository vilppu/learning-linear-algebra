namespace LearningLinearAlgebra.Numbers

open Xunit
open LearningLinearAlgebra

module RealRowVectorTests =

    open LearningLinearAlgebra.Matrices.Real

    [<Fact>]
    let ``Sum of two vectors is calculated as sum of the components`` () =
        let a = U [| -1.0; -3 |]
        let b = U [| -7.0; -13 |]

        let sum = RowVector.Add a b

        Assert.Equal<RowVector<float>>(a, a)

        sum |> Should.BeEquivalentTo.Real.RowVector(U [| -8.0; -16 |])
        a + b |> Should.BeEquivalentTo.Real.RowVector(RowVector.Add a b)

    [<Fact>]
    let ``Sum of vectors is commutative`` () =
        let a = U [| -1.0; -3 |]
        let b = U [| -7.0; -13 |]

        b + a |> Should.BeEquivalentTo.Real.RowVector(a + b)

    [<Fact>]
    let ``Sum of vectors is associative`` () =
        let a = U [| -1.0; -3 |]
        let b = U [| -7.0; -13 |]
        let c = U [| -23.0; -31 |]

        a + (b + c) |> Should.BeEquivalentTo.Real.RowVector((a + b) + c)

    [<Fact>]
    let ``Sum of vector and it's the inverse is zero`` () =
        let vector = U [| -1.0; -3 |]

        let zero = RowVector.Zero 2

        vector + (-vector) |> Should.BeEquivalentTo.Real.RowVector(zero)

    [<Fact>]
    let ``Zero is an additive identity`` () =
        let vector = U [| -1.0; -3 |]
        let zero = RowVector.Zero 2

        vector + zero |> Should.BeEquivalentTo.Real.RowVector(vector)
        zero + vector |> Should.BeEquivalentTo.Real.RowVector(vector)

    [<Fact>]
    let ``Difference of two vectors is calculated as difference of the components`` () =
        let a = U [| 1.0; 3 |]
        let b = U [| 7.0; 13 |]

        let difference = RowVector.Subtract a b

        difference |> Should.BeEquivalentTo.Real.RowVector(U [| -6.0; -10 |])
        a - b |> Should.BeEquivalentTo.Real.RowVector(RowVector.Subtract a b)

    [<Fact>]
    let ``When multiplying a vector by scalar then each element of the vector is multiplied by the scalar`` () =
        let scalar = 5.0
        let vector = U [| 11.0; 19.0 |]

        let multiplied = RowVector.Multiply scalar vector

        multiplied |> Should.BeEquivalentTo.Real.RowVector(U [| 55.0; 95.0 |])

        scalar * vector
        |> Should.BeEquivalentTo.Real.RowVector(RowVector.Multiply scalar vector)

    [<Fact>]
    let ``Scalar multiplication respects vector multiplication`` () =
        let scalarA = -3.0
        let scalarB = -7.0
        let vector = U [| -23.0; -31.0 |]

        (scalarA * scalarB) * vector
        |> Should.BeEquivalentTo.Real.RowVector(scalarA * (scalarB * vector))

    [<Fact>]
    let ``Scalar multiplication distributes over addition`` () =
        let scalar = -3.0
        let vectorA = U [| -7.0; -13.0 |]
        let vectorB = U [| -23.0; -31.0 |]

        (scalar * vectorA) + (scalar * vectorB)
        |> Should.BeEquivalentTo.Real.RowVector(scalar * (vectorA + vectorB))

    [<Fact>]
    let ``Scalar multiplication distributes over vector addition`` () =
        let scalarA = -3.0
        let scalarB = -7.0
        let vector = U [| -23.0; -31.0 |]

        (scalarA * vector) + (scalarB * vector)
        |> Should.BeEquivalentTo.Real.RowVector((scalarA + scalarB) * vector)

    [<Fact>]
    let ``Product of row vector and column vector is sum of products of vector components`` () =
        let a = U [| 5.0; 3.0; -7.0 |]
        let b = V [| 6.0; 2.0; 0.0 |]

        let product = RowVector.MultiplyVectors a b

        product |> Should.BeEquivalentTo.Real.Number(36.0)
        a * b |> Should.BeEquivalentTo.Real.Number(RowVector.MultiplyVectors a b)

    [<Fact>]
    let ``Inner product is a sum of products of vector components`` () =
        let a = U [| 5.0; 3.0; -7.0 |]
        let b = U [| 6.0; 2.0; 0.0 |]

        let innerProduct = RowVector.InnerProduct a b

        innerProduct |> Should.BeEquivalentTo.Real.Number(36.0)
        a * b |> Should.BeEquivalentTo.Real.Number(RowVector.InnerProduct a b)

    [<Fact>]
    let ``Inner product respects addition`` () =
        let a = U [| -1.0; -3.0 |]
        let b = U [| -7.0; -13.0 |]
        let c = U [| -23.0; -31.0 |]

        (a * c) + (b * c) |> Should.BeEquivalentTo.Real.Number((a + b) * c)

    [<Fact>]
    let ``Inner product respects scalar multiplication`` () =
        let a = U [| -1.0; -3.0 |]
        let b = U [| -7.0; -13.0 |]
        let scalar = 23.0

        scalar * (a * b) |> Should.BeEquivalentTo.Real.Number((scalar * a) * b)

    [<Fact>]
    let ``Norm is square root of inner product of vector with itself`` () =
        let vector = U [| 3.0; -6.0; 2 |]

        let norm = RowVector.Norm vector

        norm |> Should.BeEquivalentTo.Real.Number(7.0)

    [<Fact>]
    let ``Distance of the two vectors is the norm of the difference`` () =
        let a = U [| 3.0; 1.0; 2 |]
        let b = U [| 2.0; 2.0; -1 |]

        let distance = RowVector.Distance a b

        distance |> Should.BeEquivalentTo.Real.Number(sqrt (11.0))

    [<Fact>]
    let ``Tensor product of vectors contains combinations scalar products of all elements of both vectors`` () =

        let a = U [| 3.0; 4.0; 7 |]
        let b = U [| -1.0; 2 |]

        let tensorProduct = RowVector.TensorProduct a b

        tensorProduct
        |> Should.BeEquivalentTo.Real.RowVector(U [| -3.0; 6.0; -4.0; 8.0; -7.0; 14 |])

    [<Fact>]
    let ``Another example of tensor product`` () =

        let a = U [| -1.0; 2 |]
        let b = U [| 3.0; 4.0; 7 |]

        let tensorProduct = RowVector.TensorProduct a b

        tensorProduct
        |> Should.BeEquivalentTo.Real.RowVector(U [| -3.0; -4.0; -7.0; 6.0; 8.0; 14.0 |])

    [<Fact>]
    let ``Tensor product is associative`` () =
        let a = U [| -1.0; -3 |]
        let b = U [| -7.0; -13 |]
        let c = U [| -23.0; -31 |]

        RowVector.TensorProduct a (RowVector.TensorProduct b c)
        |> Should.BeEquivalentTo.Real.RowVector(RowVector.TensorProduct (RowVector.TensorProduct a b) c)

    [<Fact>]
    let ``Convert from linearly independenr base to orthonormal base`` () =
        let I = U [| 3.0; 0.0; 0.0 |]
        let II = U [| 0.0; 1.0; 2.0 |]
        let III = U [| 0.0; 25.0 |]

        let norm = (RowVector.Norm I)
        let a = (1.0 / norm) * I
        U [| 1.0; 0.0; 0.0 |] |> Should.BeEquivalentTo.Real.RowVector(a)
