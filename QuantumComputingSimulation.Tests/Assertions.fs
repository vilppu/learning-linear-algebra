namespace QuantumComputing

open FluentAssertions

module Should =
    module BeEquivalentTo =
        module Real =

            let Number (expected: float) (actual: float) =
                actual.Should().Be(expected, "") |> ignore

            let Zero () (actual: float) = actual.Should().Be(0, "") |> ignore

            let One () (actual: float) = actual.Should().Be(1, "") |> ignore


        module Complex =

            open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace

            let Ket (expected: Ket) (actual: Ket) =
                actual.Should().Equal(expected, "") |> ignore

            let Bra (expected: Bra) (actual: Bra) =
                actual.Should().Equal(expected, "") |> ignore

            let Operator (expected: Operator) (actual: Operator) =
                actual.Should().BeEquivalentTo(expected, "") |> ignore
