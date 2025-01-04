using FluentAssertions;
using Xunit;
using static Computation.Numbers.RealNumber<float>;
using static Computation.Numbers.Polar<float>;

namespace LearningLinearAlgebra.Tests.Numbers;

public class PolarRepresentationTests
{
    [Fact]
    public void Sum_of_two_complex_numbers_is_calculated_using_the_cartesian_format()
    {
        var a = P(1, 0);
        var b = P(1, Pi / 2.0f);

        var sum = Add(a, b);

        sum.Magnitude.Should().Be(Sqrt(2));
        sum.Phase.Should().Be(Pi / 4.0f);
        sum.Should().Be(a + b);
    }

    [Fact]
    public void Difference_of_two_complex_numbers_is_calculated_using_the_cartesian_format()
    {
        var a = P(1, 0);
        var b = P(1, Pi / 2.0f);

        var difference = Subtract(a, b);

        difference.Magnitude.Should().Be(Sqrt(2));
        difference.Phase.Should().Be(Pi / -4.0f);
        difference.Should().Be(a - b);
    }

    [Fact]
    public void Product_of_two_complex_numbers_is_calculated_by_multiplying_the_magnitudes_and_adding_their_phase()
    {
        var a = P(Sqrt(2), Pi / 4.0f);
        var b = P(Sqrt(2), Pi * (3.0f / 4.0f));

        var product = Multiply(a, b);

        product.Magnitude.Should().BeApproximately(2, 10);
        product.Phase.Should().BeApproximately(Pi, 10);
        product.Should().Be(a * b);
    }

    [Fact]
    public void Quotient_of_two_complex_numbers_is_calculated_by_dividing_the_magnitudes_and_subtracting_their_phase()
    {
        var a = P(Sqrt(2), Pi / 4.0f);
        var b = P(Sqrt(2), Pi * (3.0f / 4.0f));


        var quotient = Divide(a, b);

        quotient.Magnitude.Should().Be(1);
        quotient.Phase.Should().Be(Pi * 1.5f);
        quotient.Should().Be(a / b);
    }

    [Fact]
    public void Another_example_of_dividing_complex_numbers()
    {
        var a = P(Sqrt(10), Atan(-3.0f));
        var b = P(Sqrt(17), Atan(4.0f));

        var quotient = Divide(a, b);

        quotient.Magnitude.Should().BeApproximately(0.7669649888f, 10);
        quotient.Phase.Should().BeApproximately(3.7083218711f, 10);
        quotient.Should().Be(a / b);
    }
}