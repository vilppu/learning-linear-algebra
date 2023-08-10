namespace Algebra

module ComplexVectorSpace =

    open Algebra.ComplexNumbers.CartesianPresentation

    type Vector =
        | Vector of Complex []

        static member Zero n : Vector = Vector(Array.create n Complex.Zero)

        static member Add (left: Vector) (right: Vector) : Vector =

            let (Vector (left)) = left
            let (Vector (right)) = right

            (Array.map2 (fun leftElement rightElement -> leftElement + rightElement) left right)
            |> Vector

        static member Subtract (left: Vector) (right: Vector) : Vector =

            let (Vector (left)) = left
            let (Vector (right)) = right

            (Array.map2 (fun leftElement rightElement -> leftElement - rightElement) left right)
            |> Vector

        static member Multiply (scalar: Complex) (vector: Vector) : Vector =

            let (Vector (vector)) = vector

            vector
            |> Array.map (fun element -> scalar * element)
            |> Vector

        static member Inverse(vector: Vector) : Vector = Vector.Multiply Complex.MinusOne vector

        static member inline public (+)(left: Vector, right: Vector) = Vector.Add left right
        static member inline public (-)(left: Vector, right: Vector) = Vector.Subtract left right
        static member inline public (*)(scalar: Complex, vector: Vector) = Vector.Multiply scalar vector
        static member inline public (~-)(vector: Vector) = Vector.Inverse vector
