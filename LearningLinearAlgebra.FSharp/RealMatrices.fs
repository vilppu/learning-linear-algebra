namespace LearningLinearAlgebra.Matrices

module Real =

    open LearningLinearAlgebra.Matrices.Real

    type RowVector = RowVector<float>
    type ColumnVector = ColumnVector<float>
    type SquareMatrix = SquareMatrix<float>

    module RowVector =

        let Zero n = RowVector<'R>.Zero(n)

        let Add left right = RowVector<'R>.Add(left, right)

        let Subtract left right = RowVector<'R>.Subtract(left, right)

        let Multiply (scalar: 'R) vector = RowVector<'R>.Multiply(scalar, vector)

        let MultiplyByReal (scalar: 'R) vector = RowVector<'R>.Multiply(scalar, vector)

        let MultiplyVectors (left: RowVector<'R>) right = RowVector<'R>.Multiply(left, right)

        let Transpose vector = RowVector<'R>.Transpose(vector)

        let InnerProduct left right = RowVector<'R>.InnerProduct(left, right)

        let Norm vector = RowVector<'R>.Norm(vector)

        let Normalized vector = RowVector<'R>.Normalized(vector)

        let Distance left right = RowVector<'R>.Distance(left, right)

        let TensorProduct left right =
            RowVector<'R>.TensorProduct(left, right)

        let AdditiveInverse vector = RowVector<'R>.AdditiveInverse(vector)

        let Round vector = RowVector<'R>.Round(vector)

    module ColumnVector =

        let Zero n = ColumnVector<'R>.Zero(n)

        let Add left right = ColumnVector<'R>.Add(left, right)

        let Subtract left right = ColumnVector<'R>.Subtract(left, right)

        let Multiply (scalar: 'R) vector =
            ColumnVector<'R>.Multiply(scalar, vector)

        let MultiplyByReal (scalar: 'R) vector =
            ColumnVector<'R>.Multiply(scalar, vector)

        let Transpose vector = ColumnVector<'R>.Transpose(vector)

        let InnerProduct left right =
            ColumnVector<'R>.InnerProduct(left, right)

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

        let Multiply (scalar: 'R) matrix =
            SquareMatrix<'R>.Multiply(scalar, matrix)

        let Transpose (matrix: SquareMatrix<'R>) = SquareMatrix<'R>.Transpose(matrix)

        let Product (left: SquareMatrix<'R>) (right: SquareMatrix<'R>) = SquareMatrix<'R>.Multiply(left, right)

        let Act left right = SquareMatrix<'R>.Act(left, right)

        let Commutator left right =
            SquareMatrix<'R>.Commutator(left, right)

        let Round matrix = SquareMatrix<'R>.Round(matrix)

        let TensorProduct left right =
            SquareMatrix<'R>.TensorProduct(left, right)

        let AdditiveInverse matrix =
            SquareMatrix<'R>.AdditiveInverse(matrix)


    let CreateColumnVector entries = entries |> ColumnVector<'R>

    let CreateRowVector entries = entries |> RowVector<'R>

    let CreateMatrix entries = entries |> array2D |> SquareMatrix<'R>

    let V entries = CreateColumnVector entries

    let U entries = CreateRowVector entries

    let M entries = CreateMatrix entries
