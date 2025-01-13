using System.Numerics;
using LearningLinearAlgebra.Matrices.Complex;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.ComplexVectorSpace;

public record Ket<TRealNumber>(ColumnVector<TRealNumber> Components)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(Ket<TRealNumber>? other) => 
        Components.Equals(other?.Components);

    public override int GetHashCode() => 
        Components.GetHashCode();

    public ComplexNumber<TRealNumber> this[int index] => Components[index];

    public static Ket<TRealNumber> operator +(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        self.Add(other);

    public static Ket<TRealNumber> operator -(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        self.Subtract(other);

    public static Ket<TRealNumber> operator -(Ket<TRealNumber> self) =>
        self.AdditiveInverse();

    public static Ket<TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, Ket<TRealNumber> self) =>
        self.Multiply(scalar);

    public static ComplexNumber<TRealNumber> operator *(Bra<TRealNumber> bra, Ket<TRealNumber> self) =>
        bra.Multiply(self);

    public static ComplexNumber<TRealNumber> operator *(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        self.InnerProduct(other);
}

public static class Ket
{
    public static Ket<TRealNumber> V<TRealNumber>(ColumnVector<TRealNumber> components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(components);

    public static Ket<TRealNumber> V<TRealNumber>(ComplexNumber<TRealNumber>[] components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        V(ColumnVector<TRealNumber>.V(components));

    public static Ket<TRealNumber> Zero<TRealNumber>(int dimension)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        V(ColumnVector<TRealNumber>.Zero(dimension));

    public static int Dimension<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.Length();

    public static Ket<TRealNumber> Add<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        (Ket<TRealNumber>)new Ket<TRealNumber>(self.Components.Add(other.Components));

    public static Ket<TRealNumber> Subtract<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        (Ket<TRealNumber>)new Ket<TRealNumber>(self.Components.Subtract(other.Components));

    public static Ket<TRealNumber> AdditiveInverse<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        (Ket<TRealNumber>)new Ket<TRealNumber>(self.Components.AdditiveInverse());

    public static Ket<TRealNumber> Multiply<TRealNumber>(this ComplexNumber<TRealNumber> scalar, Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        (Ket<TRealNumber>)new Ket<TRealNumber>(self.Components.Multiply(scalar));

    public static Ket<TRealNumber> Multiply<TRealNumber>(this Ket<TRealNumber> self, ComplexNumber<TRealNumber> scalar)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        (Ket<TRealNumber>)new Ket<TRealNumber>(self.Components.Multiply(scalar));

    public static ComplexNumber<TRealNumber> InnerProduct<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.InnerProduct(other.Components);

    public static Ket<TRealNumber> TensorProduct<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        (Ket<TRealNumber>)new Ket<TRealNumber>(self.Components.TensorProduct(other.Components));

    public static Bra<TRealNumber> Bra<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexVectorSpace.Bra.U(self.Components.Adjoint());

    public static TRealNumber Norm<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.Norm();

    public static TRealNumber Distance<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.Distance(other.Components);

    public static Ket<TRealNumber> Normalized<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        (Ket<TRealNumber>)new Ket<TRealNumber>(self.Components.Normalized());

    public static Ket<TRealNumber> Conjucate<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        (Ket<TRealNumber>)new Ket<TRealNumber>(self.Components.Conjucate());

    public static Bra<TRealNumber> Transpose<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexVectorSpace.Bra.U(self.Components.Transpose());

    public static Bra<TRealNumber> Adjoint<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ComplexVectorSpace.Bra.U(self.Components.Adjoint());
}
