namespace LearningLinearAlgebra.Matrices

module Complex =

    open LearningLinearAlgebra.Numbers
    open LearningLinearAlgebra.Matrices.Complex

    module RowVector =

        let Zero n = RowVector<'R>.Zero(n)

        let Add left right = RowVector<'R>.Add(left, right)

        let Subtract left right = RowVector<'R>.Subtract(left, right)

        let Multiply (scalar: ComplexNumber<'R>) (vector: RowVector<'R>) = RowVector<'R>.Multiply(scalar, vector)

        let MultiplyByReal (scalar: 'R) (vector: RowVector<'R>) = RowVector<'R>.Multiply(scalar, vector)

        let MultiplyVectors (left: RowVector<'R>) right = RowVector<'R>.Multiply(left, right)

        let InnerProduct left right = RowVector<'R>.InnerProduct(left, right)

        let Conjucate vector = RowVector<'R>.Conjucate(vector)

        let Adjoint vector = RowVector<'R>.Adjoint(vector)

        let Transpose vector = RowVector<'R>.Transpose(vector)

        let Norm vector = RowVector<'R>.Norm(vector)

        let Normalized vector = RowVector<'R>.Normalized(vector)

        let Distance left right = RowVector<'R>.Distance(left, right)

        let TensorProduct left right =
            RowVector<'R>.TensorProduct(left, right)

        let AdditiveInverse vector = RowVector<'R>.AdditiveInverse(vector)

        let Round vector = RowVector<'R>.Round(vector)

    module ColumnVector =

        let Entries (vector: ColumnVector<'R>) = vector.Entries

        let Zero n = ColumnVector<'R>.Zero(n)

        let Add left right = ColumnVector<'R>.Add(left, right)

        let Subtract left right = ColumnVector<'R>.Subtract(left, right)

        let Multiply (scalar: ComplexNumber<'R>) vector =
            ColumnVector<'R>.Multiply(scalar, vector)

        let MultiplyByReal (scalar: 'R) vector =
            ColumnVector<'R>.Multiply(scalar, vector)

        let InnerProduct left right =
            ColumnVector<'R>.InnerProduct(left, right)

        let Conjucate vector = ColumnVector<'R>.Conjucate(vector)

        let Adjoint vector = ColumnVector<'R>.Adjoint(vector)

        let Transpose vector = ColumnVector<'R>.Transpose(vector)

        let Norm vector = ColumnVector<'R>.Norm(vector)

        let Normalized vector = ColumnVector<'R>.Normalized(vector)

        let Distance left right = ColumnVector<'R>.Distance(left, right)

        let TensorProduct left right =
            ColumnVector<'R>.TensorProduct(left, right)

        let AdditiveInverse vector =
            ColumnVector<'R>.AdditiveInverse(vector)

        let Round vector = ColumnVector<'R>.Round(vector)

    module SquareMatrix =

        let Zero m = SquareMatrix<'R>.Zero(m)

        let Identity m = SquareMatrix<'R>.Identity(m)

        let M (matrix: SquareMatrix<'R>) = SquareMatrix<'R>.M(matrix)

        let N matrix = SquareMatrix<'R>.N(matrix)

        let Add left right = SquareMatrix<'R>.Add(left, right)

        let Subtract left right = SquareMatrix<'R>.Subtract(left, right)

        let Multiply (scalar: ComplexNumber<'R>) matrix =
            SquareMatrix<'R>.Multiply(scalar, matrix)

        let Transpose (matrix) = SquareMatrix<'R>.Transpose(matrix)

        let Conjucate (matrix) = SquareMatrix<'R>.Conjucate(matrix)

        let Adjoint matrix = SquareMatrix<'R>.Adjoint(matrix)

        let Product (left: SquareMatrix<'R>) right = SquareMatrix<'R>.Multiply(left, right)

        let Act (left: SquareMatrix<'R>) (right: ColumnVector<'R>) = SquareMatrix<'R>.Act(left, right)

        let ActToLeft (left: RowVector<'R>) (right: SquareMatrix<'R>) = SquareMatrix<'R>.Act(left, right)

        let Commutator left right =
            SquareMatrix<'R>.Commutator(left, right)

        let Round matrix = SquareMatrix<'R>.Round(matrix)

        let IsHermitian matrix = SquareMatrix<'R>.IsHermitian(matrix)

        let IsUnitary matrix = SquareMatrix<'R>.IsUnitary(matrix)

        let TensorProduct left right =
            SquareMatrix<'R>.TensorProduct(left, right)

        let AdditiveInverse matrix =
            SquareMatrix<'R>.AdditiveInverse(matrix)

    let Unwrap<'R when 'R :> System.Numerics.IFloatingPointIeee754<'R>> complex : ComplexNumber<'R> = complex

    let CreateColumnVector entries =
        entries |> Array.map Unwrap |> ColumnVector<float>

    let CreateRowVector entries =
        entries |> Array.map Unwrap |> RowVector<float>

    let CreateMatrix entries =
        entries
        |> Array.map (fun row -> row |> Array.map (fun element -> Unwrap element))
        |> array2D
        |> SquareMatrix<float>

    let V (entries: ComplexNumber<float> array) = CreateColumnVector entries

    let U (entries: ComplexNumber<float> array) = CreateRowVector entries

    let M (entries: ComplexNumber<float> array array) = CreateMatrix entries
