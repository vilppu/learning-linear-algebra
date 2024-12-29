using System.Numerics;

namespace Computation.Matrices.Real;

public interface IBoxedRowVector<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public RowVector<TRealNumber> RowVector() => RowVector<TRealNumber>.U(this);
    public TRealNumber[] Entries { get; }
    public TRealNumber this[int index] { get; }
    public bool IsEquivalentTo(IBoxedRowVector<TRealNumber> right);
    public TRealNumber InnerProduct(IBoxedRowVector<TRealNumber> right);
    public TRealNumber Multiply(IBoxedColumnVector<TRealNumber> right);
    public TRealNumber Sum();
    public IBoxedColumnVector<TRealNumber> Transpose();
    public int Length();
    public IBoxedRowVector<TRealNumber> Add(IBoxedRowVector<TRealNumber> right);
    public IBoxedRowVector<TRealNumber> AdditiveInverse();
    public IBoxedRowVector<TRealNumber> Map(Func<TRealNumber, TRealNumber> elementMapping);
    public IBoxedRowVector<TRealNumber> Multiply(TRealNumber scalar);
    public IBoxedRowVector<TRealNumber> Normalized();
    public IBoxedRowVector<TRealNumber> Orthonormal();
    public IBoxedRowVector<TRealNumber> Round();
    public IBoxedRowVector<TRealNumber> Subtract(IBoxedRowVector<TRealNumber> right);
    public IBoxedRowVector<TRealNumber> TensorProduct(IBoxedRowVector<TRealNumber> right);
    public IBoxedRowVector<TRealNumber> Zip(IBoxedRowVector<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping);
    public TRealNumber Distance(IBoxedRowVector<TRealNumber> right);
    public TRealNumber Norm();
}

record BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>(TRowVector RowVector)
    : IBoxedRowVector<TRealNumber>
    where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static TRowVector Unbox(IBoxedRowVector<TRealNumber> boxedSquareMatrix) =>
        ((BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)boxedSquareMatrix).RowVector;

    public TRealNumber[] Entries =>
        RowVector.Entries;

    public TRealNumber this[int index] => RowVector[index];

    public bool IsEquivalentTo(IBoxedRowVector<TRealNumber> right) =>
        RowVector.IsEquivalentTo(Unbox(right));

    public TRealNumber InnerProduct(IBoxedRowVector<TRealNumber> right) =>
        RowVector.InnerProduct(Unbox(right));

    public TRealNumber Multiply(IBoxedColumnVector<TRealNumber> right) =>
        RowVector.Multiply(((BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)right).ColumnVector);

    public TRealNumber Sum() =>
        RowVector.Sum();

    public IBoxedColumnVector<TRealNumber> Transpose() =>
        BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>.V(RowVector.Transpose());

    public int Length() =>
        RowVector.Length();

    public IBoxedRowVector<TRealNumber> Add(IBoxedRowVector<TRealNumber> right) =>
        U(RowVector.Add(Unbox(right)));

    public IBoxedRowVector<TRealNumber> AdditiveInverse() =>
        U(RowVector.AdditiveInverse());

    public IBoxedRowVector<TRealNumber> Map(Func<TRealNumber, TRealNumber> elementMapping) =>
        U(RowVector.Map(elementMapping));

    public IBoxedRowVector<TRealNumber> Multiply(TRealNumber scalar) =>
        U(scalar.Multiply(RowVector));

    public IBoxedRowVector<TRealNumber> Normalized() =>
        U(RowVector.Normalized());

    public IBoxedRowVector<TRealNumber> Orthonormal() =>
        U(RowVector.Orthonormal());

    public IBoxedRowVector<TRealNumber> Round() =>
        U(RowVector.Round());

    public IBoxedRowVector<TRealNumber> Subtract(IBoxedRowVector<TRealNumber> right) =>
        U(RowVector.Subtract(Unbox(right)));

    public IBoxedRowVector<TRealNumber> TensorProduct(IBoxedRowVector<TRealNumber> right) =>
        U(RowVector.TensorProduct(Unbox(right)));

    public IBoxedRowVector<TRealNumber> Zip(IBoxedRowVector<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
        U(RowVector.Zip(((BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)second).RowVector, elementMapping));

    public TRealNumber Distance(IBoxedRowVector<TRealNumber> right) =>
        RowVector.Distance(Unbox(right));

    public TRealNumber Norm() =>
        RowVector.Norm();

    public static BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber> U(TRowVector managed) =>
        new(managed);

    public static IBoxedRowVector<TRealNumber> U(TRealNumber[] entries) =>
        U(TRowVector.U(entries));

    public static IBoxedRowVector<TRealNumber> U(int[] entries) =>
        U(TRowVector.U(entries));

    public static IBoxedRowVector<TRealNumber> U(float[] entries) =>
        U(TRowVector.U(entries));

    public static IBoxedRowVector<TRealNumber> U(double[] entries) =>
        U(TRowVector.U(entries));

    public static IBoxedRowVector<TRealNumber> U(IEnumerable<TRealNumber> entries) =>
        U(TRowVector.U(entries));

    public static IBoxedRowVector<TRealNumber> U(int length, Func<int, TRealNumber> initializer) =>
        U(TRowVector.U(length, initializer));

    public static IBoxedRowVector<TRealNumber> Zero(int length) =>
        U(TRowVector.Zero(length));
}