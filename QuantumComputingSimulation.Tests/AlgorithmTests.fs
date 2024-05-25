namespace QuantumComputing

open Xunit

module AlgorithmTests =

    open LearningLinearAlgebra.Numbers.RealNumber 
    open Qubit
    open Algorithms

    [<Fact>]
    let ``Deutch algorithm returns zero at top qubit if function is constant`` () =

        let result = Deutsch ConstantToOne Zero One
        let probabilityOfZeroOnTop = TopZero result

        probabilityOfZeroOnTop |> Round |> Should.BeEquivalentTo.Real.Number(1.0)

        let result = Deutsch ConstantToZero Zero One
        let probabilityOfZeroOnTop = TopZero result

        probabilityOfZeroOnTop |> Round |> Should.BeEquivalentTo.Real.Number(1.0)


    [<Fact>]
    let ``Deutch algorithm returns one at top qubit if function is balanced`` () =

        let result = Deutsch BalancedZeroToZero Zero One
        let probabilityOfOneOnTop = TopOne result

        probabilityOfOneOnTop |> Round |> Should.BeEquivalentTo.Real.Number(1.0)

        let result = Deutsch BalancedZeroToOne Zero One
        let probabilityOfOneOnTop = TopOne result

        probabilityOfOneOnTop |> Round |> Should.BeEquivalentTo.Real.Number(1.0)
