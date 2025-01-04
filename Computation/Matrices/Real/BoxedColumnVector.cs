using System.Numerics;

namespace Computation.Matrices.Real;

public interface IBoxedColumnVector<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public ColumnVector<TRealNumber> ColumnVector() => ColumnVector<TRealNumber>.V(this);
    public TRealNumber[] Entries { get; }
    public TRealNumber this[int index] { get; }
    public TRealNumber InnerProduct(IBoxedColumnVector<TRealNumber> right);
    public TRealNumber Multiply(IBoxedRowVector<TRealNumber> right);
    public TRealNumber Sum();
    public IBoxedColumnVector<TRealNumber> Add(IBoxedColumnVector<TRealNumber> right);
    public IBoxedColumnVector<TRealNumber> AdditiveInverse();
    public IBoxedColumnVector<TRealNumber> Map(IBoxedColumnVector<TRealNumber> source, Func<TRealNumber, TRealNumber> elementMapping);
    public IBoxedColumnVector<TRealNumber> Multiply(TRealNumber scalar);
    public IBoxedColumnVector<TRealNumber> Normalized();
    public IBoxedColumnVector<TRealNumber> Orthonormal();
    public IBoxedColumnVector<TRealNumber> Round();
    public IBoxedColumnVector<TRealNumber> Subtract(IBoxedColumnVector<TRealNumber> right);
    public IBoxedColumnVector<TRealNumber> TensorProduct(IBoxedColumnVector<TRealNumber> right);
    public IBoxedColumnVector<TRealNumber> Zip(IBoxedColumnVector<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping);
    public int Length();
    public IBoxedRowVector<TRealNumber> Transpose();
    public TRealNumber Distance(IBoxedColumnVector<TRealNumber> right);
    public TRealNumber Norm();
}

record BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>(TColumnVector ColumnVector)
    : IBoxedColumnVector<TRealNumber>
    where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>? other) =>
        other != null && ColumnVector.Equals(other.ColumnVector);

    public override int GetHashCode() =>
        ColumnVector.GetHashCode();

    public static TColumnVector Unbox(IBoxedColumnVector<TRealNumber> boxedSquareMatrix) =>
        ((BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)boxedSquareMatrix).ColumnVector;

    public TRealNumber[] Entries =>
        ColumnVector.Entries;

    public TRealNumber this[int index] => ColumnVector[index];

    public TRealNumber InnerProduct(IBoxedColumnVector<TRealNumber> right) =>
        ColumnVector.InnerProduct(Unbox(right));

    public TRealNumber Multiply(IBoxedRowVector<TRealNumber> right) =>
        ((BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)right).RowVector.Multiply(ColumnVector);

    public TRealNumber Sum() =>
        ColumnVector.Sum();

    public IBoxedColumnVector<TRealNumber> Add(IBoxedColumnVector<TRealNumber> right) =>
        V(ColumnVector.Add(Unbox(right)));

    public IBoxedColumnVector<TRealNumber> AdditiveInverse() =>
        V(ColumnVector.AdditiveInverse());

    public IBoxedColumnVector<TRealNumber> Map(IBoxedColumnVector<TRealNumber> source, Func<TRealNumber, TRealNumber> elementMapping) =>
        V(ColumnVector.Map(elementMapping));

    public IBoxedColumnVector<TRealNumber> Multiply(TRealNumber scalar) =>
        V(scalar.Multiply(ColumnVector));

    public IBoxedColumnVector<TRealNumber> Normalized() =>
        V(ColumnVector.Normalized());

    public IBoxedColumnVector<TRealNumber> Orthonormal() =>
        V(ColumnVector.Orthonormal());

    public IBoxedColumnVector<TRealNumber> Round() =>
        V(ColumnVector.Round());

    public IBoxedColumnVector<TRealNumber> Subtract(IBoxedColumnVector<TRealNumber> right) =>
        V(ColumnVector.Subtract(Unbox(right)));

    public IBoxedColumnVector<TRealNumber> TensorProduct(IBoxedColumnVector<TRealNumber> right) =>
        V(ColumnVector.TensorProduct(Unbox(right)));

    public IBoxedColumnVector<TRealNumber> Zip(IBoxedColumnVector<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
        V(ColumnVector.Zip(((BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)second).ColumnVector, elementMapping));

    public int Length() =>
        ColumnVector.Length();

    public IBoxedRowVector<TRealNumber> Transpose() =>
        BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>.U(ColumnVector.Transpose());

    public TRealNumber Distance(IBoxedColumnVector<TRealNumber> right) =>
        ColumnVector.Distance(Unbox(right));

    public TRealNumber Norm() =>
        ColumnVector.Norm();

    public static BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber> V(TColumnVector managed) =>
        new(managed);

    public static IBoxedColumnVector<TRealNumber> V(double[] entries) =>
        V(TColumnVector.V(entries));

    public static IBoxedColumnVector<TRealNumber> V(float[] entries) =>
        V(TColumnVector.V(entries));

    public static IBoxedColumnVector<TRealNumber> V(int[] entries) =>
        V(TColumnVector.V(entries));

    public static IBoxedColumnVector<TRealNumber> V(TRealNumber[] entries) =>
        V(TColumnVector.V(entries));

    public static IBoxedColumnVector<TRealNumber> V(IEnumerable<TRealNumber> entries) =>
        V(TColumnVector.V(entries));

    public static IBoxedColumnVector<TRealNumber> V(int length, Func<int, TRealNumber> initializer) =>
        V(TColumnVector.V(length, initializer));

    public static IBoxedColumnVector<TRealNumber> Zero(int length) =>
        V(TColumnVector.Zero(length));
}