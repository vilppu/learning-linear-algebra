using System.Numerics;

namespace LearningLinearAlgebra.Numbers;

public record Polar<TRealNumber>(TRealNumber Magnitude, TRealNumber Phase) where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static Polar<TRealNumber> P(TRealNumber magniture, TRealNumber phase) =>
         new(magniture, phase);

    public static TRealNumber NormalizePhase(TRealNumber phase) =>
        PositiveModulo(phase, TRealNumber.Pi * (TRealNumber.One + TRealNumber.One));

    public static Polar<TRealNumber> ToPolar(ComplexNumber<TRealNumber> cartesian) =>
        new(
            RealNumber<TRealNumber>.Sqrt(cartesian.Real * cartesian.Real + cartesian.Imaginary * cartesian.Imaginary),
            TRealNumber.Atan2(cartesian.Imaginary, cartesian.Real)
        );

    public static ComplexNumber<TRealNumber> ToCartesian(Polar<TRealNumber> polar) =>
        new(
            polar.Magnitude * TRealNumber.Cos(polar.Phase),
            polar.Magnitude * TRealNumber.Sin(polar.Phase)
        );

    public static Polar<TRealNumber> Add(Polar<TRealNumber> left, Polar<TRealNumber> right) =>
        ToPolar(ToCartesian(left) + ToCartesian(right));

    public static Polar<TRealNumber> Subtract(Polar<TRealNumber> left, Polar<TRealNumber> right) =>
        ToPolar(ToCartesian(left) - ToCartesian(right));

    public static Polar<TRealNumber> Multiply(Polar<TRealNumber> left, Polar<TRealNumber> right) =>
        new(
            left.Magnitude * right.Magnitude,
            NormalizePhase(left.Phase + right.Phase)
        );

    public static Polar<TRealNumber> Divide(Polar<TRealNumber> left, Polar<TRealNumber> right) =>
        new(
            left.Magnitude / right.Magnitude,
            NormalizePhase(left.Phase - right.Phase)
        );

    public static Polar<TRealNumber> Round(Polar<TRealNumber> polar) =>
        P(polar.Magnitude.Round(), polar.Phase.Round());

    private static TRealNumber PositiveModulo(TRealNumber dividend, TRealNumber divisor) =>
        (dividend % divisor + divisor) % divisor;

    public static Polar<TRealNumber> operator +(Polar<TRealNumber> left, Polar<TRealNumber> right) => Add(left, right);
    public static Polar<TRealNumber> operator -(Polar<TRealNumber> left, Polar<TRealNumber> right) => Subtract(left, right);
    public static Polar<TRealNumber> operator *(Polar<TRealNumber> left, Polar<TRealNumber> right) => Multiply(left, right);
    public static Polar<TRealNumber> operator /(Polar<TRealNumber> left, Polar<TRealNumber> right) => Divide(left, right);
}
