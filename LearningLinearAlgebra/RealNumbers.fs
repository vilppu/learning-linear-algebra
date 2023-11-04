namespace Algebra

module RealNumbers =

    let Pi = 3.141592653589
    let Round (value: float) : float = System.Math.Round(value, 10)
    let RoundToTwoDecimals (value: float) : float = System.Math.Round(value, 2)
    let Square (value: float) : float = value * value

    type Vector<'R> =
        | V of 'R[]

        static member inline Zero n =
            V(Array.create n LanguagePrimitives.GenericZero)

        static member inline AsScalar(V vector) = Array.exactlyOne vector

        static member inline Add (V left) (V right) =

            (Array.map2 (fun leftElement rightElement -> leftElement + rightElement) left right)
            |> V

        static member inline Subtract (V left) (V right) =

            (Array.map2 (fun leftElement rightElement -> leftElement - rightElement) left right)
            |> V

        static member inline Multiply (scalar) (V vector) =

            vector |> Array.map (fun element -> scalar * element) |> V

        static member inline InnerProduct (V left) (V right) =

            (Array.map2 (fun leftElement rightElement -> leftElement * rightElement) left right)
            |> Array.sum

        static member inline Norm vector =
            sqrt (Vector<float>.InnerProduct vector vector)

        static member inline Distance left right =
            (Vector<_>.Subtract left right) |> Vector<_>.Norm

        static member inline TensorProduct (V left) (V right) =
            let length = left.Length * right.Length

            Array.init length (fun index ->
                let leftIndex = index / right.Length
                let rightIndex = index % right.Length
                left[leftIndex] * right[rightIndex])
            |> V

        static member inline Inverse(vector) =
            Vector<_>.Multiply -LanguagePrimitives.GenericOne vector

        static member inline Round(V vector) =
            vector |> Array.map (fun element -> Round element) |> V

        static member inline (+)(left, right) = Vector<_>.Add left right
        static member inline (-)(left, right) = Vector<_>.Subtract left right
        static member inline (*)(scalar, vector) = Vector<_>.Multiply scalar vector
        static member inline (*)(left, right) = Vector<_>.InnerProduct left right
        static member inline (~-)(vector) = Vector<_>.Inverse vector

    type Matrix<'R> =
        | M of 'R[][]

        static member inline Fill m n elementValue =
            Array.init m (fun i -> Array.init n (fun j -> elementValue i j)) |> M

        static member inline Zero m n =
            Matrix<_>.Fill m n (fun i j -> LanguagePrimitives.GenericZero)

        static member inline Identity m =
            Matrix<_>.Fill m m (fun i j ->
                if i = j then
                    LanguagePrimitives.GenericOne
                else
                    LanguagePrimitives.GenericZero)

        static member inline AsVector(M matrix) =
            matrix |> Array.map (fun row -> Array.exactlyOne row) |> V

        static member inline AsScalar matrix =
            matrix |> Matrix<_>.AsVector |> Vector<_>.AsScalar

        static member inline Rows matrix = Array.length (matrix)

        static member inline Columns matrix =
            if Array.length (matrix) > 0 then
                Array.length (Array.head matrix)
            else
                0

        static member inline Add (M left) (M right) =

            let add leftRow rightRow =
                rightRow
                |> (leftRow
                    |> Array.map2 (fun leftElement rightElement -> leftElement + rightElement))

            Array.map2 (fun leftRow rightRow -> add leftRow rightRow) left right |> M

        static member inline Subtract (M left) (M right) =

            let subtract leftRow rightRow =
                rightRow
                |> (leftRow
                    |> Array.map2 (fun leftElement rightElement -> leftElement - rightElement))

            Array.map2 (fun leftRow rightRow -> subtract leftRow rightRow) left right |> M

        static member inline Multiply (scalar) (M matrix) =

            matrix
            |> Array.map (fun row -> row |> Array.map (fun element -> scalar * element))
            |> M

        static member inline Transpose(M matrix) =
            Matrix<'R>.Fill (Matrix<'R>.Columns matrix) (Matrix<'R>.Rows matrix) (fun i j -> matrix[j][i])

        static member inline Transpose(V vector) =
            vector |> Array.map (fun element -> [| element |]) |> M

        static member inline Product (M left) (M right) =

            left
            |> Array.mapi (fun i row ->
                Array.init (right[0].Length) (fun j ->
                    let column = right |> Array.map (fun row -> row[j])

                    column |> (row |> Array.map2 (fun x y -> x * y)) |> Array.sum

                ))
            |> Seq.toArray
            |> M

        static member inline Act (M matrix) (V vector) =
            matrix
            |> Array.mapi (fun i row -> vector |> (row |> Array.map2 (fun x y -> x * y)) |> Array.sum

            )
            |> Seq.toArray
            |> V

        static member inline TensorProduct (M left) (M right) =

            let inline ColumnCount matrix =
                match Array.length (matrix) with
                | 0 -> 0
                | _ -> Array.length (matrix[0])

            let rowCount = Array.length (left) * Array.length (right)
            let columnCount = (left |> ColumnCount) * (right |> ColumnCount)

            let m = right.Length
            let n = (right |> ColumnCount)

            Matrix<_>.Fill rowCount columnCount (fun j k -> left[j / n][k / m] * right[j % n][k % m])

        static member inline Round(M matrix) =
            matrix
            |> Array.map (fun row -> row |> Array.map (fun element -> Round element))
            |> M

        static member inline Inverse(matrix) =
            Matrix<_>.Multiply -LanguagePrimitives.GenericOne matrix

        static member inline (+)(left, right) = Matrix<_>.Add left right
        static member inline (-)(left, right) = Matrix<_>.Subtract left right
        static member inline (*)(scalar, matrix) = Matrix<_>.Multiply scalar matrix
        static member inline (*)(left, right) = Matrix<_>.Product left right
        static member inline (*)(matrix, vector) = Matrix<_>.Act matrix vector
        static member inline (~-)(matrix) = Matrix<_>.Inverse matrix
