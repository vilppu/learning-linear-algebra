using System.Numerics;

namespace LearningLinearAlgebra.Numbers;

public record ComplexNumber<TRealNumber>(TRealNumber Real, TRealNumber Imaginary) where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static readonly ComplexNumber<TRealNumber> One = C(TRealNumber.One, TRealNumber.Zero);
    public static readonly ComplexNumber<TRealNumber> Two = C(TRealNumber.One + TRealNumber.One, TRealNumber.Zero);
    public static readonly ComplexNumber<TRealNumber> NegativeOne = C(TRealNumber.NegativeOne, TRealNumber.Zero);
    public static readonly ComplexNumber<TRealNumber> Zero = C(TRealNumber.Zero, TRealNumber.Zero);

    public static ComplexNumber<TRealNumber> C(TRealNumber real, TRealNumber imaginary) =>
         new(real, imaginary);

    public static ComplexNumber<TRealNumber> C(TRealNumber real, double imaginary) =>
         new(real, RealNumber<TRealNumber>.R(imaginary));

    public static ComplexNumber<TRealNumber> C(float real, float imaginary) =>
         new(RealNumber<TRealNumber>.R(real), RealNumber<TRealNumber>.R(imaginary));

    public static ComplexNumber<TRealNumber> C(double real, double imaginary) =>
         new(RealNumber<TRealNumber>.R(real), RealNumber<TRealNumber>.R(imaginary));

    public static ComplexNumber<TRealNumber> C(ComplexNumber<float> complex) =>
         C(complex.Real, complex.Imaginary);

    public static ComplexNumber<TRealNumber> C(ComplexNumber<double> complex) =>
         C(complex.Real, complex.Imaginary);

    public static ComplexNumber<TRealNumber> C(TRealNumber real) =>
         new(real, TRealNumber.Zero);

    public static ComplexNumber<TRealNumber> Add(ComplexNumber<TRealNumber> left, ComplexNumber<TRealNumber> right) =>
        C(left.Real + right.Real, left.Imaginary + right.Imaginary);

    public static ComplexNumber<TRealNumber> Subtract(ComplexNumber<TRealNumber> left, ComplexNumber<TRealNumber> right) =>
        C(left.Real - right.Real, left.Imaginary - right.Imaginary);

    public static ComplexNumber<TRealNumber> Multiply(ComplexNumber<TRealNumber> left, ComplexNumber<TRealNumber> right) =>
        C(left.Real * right.Real - left.Imaginary * right.Imaginary,
          left.Real * right.Imaginary + right.Real * left.Imaginary);

    public static ComplexNumber<TRealNumber> Multiply(ComplexNumber<TRealNumber> complex, TRealNumber real) =>
        C(complex.Real * real, complex.Imaginary * real);

    public static ComplexNumber<TRealNumber> Multiply(TRealNumber real, ComplexNumber<TRealNumber> complex) =>
        Multiply(complex, real);

    public static ComplexNumber<TRealNumber> Divide(ComplexNumber<TRealNumber> numerator, ComplexNumber<TRealNumber> denominator)
    {
        var complexConjucateOfDenominator = Conjucate(denominator);
        var numeratorMultipliedByComplexConjucate = Multiply(numerator, complexConjucateOfDenominator);
        var denominatorMultipliedByComplexConjucate = Multiply(denominator, complexConjucateOfDenominator);

        return C(
            numeratorMultipliedByComplexConjucate.Real / denominatorMultipliedByComplexConjucate.Real,
            numeratorMultipliedByComplexConjucate.Imaginary / denominatorMultipliedByComplexConjucate.Real);
    }

    public static ComplexNumber<TRealNumber> Square(ComplexNumber<TRealNumber> complex) =>
        Multiply(complex, complex);

    public static ComplexNumber<TRealNumber> Square(TRealNumber real) =>
        Multiply(C(real), C(real));

    public static ComplexNumber<TRealNumber> Conjucate(ComplexNumber<TRealNumber> complex) =>
        C(complex.Real, TRealNumber.NegativeOne * complex.Imaginary);

    public static TRealNumber Modulus(ComplexNumber<TRealNumber> complex) =>
        RealNumber<TRealNumber>.Sqrt(complex.Real * complex.Real + complex.Imaginary * complex.Imaginary);

    public static ComplexNumber<TRealNumber> Sqrt(ComplexNumber<TRealNumber> complex) =>
       C(RealNumber<TRealNumber>.Sqrt(RealNumber<TRealNumber>.Abs(complex.Real)), RealNumber<TRealNumber>.Sqrt(RealNumber<TRealNumber>.Abs(complex.Imaginary)));

    public static ComplexNumber<TRealNumber> AdditiveInverse(ComplexNumber<TRealNumber> complex) =>
        C(-complex.Real, -complex.Imaginary);

    public static ComplexNumber<TRealNumber> Round(ComplexNumber<TRealNumber> complex) =>
        C(complex.Real.Round(), complex.Imaginary.Round());

    public static ComplexNumber<TRealNumber> operator +(ComplexNumber<TRealNumber> left, ComplexNumber<TRealNumber> right) => Add(left, right);
    public static ComplexNumber<TRealNumber> operator -(ComplexNumber<TRealNumber> left, ComplexNumber<TRealNumber> right) => Subtract(left, right);
    public static ComplexNumber<TRealNumber> operator *(ComplexNumber<TRealNumber> left, ComplexNumber<TRealNumber> right) => Multiply(left, right);
    public static ComplexNumber<TRealNumber> operator *(ComplexNumber<TRealNumber> complex, TRealNumber real) => Multiply(complex, real);
    public static ComplexNumber<TRealNumber> operator *(TRealNumber real, ComplexNumber<TRealNumber> complex) => Multiply(real, complex);
    public static ComplexNumber<TRealNumber> operator /(ComplexNumber<TRealNumber> left, ComplexNumber<TRealNumber> right) => Divide(left, right);
    public static ComplexNumber<TRealNumber> operator -(ComplexNumber<TRealNumber> complex) => AdditiveInverse(complex);
    public static implicit operator ComplexNumber<TRealNumber>(TRealNumber real) => C(real, TRealNumber.Zero);
    public static implicit operator ComplexNumber<TRealNumber>(int real) => C(RealNumber<TRealNumber>.R(real), TRealNumber.Zero);
    public static implicit operator ComplexNumber<TRealNumber>((int Real, int Imaginary) tuple) => C(tuple.Real, tuple.Imaginary);
    public static implicit operator ComplexNumber<TRealNumber>((TRealNumber Real, int Imaginary) tuple) => C(tuple.Real, tuple.Imaginary);
    public static implicit operator ComplexNumber<TRealNumber>((TRealNumber Real, TRealNumber Imaginary) tuple) => C(tuple.Real, tuple.Imaginary);
    public static implicit operator ComplexNumber<TRealNumber>((float Real, float Imaginary) tuple) => C(tuple.Real, tuple.Imaginary);
    public static implicit operator ComplexNumber<TRealNumber>((double Real, double Imaginary) tuple) => C(tuple.Real, tuple.Imaginary);
}

