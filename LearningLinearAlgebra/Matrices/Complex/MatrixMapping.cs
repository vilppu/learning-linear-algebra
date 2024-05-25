using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.Matrices.Complex;

public interface ITwoDimensionalMap<TSelf, TRealNumber>
    where TSelf : ITwoDimensionalMap<TSelf, TRealNumber>
    where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber>
{
    public abstract static TSelf Map(TSelf matrix, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
}

public interface ITwoDimensionalZip<TSelf, TRealNumber>
    where TSelf : ITwoDimensionalZip<TSelf, TRealNumber>
    where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber>
{
    public abstract static TSelf Zip(TSelf left, TSelf right, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
}

public interface IOrthonormalization<TSelf> where TSelf : IOrthonormalization<TSelf>
{
    public abstract static TSelf Orthonormal(TSelf vector);
}

public interface IOneDimensionalMap<TSelf, TRealNumber>
    where TSelf : IOneDimensionalMap<TSelf, TRealNumber>
    where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber>
{
    public abstract static TSelf Map(TSelf source, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
}

public interface IOneDimensionalZip<TSelf, TRealNumber>
    where TSelf : IOneDimensionalZip<TSelf, TRealNumber>
    where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber>
{
    public abstract static TSelf Zip(TSelf first, TSelf second, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping);
}

public static class FluentMatrixMapping
{
    public static TSelf Map<TSelf, TRealNumber>(this ITwoDimensionalMap<TSelf, TRealNumber> matrix, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping)
        where TSelf : ITwoDimensionalMap<TSelf, TRealNumber>
        where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber> =>
        TSelf.Map((TSelf)matrix, elementMapping);

    public static TSelf Map<TSelf, TRealNumber>(this IOneDimensionalMap<TSelf, TRealNumber> source, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping)
        where TSelf : IOneDimensionalMap<TSelf, TRealNumber>
        where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber> =>
        TSelf.Map((TSelf)source, elementMapping);

    public static TSelf Orthonormal<TSelf>(this TSelf vector)
        where TSelf : IOrthonormalization<TSelf> =>
        TSelf.Orthonormal(vector);

    public static TSelf Zip<TSelf, TRealNumber>(this ITwoDimensionalZip<TSelf, TRealNumber> left, TSelf right, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping)
        where TSelf : ITwoDimensionalZip<TSelf, TRealNumber>
        where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber> =>
        TSelf.Zip((TSelf)left, right, elementMapping);

    public static TSelf Zip<TSelf, TRealNumber>(this IOneDimensionalZip<TSelf, TRealNumber> first, TSelf second, Func<ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>, ComplexNumber<TRealNumber>> elementMapping)
        where TSelf : IOneDimensionalZip<TSelf, TRealNumber>
        where TRealNumber : System.Numerics.IFloatingPointIeee754<TRealNumber> =>
        TSelf.Zip((TSelf)first, second, elementMapping);
}
