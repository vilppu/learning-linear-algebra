namespace Algebra.ComplexNumbers

module PolarPresentation =

    type Complex =
        | Complex of float * float

        static member inline NormalizePhase phase =

            let PositiveModulo divident divisor =
                (divident % divisor + divisor) % divisor

            PositiveModulo phase (Algebra.Constants.Pi * 2.0)

        static member inline public ToPolar(cartesian: CartesianPresentation.Complex) =
            let (CartesianPresentation.Complex (real, imaginary)) = cartesian

            Complex(System.Math.Sqrt(real * real + imaginary * imaginary), System.Math.Atan(real / imaginary))

        static member inline public ToCartesian(polar: Complex) =
            let (Complex (magniture, phase)) = polar

            CartesianPresentation.Complex(magniture * System.Math.Cos(phase), magniture * System.Math.Sin(phase))

        static member inline public Add (left: Complex) (right: Complex) : Complex =
            (left |> Complex.ToCartesian)
            + (right |> Complex.ToCartesian)
            |> Complex.ToPolar

        static member inline public Subtract (left: Complex) (right: Complex) : Complex =
            (left |> Complex.ToCartesian)
            - (right |> Complex.ToCartesian)
            |> Complex.ToPolar

        static member inline public Multiply (left: Complex) (right: Complex) : Complex =

            let (Complex (leftMagnitude, leftPhase)) = left
            let (Complex (rightMagnitude, rightPhase)) = right

            Complex(leftMagnitude * rightMagnitude, leftPhase + rightPhase |> Complex.NormalizePhase)

        static member inline public Divide (left: Complex) (right: Complex) : Complex =

            let (Complex (leftMagnitude, leftPhase)) = left
            let (Complex (rightMagnitude, rightPhase)) = right

            Complex(leftMagnitude / rightMagnitude, leftPhase - rightPhase |> Complex.NormalizePhase)

        static member (+)(left: Complex, right: Complex) = Complex.Add left right
        static member (-)(left: Complex, right: Complex) = Complex.Subtract left right
        static member (*)(left: Complex, right: Complex) = Complex.Multiply left right
        static member (/)(left: Complex, right: Complex) = Complex.Divide left right
