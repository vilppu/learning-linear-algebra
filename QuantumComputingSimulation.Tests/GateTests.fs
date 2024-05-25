namespace QuantumComputing

open Xunit

module GateTests =
    open State
    open Qubit
    open Gates

    [<Fact>]
    let ``XOR puts zero and zero to zero`` () =
        let left = Zero
        let right = Zero

        let result = Xor * (Pair left right)

        result |> Should.BeEquivalentTo.Complex.Ket(Zero)

    [<Fact>]
    let ``XOR puts zero and one to one`` () =
        let left = Zero
        let right = One

        let result = Xor * (Pair left right)

        result |> Should.BeEquivalentTo.Complex.Ket(One)

    [<Fact>]
    let ``XOR puts one and one to zero`` () =
        let left = One
        let right = One

        let result = Xor * (Pair left right)

        result |> Should.BeEquivalentTo.Complex.Ket(Zero)

    [<Fact>]
    let ``XOR puts one and zero to one`` () =
        let left = One
        let right = Zero

        let result = Xor * (Pair left right)

        result |> Should.BeEquivalentTo.Complex.Ket(One)

    [<Fact>]
    let ``Hadamart puts zero qubit to state where there is 50% change to be zero or one`` () =
        let zero = Q (1.0, 0.0) (0.0, 0.0)

        let result = Hadamart * zero

        let probabilityOfZero = result |> Probability 0
        let probabilityOfOne = result |> Probability 1

        probabilityOfZero |> Should.BeEquivalentTo.Real.Number(0.5)
        probabilityOfOne |> Should.BeEquivalentTo.Real.Number(0.5)

    [<Fact>]
    let ``Balanced "zero to one" puts zero to one`` () =
        let qubitToBeEvaluated = Zero
        let controlQubit = Zero

        let input = (Pair qubitToBeEvaluated controlQubit)

        BalancedZeroToOne * input
        |> Should.BeEquivalentTo.Complex.Ket((Pair qubitToBeEvaluated One))

    [<Fact>]
    let ``Balanced "zero to one" puts zero to one and one to zero with control qubit XORing the result`` () =

        let controlQubitZero = Zero
        let controlQubitOne = One

        BalancedZeroToOne * (Pair Zero controlQubitZero)
        |> Should.BeEquivalentTo.Complex.Ket((Pair Zero One))

        BalancedZeroToOne * (Pair One controlQubitZero)
        |> Should.BeEquivalentTo.Complex.Ket((Pair One Zero))

        BalancedZeroToOne * (Pair Zero controlQubitOne)
        |> Should.BeEquivalentTo.Complex.Ket((Pair Zero Zero))

        BalancedZeroToOne * (Pair One controlQubitOne)
        |> Should.BeEquivalentTo.Complex.Ket((Pair One One))

    [<Fact>]
    let ``Balanced "zero to zero" puts zero to zero and one to one with control qubit XORing the result`` () =

        let controlQubitZero = Zero
        let controlQubitOne = One

        BalancedZeroToZero * (Pair Zero controlQubitZero)
        |> Should.BeEquivalentTo.Complex.Ket((Pair Zero Zero))

        BalancedZeroToZero * (Pair One controlQubitZero)
        |> Should.BeEquivalentTo.Complex.Ket((Pair One One))

        BalancedZeroToZero * (Pair Zero controlQubitOne)
        |> Should.BeEquivalentTo.Complex.Ket((Pair Zero One))

        BalancedZeroToZero * (Pair One controlQubitOne)
        |> Should.BeEquivalentTo.Complex.Ket((Pair One Zero))

    [<Fact>]
    let ``Constant "to zero" puts zero to zero and one to zero with control qubit XORing the result`` () =

        let controlQubitZero = Zero
        let controlQubitOne = One

        ConstantToZero * (Pair Zero controlQubitZero)
        |> Should.BeEquivalentTo.Complex.Ket((Pair Zero Zero))

        ConstantToZero * (Pair One controlQubitZero)
        |> Should.BeEquivalentTo.Complex.Ket((Pair One Zero))

        ConstantToZero * (Pair Zero controlQubitOne)
        |> Should.BeEquivalentTo.Complex.Ket((Pair Zero One))

        ConstantToZero * (Pair One controlQubitOne)
        |> Should.BeEquivalentTo.Complex.Ket((Pair One One))

    [<Fact>]
    let ``Constant "to one" puts zero to one and one to one with control qubit XORing the result`` () =

        let controlQubitZero = Zero
        let controlQubitOne = One

        ConstantToOne * (Pair Zero controlQubitZero)
        |> Should.BeEquivalentTo.Complex.Ket((Pair One Zero))

        ConstantToOne * (Pair One controlQubitZero)
        |> Should.BeEquivalentTo.Complex.Ket((Pair One Zero))

        ConstantToOne * (Pair Zero controlQubitOne)
        |> Should.BeEquivalentTo.Complex.Ket((Pair One One))

        ConstantToOne * (Pair One controlQubitOne)
        |> Should.BeEquivalentTo.Complex.Ket((Pair One One))
