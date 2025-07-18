using Rationals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Radicals.Test
{
    public class RadicalSumTests
    {
        private static readonly RadicalSum[] _zeroRepresentations =
        [
            0,
            RadicalSum.Zero,
            new(0),
            new(0, 0),
            new(3, 0),
            new(0, 5),
            new(new Rational(0, 1)),
            new(new Rational(0, 1), 1),
        ];

        private static readonly RadicalSum[] _oneRepresentations =
        [
            1,
            RadicalSum.One,
            new(1),
            new(1, 1),
            new(new Rational(1, 1)),
            new(new Rational(1, 1), 1),
        ];

        public static TheoryData<RadicalSum[], RadicalSum> TestCases_Addition()
        {
            return new()
            {
                // sqrt(2) + sqrt(3)
                {
                    [
                        new RadicalSum(1, 2),
                        new RadicalSum(1, 3),
                    ],
                    new RadicalSum([new Radical(1, 2), new Radical(1, 3)])
                },

                // sqrt(2) + 2 * sqrt(2) = 3 * sqrt(2)
                {
                    [
                        new RadicalSum(1, 2),
                        new RadicalSum(2, 2),
                    ],
                    new RadicalSum(new Radical(3, 2))
                },

                // 5*sqrt(27) + 7*sqrt(12) = 15*sqrt(3) + 14*sqrt(3) = 29*sqrt(3)
                {
                    [
                        new RadicalSum(5, 27),
                        new RadicalSum(7, 12),
                    ],
                    new RadicalSum(new Radical(29, 3))
                },

                // 3*sqrt(2) + 2*sqrt(3)
                {
                    [
                        new RadicalSum(3, 2),
                        new RadicalSum(2, 3),
                    ],
                    new RadicalSum([new Radical(3, 2), new Radical(2, 3)])
                },

                // (3/2)*sqrt(5) + 0 = (3/2)*sqrt(5)
                // 0 + (3/2)*sqrt(5) = (3/2)*sqrt(5)
                {
                    [
                        new RadicalSum(new Rational(3, 2), 5),
                        RadicalSum.Zero,
                    ],
                    new RadicalSum(new Radical(new Rational(3, 2), 5))
                },

                // (3/2)*sqrt(5) + (-3/2)*sqrt(5) = 0
                {
                    [
                        new RadicalSum(new Rational(3, 2), 5),
                        new RadicalSum(new Rational(-3, 2), 5),
                    ],
                    RadicalSum.Zero
                },

                // 2*sqrt(2) + 5*sqrt(28) + sqrt(1/2) + 3 + sqrt(7/9) + 11*sqrt(4) = 25 + (5/2)sqrt(2) + (31/3)*sqrt(7)
                {
                    [
                        new RadicalSum(2, 2),
                        new RadicalSum(5, 28),
                        new RadicalSum(new Rational(1, 2)),
                        new RadicalSum(3, 1),
                        new RadicalSum(new Rational(7, 9)),
                        new RadicalSum(11, 4),
                    ],
                    new RadicalSum(
                        [
                            new(25, 1),
                            new(new Rational(5,2),2),
                            new(new Rational(31,3),7)
                        ])
                },
            };
        }

        public static TheoryData<RadicalSum, Radical, RadicalSum> TestCases_Division_RadicalDenominator()
        {
            return new()
            {
                // 0 / 1 = 0
                {
                    RadicalSum.Zero,
                    Radical.One,
                    RadicalSum.Zero
                },

                // 1 / 1 = 1
                {
                    RadicalSum.One,
                    Radical.One,
                    RadicalSum.One
                },

                // (3/2)*sqrt(5) / 1 = (3/2)*sqrt(5)
                {
                    new RadicalSum(new Rational(3, 2), 5),
                    Radical.One,
                    new RadicalSum(new Rational(3, 2), 5)
                },

                // (3/2)*sqrt(5) / -1 = (-3/2)*sqrt(5)
                {
                    new RadicalSum(new Rational(3, 2), 5),
                    -1,
                    new RadicalSum(new Rational(-3, 2), 5)
                },

                // (3*sqrt(2)) / ((5/3)*sqrt(3)) = (9/5)*sqrt(2/3) = (3/5)*sqrt(6)
                {
                    new RadicalSum(3, 2),
                    new Radical(new Rational(5, 3), 3),
                    new RadicalSum(new Rational(3, 5), 6)
                },

                // 11 / sqrt(4/9) = 33/2
                {
                    new RadicalSum(11, 1),
                    new Radical(new Rational(4, 9)),
                    new RadicalSum(new Rational(33, 2), 1)
                },

                // sqrt(4/9) / 11 = 2/33
                {
                    new RadicalSum(new Rational(4, 9)),
                    11,
                    new RadicalSum(new Rational(2, 33), 1)
                },

                // [(3/2)*sqrt(2) - (7/3)*sqrt(5) + (1/3)*sqrt(2) - (7/5)*sqrt(5)] / (11/4)*sqrt(2)
                // = [(11/6)*sqrt(2) - (56/15)*sqrt(5)] / (11/4)*sqrt(2)
                // = (2/3) - (224/165)*sqrt(5/2)
                // = (2/3) - (112/165)*sqrt(10)
                {
                    new RadicalSum(
                        [
                            new Radical(new Rational(3, 2), 2),
                            new Radical(new Rational(-7, 3), 5),
                            new Radical(new Rational(1, 3), 2),
                            new Radical(new Rational(-7, 5), 5),
                        ]),
                    new Radical(new Rational(11, 4), 2),
                    new RadicalSum(
                        [
                            new Radical(new Rational(2, 3), 1),
                            new Radical(new Rational(-112, 165), 10),
                        ])
                },
            };
        }

        public static TheoryData<RadicalSum, RadicalSum, RadicalSumRatio> TestCases_Division_RadicalSumDenominator()
        {
            return new()
            {
                // 0 / 1 = 0
                {
                    RadicalSum.Zero,
                    RadicalSum.One,
                    RadicalSumRatio.Zero
                },

                // 1 / 1 = 0
                {
                    RadicalSum.One,
                    RadicalSum.One,
                    RadicalSumRatio.One
                },

                // [3*sqrt(2) + (7/2)*sqrt(3)] / [(-9/3)*sqrt(2) + (-14/4)*sqrt(3)] = -1
                {
                    new RadicalSum(
                        [
                            new Radical(3, 2),
                            new Radical(new Rational(7, 2), 3),
                        ]),
                    new RadicalSum(
                        [
                            new Radical(new Rational(-9, 3), 2),
                            new Radical(new Rational(-14, 4), 3),
                        ]),
                    new RadicalSumRatio(-1, 1)
                },
            };
        }

        public static TheoryData<RadicalSum, RadicalSum> TestCases_Equals_BasicCases()
        {
            var testCases = new TheoryData<RadicalSum, RadicalSum>();

            // 0 = 0
            foreach (var zero1 in _zeroRepresentations.ToList())
            {
                foreach (var zero2 in _zeroRepresentations.ToList())
                {
                    testCases.Add(zero1, zero2);
                }
            }

            // 1 = 1
            foreach (var one1 in _oneRepresentations.ToList())
            {
                foreach (var one2 in _oneRepresentations.ToList())
                {
                    testCases.Add(one1, one2);
                }
            }

            testCases.Add(11, new RadicalSum(11, 1));
            testCases.Add((RadicalSum)11, new RadicalSum(11, 1));

            return testCases;
        }

        public static TheoryData<RadicalSum, RadicalSum> TestCases_Equals_SimplificationCases()
        {
            return new()
            {
                // (3/4) * sqrt(12)
                // = (2*3/4) * sqrt(3)
                // = (3/2) * sqrt(3)
                {
                    new(new Rational(3, 4), 12),
                    new(new Rational(3, 2), 3)
                },

                // sqrt(2/9) = (1/3)sqrt(2)
                {
                    new(new Rational(2, 9)),
                    new(new Rational(1, 3), 2)
                },

                // sqrt(1/2) = (1/2)sqrt(2)
                {
                    new(new Rational(1, 2)),
                    new(new Rational(1, 2), 2)
                },
            };
        }

        public static TheoryData<RadicalSum> TestCases_GetRationalizer()
        {
            return new()
            {
                // 3*sqrt(2) + 2*sqrt(3)
                {
                    new RadicalSum(
                        [
                            new Radical(3, 2),
                            new Radical(2, 3)
                        ])
                },

                // sqrt(2) + sqrt(3) + sqrt(11)
                {
                    new RadicalSum(
                        [
                            new Radical(1, 2),
                            new Radical(1, 3),
                            new Radical(1, 11),
                        ])
                },

                // 8*sqrt(6) - 3*sqrt(10) - 5*sqrt(12) + 2*sqrt(14)
                {
                    new RadicalSum(
                        [
                            new Radical(8, 6),
                            new Radical(-3, 10),
                            new Radical(-5, 12),
                            new Radical(2, 14),
                        ])
                },

                // Multiplicative inverse of sqrt(2) * Root[3](5)
                // Inverse: (10/17) + (-4/17)*Sqrt(2) + (4/17)*Root[3](5) + (5/17)*Root[3](25) + (-5/17)*Root[6](200) + (-2/17)*Root[6](5000)
                {
                    new RadicalSum(
                        [
                            Radical.Sqrt(2),
                            Radical.NthRoot(5, 3),
                        ])
                },

                // Multiplicative inverse of sqrt(2) + root[3](3)
                {
                    new RadicalSum(
                        [
                            Radical.Sqrt(2),
                            Radical.NthRoot(3, 3)
                        ])
                },

                //// sqrt(2) + sqrt(3) + sqrt(6) + root[3](3) + root[3](4) + root[6](2) + root[6](3)
                //// Warning: was not able to complete due to memory/cpu/time constraints
                //{
                //    new RadicalSum(
                //        [
                //            Radical.Sqrt(2),
                //            Radical.Sqrt(3),
                //            Radical.Sqrt(6),
                //            Radical.NthRoot(3, 3),
                //            Radical.NthRoot(4, 3),
                //            Radical.NthRoot(2, 6),
                //            Radical.NthRoot(3, 6),
                //        ])
                //},
            };
        }

        public static TheoryData<RadicalSum, RadicalSum, RadicalSum> TestCases_GroupDivision()
        {
            return new()
            {
                // Group division: sqrt(7) / [sqrt(2) * root[3](5)]
                // = Sqrt(7) * [(10/17) + (-4/17)*Sqrt(2) + (4/17)*Root[3](5) + (5/17)*Root[3](25) + (-5/17)*Root[6](200) + (-2/17)*Root[6](5000)]
                // = (10/17)*Sqrt(7) + (-4/17)*Sqrt(14) + (4/17)*Root[6](8575) + (5/17)*Root[6](214375) + (-5/17)*Root[6](68600) + (-2/17)*Root[6](1715000)
                {
                    new RadicalSum([Radical.Sqrt(7)]),
                    new RadicalSum([Radical.Sqrt(2), Radical.NthRoot(5, 3)]),
                    new RadicalSum(
                        [
                            ((Rational)10 / 17) * Radical.Sqrt(7),
                            ((Rational)(-4) / 17) * Radical.Sqrt(14),
                            ((Rational)4 / 17) * Radical.NthRoot(8575, 6),
                            ((Rational)5 / 17) * Radical.NthRoot(214375, 6),
                            ((Rational)(-5) / 17) * Radical.NthRoot(68600, 6),
                            ((Rational)(-2) / 17) * Radical.NthRoot(1715000, 6)
                        ])
                },
            };
        }

        public static TheoryData<RadicalSum> TestCases_IsOne() => [.. _oneRepresentations];

        public static TheoryData<RadicalSum> TestCases_IsZero() => [.. _zeroRepresentations];

        public static TheoryData<RadicalSum, RadicalSum, RadicalSum> TestCases_Multiplication()
        {
            return new()
            {
                // 0 * 0 = 0
                {
                    RadicalSum.Zero,
                    RadicalSum.Zero,
                    RadicalSum.Zero
                },

                // 0 * 1 = 0
                {
                    RadicalSum.Zero,
                    RadicalSum.One,
                    RadicalSum.Zero
                },

                // 1 * 1 = 1
                {
                    RadicalSum.One,
                    RadicalSum.One,
                    RadicalSum.One
                },

                // (3/2)*sqrt(5) * 1 = (3/2)*sqrt(5)
                // 1 * (3/2)*sqrt(5) = (3/2)*sqrt(5)
                {
                    new RadicalSum(new Rational(3, 2), 5),
                    RadicalSum.One,
                    new RadicalSum(new Rational(3, 2), 5)
                },
                {
                    RadicalSum.One,
                    new RadicalSum(new Rational(3, 2), 5),
                    new RadicalSum(new Rational(3, 2), 5)
                },

                // (3/2)*sqrt(5) * 0 = 0
                // 0 * (3/2)*sqrt(5) = 0
                {
                    RadicalSum.Zero,
                    new RadicalSum(new Rational(3, 2), 5),
                    RadicalSum.Zero
                },

                // (3*sqrt(2)) * ((5/3)*sqrt(3)) = (15/3)*sqrt(6) = 5*sqrt(6)
                {
                    new RadicalSum(3, 2),
                    new RadicalSum(new Rational(5, 3), 3),
                    new RadicalSum(5, 6)
                },


                // 11 * sqrt(4/9) = 22/3
                {
                    new RadicalSum(11, 1),
                    new RadicalSum(new Rational(4, 9)),
                    new RadicalSum(new Rational(22, 3), 1)
                },

                // (3/2)*sqrt(5) * -1 = (-3/2)*sqrt(5)
                {
                    new RadicalSum(new Rational(3, 2), 5),
                    new RadicalSum(-1, 1),
                    new RadicalSum(new Rational(-3, 2), 5)
                },

                // [(3/2)*sqrt(2) - (7/3)*sqrt(5) + (1/3)*sqrt(2) - (7/5)*sqrt(5)] * (11/4)*sqrt(2)
                // = [(11/6)*sqrt(2) - (56/15)*sqrt(5)] * (11/4)*sqrt(2)
                // = (121/24)*2 - (154/15)*sqrt(10)
                // = (121/12) - (56/15)*sqrt(10)
                {
                    new RadicalSum(
                        [
                            new Radical(new Rational(3, 2), 2),
                            new Radical(new Rational(-7, 3), 5),
                            new Radical(new Rational(1, 3), 2),
                            new Radical(new Rational(-7, 5), 5)
                        ]),
                    new RadicalSum(new Rational(11, 4), 2),
                    new RadicalSum(
                        [
                            new Radical(new Rational(121, 12), 1),
                            new Radical(new Rational(-154, 15), 10),
                        ])
                },

                // [(5/3)*sqrt(7) - (2/9)*sqrt(11) - 6*sqrt(13)] * [2*sqrt(6) - 3*sqrt(8)]
                // = (10/3)*sqrt(42) - (4/9)*sqrt(66) - 12*sqrt(78) - 5*sqrt(56) + (2/3)*sqrt(88) + 18*sqrt(104)
                // = (10/3)*sqrt(42) - 5*sqrt(56) - (4/9)*sqrt(66) - (34/3)*sqrt(88) + 18*sqrt(104) - 12*sqrt(78)
                // = -10*sqrt(14) + (4/3)*sqrt(22) + 36*sqrt(26) + (10/3)*sqrt(42) - (4/9)*sqrt(66) - 12*sqrt(78)
                {
                    new RadicalSum(
                        [
                            new Radical(new Rational(5, 3), 7),
                            new Radical(new Rational(-2, 9), 11),
                            new Radical(-6, 13),
                        ]),
                    new RadicalSum(
                        [
                            new Radical(2, 6),
                            new Radical(-3, 8)
                        ]),
                    new RadicalSum(
                        [
                            new Radical(-10, 14),
                            new Radical(new Rational(4,3),22),
                            new Radical(36, 26),
                            new Radical(new Rational(10,3),42),
                            new Radical(new Rational(-4,9),66),
                            new Radical(-12,78),
                        ])
                },
            };
        }

        public static TheoryData<RadicalSum, RadicalSum, RadicalSum> TestCases_Subtraction()
        {
            return new()
            {
                // sqrt(2) - sqrt(3)
                {
                    new RadicalSum(1, 2),
                    new RadicalSum(1, 3),
                    new RadicalSum([new Radical(1, 2), new Radical(-1, 3)])
                },

                // sqrt(2) - 2 * sqrt(2) = -1 * sqrt(2)
                {
                    new RadicalSum(1, 2),
                    new RadicalSum(2, 2),
                    new RadicalSum(new Radical(-1, 2))
                },

                // 5*sqrt(27) - 7*sqrt(12) = 15*sqrt(3) - 14*sqrt(3) = 1*sqrt(3)
                {
                    new RadicalSum(5, 27),
                    new RadicalSum(7, 12),
                    new RadicalSum(new Radical(1, 3))
                },

                // 3*sqrt(2) - 2*sqrt(3)
                {
                    new RadicalSum(3, 2),
                    new RadicalSum(2, 3),
                    new RadicalSum([new(3, 2), new(-2, 3)])
                },

                // (3/2)*sqrt(5) - 0 = (3/2)*sqrt(5)
                {
                    new RadicalSum(new Rational(3, 2), 5),
                    RadicalSum.Zero,
                    new RadicalSum(new Radical(new Rational(3, 2), 5))
                },

                // 0 - (3/2)*sqrt(5) = (-3/2)*sqrt(5)
                {
                    RadicalSum.Zero,
                    new RadicalSum(new Rational(3, 2), 5),
                    new RadicalSum(new Radical(new Rational(-3, 2), 5))
                },

                // (3/2)*sqrt(5) - (3/2)*sqrt(5) = 0
                {
                    new RadicalSum(new Rational(3, 2), 5),
                    new RadicalSum(new Rational(3, 2), 5),
                    RadicalSum.Zero
                },
            };
        }

        [Theory]
        [MemberData(nameof(TestCases_Addition))]
        public void Test_Addition(RadicalSum[] summands, RadicalSum expectedResult)
        {
            var resultByMethod = RadicalSum.Zero;
            var resultByOperator = RadicalSum.Zero;

            foreach (var summand in summands)
            {
                resultByMethod = RadicalSum.Add(resultByMethod, summand);
                resultByOperator += summand;
            }

            Assert.Equal(expectedResult, resultByMethod);
            Assert.Equal(expectedResult, resultByOperator);
        }

        [Theory]
        [MemberData(nameof(TestCases_Division_RadicalDenominator))]
        public void Test_Division_RadicalDenominator(RadicalSum numerator, Radical denominator, RadicalSum expectedResult)
        {
            var resultByMethod = RadicalSum.Divide(numerator, denominator);
            var resultByOperator = numerator / denominator;

            Assert.Equal(expectedResult, resultByMethod);
            Assert.Equal(expectedResult, resultByOperator);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(11)]
        public void Test_Division_Throws_DivideByZeroException(RadicalSum numerator)
        {
            Assert.Throws<DivideByZeroException>(() => numerator / Radical.Zero);
            Assert.Throws<DivideByZeroException>(() => numerator / RadicalSum.Zero);
        }

        [Theory]
        [MemberData(nameof(TestCases_Equals_BasicCases))]
        [MemberData(nameof(TestCases_Equals_SimplificationCases))]
        public void Test_Equals(RadicalSum a, RadicalSum b)
            => Assert.Equal(a, b);

        [Theory]
        [MemberData(nameof(TestCases_GetRationalizer))]
        public void Test_GetRationalizer(RadicalSum value)
            => Assert.Equal(RadicalSum.One, value * RadicalSum.GetRationalizer(value));

        [Theory]
        [MemberData(nameof(TestCases_GroupDivision))]
        public void Test_GroupDivision(RadicalSum numerator, RadicalSum denominator, RadicalSum expectedResult)
            => Assert.Equal(expectedResult, RadicalSum.GroupDivide(numerator, denominator));

        [Theory]
        [MemberData(nameof(TestCases_IsOne))]
        public void Test_IsOne(RadicalSum one)
            => Assert.True(one.IsOne);

        [Theory]
        [MemberData(nameof(TestCases_IsZero))]
        public void Test_IsZero(RadicalSum zero)
            => Assert.True(zero.IsZero);

        [Theory]
        [MemberData(nameof(TestCases_Subtraction))]
        public void Test_Subtraction(RadicalSum a, RadicalSum b, RadicalSum expectedResult)
        {
            var resultByMethod = RadicalSum.Subtract(a, b);
            var resultByOperator = a - b;

            Assert.Equal(expectedResult, resultByMethod);
            Assert.Equal(expectedResult, resultByOperator);
        }

        [Theory]
        [MemberData(nameof(TestCases_Multiplication))]
        public void Test_Multiplication(RadicalSum a, RadicalSum b, RadicalSum expectedResult)
        {
            var resultByMethod = RadicalSum.Multiply(a, b);
            var resultByOperator = a * b;

            Assert.Equal(expectedResult, resultByMethod);
            Assert.Equal(expectedResult, resultByOperator);
        }
    }
}
