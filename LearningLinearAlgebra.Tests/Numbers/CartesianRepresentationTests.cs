using Computation.Numbers;
using FluentAssertions;
using Xunit;
using static Computation.Numbers.ComplexNumber<float>;

namespace LearningLinearAlgebra.Tests.Numbers;

public class CartesianRepresentationTests
{
    [Fact]
    public void Sum_of_two_complex_numbers_is_calculated_as_sum_of_components()
    {
        var a = C(5, 7);
        var b = C(11, 13);

        var sum = Add(a, b);

        sum.Should().Be(C(16, 20));
        (a + b).Should().Be(Add(a, b));
        a.Add(b).Should().Be(Add(a, b));
    }

    [Fact]
    public void Sum_of_complex_numbers_is_commutative()
    {
        var a = C(5, 7);
        var b = C(11, 13);

        var aPlusB = Add(a, b);
        var bPlusA = Add(b, a);

        aPlusB.Should().Be(bPlusA);
    }

    [Fact]
    public void Sum_of_complex_numbers_is_associative()
    {
        var a = C(5, 7);
        var b = C(11, 13);
        var c = C(17, 19);

        var sumOfAndBPlusC = Add(Add(a, b), c);
        var aPlusSumOfBAndC = Add(a, Add(b, c));

        sumOfAndBPlusC.Should().Be(aPlusSumOfBAndC);
    }

    [Fact]
    public void Difference_of_two_complex_numbers_is_calculated_as_difference_of_components()
    {
        var a = C(5, 7);
        var b = C(11, 13);

        var difference = Subtract(a, b);

        difference.Should().Be(C(-6, -6));
        (a - b).Should().Be(Subtract(a, b));
        a.Subtract(b).Should().Be(Subtract(a, b));
    }

    [Fact]
    public void Difference_of_complex_numbers_is_not_commutative()
    {
        var a = C(5, 7);
        var b = C(11, 13);

        var aMinusB = Subtract(a, b);
        var bMinusA = Subtract(b, a);

        aMinusB.Should().NotBe(bMinusA);
    }

    [Fact]
    public void Difference_of_complex_numbers_is_not_associative()
    {
        var a = C(5, 7);
        var b = C(11, 13);
        var c = C(17, 19);

        var differenceOfAndBMinusC = Subtract(Subtract(a, b), c);
        var aMinusDifferenceOfBAndC = Subtract(a, Subtract(b, c));

        differenceOfAndBMinusC.Should().NotBe(aMinusDifferenceOfBAndC);
    }

    [Fact]
    public void Product_of_complex_numbers()
    // Product of complex numbers (a1, b1) and (a2, b2) is calculated as (a1*a2 - b1*b2, a1*b2 + a2*b1)
    {
        var a = C(5, 7);
        var b = C(11, 13);

        var product = Multiply(a, b);

        product.Should().Be(C(-36, 142));
        (a * b).Should().Be(Multiply(a, b));
        a.Multiply(b).Should().Be(Multiply(a, b));
    }

    [Fact]
    public void Product_of_complex_numbers_is_commutative()
    {
        var a = C(5, 7);
        var b = C(11, 13);

        var aMultipliedByB = Multiply(a, b);
        var bMultipliedByA = Multiply(b, a);

        aMultipliedByB.Should().Be(bMultipliedByA);
    }

    [Fact]
    public void Product_of_complex_numbers_is_associative()
    {
        var a = C(5, 7);
        var b = C(11, 13);
        var c = C(17, 19);

        var productOfAndBMultipliedByC = Multiply(Multiply(a, b), c);
        var aMultipliedByProductOfBAndC = Multiply(a, Multiply(b, c));

        productOfAndBMultipliedByC.Should().Be(aMultipliedByProductOfBAndC);
    }

    [Fact]
    public void Multiplication_of_complex_numbers_distributes_over_addition()
    {
        var a = C(5, 7);
        var b = C(11, 13);
        var c = C(17, 19);

        var left = Multiply(a, Add(b, c));
        var right = Add(Multiply(a, b), Multiply(a, c));

        left.Should().Be(right);
    }

