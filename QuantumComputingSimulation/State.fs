namespace QuantumComputing


module State =

    open System.Linq
    open LearningLinearAlgebra.Numbers
    open LearningLinearAlgebra.Numbers.Complex
    open LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace

    let AsScalar (vector: Ket<float>) = vector.Single()


    let Probability index (ket: Ket<float>) =
        let i = ket[index]
        let m = Modulus i
        RealNumber.Square(m) / RealNumber.Square(Ket.Norm ket)

    let TransitionAmplitude startState endState = Ket.InnerProduct startState endState

    let NextState startState observable = observable * startState

    let Mean startState (observable: Operator<float>) =
        Ket.InnerProduct (NextState startState observable) startState

    let Variance startState (observable: Operator<float>) =

        let mean = Mean startState observable
        let m = (mean * Operator<float>.Identity 2)
        let delta = observable - m
        let deltaSquared = delta * delta
        let bra = Ket.Bra startState

        bra * (deltaSquared * startState)
