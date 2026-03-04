using Rationals;
using Xunit;

namespace Radicals.Test
{
    public class RadicalSumRatioTests
    {
        private static readonly RadicalSumRatio[] _zeroRepresentations =
        [
            0,
            RadicalSumRatio.Zero,
            new RadicalSumRatio(0),
            new RadicalSumRatio(0, 0),
            new RadicalSumRatio(3, 0),
            new RadicalSumRatio(0, 5),
        ];

        private static readonly RadicalSumRatio[] _oneRepresentations =
        [
            1,
            RadicalSumRatio.One,
            new RadicalSumRatio(1, 1),
            new RadicalSumRatio(1),
        ];

        public static TheoryData<RadicalSumRatio[], RadicalSumRatio> TestCases_Addition()
            => new()
            {
                // sqrt(2) + sqrt(3)
                {
                    [new RadicalSumRatio(1, 2), new RadicalSumRatio(1, 3)],
                    new RadicalSumRatio([new Radical(1, 2), new Radical(1, 3)])
                },

                // sqrt(2) + 2*sqrt(2) = 3*sqrt(2)
                {
                    [new RadicalSumRatio(1, 2), new RadicalSumRatio(2, 2)],
                    new RadicalSumRatio(new Radical(3, 2))
                },

                // 5*sqrt(27) + 7*sqrt(12) = 15*sqrt(3) + 14*sqrt(3) = 29*sqrt(3)
                {
                    [new RadicalSumRatio(5, 27), new RadicalSumRatio(7, 12)],
                    new RadicalSumRatio(new Radical(29, 3))
                },

                // 3*sqrt(2) + 2*sqrt(3)
                {
                    [new RadicalSumRatio(3, 2), new RadicalSumRatio(2, 3)],
                    new RadicalSumRatio([new Radical(3, 2), new Radical(2, 3)])
                },

                // 2*sqrt(2) + 5*sqrt(28) + sqrt(1/2) + 3 + sqrt(7/9) + 11*sqrt(4) = 25 + (5/2)*sqrt(2) + (31/3)*sqrt(7)
                {
                    [
                        new RadicalSumRatio(2, 2),
                        new RadicalSumRatio(5, 28),
                        new RadicalSumRatio(new Rational(1, 2)),
                        new RadicalSumRatio(3, 1),
                        new RadicalSumRatio(new Rational(7, 9)),
                        new RadicalSumRatio(11, 4),
                    ],
                    new RadicalSumRatio([
                        new Radical(25, 1),
                        new Radical(new Rational(5, 2), 2),
                        new Radical(new Rational(31, 3), 7)
                    ])
                },

                // (3/2)*sqrt(5) + 0 = (3/2)*sqrt(5)
                {
                    [new RadicalSumRatio(new Rational(3, 2), 5), RadicalSumRatio.Zero],
                    new RadicalSumRatio(new Radical(new Rational(3, 2), 5))
                },

                // 0 + (3/2)*sqrt(5) = (3/2)*sqrt(5)
                {
                    [RadicalSumRatio.Zero, new RadicalSumRatio(new Rational(3, 2), 5)],
                    new RadicalSumRatio(new Radical(new Rational(3, 2), 5))
                },

                // (3/2)*sqrt(5) + (-3/2)*sqrt(5) = 0
                {
                    [new RadicalSumRatio(new Rational(3, 2), 5), new RadicalSumRatio(new Rational(-3, 2), 5)],
                    RadicalSumRatio.Zero
                },

                // {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]}
                // + {[(5/6)*sqrt(3) + (7/3)*sqrt(2)] / [(4/3)*sqrt(3) - (5/7)*sqrt(2)]}
                // = [(283) + (83/12)*sqrt(6)] / [(11) + 32*sqrt(6)]
                {
                    [
                        new RadicalSumRatio(
                            new Radical(new Rational(2, 3), 3) - new Radical(new Rational(3, 2), 2),
                            new Radical(new Rational(2, 3), 3) + new Radical(new Rational(3, 2), 2)),
                        new RadicalSumRatio(
                            new Radical(new Rational(5, 6), 3) + new Radical(new Rational(7, 3), 2),
                            new Radical(new Rational(4, 3), 3) - new Radical(new Rational(5, 7), 2)),
                    ],
                    new RadicalSumRatio(
                        new RadicalSum([new Radical(new Rational(283), 1), new Radical(new Rational(83, 12), 6)]),
                        new RadicalSum([new Radical(new Rational(11), 1), new Radical(new Rational(32), 6)]))
                },

                // (2/3) + (1/3) = 1
                {
                    [
                        new RadicalSumRatio(new RadicalSum(new Radical(2, 1)), new RadicalSum(new Radical(3, 1))),
                        new RadicalSumRatio(new RadicalSum(new Radical(1, 1)), new RadicalSum(new Radical(3, 1))),
                    ],
                    RadicalSumRatio.One
                },
            };

