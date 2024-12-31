﻿using System.Numerics;

namespace Computation.Matrices.Real;

public interface IMatrix<TSelf, TRealNumber> :
    IAddition<TSelf>,
    ICanBeRounded<TSelf>,
    IEquality<TSelf>,
    IHasInverse<TSelf>,
    IScalarMultiplication<TSelf, TRealNumber>,
    ISubtraction<TSelf>,
    ITensorProduct<TSelf>

    where TSelf : IMatrix<TSelf, TRealNumber>
    where TRealNumber : IFloatingPointIeee754<TRealNumber>
{
}