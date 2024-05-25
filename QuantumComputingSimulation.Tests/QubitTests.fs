namespace QuantumComputing

open Xunit

module ComputationTests =
    open Qubit

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
