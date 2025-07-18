using Radicals;
using Rationals;
using System;
using System.Numerics;
using Xunit;
using Xunit.Sdk;

namespace Radicals.Test
{
    public class RadicalTests
    {
        public static TheoryData<Radical, Radical> EqualsTestCases_DefaultConstructor()
        {
            return new TheoryData<Radical, Radical>
            {
                // Default constructor
                { new(), new() }
            };
        }

        public static TheoryData<Radical, Radical> EqualsTestCases_RadicandOnly()
        {
            return new TheoryData<Radical, Radical>
            {
                // Constructor with only radicand
                { new(0), new(0) },
                { new(1), new(1) },
                { new(5), new(5) }
            };
        }

        public static TheoryData<Radical, Radical> EqualsTestCases_CoefficientAndRadicand()
        {
            var data = new TheoryData<Radical, Radical>();

            // Get full set of permutations of radicand-only cases
            foreach (var coefficient in new[] { 0, 1, 5, new Rational(3, 4), new Rational(2, 9), new Rational(1, 2) })
            {
                foreach (var testCase in EqualsTestCases_RadicandOnly())
                {
                    var radical = testCase.Data.Item1;
                    data.Add(new(coefficient, radical.Radicand), new(coefficient, radical.Radicand));
                }
            }

            return data;
        }

        public static TheoryData<Radical, Radical> EqualsTestCases_CoefficientAndRadicand_ImplicitIndex2()
        {
            var data = new TheoryData<Radical, Radical>();

            foreach (var testCase in EqualsTestCases_CoefficientAndRadicand())
            {
                var radical = testCase.Data.Item1;
                data.Add(radical, new(radical.Coefficient, radical.Radicand, 2));
            }

            return data;
        }

        public static TheoryData<Radical, Radical> EqualsTestCases_CoefficientAndRadicandAndIndex()
        {
            var data = new TheoryData<Radical, Radical>();

            // Get full set of permutations of coefficient and radicand cases
            foreach (var index in new[] { 1, 2, 3, 4})
            {
                foreach (var testCase in EqualsTestCases_CoefficientAndRadicand())
                {
                    var radical = testCase.Data.Item1;
                    data.Add(new(radical.Coefficient, radical.Radicand, index), new(radical.Coefficient, radical.Radicand, index));
                }
            }

            return data;
        }

        public static TheoryData<Radical, Radical> EqualsTestCases_ToSimplestForm()
        {
            return new()
            {
                // Index simplifies to 1
                { new(0, 0, 5), new(0, 0, 1) },
                { new(1, 1, 5), new(1, 1, 1) },

                // Radicand is simplified and perfect roots are moved out into the coefficient
                { new(new(3, 4), 12), new(new(3, 2), 3) },
                { new(new(2, 9)), new(new(1, 3), 2) },
                { new(new(1, 2)), new(new(1, 2), 2) },

                // Higher index cases
                { new(1, 8, 3), new(2, 1) },
                { new(1, 4, 4), new(2) },
            };
        }

        public static TheoryData<Radical[], RadicalSum> AdditionTestCases()
        {
            return new()
            {
                // sqrt(2) + sqrt(3)
                {
                    new Radical[] { new(1, 2), new(1, 3) },
                    new([new(1, 2), new(1, 3)])
                },

                // sqrt(2) + 2 * sqrt(2) = 3 * sqrt(2)
                {
                    new Radical[] { new(1, 2), new(2, 2) },
                    new([new(3, 2)])
                },

                // 5*sqrt(27) + 7*sqrt(12) = 15*sqrt(3) + 14*sqrt(3) = 29*sqrt(3)
                {
                    new Radical[] { new(5, 27), new(7, 12) },
                    new([new(29, 3)])
                },

                // 3*sqrt(2) + 2*sqrt(3)
                {
                    new Radical[] { new(3, 2), new(2, 3) },
                    new([new(3, 2), new(2, 3)])
                },

                // (3/2)*sqrt(5) + 0 = (3/2)*sqrt(5)
                // 0 + (3/2)*sqrt(5) = (3/2)*sqrt(5)
                {
                    new Radical[] { new(new(3, 2), 5), Radical.Zero },
                    new([new(new(3, 2), 5)])
                },
                {
                    new Radical[] { new(new(3, 2), 5), 0 },
                    new([new(new(3, 2), 5)])
                },
                {
                    new Radical[] { 0, new(new(3, 2), 5) },
                    new([new(new(3, 2), 5)])
                },
                {
                    new Radical[] { Radical.Zero, new(new(3, 2), 5) },
                    new([new(new(3, 2), 5)])
                },

                // (3/2)*sqrt(5) + (-3/2)*sqrt(5) = 0
                {
                    new Radical[] { new(new(3, 2), 5), new(new(-3, 2), 5) },
                    new([Radical.Zero])
                },

                // 2*sqrt(2) + 5*sqrt(28) + sqrt(1/2) + 3 + sqrt(7/9) + 11*sqrt(4) = 25 + (5/2)sqrt(2) + (31/3)*sqrt(7)
                {
                    new Radical[] {
                        new(2, 2),
                        new(5, 28),
                        new(new(1, 2)),
                        new(3, 1),
                        new(new(7, 9)),
                        new(11, 4)
                    },
                    new([
                        new(25, 1),
                        new(new(5,2),2),
                        new(new(31,3),7)
                    ])
                },
            };
        }

