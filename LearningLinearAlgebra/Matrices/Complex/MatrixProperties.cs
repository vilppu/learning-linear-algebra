﻿using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.Matrices.Complex;

public interface ICanBeHermitian<TSelf>
    where TSelf : ICanBeHermitian<TSelf>
{
    public abstract static bool IsHermitian(TSelf matrix);
}

public interface ICanBeIdentity<TSelf>
    where TSelf : ICanBeIdentity<TSelf>
{
    public abstract static bool IsIdentity(TSelf matrix);
}

public interface ICanBeUnitary<TSelf>
    where TSelf : ICanBeUnitary<TSelf>
{
    public abstract static bool IsUnitary(TSelf matrix);
}

public interface IDistance<TSelf, TRealNumber>
    where TSelf : IDistance<TSelf, TRealNumber>
    where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber>
{
    public abstract static TRealNumber Distance(TSelf left, TSelf right);
}

public interface IEquality<TSelf>
    where TSelf : IEquality<TSelf>
{
    public abstract static bool AreEquivalent(TSelf left, TSelf right);
}

public interface IHasColumns<TSelf, TRealNumber>
    where TSelf : IHasColumns<TSelf, TRealNumber>
    where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber>
{
    public abstract static IEnumerable<ComplexNumber<TRealNumber>> Column(TSelf matrix, int j);
}

public interface IHasColumns<TSelf>
    where TSelf : IHasColumns<TSelf>
{
    public abstract static int N(TSelf matrix);
}

public interface IHasCommutator<TSelf>
    where TSelf : IHasCommutator<TSelf>
{
    public abstract static TSelf Commutator(TSelf left, TSelf right);
}

public interface ISum<TSelf, TRealNumber>
    where TSelf : ISum<TSelf, TRealNumber>
    where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber>
{
    public abstract static ComplexNumber<TRealNumber> Sum(TSelf vector);
}

public interface IHasLength<TSelf>
    where TSelf : IHasLength<TSelf>
{
    public abstract static int Length(TSelf vector);
}

public interface IHasMatrixEntries<TSelf, TRealNumber>
    where TSelf : IHasMatrixEntries<TSelf, TRealNumber>
    where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber>
{
    public ComplexNumber<TRealNumber> this[int i, int j] { get; }
}

public interface IHasNorm<TSelf, TRealNumber>
    where TSelf : IHasNorm<TSelf, TRealNumber>
    where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber>
{
    public abstract static TRealNumber Norm(TSelf vector);
}

public interface IHasRows<TSelf, TRealNumber>
    where TSelf : IHasRows<TSelf, TRealNumber>
    where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber>
{
    public abstract static IEnumerable<ComplexNumber<TRealNumber>> Row(TSelf matrix, int i);
}

public interface IHasRows<TSelf> where TSelf : IHasRows<TSelf>
{
    public abstract static int M(TSelf matrix);
}

public interface IHasVectorEntries<TSelf, TRealNumber>
    where TSelf : IHasVectorEntries<TSelf, TRealNumber>
    where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber>
{
    public ComplexNumber<TRealNumber> this[int index] { get; }
}

public static class FluentMatrixProperties
{
    public static bool IsEquivalentTo<TSelf>(this TSelf left, TSelf right) where TSelf : IEquality<TSelf> =>
        TSelf.AreEquivalent(left, right);

    public static bool IsHermitian<TSelf>(this TSelf matrix) where TSelf : ICanBeHermitian<TSelf> =>
        TSelf.IsHermitian(matrix);

    public static bool IsIdentity<TSelf>(this TSelf matrix) where TSelf : ICanBeIdentity<TSelf> =>
        TSelf.IsIdentity(matrix);

    public static bool IsUnitary<TSelf>(this TSelf matrix) where TSelf : ICanBeUnitary<TSelf> =>
        TSelf.IsUnitary(matrix);

    public static IEnumerable<ComplexNumber<TRealNumber>> Column<TSelf, TRealNumber>(this IHasColumns<TSelf, TRealNumber> matrix, int j)
        where TSelf : IHasColumns<TSelf, TRealNumber>
        where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber> =>
        TSelf.Column((TSelf)matrix, j);

    public static IEnumerable<ComplexNumber<TRealNumber>> Row<TSelf, TRealNumber>(this IHasRows<TSelf, TRealNumber> matrix, int i)
        where TSelf : IHasRows<TSelf, TRealNumber>
        where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber> =>
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
        where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber> =>
        TSelf.Distance((TSelf)left, right);

    public static TRealNumber Norm<TSelf, TRealNumber>(this IHasNorm<TSelf, TRealNumber> vector)
        where TSelf : IHasNorm<TSelf, TRealNumber>
        where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber> =>
        TSelf.Norm((TSelf)vector);

    public static TSelf Commutator<TSelf>(this TSelf left, TSelf right) where TSelf : IHasCommutator<TSelf> =>
        TSelf.Commutator(left, right);
}