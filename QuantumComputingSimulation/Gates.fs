namespace QuantumComputing



module Gates =

    open LearningLinearAlgebra.Numbers
    open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace

    let BinaryGate x y = Operator.TensorProduct x y

    let I: Operator =
        M
            [| [| ComplexNumber.One; ComplexNumber.Zero |]
               [| ComplexNumber.Zero; ComplexNumber.One |] |]

    let Xor: Operator =
        M
            [| [| ComplexNumber.One
                  ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.One |]
               [| ComplexNumber.Zero
                  ComplexNumber.One
                  ComplexNumber.One
                  ComplexNumber.Zero |] |]

    let Hadamart: Operator =
        M
            [| [| Complex.OnePerSqrtTwo; Complex.OnePerSqrtTwo |]
               [| Complex.OnePerSqrtTwo; -Complex.OnePerSqrtTwo |] |]

    let ConstantToZero: Operator =
        M
            [| [| ComplexNumber.One
                  ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.Zero |]
               [| ComplexNumber.Zero
                  ComplexNumber.One
                  ComplexNumber.Zero
                  ComplexNumber.Zero |]
               [| ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.One
                  ComplexNumber.Zero |]
               [| ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.One |] |]

    let ConstantToOne: Operator =
        M
            [| [| ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.Zero |]
               [| ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.Zero |]
               [| ComplexNumber.One
                  ComplexNumber.Zero
                  ComplexNumber.One
                  ComplexNumber.Zero |]
               [| ComplexNumber.Zero
                  ComplexNumber.One
                  ComplexNumber.Zero
                  ComplexNumber.One |] |]

    let BalancedZeroToZero: Operator =
        M
            [| [| ComplexNumber.One
                  ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.Zero |]
               [| ComplexNumber.Zero
                  ComplexNumber.One
                  ComplexNumber.Zero
                  ComplexNumber.Zero |]
               [| ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.One |]
               [| ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.One
                  ComplexNumber.Zero |] |]

    let BalancedZeroToOne: Operator =
        M
            [| [| ComplexNumber.Zero
                  ComplexNumber.One
                  ComplexNumber.Zero
                  ComplexNumber.Zero |]
               [| ComplexNumber.One
                  ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.Zero |]
               [| ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.One
                  ComplexNumber.Zero |]
               [| ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.Zero
                  ComplexNumber.One |] |]
