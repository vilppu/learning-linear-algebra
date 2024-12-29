using System.Numerics;
using Computation.Matrices.Complex;
using Computation.Numbers;

namespace LearningLinearAlgebra.ComplexVectorSpace;

public record Ket<TRealNumber>(ColumnVector<TRealNumber> Components)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static Ket<TRealNumber> V(ColumnVector<TRealNumber> components) =>
        new(components);

    public static Ket<TRealNumber> V(ComplexNumber<TRealNumber>[] components) =>
        V(Matrices<TRealNumber>.V(components));

    public static Ket<TRealNumber> Zero(int dimension) =>
        V(Matrices<TRealNumber>.ZeroColumnVector(dimension));

    public ComplexNumber<TRealNumber> this[int index] => Components[index];

    public static Ket<TRealNumber> Add(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        V(left.Components.Add(right.Components));

    public static Ket<TRealNumber> AdditiveInverse(Ket<TRealNumber> ket) =>
        V(ket.Components.AdditiveInverse());

    public static Bra<TRealNumber> Bra(Ket<TRealNumber> ket) =>
        Bra<TRealNumber>.U(ket.Components.Adjoint());

    public static Ket<TRealNumber> Conjucate(Ket<TRealNumber> ket) =>
        V(ket.Components.Conjucate());

    public static int Dimension(Ket<TRealNumber> ket) =>
        ket.Components.Length();

    public static TRealNumber Distance(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        left.Components.Distance(right.Components);

    public static ComplexNumber<TRealNumber> InnerProduct(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        left.Components.InnerProduct(right.Components);

    public static TRealNumber Norm(Ket<TRealNumber> ket) =>
        ket.Components.Norm();

    public static Ket<TRealNumber> Normalized(Ket<TRealNumber> ket) =>
        V(ket.Components.Normalized());

    public static Ket<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar, Ket<TRealNumber> ket) =>
        V(ket.Components.Multiply(scalar));

    public static ComplexNumber<TRealNumber> Multiply(Bra<TRealNumber> bra, Ket<TRealNumber> ket) =>
        bra.Components.Multiply(ket.Components);

    public static Ket<TRealNumber> Subtract(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        V(left.Components.Subtract(right.Components));

    public static Ket<TRealNumber> TensorProduct(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        V(left.Components.TensorProduct(right.Components));

    public static Ket<TRealNumber> operator +(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        V(left.Components.Add(right.Components));

    public static Ket<TRealNumber> operator -(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        V(left.Components.Subtract(right.Components));

    public static Ket<TRealNumber> operator -(Ket<TRealNumber> ket) =>
        V(ket.Components.AdditiveInverse());

    public static Ket<TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, Ket<TRealNumber> ket) =>
        V(ket.Components.Multiply(scalar));

    public static ComplexNumber<TRealNumber> operator *(Bra<TRealNumber> bra, Ket<TRealNumber> ket) =>
        bra.Components.Multiply(ket.Components);

    public static ComplexNumber<TRealNumber> operator *(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        left.Components.InnerProduct(right.Components);
}