        public static TheoryData<RadicalSumRatio, RadicalSumRatio, RadicalSumRatio> TestCases_Division()
            => new()
            {
                // (3*sqrt(2)) / ((5/3)*sqrt(3)) = (3/5)*sqrt(6)
                {
                    new RadicalSumRatio(3, 2),
                    new RadicalSumRatio(new Rational(5, 3), 3),
                    new RadicalSumRatio(new Rational(3, 5), 6)
                },

                // 11 / sqrt(4/9) = 33/2
                {
                    new RadicalSumRatio(11, 1),
                    new RadicalSumRatio(new Rational(4, 9)),
                    new RadicalSumRatio(new Rational(33, 2), 1)
                },

                // sqrt(4/9) / 11 = 2/33
                {
                    new RadicalSumRatio(new Rational(4, 9)),
                    11,
                    new RadicalSumRatio(new Rational(2, 33), 1)
                },

                // [(3/2)*sqrt(2) - (7/3)*sqrt(5) + (1/3)*sqrt(2) - (7/5)*sqrt(5)] / (11/4)*sqrt(2)
                // = (2/3) - (112/165)*sqrt(10)
                {
                    new RadicalSumRatio(new Rational(3, 2), 2) - new RadicalSumRatio(new Rational(7, 3), 5) + new RadicalSumRatio(new Rational(1, 3), 2) - new RadicalSumRatio(new Rational(7, 5), 5),
                    new RadicalSumRatio(new Rational(11, 4), 2),
                    new RadicalSumRatio(
                    [
                        new Radical(new Rational(2, 3), 1),
                        new Radical(new Rational(-112, 165), 10)
                    ])
                },

                // (3/2)*sqrt(5) / 1 = (3/2)*sqrt(5)
                { new RadicalSumRatio(new Rational(3, 2), 5), new RadicalSumRatio(1, 1),           new RadicalSumRatio(new Rational(3, 2), 5) },
                { new RadicalSumRatio(new Rational(3, 2), 5), 1,                               new RadicalSumRatio(new Rational(3, 2), 5) },
                { new RadicalSumRatio(new Rational(3, 2), 5), new RadicalSumRatio(new Radical(1)), new RadicalSumRatio(new Rational(3, 2), 5) },
                { new RadicalSumRatio(new Rational(3, 2), 5), new RadicalSumRatio(Radical.One),    new RadicalSumRatio(new Rational(3, 2), 5) },

                // (3/2)*sqrt(5) / -1 = (-3/2)*sqrt(5)
                { new RadicalSumRatio(new Rational(3, 2), 5), new RadicalSumRatio(new Radical(-1, 1)), new RadicalSumRatio(new Rational(-3, 2), 5) },
                { new RadicalSumRatio(new Rational(3, 2), 5), -1,                                      new RadicalSumRatio(new Rational(-3, 2), 5) },
                { new RadicalSumRatio(new Rational(3, 2), 5), new RadicalSumRatio(-Radical.One),        new RadicalSumRatio(new Rational(-3, 2), 5) },

                // {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]} / {[(5/6)*sqrt(3) + (7/3)*sqrt(2)] / [(4/3)*sqrt(3) - (5/7)*sqrt(2)]}
                // = [(101/7) - (52/7)*sqrt(6)] / [26 + (101/12)*sqrt(6)]
                {
                    new RadicalSumRatio(
                        new Radical(new Rational(2, 3), 3) - new Radical(new Rational(3, 2), 2),
                        new Radical(new Rational(2, 3), 3) + new Radical(new Rational(3, 2), 2)),
                    new RadicalSumRatio(
                        new Radical(new Rational(5, 6), 3) + new Radical(new Rational(7, 3), 2),
                        new Radical(new Rational(4, 3), 3) - new Radical(new Rational(5, 7), 2)),
                    new RadicalSumRatio(
                        new RadicalSum([new Radical(new Rational(101, 7), 1), new Radical(new Rational(-52, 7), 6)]),
                        new RadicalSum([new Radical(new Rational(26), 1), new Radical(new Rational(101, 12), 6)]))
                },
            };

