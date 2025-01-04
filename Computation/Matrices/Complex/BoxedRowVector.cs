using System.Numerics;
using Computation.Numbers;

namespace Computation.Matrices.Complex;

public interface IBoxedRowVector<TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public RowVector<TRealNumber> RowVector() => RowVector<TRealNumber>.U(this);
    public ComplexNumber<TRealNumber>[] Entries { get; }
    public ComplexNumber<TRealNumber> this[int index] { get; }
    public ComplexNumber<TRealNumber> InnerProduct(IBoxedRowVector<TRealNumber> right);
    public ComplexNumber<TRealNumber> Multiply(IBoxedColumnVector<TRealNumber> right);
    public ComplexNumber<TRealNumber> Sum();
    public IBoxedColumnVector<TRealNumber> Adjoint();
    public IBoxedColumnVector<TRealNumber> Transpose();
    public int Length();
    public IBoxedRowVector<TRealNumber> Add(IBoxedRowVector<TRealNumber> right);
    public IBoxedRowVector<TRealNumber> AdditiveInverse();
    public IBoxedRowVector<TRealNumber> Conjucate();
    public IBoxedRowVector<TRealNumber> Map(Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
    public IBoxedRowVector<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar);
    public IBoxedRowVector<TRealNumber> Multiply(TRealNumber scalar);
    public IBoxedRowVector<TRealNumber> Normalized();
    public IBoxedRowVector<TRealNumber> Orthonormal();
    public IBoxedRowVector<TRealNumber> Round();
    public IBoxedRowVector<TRealNumber> Subtract(IBoxedRowVector<TRealNumber> right);
    public IBoxedRowVector<TRealNumber> TensorProduct(IBoxedRowVector<TRealNumber> right);
    public IBoxedRowVector<TRealNumber> Zip(IBoxedRowVector<TRealNumber> second, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
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
    public virtual bool Equals(BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>? other) =>
        other != null && RowVector.Equals(other.RowVector);

    public override int GetHashCode() =>
        RowVector.GetHashCode();

    public static TRowVector Unbox(IBoxedRowVector<TRealNumber> boxedSquareMatrix) =>
        ((BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)boxedSquareMatrix).RowVector;

    public ComplexNumber<TRealNumber>[] Entries =>
        RowVector.Entries;

    public ComplexNumber<TRealNumber> this[int index] => RowVector[index];

    public ComplexNumber<TRealNumber> InnerProduct(IBoxedRowVector<TRealNumber> right) =>
        RowVector.InnerProduct(Unbox(right));

    public ComplexNumber<TRealNumber> Multiply(IBoxedColumnVector<TRealNumber> right) =>
        RowVector.Multiply(((BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)right).ColumnVector);

    public ComplexNumber<TRealNumber> Sum() =>
        RowVector.Sum();

    public IBoxedColumnVector<TRealNumber> Adjoint() =>
        BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>.V(RowVector.Adjoint());

    public IBoxedColumnVector<TRealNumber> Transpose() =>
        BoxedColumnVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>.V(RowVector.Transpose());

    public int Length() =>
        RowVector.Length();

    public IBoxedRowVector<TRealNumber> Add(IBoxedRowVector<TRealNumber> right) =>
        U(RowVector.Add(Unbox(right)));

    public IBoxedRowVector<TRealNumber> AdditiveInverse() =>
        U(RowVector.AdditiveInverse());

    public IBoxedRowVector<TRealNumber> Conjucate() =>
        U(RowVector.Conjucate());

    public IBoxedRowVector<TRealNumber> Map(Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
        U(RowVector.Map(elementMapping));

    public IBoxedRowVector<TRealNumber> Multiply(ComplexNumber<TRealNumber> scalar) =>
        U(scalar.Multiply(RowVector));

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

    public IBoxedRowVector<TRealNumber> Zip(IBoxedRowVector<TRealNumber> second, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping) =>
        U(RowVector.Zip(((BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber>)second).RowVector, elementMapping));

    public TRealNumber Distance(IBoxedRowVector<TRealNumber> right) =>
        RowVector.Distance(Unbox(right));

    public TRealNumber Norm() =>
        RowVector.Norm();

    public static BoxedRowVector<TSquareMatrix, TRowVector, TColumnVector, TRealNumber> U(TRowVector managed) =>
        new(managed);

    public static IBoxedRowVector<TRealNumber> U(ComplexNumber<TRealNumber>[] entries) =>
        U(TRowVector.U(entries));

    public static IBoxedRowVector<TRealNumber> U(IEnumerable<ComplexNumber<TRealNumber>> entries) =>
        U(TRowVector.U(entries));

    public static IBoxedRowVector<TRealNumber> U(int length, Func<int, ComplexNumber<TRealNumber>> initializer) =>
        U(TRowVector.U(length, initializer));

    public static IBoxedRowVector<TRealNumber> Zero(int length) =>
        U(TRowVector.Zero(length));
}