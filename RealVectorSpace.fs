namespace Algebra

module RealVectorSpace =

    type Vector =
        | Vector of float []

        static member Zero n : Vector = Vector(Array.create n 0)

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

        static member Inverse(vector: Vector) : Vector = Vector.Multiply -1 vector

        static member inline (+)(left: Vector, right: Vector) = Vector.Add left right
        static member inline (-)(left: Vector, right: Vector) = Vector.Subtract left right
        static member inline (*)(scalar: float, vector: Vector) = Vector.Multiply scalar vector
        static member inline (~-)(vector: Vector) = Vector.Inverse vector

    type Matrix =
        | Matrix of float [] []

        static member Zero m n : Matrix =
            Matrix(Array.create m (Array.create n 0))

        static member Identity m : Matrix =
            ({ 0 .. (m - 1) })
            |> Seq.map (fun i -> Array.init m (fun j -> if i = j then 1.0 else 0.0))
            |> Seq.toArray
            |> Matrix

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
            matrix
            |> Array.mapi (fun i row -> row |> Array.mapi (fun j _ -> matrix[j][i]))
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

        static member ToVector(Matrix matrix) : Vector =
            matrix |> Array.map (fun row -> row[0]) |> Vector

        static member FromVector(Vector vector) : Matrix =
            vector
            |> Array.map (fun element -> [| element |])
            |> Matrix

        static member Inverse(matrix: Matrix) : Matrix = Matrix.Multiply -1 matrix

        static member inline (+)(left: Matrix, right: Matrix) = Matrix.Add left right
        static member inline (-)(left: Matrix, right: Matrix) = Matrix.Subtract left right
        static member inline (*)(scalar: float, matrix: Matrix) = Matrix.Multiply scalar matrix
        static member inline (*)(left: Matrix, right: Matrix) = Matrix.Product left right
        static member inline (*)(matrix: Matrix, vector: Vector) = Matrix.Act matrix vector
        static member inline (~-)(matrix: Matrix) = Matrix.Inverse matrix
