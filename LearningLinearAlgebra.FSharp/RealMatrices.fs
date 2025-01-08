namespace Computation.Matrices

module Real =

    open Computation.Matrices.Real

    type RowVector = RowVector<float>
    type ColumnVector = ColumnVector<float>
    type SquareMatrix = SquareMatrix<float>

    module RowVector =

        let Zero n = RowVector<'R>.Zero(n)

        let Add (left: RowVector<'R>) (right: RowVector<'R>) = left.Add(right)

        let Subtract (left: RowVector<'R>) (right: RowVector<'R>) = left.Subtract(right)

        let Multiply (scalar: 'R) (vector: RowVector<'R>) = vector.Multiply(scalar)

        let MultiplyByReal (scalar: 'R) (vector: RowVector<'R>) = vector.Multiply(scalar)

        let MultiplyVectors (left: RowVector<'R>) (right: ColumnVector<'R>) = left.Multiply(right)

        let Transpose (vector: RowVector<'R>) = vector.Transpose()

        let InnerProduct (left: RowVector<'R>) (right: RowVector<'R>) = left.InnerProduct(right)

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

        let Multiply (scalar: 'R) (vector: ColumnVector<'R>) = vector.Multiply(scalar)

        let MultiplyByReal (scalar: 'R) (vector: ColumnVector<'R>) = vector.Multiply(scalar)

        let Transpose (vector: ColumnVector<'R>) = vector.Transpose()

        let InnerProduct (left: ColumnVector<'R>) (right: ColumnVector<'R>) = left.InnerProduct(right)

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

        let Multiply (scalar: 'R) (matrix: SquareMatrix<'R>) = matrix.Multiply(scalar)

        let Transpose (matrix: SquareMatrix<'R>) = matrix.Transpose()

        let Product (left: SquareMatrix<'R>) (right: SquareMatrix<'R>) = left.Multiply(right)

        let Act (left: SquareMatrix<'R>) (right: ColumnVector<'R>) = left.Act(right)

        let Round (matrix: SquareMatrix<'R>) = matrix.Round()

        let TensorProduct (left: SquareMatrix<'R>) (right: SquareMatrix<'R>) = left.TensorProduct(right)

        let AdditiveInverse (matrix: SquareMatrix<'R>) = matrix.AdditiveInverse()


    let CreateColumnVector (components: 'R array) = components |> ColumnVector<'R>.V

    let CreateRowVector (components: 'R array) = components |> RowVector<'R>.U

    let CreateMatrix (components: 'R array array) =
        components |> array2D |> SquareMatrix<'R>.M

    let V components = CreateColumnVector components

    let U components = CreateRowVector components

    let M components = CreateMatrix components
