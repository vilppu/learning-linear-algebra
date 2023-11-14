namespace Algebra

open Xunit

module RealVectorTests =

    open RealNumbers

    [<Fact>]
    let ``Vector with one element can be presented as scalar`` () =
        let vector = V [| 123 |]

        let scalar = Vector<_>.AsScalar vector

        Assert.Equal(123.0, scalar)

    [<Fact>]
    let ``Sum of two vectors is calculated as sum of the components`` () =
        let a = V [| -1.0; -3 |]
        let b = V [| -7.0; -13 |]

        let sum = Vector<_>.Add a b

        Assert.Equal(V [| -8.0; -16 |], sum)
        Assert.Equal(Vector<_>.Add a b, a + b)

    [<Fact>]
    let ``Sum of vectors is commutative`` () =
        let a = V [| -1.0; -3 |]
        let b = V [| -7.0; -13 |]

        Assert.Equal(a + b, b + a)

    [<Fact>]
    let ``Sum of vectors is associative`` () =
        let a = V [| -1.0; -3 |]
        let b = V [| -7.0; -13 |]
        let c = V [| -23.0; -31 |]

        Assert.Equal((a + b) + c, a + (b + c))

    [<Fact>]
    let ``Sum of vector and it's the inverse is zero`` () =
        let vector = V [| -1.0; -3 |]

        let zero = Vector<_>.Zero 2

        Assert.Equal(zero, vector + (-vector))

    [<Fact>]
    let ``Zero is an additive identity`` () =
        let vector = V [| -1.0; -3 |]
        let zero = Vector<_>.Zero 2

        Assert.Equal(vector, vector + zero)
        Assert.Equal(vector, zero + vector)

    [<Fact>]
    let ``Difference of two vectors is calculated as difference of the components`` () =
        let a = V [| 1.0; 3 |]
        let b = V [| 7.0; 13 |]

        let difference = Vector<_>.Subtract a b

        Assert.Equal(V [| -6.0; -10 |], difference)
        Assert.Equal(Vector<_>.Subtract a b, a - b)

    [<Fact>]
    let ``When multiplying a vector by scalar then each element of the vector is multiplied by the scalar`` () =
        let scalar = 5.0
        let vector = V [| 11.0; 19.0 |]

        let multiplied = Vector<_>.Multiply scalar vector

        Assert.Equal(V [| 55.0; 95.0 |], multiplied)

        Assert.Equal(Vector<_>.Multiply scalar vector, scalar * vector)

    [<Fact>]
    let ``Scalar multiplication respects vector multiplication`` () =
        let scalarA = -3.0
        let scalarB = -7.0
        let vector = V [| -23.0; -31.0 |]

        Assert.Equal(scalarA * (scalarB * vector), (scalarA * scalarB) * vector)

    [<Fact>]
    let ``Scalar multiplication distributes over addition`` () =
        let scalar = -3.0
        let vectorA = V [| -7.0; -13.0 |]
        let vectorB = V [| -23.0; -31.0 |]

        Assert.Equal(scalar * (vectorA + vectorB), (scalar * vectorA) + (scalar * vectorB))

    [<Fact>]
    let ``Scalar multiplication distributes over vector addition`` () =
        let scalarA = -3.0
        let scalarB = -7.0
        let vector = V [| -23.0; -31.0 |]

        Assert.Equal((scalarA + scalarB) * vector, (scalarA * vector) + (scalarB * vector))

    [<Fact>]
    let ``Inner product is a sum of products of vector components`` () =
        let a = V [| 5.0; 3.0; -7.0 |]
        let b = V [| 6.0; 2.0; 0.0 |]

        let innerProduct = Vector<float>.InnerProduct a b

        Assert.Equal(36.0, innerProduct)
        Assert.Equal(Vector<float>.InnerProduct a b, a ^<> b)

    [<Fact>]
    let ``Inner product respects addition`` () =
        let a = V [| -1.0; -3.0 |]
        let b = V [| -7.0; -13.0 |]
        let c = V [| -23.0; -31.0 |]

        Assert.Equal((a + b) ^<> c, (a ^<> c) + (b ^<> c))

    [<Fact>]
    let ``Inner product respects scalar multiplication`` () =
        let a = V [| -1.0; -3.0 |]
        let b = V [| -7.0; -13.0 |]
        let scalar = 23.0

        Assert.Equal((scalar * a) ^<> b, scalar * (a ^<> b))

    [<Fact>]
    let ``Norm is square root of inner product of vector with itself`` () =
        let vector = V [| 3.0; -6.0; 2 |]

        let norm = Vector<_>.Norm vector

        Assert.Equal(7.0, norm)

    [<Fact>]
    let ``Distance of the two vectors is the norm of the difference`` () =
        let a = V [| 3.0; 1.0; 2 |]
        let b = V [| 2.0; 2.0; -1 |]

        let distance = Vector<_>.Distance a b

        Assert.Equal(sqrt (11.0), distance)

    [<Fact>]
    let ``Tensor product of vectors contains combinations scalar products of all elements of both vectors`` () =

        let a = V [| 3.0; 4.0; 7 |]
        let b = V [| -1.0; 2 |]

        let tensorProduct = Vector<_>.TensorProduct a b

        Assert.Equal(V [| -3.0; 6.0; -4.0; 8.0; -7.0; 14 |], tensorProduct)

    [<Fact>]
    let ``Another example of tensor product`` () =

        let a = V [| -1.0; 2 |]
        let b = V [| 3.0; 4.0; 7 |]

        let tensorProduct = Vector<_>.TensorProduct a b

        Assert.Equal(V [| -3.0; -4.0; -7.0; 6.0; 8.0; 14.0 |], tensorProduct)

    [<Fact>]
    let ``Tensor product is associative`` () =
        let a = V [| -1.0; -3 |]
        let b = V [| -7.0; -13 |]
        let c = V [| -23.0; -31 |]

        Assert.Equal(
            Vector<_>.TensorProduct (Vector<_>.TensorProduct a b) c,
            Vector<_>.TensorProduct a (Vector<_>.TensorProduct b c)
        )

    [<Fact>]
    let ``Convert from linearly independenr base to orthonormal base`` () =
        let I = V [| 3.0; 0.0; 0.0 |]
        let II = V [| 0.0; 1.0; 2.0 |]
        let III = V [| 0.0; 25.0 |]

        let norm = (Vector<_>.Norm I)
        let a = (1.0 / norm) * I
        Assert.Equal(a, V [| 1.0; 0.0; 0.0 |])
