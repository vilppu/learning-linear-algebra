namespace LearningLinearAlgebra.Numbers

type ComplexNumber = ComplexNumber<float>

module Complex =

    let SqrtTwo: ComplexNumber<float> =
        ComplexNumber<float>.Sqrt(ComplexNumber<float>.Two)

    let OnePerSqrtTwo =
        (ComplexNumber<float>.One / ComplexNumber<float>.Sqrt(ComplexNumber<float>.Two))

    let Add left right = ComplexNumber<'R>.Add(left, right)

    let Subtract left right = ComplexNumber<'R>.Subtract(left, right)

    let Multiply (left: ComplexNumber<'R>) (right: ComplexNumber<'R>) = ComplexNumber<'R>.Multiply(left, right)

    let Square (complex: ComplexNumber<'R>) = ComplexNumber<'R>.Square(complex)

    let Conjucate complex = ComplexNumber<'R>.Conjucate(complex)

    let Divide left right = ComplexNumber<'R>.Divide(left, right)

    let Modulus (complex: ComplexNumber<'R>) = ComplexNumber<'R>.Modulus(complex)

    let AdditiveInverse complex =
        ComplexNumber<'R>.AdditiveInverse(complex)

    let Round complex = ComplexNumber<'R>.Round(complex)

    let CreateComplex<'R when 'R :> System.Numerics.IFloatingPointIeee754<'R>> (real: float, imaginary: float) =
        ComplexNumber<'R>.C(real, imaginary)

    let C (real, imaginary) = CreateComplex<float>(real, imaginary)