        public static TheoryData<RadicalSumRatio, RadicalSumRatio> TestCases_Equals_BasicCases()
        {
            var testCases = new TheoryData<RadicalSumRatio, RadicalSumRatio>();

            // 0 = 0
            foreach (var zero1 in _zeroRepresentations)
                foreach (var zero2 in _zeroRepresentations)
                    testCases.Add(zero1, zero2);

            // 1 = 1
            foreach (var one1 in _oneRepresentations)
                foreach (var one2 in _oneRepresentations)
                    testCases.Add(one1, one2);

            return testCases;
        }

        public static TheoryData<RadicalSumRatio, RadicalSumRatio> TestCases_Equals_SimplificationCases()
        {
            var c61 = new RadicalSum([new Radical(new Rational(3, 2), 7), new Radical(new Rational(-2, 3), 5)]);
            var c62 = new RadicalSum([new Radical(new Rational(1, 2), 7), new Radical(new Rational(4, 5), 6)]);

            return new()
            {
                // (3/4)*sqrt(12) = (3/2)*sqrt(3)
                { new RadicalSumRatio(new Rational(3, 4), 12), new RadicalSumRatio(new Rational(3, 2), 3) },

                // sqrt(2/9) = (1/3)*sqrt(2)
                { new RadicalSumRatio(new Rational(2, 9)), new RadicalSumRatio(new Rational(1, 3), 2) },

                // sqrt(1/2) = (1/2)*sqrt(2)
                { new RadicalSumRatio(new Rational(1, 2)), new RadicalSumRatio(new Rational(1, 2), 2) },

                // [(3/2)*sqrt(7) - (2/3)*sqrt(5)] / [(1/2)*sqrt(7) + (4/5)*sqrt(6)]
                { (RadicalSumRatio)c61 / c62, new RadicalSumRatio(c61, c62) },
            };
        }

        public static TheoryData<RadicalSumRatio, bool> TestCases_IsRational()
        {
            // [3*sqrt(2) + (7/2)*sqrt(3)] / [(-9/3)*sqrt(2) + (-14/4)*sqrt(3)] = -1 (rational)
            var rational = new RadicalSumRatio(
                new RadicalSum([new Radical(3, 2), new Radical(new Rational(7, 2), 3)]),
                new RadicalSum([new Radical(new Rational(-9, 3), 2), new Radical(new Rational(-14, 4), 3)]));

            // (3*sqrt(2)) / ((5/3)*sqrt(3)) = (3/5)*sqrt(6) (irrational)
            var irrational = new RadicalSumRatio(3, 2) / new RadicalSumRatio(new Rational(5, 3), 3);

            return new()
            {
                { rational, true },
                { irrational, false },
            };
        }

        public static TheoryData<RadicalSumRatio> TestCases_IsOne() => [.. _oneRepresentations];

        public static TheoryData<RadicalSumRatio> TestCases_IsZero() => [.. _zeroRepresentations];

