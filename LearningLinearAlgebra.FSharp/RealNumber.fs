namespace LearningLinearAlgebra.Numbers

module RealNumber =

    let Pi = 3.141592653589
    let Round (value: float) : float = System.Math.Round(value, 6)
    let RoundToTwoDecimals (value: float) : float = System.Math.Round(value, 2)
    let Square (value: float) : float = value * value
