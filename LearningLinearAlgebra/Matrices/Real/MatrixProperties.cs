using System.Numerics;

namespace LearningLinearAlgebra.Matrices.Real;

public interface ICanBeIdentity<in TSelf>
    where TSelf : ICanBeIdentity<TSelf>
{
    public static abstract bool IsIdentity(TSelf matrix);
}

public interface IDistance<in TSelf, out TRealNumber>
    where TSelf : IDistance<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TRealNumber Distance(TSelf left, TSelf right);
}

public interface IEquality<in TSelf>
    where TSelf : IEquality<TSelf>
{
    public static abstract bool AreEquivalent(TSelf left, TSelf right);
}

public interface IHasColumns<in TSelf, out TRealNumber>
    where TSelf : IHasColumns<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract IEnumerable<TRealNumber> Column(TSelf matrix, int j);
}

public interface IHasColumns<in TSelf>
    where TSelf : IHasColumns<TSelf>
{
    public static abstract int N(TSelf matrix);
}

public interface IHasCommutator<TSelf>
    where TSelf : IHasCommutator<TSelf>
{
    public static abstract TSelf Commutator(TSelf left, TSelf right);
}

public interface ISum<in TSelf, out TRealNumber>
    where TSelf : ISum<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TRealNumber Sum(TSelf vector);
}

public interface IHasLength<in TSelf>
    where TSelf : IHasLength<TSelf>
{
    public static abstract int Length(TSelf vector);
}

public interface IHasMatrixEntries<TSelf, out TRealNumber>
    where TSelf : IHasMatrixEntries<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public TRealNumber this[int i, int j] { get; }
}

public interface IHasNorm<in TSelf, out TRealNumber>
    where TSelf : IHasNorm<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract TRealNumber Norm(TSelf vector);
}

public interface IHasRows<in TSelf, out TRealNumber>
    where TSelf : IHasRows<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public static abstract IEnumerable<TRealNumber> Row(TSelf matrix, int i);
}

public interface IHasRows<in TSelf> where TSelf : IHasRows<TSelf>
{
    public static abstract int M(TSelf matrix);
}

public interface IHasVectorEntries<TSelf, out TRealNumber>
    where TSelf : IHasVectorEntries<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public TRealNumber this[int index] { get; }
}

public static class FluentMatrixProperties
{
    public static bool IsEquivalentTo<TSelf>(this TSelf left, TSelf right) where TSelf : IEquality<TSelf> =>
        TSelf.AreEquivalent(left, right);

    public static bool IsIdentity<TSelf>(this TSelf matrix) where TSelf : ICanBeIdentity<TSelf> =>
        TSelf.IsIdentity(matrix);

    public static IEnumerable<TRealNumber> Column<TSelf, TRealNumber>(this IHasColumns<TSelf, TRealNumber> matrix, int j)
        where TSelf : IHasColumns<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Column((TSelf)matrix, j);

    public static IEnumerable<TRealNumber> Row<TSelf, TRealNumber>(this IHasRows<TSelf, TRealNumber> matrix, int i)
        where TSelf : IHasRows<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Row((TSelf)matrix, i);

    public static int Length<TSelf>(this TSelf vector)
        where TSelf : IHasLength<TSelf> =>
        TSelf.Length(vector);

    public static int M<TSelf>(this TSelf matrix) where TSelf : IHasRows<TSelf> =>
        TSelf.M(matrix);

    public static int N<TSelf>(this TSelf matrix) where TSelf : IHasColumns<TSelf> =>
        TSelf.N(matrix);

    public static TRealNumber Distance<TSelf, TRealNumber>(this IDistance<TSelf, TRealNumber> left, TSelf right
        ) where TSelf : IDistance<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Distance((TSelf)left, right);

    public static TRealNumber Norm<TSelf, TRealNumber>(this IHasNorm<TSelf, TRealNumber> vector)
        where TSelf : IHasNorm<TSelf, TRealNumber>
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        TSelf.Norm((TSelf)vector);

    public static TSelf Commutator<TSelf>(this TSelf left, TSelf right) where TSelf : IHasCommutator<TSelf> =>
        TSelf.Commutator(left, right);
}