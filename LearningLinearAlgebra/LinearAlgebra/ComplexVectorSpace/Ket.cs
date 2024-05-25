using System.Collections;
using System.Numerics;
using LearningLinearAlgebra.Matrices.Complex;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace;

public record Ket<TRealNumber>(ColumnVector<TRealNumber> Components)
    : IKet<Ket<TRealNumber>, Bra<TRealNumber>, TRealNumber>, IEnumerable<ComplexNumber<TRealNumber>>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    IEnumerator IEnumerable.GetEnumerator() => Components.GetEnumerator();

    public IEnumerator<ComplexNumber<TRealNumber>> GetEnumerator() => Components.GetEnumerator();

    public ComplexNumber<TRealNumber> this[int index] => Components[index];

    public static Ket<TRealNumber> Create(ColumnVector<TRealNumber> components) =>
        new(components);

    public static Ket<TRealNumber> V(ComplexNumber<TRealNumber>[] components) =>
        Create(ColumnVector<TRealNumber>.V(components));

    public static int Dimension(Ket<TRealNumber> ket) =>
        ColumnVector<TRealNumber>.Length(ket.Components);

    public static Ket<TRealNumber> Zero(int dimension) =>
        Create(ColumnVector<TRealNumber>.Zero(dimension));

    public static Ket<TRealNumber> Add(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        Create(ColumnVector<TRealNumber>.Add(left.Components, right.Components));

    public static Ket<TRealNumber> operator +(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        Create(ColumnVector<TRealNumber>.Add(left.Components, right.Components));

    public static Ket<TRealNumber> Subtract(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        Create(ColumnVector<TRealNumber>.Subtract(left.Components, right.Components));

    public static Ket<TRealNumber> operator -(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        Create(ColumnVector<TRealNumber>.Subtract(left.Components, right.Components));

    public static Ket<TRealNumber> AdditiveInverse(Ket<TRealNumber> ket) =>
        Create(ColumnVector<TRealNumber>.AdditiveInverse(ket.Components));

    public static Ket<TRealNumber> operator -(Ket<TRealNumber> ket) =>
        Create(ColumnVector<TRealNumber>.AdditiveInverse(ket.Components));

    public static Ket<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar, Ket<TRealNumber> ket) =>
        Create(ColumnVector<TRealNumber>.Multiply(scalar, ket.Components));

    public static Ket<TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, Ket<TRealNumber> ket) =>
        Create(ColumnVector<TRealNumber>.Multiply(scalar, ket.Components));

    public static ComplexNumber<TRealNumber> Multiply(Bra<TRealNumber> bra, Ket<TRealNumber> ket) =>
        RowVector<TRealNumber>.Multiply(bra.Components, ket.Components);

    public static ComplexNumber<TRealNumber> operator *(Bra<TRealNumber> bra, Ket<TRealNumber> ket) =>
        RowVector<TRealNumber>.Multiply(bra.Components, ket.Components);

    public static ComplexNumber<TRealNumber> InnerProduct(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        ColumnVector<TRealNumber>.InnerProduct(left.Components, right.Components);

    public static Ket<TRealNumber> TensorProduct(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        Create(ColumnVector<TRealNumber>.TensorProduct(left.Components, right.Components));

    public static ComplexNumber<TRealNumber> operator *(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        ColumnVector<TRealNumber>.InnerProduct(left.Components, right.Components);

    public static Bra<TRealNumber> Bra(Ket<TRealNumber> ket) =>
        Bra<TRealNumber>.Create(ColumnVector<TRealNumber>.Adjoint(ket.Components));

    public static TRealNumber Norm(Ket<TRealNumber> ket) =>
        ColumnVector<TRealNumber>.Norm(ket.Components);

    public static TRealNumber Distance(Ket<TRealNumber> left, Ket<TRealNumber> right) =>
        ColumnVector<TRealNumber>.Distance(left.Components, right.Components);

    public static Ket<TRealNumber> Normalized(Ket<TRealNumber> ket) =>
        Create(ColumnVector<TRealNumber>.Normalized(ket.Components));

    public static Ket<TRealNumber> Conjucate(Ket<TRealNumber> ket) =>
        Create(ColumnVector<TRealNumber>.Conjucate(ket.Components));
}
