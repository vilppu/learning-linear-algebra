﻿using System.Numerics;

namespace Computation.Numbers;

public static class NumberFormatting
{
    public static string Formatted<TRealNumber>(this ComplexNumber<TRealNumber> complexNumber)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> => 
        $"({complexNumber.Real}, {complexNumber.Imaginary})";
}