        public static TheoryData<RadicalSumRatio, RadicalSumRatio, RadicalSumRatio> TestCases_Multiplication()
        {
            var product4Input =
                new RadicalSumRatio(new Rational(3, 2), 2)
                - new RadicalSumRatio(new Rational(7, 3), 5)
                + new RadicalSumRatio(new Rational(1, 3), 2)
                - new RadicalSumRatio(new Rational(7, 5), 5);
            var product4Factor = new RadicalSumRatio(new Rational(11, 4), 2);

            var bigProduct8Left =
                new RadicalSumRatio(new Rational(5, 3), 7)
                - new RadicalSumRatio(new Rational(2, 9), 11)
                - new RadicalSumRatio(6, 13);
            var bigProduct8Right = new RadicalSumRatio(2, 6) - new RadicalSumRatio(3, 8);

            var c91 = new Radical(new Rational(2, 3), 3) - new Radical(new Rational(3, 2), 2);
            var c92 = new Radical(new Rational(2, 3), 3) + new Radical(new Rational(3, 2), 2);
            var c93 = new Radical(new Rational(5, 6), 3) + new Radical(new Rational(7, 3), 2);
            var c94 = new Radical(new Rational(4, 3), 3) - new Radical(new Rational(5, 7), 2);
            var cr91 = new RadicalSumRatio(c91, c92);
            var cr92 = new RadicalSumRatio(c93, c94);

            return new()
            {
                // (3*sqrt(2)) * ((5/3)*sqrt(3)) = (15/3)*sqrt(6) = 5*sqrt(6)
                {
                    new RadicalSumRatio(3, 2),
                    new RadicalSumRatio(new Rational(5, 3), 3),
                    new RadicalSumRatio(5, 6)
                },

                // 11 * sqrt(4/9) = 22/3
                {
                    new RadicalSumRatio(11, 1),
                    new RadicalSumRatio(new Rational(4, 9)),
                    new RadicalSumRatio(new Rational(22, 3), 1)
                },

                // 11 * sqrt(4/9) = 22/3 (with implicit int)
                {
                    11,
                    new RadicalSumRatio(new Rational(4, 9)),
                    new RadicalSumRatio(new Rational(22, 3), 1)
                },

                // [(3/2)*sqrt(2) - (7/3)*sqrt(5) + (1/3)*sqrt(2) - (7/5)*sqrt(5)] * (11/4)*sqrt(2)
                // = (121/12) - (154/15)*sqrt(10)
                {
                    product4Input,
                    product4Factor,
                    new RadicalSumRatio([
                        new Radical(new Rational(121, 12), 1),
                        new Radical(new Rational(-154, 15), 10)
                    ])
                },

                // (3/2)*sqrt(5) * 1 = (3/2)*sqrt(5)
                { new RadicalSumRatio(new Rational(3, 2), 5), new RadicalSumRatio(1, 1),        new RadicalSumRatio(new Rational(3, 2), 5) },
                { new RadicalSumRatio(new Rational(3, 2), 5), 1,                            new RadicalSumRatio(new Rational(3, 2), 5) },
                { new RadicalSumRatio(new Rational(3, 2), 5), new RadicalSumRatio(1),          new RadicalSumRatio(new Rational(3, 2), 5) },
                { new RadicalSumRatio(new Rational(3, 2), 5), RadicalSumRatio.One,             new RadicalSumRatio(new Rational(3, 2), 5) },

                // (3/2)*sqrt(5) * -1 = (-3/2)*sqrt(5)
                { new RadicalSumRatio(new Rational(3, 2), 5), -RadicalSumRatio.One,           new RadicalSumRatio(new Rational(-3, 2), 5) },
                { new RadicalSumRatio(new Rational(3, 2), 5), -1,                             new RadicalSumRatio(new Rational(-3, 2), 5) },
                { new RadicalSumRatio(new Rational(3, 2), 5), new RadicalSumRatio(-1, 1),      new RadicalSumRatio(new Rational(-3, 2), 5) },

                // (3/2)*sqrt(5) * 0 = 0
                { new RadicalSumRatio(new Rational(3, 2), 5), 0,                              RadicalSumRatio.Zero },
                { new RadicalSumRatio(new Rational(3, 2), 5), RadicalSumRatio.Zero,            RadicalSumRatio.Zero },
                { new RadicalSumRatio(new Rational(3, 2), 5), new RadicalSumRatio(0),          RadicalSumRatio.Zero },
                { new RadicalSumRatio(new Rational(3, 2), 5), new RadicalSumRatio(0, 0),       RadicalSumRatio.Zero },

                // [(5/3)*sqrt(7) - (2/9)*sqrt(11) - 6*sqrt(13)] * [2*sqrt(6) - 3*sqrt(8)]
                // = -10*sqrt(14) + (4/3)*sqrt(22) + 36*sqrt(26) + (10/3)*sqrt(42) - (4/9)*sqrt(66) - 12*sqrt(78)
                {
                    bigProduct8Left,
                    bigProduct8Right,
                    new RadicalSumRatio([
                        new Radical(-10, 14),
                        new Radical(new Rational(4, 3), 22),
                        new Radical(36, 26),
                        new Radical(new Rational(10, 3), 42),
                        new Radical(new Rational(-4, 9), 66),
                        new Radical(-12, 78)
                    ])
                },

                // {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]} * {[(5/6)*sqrt(3) + (7/3)*sqrt(2)] / [(4/3)*sqrt(3) - (5/7)*sqrt(2)]}
                // = [(-112) + (77/12)*sqrt(6)] / [11 + 32*sqrt(6)]
                {
                    cr91,
                    cr92,
                    new RadicalSumRatio(
                        new RadicalSum([new Radical(new Rational(-112), 1), new Radical(new Rational(77, 12), 6)]),
                        new RadicalSum([new Radical(new Rational(11), 1), new Radical(new Rational(32), 6)]))
                },
            };
        }

