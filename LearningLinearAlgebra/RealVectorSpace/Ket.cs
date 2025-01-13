using System.Numerics;
using LearningLinearAlgebra.Matrices.Real;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.RealVectorSpace;

public record Ket<TRealNumber>(ColumnVector<TRealNumber> Components)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(Ket<TRealNumber>? other) =>
        Components.Equals(other?.Components);

    public override int GetHashCode() =>
        Components.GetHashCode();

    public TRealNumber this[int index] => Components[index];

    public static Ket<TRealNumber> operator +(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        Ket.V(self.Components.Add(other.Components));

    public static Ket<TRealNumber> operator -(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        Ket.V(self.Components.Subtract(other.Components));

    public static Ket<TRealNumber> operator -(Ket<TRealNumber> self) =>
        Ket.V(self.Components.AdditiveInverse());

    public static Ket<TRealNumber> operator *(TRealNumber scalar, Ket<TRealNumber> self) =>
        Ket.V(self.Components.Multiply(scalar));

    public static TRealNumber operator *(Bra<TRealNumber> bra, Ket<TRealNumber> self) =>
        bra.Components.Multiply(self.Components);

    public static TRealNumber operator *(Ket<TRealNumber> self, Ket<TRealNumber> other) =>
        self.Components.InnerProduct(other.Components);
}

public static class Ket
{
    public static Ket<TRealNumber> V<TRealNumber>(ColumnVector<TRealNumber> components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(components);

    public static Ket<TRealNumber> V<TRealNumber>(float[] components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        V(ColumnVector<TRealNumber>.V(components));

    public static Ket<TRealNumber> V<TRealNumber>(double[] components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        V(ColumnVector<TRealNumber>.V(components));

    public static Ket<TRealNumber> V<TRealNumber>(TRealNumber[] components)
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
        Ket.V(self.Components.Add(other.Components));

    public static Ket<TRealNumber> Subtract<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket.V(self.Components.Subtract(other.Components));

    public static Ket<TRealNumber> AdditiveInverse<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket.V(self.Components.AdditiveInverse());

    public static Ket<TRealNumber> Multiply<TRealNumber>(this TRealNumber scalar, Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket.V(self.Components.Multiply(scalar));

    public static Ket<TRealNumber> Multiply<TRealNumber>(this Ket<TRealNumber> self, TRealNumber scalar)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket.V(self.Components.Multiply(scalar));

    public static TRealNumber Multiply<TRealNumber>(this Bra<TRealNumber> bra, Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        bra.Components.Multiply(self.Components);

    public static TRealNumber InnerProduct<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.InnerProduct(other.Components);

    public static Ket<TRealNumber> TensorProduct<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket.V(self.Components.TensorProduct(other.Components));

    public static Bra<TRealNumber> Bra<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        RealVectorSpace.Bra.U(self.Components.Transpose());

    public static Bra<TRealNumber> Transpose<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        RealVectorSpace.Bra.U(self.Components.Transpose());

    public static TRealNumber Norm<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.Norm();

    public static TRealNumber Distance<TRealNumber>(this Ket<TRealNumber> self, Ket<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.Distance(other.Components);

    public static Ket<TRealNumber> Normalized<TRealNumber>(this Ket<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Ket.V(self.Components.Normalized());
}