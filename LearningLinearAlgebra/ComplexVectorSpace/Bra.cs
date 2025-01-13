using System.Numerics;
using LearningLinearAlgebra.Matrices.Complex;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.ComplexVectorSpace;

public record Bra<TRealNumber>(RowVector<TRealNumber> Components)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(Bra<TRealNumber>? other) =>
        Components.Equals(other?.Components);

    public override int GetHashCode() =>
        Components.GetHashCode();

    public ComplexNumber<TRealNumber> this[int index] => Components[index];

    public static Bra<TRealNumber> operator +(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        self.Add(other);

    public static Bra<TRealNumber> operator -(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        self.Subtract(other);

    public static Bra<TRealNumber> operator -(Bra<TRealNumber> self) =>
        self.AdditiveInverse();

    public static Bra<TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, Bra<TRealNumber> self) =>
        self.Multiply(scalar);

    public static ComplexNumber<TRealNumber> operator *(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        self.InnerProduct(other);
}

public static class Bra
{
    public static Bra<TRealNumber> U<TRealNumber>(RowVector<TRealNumber> components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(components);

    public static Bra<TRealNumber> U<TRealNumber>(ComplexNumber<TRealNumber>[] components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(RowVector<TRealNumber>.U(components));

    public static Bra<TRealNumber> Zero<TRealNumber>(int dimension)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(RowVector<TRealNumber>.Zero(dimension));

    public static int Dimension<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.Length();

    public static Bra<TRealNumber> Add<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(self.Components.Add(other.Components));

    public static Bra<TRealNumber> Subtract<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(self.Components.Subtract(other.Components));

    public static Bra<TRealNumber> AdditiveInverse<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(self.Components.AdditiveInverse());

    public static Bra<TRealNumber> Multiply<TRealNumber>(this ComplexNumber<TRealNumber> scalar, Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(self.Components.Multiply(scalar));

    public static Bra<TRealNumber> Multiply<TRealNumber>(this Bra<TRealNumber> self, ComplexNumber<TRealNumber> scalar)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(self.Components.Multiply(scalar));

    public static ComplexNumber<TRealNumber> Multiply<TRealNumber>(this Bra<TRealNumber> self, Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.Multiply(ket.Components);

    public static ComplexNumber<TRealNumber> InnerProduct<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.InnerProduct(other.Components);

    public static Bra<TRealNumber> TensorProduct<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(self.Components.TensorProduct(other.Components));

    public static Ket<TRealNumber> Ket<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        (Ket<TRealNumber>)new Ket<TRealNumber>(self.Components.Adjoint());

    public static TRealNumber Norm<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.Norm();

    public static TRealNumber Distance<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.Distance(other.Components);

    public static Bra<TRealNumber> Normalized<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(self.Components.Normalized());

    public static Bra<TRealNumber> Conjucate<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
       U(self.Components.Conjucate());

    public static Ket<TRealNumber> Transpose<TRealNumber>(this Bra<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        (Ket<TRealNumber>)new Ket<TRealNumber>(ket.Components.Transpose());

    public static Ket<TRealNumber> Adjoint<TRealNumber>(this Bra<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        (Ket<TRealNumber>)new Ket<TRealNumber>(ket.Components.Adjoint());
}