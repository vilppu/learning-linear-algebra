namespace LearningLinearAlgebra.LinearAlgebra

module ComplexVectorSpace =

    open LearningLinearAlgebra.Numbers
    open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace

    module Ket =

        let Zero n = Ket<'R>.Zero(n)

        let Add left right = Ket<'R>.Add(left, right)

        let Subtract left right = Ket<'R>.Subtract(left, right)

        let AdditiveInverse ket = Ket<'R>.AdditiveInverse(ket)

        let Multiply (scalar: ComplexNumber<'R>) ket = Ket<'R>.Multiply(scalar, ket)

        let MultiplyByReal (scalar: 'R) ket = Ket<'R>.Multiply(scalar, ket)

        let MultiplyByBra (bra: Bra<'R>) ket = Ket<'R>.Multiply(bra, ket)

        let InnerProduct left right = Ket<'R>.InnerProduct(left, right)

        let TensorProduct left right = Ket<'R>.TensorProduct(left, right)

        let Bra ket = Ket<'R>.Bra(ket)

        let Norm ket = Ket<'R>.Norm(ket)

        let Distance left right = Ket<'R>.Distance(left, right)

        let Normalized ket = Ket<'R>.Normalized(ket)

        let Conjucate ket = Ket<'R>.Conjucate(ket)

    module Bra =

        let Zero n = Bra<'R>.Zero(n)

        let Add left right = Bra<'R>.Add(left, right)

        let Subtract left right = Bra<'R>.Subtract(left, right)

        let AdditiveInverse bra = Bra<'R>.AdditiveInverse(bra)

        let Multiply (scalar: ComplexNumber<'R>) bra = Bra<'R>.Multiply(scalar, bra)

        let MultiplyByReal (scalar: 'R) bra = Bra<'R>.Multiply(scalar, bra)

        let InnerProduct left right = Bra<'R>.InnerProduct(left, right)

        let TensorProduct left right = Bra<'R>.TensorProduct(left, right)

        let Ket bra = Bra<'R>.Ket(bra)

        let Norm bra = Bra<'R>.Norm(bra)

        let Distance left right = Bra<'R>.Distance(left, right)

        let Normalized bra = Bra<'R>.Normalized(bra)

        let Conjucate bra = Bra<'R>.Conjucate(bra)

    module Operator =

        let Zero n = Operator<'R>.Zero(n)

        let Identity n = Operator<'R>.Identity(n)

        let Multiply (scalar: ComplexNumber<'R>) (operator: Operator<'R>) = Operator<'R>.Multiply(scalar, operator)

        let MultiplyOperators (left: Operator<'R>) (right: Operator<'R>) = Operator<'R>.Multiply(left, right)

        let TensorProduct left right = Operator<'R>.TensorProduct(left, right)

        let Commutator left right = Operator<'R>.Commutator(left, right)

        let Act (operator: Operator<'R>) ket = Operator<'R>.Act(operator, ket)

        let ActToLeft bra (operator: Operator<'R>) = Operator<'R>.Act(bra, operator)

        let Conjucate operator = Operator<'R>.Conjucate(operator)

        let Adjoint operator = Operator<'R>.Adjoint(operator)

        let Round operator = Operator<'R>.Round(operator)

        let IsIdentity operator = Operator<'R>.IsIdentity(operator)

        let IsUnitary operator = Operator<'R>.IsUnitary(operator)

        let IsHermitian operator = Operator<'R>.IsHermitian(operator)

    let private Unwrap<'R when 'R :> System.Numerics.IFloatingPointIeee754<'R>> complex : ComplexNumber<'R> = complex

    let private CreateOperator (entries: ComplexNumber<'R> array array) =
        entries
        |> Array.map (fun row -> row |> Array.map (fun element -> Unwrap element))
        |> array2D
        |> Operator<'R>.M

    let V (entries: ComplexNumber<'R> array) = Ket<'R>.V entries
    let U (entries: ComplexNumber<'R> array) = Bra<'R>.U entries
    let M (components: ComplexNumber<'R> array array) = components |> CreateOperator
