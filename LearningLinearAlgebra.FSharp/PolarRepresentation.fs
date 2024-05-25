namespace LearningLinearAlgebra.Numbers


module PolarPresentation =

    module Complex =

        let NormalizePhase phase = Polar<float>.NormalizePhase(phase)

        let ToPolar cartesian = Polar<float>.ToPolar(cartesian)

        let ToCartesian cartesian = Polar<float>.ToCartesian(cartesian)

        let Add left right = Polar<float>.Add(left, right)

        let Subtract left right = Polar<float>.Subtract(left, right)

        let Multiply left right = Polar<float>.Multiply(left, right)

        let Divide left right = Polar<float>.Divide(left, right)

        let Round number = Polar<float>.Round(number)

    let C = Polar<float>
    let Components (polar: Polar<float>) = (polar.Magnitude, polar.Phase)
