namespace Algebra

type ComplexNumber = float * float

module ComplexNumber =

    let Add (x: ComplexNumber) (y: ComplexNumber) : ComplexNumber =
        let (xReal, xImaginary) = x
        let (yReal, yImaginary) = y
        (xReal + yReal, xImaginary + yImaginary)

    let Subtract (x: ComplexNumber) (y: ComplexNumber) : ComplexNumber =
        let (xReal, xImaginary) = x
        let (yReal, yImaginary) = y
        (xReal - yReal, xImaginary - yImaginary)

    let Multiply (x: ComplexNumber) (y: ComplexNumber) : ComplexNumber =
        let (xReal, xImaginary) = x
        let (yReal, yImaginary) = y
        (xReal * yReal - xImaginary * yImaginary, xReal * yImaginary + yReal * xImaginary)

    let ComplexConjucate (complexNumber: ComplexNumber) =
        let (real, imaginary) = complexNumber
        (real, -1.0 * imaginary)

    let Divide (numerator: ComplexNumber) (denominator: ComplexNumber) : ComplexNumber =
        let complexConjucateOfDenominator = ComplexConjucate denominator

        let numeratorMultipliedByComplexConjucate =
            Multiply numerator complexConjucateOfDenominator

        let denominatorMultipliedByComplexConjucate =
            Multiply denominator complexConjucateOfDenominator

        let (numeratorReal, numeratorImaginary) = numeratorMultipliedByComplexConjucate

        let (demoninatorReal, _) = denominatorMultipliedByComplexConjucate

        (numeratorReal / demoninatorReal, numeratorImaginary / demoninatorReal)

    let Modulus (complexNumber: ComplexNumber) =
        let (real, imaginary) = complexNumber
        System.Math.Sqrt(real * real + imaginary * imaginary)

type ComplexNumberOperators = ComplexNumberOperators
    with
        static member (+)(x: ComplexNumber, y: ComplexNumber) = ComplexNumber.Add x y
        static member (-)(x: ComplexNumber, y: ComplexNumber) = ComplexNumber.Subtract x y
        static member (*)(x: ComplexNumber, y: ComplexNumber) = ComplexNumber.Multiply x y
        static member (/)(x: ComplexNumber, y: ComplexNumber) = ComplexNumber.Divide x y
