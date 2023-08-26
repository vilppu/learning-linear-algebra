namespace QuantumComputing

module QuantumState =

    open RealNumbers
    open Algebra.ComplexNumbers.CartesianPresentation
    open Algebra.ComplexVectorSpace

    let Probability (ket: Vector) index : float =

        Square(Complex.Modulus ket[index])
        / Square(Vector.Norm ket)

    let Ket amplitudes : Vector = Vector amplitudes

    let Normalized (ket: Vector) : Vector =
        let foo = (1.0 / (Vector.Norm ket))
        foo * ket

    let Bra (ket: Vector) : Matrix = ket |> Matrix.Adjoint

    let Braket (leftKet: Vector) (rightKet: Vector) : Complex =
        let ket = leftKet
        let bra = Bra rightKet
        let braket = Vector.AsScalar(bra * ket)

        braket
