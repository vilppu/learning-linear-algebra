using System.Numerics;

namespace LearningLinearAlgebra.LinearAlgebra.RealVectorSpace;

public static class FluentKetOperations
{
    public static int Dimension<TKet, TBra, TRealNumber>(this IKet<TKet, TBra, TRealNumber> ket)
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TKet.Dimension((TKet)ket);

    public static TKet Add<TKet, TBra, TRealNumber>(this IKet<TKet, TBra, TRealNumber> left, TKet right)
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TKet.Add((TKet)left, right);

    public static TKet Subtract<TKet, TBra, TRealNumber>(this IKet<TKet, TBra, TRealNumber> left, TKet right)
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TKet.Subtract((TKet)left, right);

    public static TKet AdditiveInverse<TKet, TBra, TRealNumber>(this IKet<TKet, TBra, TRealNumber> ket)
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TKet.AdditiveInverse((TKet)ket);

    public static TKet Multiply<TKet, TBra, TRealNumber>(this TRealNumber scalar, IKet<TKet, TBra, TRealNumber> ket)
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TKet.Multiply(scalar, (TKet)ket);

    public static TRealNumber Multiply<TKet, TBra, TRealNumber>(this IBra<TBra, TKet, TRealNumber> bra, TKet ket)
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TKet.Multiply((TBra)bra, ket);

    public static TRealNumber InnerProduct<TKet, TBra, TRealNumber>(this IKet<TKet, TBra, TRealNumber> left, TKet right)
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TKet.InnerProduct((TKet)left, right);

    public static TKet TensorProduct<TKet, TBra, TRealNumber>(this IKet<TKet, TBra, TRealNumber> left, TKet right)
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TKet.TensorProduct((TKet)left, right);

    public static TBra Bra<TBra, TKet, TRealNumber>(this IKet<TKet, TBra, TRealNumber> ket)
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TKet.Bra((TKet)ket);

    public static TRealNumber Norm<TBra, TKet, TRealNumber>(this IKet<TKet, TBra, TRealNumber> ket)
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TKet.Norm((TKet)ket);

    public static TRealNumber Distance<TBra, TKet, TRealNumber>(this IKet<TKet, TBra, TRealNumber> left, TKet right)
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TKet.Distance((TKet)left, right);

    public static TKet Normalized<TBra, TKet, TRealNumber>(this IKet<TKet, TBra, TRealNumber> ket)
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TKet.Normalized((TKet)ket);
}

public static class FluentBraOperations
{
    public static int Dimension<TBra, TKet, TRealNumber>(this IBra<TBra, TKet, TRealNumber> ket)
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TBra.Dimension((TBra)ket);

    public static TBra Add<TBra, TKet, TRealNumber>(this IBra<TBra, TKet, TRealNumber> left, TBra right)
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TBra.Add((TBra)left, right);

    public static TBra Subtract<TBra, TKet, TRealNumber>(this IBra<TBra, TKet, TRealNumber> left, TBra right)
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TBra.Subtract((TBra)left, right);

    public static TBra AdditiveInverse<TBra, TKet, TRealNumber>(this IBra<TBra, TKet, TRealNumber> ket)
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TBra.AdditiveInverse((TBra)ket);

    public static TBra Multiply<TBra, TKet, TRealNumber>(this TRealNumber scalar, IBra<TBra, TKet, TRealNumber> ket)
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TBra.Multiply(scalar, (TBra)ket);

    public static TRealNumber InnerProduct<TBra, TKet, TRealNumber>(this IBra<TBra, TKet, TRealNumber> left, TBra right)
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TBra.InnerProduct((TBra)left, right);

    public static TBra TensorProduct<TBra, TKet, TRealNumber>(this IBra<TBra, TKet, TRealNumber> left, TBra right)
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TBra.TensorProduct((TBra)left, right);

    public static TKet Ket<TBra, TKet, TRealNumber>(this IBra<TBra, TKet, TRealNumber> bra)
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TBra.Ket((TBra)bra);

    public static TRealNumber Norm<TBra, TKet, TRealNumber>(this IBra<TBra, TKet, TRealNumber> bra)
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TBra.Norm((TBra)bra);

    public static TRealNumber Distance<TBra, TKet, TRealNumber>(this IBra<TBra, TKet, TRealNumber> left, TBra right)
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TBra.Distance((TBra)left, right);

    public static TBra Normalized<TBra, TKet, TRealNumber>(this IBra<TBra, TKet, TRealNumber> bra)
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TBra.Normalized((TBra)bra);
}

public static class FluentOperatorOperations
{
    public static int Dimension<TOperator, TKet, TBra, TRealNumber>(this IOperator<TOperator, TKet, TBra, TRealNumber> @operator)
        where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TOperator.Dimension((TOperator)@operator);

    public static TOperator Add<TOperator, TKet, TBra, TRealNumber>(this IOperator<TOperator, TKet, TBra, TRealNumber> left, TOperator right)
        where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TOperator.Add((TOperator)left, right);

    public static TOperator Subtract<TOperator, TKet, TBra, TRealNumber>(this IOperator<TOperator, TKet, TBra, TRealNumber> left, TOperator right)
        where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TOperator.Subtract((TOperator)left, right);

    public static TOperator AdditiveInverse<TOperator, TKet, TBra, TRealNumber>(this IOperator<TOperator, TKet, TBra, TRealNumber> @operator)
        where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TOperator.AdditiveInverse((TOperator)@operator);

    public static TOperator Multiply<TOperator, TKet, TBra, TRealNumber>(this TRealNumber scalar, IOperator<TOperator, TKet, TBra, TRealNumber> @operator)
        where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
    TOperator.Multiply(scalar, (TOperator)@operator);

    public static TOperator Multiply<TOperator, TKet, TBra, TRealNumber>(this IOperator<TOperator, TKet, TBra, TRealNumber> left, TOperator right)
        where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TOperator.Multiply((TOperator)left, right);

    public static TOperator TensorProduct<TOperator, TKet, TBra, TRealNumber>(this IOperator<TOperator, TKet, TBra, TRealNumber> left, TOperator right)
        where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TOperator.TensorProduct((TOperator)left, right);

    public static TKet Act<TOperator, TKet, TBra, TRealNumber>(this IOperator<TOperator, TKet, TBra, TRealNumber> @operator, TKet ket)
        where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TOperator.Act((TOperator)@operator, ket);

    public static TBra Act<TOperator, TKet, TBra, TRealNumber>(this IBra<TBra, TKet, TRealNumber> bra, TOperator @operator)
        where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TOperator.Act((TBra)bra, @operator);

    public static bool IsIdentity<TOperator, TKet, TBra, TRealNumber>(this IOperator<TOperator, TKet, TBra, TRealNumber> @operator)
        where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TOperator.IsIdentity((TOperator)@operator);

    public static TOperator Round<TOperator, TKet, TBra, TRealNumber>(this IOperator<TOperator, TKet, TBra, TRealNumber> @operator)
        where TOperator : IOperator<TOperator, TKet, TBra, TRealNumber>
        where TKet : IKet<TKet, TBra, TRealNumber>
        where TBra : IBra<TBra, TKet, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TOperator.Round((TOperator)@operator);
}