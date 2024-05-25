namespace LearningLinearAlgebra.Numbers

open Xunit

module PolarRepresentationTests =

    open LearningLinearAlgebra
    open LearningLinearAlgebra.Numbers
    open RealNumber
    open PolarPresentation

    let tolerance = 0.0000000001

    [<Fact>]
    let ``Sum of two complex numbers is calculated using the cartesian format`` () =
        let a = C(1, 0)
        let b = C(1, Pi / 2.0)

        let (sumMagniture, sumPhase) = Complex.Add a b |> Complex.Round |> Components

        sumMagniture |> Should.BeEquivalentTo.Real.Number(sqrt 2.0 |> Round)
        sumPhase |> Should.BeEquivalentTo.Real.Number(Pi / 4.0 |> Round)

    [<Fact>]
    let ``Difference of two complex numbers is calculated using the cartesian format`` () =
        let a = C(1, 0)
        let b = C(1, Pi / 2.0)

        let (differenceMagniture, differencePhase) =
            Complex.Subtract a b |> Complex.Round |> Components

        differenceMagniture |> Should.BeEquivalentTo.Real.Number(sqrt 2.0 |> Round)
        differencePhase |> Should.BeEquivalentTo.Real.Number(-1.0 * Pi / 4.0 |> Round)

    [<Fact>]
    let ``Product of two complex numbers is calculated by multiplying the magnitudes and adding their phase`` () =
        let a = C(sqrt 2.0, Pi / 4.0)
        let b = C(sqrt 2.0, Pi * (3.0 / 4.0))

        let (productMagniture, productPhase) =
            Complex.Multiply a b |> Complex.Round |> Components

        productMagniture |> Should.BeEquivalentTo.Real.Number(2)
        productPhase |> Should.BeEquivalentTo.Real.Number(Pi |> Round)

    [<Fact>]
    let ``Quotient of two complex numbers is calculated by dividing the magnitudes and subtracting their phase`` () =
        let a = C(sqrt 2.0, Pi / 4.0)
        let b = C(sqrt 2.0, Pi * (3.0 / 4.0))

        let (quotientMagniture, quotientPhase) =
            Complex.Divide a b |> Complex.Round |> Components

        quotientMagniture |> Should.BeEquivalentTo.Real.Number(1)
        quotientPhase |> Should.BeEquivalentTo.Real.Number((Pi * 1.5) |> Round)

    [<Fact>]
    let ``Another example of dividing complex numbers`` () =
        let a = C(sqrt (10.0), atan (-3.0))
        let b = C(sqrt (17.0), atan (4.0))

        let (quotientMagniture, quotientPhase) =
            Complex.Divide a b |> Complex.Round |> Components

        quotientMagniture |> Should.BeEquivalentTo.Real.Number(0.766965)
        quotientPhase |> Should.BeEquivalentTo.Real.Number(3.708322)
