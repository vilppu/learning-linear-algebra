using System.Numerics;
using Computation.Numbers;

namespace Computation.Matrices.Complex;

public interface ISquareMatrix<TSelf, TRowVector, TColumnVector, TRealNumber> :
    IMatrix<TSelf, TRealNumber>,
    IAction<TSelf, TRowVector, TColumnVector, TRealNumber>,
    IAddition<TSelf>,
    IHasSquareMatrixAdjoint<TSelf>,
    ICanBeHermitian<TSelf>,
    ICanBeIdentity<TSelf>,
    ICanBeRounded<TSelf>,
    ICanBeUnitary<TSelf>,
    IEquality<TSelf>,
    IHasColumns<TSelf, TRealNumber>,
    IHasColumns<TSelf>,
    IHasCommutator<TSelf>,
    IHasConjucate<TSelf>,
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
    public static abstract TSelf M(ComplexNumber<TRealNumber>[,] entries);
    public static abstract TSelf M(ComplexNumber<float>[,] entries);
    public static abstract TSelf M(ComplexNumber<double>[,] entries);
    public static abstract TSelf M(int m, Func<int, int, ComplexNumber<TRealNumber>> initializer);
    public static abstract TSelf Zero(int m);
    public static abstract TSelf Identity(int m);
}

public record SquareMatrix<TRealNumber>(IBoxedSquareMatrix<TRealNumber> BoxedSquareMatrix) :
    IEquality<SquareMatrix<TRealNumber>>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static bool AreEquivalent(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.IsEquivalentTo(right);

    public ComplexNumber<TRealNumber>[,] Entries => BoxedSquareMatrix.Entries;

    public ComplexNumber<TRealNumber> this[int i, int j] => BoxedSquareMatrix[i, j];

    public bool IsEquivalentTo(SquareMatrix<TRealNumber> matrix) =>
        BoxedSquareMatrix.IsEquivalentTo(matrix.BoxedSquareMatrix);

    public bool IsHermitian() =>
        BoxedSquareMatrix.IsHermitian();

    public bool IsIdentity() =>
        BoxedSquareMatrix.IsIdentity();

    public bool IsUnitary() =>
        BoxedSquareMatrix.IsUnitary();

    public IEnumerable<ComplexNumber<TRealNumber>> Column(int j) =>
        BoxedSquareMatrix.Column(j);

    public IEnumerable<ComplexNumber<TRealNumber>> Row(int i) =>
        BoxedSquareMatrix.Row(i);

    public int M() =>
        BoxedSquareMatrix.M();

    public int N() =>
        BoxedSquareMatrix.N();

    public SquareMatrix<TRealNumber> Add(SquareMatrix<TRealNumber> right) =>
        M(BoxedSquareMatrix.Add(right.BoxedSquareMatrix));

    public SquareMatrix<TRealNumber> AdditiveInverse() =>
        M(BoxedSquareMatrix.AdditiveInverse());

    public SquareMatrix<TRealNumber> Adjoint() =>
        M(BoxedSquareMatrix.Adjoint());

    public SquareMatrix<TRealNumber> Commutator(SquareMatrix<TRealNumber> right) =>
        M(BoxedSquareMatrix.Commutator(right.BoxedSquareMatrix));

    public SquareMatrix<TRealNumber> Conjucate() =>
        M(BoxedSquareMatrix.Conjucate());

    public SquareMatrix<TRealNumber> Map(Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
        M(BoxedSquareMatrix.Map(elementMapping));

    public SquareMatrix<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar) =>
        M(BoxedSquareMatrix.Multiply(scalar));

    public SquareMatrix<TRealNumber> Multiply(SquareMatrix<TRealNumber> right) =>
        M(BoxedSquareMatrix.Multiply(right.BoxedSquareMatrix));

    public SquareMatrix<TRealNumber> Multiply(TRealNumber scalar) =>
        M(BoxedSquareMatrix.Multiply(scalar));

    public SquareMatrix<TRealNumber> Round() =>
        M(BoxedSquareMatrix.Round());

    public SquareMatrix<TRealNumber> Subtract(SquareMatrix<TRealNumber> right) =>
        M(BoxedSquareMatrix.Subtract(right.BoxedSquareMatrix));

    public SquareMatrix<TRealNumber> TensorProduct(SquareMatrix<TRealNumber> right) =>
        M(BoxedSquareMatrix.TensorProduct(right.BoxedSquareMatrix));

    public SquareMatrix<TRealNumber> Transpose() =>
        M(BoxedSquareMatrix.Transpose());

    public SquareMatrix<TRealNumber> Zip(SquareMatrix<TRealNumber> second, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
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

    public static SquareMatrix<TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, SquareMatrix<TRealNumber> self) =>
        self.Multiply(scalar);

    public static SquareMatrix<TRealNumber> operator *(TRealNumber scalar, SquareMatrix<TRealNumber> self) =>
        self.Multiply(scalar);

    public static SquareMatrix<TRealNumber> operator *(SquareMatrix<TRealNumber> left, SquareMatrix<TRealNumber> right) =>
        left.Multiply(right);

    public static ColumnVector<TRealNumber> operator *(SquareMatrix<TRealNumber> self, ColumnVector<TRealNumber> vector) =>
        self.Act(vector);

    public static RowVector<TRealNumber> operator *(RowVector<TRealNumber> vector, SquareMatrix<TRealNumber> self) =>
        self.Act(vector);
}