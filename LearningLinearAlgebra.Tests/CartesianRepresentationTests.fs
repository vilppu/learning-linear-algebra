namespace Algebra

open Xunit

module CartesianRepresentationTests =

    open ComplexNumbers.CartesianPresentation

    [<Fact>]
    let ``Sum of two complex numbers is calculated as sum of the components`` () =
        let a = Complex(5, 7)
        let b = Complex(11, 13)

        let sum = Complex.Add a b

        Assert.Equal(Complex(16.0, 20.0), sum)
        Assert.Equal(Complex.Add a b, (a + b))

    [<Fact>]
    let ``Sum of complex numbers is commutative`` () =
        let a = Complex(5, 7)
        let b = Complex(11, 13)

        let aPlusB = Complex.Add a b
        let bPlusA = Complex.Add b a

        Assert.Equal(aPlusB, bPlusA)

    [<Fact>]
    let ``Sum of complex numbers is associative`` () =
        let a = Complex(5, 7)
        let b = Complex(11, 13)
        let c = Complex(17, 19)

        let sumOfAndBPlusC = Complex.Add (Complex.Add a b) c
        let aPlusSumOfBAndC = Complex.Add a (Complex.Add b c)

        Assert.Equal(sumOfAndBPlusC, aPlusSumOfBAndC)

    [<Fact>]
    let ``Difference of two complex numbers is calculated as difference of the components`` () =
        let a = Complex(5, 7)
        let b = Complex(11, 13)

        let difference = Complex.Subtract a b

        Assert.Equal(Complex(-6.0, -6.0), difference)
        Assert.Equal(Complex.Subtract a b, a - b)

    [<Fact>]
    let ``Difference of complex numbers is not commutative`` () =
        let a = Complex(5, 7)
        let b = Complex(11, 13)

        let aPlusB = Complex.Subtract a b
        let bPlusA = Complex.Subtract b a

        Assert.NotEqual(aPlusB, bPlusA)

    [<Fact>]
    let ``Difference of complex numbers is not associative`` () =
        let a = Complex(5, 7)
        let b = Complex(11, 13)
        let c = Complex(17, 19)

        let differenceOfAndBPlusC = Complex.Subtract (Complex.Subtract a b) c

        let aPlusDifferenceOfBAndC = Complex.Subtract a (Complex.Subtract b c)

        Assert.NotEqual(differenceOfAndBPlusC, aPlusDifferenceOfBAndC)

    [<Fact>]
    let ``Product of complex numbers (a1, b1) and (a2, b2) is calculated as (a1*a2 - b1*b2, a1*b2 + a2*b1)`` () =
        let a = Complex(5, 7)
        let b = Complex(11, 13)

        let product = Complex.Multiply a b

        Assert.Equal(Complex(-36.0, 142.0), product)
        Assert.Equal(Complex.Multiply a b, a * b)

    [<Fact>]
    let ``Product of complex numbers is commutative`` () =
        let a = Complex(5, 7)
        let b = Complex(11, 13)

        let aMultipliedByB = Complex.Multiply a b
        let bMultipliedByA = Complex.Multiply b a

        Assert.Equal(aMultipliedByB, bMultipliedByA)

    [<Fact>]
    let ``Product of complex numbers is associative`` () =
        let a = Complex(5, 7)
        let b = Complex(11, 13)
        let c = Complex(17, 19)

        let productOfAndBMultipliedByC = Complex.Multiply (Complex.Multiply a b) c

        let aMultipliedByProductOfBAndC = Complex.Multiply a (Complex.Multiply b c)

        Assert.Equal(productOfAndBMultipliedByC, aMultipliedByProductOfBAndC)

    [<Fact>]
    let ``Multiplication of complex numbers distributes over addition`` () =
        let a = Complex(5, 7)
        let b = Complex(11, 13)
        let c = Complex(17, 19)

        let left = Complex.Multiply a (Complex.Add b c)

        let right = Complex.Add (Complex.Multiply a b) (Complex.Multiply a c)

        Assert.Equal(left, right)

    [<Fact>]
    let ``Quotient of complex numbers is calculated calculated using complex conjucations`` () =
        let a = Complex(-2, 1)
        let b = Complex(1, 2)

        let quotient = Complex.Divide a b

        Assert.Equal(Complex(0.0, 1.0), quotient)

    [<Fact>]
    let ``Another example of division of complex numbers`` () =
        let a = Complex(0, 3)
        let b = Complex(-1, -1)

        let quotient = Complex.Divide a b
        let f = -3.0 / 2.0
        Assert.Equal(Complex(-3.0 / 2.0, -3.0 / 2.0), quotient)

    [<Fact>]
    let ``Quotient of complex numbers is not commutative`` () =
        let a = Complex(5, 7)
        let b = Complex(11, 13)

        let aDividedByB = Complex.Divide a b
        let bDividedByA = Complex.Divide b a

        Assert.NotEqual(aDividedByB, bDividedByA)

    [<Fact>]
    let ``Quotient of complex numbers is not associative`` () =
        let a = Complex(5, 7)
        let b = Complex(11, 13)
        let c = Complex(17, 19)

        let quotientOfAndBDividedByC = Complex.Divide (Complex.Divide a b) c

        let aDividedByQuotientOfBAndC = Complex.Divide a (Complex.Divide b c)

        Assert.NotEqual(quotientOfAndBDividedByC, aDividedByQuotientOfBAndC)

    [<Fact>]
    let ``Modulus of complex number is the square root of sum of second power of components`` () =
        let a = Complex(5, 7)

        let modulus = Complex.Modulus a

        Assert.Equal(System.Math.Sqrt(74.0), modulus)

    [<Fact>]
    let ``Another example of modulus`` () =
        let a = Complex(1, -1)

        let modulus = Complex.Modulus a

        Assert.Equal(System.Math.Sqrt(2), modulus)
