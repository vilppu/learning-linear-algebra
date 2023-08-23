namespace Algebra

open Xunit


module PolarRepresentationTests =

    open ComplexNumbers.PolarPresentation

    let tolerance = 0.0000000001

    [<Fact>]
    let ``Sum of two complex numbers is calculated using the cartesian format`` () =
        let a = Complex(1, 0)
        let b = Complex(1, Constants.Pi / 2.0)

        let (Complex (sumMagniture, sumPhase)) = Complex.Add a b

        Assert.Equal(sqrt 2.0, sumMagniture, tolerance)
        Assert.Equal(Constants.Pi / 4.0, sumPhase, tolerance)

    [<Fact>]
    let ``Difference of two complex numbers is calculated using the cartesian format`` () =
        let a = Complex(1, 0)
        let b = Complex(1, Constants.Pi / 2.0)

        let (Complex (differenceMagniture, differencePhase)) = Complex.Subtract a b

        Assert.Equal(sqrt 2.0, differenceMagniture, tolerance)
        Assert.Equal(-1.0 * Constants.Pi / 4.0, differencePhase, tolerance)

    [<Fact>]
    let ``Product of two complex numbers is calculated by multiplying the magnitudes and adding their phase`` () =
        let a = Complex(sqrt 2.0, Constants.Pi / 4.0)
        let b = Complex(sqrt 2.0, Constants.Pi * (3.0 / 4.0))

        let (Complex (productMagniture, productPhase)) = Complex.Multiply a b

        Assert.Equal(2, productMagniture, tolerance)
        Assert.Equal(Constants.Pi, productPhase, tolerance)

    [<Fact>]
    let ``Quotient of two complex numbers is calculated by dividing the magnitudes and subtracting their phase`` () =
        let a = Complex(sqrt 2.0, Constants.Pi / 4.0)
        let b = Complex(sqrt 2.0, Constants.Pi * (3.0 / 4.0))

        let (Complex (quotientMagniture, quotientPhase)) = Complex.Divide a b

        Assert.Equal(1, quotientMagniture, tolerance)
        Assert.Equal(Constants.Pi * 1.5, quotientPhase, tolerance)

    [<Fact>]
    let ``Another example of dividing complex numbers`` () =
        let a = Complex(sqrt (10.0), atan (-3.0))
        let b = Complex(sqrt (17.0), atan (4.0))

        let (Complex (quotientMagniture, quotientPhase)) = Complex.Divide a b

        Assert.Equal(0.7669649888, quotientMagniture, tolerance)
        Assert.Equal(3.7083218711, quotientPhase, tolerance)
