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

    public TRealNumber this[int index] => Components[index];

    public static Bra<TRealNumber> operator +(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        self.Add(other);

    public static Bra<TRealNumber> operator -(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        self.Subtract(other);

    public static Bra<TRealNumber> operator -(Bra<TRealNumber> self) =>
        self.AdditiveInverse();

    public static Bra<TRealNumber> operator *(TRealNumber scalar, Bra<TRealNumber> self) =>
        self.Multiply(scalar);

    public static TRealNumber operator *(Bra<TRealNumber> self, Bra<TRealNumber> other) =>
        self.InnerProduct(other);
}

public static class Bra
{
    public static Bra<TRealNumber> U<TRealNumber>(RowVector<TRealNumber> components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(components);

    public static Bra<TRealNumber> U<TRealNumber>(float[] components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(RowVector<TRealNumber>.U(components));

    public static Bra<TRealNumber> U<TRealNumber>(double[] components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(RowVector<TRealNumber>.U(components));

    public static Bra<TRealNumber> U<TRealNumber>(TRealNumber[] components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(RowVector<TRealNumber>.U(components));

    public static Bra<TRealNumber> Zero<TRealNumber>(int dimension)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(RowVector<TRealNumber>.Zero(dimension));

    public static int Dimension<TRealNumber>(this Bra<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        ket.Components.Length();

    public static Bra<TRealNumber> Add<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(self.Components.Add(other.Components));

    public static Bra<TRealNumber> Subtract<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(self.Components.Subtract(other.Components));

    public static Bra<TRealNumber> AdditiveInverse<TRealNumber>(this Bra<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(ket.Components.AdditiveInverse());

    public static Bra<TRealNumber> Multiply<TRealNumber>(this TRealNumber scalar, Bra<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(ket.Components.Multiply(scalar));

    public static Bra<TRealNumber> Multiply<TRealNumber>(this Bra<TRealNumber> ket, TRealNumber scalar)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(ket.Components.Multiply(scalar));

    public static TRealNumber InnerProduct<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.InnerProduct(other.Components);

    public static Bra<TRealNumber> TensorProduct<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(self.Components.TensorProduct(other.Components));

    public static Ket<TRealNumber> Ket<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        RealVectorSpace.Ket.V(self.Components.Transpose());

    public static Ket<TRealNumber> Transpose<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        RealVectorSpace.Ket.V(self.Components.Transpose());

    public static TRealNumber Norm<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.Norm();

    public static TRealNumber Distance<TRealNumber>(this Bra<TRealNumber> self, Bra<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.Distance(other.Components);

    public static Bra<TRealNumber> Normalized<TRealNumber>(this Bra<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        U(self.Components.Normalized());
}
