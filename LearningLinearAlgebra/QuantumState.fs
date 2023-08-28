namespace QuantumComputing

module QuantumState =

    open RealNumbers
    open Algebra.ComplexNumbers.CartesianPresentation
    open Algebra.ComplexVectorSpace

    let Probability (ket: Vector) index : float =

        Square(Complex.Modulus ket[index])
        / Square(Vector.Norm ket)

    let Ket amplitudes : Vector = Vector amplitudes

    let Observable values : Matrix = Matrix values

    let Normalized (ket: Vector) : Vector =
        let foo = (1.0 / (Vector.Norm ket))
        foo * ket

    let Bra (ket: Vector) : Matrix = ket |> Matrix.Adjoint

    let BraKet (leftKet: Vector) (rightKet: Vector) : Complex =
        let ket = leftKet
        let bra = Bra rightKet
        let braket = Vector.AsScalar(bra * ket)

        braket

    let TransitionAmplitude (startState: Vector) (endState: Vector) : Complex = BraKet startState endState

    let Mean (startState: Vector) (observable: Matrix) : Complex =

        let endState = observable * startState

        BraKet endState startState |> Complex.Round

    let Variance (startState: Vector) (observable: Matrix) : Complex =

        let mean = Mean startState observable
        let delta = observable - (mean * Matrix.Identity 2)
        let deltaSquared = delta * delta
        let bra = Bra startState

        bra * deltaSquared * startState |> Vector.AsScalar
