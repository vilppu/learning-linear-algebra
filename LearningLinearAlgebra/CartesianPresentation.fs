﻿namespace Algebra.ComplexNumbers

module CartesianPresentation =
    open RealNumbers

    type Complex =
        | Complex of float * float

        static member FromReal real = Complex(real, 0)
        static member Zero = Complex(0, 0)
        static member One = Complex.FromReal 1.0
        static member MinusOne = Complex.FromReal -1.0

        static member Add (left: Complex) (right: Complex) : Complex =

            let (Complex(leftReal, leftImaginary)) = left
            let (Complex(rightReal, rightImaginary)) = right

            Complex(leftReal + rightReal, leftImaginary + rightImaginary)

        static member Subtract (left: Complex) (right: Complex) : Complex =

            let (Complex(leftReal, leftImaginary)) = left
            let (Complex(rightReal, rightImaginary)) = right

            Complex(leftReal - rightReal, leftImaginary - rightImaginary)

        static member Multiply (left: Complex) (right: Complex) : Complex =

            let (Complex(leftReal, leftImaginary)) = left
            let (Complex(rightReal, rightImaginary)) = right

            Complex(
                leftReal * rightReal - leftImaginary * rightImaginary,
                leftReal * rightImaginary + rightReal * leftImaginary
            )

        static member Square(complex: Complex) : Complex = Complex.Multiply complex complex

        static member Conjucate(complexNumber: Complex) =
            let (Complex(real, imaginary)) = complexNumber

            Complex(real, -1.0 * imaginary)

        static member Divide (numerator: Complex) (denominator: Complex) : Complex =
            let complexConjucateOfDenominator = Complex.Conjucate denominator

            let numeratorMultipliedByComplexConjucate =
                Complex.Multiply numerator complexConjucateOfDenominator

            let denominatorMultipliedByComplexConjucate =
                Complex.Multiply denominator complexConjucateOfDenominator

            let (Complex(numeratorReal, numeratorImaginary)) =
                numeratorMultipliedByComplexConjucate

            let (Complex(demoninatorReal, _)) = denominatorMultipliedByComplexConjucate

            Complex(numeratorReal / demoninatorReal, numeratorImaginary / demoninatorReal)

        static member Modulus(Complex(real, imaginary)) =
            sqrt (real * real + imaginary * imaginary)

        static member SquareRoot(Complex(real, imaginary)) : Complex =
            Complex(sqrt (abs (real)), sqrt (abs ((imaginary))))

        static member Round(Complex(real, imaginary)) : Complex = Complex(Round(real), Round(imaginary))

        static member RoundToTwoDecimals(Complex(real, imaginary)) : Complex =
            Complex(RoundToTwoDecimals(real), RoundToTwoDecimals(imaginary))

        static member inline (+)(left: Complex, right: Complex) = Complex.Add left right
        static member inline (-)(left: Complex, right: Complex) = Complex.Subtract left right
        static member inline (*)(left: Complex, right: Complex) = Complex.Multiply left right
        static member inline (/)(left: Complex, right: Complex) = Complex.Divide left right

    let C = Complex
