namespace Algebra.ComplexNumbers

module PolarPresentation =

    type Complex = Complex of float * float

    let Complex (magnitude, phase) = Complex(magnitude, phase)

    let private PositiveModulo divident divisor =
        (divident % divisor + divisor) % divisor

    let private NormalizePhase phase =
        PositiveModulo phase (Algebra.Constants.Pi * 2.0)

    let ToPolar (cartesian: CartesianPresentation.Complex) =
        let (CartesianPresentation.Complex (real, imaginary)) = cartesian

        Complex(System.Math.Sqrt(real * real + imaginary * imaginary), System.Math.Atan(real / imaginary))

    let ToCartesian (polar: Complex) =
        let (Complex (magniture, phase)) = polar

        CartesianPresentation.Complex(magniture * System.Math.Cos(phase), magniture * System.Math.Sin(phase))

    let Add (x: Complex) (y: Complex) : Complex =
        CartesianPresentation.Add (x |> ToCartesian) (y |> ToCartesian)
        |> ToPolar

    let Subtract (x: Complex) (y: Complex) : Complex =
        CartesianPresentation.Subtract (x |> ToCartesian) (y |> ToCartesian)
        |> ToPolar

    let Multiply (x: Complex) (y: Complex) : Complex =

        let (Complex (xMagniture, xPhase)) = x
        let (Complex (yMagniture, yPhase)) = y

        Complex(xMagniture * yMagniture, xPhase + yPhase |> NormalizePhase)

    let Divide (x: Complex) (y: Complex) : Complex =

        let (Complex (xMagniture, xPhase)) = x
        let (Complex (yMagniture, yPhase)) = y

        Complex(xMagniture / yMagniture, xPhase - yPhase |> NormalizePhase)

    type PolarOperators = PolarOperators
        with
            static member (+)(x: Complex, y: Complex) = Add x y
            static member (-)(x: Complex, y: Complex) = Subtract x y
            static member (*)(x: Complex, y: Complex) = Multiply x y
            static member (/)(x: Complex, y: Complex) = Divide x y
