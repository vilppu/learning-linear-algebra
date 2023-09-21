namespace Algebra

open Xunit

module ComplexVectorTests =

    open ComplexNumbers

    [<Fact>]
    let ``Vector with one element can be presented as scalar`` () =
        let vector = Vector [| Complex(1, 2) |]

        let scalar = Vector.AsScalar vector

        Assert.Equal(Complex(1, 2), scalar)

    [<Fact>]
    let ``Sum of two vectors is calculated as sum of the components`` () =
        let a =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let b =
            Vector [| Complex(7, 11)
                      Complex(13, 19) |]

        let sum = Vector.Add a b

        Assert.Equal(
            Vector [| Complex(8, 13)
                      Complex(16, 24) |],
            sum
        )

        Assert.Equal(Vector.Add a b, a + b)

    [<Fact>]
    let ``Sum of complex vectors is commutative`` () =
        let a =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let b =
            Vector [| Complex(7, 11)
                      Complex(13, 19) |]

        Assert.Equal(a + b, b + a)

    [<Fact>]
    let ``Sum of complex vectors is associative`` () =
        let a =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let b =
            Vector [| Complex(7, 11)
                      Complex(13, 19) |]

        let c =
            Vector [| Complex(23, 29)
                      Complex(31, 37) |]

        Assert.Equal((a + b) + c, a + (b + c))

    [<Fact>]
    let ``Sum of vector and it's the inverse is zero`` () =
        let vector =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let zero = Vector.Zero 2

        Assert.Equal(zero, vector + (-vector))

    [<Fact>]
    let ``Zero is an additive identity`` () =
        let vector =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let zero = Vector.Zero 2

        Assert.Equal(vector, vector + zero)
        Assert.Equal(vector, zero + vector)

    [<Fact>]
    let ``Difference of two vectors is calculated as difference of the components`` () =
        let a =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let b =
            Vector [| Complex(7, 11)
                      Complex(13, 19) |]

        let difference = Vector.Subtract a b

        Assert.Equal(
            Vector [| Complex(-6, -9)
                      Complex(-10, -14) |],
            difference
        )

        Assert.Equal(Vector.Subtract a b, a - b)

    [<Fact>]
    let ``When multiplying a vector by scalar then each element of the vector is multiplied by the scalar`` () =
        let scalar = Complex(5, 7)

        let vector =
            Vector [| Complex(11, 13)
                      Complex(19, 21) |]

        let multiplied = Vector.Multiply scalar vector

        Assert.Equal(
            Vector [| Complex(-36.0, 142.0)
                      Complex(-52.0, 238.0) |],
            multiplied
        )

        Assert.Equal(Vector.Multiply scalar vector, scalar * vector)

    [<Fact>]
    let ``Complex vector can by multiplied by a real scalar`` () =
        let scalar = 5.0

        let vector =
            Vector [| Complex(1, 0)
                      Complex(2, 0) |]

        let multiplied = Vector.MultiplyByReal scalar vector

        Assert.Equal(
            Vector [| Complex(5.0, 0.0)
                      Complex(10.0, 0.0) |],
            multiplied
        )

        Assert.Equal(Vector.MultiplyByReal scalar vector, scalar * vector)

    [<Fact>]
    let ``Scalar multiplication respects complex multiplication`` () =
        let scalarA = Complex(3, 5)
        let scalarB = Complex(7, 11)

        let vector =
            Vector [| Complex(23, 29)
                      Complex(31, 37) |]

        Assert.Equal(scalarA * (scalarB * vector), (scalarA * scalarB) * vector)

    [<Fact>]
    let ``Scalar multiplication distributes over addition`` () =
        let scalar = Complex(3, 5)

        let vectorA =
            Vector [| Complex(7, 11)
                      Complex(13, 19) |]

        let vectorB =
            Vector [| Complex(23, 29)
                      Complex(31, 37) |]

        Assert.Equal(scalar * (vectorA + vectorB), (scalar * vectorA) + (scalar * vectorB))

    [<Fact>]
    let ``Scalar multiplication distributes over complex addition`` () =
        let scalarA = Complex(3, 5)
        let scalarB = Complex(7, 11)

        let vector =
            Vector [| Complex(23, 29)
                      Complex(31, 37) |]

        Assert.Equal((scalarA + scalarB) * vector, (scalarA * vector) + (scalarB * vector))

    [<Fact>]
    let ``Conjucate of a vector is where each element is a complex conjucate of the original vector`` () =
        let vector =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let conjucate = Vector.Conjucate vector

        Assert.Equal(
            Vector [| Complex(1, -2)
                      Complex(3, -5) |],
            conjucate
        )

    [<Fact>]
    let ``Inner product is a sum of products of vector components`` () =
        let a =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let b =
            Vector [| Complex(7, 11)
                      Complex(13, 19) |]

        let innerProduct = Vector.InnerProduct a b

        Assert.Equal(Complex(163.0, 11.0), innerProduct)
        Assert.Equal(Vector.InnerProduct a b, a * b)

    [<Fact>]
    let ``Inner product respects addition`` () =
        let a =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let b =
            Vector [| Complex(7, 11)
                      Complex(13, 19) |]

        let c =
            Vector [| Complex(23, 29)
                      Complex(31, 37) |]

        Assert.Equal((a + b) * c, (a * c) + (b * c))

    [<Fact>]
    let ``Inner product respects scalar multiplication`` () =
        let a =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let b =
            Vector [| Complex(7, 11)
                      Complex(13, 19) |]

        let scalar = Complex(23, 29)

        Assert.Equal((scalar * a) * b, scalar * (a * b))

    [<Fact>]
    let ``Inner product of a complex vector with itself is a real number`` () =
        let vector =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let innerProduct = Vector.InnerProduct vector vector

        Assert.Equal(Complex(39.0, 0.0), innerProduct)

    [<Fact>]
    let ``Norm is square root of inner product of vector with itself`` () =
        let vector =
            Vector [| Complex(4, 3)
                      Complex(6, -4)
                      Complex(12, -7)
                      Complex(0, 13) |]

        let norm = Vector.Norm vector

        Assert.Equal(sqrt 439.0, norm)

    [<Fact>]
    let ``Vector can be normalized to have length of one by dividing it by it's length`` () =
        let vector =
            Vector [| Complex(3.0, 1.0)
                      Complex(2.0, 5.0)
                      Complex(-1.0, 0) |]

        let normalized = Vector.Normalized vector |> Vector.Round

        Assert.Equal(
            Vector [| Complex(0.474341649, 0.158113883)
                      Complex(0.316227766, 0.790569415)
                      Complex(-0.158113883, 0.0) |]
            |> Vector.Round,
            normalized
        )

    [<Fact>]
    let ``Distance of the two vectors is the norm of the difference`` () =

        let a =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let b =
            Vector [| Complex(7, 11)
                      Complex(13, 19) |]

        let distance = Vector.Distance a b

        Assert.Equal(sqrt 413.0, distance)

    [<Fact>]
    let ``Tensor product of vectors contains combinations scalar products of all elements of both vectors`` () =

        let a =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let b =
            Vector [| Complex(7, 11)
                      Complex(13, 19) |]

        let tensorProduct = Vector.TensorProduct a b

        Assert.Equal(
            Vector [| Complex(-15.0, 25.0)
                      Complex(-25.0, 45.0)
                      Complex(-34.0, 68.0)
                      Complex(-56.0, 122.0) |],
            tensorProduct
        )

    [<Fact>]
    let ``Tensor product is associative`` () =

        let a =
            Vector [| Complex(1, 2)
                      Complex(3, 5) |]

        let b =
            Vector [| Complex(7, 11)
                      Complex(13, 19) |]

        let c =
            Vector [| Complex(23, 29)
                      Complex(31, 37) |]

        Assert.Equal(
            Vector.TensorProduct (Vector.TensorProduct a b) c,
            Vector.TensorProduct a (Vector.TensorProduct b c)
        )
