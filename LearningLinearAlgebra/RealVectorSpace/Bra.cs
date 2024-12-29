using System.Numerics;
using Computation.Matrices.Real;

namespace LearningLinearAlgebra.RealVectorSpace;

public record Bra<TRealNumber>(RowVector<TRealNumber> Components)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static Bra<TRealNumber> U(RowVector<TRealNumber> components) =>
        new(components);

    public static Bra<TRealNumber> U(TRealNumber[] components) =>
        U(Matrices<TRealNumber>.U(components));

    public static Bra<TRealNumber> Zero(int dimension) =>
        U(Matrices<TRealNumber>.ZeroRowVector(dimension));

    public TRealNumber this[int index] => Components[index];

    public static Bra<TRealNumber> Add(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        U(left.Components.Add(right.Components));

    public static Bra<TRealNumber> AdditiveInverse(Bra<TRealNumber> bra) =>
        U(bra.Components.AdditiveInverse());

    public static TRealNumber Distance(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        left.Components.Distance(right.Components);

    public static int Dimension(Bra<TRealNumber> bra) =>
        bra.Components.Length();

    public static TRealNumber InnerProduct(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        left.Components.InnerProduct(right.Components);

    public static Ket<TRealNumber> Ket(Bra<TRealNumber> bra) =>
        Ket<TRealNumber>.V(bra.Components.Transpose());

    public static Bra<TRealNumber> Multiply(TRealNumber scalar, Bra<TRealNumber> bra) =>
        U(bra.Components.Multiply(scalar));

    public static TRealNumber Norm(Bra<TRealNumber> bra) =>
        bra.Components.Norm();

    public static Bra<TRealNumber> Normalized(Bra<TRealNumber> bra) =>
        U(bra.Components.Normalized());

    public static Bra<TRealNumber> Subtract(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        U(left.Components.Subtract(right.Components));

    public static Bra<TRealNumber> TensorProduct(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        U(left.Components.TensorProduct(right.Components));

    public static Bra<TRealNumber> operator +(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        U(left.Components.Add(right.Components));

    public static Bra<TRealNumber> operator -(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        U(left.Components.Subtract(right.Components));

    public static Bra<TRealNumber> operator -(Bra<TRealNumber> bra) =>
        U(bra.Components.AdditiveInverse());

    public static Bra<TRealNumber> operator *(TRealNumber scalar, Bra<TRealNumber> bra) =>
        U(bra.Components.Multiply(scalar));

    public static TRealNumber operator *(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        left.Components.InnerProduct(right.Components);
}
