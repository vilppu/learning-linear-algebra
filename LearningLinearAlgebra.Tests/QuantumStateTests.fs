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
    let ``Amplitude of transition from state to another is the bra-ket of start and end states`` () =
        let startState =
            Ket [| Complex((sqrt 2.0) / 2.0, 0)
                   Complex(0, (sqrt 2.0) / 2.0) |]

        let endState =
            Ket [| Complex(0, (sqrt 2.0) / 2.0)
                   Complex((sqrt 2.0) / -2.0, 0) |]

        let braket =
            TransitionAmplitude startState endState
            |> Complex.Round

        Assert.Equal(Complex(0, -1.0), braket)

    [<Fact>]
    let ``The mean expected value is bra-ket of state and observable applied to state`` () =
        let startState =
            Ket [| Complex((sqrt 2.0) / 2.0, 0)
                   Complex(0, (sqrt 2.0) / 2.0) |]

        let observable =
            Observable [| [| Complex(1, 0); Complex(0, -1) |]
                          [| Complex(0, 1); Complex(2, 0) |] |]

        let expected = Mean startState observable |> Complex.Round

        Assert.Equal(Complex(2.5, 0), expected)

    [<Fact>]
    let ``Another example of calulating expected value after applying observable`` () =
        let startState =
            Ket [| Complex((sqrt 2.0) / 2.0, 0)
                   Complex(0, (sqrt 2.0) / 2.0) |]

        let observable =
            Observable [| [| Complex(1, 0); Complex(0, -1) |]
                          [| Complex(0, 1); Complex(2, 0) |] |]

        let expected = Mean startState observable |> Complex.Round

        Assert.Equal(Complex(2.5, 0), expected)

    [<Fact>]
    let ``The variance of observable Ω at state vector is the expectation value of mean of Ω subtracted from result of Ω``
        ()
        =
        let startState =
            Ket [| Complex((sqrt 2.0) / 2.0, 0)
                   Complex(0, (sqrt 2.0) / 2.0) |]

        let observable =
            Observable [| [| Complex(1, 0); Complex(0, -1) |]
                          [| Complex(0, 1); Complex(2, 0) |] |]

        let variance = Variance startState observable |> Complex.Round

        Assert.Equal(Complex(0.25, 0), variance)

    [<Fact>]
    let ``If result of measuring is eigen value of observable then end state is eigen vector of observable`` () =

        let observable =
            Observable [| [| Complex(-1, 0); Complex(0, -1) |]
                          [| Complex(0, 1); Complex(1, 0) |] |]

        let endState =
            Vector [| Complex(0, -0.924)
                      Complex(-0.383, 0) |]

        let measuredValue = -sqrt 2.0

        let AssertEigenVector (matrix: Matrix) (eigenValue: float) (eigenVector: Vector) =

            Assert.Equal(
                matrix * eigenVector |> Vector.RoundToTwoDecimals,
                eigenValue * eigenVector
                |> Vector.RoundToTwoDecimals
            )

        AssertEigenVector observable measuredValue endState

    [<Fact>]
    let ``Probability to transition to specific eigen vector is the square of the inner product of the states`` () =

        let startState =
            (sqrt 2.0 / 2.0)
            * Vector [| Complex.One; Complex.One |]

        let endState =
            Vector [| Complex(0, -0.924)
                      Complex(-0.383, 0) |]

        let transitionAmplitude = BraKet startState endState
        let transitionProbability = RealNumbers.Square(Complex.Modulus transitionAmplitude)

        Assert.Equal(0.50023250000000019, transitionProbability)
