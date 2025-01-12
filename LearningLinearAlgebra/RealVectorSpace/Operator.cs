using System.Numerics;
using LearningLinearAlgebra.Matrices.Real;

namespace LearningLinearAlgebra.RealVectorSpace;

public record Operator<TRealNumber>(SquareMatrix<TRealNumber> Components)
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
    public virtual bool Equals(Operator<TRealNumber>? other) =>
        Components.Equals(other?.Components);

    public override int GetHashCode() =>
        Components.GetHashCode();

    public static Operator<TRealNumber> M(SquareMatrix<TRealNumber> components) =>
        new(components);

    public static Operator<TRealNumber> M(TRealNumber[,] components) =>
        M(SquareMatrix<TRealNumber>.M(components));

    public static Operator<TRealNumber> M(int[,] components) =>
        M(SquareMatrix<TRealNumber>.M(components));

    public static Operator<TRealNumber> M(float[,] components) =>
        M(SquareMatrix<TRealNumber>.M(components));

    public static Operator<TRealNumber> M(double [,] components) =>
        M(SquareMatrix<TRealNumber>.M(components));

    public static Operator<TRealNumber> Zero(int dimension) =>
        M(SquareMatrix<TRealNumber>.Zero(dimension));

    public static Operator<TRealNumber> Identity(int dimension) =>
        M(SquareMatrix<TRealNumber>.Identity(dimension));

    public TRealNumber this[int i, int j] => Components[i, j];

    public static Operator<TRealNumber> Add(Operator<TRealNumber> self, Operator<TRealNumber> other) =>
        M(self.Components.Add(other.Components));

    public static Operator<TRealNumber> AdditiveInverse(Operator<TRealNumber> self) =>
        M(self.Components.AdditiveInverse());

    public static Bra<TRealNumber> Act(Bra<TRealNumber> bra, Operator<TRealNumber> self) =>
        Bra<TRealNumber>.U(bra.Components.Act(self.Components));

    public static Ket<TRealNumber> Act(Operator<TRealNumber> self, Ket<TRealNumber> ket) =>
        Ket<TRealNumber>.V(self.Components.Act(ket.Components));

    public static Operator<TRealNumber> Transpose(Operator<TRealNumber> self) =>
        M(self.Components.Transpose());

    public static int Dimension(Operator<TRealNumber> self) =>
        self.Components.M();

    public static bool IsIdentity(Operator<TRealNumber> self) =>
        self.Components.IsIdentity();

    public static Operator<TRealNumber> Multiply(TRealNumber scalar, Operator<TRealNumber> self) =>
        M(self.Components.Multiply(scalar));

    public static Operator<TRealNumber> Multiply(Operator<TRealNumber> self, Operator<TRealNumber> other) =>
        M(self.Components.Multiply(other.Components));

    public static Operator<TRealNumber> Round(Operator<TRealNumber> self) =>
        M(self.Components.Round());

    public static Operator<TRealNumber> Subtract(Operator<TRealNumber> self, Operator<TRealNumber> other) =>
        M(self.Components.Subtract(other.Components));

    public static Operator<TRealNumber> TensorProduct(Operator<TRealNumber> self, Operator<TRealNumber> other) =>
        M(self.Components.TensorProduct(other.Components));

    public static Operator<TRealNumber> operator +(Operator<TRealNumber> self, Operator<TRealNumber> other) =>
        M(self.Components.Add(other.Components));

    public static Operator<TRealNumber> operator -(Operator<TRealNumber> self, Operator<TRealNumber> other) =>
        M(self.Components.Subtract(other.Components));

    public static Operator<TRealNumber> operator -(Operator<TRealNumber> self) =>
        M(self.Components.AdditiveInverse());

    public static Operator<TRealNumber> operator *(TRealNumber scalar, Operator<TRealNumber> self) =>
        M(self.Components.Multiply(scalar));

    public static Operator<TRealNumber> operator *(Operator<TRealNumber> self, Operator<TRealNumber> other) =>
        M(self.Components.Multiply(other.Components));

    public static Ket<TRealNumber> operator *(Operator<TRealNumber> self, Ket<TRealNumber> ket) =>
        Ket<TRealNumber>.V(self.Components.Act(ket.Components));

    public static Bra<TRealNumber> operator *(Bra<TRealNumber> bra, Operator<TRealNumber> self) =>
        Bra<TRealNumber>.U(bra.Components.Act(self.Components));
}

public static class Operator
{
    public static int Dimension<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Dimension(self);

    public static Operator<TRealNumber> Add<TRealNumber>(this Operator<TRealNumber> self, Operator<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Add(self, other);

    public static Operator<TRealNumber> Subtract<TRealNumber>(this Operator<TRealNumber> self, Operator<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Subtract(self, other);

    public static Operator<TRealNumber> AdditiveInverse<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.AdditiveInverse(self);

    public static Operator<TRealNumber> Transpose<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Transpose(self);

    public static Operator<TRealNumber> Multiply<TRealNumber>(this TRealNumber scalar, Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
    Operator<TRealNumber>.Multiply(scalar, self);

    public static Operator<TRealNumber> Multiply<TRealNumber>(this Operator<TRealNumber> self, TRealNumber scalar)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Multiply(scalar, self);

    public static Operator<TRealNumber> Multiply<TRealNumber>(this Operator<TRealNumber> self, Operator<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Multiply(self, other);

    public static Operator<TRealNumber> TensorProduct<TRealNumber>(this Operator<TRealNumber> self, Operator<TRealNumber> other)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.TensorProduct(self, other);

    public static Ket<TRealNumber> Act<TRealNumber>(this Operator<TRealNumber> self, Ket<TRealNumber> ket)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Act(self, ket);

    public static Bra<TRealNumber> Act<TRealNumber>(this Bra<TRealNumber> bra, Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Act(bra, self);

    public static Operator<TRealNumber> Round<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.Round(self);

    public static bool IsIdentity<TRealNumber>(this Operator<TRealNumber> self)
        where TRealNumber : IFloatingPointIeee754<TRealNumber> =>
        Operator<TRealNumber>.IsIdentity(self);
}