        public static TheoryData<RadicalSumRatio, RadicalSumRatio, RadicalSumRatio> TestCases_Subtraction()
        {
            var a51 =
                new RadicalSumRatio(2, 2)
                + new RadicalSumRatio(5, 28)
                + new RadicalSumRatio(3, 1)
                + new RadicalSumRatio(11, 4);
            var b51 = new RadicalSumRatio(new Rational(1, 2)) + new RadicalSumRatio(new Rational(7, 9));

            var c91 = new Radical(new Rational(2, 3), 3) - new Radical(new Rational(3, 2), 2);
            var c92 = new Radical(new Rational(2, 3), 3) + new Radical(new Rational(3, 2), 2);
            var c93 = new Radical(new Rational(5, 6), 3) + new Radical(new Rational(7, 3), 2);
            var c94 = new Radical(new Rational(4, 3), 3) - new Radical(new Rational(5, 7), 2);
            var cr91 = new RadicalSumRatio(c91, c92);
            var cr92 = new RadicalSumRatio(c93, c94);

            var cr101 = new RadicalSumRatio(new RadicalSum(new Radical(2, 1)), new RadicalSum(new Radical(3, 1)));
            var cr102 = new RadicalSumRatio(new RadicalSum(new Radical(1, 1)), new RadicalSum(new Radical(3, 1)));

            return new()
            {
                // sqrt(2) - sqrt(3)
                {
                    new RadicalSumRatio(1, 2),
                    new RadicalSumRatio(1, 3),
                    new RadicalSumRatio([new Radical(1, 2), new Radical(-1, 3)])
                },

                // sqrt(2) - 2*sqrt(2) = -sqrt(2)
                {
                    new RadicalSumRatio(1, 2),
                    new RadicalSumRatio(2, 2),
                    new RadicalSumRatio(new Radical(-1, 2))
                },

                // 5*sqrt(27) - 7*sqrt(12) = 15*sqrt(3) - 14*sqrt(3) = sqrt(3)
                {
                    new RadicalSumRatio(5, 27),
                    new RadicalSumRatio(7, 12),
                    new RadicalSumRatio(new Radical(1, 3))
                },

                // 3*sqrt(2) - 2*sqrt(3)
                {
                    new RadicalSumRatio(3, 2),
                    new RadicalSumRatio(2, 3),
                    new RadicalSumRatio([new Radical(3, 2), new Radical(-2, 3)])
                },

                // 2*sqrt(2) + 5*sqrt(28) - sqrt(1/2) + 3 - sqrt(7/9) + 11*sqrt(4) = 25 + (3/2)*sqrt(2) + (29/3)*sqrt(7)
                {
                    a51,
                    b51,
                    new RadicalSumRatio([
                        new Radical(25, 1),
                        new Radical(new Rational(3, 2), 2),
                        new Radical(new Rational(29, 3), 7)
                    ])
                },

                // (3/2)*sqrt(5) - 0 = (3/2)*sqrt(5)
                {
                    new RadicalSumRatio(new Rational(3, 2), 5),
                    RadicalSumRatio.Zero,
                    new RadicalSumRatio(new Radical(new Rational(3, 2), 5))
                },

                // 0 - (3/2)*sqrt(5) = (-3/2)*sqrt(5)
                {
                    RadicalSumRatio.Zero,
                    new RadicalSumRatio(new Rational(3, 2), 5),
                    new RadicalSumRatio(new Radical(new Rational(-3, 2), 5))
                },

                // (3/2)*sqrt(5) - (3/2)*sqrt(5) = 0
                {
                    new RadicalSumRatio(new Rational(3, 2), 5),
                    new RadicalSumRatio(new Rational(3, 2), 5),
                    RadicalSumRatio.Zero
                },

                // {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]} - {[(5/6)*sqrt(3) + (7/3)*sqrt(2)] / [(4/3)*sqrt(3) - (5/7)*sqrt(2)]}
                // = [(-81) - (1331/12)*sqrt(6)] / [(11) + 32*sqrt(6)]
                {
                    cr91,
                    cr92,
                    new RadicalSumRatio(
                        new RadicalSum([new Radical(new Rational(-81), 1), new Radical(new Rational(-1331, 12), 6)]),
                        new RadicalSum([new Radical(new Rational(11), 1), new Radical(new Rational(32), 6)]))
                },

                // (2/3) - (1/3) = 1/3
                {
                    cr101,
                    cr102,
                    new RadicalSumRatio(new RadicalSum(new Radical(1, 1)), new RadicalSum(new Radical(3, 1)))
                },
            };
        }

