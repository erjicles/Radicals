using System;
using Radicals.Extensions;
using Rationals;
using Xunit;

namespace Radicals.Test.Extensions
{
    public class MathExtensionsTests
    {
        public static TheoryData<int, int, int> TestCases_Pow_GenericInteger()
            => new()
            {
                { 0, 0, 1 },
                { 0, 5, 0 },
                { 2, 0, 1 },
                { 2, 1, 2 },
                { 2, 10, 1024 },
                { -3, 2, 9 },
                { -3, 3, -27 },
            };

        public static TheoryData<Rational, int, Rational> TestCases_Pow_Rational()
            => new()
            {
                { 0, 0, Rational.One },
                { 0, 7, Rational.Zero },
                { (Rational)3 / 2, 0, Rational.One },
                { (Rational)3 / 2, 3, new Rational(27, 8) },
                { (Rational)(-2) / 3, 2, new Rational(4, 9) },
                { (Rational)(-2) / 3, 3, new Rational(-8, 27) },
            };

        public static TheoryData<string, int, string, string> TestCases_Pow_CustomMultiplyOperator()
            => new()
            {
                { "ab", 0, string.Empty, string.Empty },
                { "ab", 0, "identity", "identity" },
                { "ab", 1, "identity", "ab" },
                { "ab", 2, string.Empty, "abab" },
                { "ab", 3, string.Empty, "ababab" },
            };

        [Theory]
        [MemberData(nameof(TestCases_Pow_GenericInteger))]
        public void Test_Pow_GenericInteger(int value, int exponent, int expectedResult)
            => Assert.Equal(expectedResult, value.Pow(exponent));

        [Theory]
        [MemberData(nameof(TestCases_Pow_Rational))]
        public void Test_Pow_Rational(Rational value, int exponent, Rational expectedResult)
            => Assert.Equal(expectedResult, value.Pow(exponent));

        [Theory]
        [MemberData(nameof(TestCases_Pow_CustomMultiplyOperator))]
        public void Test_Pow_CustomMultiplyOperator(string value, int exponent, string multiplicativeIdentity, string expectedResult)
            => Assert.Equal(expectedResult, value.Pow(exponent, multiplicativeIdentity, static (left, right) => left + right));

        [Fact]
        public void Test_Pow_Throws_ArgumentOutOfRangeException_WhenExponentIsNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 2.Pow(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => ((Rational)3 / 2).Pow(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => "ab".Pow(-1, string.Empty, static (left, right) => left + right));
        }
    }
}