        public static TheoryData<Radical, Radical, Radical> DivisionTestCases_SimpleCases()
        {
            var b61 = new Radical(new(3, 2), 5);
            var b71 = new Radical(new(3, 2), 5);

            return new()
            {
                // 0 divided by anything is 0
                { Radical.Zero, 1, Radical.Zero },
                { Radical.Zero, 2, Radical.Zero },
                { Radical.Zero, new(2), Radical.Zero },
                { Radical.Zero, Radical.One, Radical.Zero },

                // (3*sqrt(2)) / ((5/3)*sqrt(3)) = (9/5)*sqrt(2/3) = (3/5)*sqrt(6)
                {
                    new(3, 2), new(new(5, 3), 3),
                    new(new(3, 5), 6)
                },
                // 11 / sqrt(4/9) = 33/2
                {
                    new(11, 1), new(new(4, 9)),
                    new(new(33, 2), 1)
                },
                // 11 / sqrt(4/9) = 33/2 (commutative numerator)
                {
                    11, new(new(4, 9)),
                    new(new(33, 2), 1)
                },
                // sqrt(4/9) / 11 = 2/33
                {
                    new(new(4, 9)), 11,
                    new(new(2, 33), 1)
                },

                // (3/2)*sqrt(5) / 1 = (3/2)*sqrt(5)
                { b61, new(1, 1), new(new(3, 2), 5) },
                { b61, 1, new(new(3, 2), 5) },
                { b61, new(1), new(new(3, 2), 5) },
                { b61, Radical.One, new(new(3, 2), 5) },

                // (3/2)*sqrt(5) / -1 = (-3/2)*sqrt(5)
                { b71, new(-1, 1), new(new(-3, 2), 5) },
                { b71, -1, new(new(-3, 2), 5) },
                { b71, -Radical.One, new(new(-3, 2), 5) },

                // Sqrt(2) / Root[3](5) = Root[6](5000) * (1/5)
                { new(2), Radical.NthRoot(5, 3), new((Rational)1 / 5, 5000, 6) },
            };
        }

        public static TheoryData<RadicalSum, Radical, RadicalSum> DivisionTestCases_RadicalSumResults()
        {
            var b51 = new Radical(new(3, 2), 2);
            var b52 = new Radical(new(7, 3), 5);
            var b53 = new Radical(new(1, 3), 2);
            var b54 = new Radical(new(7, 5), 5);
            var b55 = new Radical(new(11, 4), 2);

            return new()
            {
                // [(3/2)*sqrt(2) - (7/3)*sqrt(5) + (1/3)*sqrt(2) - (7/5)*sqrt(5)] / (11/4)*sqrt(2)
                // = (2/3) - (112/165)*sqrt(10)
                {
                    b51 - b52 + b53 - b54,
                    b55,
                    new(
                        [
                            new(new(2, 3), 1),
                            new(new(-112, 165), 10)
                        ])
                },
            };
        }

