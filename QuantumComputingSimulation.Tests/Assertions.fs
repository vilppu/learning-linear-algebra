namespace QuantumComputing

open FluentAssertions

module Should =
    module BeEquivalentTo =
        module Real =

            let Number (expected: float) (actual: float) =
                actual.Should().Be(expected, "") |> ignore


        module Complex =

            open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace

            let Ket (expected: Ket<'R>) (actual: Ket<'R>) =
                actual.Should().BeEquivalentTo(expected, "") |> ignore

            let Bra (expected: Bra<'R>) (actual: Bra<'R>) =
                actual.Should().BeEquivalentTo(expected, "") |> ignore

            let Operator (expected: Operator<'R>) (actual: Operator<'R>) =
                actual.Should().BeEquivalentTo(expected, "") |> ignore