        public static TheoryData<RadicalSumRatio, Rational> TestCases_ToRational()
        {
            // [3*sqrt(2) + (7/2)*sqrt(3)] / [(-9/3)*sqrt(2) + (-14/4)*sqrt(3)] = -1
            var value = new RadicalSumRatio(
                new RadicalSum([new Radical(3, 2), new Radical(new Rational(7, 2), 3)]),
                new RadicalSum([new Radical(new Rational(-9, 3), 2), new Radical(new Rational(-14, 4), 3)]));

            return new()
            {
                { value, new Rational(-1) },
            };
        }

        public static TheoryData<RadicalSumRatio, double> TestCases_ToDouble()
        {
            // (3*sqrt(2)) / ((5/3)*sqrt(3)) = (3/5)*sqrt(6) ≈ 1.4697
            var value = new RadicalSumRatio(3, 2) / new RadicalSumRatio(new Rational(5, 3), 3);

            return new()
            {
                { value, 1.4696938456699068589183704448235 },
            };
        }

        [Theory]
        [MemberData(nameof(TestCases_Addition))]
        public void Test_Addition(RadicalSumRatio[] summands, RadicalSumRatio expectedResult)
        {
            var resultByMethod = RadicalSumRatio.Zero;
            var resultByOperator = RadicalSumRatio.Zero;

            foreach (var summand in summands)
            {
                resultByMethod = RadicalSumRatio.Add(resultByMethod, summand);
                resultByOperator += summand;
            }

            Assert.Equal(expectedResult, resultByMethod);
            Assert.Equal(expectedResult, resultByOperator);
        }

        [Theory]
        [MemberData(nameof(TestCases_Division))]
        public void Test_Division(RadicalSumRatio numerator, RadicalSumRatio denominator, RadicalSumRatio expectedResult)
        {
            var resultByMethod = RadicalSumRatio.Divide(numerator, denominator);
            var resultByOperator = numerator / denominator;

            Assert.Equal(expectedResult, resultByMethod);
            Assert.Equal(expectedResult, resultByOperator);
        }

        [Theory]
        [MemberData(nameof(TestCases_Equals_BasicCases))]
        [MemberData(nameof(TestCases_Equals_SimplificationCases))]
        public void Test_Equals(RadicalSumRatio a, RadicalSumRatio b)
            => Assert.Equal(a, b);

        [Theory]
        [MemberData(nameof(TestCases_IsOne))]
        public void Test_IsOne(RadicalSumRatio one)
            => Assert.True(one.IsOne);

        [Theory]
        [MemberData(nameof(TestCases_IsRational))]
        public void Test_IsRational(RadicalSumRatio value, bool expectedResult)
            => Assert.Equal(expectedResult, value.IsRational);

        [Theory]
        [MemberData(nameof(TestCases_IsZero))]
        public void Test_IsZero(RadicalSumRatio zero)
            => Assert.True(zero.IsZero);

        [Theory]
        [MemberData(nameof(TestCases_Multiplication))]
        public void Test_Multiplication(RadicalSumRatio a, RadicalSumRatio b, RadicalSumRatio expectedResult)
        {
            var resultByMethod = RadicalSumRatio.Multiply(a, b);
            var resultByOperator = a * b;

            Assert.Equal(expectedResult, resultByMethod);
            Assert.Equal(expectedResult, resultByOperator);
            Assert.Equal(expectedResult, b * a);
        }

        [Theory]
        [MemberData(nameof(TestCases_Subtraction))]
        public void Test_Subtraction(RadicalSumRatio a, RadicalSumRatio b, RadicalSumRatio expectedResult)
        {
            var resultByMethod = RadicalSumRatio.Subtract(a, b);
            var resultByOperator = a - b;

            Assert.Equal(expectedResult, resultByMethod);
            Assert.Equal(expectedResult, resultByOperator);
        }

        [Theory]
        [MemberData(nameof(TestCases_ToDouble))]
        public void Test_ToDouble(RadicalSumRatio value, double expected)
            => Assert.Equal(expected.ToString("0.000000"), value.ToDouble().ToString("0.000000"));

        [Theory]
        [MemberData(nameof(TestCases_ToRational))]
        public void Test_ToRational(RadicalSumRatio value, Rational expected)
            => Assert.Equal(expected, value.ToRational());
    }
}
