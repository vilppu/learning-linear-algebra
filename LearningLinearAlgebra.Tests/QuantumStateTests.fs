namespace QuantumComputing

open Xunit

module QuantumStateTests =
    open Algebra.ComplexNumbers.CartesianPresentation
    open Algebra.ComplexVectorSpace
    open QuantumState

    [<Fact>]
    let ``Norm square of the amplitude at x divided by norm square of ket tells the propablity of finding the particle at state x``
        ()
        =
        let ket =
            Vector [| Complex(-3.0, -1.0)
                      Complex(0.0, -2.0)
                      Complex(0.0, 1.0)
                      Complex(2.0, 0.0) |]

        let probability = Probability ket 2

        Assert.Equal(0.052631578947368411, probability)

    [<Fact>]
    let ``Another example of propability of specific end state`` () =
        let precision = 10

        let ketsForSpin =
            Vector [| Complex(3.0, -4.0)
                      Complex(7.0, 2.0) |]

        let spinUp = Probability ketsForSpin 0
        let spinDown = Probability ketsForSpin 1

        Assert.Equal(25.0 / 78.0, spinUp, precision)
        Assert.Equal(53.0 / 78.0, spinDown, precision)

    [<Fact>]
    let ``Bra is the adjoint of ket`` () =
        let ket =
            Ket [| Complex(1.0, 1.0)
                   Complex(2.0, 2.0)
                   Complex(3.0, 3.0) |]

        let bra = Bra ket

        Assert.Equal(
            Matrix [| [| Complex(1.0, -1.0)
                         Complex(2.0, -2.0)
                         Complex(3.0, -3.0) |] |],
            bra
        )

    [<Fact>]
    let ``Another example of bra`` () =
        let ket =
            Ket [| Complex(3.0, 0.0)
                   Complex(1.0, -2.0) |]

        let bra = Bra ket

        Assert.Equal(
            Matrix [| [| Complex(3.0, 0.0)
                         Complex(1.0, 2.0) |] |],
            bra
        )

    [<Fact>]
    let ``Amplitude of transition from state to another is the braket start and end states`` () =
        let startState =
            Ket [| Complex((sqrt 2.0) / 2.0, 0)
                   Complex(0, (sqrt 2.0) / 2.0) |]

        let endState =
            Ket [| Complex(0, (sqrt 2.0) / 2.0)
                   Complex((sqrt 2.0) / -2.0, 0) |]

        let braket = Braket startState endState |> Complex.Round

        Assert.Equal(Complex(0, -1.0), braket)
