using System.Numerics;
using Computation.Matrices.Complex;
using Computation.Numbers;

namespace LearningLinearAlgebra.ComplexVectorSpace;

public record Ket<TRealNumber>(ColumnVector<TRealNumber> Components)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(Ket<TRealNumber>? other) => 
        Components.Equals(other?.Components);

    public override int GetHashCode() => 
        Components.GetHashCode();

    public static Ket<TRealNumber> V(ColumnVector<TRealNumber> components) =>
        new(components);

    public static Ket<TRealNumber> V(ComplexNumber<TRealNumber>[] components) =>
        V(ColumnVector<TRealNumber>.V(components));

    public static Ket<TRealNumber> Zero(int dimension) =>
        V(ColumnVector<TRealNumber>.Zero(dimension));

    public ComplexNumber<TRealNumber> this[int index] => Components[index];

    public static Ket<TRealNumber> Add(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        V(left.Components.Add(right.Components));

    public static Ket<TRealNumber> AdditiveInverse(Ket<TRealNumber> ket) =>
        V(ket.Components.AdditiveInverse());

    public static Bra<TRealNumber> Bra(Ket<TRealNumber> ket) =>
        Adjoint(ket);

    public static Ket<TRealNumber> Conjucate(Ket<TRealNumber> ket) =>
        V(ket.Components.Conjucate());

    public static Bra<TRealNumber> Transpose(Ket<TRealNumber> ket) =>
        Bra<TRealNumber>.U(ket.Components.Transpose());

    public static Bra<TRealNumber> Adjoint(Ket<TRealNumber> ket) =>
        Bra<TRealNumber>.U(ket.Components.Adjoint());

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

public static class Ket
{
    public static int Dimension<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Dimension(ket);

    public static Ket<TRealNumber> Add<TRealNumber>(this Ket<TRealNumber> left, Ket<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Add(left, right);

    public static Ket<TRealNumber> Subtract<TRealNumber>(this Ket<TRealNumber> left, Ket<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Subtract(left, right);

    public static Ket<TRealNumber> AdditiveInverse<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.AdditiveInverse(ket);

    public static Ket<TRealNumber> Multiply<TRealNumber>(this ComplexNumber<TRealNumber> scalar, Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Multiply(scalar, ket);

    public static Ket<TRealNumber> Multiply<TRealNumber>(this Ket<TRealNumber> ket, ComplexNumber<TRealNumber> scalar)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Multiply(scalar, ket);

    public static ComplexNumber<TRealNumber> InnerProduct<TRealNumber>(this Ket<TRealNumber> left, Ket<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.InnerProduct(left, right);

    public static Ket<TRealNumber> TensorProduct<TRealNumber>(this Ket<TRealNumber> left, Ket<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.TensorProduct(left, right);

    public static Bra<TRealNumber> Bra<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Bra(ket);

    public static TRealNumber Norm<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Norm(ket);

    public static TRealNumber Distance<TRealNumber>(this Ket<TRealNumber> left, Ket<TRealNumber> right)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Distance(left, right);

    public static Ket<TRealNumber> Normalized<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Normalized(ket);

    public static Ket<TRealNumber> Conjucate<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Conjucate(ket);

    public static Bra<TRealNumber> Transpose<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Transpose(ket);

    public static Bra<TRealNumber> Adjoint<TRealNumber>(this Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Adjoint(ket);
}
