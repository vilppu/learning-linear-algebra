namespace Algebra

module ComplexVectorSpace =

    open Algebra.ComplexNumbers.CartesianPresentation

    type Vector =
        | Vector of Complex []

        static member Zero n : Vector = Vector(Array.create n Complex.Zero)

        static member Add (Vector left) (Vector right) : Vector =

            (Array.map2 (fun leftElement rightElement -> leftElement + rightElement) left right)
            |> Vector

        static member Subtract (Vector left) (Vector right) : Vector =

            (Array.map2 (fun leftElement rightElement -> leftElement - rightElement) left right)
            |> Vector

        static member Multiply (scalar: Complex) (Vector vector) : Vector =

            vector
            |> Array.map (fun element -> scalar * element)
            |> Vector

        static member InnerProduct (Vector left) (Vector right) : Complex =

            (Array.map2 (fun leftElement rightElement -> leftElement * (rightElement |> Complex.Conjucate)) left right)
            |> Array.sum

        static member Norm vector : Complex =
            Complex.SquareRoot(Vector.InnerProduct vector vector)

        static member Distance left right : Complex =
            (Vector.Subtract left right) |> Vector.Norm

        static member Inverse(vector: Vector) : Vector = Vector.Multiply Complex.MinusOne vector

        static member inline (+)(left: Vector, right: Vector) = Vector.Add left right
        static member inline (-)(left: Vector, right: Vector) = Vector.Subtract left right
        static member inline (*)(scalar: Complex, vector: Vector) = Vector.Multiply scalar vector
        static member inline (*)(left: Vector, right: Vector) = Vector.InnerProduct left right
        static member inline (~-)(vector: Vector) = Vector.Inverse vector

    type Matrix =
        | Matrix of Complex [] []

        static member Zero m n : Matrix =
            Matrix(Array.create m (Array.create n Complex.Zero))

        static member Identity n : Matrix =
            ({ 0 .. (n - 1) })
            |> Seq.map (fun i ->
                Array.init n (fun j ->
                    if i = j then
                        Complex(1, 0)
                    else
                        Complex(0, 0)))
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

        static member Multiply (scalar: Complex) (Matrix matrix) : Matrix =

            matrix
            |> Array.map (fun row -> row |> Array.map (fun element -> scalar * element))
            |> Matrix

        static member Transpose(Matrix matrix) : Matrix =
            matrix
            |> Array.mapi (fun i row -> row |> Array.mapi (fun j _ -> matrix[j][i]))
            |> Matrix

        static member Conjucate(Matrix matrix) : Matrix =

            matrix
            |> Array.map (Array.map (fun element -> element |> Complex.Conjucate))
            |> Matrix

        static member Adjoint matrix : Matrix =
            matrix |> Matrix.Conjucate |> Matrix.Transpose

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

        static member IsHermitian matrix : bool = Matrix.Adjoint matrix = matrix

        static member IsUnitary(matrix: Matrix) : bool =

            let RoundOnes (Matrix matrixToRound) =

                let RoundOne (toBeRounded: Complex) =
                    let precision = 0.000000000001

                    let (Complex (real, imaginary)) = toBeRounded

                    if imaginary = 0
                       && System.Math.Abs(real - 1.0) < precision then
                        Complex.One
                    else
                        toBeRounded

                matrixToRound
                |> Array.map (fun row -> row |> Array.map RoundOne)
                |> Matrix

            let (Matrix rows) = matrix
            let identity = Matrix.Identity rows.Length
            let adjoint = Matrix.Adjoint matrix

            Matrix.Product matrix adjoint = Matrix.Product adjoint matrix
            && Matrix.Product matrix adjoint |> RoundOnes = identity

        static member ToVector(Matrix matrix) : Vector =
            matrix |> Array.map (fun row -> row[0]) |> Vector

        static member FromVector(Vector vector) : Matrix =
            vector
            |> Array.map (fun element -> [| element |])
            |> Matrix

        static member Inverse(matrix: Matrix) : Matrix = Matrix.Multiply Complex.MinusOne matrix

        static member inline (+)(left: Matrix, right: Matrix) = Matrix.Add left right
        static member inline (-)(left: Matrix, right: Matrix) = Matrix.Subtract left right
        static member inline (*)(scalar: Complex, matrix: Matrix) = Matrix.Multiply scalar matrix
        static member inline (*)(left: Matrix, right: Matrix) = Matrix.Product left right
        static member inline (*)(matrix: Matrix, vector: Vector) = Matrix.Act matrix vector
        static member inline (~-)(matrix: Matrix) = Matrix.Inverse matrix
