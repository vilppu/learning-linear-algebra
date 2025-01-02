using System.Numerics;
using Computation.Numbers;

namespace Computation.Matrices.Real;

public interface IBoxedSquareMatrix<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public SquareMatrix<TRealNumber> SquareMatrix() => SquareMatrix<TRealNumber>.M(this);
    public TRealNumber[,] Entries { get; }
    public TRealNumber this[int i, int j] { get; }
    public bool IsIdentity();
    public IEnumerable<TRealNumber> Column(int j);
    public IEnumerable<TRealNumber> Row(int i);
    public int M();
    public int N();
    public IBoxedSquareMatrix<TRealNumber> Add(IBoxedSquareMatrix<TRealNumber> right);
    public IBoxedSquareMatrix<TRealNumber> AdditiveInverse();
    public IBoxedSquareMatrix<TRealNumber> Commutator(IBoxedSquareMatrix<TRealNumber> right);
    public IBoxedSquareMatrix<TRealNumber> Map(Func<TRealNumber, TRealNumber> elementMapping);
    public IBoxedSquareMatrix<TRealNumber> Multiply(TRealNumber scalar);
    public IBoxedSquareMatrix<TRealNumber> Multiply(IBoxedSquareMatrix<TRealNumber> right);
    public IBoxedSquareMatrix<TRealNumber> Round();
    public IBoxedSquareMatrix<TRealNumber> Subtract(IBoxedSquareMatrix<TRealNumber> right);
    public IBoxedSquareMatrix<TRealNumber> TensorProduct(IBoxedSquareMatrix<TRealNumber> right);
    public IBoxedSquareMatrix<TRealNumber> Transpose();
    public IBoxedSquareMatrix<TRealNumber> Zip(IBoxedSquareMatrix<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping);
    public IBoxedColumnVector<TRealNumber> Act(IBoxedColumnVector<TRealNumber> vector);
    public IBoxedRowVector<TRealNumber> Act(IBoxedRowVector<TRealNumber> vector);

    public static abstract IBoxedSquareMatrix<TRealNumber> M(TRealNumber[,] entries);
    public static abstract IBoxedSquareMatrix<TRealNumber> M(float[,] entries);
    public static abstract IBoxedSquareMatrix<TRealNumber> M(double[,] entries);
    public static abstract IBoxedSquareMatrix<TRealNumber> M(int m, Func<int, int, TRealNumber> initializer);
    public static abstract IBoxedSquareMatrix<TRealNumber> Zero(int m);
    public static abstract IBoxedSquareMatrix<TRealNumber> Identity(int m);
}

record BoxedSquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>(TSquareMatrix SquareMatrix)
    : IBoxedSquareMatrix<TRealNumber>

    where TSquareMatrix : ISquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static TSquareMatrix Unbox(IBoxedSquareMatrix<TRealNumber> boxedSquareMatrix) =>
        ((BoxedSquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)boxedSquareMatrix).SquareMatrix;

    public TRealNumber[,] Entries =>
        SquareMatrix.Entries;

    public TRealNumber this[int i, int j] =>
        SquareMatrix[i, j];

    public bool IsIdentity() =>
        SquareMatrix.IsIdentity();

    public IEnumerable<TRealNumber> Column(int j) =>
        SquareMatrix.Column(j);

    public IEnumerable<TRealNumber> Row(int i) =>
        SquareMatrix.Row(i);

    public int M() => SquareMatrix.M();

    public int N() => SquareMatrix.N();

    public IBoxedSquareMatrix<TRealNumber> Add(IBoxedSquareMatrix<TRealNumber> right) =>
        M(SquareMatrix.Add(Unbox(right)));

    public IBoxedSquareMatrix<TRealNumber> AdditiveInverse() =>
        M(SquareMatrix.AdditiveInverse());
    public IBoxedSquareMatrix<TRealNumber> Commutator(IBoxedSquareMatrix<TRealNumber> right) =>
        M(SquareMatrix.Commutator(Unbox(right)));

    public IBoxedSquareMatrix<TRealNumber> Map(Func<TRealNumber, TRealNumber> elementMapping) =>
        M(SquareMatrix.Map(elementMapping));

    public IBoxedSquareMatrix<TRealNumber> Multiply(TRealNumber scalar) =>
        M(scalar.Multiply(SquareMatrix));

    public IBoxedSquareMatrix<TRealNumber> Multiply(IBoxedSquareMatrix<TRealNumber> right) =>
        M(SquareMatrix.Multiply(Unbox(right)));

    public IBoxedSquareMatrix<TRealNumber> Round() =>
        M(SquareMatrix.Round());

    public IBoxedSquareMatrix<TRealNumber> Subtract(IBoxedSquareMatrix<TRealNumber> right) =>
        M(SquareMatrix.Subtract(Unbox(right)));

    public IBoxedSquareMatrix<TRealNumber> TensorProduct(IBoxedSquareMatrix<TRealNumber> right) =>
        M(SquareMatrix.TensorProduct(Unbox(right)));

    public IBoxedSquareMatrix<TRealNumber> Transpose() =>
        M(SquareMatrix.Transpose());

    public IBoxedSquareMatrix<TRealNumber> Zip(IBoxedSquareMatrix<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
        M(SquareMatrix.Zip(((BoxedSquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)second).SquareMatrix, elementMapping));

    public IBoxedColumnVector<TRealNumber> Act(IBoxedColumnVector<TRealNumber> vector) =>
        BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>.V(SquareMatrix.Act(((BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)vector).ColumnVector));

    public IBoxedRowVector<TRealNumber> Act(IBoxedRowVector<TRealNumber> vector) =>
        BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>.U(((BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)vector).RowVector.Act(SquareMatrix));

    public static IBoxedSquareMatrix<TRealNumber> M(TSquareMatrix managed) =>
        new BoxedSquareMatrix<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>(managed);

    public static IBoxedSquareMatrix<TRealNumber> M(TRealNumber[,] entries) =>
        M(TSquareMatrix.M(entries));

    public static IBoxedSquareMatrix<TRealNumber> M(float[,] entries) =>
        M(TSquareMatrix.M(entries));

    public static IBoxedSquareMatrix<TRealNumber> M(double[,] entries) =>
        M(TSquareMatrix.M(entries));

    public static IBoxedSquareMatrix<TRealNumber> M(int m, Func<int, int, TRealNumber> initializer) =>
        M(TSquareMatrix.M(m, initializer));

    public static IBoxedSquareMatrix<TRealNumber> Zero(int m) =>
        M(TSquareMatrix.Zero(m));

    public static IBoxedSquareMatrix<TRealNumber> Identity(int m) =>
        M(TSquareMatrix.Identity(m));
}