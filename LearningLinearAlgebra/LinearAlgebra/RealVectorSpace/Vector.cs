using System.Numerics;

namespace LearningLinearAlgebra.LinearAlgebra.RealVectorSpace;

public interface IKet<TSelf, TBra, TRealNumber>
    where TSelf : IKet<TSelf, TBra, TRealNumber>
    where TBra : IBra<TBra, TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf V(TRealNumber[] components);

    public static abstract int Dimension(TSelf ket);

    public static abstract TSelf Zero(int dimension);

    public static abstract TSelf Add(TSelf left, TSelf right);
    public static abstract TSelf operator +(TSelf left, TSelf right);

    public static abstract TSelf Subtract(TSelf left, TSelf right);
    public static abstract TSelf operator -(TSelf left, TSelf right);

    public static abstract TSelf AdditiveInverse(TSelf ket);
    public static abstract TSelf operator -(TSelf ket);

    public static abstract TSelf Multiply(TRealNumber scalar, TSelf ket);
    public static abstract TSelf operator *(TRealNumber scalar, TSelf ket);

    public static abstract TRealNumber Multiply(TBra bra, TSelf ket);
    public static abstract TRealNumber operator *(TBra bra, TSelf ket);

    public static abstract TRealNumber InnerProduct(TSelf left, TSelf right);
    public static abstract TRealNumber operator *(TSelf left, TSelf right);

    public static abstract TSelf TensorProduct(TSelf left, TSelf right);

    public static abstract TBra Bra(TSelf ket);

    public static abstract TRealNumber Norm(TSelf ket);
    public static abstract TRealNumber Distance(TSelf left, TSelf right);
    public static abstract TSelf Normalized(TSelf ket);
}

public interface IBra<TSelf, out TKet, TRealNumber>
    where TSelf : IBra<TSelf, TKet, TRealNumber>
    where TKet : IKet<TKet, TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf U(TRealNumber[] components);

    public static abstract TSelf Zero(int dimension);

    public static abstract int Dimension(TSelf bra);

    public static abstract TSelf Add(TSelf left, TSelf right);
    public static abstract TSelf operator +(TSelf left, TSelf right);

    public static abstract TSelf Subtract(TSelf left, TSelf right);
    public static abstract TSelf operator -(TSelf left, TSelf right);

    public static abstract TSelf AdditiveInverse(TSelf bra);
    public static abstract TSelf operator -(TSelf bra);

    public static abstract TSelf Multiply(TRealNumber scalar, TSelf bra);
    public static abstract TSelf operator *(TRealNumber scalar, TSelf bra);

    public static abstract TRealNumber InnerProduct(TSelf left, TSelf right);
    public static abstract TRealNumber operator *(TSelf left, TSelf right);

    public static abstract TSelf TensorProduct(TSelf left, TSelf right);

    public static abstract TKet Ket(TSelf bra);

    public static abstract TRealNumber Norm(TSelf bra);
    public static abstract TRealNumber Distance(TSelf left, TSelf right);
    public static abstract TSelf Normalized(TSelf bra);
}

public interface IOperator<TSelf, TKet, TBra, in TRealNumber>
    where TSelf : IOperator<TSelf, TKet, TBra, TRealNumber>
    where TKet : IKet<TKet, TBra, TRealNumber>
    where TBra : IBra<TBra, TKet, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TSelf M(TRealNumber[,] components);
    public static abstract TSelf M(float[,] components);

    public static abstract TSelf Identity(int dimension);
    public static abstract TSelf Zero(int dimension);

    public static abstract int Dimension(TSelf @operator);

    public static abstract TSelf Add(TSelf left, TSelf right);
    public static abstract TSelf operator +(TSelf left, TSelf right);

    public static abstract TSelf Subtract(TSelf left, TSelf right);
    public static abstract TSelf operator -(TSelf left, TSelf right);

    public static abstract TSelf AdditiveInverse(TSelf bra);
    public static abstract TSelf operator -(TSelf bra);

    public static abstract TSelf Multiply(TRealNumber scalar, TSelf @operator);
    public static abstract TSelf operator *(TRealNumber scalar, TSelf @operator);

    public static abstract TSelf Multiply(TSelf left, TSelf right);
    public static abstract TSelf operator *(TSelf left, TSelf right);

    public static abstract TSelf TensorProduct(TSelf left, TSelf right);

    public static abstract TSelf Commutator(TSelf left, TSelf right);

    public static abstract TKet Act(TSelf @operator, TKet ket);
    public static abstract TKet operator *(TSelf @operator, TKet ket);

    public static abstract TBra Act(TBra bra, TSelf @operator);
    public static abstract TBra operator *(TBra bra, TSelf @operator);

    public static abstract TSelf Round(TSelf ket);

    public static abstract bool IsIdentity(TSelf ket);
}