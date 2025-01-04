using System.Numerics;

namespace Computation.Numbers;

public static class RealNumber<TRealNumber> where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static TRealNumber Pi = TRealNumber.Pi; 
    
    public static TRealNumber R(int number) =>
        TRealNumber.CreateChecked(number);

    public static TRealNumber R(float number) =>
        TRealNumber.CreateChecked(number);

    public static TRealNumber R(double number) =>
        TRealNumber.CreateChecked(number);

    public static TRealNumber Round(TRealNumber real) =>
      TRealNumber.Round(real, 6);

    public static TRealNumber Abs(TRealNumber real) =>
       TRealNumber.Abs(real);

    public static float Abs(int real) =>
       float.Abs(real);

    public static TRealNumber Sqrt(TRealNumber real) =>
       TRealNumber.Sqrt(real);

    public static TRealNumber Sqrt(int real) =>
       TRealNumber.Sqrt(R(real));

    public static TRealNumber Sqrt(float real) =>
       TRealNumber.Sqrt(R(real));

    public static TRealNumber Sqrt(double real) =>
        TRealNumber.Sqrt(R(real));

    public static TRealNumber Square(TRealNumber real) =>
       TRealNumber.CreateChecked(real) * TRealNumber.CreateChecked(real);

    public static TRealNumber Square(int real) =>
       TRealNumber.CreateChecked(real) * TRealNumber.CreateChecked(real);

    public static TRealNumber Square(float real) =>
       TRealNumber.CreateChecked(real) * TRealNumber.CreateChecked(real);

    public static TRealNumber Sin(TRealNumber real) =>
      TRealNumber.Sin(real);

    public static float Sin(int real) =>
       float.Sin(real);

    public static TRealNumber Cos(TRealNumber real) =>
      TRealNumber.Cos(real);

    public static float Cos(int real) =>
       float.Cos(real);

    public static TRealNumber Tan(TRealNumber real) =>
      TRealNumber.Tan(real);

    public static float Tan(int real) =>
       float.Tan(real);

    public static TRealNumber Atan(TRealNumber real) =>
      TRealNumber.Atan(real);

    public static float Atan(int real) =>
       float.Atan(real);
}

public static class RealExtensions
{
    public static TRealNumber Round<TRealNumber>(this TRealNumber real) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
      TRealNumber.Round(real, 6);
}