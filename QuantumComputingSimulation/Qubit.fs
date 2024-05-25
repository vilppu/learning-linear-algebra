namespace QuantumComputing



module Qubit =

    open LearningLinearAlgebra.Numbers
    open LearningLinearAlgebra.Numbers.Complex
    open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace

    let Qubit zeroWeight oneWeight : Ket<float> = V [| zeroWeight; oneWeight |]
    let Zero: Ket<float> = Qubit ComplexNumber<float>.One ComplexNumber<float>.Zero
    let One: Ket<float> = Qubit ComplexNumber<float>.Zero ComplexNumber<float>.One
    let Q zeroWeight oneWeight = Qubit (C(zeroWeight)) (C(oneWeight))
    let Pair x y = Ket.TensorProduct x y

    let Probabilities (system: Ket<float>) =
        system.Components.Entries |> Array.map (fun qubit -> Square(Modulus qubit))

    let TopZero pair =
        Probabilities(pair)[0] + Probabilities(pair)[1]

    let TopOne pair =
        Probabilities(pair)[2] + Probabilities(pair)[3]

    let BottomZero pair =
        Probabilities(pair)[0] + Probabilities(pair)[2]

    let BottomOne pair =
        Probabilities(pair)[1] + Probabilities(pair)[3]

    let PropabilityOfZero qubit = qubit |> State.Probability 0
    let PropabilityOfOne qubit = qubit |> State.Probability 1
