using System.Numerics;
using LearningLinearAlgebra.Matrices.Complex;
using LearningLinearAlgebra.Numbers;

namespace LearningLinearAlgebra.ComplexVectorSpace;

public record Operator<TRealNumber>(SquareMatrix<TRealNumber> Components)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(Operator<TRealNumber>? other) =>
        Components.Equals(other?.Components);

    public override int GetHashCode() =>
        Components.GetHashCode();

    public ComplexNumber<TRealNumber> this[int i, int j] => Components[i, j];

    public static Operator<TRealNumber> operator +(Operator<TRealNumber> self, Operator<TRealNumber> other) =>
        new(self.Components.Add(other.Components));

    public static Operator<TRealNumber> operator -(Operator<TRealNumber> self, Operator<TRealNumber> other) =>
        new(self.Components.Subtract(other.Components));

    public static Operator<TRealNumber> operator -(Operator<TRealNumber> self) =>
        new(self.Components.AdditiveInverse());

    public static Operator<TRealNumber> operator *(ComplexNumber<TRealNumber> scalar, Operator<TRealNumber> self) =>
        new(self.Components.Multiply(scalar));

    public static Operator<TRealNumber> operator *(Operator<TRealNumber> self, Operator<TRealNumber> other) =>
        new(self.Components.Multiply(other.Components));

    public static Ket<TRealNumber> operator *(Operator<TRealNumber> self, Ket<TRealNumber> ket) =>
        new(self.Components.Act(ket.Components));

    public static Bra<TRealNumber> operator *(Bra<TRealNumber> bra, Operator<TRealNumber> self) =>
        Bra.U(bra.Components.Act(self.Components));
}

public static class Operator
{
    public static Operator<TRealNumber> M<TRealNumber>(SquareMatrix<TRealNumber> components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(components);

    public static Operator<TRealNumber> M<TRealNumber>(ComplexNumber<TRealNumber>[,] components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(SquareMatrix<TRealNumber>.M(components));

    public static Operator<TRealNumber> M<TRealNumber>(ComplexNumber<float>[,] components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(SquareMatrix<TRealNumber>.M(components));

    public static Operator<TRealNumber> M<TRealNumber>(ComplexNumber<double>[,] components)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(SquareMatrix<TRealNumber>.M(components));

    public static Operator<TRealNumber> Zero<TRealNumber>(int dimension)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(SquareMatrix<TRealNumber>.Zero(dimension));

    public static Operator<TRealNumber> Identity<TRealNumber>(int dimension)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(SquareMatrix<TRealNumber>.Identity(dimension));

    public static int Dimension<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.M();

    public static Operator<TRealNumber> Add<TRealNumber>(this Operator<TRealNumber> self, Operator<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(self.Components.Add(other.Components));

    public static Operator<TRealNumber> Subtract<TRealNumber>(this Operator<TRealNumber> self,
        Operator<TRealNumber> other) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(self.Components.Subtract(other.Components));

    public static Operator<TRealNumber> AdditiveInverse<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(self.Components.AdditiveInverse());

    public static Operator<TRealNumber> Multiply<TRealNumber>(this ComplexNumber<TRealNumber> scalar,
        Operator<TRealNumber> self) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(self.Components.Multiply(scalar));

    public static Operator<TRealNumber> Multiply<TRealNumber>(this Operator<TRealNumber> self,
        ComplexNumber<TRealNumber> scalar) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(self.Components.Multiply(scalar));

    public static Operator<TRealNumber> Multiply<TRealNumber>(this Operator<TRealNumber> self,
        Operator<TRealNumber> other) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(self.Components.Multiply(other.Components));

    public static Operator<TRealNumber> TensorProduct<TRealNumber>(this Operator<TRealNumber> self,
        Operator<TRealNumber> other) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(self.Components.TensorProduct(other.Components));

    public static Operator<TRealNumber> Commutator<TRealNumber>(this Operator<TRealNumber> self,
        Operator<TRealNumber> other) where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(self.Components.Commutator(other.Components));

    public static Ket<TRealNumber> Act<TRealNumber>(this Operator<TRealNumber> self, Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(self.Components.Act(ket.Components));

    public static Bra<TRealNumber> Act<TRealNumber>(this Bra<TRealNumber> bra, Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Bra.U(bra.Components.Act(self.Components));

    public static Operator<TRealNumber> Conjucate<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(self.Components.Conjucate());

    public static Operator<TRealNumber> Adjoint<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(self.Components.Adjoint());

    public static Operator<TRealNumber> Transpose<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(self.Components.Transpose());

    public static Operator<TRealNumber> Round<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        new(self.Components.Round());

    public static bool IsIdentity<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.IsIdentity();

    public static bool IsUnitary<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.IsUnitary();

    public static bool IsHermitian<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        self.Components.IsHermitian();
}
