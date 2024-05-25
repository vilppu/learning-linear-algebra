namespace LearningLinearAlgebra.Numbers

open Xunit

module CartesianRepresentationTests =

    open LearningLinearAlgebra
    open Complex

    [<Fact>]
    let ``Sum of two complex numbers is calculated as sum of the components`` () =
        let a = C(5, 7)
        let b = C(11, 13)

        let sum = Add a b

        sum |> Should.BeEquivalentTo.Complex.Number(C(16, 20))
        (a + b) |> Should.BeEquivalentTo.Complex.Number(sum)

    [<Fact>]
    let ``Sum of complex numbers is commutative`` () =
        let a = C(5, 7)
        let b = C(11, 13)

        let aPlusB = Add a b
        let bPlusA = Add b a

        bPlusA |> Should.BeEquivalentTo.Complex.Number(aPlusB)

    [<Fact>]
    let ``Sum of complex numbers is associative`` () =
        let a = C(5, 7)
        let b = C(11, 13)
        let c = C(17, 19)

        let sumOfAndBPlusC = Add (Add a b) c
        let aPlusSumOfBAndC = Add a (Add b c)

        aPlusSumOfBAndC |> Should.BeEquivalentTo.Complex.Number(sumOfAndBPlusC)

    [<Fact>]
    let ``Difference of two complex numbers is calculated as difference of the components`` () =
        let a = C(5, 7)
        let b = C(11, 13)

        let difference = Subtract a b

        difference |> Should.BeEquivalentTo.Complex.Number(C(-6.0, -6.0))
        a - b |> Should.BeEquivalentTo.Complex.Number(Subtract a b)

    [<Fact>]
    let ``Difference of complex numbers is not commutative`` () =
        let a = C(5, 7)
        let b = C(11, 13)

        let aPlusB = Subtract a b
        let bPlusA = Subtract b a

        Assert.NotEqual(aPlusB, bPlusA)

    [<Fact>]
    let ``Difference of complex numbers is not associative`` () =
        let a = C(5, 7)
        let b = C(11, 13)
        let c = C(17, 19)

        let differenceOfAndBPlusC = Subtract (Subtract a b) c

        let aPlusDifferenceOfBAndC = Subtract a (Subtract b c)

        Assert.NotEqual(differenceOfAndBPlusC, aPlusDifferenceOfBAndC)

    [<Fact>]
    let ``Product of complex numbers (a1, b1) and (a2, b2) is calculated as (a1*a2 - b1*b2, a1*b2 + a2*b1)`` () =
        let a = C(5, 7)
        let b = C(11, 13)

        let product = Multiply a b

        product |> Should.BeEquivalentTo.Complex.Number(C(-36.0, 142.0))
        a * b |> Should.BeEquivalentTo.Complex.Number(Multiply a b)

    [<Fact>]
    let ``Product of complex numbers is commutative`` () =
        let a = C(5, 7)
        let b = C(11, 13)

        let aMultipliedByB = Multiply a b
        let bMultipliedByA = Multiply b a

        bMultipliedByA |> Should.BeEquivalentTo.Complex.Number(aMultipliedByB)

    [<Fact>]
    let ``Product of complex numbers is associative`` () =
        let a = C(5, 7)
        let b = C(11, 13)
        let c = C(17, 19)

        let productOfAndBMultipliedByC = Multiply (Multiply a b) c

        let aMultipliedByProductOfBAndC = Multiply a (Multiply b c)

        aMultipliedByProductOfBAndC
        |> Should.BeEquivalentTo.Complex.Number(productOfAndBMultipliedByC)

    [<Fact>]
    let ``Multiplication of complex numbers distributes over addition`` () =
        let a = C(5, 7)
        let b = C(11, 13)
        let c = C(17, 19)

        let left = Multiply a (Add b c)

        let right = Add (Multiply a b) (Multiply a c)

        right |> Should.BeEquivalentTo.Complex.Number(left)

    [<Fact>]
    let ``Square of complex number is the number multiplied by itself`` () =
        let complex = C(5, 7)

        let squared = Square complex

        squared |> Should.BeEquivalentTo.Complex.Number(complex * complex)

    [<Fact>]
    let ``Quotient of complex numbers is calculated calculated using complex conjucations`` () =
        let a = C(-2, 1)
        let b = C(1, 2)

        let quotient = Divide a b

        quotient |> Should.BeEquivalentTo.Complex.Number(C(0.0, 1.0))

    [<Fact>]
    let ``Another example of division of complex numbers`` () =
        let a = C(0, 3)
        let b = C(-1, -1)

        let quotient = Divide a b
        let f = -3.0 / 2.0

        quotient |> Should.BeEquivalentTo.Complex.Number(C(-3.0 / 2.0, -3.0 / 2.0))

    [<Fact>]
    let ``Quotient of complex numbers is not commutative`` () =
        let a = C(5, 7)
        let b = C(11, 13)

        let aDividedByB = Divide a b
        let bDividedByA = Divide b a

        Assert.NotEqual(aDividedByB, bDividedByA)

    [<Fact>]
    let ``Quotient of complex numbers is not associative`` () =
        let a = C(5, 7)
        let b = C(11, 13)
        let c = C(17, 19)

        let quotientOfAndBDividedByC = Divide (Divide a b) c

        let aDividedByQuotientOfBAndC = Divide a (Divide b c)

        Assert.NotEqual(quotientOfAndBDividedByC, aDividedByQuotientOfBAndC)

    [<Fact>]
    let ``Modulus of complex number is the square root of sum of second power of components`` () =
        let a = C(5, 7)

        let modulus = Modulus a

        modulus |> Should.BeEquivalentTo.Real.Number(sqrt (74.0))

    [<Fact>]
    let ``Another example of modulus`` () =
        let a = C(1, -1)

        let modulus = Modulus a

        modulus |> Should.BeEquivalentTo.Real.Number(sqrt (2.0))

    [<Fact>]
    let ``Sum of complex and it's addivive inverse is zero`` () =
        let a = C(2, -3)

        a + (-a) |> Should.BeEquivalentTo.Complex.Number(C(0, 0))
