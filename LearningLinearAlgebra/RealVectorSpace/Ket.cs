using System.Numerics;
using LearningLinearAlgebra.Matrices.Real;

namespace LearningLinearAlgebra.RealVectorSpace;

public record Ket<TRealNumber>(ColumnVector<TRealNumber> Components)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(Ket<TRealNumber>? other) =>
        Components.Equals(other?.Components);

    public override int GetHashCode() =>
        Components.GetHashCode();

    public static Ket<TRealNumber> V(ColumnVector<TRealNumber> components) =>
        new(components);

    public static Ket<TRealNumber> V(float[] components) =>
        V(ColumnVector<TRealNumber>.V(components));

    public static Ket<TRealNumber> V(double[] components) =>
        V(ColumnVector<TRealNumber>.V(components));

    public static Ket<TRealNumber> V(TRealNumber[] components) =>
        V(ColumnVector<TRealNumber>.V(components));

    public static Ket<TRealNumber> Zero(int dimension) =>
        V(ColumnVector<TRealNumber>.Zero(dimension));

    public TRealNumber this[int index] => Components[index];

    public static Ket<TRealNumber> Add(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        V(self.Components.Add(other.Components));

    public static Ket<TRealNumber> AdditiveInverse(Ket<TRealNumber> self) =>
        V(self.Components.AdditiveInverse());

    public static Bra<TRealNumber> Bra(Ket<TRealNumber> self) =>
        Transpose(self);

    public static Bra<TRealNumber> Transpose(Ket<TRealNumber> self) =>
        Bra<TRealNumber>.U(self.Components.Transpose());

    public static int Dimension(Ket<TRealNumber> self) =>
        self.Components.Length();

    public static TRealNumber Distance(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        self.Components.Distance(other.Components);

    public static TRealNumber InnerProduct(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        self.Components.InnerProduct(other.Components);

    public static Ket<TRealNumber> Multiply(TRealNumber scalar, Ket<TRealNumber> self) =>
        V(self.Components.Multiply(scalar));

    public static TRealNumber Multiply(Bra<TRealNumber> bra, Ket<TRealNumber> self) =>
        bra.Components.Multiply(self.Components);

    public static TRealNumber Norm(Ket<TRealNumber> self) =>
        self.Components.Norm();

    public static Ket<TRealNumber> Normalized(Ket<TRealNumber> self) =>
        V(self.Components.Normalized());

    public static Ket<TRealNumber> Subtract(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        V(self.Components.Subtract(other.Components));

    public static Ket<TRealNumber> TensorProduct(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        V(self.Components.TensorProduct(other.Components));

    public static Ket<TRealNumber> operator +(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        V(self.Components.Add(other.Components));

    public static Ket<TRealNumber> operator -(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        V(self.Components.Subtract(other.Components));

    public static Ket<TRealNumber> operator -(Ket<TRealNumber> self) =>
        V(self.Components.AdditiveInverse());

    public static Ket<TRealNumber> operator *(TRealNumber scalar, Ket<TRealNumber> self) =>
        V(self.Components.Multiply(scalar));

    public static TRealNumber operator *(Bra<TRealNumber> bra, Ket<TRealNumber> self) =>
        bra.Components.Multiply(self.Components);

    public static TRealNumber operator *(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        self.Components.InnerProduct(other.Components);
}

public static class Ket
{
    public static int Dimension<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Dimension(self);

    public static Ket<TRealNumber> Add<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Add(self, other);

    public static Ket<TRealNumber> Subtract<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Subtract(self, other);

    public static Ket<TRealNumber> AdditiveInverse<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.AdditiveInverse(self);

    public static Ket<TRealNumber> Multiply<TRealNumber>(this TRealNumber scalar, Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Multiply(scalar, self);

    public static Ket<TRealNumber> Multiply<TRealNumber>(this Ket<TRealNumber> self, TRealNumber scalar)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Multiply(scalar, self);

    public static TRealNumber Multiply<TRealNumber>(this Bra<TRealNumber> bra, Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Multiply(bra, self);

    public static TRealNumber InnerProduct<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.InnerProduct(self, other);

    public static Ket<TRealNumber> TensorProduct<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.TensorProduct(self, other);

    public static Bra<TRealNumber> Bra<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Bra(self);

    public static Bra<TRealNumber> Transpose<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Transpose(self);

    public static TRealNumber Norm<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Norm(self);

    public static TRealNumber Distance<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Distance(self, other);

    public static Ket<TRealNumber> Normalized<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket<TRealNumber>.Normalized(self);
}