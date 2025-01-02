using System.Numerics;

namespace Computation.Matrices.Real;

public interface ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> :
    IAction<TSelf, TRowVector, TColumnVector, TRealNumber>,
    IAddition<TSelf>,
    ICanBeIdentity<TSelf>,
    ICanBeRounded<TSelf>, 
    IEquality<TSelf>,
    IHasColumns<TSelf, TRealNumber>,
    IHasColumns<TSelf>,
    IHasInverse<TSelf>,
    IHasMatrixEntries<TSelf, TRealNumber>,
    IHasRows<TSelf, TRealNumber>,
    IHasRows<TSelf>,
    IHasSquareMatrixTranspose<TSelf>,
    IMultiplication<TSelf>,
    IScalarMultiplication<TSelf, TRealNumber>,
    ISubtraction<TSelf>,
    ITensorProduct<TSelf>,
    ITwoDimensionalMap<TSelf, TRealNumber>,
    ITwoDimensionalZip<TSelf, TRealNumber>

    where TSelf : ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber>
    where TRowVector : IRowVector<TRowVector, TColumnVector, TRealNumber>
    where TColumnVector : IColumnVector<TColumnVector, TRowVector, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf M(TRealNumber[,] entries);
    public static abstract TSelf M(int[,] entries);
    public static abstract TSelf M(float[,] entries);
    public static abstract TSelf M(double[,] entries);
    public static abstract TSelf M(int m, Func<int, int, TRealNumber> initializer);
    public static abstract TSelf Zero(int m);
    public static abstract TSelf Identity(int m);
}

public record SquareMatrix<TRealNumber>(IBoxedSquareMatrix<TRealNumber> BoxedSquareMatrix) :
    IEquality<SquareMatrix<TRealNumber>>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static bool AreEquivalent(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.IsEquivalentTo(right);

    public TRealNumber[,] Entries => BoxedSquareMatrix.Entries;

    public TRealNumber this[int i, int j] => BoxedSquareMatrix[i, j];

    public bool IsEquivalentTo(SquareMatrix<TRealNumber> matrix) =>
        BoxedSquareMatrix.IsEquivalentTo(matrix.BoxedSquareMatrix);

    public bool IsIdentity() =>
        BoxedSquareMatrix.IsIdentity();

    public IEnumerable<TRealNumber> Column(int j) =>
        BoxedSquareMatrix.Column(j);

    public IEnumerable<TRealNumber> Row(int i) =>
        BoxedSquareMatrix.Row(i);

    public int M() =>
        BoxedSquareMatrix.M();

    public int N() =>
        BoxedSquareMatrix.N();

    public SquareMatrix<TRealNumber> Add(SquareMatrix<TRealNumber> right) =>
        M(BoxedSquareMatrix.Add(right.BoxedSquareMatrix));

    public SquareMatrix<TRealNumber> AdditiveInverse() =>
        M(BoxedSquareMatrix.AdditiveInverse());

    public SquareMatrix<TRealNumber> Map(Func<TRealNumber, TRealNumber> elementMapping) =>
        M(BoxedSquareMatrix.Map(elementMapping));

    public SquareMatrix<TRealNumber> Multiply(TRealNumber scalar) =>
        M(BoxedSquareMatrix.Multiply(scalar));

    public SquareMatrix<TRealNumber> Multiply(SquareMatrix<TRealNumber> right) =>
        M(BoxedSquareMatrix.Multiply(right.BoxedSquareMatrix));

    public SquareMatrix<TRealNumber> Round() =>
        M(BoxedSquareMatrix.Round());

    public SquareMatrix<TRealNumber> Subtract(SquareMatrix<TRealNumber> right) =>
        M(BoxedSquareMatrix.Subtract(right.BoxedSquareMatrix));

    public SquareMatrix<TRealNumber> TensorProduct(SquareMatrix<TRealNumber> right) =>
        M(BoxedSquareMatrix.TensorProduct(right.BoxedSquareMatrix));

    public SquareMatrix<TRealNumber> Transpose() =>
        M(BoxedSquareMatrix.Transpose());

    public SquareMatrix<TRealNumber> Zip(SquareMatrix<TRealNumber> second, Func<TRealNumber, TRealNumber, TRealNumber> elementMapping) =>
        M(BoxedSquareMatrix.Zip(second.BoxedSquareMatrix, elementMapping));

    public ColumnVector<TRealNumber> Act(ColumnVector<TRealNumber> vector) =>
        ColumnVector<TRealNumber>.V(BoxedSquareMatrix.Act(vector.BoxedColumnVector));

    public RowVector<TRealNumber> Act(RowVector<TRealNumber> vector) =>
        RowVector<TRealNumber>.U(BoxedSquareMatrix.Act(vector.BoxedRowVector));

    public static SquareMatrix<TRealNumber> M(IBoxedSquareMatrix<TRealNumber> squareMatrix) =>
        new(squareMatrix);

    public static SquareMatrix<TRealNumber> operator +(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.Add(right);

    public static SquareMatrix<TRealNumber> operator -(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.Subtract(right);

    public static SquareMatrix<TRealNumber> operator -(SquareMatrix<TRealNumber> self) =>
        self.AdditiveInverse();

    public static SquareMatrix<TRealNumber> operator *(TRealNumber scalar, SquareMatrix<TRealNumber> self) =>
        self.Multiply(scalar);

    public static SquareMatrix<TRealNumber> operator *(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.Multiply(right);

    public static ColumnVector<TRealNumber> operator *(SquareMatrix<TRealNumber> self, ColumnVector<TRealNumber> vector) =>
        self.Act(vector);

    public static RowVector<TRealNumber> operator *(RowVector<TRealNumber> vector, SquareMatrix<TRealNumber> self) =>
        self.Act(vector);
}