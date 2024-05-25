namespace QuantumComputing



module Gates =

    open LearningLinearAlgebra.Numbers
    open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace

    let BinaryGate x y = Operator.TensorProduct x y

    let I: Operator<float> =
        M
            [| [| ComplexNumber<float>.One; ComplexNumber<float>.Zero |]
               [| ComplexNumber<float>.Zero; ComplexNumber<float>.One |] |]

    let Xor: Operator<float> =
        M
            [| [| ComplexNumber<float>.One
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.One |]
               [| ComplexNumber<float>.Zero
                  ComplexNumber<float>.One
                  ComplexNumber<float>.One
                  ComplexNumber<float>.Zero |] |]

    let Hadamart: Operator<float> =
        M
            [| [| Complex.OnePerSqrtTwo; Complex.OnePerSqrtTwo |]
               [| Complex.OnePerSqrtTwo; -Complex.OnePerSqrtTwo |] |]

    let ConstantToZero: Operator<float> =
        M
            [| [| ComplexNumber<float>.One
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero |]
               [| ComplexNumber<float>.Zero
                  ComplexNumber<float>.One
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero |]
               [| ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.One
                  ComplexNumber<float>.Zero |]
               [| ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.One |] |]

    let ConstantToOne: Operator<float> =
        M
            [| [| ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero |]
               [| ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero |]
               [| ComplexNumber<float>.One
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.One
                  ComplexNumber<float>.Zero |]
               [| ComplexNumber<float>.Zero
                  ComplexNumber<float>.One
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.One |] |]

    let BalancedZeroToZero: Operator<float> =
        M
            [| [| ComplexNumber<float>.One
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero |]
               [| ComplexNumber<float>.Zero
                  ComplexNumber<float>.One
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero |]
               [| ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.One |]
               [| ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.One
                  ComplexNumber<float>.Zero |] |]

    let BalancedZeroToOne: Operator<float> =
        M
            [| [| ComplexNumber<float>.Zero
                  ComplexNumber<float>.One
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero |]
               [| ComplexNumber<float>.One
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero |]
               [| ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.One
                  ComplexNumber<float>.Zero |]
               [| ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.Zero
                  ComplexNumber<float>.One |] |]
