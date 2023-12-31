namespace Algebra

open Xunit

module PolarRepresentationTests =

    open RealNumbers
    open PolarPresentation

    let tolerance = 0.0000000001

    [<Fact>]
    let ``Sum of two complex numbers is calculated using the cartesian format`` () =
        let a = C(1, 0)
        let b = C(1, Pi / 2.0)

        let (Complex(sumMagniture, sumPhase)) = Complex.Add a b

        Assert.Equal(sqrt 2.0, sumMagniture, tolerance)
        Assert.Equal(Pi / 4.0, sumPhase, tolerance)

    [<Fact>]
    let ``Difference of two complex numbers is calculated using the cartesian format`` () =
        let a = C(1, 0)
        let b = C(1, Pi / 2.0)

        let (Complex(differenceMagniture, differencePhase)) = Complex.Subtract a b

        Assert.Equal(sqrt 2.0, differenceMagniture, tolerance)
        Assert.Equal(-1.0 * Pi / 4.0, differencePhase, tolerance)

    [<Fact>]
    let ``Product of two complex numbers is calculated by multiplying the magnitudes and adding their phase`` () =
        let a = C(sqrt 2.0, Pi / 4.0)
        let b = C(sqrt 2.0, Pi * (3.0 / 4.0))

        let (Complex(productMagniture, productPhase)) = Complex.Multiply a b

        Assert.Equal(2, productMagniture, tolerance)
        Assert.Equal(Pi, productPhase, tolerance)

    [<Fact>]
    let ``Quotient of two complex numbers is calculated by dividing the magnitudes and subtracting their phase`` () =
        let a = C(sqrt 2.0, Pi / 4.0)
        let b = C(sqrt 2.0, Pi * (3.0 / 4.0))

        let (Complex(quotientMagniture, quotientPhase)) = Complex.Divide a b

        Assert.Equal(1, quotientMagniture, tolerance)
        Assert.Equal(Pi * 1.5, quotientPhase, tolerance)

    [<Fact>]
    let ``Another example of dividing complex numbers`` () =
        let a = C(sqrt (10.0), atan (-3.0))
        let b = C(sqrt (17.0), atan (4.0))

        let (Complex(quotientMagniture, quotientPhase)) = Complex.Divide a b

        Assert.Equal(0.7669649888, quotientMagniture, tolerance)
        Assert.Equal(3.7083218711, quotientPhase, tolerance)
