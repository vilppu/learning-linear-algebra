namespace Algebra.ComplexNumbers

module PolarPresentation =

    type Complex =
        | Complex of float * float

        static member inline NormalizePhase phase =

            let PositiveModulo divident divisor =
                (divident % divisor + divisor) % divisor

            PositiveModulo phase (RealNumbers.Pi * 2.0)

        static member inline ToPolar(cartesian: CartesianPresentation.Complex) =
            let (CartesianPresentation.Complex (real, imaginary)) = cartesian

            Complex(sqrt (real * real + imaginary * imaginary), atan (real / imaginary))

        static member inline ToCartesian(polar: Complex) =
            let (Complex (magniture, phase)) = polar

            CartesianPresentation.Complex(magniture * cos (phase), magniture * sin (phase))

        static member inline Add (left: Complex) (right: Complex) : Complex =
            (left |> Complex.ToCartesian)
            + (right |> Complex.ToCartesian)
            |> Complex.ToPolar

        static member inline Subtract (left: Complex) (right: Complex) : Complex =
            (left |> Complex.ToCartesian)
            - (right |> Complex.ToCartesian)
            |> Complex.ToPolar

        static member inline Multiply (left: Complex) (right: Complex) : Complex =

            let (Complex (leftMagnitude, leftPhase)) = left
            let (Complex (rightMagnitude, rightPhase)) = right

            Complex(leftMagnitude * rightMagnitude, leftPhase + rightPhase |> Complex.NormalizePhase)

        static member inline Divide (left: Complex) (right: Complex) : Complex =

            let (Complex (leftMagnitude, leftPhase)) = left
            let (Complex (rightMagnitude, rightPhase)) = right

            Complex(leftMagnitude / rightMagnitude, leftPhase - rightPhase |> Complex.NormalizePhase)

        static member (+)(left: Complex, right: Complex) = Complex.Add left right
        static member (-)(left: Complex, right: Complex) = Complex.Subtract left right
        static member (*)(left: Complex, right: Complex) = Complex.Multiply left right
        static member (/)(left: Complex, right: Complex) = Complex.Divide left right