        public static TheoryData<Radical, Radical> InverseTestCases()
        {
            return new()
            {
                // Inverse of One is One
                { Radical.One, Radical.One },

                // Inverse of 2 is 1/2
                { new(2, 1), new(new(1, 2), 1) },

                // Inverse of 3/2 is 2/3
                { new(new(3, 2), 1), new(new(2, 3), 1) },

                // Inverse of sqrt(2) is (1/2)*sqrt(2)
                { new(2), new(new(1, 2), 2) },

                // Inverse of (3/2)*sqrt(5) = (2/3)*sqrt(1/5) = (2/15)*sqrt(5)
                {
                    new((Rational)3 / 2, 5),
                    new((Rational)2 / 15, 5)
                },
            };
        }

        public static TheoryData<Radical> IsZeroTestCases()
        {
            return new()
            {
                { (Radical)0 },
                { new Radical() },
                { new Radical(0) },
                { new Radical(0, 0) },
                { new Radical(3, 0) },
                { new Radical(0, 5) },
                { new Radical(0, 0, 1) },
                { new Radical(3, 0, 1) },
                { new Radical(3, 0, 2) },
                { new Radical(0, 5, 1) },
                { new Radical(0, 5, 2) },
                { Radical.Zero },
            };
        }

        public static TheoryData<Radical> IsOneTestCases()
        {
            return new()
            {
                { (Radical)1 },
                { new Radical(1) },
                { new Radical(1, 1) },
                { new Radical(1, 1, 1) },
                { Radical.One }
            };
        }

        public static TheoryData<Radical, Radical, Radical> MultiplicationTestCases()
        {
            var b51 = new Radical(new(3, 2), 5);
            var b61 = new Radical(new(3, 2), 5);
            var b71 = new Radical(new(3, 2), 5);

            return new()
            {
                // Multiply by zero cases
                // (3/2)*sqrt(5) * 0 = 0
                { b71, 0, Radical.Zero },
                { b71, Radical.Zero, Radical.Zero },
                { b71, new Radical(0), Radical.Zero },
                { b71, new Radical(0, 0), Radical.Zero },
                { 0, b71, Radical.Zero },
                { Radical.Zero, b71, Radical.Zero },
                { new(0), b71, Radical.Zero },
                { new(0, 0), b71, Radical.Zero },

                // Muliply by one cases
                // (3/2)*sqrt(5) * 1 = (3/2)*sqrt(5)
                { b51, new(1, 1), new(new(3, 2), 5) },
                { b51, 1, new(new(3, 2), 5) },
                { b51, new(1), new(new(3, 2), 5) },
                { b51, Radical.One, new(new(3, 2), 5) },
                { new(1, 1), b51, new(new(3, 2), 5) },
                { 1, b51, new(new(3, 2), 5) },
                { new(1), b51, new(new(3, 2), 5) },
                { Radical.One, b51, new(new(3, 2), 5) },

                // Multiply by -1 cases
                // (3/2)*sqrt(5) * -1 = (-3/2)*sqrt(5)
            
                { b61, -Radical.One, new(new(-3, 2), 5) },
                { b61, -1, new(new(-3, 2), 5) },
                { b61, new(-1, 1), new(new(-3, 2), 5) },
                { -Radical.One, b61, new(new(-3, 2), 5) },
                { -1, b61, new(new(-3, 2), 5) },
                { new(-1, 1), b61, new(new(-3, 2), 5) },

                // (3*sqrt(2)) * ((5/3)*sqrt(3)) = (15/3)*sqrt(6) = 5*sqrt(6)
                {
                    new(3, 2),
                    new(new(5, 3), 3),
                    new(5, 6)
                },

                // 11 * sqrt(4/9) = 22/3
                {
                    new(11, 1),
                    new(new(4, 9)),
                    new(new(22,3), 1)
                },

                // 11 * sqrt(4/9) = 22/3 (commutative)
                {
                    11,
                    new(new(4, 9)),
                    new(new(22, 3), 1)
                },
                {
                    new(new(4, 9)),
                    11,
                    new(new(22, 3), 1)
                },

                // Sqrt(2) * Root[3](5) = Root[6](2^3) * Root[6](5^2) = Root[6](200)
                {
                    new(2),
                    Radical.NthRoot(5, 3),
                    new(1, 200, 6) 
                },
            };
        }