public static class OperationsAsExtensions
{
    public static ComplexNumber<TRealNumber> Add<TRealNumber>(this ComplexNumber<TRealNumber> left, ComplexNumber<TRealNumber> right) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexNumber<TRealNumber>.Add(left, right);

    public static ComplexNumber<TRealNumber> Subtract<TRealNumber>(this ComplexNumber<TRealNumber> left, ComplexNumber<TRealNumber> right) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexNumber<TRealNumber>.Subtract(left, right);

    public static ComplexNumber<TRealNumber> Multiply<TRealNumber>(this ComplexNumber<TRealNumber> left, ComplexNumber<TRealNumber> right) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexNumber<TRealNumber>.Multiply(left, right);

    public static ComplexNumber<TRealNumber> Multiply<TRealNumber>(this TRealNumber scalar, ComplexNumber<TRealNumber> complex) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexNumber<TRealNumber>.Multiply(scalar, complex);

    public static ComplexNumber<TRealNumber> Divide<TRealNumber>(this ComplexNumber<TRealNumber> numerator, ComplexNumber<TRealNumber> denominator) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexNumber<TRealNumber>.Divide(numerator, denominator);

    public static ComplexNumber<TRealNumber> Square<TRealNumber>(this ComplexNumber<TRealNumber> complex) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexNumber<TRealNumber>.Square(complex);

    public static ComplexNumber<TRealNumber> Conjucate<TRealNumber>(this ComplexNumber<TRealNumber> complex) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexNumber<TRealNumber>.Conjucate(complex);

    public static TRealNumber Modulus<TRealNumber>(this ComplexNumber<TRealNumber> complex) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexNumber<TRealNumber>.Modulus(complex);

    public static ComplexNumber<TRealNumber> Sqrt<TRealNumber>(this ComplexNumber<TRealNumber> complex) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexNumber<TRealNumber>.Sqrt(complex);

    public static ComplexNumber<TRealNumber> AdditiveInverse<TRealNumber>(this ComplexNumber<TRealNumber> complex) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexNumber<TRealNumber>.Sqrt(complex);

    public static ComplexNumber<TRealNumber> Round<TRealNumber>(this ComplexNumber<TRealNumber> complex) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexNumber<TRealNumber>.Round(complex);
}
