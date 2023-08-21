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

        Assert.Equal(Constants.SquareRootOfTwo, sumMagniture, tolerance)
        Assert.Equal(Constants.Pi / 4.0, sumPhase, tolerance)

    [<Fact>]
    let ``Difference of two complex numbers is calculated using the cartesian format`` () =
        let a = Complex(1, 0)
        let b = Complex(1, Constants.Pi / 2.0)

        let (Complex (differenceMagniture, differencePhase)) = Complex.Subtract a b

        Assert.Equal(Constants.SquareRootOfTwo, differenceMagniture, tolerance)
        Assert.Equal(-1.0 * Constants.Pi / 4.0, differencePhase, tolerance)

    [<Fact>]
    let ``Product of two complex numbers is calculated by multiplying the magnitudes and adding their phase`` () =
        let a = Complex(Constants.SquareRootOfTwo, Constants.Pi / 4.0)
        let b = Complex(Constants.SquareRootOfTwo, Constants.Pi * (3.0 / 4.0))

        let (Complex (productMagniture, productPhase)) = Complex.Multiply a b

        Assert.Equal(2, productMagniture, tolerance)
        Assert.Equal(Constants.Pi, productPhase, tolerance)

    [<Fact>]
    let ``Quotient of two complex numbers is calculated by dividing the magnitudes and subtracting their phase`` () =
        let a = Complex(Constants.SquareRootOfTwo, Constants.Pi / 4.0)
        let b = Complex(Constants.SquareRootOfTwo, Constants.Pi * (3.0 / 4.0))

        let (Complex (quotientMagniture, quotientPhase)) = Complex.Divide a b

        Assert.Equal(1, quotientMagniture, tolerance)
        Assert.Equal(Constants.Pi * 1.5, quotientPhase, tolerance)

    [<Fact>]
    let ``Another example of dividing complex numbers`` () =
        let a = Complex(System.Math.Sqrt(10), System.Math.Atan(-3.0))
        let b = Complex(System.Math.Sqrt(17), System.Math.Atan(4.0))

        let (Complex (quotientMagniture, quotientPhase)) = Complex.Divide a b

        Assert.Equal(0.7669649888, quotientMagniture, tolerance)
        Assert.Equal(3.7083218711, quotientPhase, tolerance)