        public static TheoryData<RadicalSum, Radical, RadicalSum> MultiplicationTestCases_RadicalSumResults()
        {
            var b41 = new Radical(new(3, 2), 2);
            var b42 = new Radical(new(7, 3), 5);
            var b43 = new Radical(new(1, 3), 2);
            var b44 = new Radical(new(7, 5), 5);
            var b45 = new Radical(new(11, 4), 2);

            return new()
            {
                // [(3/2)*sqrt(2) - (7/3)*sqrt(5) + (1/3)*sqrt(2) - (7/5)*sqrt(5)] * (11/4)*sqrt(2)
                // = [(11/6)*sqrt(2) - (56/15)*sqrt(5)] * (11/4)*sqrt(2)
                // = (121/12) - (154/15)*sqrt(10)
                {
                    (b41 - b42 + b43 - b44), b45,
                    new(
                        [
                            new(new(121, 12), 1),
                            new(new(-154, 15), 10)
                        ])
                },
            };
        }

        public static TheoryData<Radical, Radical> NegateTestCases()
        {
            return new()
            {
                // Negate 0 = 0
                { Radical.Zero, Radical.Zero },

                // Negate 1 = -1
                { Radical.One, new(-1, 1) },

                // Negate (3/2)*sqrt(5) = (-3/2)*sqrt(5)
                { new(new(3, 2), 5), new(new(-3, 2), 5) },
                
                // Negate Sqrt(2) = -Sqrt(2)
                { Radical.Sqrt(2), new(-1, 2) },
            };
        }

        public static TheoryData<Radical, Radical> NthRootTestCases()
        {
            return new()
            {
                // Nth-Root[n:3](8)
                { Radical.NthRoot(8, 3), new(2, 1) },

                // Nth-Root[n:4](4) = 2^(2/4) = 2^(1/2) = Sqrt(2)
                { Radical.NthRoot(4, 4), new(2) },

                // Nth-Root[n:2](Sqrt(2)) = Nth-Root[n:4](2)
                { Radical.NthRoot(Radical.Sqrt(2), 2), new(1, 2, 4) },

                // Nth-Root[n:3](125) = 5
                { Radical.NthRoot(125, 3), 5 },

                // Nth-Root[m:5]((5/7)*Nth-Root[n:3](11)) 
                //              = (1/7)Nth-Root[n:15](125*‭13841287201‬*11)
                //              = (1/7)Nth-Root[n:15](‭19031769901375‬‬)
                //              = ‭1.0969847037974137166744190628521‬
                {
                    Radical.NthRoot(new Radical(new Rational(5,7), 11, 3), 5),
                    new(new(1, 7), BigInteger.Parse("19031769901375"), 15)
                },
            };
        }

        public static TheoryData<Radical, Radical> SqrtTestCases()
        {
            return new()
            {
                // Sqrt constructor: Sqrt(3/4) = (1/2)*Sqrt(3)
                { Radical.Sqrt((Rational)3 / 4), new Radical((Rational)1 / 2, 3) },

                // Sqrt(4) = 2
                { Radical.Sqrt(4), (Radical)2 },

                // Sqrt((5/7)*Nth-Root[n:3](11)) = (1/7)Nth-Root[n:6](125*343*11)
                //                               = (1/7)Nth-Root[n:6](‭471625‬)
                { Radical.Sqrt(new Radical(new Rational(5, 7), 11, 3)), new Radical(new Rational(1, 7), 471625, 6) },
            };
        }

