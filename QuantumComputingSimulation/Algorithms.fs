namespace QuantumComputing

module Algorithms =

    open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace
    open Qubit
    open Gates

    let DeutschOneLiner (gate: Operator<float>) input control =
        ((Hadamart * I) * (gate * (Hadamart * Hadamart))) * (Pair input control)

    let Deutsch (gate: Operator<float>) (input: Ket<float>) (control: Ket<float>) =

        let topInSuperPosition = (Hadamart * input)
        let bottomInSuperPosition = (Hadamart * control)
        let inputInSuperPosition = Pair topInSuperPosition bottomInSuperPosition
        let functionAppliedToInput = gate * inputInSuperPosition
        let hadamartPair = BinaryGate Hadamart Hadamart
        let resultInCanonicalBasis = hadamartPair * functionAppliedToInput
        resultInCanonicalBasis
