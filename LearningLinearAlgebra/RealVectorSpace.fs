namespace Algebra

module RealVectorSpace =
    open RealNumbers

    type Vector =
        | Vector of float []

        static member Zero n : Vector = Vector(Array.create n 0)

        static member AsScalar(Vector vector) : float = Array.exactlyOne vector

        static member Add (Vector left) (Vector right) : Vector =

            (Array.map2 (fun leftElement rightElement -> leftElement + rightElement) left right)
            |> Vector

        static member Subtract (Vector left) (Vector right) : Vector =

            (Array.map2 (fun leftElement rightElement -> leftElement - rightElement) left right)
            |> Vector

        static member Multiply (scalar: float) (Vector vector) : Vector =

            vector
            |> Array.map (fun element -> scalar * element)
            |> Vector

        static member InnerProduct (Vector left) (Vector right) : float =

            (Array.map2 (fun leftElement rightElement -> leftElement * rightElement) left right)
            |> Array.sum

        static member Norm vector : float =
            sqrt (Vector.InnerProduct vector vector)

        static member Distance left right : float =
            (Vector.Subtract left right) |> Vector.Norm

        static member TensorProduct (Vector left) (Vector right) : Vector =
            let length = left.Length * right.Length

            Array.init length (fun index ->
                let leftIndex = index / right.Length
                let rightIndex = index % right.Length
                left[leftIndex] * right[rightIndex])
            |> Vector

        static member Inverse(vector: Vector) : Vector = Vector.Multiply -1 vector

        static member Round(Vector vector) : Vector =
            vector
            |> Array.map (fun element -> Round element)
            |> Vector

        static member inline (+)(left: Vector, right: Vector) = Vector.Add left right
        static member inline (-)(left: Vector, right: Vector) = Vector.Subtract left right
        static member inline (*)(scalar: float, vector: Vector) = Vector.Multiply scalar vector
        static member inline (*)(left: Vector, right: Vector) = Vector.InnerProduct left right
        static member inline (~-)(vector: Vector) = Vector.Inverse vector

    type Matrix =
        | Matrix of float [] []

        static member Fill m n elementValue : Matrix =
            Array.init m (fun i -> Array.init n (fun j -> elementValue i j))
            |> Matrix

        static member Zero m n : Matrix = Matrix.Fill m n (fun i j -> 0)

        static member Identity m : Matrix =
            Matrix.Fill m m (fun i j -> if i = j then 1.0 else 0.0)

        static member AsVector(Matrix matrix) : Vector =
            matrix
            |> Array.map (fun row -> Array.exactlyOne row)
            |> Vector

        static member AsScalar matrix : float =
            matrix |> Matrix.AsVector |> Vector.AsScalar

        static member M(matrix: float [] []) = matrix.Length

        static member N(matrix: float [] []) =
            if matrix.Length > 0 then
                matrix[0].Length
            else
                0

        static member Add (Matrix left) (Matrix right) : Matrix =

            let add leftRow rightRow =
                rightRow
                |> (leftRow
                    |> Array.map2 (fun leftElement rightElement -> leftElement + rightElement))

            Array.map2 (fun leftRow rightRow -> add leftRow rightRow) left right
            |> Matrix

        static member Subtract (Matrix left) (Matrix right) : Matrix =

            let subtract leftRow rightRow =
                rightRow
                |> (leftRow
                    |> Array.map2 (fun leftElement rightElement -> leftElement - rightElement))

            Array.map2 (fun leftRow rightRow -> subtract leftRow rightRow) left right
            |> Matrix

        static member Multiply (scalar: float) (Matrix matrix) : Matrix =

            matrix
            |> Array.map (fun row -> row |> Array.map (fun element -> scalar * element))
            |> Matrix

        static member Transpose(Matrix matrix) : Matrix =
            Matrix.Fill (Matrix.N matrix) (Matrix.M matrix) (fun i j -> matrix[j][i])

        static member Transpose(Vector vector) : Matrix =
            vector
            |> Array.map (fun element -> [| element |])
            |> Matrix

        static member Product (Matrix left) (Matrix right) : Matrix =

            left
            |> Array.mapi (fun i row ->
                Array.init (right[0].Length) (fun j ->
                    let column = right |> Array.map (fun row -> row[j])

                    column
                    |> (row |> Array.map2 (fun x y -> x * y))
                    |> Array.sum

                ))
            |> Seq.toArray
            |> Matrix

        static member Act (Matrix matrix) (Vector vector) : Vector =
            matrix
            |> Array.mapi (fun i row ->
                vector
                |> (row |> Array.map2 (fun x y -> x * y))
                |> Array.sum

            )
            |> Seq.toArray
            |> Vector

        static member TensorProduct (Matrix left) (Matrix right) : Matrix =

            let ColumnCount (matrix: float [] []) =
                match matrix.Length with
                | 0 -> 0
                | _ -> matrix[0].Length

            let rowCount = left.Length * right.Length
            let columnCount = (left |> ColumnCount) * (right |> ColumnCount)

            let m = right.Length
            let n = (right |> ColumnCount)

            Matrix.Fill rowCount columnCount (fun j k -> left[j / n][k / m] * right[j % n][k % m])

        static member Round(Matrix matrix) : Matrix =
            matrix
            |> Array.map (fun row -> row |> Array.map (fun element -> Round element))
            |> Matrix

        static member Inverse(matrix: Matrix) : Matrix = Matrix.Multiply -1 matrix

        static member inline (+)(left: Matrix, right: Matrix) = Matrix.Add left right
        static member inline (-)(left: Matrix, right: Matrix) = Matrix.Subtract left right
        static member inline (*)(scalar: float, matrix: Matrix) = Matrix.Multiply scalar matrix
        static member inline (*)(left: Matrix, right: Matrix) = Matrix.Product left right
        static member inline (*)(matrix: Matrix, vector: Vector) = Matrix.Act matrix vector
        static member inline (~-)(matrix: Matrix) = Matrix.Inverse matrix
