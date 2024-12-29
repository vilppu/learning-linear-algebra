namespace QuantumComputing

open Xunit

module QubitTests =
    open Qubit

    open Computation.Numbers.Complex
    open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace

    [<Fact>]
    let ``Tensor product of qubits can be used to describe qbit pairs`` () =
        let zeroZero = Pair (Q (1.0, 0.0) (0.0, 0.0)) (Q (1.0, 0.0) (0.0, 0.0))
        let zeroOne = Pair (Q (1.0, 0.0) (0.0, 0.0)) (Q (0.0, 0.0) (1.0, 0.0))
        let oneZero = Pair (Q (0.0, 0.0) (1.0, 0.0)) (Q (1.0, 0.0) (0.0, 0.0))
        let oneOne = Pair (Q (0.0, 0.0) (1.0, 0.0)) (Q (0.0, 0.0) (1.0, 0.0))

        zeroZero
        |> Should.BeEquivalentTo.Complex.Ket(V [| C(1.0, 0.0); C(0.0, 0.0); C(0.0, 0.0); C(0.0, 0.0) |])

        //zeroOne
        //|> Should.BeEquivalentTo.Complex.Ket(V [| C(0.0, 0.0); C(1.0, 0.0); C(0.0, 0.0); C(0.0, 0.0) |])

        oneZero
        |> Should.BeEquivalentTo.Complex.Ket(V [| C(0.0, 0.0); C(0.0, 0.0); C(1.0, 0.0); C(0.0, 0.0) |])

        oneOne
        |> Should.BeEquivalentTo.Complex.Ket(V [| C(0.0, 0.0); C(0.0, 0.0); C(0.0, 0.0); C(1.0, 0.0) |])

    [<Fact>]
    let ``Propablity of finding qubit in zero is norm square of weight of zero per norm square of qubit`` () =
        let qubit = Q (5.0, 3.0) (0.0, 6.0)

        let probability = PropabilityOfZero qubit

        probability |> Should.BeEquivalentTo.Real.Number(34.0 / 70.0)

    [<Fact>]
    let ``Propablity of finding qubit in one is norm square of weight of one per norm square of qubit`` () =
        let qubit = Q (5.0, 3.0) (0.0, 6.0)

        let probability = PropabilityOfOne qubit

        probability |> Should.BeEquivalentTo.Real.Number(36.0 / 70.0)