    [Fact]
    public void Product_of_complex_number_and_real_number_is_calculated_by_multiplying_both_components_of_complex_by_real()
    {
        var a = 5.0f;
        var b = C(-11, 5);

        var product = Multiply(a, b);

        product.Should().Be(C(-55, 25));
        (a * b).Should().Be(Multiply(a, b));
        (b * a).Should().Be(Multiply(a, b));
        a.Multiply(b).Should().Be(Multiply(a, b));
        b.Multiply(a).Should().Be(Multiply(a, b));
    }

    [Fact]
    public void Quotient_of_complex_numbers_is_calculated_using_complex_conjugations()
    {
        var a = C(-2, 1);
        var b = C(1, 2);

        var quotient = Divide(a, b);

        quotient.Should().Be(C(0, 1));
        a.Divide(b).Should().Be(Divide(a, b));
    }

    [Fact]
    public void Another_example_of_division_of_complex_numbers()
    {
        var a = C(0, 3);
        var b = C(-1, -1);

        var quotient = Divide(a, b);

        quotient.Should().Be(C(-3.0f / 2.0f, -3.0f / 2.0f));
    }

    [Fact]
    public void Quotient_of_complex_numbers_is_not_commutative()
    {
        var a = C(5, 7);
        var b = C(11, 13);

        var aDividedByB = Divide(a, b);
        var bDividedByA = Divide(b, a);

        aDividedByB.Should().NotBe(bDividedByA);
    }

    [Fact]
    public void Quotient_of_complex_numbers_is_not_associative()
    {
        var a = C(5, 7);
        var b = C(11, 13);
        var c = C(17, 19);

        var quotientOfAndBDividedByC = Divide(Divide(a, b), c);
        var aDividedByQuotientOfBAndC = Divide(a, Divide(b, c));

        quotientOfAndBDividedByC.Should().NotBe(aDividedByQuotientOfBAndC);
    }

    [Fact]
    public void Square_of_complex_number_is_number_multiplied_by_itself()
    {
        var complex = C(5, 7);

        var squared = Square(complex);

        squared.Should().Be(complex * complex);
        complex.Square().Should().Be(Square(complex));
    }

    [Fact]
    public void Modulus_of_complex_number_is_square_root_of_sum_of_second_power_of_components()
    {
        var complex = C(5, 7);

        var modulus = Modulus(complex);

        modulus.Should().Be(float.Sqrt(74.0f));
        complex.Modulus().Should().Be(Modulus(complex));
    }

    [Fact]
    public void Another_example_of_modulus()
    {
        var complex = C(1, -1);

        var modulus = Modulus(complex);

        modulus.Should().Be(float.Sqrt(2));
    }

    [Fact]
    public void Sum_of_complex_and_its_additive_inverse_is_zero()
    {
        var a = C(2, -3);

        (a + -a).Should().Be(C(0, 0));
    }

    [Fact]
    public void Complex_can_be_rounded_to_zero_if_it_differs_from_zero_only_by_small_value()
    {
        var almostZero = C(0.0000001f, 0.0000001f);

        Round(almostZero).Should().Be(Zero);
        almostZero.Round().Should().Be(Zero);
    }

    [Fact]
    public void Complex_cannot_be_rounded_to_zero_if_it_differs_from_zero_more_than_small_value()
    {
        var almostZero = C(0.000001f, 0.000001f);

        Round(almostZero).Should().NotBe(Zero);
        almostZero.Round().Should().NotBe(Zero);
    }

    [Fact]
    public void Complex_can_be_rounded_to_one_if_it_differs_from_one_only_by_small_value()
    {
        var almostOne = C(0.9999999f, 0.0000001f);

        Round(almostOne).Should().Be(One);
        almostOne.Round().Should().Be(One);
    }

    [Fact]
    public void Complex_cannot_be_rounded_to_as_one_if_it_differs_from_one_more_than_small_value()
    {
        var almostOne = C(0.999999f, 0.000001f);

        Round(almostOne).Should().NotBe(One);
        almostOne.Round().Should().NotBe(One);
    }
}
