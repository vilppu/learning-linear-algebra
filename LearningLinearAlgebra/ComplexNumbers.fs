namespace Algebra

module ComplexNumbers =
    open RealNumbers

    type Complex =
        | Complex of float * float

        static member FromReal real = Complex(real, 0)
        static member Zero = Complex(0, 0)
        static member One = Complex(1, 0)
        static member MinusOne = Complex(-1, 0)
        static member MinusZero = Complex(-0, 0)
        static member SqrtTwo = Complex(sqrt 2.0, 0)
        static member MinusSqrtTwo = Complex(sqrt -2.0, 0)
        static member OnePerSqrtTwo = Complex(1.0 / sqrt 2.0, 0)
        static member MinusOnePerSqrtTwo = Complex(-1.0 / sqrt 2.0, 0)

        static member Add (left: Complex) (right: Complex) : Complex =

            let (Complex(leftReal, leftImaginary)) = left
            let (Complex(rightReal, rightImaginary)) = right

            Complex(leftReal + rightReal, leftImaginary + rightImaginary)

        static member Subtract (left: Complex) (right: Complex) : Complex =

            let (Complex(leftReal, leftImaginary)) = left
            let (Complex(rightReal, rightImaginary)) = right

            Complex(leftReal - rightReal, leftImaginary - rightImaginary)

        static member Multiply (left: Complex) (right: Complex) : Complex =

            let (Complex(leftReal, leftImaginary)) = left
            let (Complex(rightReal, rightImaginary)) = right

            Complex(
                leftReal * rightReal - leftImaginary * rightImaginary,
                leftReal * rightImaginary + rightReal * leftImaginary
            )

        static member Square(complex: Complex) : Complex = Complex.Multiply complex complex

        static member Conjucate(complexNumber: Complex) =
            let (Complex(real, imaginary)) = complexNumber

            Complex(real, -1.0 * imaginary)

        static member Divide (numerator: Complex) (denominator: Complex) : Complex =
            let complexConjucateOfDenominator = Complex.Conjucate denominator

            let numeratorMultipliedByComplexConjucate =
                Complex.Multiply numerator complexConjucateOfDenominator

            let denominatorMultipliedByComplexConjucate =
                Complex.Multiply denominator complexConjucateOfDenominator

            let (Complex(numeratorReal, numeratorImaginary)) =
                numeratorMultipliedByComplexConjucate

            let (Complex(demoninatorReal, _)) = denominatorMultipliedByComplexConjucate

            Complex(numeratorReal / demoninatorReal, numeratorImaginary / demoninatorReal)

        static member Modulus(Complex(real, imaginary)) =
            sqrt (real * real + imaginary * imaginary)

        static member SquareRoot(Complex(real, imaginary)) : Complex =
            Complex(sqrt (abs (real)), sqrt (abs ((imaginary))))

        static member Round(Complex(real, imaginary)) : Complex = Complex(Round(real), Round(imaginary))

        static member RoundToTwoDecimals(Complex(real, imaginary)) : Complex =
            Complex(RoundToTwoDecimals(real), RoundToTwoDecimals(imaginary))

        static member inline (+)(left: Complex, right: Complex) = Complex.Add left right
        static member inline (-)(left: Complex, right: Complex) = Complex.Subtract left right
        static member inline (*)(left: Complex, right: Complex) = Complex.Multiply left right
        static member inline (/)(left: Complex, right: Complex) = Complex.Divide left right

    type Vector =
        | Vector of Complex[]

        member this.Item
            with get (index) =
                let (Vector elements) = this
                elements[index]

        static member Zero n : Vector = Vector(Array.create n Complex.Zero)

        static member AsScalar(Vector vector) : Complex = Array.exactlyOne vector

        static member Add (Vector left) (Vector right) : Vector =

            (Array.map2 (fun leftElement rightElement -> leftElement + rightElement) left right)
            |> Vector

        static member Subtract (Vector left) (Vector right) : Vector =

            (Array.map2 (fun leftElement rightElement -> leftElement - rightElement) left right)
            |> Vector

        static member Multiply (scalar: Complex) (Vector vector) : Vector =

            vector |> Array.map (fun element -> scalar * element) |> Vector

        static member MultiplyByReal (scalar: float) (Vector vector) : Vector =
            vector |> Array.map (fun element -> Complex(scalar, 0) * element) |> Vector

        static member InnerProduct (Vector left) (Vector right) : Complex =

            (Array.map2 (fun leftElement rightElement -> leftElement * (rightElement |> Complex.Conjucate)) left right)
            |> Array.sum

        static member Conjucate(Vector vector) : Vector =
            vector |> Array.map (fun element -> element |> Complex.Conjucate) |> Vector

        static member Norm vector : float =
            let (Complex(norm, _)) = Complex.SquareRoot(Vector.InnerProduct vector vector)
            norm

        static member Normalized vector =
            Vector.Multiply (Complex.One / Complex((Vector.Norm vector), 0)) vector

        static member Distance left right : float =
            (Vector.Subtract left right) |> Vector.Norm

        static member TensorProduct (Vector left) (Vector right) : Vector =
            let length = left.Length * right.Length

            Array.init length (fun index ->
                let leftIndex = index / right.Length
                let rightIndex = index % right.Length
                left[leftIndex] * right[rightIndex])
            |> Vector

        static member Inverse(vector: Vector) : Vector = Vector.Multiply Complex.MinusOne vector

        static member Round(Vector vector) : Vector =
            vector |> Array.map (fun element -> Complex.Round element) |> Vector

        static member RoundToTwoDecimals(Vector vector) : Vector =
            vector
            |> Array.map (fun element -> Complex.RoundToTwoDecimals element)
            |> Vector

        static member inline (+)(left, right) = Vector.Add left right
        static member inline (-)(left, right) = Vector.Subtract left right
        static member inline (*)(scalar, vector) = Vector.Multiply scalar vector
        static member inline (*)(scalar: float, vector: Vector) = Vector.MultiplyByReal scalar vector
        static member inline (^<>)(left, right) = Vector.InnerProduct left right
        static member inline (^*)(left, right) = Vector.TensorProduct left right
        static member inline (~-)(vector) = Vector.Inverse vector

    type Matrix =
        | Matrix of Complex[][]

        member this.Item
            with get (index) =
                let (Matrix rows) = this
                rows[index]

        static member Fill m n elementValue : Matrix =
            Array.init m (fun i -> Array.init n (fun j -> elementValue i j)) |> Matrix

        static member Zero m n : Matrix =
            Matrix.Fill m n (fun i j -> Complex.Zero)

        static member Identity m : Matrix =
            Matrix.Fill m m (fun i j -> if i = j then Complex.One else Complex.Zero)

        static member AsVector(Matrix matrix) : Vector =
            matrix |> Array.map (fun row -> Array.exactlyOne row) |> Vector

        static member AsScalar matrix : Complex =
            matrix |> Matrix.AsVector |> Vector.AsScalar

        static member M(matrix: Complex[][]) = matrix.Length

        static member N(matrix: Complex[][]) =
            if matrix.Length > 0 then matrix[0].Length else 0

        static member Add (Matrix left) (Matrix right) : Matrix =

            let add leftRow rightRow =
                rightRow
                |> (leftRow
                    |> Array.map2 (fun leftElement rightElement -> leftElement + rightElement))

            Array.map2 (fun leftRow rightRow -> add leftRow rightRow) left right |> Matrix

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
            Matrix.Fill (Matrix.N matrix) (Matrix.M matrix) (fun i j -> matrix[j][i])

        static member Transpose(Vector vector) : Matrix = [| vector |] |> Matrix

        static member Conjucate(Matrix matrix) : Matrix =
            matrix
            |> Array.map (Array.map (fun element -> element |> Complex.Conjucate))
            |> Matrix

        static member Conjucate(Vector vector) : Vector =
            vector |> Array.map (fun element -> element |> Complex.Conjucate) |> Vector

        static member Adjoint(matrix: Matrix) : Matrix =
            matrix |> Matrix.Conjucate |> Matrix.Transpose

        static member Adjoint(vector: Vector) : Matrix =
            vector |> Matrix.Conjucate |> Matrix.Transpose

        static member Product (Matrix left) (Matrix right) : Matrix =

            left
            |> Array.mapi (fun i row ->
                Array.init (right[0].Length) (fun j ->
                    let column = right |> Array.map (fun row -> row[j])

                    column |> (row |> Array.map2 (fun x y -> x * y)) |> Array.sum

                ))
            |> Seq.toArray
            |> Matrix

        static member Act (Matrix matrix) (Vector vector) : Vector =
            matrix
            |> Array.mapi (fun i row -> vector |> (row |> Array.map2 (fun x y -> x * y)) |> Array.sum

            )
            |> Seq.toArray
            |> Vector

        static member Commutator (left: Matrix) (right: Matrix) : Matrix =
            Matrix.Subtract (Matrix.Product left right) (Matrix.Product right left)

        static member Round(Matrix matrix) : Matrix =
            matrix
            |> Array.map (fun row -> row |> Array.map (fun element -> Complex.Round element))
            |> Matrix

        static member IsHermitian(matrix: Matrix) : bool = Matrix.Adjoint matrix = matrix

        static member IsUnitary(matrix: Matrix) : bool =

            let RoundOnes (Matrix matrixToRound) =

                let RoundOne (toBeRounded: Complex) =
                    let precision = 0.000000000001

                    let (Complex(real, imaginary)) = toBeRounded

                    if imaginary = 0 && abs (real - 1.0) < precision then
                        Complex.One
                    else
                        toBeRounded

                matrixToRound |> Array.map (fun row -> row |> Array.map RoundOne) |> Matrix

            let (Matrix rows) = matrix
            let identity = Matrix.Identity rows.Length
            let adjoint = Matrix.Adjoint matrix

            Matrix.Product matrix adjoint = Matrix.Product adjoint matrix
            && Matrix.Product matrix adjoint |> RoundOnes = identity

        static member TensorProduct (Matrix left) (Matrix right) : Matrix =

            let ColumnCount (matrix: Complex[][]) =
                match matrix.Length with
                | 0 -> 0
                | _ -> matrix[0].Length

            let rowCount = left.Length * right.Length
            let columnCount = (left |> ColumnCount) * (right |> ColumnCount)

            let m = right.Length
            let n = (right |> ColumnCount)

            Matrix.Fill rowCount columnCount (fun j k -> left[j / n][k / m] * right[j % n][k % m])

        static member Inverse(matrix: Matrix) : Matrix = Matrix.Multiply Complex.MinusOne matrix

        static member inline (+)(left, right) = Matrix.Add left right
        static member inline (-)(left, right) = Matrix.Subtract left right
        static member inline (*)(scalar, matrix) = Matrix.Multiply scalar matrix
        static member inline (*)(left: Matrix, right: Matrix) = Matrix.Product left right
        static member inline (*)(matrix: Matrix, vector: Vector) = Matrix.Act matrix vector
        static member inline (^*)(matrix, vector) = Matrix.TensorProduct matrix vector
        static member inline (~-)(matrix) = Matrix.Inverse matrix

    let C = Complex
    let V = Vector
    let M = Matrix
