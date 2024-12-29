using System.Numerics;
using Computation.Matrices.Real;

namespace LearningLinearAlgebra.RealVectorSpace;

public record Ket<TRealNumber>(ColumnVector<TRealNumber> Components)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static Ket<TRealNumber> V(ColumnVector<TRealNumber> components) =>
        new(components);

    public static Ket<TRealNumber> V(TRealNumber[] components) =>
        V(Matrices<TRealNumber>.V(components));

    public static Ket<TRealNumber> Zero(int dimension) =>
        V(Matrices<TRealNumber>.ZeroColumnVector(dimension));

    public TRealNumber this[int index] => Components[index];

    public static Ket<TRealNumber> Add(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        V(left.Components.Add(right.Components));

    public static Ket<TRealNumber> AdditiveInverse(Ket<TRealNumber> ket) =>
        V(ket.Components.AdditiveInverse());

    public static Bra<TRealNumber> Bra(Ket<TRealNumber> ket) =>
        Bra<TRealNumber>.U(ket.Components.Transpose());

    public static int Dimension(Ket<TRealNumber> ket) =>
        ket.Components.Length();

    public static TRealNumber Distance(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        left.Components.Distance(right.Components);

    public static TRealNumber InnerProduct(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        left.Components.InnerProduct(right.Components);

    public static Ket<TRealNumber> Multiply(TRealNumber scalar, Ket<TRealNumber> ket) =>
        V(ket.Components.Multiply(scalar));

    public static TRealNumber Multiply(Bra<TRealNumber> bra, Ket<TRealNumber> ket) =>
        bra.Components.Multiply(ket.Components);

    public static TRealNumber Norm(Ket<TRealNumber> ket) =>
        ket.Components.Norm();

    public static Ket<TRealNumber> Normalized(Ket<TRealNumber> ket) =>
        V(ket.Components.Normalized());

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

    public static Ket<TRealNumber> operator *(TRealNumber scalar, Ket<TRealNumber> ket) =>
        V(ket.Components.Multiply(scalar));

    public static TRealNumber operator *(Bra<TRealNumber> bra, Ket<TRealNumber> ket) =>
        bra.Components.Multiply(ket.Components);

    public static TRealNumber operator *(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        left.Components.InnerProduct(right.Components);
}
