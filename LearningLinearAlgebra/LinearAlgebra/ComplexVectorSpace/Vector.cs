using LearningLinearAlgebra.Numbers;
using System.Numerics;

namespace LearningLinearAlgebra.LinearAlgebra.ComplexVectorSpace;

public interface IKet<TSelf, TBra, TRealNumber>
    where TSelf : IKet<TSelf, TBra, TRealNumber>
    where TBra : IBra<TBra, TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public abstract static TSelf V(ComplexNumber<TRealNumber>[] components);

    public abstract static int Dimension(TSelf ket);

    public abstract static TSelf Zero(int dimension);

    public abstract static TSelf Add(TSelf left, TSelf right);
    public abstract static TSelf operator +(TSelf left, TSelf right);

    public abstract static TSelf Subtract(TSelf left, TSelf right);
    public abstract static TSelf operator -(TSelf left, TSelf right);

    public abstract static TSelf AdditiveInverse(TSelf ket);
    public abstract static TSelf operator -(TSelf ket);

    public abstract static TSelf Multiply(ComplexNumber<TRealNumber> scalar, TSelf ket);
    public abstract static TSelf operator *(ComplexNumber<TRealNumber> scalar, TSelf ket);

    public abstract static ComplexNumber<TRealNumber> Multiply(TBra bra, TSelf ket);
    public abstract static ComplexNumber<TRealNumber> operator *(TBra bra, TSelf ket);

    public abstract static ComplexNumber<TRealNumber> InnerProduct(TSelf left, TSelf right);
    public abstract static ComplexNumber<TRealNumber> operator *(TSelf left, TSelf right);

    public abstract static TSelf TensorProduct(TSelf left, TSelf right);

    public abstract static TBra Bra(TSelf ket);

    public abstract static TRealNumber Norm(TSelf ket);
    public abstract static TRealNumber Distance(TSelf left, TSelf right);
    public abstract static TSelf Normalized(TSelf ket);
    public abstract static TSelf Conjucate(TSelf ket);
}

public interface IBra<TSelf, TKet, TRealNumber>
    where TSelf : IBra<TSelf, TKet, TRealNumber>
    where TKet : IKet<TKet, TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public abstract static TSelf U(ComplexNumber<TRealNumber>[] components);

    public abstract static TSelf Zero(int dimension);

    public abstract static int Dimension(TSelf bra);

    public abstract static TSelf Add(TSelf left, TSelf right);
    public abstract static TSelf operator +(TSelf left, TSelf right);

    public abstract static TSelf Subtract(TSelf left, TSelf right);
    public abstract static TSelf operator -(TSelf left, TSelf right);

    public abstract static TSelf AdditiveInverse(TSelf bra);
    public abstract static TSelf operator -(TSelf bra);

    public abstract static TSelf Multiply(ComplexNumber<TRealNumber> scalar, TSelf bra);
    public abstract static TSelf operator *(ComplexNumber<TRealNumber> scalar, TSelf bra);

    public abstract static ComplexNumber<TRealNumber> InnerProduct(TSelf left, TSelf right);
    public abstract static ComplexNumber<TRealNumber> operator *(TSelf left, TSelf right);

    public abstract static TSelf TensorProduct(TSelf left, TSelf right);

    public abstract static TKet Ket(TSelf bra);

    public abstract static TRealNumber Norm(TSelf bra);
    public abstract static TRealNumber Distance(TSelf left, TSelf right);
    public abstract static TSelf Normalized(TSelf bra);
    public abstract static TSelf Conjucate(TSelf ket);
}

public interface IOperator<TSelf, TKet, TBra, TRealNumber>
    where TSelf : IOperator<TSelf, TKet, TBra, TRealNumber>
    where TKet : IKet<TKet, TBra, TRealNumber>
    where TBra : IBra<TBra, TKet, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public abstract static TSelf M(ComplexNumber<TRealNumber>[,] components);
    public abstract static TSelf M(ComplexNumber<float>[,] components);

    public abstract static TSelf Identity(int dimension);
    public abstract static TSelf Zero(int dimension);

    public abstract static int Dimension(TSelf @operator);

    public abstract static TSelf Add(TSelf left, TSelf right);
    public abstract static TSelf operator +(TSelf left, TSelf right);

    public abstract static TSelf Subtract(TSelf left, TSelf right);
    public abstract static TSelf operator -(TSelf left, TSelf right);

    public abstract static TSelf AdditiveInverse(TSelf bra);
    public abstract static TSelf operator -(TSelf bra);

    public abstract static TSelf Multiply(ComplexNumber<TRealNumber> scalar, TSelf @operator);
    public abstract static TSelf operator *(ComplexNumber<TRealNumber> scalar, TSelf @operator);

    public abstract static TSelf Multiply(TSelf left, TSelf right);
    public abstract static TSelf operator *(TSelf left, TSelf right);

    public abstract static TSelf TensorProduct(TSelf left, TSelf right);

    public abstract static TSelf Commutator(TSelf left, TSelf right);

    public abstract static TKet Act(TSelf @operator, TKet ket);
    public abstract static TKet operator *(TSelf @operator, TKet ket);

    public abstract static TBra Act(TBra bra, TSelf @operator);
    public abstract static TBra operator *(TBra bra, TSelf @operator);

    public abstract static TSelf Conjucate(TSelf ket);
    public abstract static TSelf Adjoint(TSelf ket);
    public abstract static TSelf Round(TSelf ket);

    public abstract static bool IsIdentity(TSelf ket);
    public abstract static bool IsUnitary(TSelf ket);
    public abstract static bool IsHermitian(TSelf ket);
}