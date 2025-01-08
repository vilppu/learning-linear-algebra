namespace Computation.Matrices

module Complex =

    open Computation.Numbers
    open Computation.Matrices.Complex

    type ComplexNumber = ComplexNumber<float>
    type RowVector = RowVector<float>
    type ColumnVector = ColumnVector<float>
    type SquareMatrix = SquareMatrix<float>

    module RowVector =

        let Zero n = RowVector<'R>.Zero(n)

        let Add (left: RowVector<'R>) (right: RowVector<'R>) = left.Add(right)

        let Subtract (left: RowVector<'R>) (right: RowVector<'R>) = left.Subtract(right)

        let Multiply (scalar: ComplexNumber<'R>) (vector: RowVector<'R>) = vector.Multiply(scalar)

        let MultiplyByReal (scalar: 'R) (vector: RowVector<'R>) = vector.Multiply(scalar)

        let MultiplyVectors (left: RowVector<'R>) (right: ColumnVector<'R>) = left.Multiply(right)

        let InnerProduct (left: RowVector<'R>) (right: RowVector<'R>) = left.InnerProduct(right)

        let Conjucate (vector: RowVector<'R>) = vector.Conjucate()

        let Adjoint (vector: RowVector<'R>) = vector.Adjoint()

        let Transpose (vector: RowVector<'R>) = vector.Transpose()

        let Norm (vector: RowVector<'R>) = vector.Norm()

        let Normalized (vector: RowVector<'R>) = vector.Normalized()

        let Distance (left: RowVector<'R>) (right: RowVector<'R>) = left.Distance(right)

        let TensorProduct (left: RowVector<'R>) (right: RowVector<'R>) = left.TensorProduct(right)

        let AdditiveInverse (vector: RowVector<'R>) = vector.AdditiveInverse()

        let Round (vector: RowVector<'R>) = vector.Round()

    module ColumnVector =

        let Zero n = ColumnVector<'R>.Zero(n)

        let Add (left: ColumnVector<'R>) (right: ColumnVector<'R>) = left.Add(right)

        let Subtract (left: ColumnVector<'R>) (right: ColumnVector<'R>) = left.Subtract(right)

        let Multiply (scalar: ComplexNumber<'R>) (vector: ColumnVector<'R>) = vector.Multiply(scalar)

        let MultiplyByReal (scalar: 'R) (vector: ColumnVector<'R>) = vector.Multiply(scalar)

        let InnerProduct (left: ColumnVector<'R>) (right: ColumnVector<'R>) = left.InnerProduct(right)

        let Conjucate (vector: ColumnVector<'R>) = vector.Conjucate()

        let Adjoint (vector: ColumnVector<'R>) = vector.Adjoint()

        let Transpose (vector: ColumnVector<'R>) tor = vector.Transpose()

        let Norm (vector: ColumnVector<'R>) = vector.Norm()

        let Normalized (vector: ColumnVector<'R>) = vector.Normalized()

        let Distance (left: ColumnVector<'R>) (right: ColumnVector<'R>) = left.Distance(right)

        let TensorProduct (left: ColumnVector<'R>) (right: ColumnVector<'R>) = left.TensorProduct(right)

        let AdditiveInverse (vector: ColumnVector<'R>) = vector.AdditiveInverse()

        let Round (vector: ColumnVector<'R>) = vector.Round()

    module SquareMatrix =

        let Zero m = SquareMatrix<'R>.Zero(m)

        let Identity m = SquareMatrix<'R>.Identity(m)

        let M (matrix: SquareMatrix<'R>) = matrix.M()

        let N (matrix: SquareMatrix<'R>) = matrix.N()

        let Add (left: SquareMatrix<'R>) (right: SquareMatrix<'R>) = left.Add(right)

        let Subtract (left: SquareMatrix<'R>) (right: SquareMatrix<'R>) = left.Subtract(right)

        let Multiply (scalar: ComplexNumber<'R>) (matrix: SquareMatrix<'R>) = matrix.Multiply(scalar)

        let Transpose (matrix: SquareMatrix<'R>) = matrix.Transpose()

        let Conjucate (matrix: SquareMatrix<'R>) = matrix.Conjucate()

        let Adjoint (matrix: SquareMatrix<'R>) = matrix.Adjoint()

        let Product (left: SquareMatrix<'R>) (right: SquareMatrix<'R>) = left.Multiply(right)

        let Act (left: SquareMatrix<'R>) (right: ColumnVector<'R>) = left.Act(right)

        let ActToLeft (left: RowVector<'R>) (right: SquareMatrix<'R>) = left.Act(right)

        let Commutator (left: SquareMatrix<'R>) (right: SquareMatrix<'R>) = left.Commutator(right)

        let Round (matrix: SquareMatrix<'R>) = matrix.Round()

        let IsHermitian (matrix: SquareMatrix<'R>) = matrix.IsHermitian()

        let IsUnitary (matrix: SquareMatrix<'R>) = matrix.IsUnitary()

        let TensorProduct (left: SquareMatrix<'R>) (right: SquareMatrix<'R>) = left.TensorProduct(right)

        let AdditiveInverse (matrix: SquareMatrix<'R>) = matrix.AdditiveInverse()

    let Unwrap<'R when 'R :> System.Numerics.IFloatingPointIeee754<'R>> complex : ComplexNumber<'R> = complex

    let CreateColumnVector (components: ComplexNumber<'R> array) =
        components |> Array.map Unwrap |> ColumnVector<'R>.V

    let CreateRowVector (components: ComplexNumber<'R> array) =
        components |> Array.map Unwrap |> RowVector<'R>.U

    let CreateMatrix (components: ComplexNumber<'R> array array) =
        components
        |> Array.map (fun row -> row |> Array.map (fun element -> Unwrap element))
        |> array2D
        |> SquareMatrix<'R>.M

    let V (components: ComplexNumber<float> array) = CreateColumnVector components

    let U (components: ComplexNumber<float> array) = CreateRowVector components

    let M (components: ComplexNumber<float> array array) = CreateMatrix components
