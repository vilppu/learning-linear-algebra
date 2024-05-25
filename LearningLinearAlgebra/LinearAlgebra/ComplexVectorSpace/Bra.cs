using LearningLinearAlgebra.Matrices.Complex;
using LearningLinearAlgebra.Numbers;
using System.Collections;
using System.Numerics;

namespace LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace;

public record Bra<TRealNumber>(RowVector<TRealNumber> Components)
    : IBra<Bra<TRealNumber>, Ket<TRealNumber>, TRealNumber>, IEnumerable<ComplexNumber<TRealNumber>>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    IEnumerator IEnumerable.GetEnumerator() => Components.GetEnumerator();

    public IEnumerator<ComplexNumber<TRealNumber>> GetEnumerator() => Components.GetEnumerator();

    public ComplexNumber<TRealNumber> this[int index] => Components[index];

    public static Bra<TRealNumber> Create(RowVector<TRealNumber> components) =>
        new(components);

    public static Bra<TRealNumber> U(ComplexNumber<TRealNumber>[] components) =>
        Create(RowVector<TRealNumber>.U(components));

    public static int Dimension(Bra<TRealNumber> bra) =>
        RowVector<TRealNumber>.Length(bra.Components);

    public static Bra<TRealNumber> Zero(int dimension) =>
        Create(RowVector<TRealNumber>.Zero(dimension));

    public static Bra<TRealNumber> Add(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        Create(RowVector<TRealNumber>.Add(left.Components, right.Components));

    public static Bra<TRealNumber> operator +(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        Create(RowVector<TRealNumber>.Add(left.Components, right.Components));

    public static Bra<TRealNumber> Subtract(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        Create(RowVector<TRealNumber>.Subtract(left.Components, right.Components));

    public static Bra<TRealNumber> operator -(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        Create(RowVector<TRealNumber>.Subtract(left.Components, right.Components));

    public static Bra<TRealNumber> AdditiveInverse(Bra<TRealNumber> bra) =>
        Create(RowVector<TRealNumber>.AdditiveInverse(bra.Components));

    public static Bra<TRealNumber> operator -(Bra<TRealNumber> bra) =>
        Create(RowVector<TRealNumber>.AdditiveInverse(bra.Components));

    public static Bra<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar, Bra<TRealNumber> bra) =>
        Create(RowVector<TRealNumber>.Multiply(scalar, bra.Components));

    public static Bra<TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, Bra<TRealNumber> bra) =>
        Create(RowVector<TRealNumber>.Multiply(scalar, bra.Components));

    public static ComplexNumber<TRealNumber> InnerProduct(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        RowVector<TRealNumber>.InnerProduct(left.Components, right.Components);

    public static ComplexNumber<TRealNumber> operator *(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        RowVector<TRealNumber>.InnerProduct(left.Components, right.Components);

    public static Bra<TRealNumber> TensorProduct(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        Create(RowVector<TRealNumber>.TensorProduct(left.Components, right.Components));

    public static Ket<TRealNumber> Ket(Bra<TRealNumber> bra) =>
        Ket<TRealNumber>.Create(RowVector<TRealNumber>.Adjoint(bra.Components));

    public static TRealNumber Norm(Bra<TRealNumber> bra) =>
        RowVector<TRealNumber>.Norm(bra.Components);

    public static TRealNumber Distance(Bra<TRealNumber> left, Bra<TRealNumber> right) =>
        RowVector<TRealNumber>.Distance(left.Components, right.Components);

    public static Bra<TRealNumber> Normalized(Bra<TRealNumber> bra) =>
        Create(RowVector<TRealNumber>.Normalized(bra.Components));

    public static Bra<TRealNumber> Conjucate(Bra<TRealNumber> bra) =>
        Create(RowVector<TRealNumber>.Conjucate(bra.Components));
}