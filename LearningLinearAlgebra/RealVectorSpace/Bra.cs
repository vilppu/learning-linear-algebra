using System.Numerics;
using LearningLinearAlgebra.Matrices.Real;

namespace LearningLinearAlgebra.RealVectorSpace;

public record Bra<TRealNumber>(RowVector<TRealNumber> Components)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(Bra<TRealNumber>? other) =>
        Components.Equals(other?.Components);

    public override int GetHashCode() =>
        Components.GetHashCode();

    public static Bra<TRealNumber> U(RowVector<TRealNumber> components) =>
        new(components);

    public static Bra<TRealNumber> U(float[] components) =>
        U(RowVector<TRealNumber>.U(components));

    public static Bra<TRealNumber> U(double[] components) =>
        U(RowVector<TRealNumber>.U(components));

    public static Bra<TRealNumber> U(TRealNumber[] components) =>
        U(RowVector<TRealNumber>.U(components));

    public static Bra<TRealNumber> Zero(int dimension) =>
        U(RowVector<TRealNumber>.Zero(dimension));

    public TRealNumber this[int index] => Components[index];

    public static Bra<TRealNumber> Add(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        U(self.Components.Add(other.Components));

    public static Bra<TRealNumber> AdditiveInverse(Bra<TRealNumber> self) =>
        U(self.Components.AdditiveInverse());

    public static TRealNumber Distance(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        self.Components.Distance(other.Components);

    public static int Dimension(Bra<TRealNumber> self) =>
        self.Components.Length();

    public static TRealNumber InnerProduct(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        self.Components.InnerProduct(other.Components);

    public static Ket<TRealNumber> Ket(Bra<TRealNumber> self) =>
        Transpose(self);

    public static Ket<TRealNumber> Transpose(Bra<TRealNumber> self) =>
        Ket<TRealNumber>.V(self.Components.Transpose());

    public static Bra<TRealNumber> Multiply(TRealNumber scalar, Bra<TRealNumber> self) =>
        U(self.Components.Multiply(scalar));

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

    public static Bra<TRealNumber> operator *(TRealNumber scalar, Bra<TRealNumber> self) =>
        U(self.Components.Multiply(scalar));

    public static TRealNumber operator *(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        self.Components.InnerProduct(other.Components);
}

public static class Bra
{
    public static int Dimension<TRealNumber>(this Bra<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Dimension(ket);

    public static Bra<TRealNumber> Add<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Add(self, other);

    public static Bra<TRealNumber> Subtract<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Subtract(self, other);

    public static Bra<TRealNumber> AdditiveInverse<TRealNumber>(this Bra<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.AdditiveInverse(ket);

    public static Bra<TRealNumber> Multiply<TRealNumber>(this TRealNumber scalar, Bra<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Multiply(scalar, ket);

    public static Bra<TRealNumber> Multiply<TRealNumber>(this Bra<TRealNumber> ket, TRealNumber scalar)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Multiply(scalar, ket);

    public static TRealNumber InnerProduct<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.InnerProduct(self, other);

    public static Bra<TRealNumber> TensorProduct<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.TensorProduct(self, other);

    public static Ket<TRealNumber> Ket<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra<TRealNumber>.Ket(self);

    public static Ket<TRealNumber> Transpose<TRealNumber>(this Bra<TRealNumber> self)
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
}
