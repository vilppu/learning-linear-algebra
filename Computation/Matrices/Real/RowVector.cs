using System.Numerics;

namespace Computation.Matrices.Real;

public interface IRowVector<TSelf, TColumnVector, TRealNumber> :
    IAddition<TSelf>,
    ICanBeNormalized<TSelf, TRealNumber>,
    ICanBeRounded<TSelf>,
    IDistance<TSelf, TRealNumber>,
    IEquality<TSelf>,
    IHasColumnVectorTranspose<TSelf, TColumnVector, TRealNumber>,
    IHasInverse<TSelf>,
    IHasLength<TSelf>,
    IHasNorm<TSelf, TRealNumber>,
    IHasVectorEntries<TSelf, TRealNumber>,
    IInnerProduct<TSelf, TRealNumber>,
    IOneDimensionalMap<TSelf, TRealNumber>,
    IOneDimensionalZip<TSelf, TRealNumber>,
    IOrthonormalization<TSelf>,
    IScalarMultiplication<TSelf, TRealNumber>,
    ISubtraction<TSelf>,
    ISum<TSelf, TRealNumber>,
    ITensorProduct<TSelf>,
    IVectorMultiplication<TSelf, TColumnVector, TRealNumber>

    where TSelf : IRowVector<TSelf, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf U(TRealNumber[] entries);
    public static abstract TSelf U(int[] entries);
    public static abstract TSelf U(float[] entries);
    public static abstract TSelf U(double[] entries);
    public static abstract TSelf U(IEnumerable<TRealNumber> entries);
    public static abstract TSelf U(int length, Func<int, TRealNumber> initializer);
    public static abstract TSelf Zero(int length);
}

public record RowVector<TRealNumber>(IBoxedRowVector<TRealNumber> BoxedRowVector) :
    IEquality<RowVector<TRealNumber>>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static bool AreEquivalent(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.IsEquivalentTo(right);

    public TRealNumber[] Entries => BoxedRowVector.Entries;

    public TRealNumber this[int index] => BoxedRowVector[index];

    public bool IsEquivalentTo(RowVector<TRealNumber> right) =>
        BoxedRowVector.IsEquivalentTo(right.BoxedRowVector);

    public TRealNumber InnerProduct(RowVector<TRealNumber> right) =>
        BoxedRowVector.InnerProduct(right.BoxedRowVector);

    public TRealNumber Multiply(ColumnVector<TRealNumber> right) =>
        BoxedRowVector.Multiply(right.BoxedColumnVector);

    public TRealNumber Sum() =>
        BoxedRowVector.Sum();

    public ColumnVector<TRealNumber> Transpose() =>
        ColumnVector<TRealNumber>.V(BoxedRowVector.Transpose());

    public int Length() =>
        BoxedRowVector.Length();

    public RowVector<TRealNumber> Add(RowVector<TRealNumber> right) =>
        U(BoxedRowVector.Add(right.BoxedRowVector));

    public RowVector<TRealNumber> AdditiveInverse() =>
        U(BoxedRowVector.AdditiveInverse());

    public RowVector<TRealNumber> Map(Func<TRealNumber, TRealNumber> elementMapping) =>
        U(BoxedRowVector.Map(elementMapping));

    public RowVector<TRealNumber> Multiply(TRealNumber scalar) =>
        U(BoxedRowVector.Multiply(scalar));

    public RowVector<TRealNumber> Normalized() =>
        U(BoxedRowVector.Normalized());

    public RowVector<TRealNumber> Orthonormal() =>
        U(BoxedRowVector.Orthonormal());

    public RowVector<TRealNumber> Round() =>
        U(BoxedRowVector.Round());

    public RowVector<TRealNumber> Subtract(RowVector<TRealNumber> right) =>
        U(BoxedRowVector.Subtract(right.BoxedRowVector));

    public RowVector<TRealNumber> TensorProduct(RowVector<TRealNumber> right) =>
        U(BoxedRowVector.TensorProduct(right.BoxedRowVector));

    public RowVector<TRealNumber> Zip(RowVector<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
        U(BoxedRowVector.Zip(second.BoxedRowVector, elementMapping));

    public TRealNumber Distance(RowVector<TRealNumber> right) =>
        BoxedRowVector.Distance(right.BoxedRowVector);

    public TRealNumber Norm() =>
        BoxedRowVector.Norm();

    public static RowVector<TRealNumber> U(IBoxedRowVector<TRealNumber> vector) =>
        new(vector);

    public static RowVector<TRealNumber> operator +(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.Add(right);

    public static RowVector<TRealNumber> operator -(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.Subtract(right);

    public static RowVector<TRealNumber> operator -(RowVector<TRealNumber> self) =>
        self.AdditiveInverse();

    public static TRealNumber operator *(RowVector<TRealNumber> left, RowVector<TRealNumber> right) =>
        left.InnerProduct(right);

    public static RowVector<TRealNumber> operator *(TRealNumber scalar, RowVector<TRealNumber> self) =>
        self.Multiply(scalar);
}