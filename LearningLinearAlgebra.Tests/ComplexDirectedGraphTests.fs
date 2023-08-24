namespace Application

open Xunit

module ComplexDirectedGraphTests =
    open Algebra.ComplexVectorSpace
    open Algebra.ComplexNumbers.CartesianPresentation

    [<Fact>]
    let ``Change of graph state presented by column vector can be described by square adjacency matrix`` () =
        let initialState =
            Matrix [| [| Complex(1.0 / sqrt 3.0, 0) |]
                      [| Complex(0, 2.0 / sqrt 15.0) |]
                      [| Complex(sqrt (2.0 / 5.0), 0) |] |]

        let adjacencyMatrix =
            Matrix [| [| Complex(1.0 / sqrt 2.0, 0)
                         Complex(1.0 / sqrt 2.0, 0)
                         Complex(0, 0) |]
                      [| Complex(0, -1.0 / sqrt 2.0)
                         Complex(0, 1.0 / sqrt 2.0)
                         Complex(0, 0) |]
                      [| Complex(0, 0)
                         Complex(0, 0)
                         Complex(0, 1) |] |]

        let nextState = adjacencyMatrix * initialState

        Assert.Equal(
            Matrix [| [| Complex(1.0 / sqrt 6.0, sqrt (2.0 / 15.0)) |]
                      [| Complex(-2.0 / sqrt 30.0, (-1.0 * sqrt 5.0) / sqrt 30.0) |]
                      [| Complex(0, sqrt (2.0 / 5.0)) |] |]
            |> Matrix.Round,
            nextState |> Matrix.Round
        )


    [<Fact>]
    let ``Multiplying all state transition matrices gives a matrix that does all those transitions`` () =
        let initialState =
            Matrix [| [| Complex(1.0 / sqrt 3.0, 0) |]
                      [| Complex(0, 2.0 / sqrt 15.0) |]
                      [| Complex(sqrt (2.0 / 5.0), 0) |] |]

        let adjacencyMatrix =
            Matrix [| [| Complex(1.0 / sqrt 2.0, 0)
                         Complex(1.0 / sqrt 2.0, 0)
                         Complex(0, 0) |]
                      [| Complex(0, -1.0 / sqrt 2.0)
                         Complex(0, 1.0 / sqrt 2.0)
                         Complex(0, 0) |]
                      [| Complex(0, 0)
                         Complex(0, 0)
                         Complex(0, 1) |] |]

        let applyTransition = Matrix.Product adjacencyMatrix

        let applyThreeTransitions =
            Matrix.Product(
                adjacencyMatrix
                * adjacencyMatrix
                * adjacencyMatrix
            )

        Assert.Equal(
            initialState
            |> applyTransition
            |> applyTransition
            |> applyTransition
            |> Matrix.Round,
            initialState
            |> applyThreeTransitions
            |> Matrix.Round
        )

    [<Fact>]
    let ``Transposing the adjacency matrix reverses the graph dynamics`` () =

        let initialState =
            Matrix [| [| Complex(1.0 / sqrt 3.0, 0) |]
                      [| Complex(0, 2.0 / sqrt 15.0) |]
                      [| Complex(sqrt (2.0 / 5.0), 0) |] |]

        let adjacencyMatrix =
            Matrix [| [| Complex(1.0 / sqrt 2.0, 0)
                         Complex(1.0 / sqrt 2.0, 0)
                         Complex(0, 0) |]
                      [| Complex(0, -1.0 / sqrt 2.0)
                         Complex(0, 1.0 / sqrt 2.0)
                         Complex(0, 0) |]
                      [| Complex(0, 0)
                         Complex(0, 0)
                         Complex(0, 1) |] |]

        let transposeOfAdjacencyMatrix = Matrix.Adjoint adjacencyMatrix

        let nextState = adjacencyMatrix * initialState
        let reversedState = transposeOfAdjacencyMatrix * nextState

        Assert.Equal(initialState |> Matrix.Round, reversedState |> Matrix.Round)
