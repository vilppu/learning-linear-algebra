namespace QuantumComputing



module Qubit =

    open Computation.Numbers
    open Computation.Numbers.Complex
    open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace

    let Qubit zeroWeight oneWeight : Ket = V [| zeroWeight; oneWeight |]
    let Zero: Ket = Qubit ComplexNumber.One ComplexNumber.Zero
    let One: Ket = Qubit ComplexNumber.Zero ComplexNumber.One
    let Q zeroWeight oneWeight = Qubit (C(zeroWeight)) (C(oneWeight))
    let Pair x y = Ket.TensorProduct x y

    let Probabilities (system: Ket) =
        system.Components.Entries
        |> Seq.map (fun qubit -> RealNumber.Square(Modulus qubit))
        |> Seq.toArray

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
