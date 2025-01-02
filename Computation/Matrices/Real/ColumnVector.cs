using System.Numerics;

namespace Computation.Matrices.Real;

public interface IColumnVector<TSelf, out TRowVector, TRealNumber> :
    IAddition<TSelf>,
    ICanBeNormalized<TSelf, TRealNumber>,
    ICanBeRounded<TSelf>,
    IDistance<TSelf, TRealNumber>,
    IEquality<TSelf>,
    IHasInverse<TSelf>,
    IHasLength<TSelf>,
    IHasNorm<TSelf, TRealNumber>,
    IHasRowVectorTranspose<TSelf, TRowVector, TRealNumber>,
    IHasVectorEntries<TSelf, TRealNumber>,
    IInnerProduct<TSelf, TRealNumber>,
    IOneDimensionalMap<TSelf, TRealNumber>,
    IOneDimensionalZip<TSelf, TRealNumber>,
    IOrthonormalization<TSelf>,
    IScalarMultiplication<TSelf, TRealNumber>,
    ISubtraction<TSelf>,
    ISum<TSelf, TRealNumber>,
    ITensorProduct<TSelf>

    where TSelf : IColumnVector<TSelf, TRowVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf V(TRealNumber[] entries);
    public static abstract TSelf V(int[] entries);
    public static abstract TSelf V(float[] entries);
    public static abstract TSelf V(double[] entries);
    public static abstract TSelf V(IEnumerable<TRealNumber> entries);
    public static abstract TSelf V(int length, Func<int, TRealNumber> initializer);
    public static abstract TSelf Zero(int length);
}

public record ColumnVector<TRealNumber>(IBoxedColumnVector<TRealNumber> BoxedColumnVector) :
    IEquality<ColumnVector<TRealNumber>>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static bool AreEquivalent(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.IsEquivalentTo(right);

    public TRealNumber[] Entries => BoxedColumnVector.Entries;

    public TRealNumber this[int index] => BoxedColumnVector[index];

    public bool IsEquivalentTo(ColumnVector<TRealNumber> right) =>
        BoxedColumnVector.IsEquivalentTo(right.BoxedColumnVector);

    public TRealNumber InnerProduct(ColumnVector<TRealNumber> right) =>
        BoxedColumnVector.InnerProduct(right.BoxedColumnVector);

    public TRealNumber Sum() =>
        BoxedColumnVector.Sum();

    public ColumnVector<TRealNumber> Add(ColumnVector<TRealNumber> right) =>
        V(BoxedColumnVector.Add(right.BoxedColumnVector));

    public ColumnVector<TRealNumber> AdditiveInverse() =>
        V(BoxedColumnVector.AdditiveInverse());

    public ColumnVector<TRealNumber> Map(ColumnVector<TRealNumber> source, Func<TRealNumber, TRealNumber> elementMapping) =>
        V(BoxedColumnVector.Map(source.BoxedColumnVector, elementMapping));

    public ColumnVector<TRealNumber> Multiply(TRealNumber scalar) =>
        V(BoxedColumnVector.Multiply(scalar));

    public ColumnVector<TRealNumber> Normalized() =>
        V(BoxedColumnVector.Normalized());

    public ColumnVector<TRealNumber> Orthonormal() =>
        V(BoxedColumnVector.Orthonormal());

    public ColumnVector<TRealNumber> Round() =>
        V(BoxedColumnVector.Round());

    public ColumnVector<TRealNumber> Subtract(ColumnVector<TRealNumber> right) =>
        V(BoxedColumnVector.Subtract(right.BoxedColumnVector));

    public ColumnVector<TRealNumber> TensorProduct(ColumnVector<TRealNumber> right) =>
        V(BoxedColumnVector.TensorProduct(right.BoxedColumnVector));

    public ColumnVector<TRealNumber> Zip(ColumnVector<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
        V(BoxedColumnVector.Zip(second.BoxedColumnVector, elementMapping));

    public int Length() =>
        BoxedColumnVector.Length();

    public RowVector<TRealNumber> Transpose() =>
        RowVector<TRealNumber>.U(BoxedColumnVector.Transpose());

    public TRealNumber Distance(ColumnVector<TRealNumber> right) =>
        BoxedColumnVector.Distance(right.BoxedColumnVector);

    public TRealNumber Norm() =>
        BoxedColumnVector.Norm();

    public static ColumnVector<TRealNumber> V(IBoxedColumnVector<TRealNumber> vector) =>
        new(vector);

    public static ColumnVector<TRealNumber> operator +(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Add(right);

    public static ColumnVector<TRealNumber> operator -(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Subtract(right);

    public static ColumnVector<TRealNumber> operator -(ColumnVector<TRealNumber> self) =>
        self.AdditiveInverse();

    public static TRealNumber operator *(ColumnVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.InnerProduct(right);

    public static TRealNumber operator *(RowVector<TRealNumber> left, ColumnVector<TRealNumber> right) =>
        left.Multiply(right);

    public static ColumnVector<TRealNumber> operator *(TRealNumber scalar, ColumnVector<TRealNumber> self) =>
        self.Multiply(scalar);
}