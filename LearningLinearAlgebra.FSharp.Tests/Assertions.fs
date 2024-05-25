namespace LearningLinearAlgebra

open FluentAssertions

module Should =
    module BeEquivalentTo =
        module Real =

            open LearningLinearAlgebra.Matrices.Real

            let Number (expected: float) (actual: float) =
                actual.Should().Be(expected, "") |> ignore

            let RowVector (expected: RowVector<'R>) (actual: RowVector<'R>) =
                actual.Should().BeEquivalentTo(expected, "") |> ignore

            let ColumnVector (expected: ColumnVector<'R>) (actual: ColumnVector<'R>) =
                actual.Should().BeEquivalentTo(expected, "") |> ignore

            let SquareMatrix (expected: SquareMatrix<'R>) (actual: SquareMatrix<'R>) =
                actual.Should().BeEquivalentTo(expected, "") |> ignore

        module Complex =

            open LearningLinearAlgebra.Numbers
            open LearningLinearAlgebra.Numbers.Complex
            open LearningLinearAlgebra.Matrices.Complex
            open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace

            let Number (expected: ComplexNumber<'R>) (actual: ComplexNumber<'R>) =
                actual.Should().Be(expected, "") |> ignore

            let RowVector (expected: RowVector<'R>) (actual: RowVector<'R>) =
                actual.Should().BeEquivalentTo(expected, "") |> ignore

            let ColumnVector (expected: ColumnVector<'R>) (actual: ColumnVector<'R>) =
                actual.Should().BeEquivalentTo(expected, "") |> ignore

            let SquareMatrix (expected: SquareMatrix<'R>) (actual: SquareMatrix<'R>) =
                actual.Should().BeEquivalentTo(expected, "") |> ignore

            let Ket (expected: Ket<'R>) (actual: Ket<'R>) =
                actual.Should().BeEquivalentTo(expected, "") |> ignore

            let Bra (expected: Bra<'R>) (actual: Bra<'R>) =
                actual.Should().BeEquivalentTo(expected, "") |> ignore

            let Operator (expected: Operator<'R>) (actual: Operator<'R>) =
                actual.Should().BeEquivalentTo(expected, "") |> ignore