        public static TheoryData<Radical, Radical, RadicalSum> SubtractionTestCases()
        {
            return new()
            {
                // (3/2)*sqrt(5) - 0 = (3/2)*sqrt(5)
                {
                    new(new(3, 2), 5),
                    Radical.Zero,
                    new([new(new(3, 2), 5)])
                },

                // 0 - (3/2)*sqrt(5) = (-3/2)*sqrt(5)
                {
                    Radical.Zero,
                    new(new(3, 2), 5),
                    new([new(new(-3, 2), 5)])
                },

                // (3/2)*sqrt(5) - (3/2)*sqrt(5) = 0
                {
                    new(new(3, 2), 5),
                    new(new(3, 2), 5),
                    new([Radical.Zero])
                },

                // sqrt(2) - sqrt(3)
                {
                    new(1, 2),
                    new(1, 3),
                    new([new(1, 2), new(-1, 3)])
                },

                // sqrt(2) - 2 * sqrt(2) = -1 * sqrt(2)
                {
                    new(1, 2),
                    new(2, 2),
                    new([new(-1, 2)])
                },

                // 5*sqrt(27) - 7*sqrt(12) = 15*sqrt(3) - 14*sqrt(3) = 1*sqrt(3)
                {
                    new(5, 27),
                    new(7, 12),
                    new([new(1, 3)])
                },

                // 3*sqrt(2) - 2*sqrt(3)
                {
                    new(3, 2),
                    new(2, 3),
                    new([new(3, 2), new(-2, 3)])
                },
            };
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(-1)]
        public void Test_Constructor_Throws_ArgumentOutOfRangeException_Radicand(int radicand)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Radical(radicand));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Radical(1, radicand));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Radical(1, radicand, 1));
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(0)]
        public void Test_Constructor_Throws_ArgumentOutOfRangeException_Index(int index)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Radical(1, 1, index));
        }

        [Theory]
        [MemberData(nameof(AdditionTestCases))]
        public void Test_Addition(Radical[] addends, RadicalSum expected)
        {
            var actual = RadicalSum.Zero;
            foreach (var radical in addends)
            {
                actual += radical;
            }
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(DivisionTestCases_SimpleCases))]
        public void Test_Division(Radical numerator, Radical denominator, Radical expected)
        {
            Assert.Equal(numerator / denominator, expected);
        }

        [Theory]
        [MemberData(nameof(DivisionTestCases_RadicalSumResults))]
        public void Test_Division_RadicalSumResults(RadicalSum numerator, Radical denominator, RadicalSum expected)
        {
            Assert.Equal(numerator / denominator, expected);
        }

        [Fact]
        public void Test_Division_Throws_DivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => 1 / Radical.Zero);
        }

        [Theory]
        [MemberData(nameof(EqualsTestCases_DefaultConstructor))]
        [MemberData(nameof(EqualsTestCases_RadicandOnly))]
        [MemberData(nameof(EqualsTestCases_CoefficientAndRadicand))]
        [MemberData(nameof(EqualsTestCases_CoefficientAndRadicand_ImplicitIndex2))]
        [MemberData(nameof(EqualsTestCases_CoefficientAndRadicandAndIndex))]
        [MemberData(nameof(EqualsTestCases_ToSimplestForm))]
        public void Test_Equals(Radical input, Radical expected)
        {
            Assert.Equal(expected, input);
        }

        [Theory]
        [MemberData(nameof(InverseTestCases))]
        public void Test_Inverse(Radical input, Radical expected)
        {
            var inverse = Radical.Invert(input);

            Assert.Equal(expected, inverse);
            Assert.Equal(Radical.One, input * inverse);
        }

        [Theory]
        [MemberData(nameof(IsOneTestCases))]
        public void Test_IsOne(Radical input)
        {
            Assert.True(input.IsOne);
        }

        [Theory]
        [MemberData(nameof(IsZeroTestCases))]
        public void Test_IsZero(Radical input)
        {
            Assert.True(input.IsZero);
        }

        [Theory]
        [MemberData(nameof(MultiplicationTestCases))]
        public void Test_Multiplication(Radical a, Radical b, Radical expected)
        {
            Assert.Equal(expected, a * b);
            Assert.Equal(expected, b * a);
        }

        [Theory]
        [MemberData(nameof(MultiplicationTestCases_RadicalSumResults))]
        public void Test_Multiplication_RadicalSumResults(RadicalSum a, Radical b, RadicalSum expected)
        {
            Assert.Equal(expected, a * b);
            Assert.Equal(expected, b * a);
        }

        [Theory]
        [MemberData(nameof(NegateTestCases))]
        public void Test_Negate(Radical input, Radical expected)
        {
            var negated = Radical.Negate(input);
            Assert.Equal(expected, negated);
            Assert.Equal(Radical.Zero, input + negated);
        }

        [Theory]
        [MemberData(nameof(NthRootTestCases))]
        public void Test_NthRoot(Radical input, Radical expected)
        {
            Assert.Equal(expected, input);
        }

        [Theory]
        [MemberData(nameof(SqrtTestCases))]
        public void Test_Sqrt(Radical input, Radical expected)
        {
            Assert.Equal(expected, input);
        }

        [Theory]
        [MemberData(nameof(SubtractionTestCases))]
        public void Test_Subtraction(Radical left, Radical Right, RadicalSum expected)
        {
            Assert.Equal(expected, left - Right);
        }
    }
}
