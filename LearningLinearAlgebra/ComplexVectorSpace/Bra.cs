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

    public static Bra<TRealNumber> U(RowVector<TRealNumber> components) =>
        new(components);

    public static Bra<TRealNumber> U(ComplexNumber<TRealNumber>[] components) =>
        U(RowVector<TRealNumber>.U(components));

    public static Bra<TRealNumber> Zero(int dimension) =>
        U(RowVector<TRealNumber>.Zero(dimension));

    public ComplexNumber<TRealNumber> this[int index] => Components[index];

    public static Bra<TRealNumber> Add(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        U(self.Components.Add(other.Components));

    public static Bra<TRealNumber> AdditiveInverse(Bra<TRealNumber> self) =>
        U(self.Components.AdditiveInverse());

    public static Bra<TRealNumber> Conjucate(Bra<TRealNumber> self) =>
        U(self.Components.Conjucate());

    public static int Dimension(Bra<TRealNumber> self) =>
        self.Components.Length();

    public static TRealNumber Distance(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        self.Components.Distance(other.Components);

    public static ComplexNumber<TRealNumber> InnerProduct(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        self.Components.InnerProduct(other.Components);

    public static Ket<TRealNumber> Ket(Bra<TRealNumber> self) =>
        Transpose(self);

    public static Ket<TRealNumber> Adjoint(Bra<TRealNumber> self) =>
        Ket<TRealNumber>.V(self.Components.Adjoint());

    public static Ket<TRealNumber> Transpose(Bra<TRealNumber> self) =>
        Ket<TRealNumber>.V(self.Components.Transpose());

    public static Bra<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar, Bra<TRealNumber> self) =>
        U(self.Components.Multiply(scalar));

    public static ComplexNumber<TRealNumber> Multiply(Bra<TRealNumber> self, Ket<TRealNumber> ket) =>
        self.Components.Multiply(ket.Components);

    public static TRealNumber Norm(Bra<TRealNumber> self) =>
        self.Components.Norm();

    public static Bra<TRealNumber> Normalized(Bra<TRealNumber> self) =>
        U(self.Components.Normalized());

    public static Bra<TRealNumber> Subtract(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        U(self.Components.Subtract(other.Components));

    public static Bra<TRealNumber> TensorProduct(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        U(self.Components.TensorProduct(other.Components));

    public static Bra<TRealNumber> operator +(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        U(self.Components.Add(other.Components));

    public static Bra<TRealNumber> operator -(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        U(self.Components.Subtract(other.Components));

    public static Bra<TRealNumber> operator -(Bra<TRealNumber> self) =>
        U(self.Components.AdditiveInverse());

    public static Bra<TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, Bra<TRealNumber> self) =>
        U(self.Components.Multiply(scalar));

    public static ComplexNumber<TRealNumber> operator *(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        self.Components.InnerProduct(other.Components);
}

public static class Bra
{
    public static int Dimension<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Dimension(self);

    public static Bra<TRealNumber> Add<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Add(self, other);

    public static Bra<TRealNumber> Subtract<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Subtract(self, other);

    public static Bra<TRealNumber> AdditiveInverse<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.AdditiveInverse(self);

    public static Bra<TRealNumber> Multiply<TRealNumber>(this ComplexNumber<TRealNumber> scalar, Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Multiply(scalar, self);

    public static Bra<TRealNumber> Multiply<TRealNumber>(this Bra<TRealNumber> self, ComplexNumber<TRealNumber> scalar)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Multiply(scalar, self);

    public static ComplexNumber<TRealNumber> Multiply<TRealNumber>(this Bra<TRealNumber> self, Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Multiply(self, ket);

    public static ComplexNumber<TRealNumber> InnerProduct<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.InnerProduct(self, other);

    public static Bra<TRealNumber> TensorProduct<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.TensorProduct(self, other);

    public static Ket<TRealNumber> Ket<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Ket(self);

    public static TRealNumber Norm<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Norm(self);

    public static TRealNumber Distance<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Distance(self, other);

    public static Bra<TRealNumber> Normalized<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Normalized(self);

    public static Bra<TRealNumber> Conjucate<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Conjucate(self);

    public static Ket<TRealNumber> Transpose<TRealNumber>(this Bra<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Transpose(ket);

    public static Ket<TRealNumber> Adjoint<TRealNumber>(this Bra<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Adjoint(ket);
}