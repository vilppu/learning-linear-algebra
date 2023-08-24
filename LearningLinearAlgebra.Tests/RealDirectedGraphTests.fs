namespace Application

open Xunit

module RealDirectedGraphTests =
    open Algebra.RealVectorSpace

    [<Fact>]
    let ``Change of graph state presented by column vector can be described by square adjacency matrix`` () =
        let initialState =
            Matrix [| [| 6 |]
                      [| 2 |]
                      [| 1 |]
                      [| 5 |]
                      [| 3 |]
                      [| 10 |] |]

        let adjacencyMatrix =
            Matrix [| [| 0; 0; 0; 0; 0; 0 |]
                      [| 0; 0; 0; 0; 0; 0 |]
                      [| 0; 1; 0; 0; 0; 1 |]
                      [| 0; 0; 0; 1; 0; 0 |]
                      [| 0; 0; 1; 0; 0; 0 |]
                      [| 1; 0; 0; 0; 1; 0 |] |]

        let nextState = adjacencyMatrix * initialState

        Assert.Equal(
            Matrix [| [| 0 |]
                      [| 0 |]
                      [| 12 |]
                      [| 5 |]
                      [| 1 |]
                      [| 9 |] |],
            nextState
        )

    [<Fact>]
    let ``Change of graph state presented by vector can be described by adjacency matrix`` () =
        let initialState = Vector [| 6; 2; 1; 5; 3; 10 |]

        let adjacencyMatrix =
            Matrix [| [| 0; 0; 0; 0; 0; 0 |]
                      [| 0; 0; 0; 0; 0; 0 |]
                      [| 0; 1; 0; 0; 0; 1 |]
                      [| 0; 0; 0; 1; 0; 0 |]
                      [| 0; 0; 1; 0; 0; 0 |]
                      [| 1; 0; 0; 0; 1; 0 |] |]

        let nextState = adjacencyMatrix * initialState

        Assert.Equal(Vector([| 0; 0; 12; 5; 1; 9 |]), nextState)

    [<Fact>]
    let ``Multiplying all state transition matrices gives a matrix that does all those transitions`` () =
        let initialState = Vector [| 6; 2; 1; 5; 3; 10 |]

        let adjacencyMatrix =
            Matrix [| [| 0; 0; 0; 0; 0; 0 |]
                      [| 0; 0; 0; 0; 0; 0 |]
                      [| 0; 1; 0; 0; 0; 1 |]
                      [| 0; 0; 0; 1; 0; 0 |]
                      [| 0; 0; 1; 0; 0; 0 |]
                      [| 1; 0; 0; 0; 1; 0 |] |]

        let applyTransition = Matrix.Act adjacencyMatrix

        let applyThreeTransitions =
            Matrix.Act(
                adjacencyMatrix
                * adjacencyMatrix
                * adjacencyMatrix
            )

        Assert.Equal(
            initialState
            |> applyTransition
            |> applyTransition
            |> applyTransition,
            initialState |> applyThreeTransitions
        )

    [<Fact>]
    let ``Transposing the adjacency matrix reverses the graph dynamics`` () =

        let initialState = Matrix [| [| 1 |]; [| 2 |]; [| 3 |] |]

        let orthogonalAdjacencyMatrix =
            Matrix [| [| 0; 1; 0 |]
                      [| 0; 0; 1 |]
                      [| 1; 0; 0 |] |]

        let transposeOfAdjacencyMatrix = Matrix.Transpose orthogonalAdjacencyMatrix

        let nextState = orthogonalAdjacencyMatrix * initialState
        let reversedState = transposeOfAdjacencyMatrix * nextState

        Assert.Equal(initialState, reversedState)

    [<Fact>]
    let ``Change of propablistic state presented by column vector can be described by adjacency matrix`` () =

        let initialState =
            Matrix [| [| 1.0 / 6.0 |]
                      [| 1.0 / 6.0 |]
                      [| 2.0 / 3.0 |] |]


        let adjacencyMatrix =
            Matrix [| [| 0; 1.0 / 6.0; 5.0 / 6.0 |]
                      [| 1.0 / 3.0; 1.0 / 2.0; 1.0 / 6.0 |]
                      [| 2.0 / 3.0; 1.0 / 3.0; 0.0 |] |]

        let nextState = adjacencyMatrix * initialState

        Assert.Equal(
            Matrix [| [| 21.0 / 36.0 |]
                      [| 9.0 / 36.0 |]
                      [| 6.0 / 36.0 |] |],
            nextState
        )

        Assert.Equal(
            (Matrix.Transpose initialState)
            * (Matrix.Transpose adjacencyMatrix),
            (Matrix.Transpose nextState)
        )
