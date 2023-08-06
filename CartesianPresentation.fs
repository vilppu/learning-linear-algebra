namespace Algebra.ComplexNumbers

module CartesianPresentation =

    type Complex = Complex of float * float

    let Complex (real, imaginary) = Complex(real, imaginary)

    let Add (x: Complex) (y: Complex) : Complex =

        let (Complex (xReal, xImaginary)) = x
        let (Complex (yReal, yImaginary)) = y

        Complex(xReal + yReal, xImaginary + yImaginary)

    let Subtract (x: Complex) (y: Complex) : Complex =

        let (Complex (xReal, xImaginary)) = x
        let (Complex (yReal, yImaginary)) = y

        Complex(xReal - yReal, xImaginary - yImaginary)

    let Multiply (x: Complex) (y: Complex) : Complex =

        let (Complex (xReal, xImaginary)) = x
        let (Complex (yReal, yImaginary)) = y

        Complex(xReal * yReal - xImaginary * yImaginary, xReal * yImaginary + yReal * xImaginary)

    let ComplexConjucate (complexNumber: Complex) =
        let (Complex (real, imaginary)) = complexNumber

        Complex(real, -1.0 * imaginary)

    let Divide (numerator: Complex) (denominator: Complex) : Complex =
        let complexConjucateOfDenominator = ComplexConjucate denominator

        let numeratorMultipliedByComplexConjucate =
            Multiply numerator complexConjucateOfDenominator

        let denominatorMultipliedByComplexConjucate =
            Multiply denominator complexConjucateOfDenominator

        let (Complex (numeratorReal, numeratorImaginary)) =
            numeratorMultipliedByComplexConjucate

        let (Complex (demoninatorReal, _)) = denominatorMultipliedByComplexConjucate

        Complex(numeratorReal / demoninatorReal, numeratorImaginary / demoninatorReal)

    let Modulus (complexNumber: Complex) =
        let (Complex (real, imaginary)) = complexNumber
        System.Math.Sqrt(real * real + imaginary * imaginary)

    type CartesianOperators = CartesianOperators
        with
            static member (+)(x: Complex, y: Complex) = Add x y
            static member (-)(x: Complex, y: Complex) = Subtract x y
            static member (*)(x: Complex, y: Complex) = Multiply x y
            static member (/)(x: Complex, y: Complex) = Divide x y
