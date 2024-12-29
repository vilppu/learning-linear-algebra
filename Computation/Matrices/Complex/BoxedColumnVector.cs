using System.Numerics;
using Computation.Numbers;

namespace Computation.Matrices.Complex;

public interface IBoxedColumnVector<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public ColumnVector<TRealNumber> ColumnVector() => ColumnVector<TRealNumber>.V(this);
    public ComplexNumber<TRealNumber>[] Entries { get; }
    public ComplexNumber<TRealNumber> this[int index] { get; }
    public bool IsEquivalentTo(IBoxedColumnVector<TRealNumber> right);
    public ComplexNumber<TRealNumber> InnerProduct(IBoxedColumnVector<TRealNumber> right);
    public ComplexNumber<TRealNumber> Multiply(IBoxedRowVector<TRealNumber> right);
    public ComplexNumber<TRealNumber> Sum();
    public IBoxedColumnVector<TRealNumber> Add(IBoxedColumnVector<TRealNumber> right);
    public IBoxedColumnVector<TRealNumber> AdditiveInverse();
    public IBoxedColumnVector<TRealNumber> Conjucate();
    public IBoxedColumnVector<TRealNumber> Map(IBoxedColumnVector<TRealNumber> source, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
    public IBoxedColumnVector<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar);
    public IBoxedColumnVector<TRealNumber> Multiply(TRealNumber scalar);
    public IBoxedColumnVector<TRealNumber> Normalized();
    public IBoxedColumnVector<TRealNumber> Orthonormal();
    public IBoxedColumnVector<TRealNumber> Round();
    public IBoxedColumnVector<TRealNumber> Subtract(IBoxedColumnVector<TRealNumber> right);
    public IBoxedColumnVector<TRealNumber> TensorProduct(IBoxedColumnVector<TRealNumber> right);
    public IBoxedColumnVector<TRealNumber> Zip(IBoxedColumnVector<TRealNumber> second, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
    public int Length();
    public IBoxedRowVector<TRealNumber> Adjoint();
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
    public static TColumnVector Unbox(IBoxedColumnVector<TRealNumber> boxedSquareMatrix) =>
        ((BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)boxedSquareMatrix).ColumnVector;

    public ComplexNumber<TRealNumber>[] Entries =>
        ColumnVector.Entries;

    public ComplexNumber<TRealNumber> this[int index] => ColumnVector[index];

    public bool IsEquivalentTo(IBoxedColumnVector<TRealNumber> right) =>
        ColumnVector.IsEquivalentTo(Unbox(right));

    public ComplexNumber<TRealNumber> InnerProduct(IBoxedColumnVector<TRealNumber> right) =>
        ColumnVector.InnerProduct(Unbox(right));

    public ComplexNumber<TRealNumber> Multiply(IBoxedRowVector<TRealNumber> right) =>
        ((BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)right).RowVector.Multiply(ColumnVector);

    public ComplexNumber<TRealNumber> Sum() =>
        ColumnVector.Sum();

    public IBoxedColumnVector<TRealNumber> Add(IBoxedColumnVector<TRealNumber> right) =>
        V(ColumnVector.Add(Unbox(right)));

    public IBoxedColumnVector<TRealNumber> AdditiveInverse() =>
        V(ColumnVector.AdditiveInverse());

    public IBoxedColumnVector<TRealNumber> Conjucate() =>
        V(ColumnVector.Conjucate());

    public IBoxedColumnVector<TRealNumber> Map(IBoxedColumnVector<TRealNumber> source, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
        V(ColumnVector.Map(elementMapping));

    public IBoxedColumnVector<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar) =>
        V(scalar.Multiply(ColumnVector));

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

    public IBoxedColumnVector<TRealNumber> Zip(IBoxedColumnVector<TRealNumber> second, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
        V(ColumnVector.Zip(((BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)second).ColumnVector, elementMapping));

    public int Length() =>
        ColumnVector.Length();

    public IBoxedRowVector<TRealNumber> Adjoint() =>
        BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>.U(ColumnVector.Adjoint());

    public IBoxedRowVector<TRealNumber> Transpose() =>
        BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>.U(ColumnVector.Transpose());

    public TRealNumber Distance(IBoxedColumnVector<TRealNumber> right) =>
        ColumnVector.Distance(Unbox(right));

    public TRealNumber Norm() =>
        ColumnVector.Norm();

    public static BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber> V(TColumnVector managed) =>
        new(managed);

    public static IBoxedColumnVector<TRealNumber> V(ComplexNumber<TRealNumber>[] entries) =>
        V(TColumnVector.V(entries));

    public static IBoxedColumnVector<TRealNumber> V(IEnumerable<ComplexNumber<TRealNumber>> entries) =>
        V(TColumnVector.V(entries));

    public static IBoxedColumnVector<TRealNumber> V(int length, Func<int, ComplexNumber<TRealNumber>> initializer) =>
        V(TColumnVector.V(length, initializer));

    public static IBoxedColumnVector<TRealNumber> Zero(int length) =>
        V(TColumnVector.Zero(length));
}