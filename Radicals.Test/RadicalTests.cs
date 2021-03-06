﻿using Rationals;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xunit;

namespace Radicals.Test
{
    public class RadicalTests
    {
        [Fact]
        public void ConstructorTests()
        {
            var testCases = new List<Tuple<Radical, Radical>>()
            {
                // (3/4) * sqrt(12)
                // = (2*3/4) * sqrt(3)
                // = (3/2) * sqrt(3)
                new Tuple<Radical, Radical>(
                    new Radical(new Rational(3, 4), 12), 
                    new Radical(new Rational(3, 2), 3)),
                // sqrt(2/9) = (1/3)sqrt(2)
                new Tuple<Radical, Radical>(
                    new Radical(new Rational(2,9)), 
                    new Radical(new Rational(1, 3), 2)),
                // sqrt(1/2) = (1/2)sqrt(2)
                new Tuple<Radical, Radical>(
                    new Radical(new Rational(1, 2)),
                    new Radical(new Rational(1, 2), 2)),
                // Nth-Root[n:3](8)
                new Tuple<Radical, Radical>(
                    new Radical(1, 8, 3),
                    new Radical(2, 1)),
                // Nth-Root[n:4](4) = 2^(2/4) = 2^(1/2) = Sqrt(2)
                new Tuple<Radical, Radical>(
                    new Radical(1, 4, 4),
                    new Radical(2)),
        };
            foreach (var testCase in testCases)
            {
                Assert.Equal(testCase.Item2, testCase.Item1);
            }
        }

        [Fact]
        public void IsZeroTest()
        {
            var testCases = new List<Radical>()
            {
                // 0 = 0
                0,
                new Radical(0),
                new Radical(0, 0),
                new Radical(),
                new Radical(3, 0),
                new Radical(0, 5),
            };
            foreach (var testCase in testCases)
            {
                Assert.True(testCase.IsZero);
            }
        }

        [Fact]
        public void IsOneTest()
        {
            var testCases = new List<Radical>()
            {
                // 1 = 1
                1,
                new Radical(1, 1),
                new Radical(1),
                Radical.One,
            };
            foreach (var testCase in testCases)
            {
                Assert.True(testCase.IsOne);
            }
        }

        [Fact]
        public void SqrtTest()
        {
            var testCases = new List<Tuple<Radical, Radical>>()
            {
                // Sqrt constructor: Sqrt(3/4) = (1/2)*Sqrt(3)
                new Tuple<Radical, Radical>(
                     Radical.Sqrt((Rational)3 / 4),
                     new Radical((Rational)1 / 2, 3)),
                // Sqrt(4) = 2
                new Tuple<Radical, Radical>(
                    Radical.Sqrt(4),
                    2),
                // Sqrt((5/7)*Nth-Root[n:3](11)) = (1/7)Nth-Root[n:6](125*343*11)
                //                               = (1/7)Nth-Root[n:6](‭471625‬)
                new Tuple<Radical, Radical>(
                    Radical.Sqrt(new Radical(new Rational(5,7), 11, 3)),
                    new Radical(new Rational(1, 7), 471625, 6)),
            };
            foreach (var testCase in testCases)
            {
                Assert.Equal(testCase.Item2, testCase.Item1);
            }
        }

        [Fact]
        public void NthRootTest()
        {
            var testCases = new List<Tuple<Radical, Radical>>()
            {
                // Nth-Root[n:3](8)
                new Tuple<Radical, Radical>(
                    Radical.NthRoot(8, 3),
                    new Radical(2, 1)),
                // Nth-Root[n:4](4) = 2^(2/4) = 2^(1/2) = Sqrt(2)
                new Tuple<Radical, Radical>(
                    Radical.NthRoot(4, 4),
                    new Radical(2)),
                // Nth-Root[n:2](Sqrt(2)) = Nth-Root[n:4](2)
                new Tuple<Radical, Radical>(
                    Radical.NthRoot(Radical.Sqrt(2), 2),
                    new Radical(1, 2, 4)),
                // Nth-Root[n:3](125) = 5
                new Tuple<Radical, Radical>(
                    Radical.NthRoot(125, 3),
                    5),
                // Nth-Root[m:5]((5/7)*Nth-Root[n:3](11)) 
                //              = (1/7)Nth-Root[n:15](125*‭13841287201‬*11)
                //              = (1/7)Nth-Root[n:15](‭19031769901375‬‬)
                //              = ‭1.0969847037974137166744190628521‬
                new Tuple<Radical, Radical>(
                    Radical.NthRoot(new Radical(new Rational(5,7), 11, 3), 5),
                    new Radical(new Rational(1, 7), BigInteger.Parse("19031769901375"), 15)),
            };
            foreach (var testCase in testCases)
            {
                Assert.Equal(testCase.Item2, testCase.Item1);
            }
        }

        [Fact]
        public void AdditionTests()
        {
            var testCases = new List<Tuple<Radical[], RadicalSum>>()
            {
                // sqrt(2) + sqrt(3)
                new Tuple<Radical[], RadicalSum>(
                    new Radical[] 
                    {
                        new Radical(1, 2),
                        new Radical(1, 3), 
                    },
                    new RadicalSum( new Radical[2] { new Radical(1, 2), new Radical(1, 3) })),
                // sqrt(2) + 2 * sqrt(2) = 3 * sqrt(2)
                new Tuple<Radical[], RadicalSum>(
                    new Radical[]
                    {
                        new Radical(1, 2),
                        new Radical(2, 2),
                    },
                    new RadicalSum(new Radical[1] { new Radical(3, 2) })),
                // 5*sqrt(27) + 7*sqrt(12) = 15*sqrt(3) + 14*sqrt(3) = 29*sqrt(3)
                new Tuple<Radical[], RadicalSum>(
                    new Radical[]
                    {
                        new Radical(5, 27),
                        new Radical(7, 12),
                    },
                    new RadicalSum( new Radical[1] { new Radical(29, 3) })),
                // 3*sqrt(2) + 2*sqrt(3)
                new Tuple<Radical[], RadicalSum>(
                    new Radical[]
                    {
                        new Radical(3, 2),
                        new Radical(2, 3),
                    },
                    new RadicalSum(new Radical[2] { new Radical(3, 2), new Radical(2, 3) })),
                // (3/2)*sqrt(5) + 0 = (3/2)*sqrt(5)
                // 0 + (3/2)*sqrt(5) = (3/2)*sqrt(5)
                new Tuple<Radical[], RadicalSum>(
                    new Radical[]
                    {
                        new Radical(new Rational(3, 2), 5),
                        Radical.Zero,
                    },
                    new RadicalSum(new Radical[1] { new Radical(new Rational(3, 2), 5) })),
                new Tuple<Radical[], RadicalSum>(
                    new Radical[]
                    {
                        new Radical(new Rational(3, 2), 5),
                        0,
                    },
                    new RadicalSum(new Radical[1] { new Radical(new Rational(3, 2), 5) })),
                new Tuple<Radical[], RadicalSum>(
                    new Radical[]
                    {
                        0,
                        new Radical(new Rational(3, 2), 5),
                    },
                    new RadicalSum(new Radical[1] { new Radical(new Rational(3, 2), 5) })),
                new Tuple<Radical[], RadicalSum>(
                    new Radical[]
                    {
                        Radical.Zero,
                        new Radical(new Rational(3, 2), 5),
                    },
                    new RadicalSum(new Radical[1] { new Radical(new Rational(3, 2), 5) })),
                // (3/2)*sqrt(5) + (-3/2)*sqrt(5) = 0
                new Tuple<Radical[], RadicalSum>(
                    new Radical[]
                    {
                        new Radical(new Rational(3, 2), 5),
                        new Radical(new Rational(-3, 2), 5),
                    },
                    new RadicalSum(new Radical[1] { Radical.Zero })),
                // 2*sqrt(2) + 5*sqrt(28) + sqrt(1/2) + 3 + sqrt(7/9) + 11*sqrt(4) = 25 + (5/2)sqrt(2) + (31/3)*sqrt(7)
                new Tuple<Radical[], RadicalSum>(
                    new Radical[]
                    {
                        new Radical(2, 2),
                        new Radical(5, 28),
                        new Radical(new Rational(1, 2)),
                        new Radical(3, 1),
                        new Radical(new Rational(7, 9)),
                        new Radical(11, 4)
                    },
                    new RadicalSum(new Radical[3] {
                        new Radical(25, 1),
                        new Radical(new Rational(5,2),2),
                        new Radical(new Rational(31,3),7)
                    })),
            };
            foreach (var testCase in testCases)
            {
                var actual = RadicalSum.Zero;
                foreach (var radical in testCase.Item1)
                {
                    actual += radical;
                }
                Assert.Equal(testCase.Item2, actual);
            }
        }

        [Fact]
        public void SubtractionTests()
        {
            // sqrt(2) - sqrt(3)
            var b11 = new Radical(1, 2);
            var b12 = new Radical(1, 3);
            var actual1 = b11 - b12;
            var expected1 = new RadicalSum(new Radical[2] { new Radical(1, 2), new Radical(-1, 3) });
            // sqrt(2) - 2 * sqrt(2) = -1 * sqrt(2)
            var b21 = new Radical(1, 2);
            var b22 = new Radical(2, 2);
            var actual2 = b21 - b22;
            var expected2 = new RadicalSum(new Radical[1] { new Radical(-1, 2) });
            // 5*sqrt(27) - 7*sqrt(12) = 15*sqrt(3) - 14*sqrt(3) = 1*sqrt(3)
            var b31 = new Radical(5, 27);
            var b32 = new Radical(7, 12);
            var actual3 = b31 - b32;
            var expected3 = new RadicalSum(new Radical[1] { new Radical(1, 3) });
            // 3*sqrt(2) - 2*sqrt(3)
            var b41 = new Radical(3, 2);
            var b42 = new Radical(2, 3);
            var actual4 = b41 - b42;
            var expected4 = new RadicalSum(new Radical[2] { new Radical(3, 2), new Radical(-2, 3) });
            // 2*sqrt(2) + 5*sqrt(28) - sqrt(1/2) + 3 - sqrt(7/9) + 11*sqrt(4) = 25 + (3/2)sqrt(2) + (29/3)*sqrt(7)
            var b51 = new Radical(2, 2);
            var b52 = new Radical(5, 28);
            var b53 = new Radical(new Rational(1, 2));
            var b54 = new Radical(3, 1);
            var b55 = new Radical(new Rational(7, 9));
            var b56 = new Radical(11, 4);
            var actual5 = b51 + b52 - b53 + b54 - b55 + b56;
            var expected5 = new RadicalSum(new Radical[3] {
                new Radical(25, 1),
                new Radical(new Rational(3,2),2),
                new Radical(new Rational(29,3),7)
            });
            // (3/2)*sqrt(5) - 0 = (3/2)*sqrt(5)
            var b61 = new Radical(new Rational(3, 2), 5);
            var b62 = Radical.Zero;
            var actual6 = b61 - b62;
            var expected6 = new RadicalSum(new Radical[1] { new Radical(new Rational(3, 2), 5) });
            // 0 - (3/2)*sqrt(5) = (-3/2)*sqrt(5)
            var b71 = Radical.Zero;
            var b72 = new Radical(new Rational(3, 2), 5);
            var actual7 = b71 - b72;
            var expected7 = new RadicalSum(new Radical[1] { new Radical(new Rational(-3, 2), 5) });
            // (3/2)*sqrt(5) - (3/2)*sqrt(5) = 0
            var b81 = new Radical(new Rational(3, 2), 5);
            var b82 = new Radical(new Rational(3, 2), 5);
            var actual8 = b81 - b82;
            var expected8 = new RadicalSum(new Radical[1] { Radical.Zero });

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
            Assert.Equal(expected4, actual4);
            Assert.Equal(expected5, actual5);
            Assert.Equal(expected6, actual6);
            Assert.Equal(expected7, actual7);
            Assert.Equal(expected8, actual8);
        }

        [Fact]
        public void MultiplicationTests()
        {
            // (3*sqrt(2)) * ((5/3)*sqrt(3)) = (15/3)*sqrt(6) = 5*sqrt(6)
            var b11 = new Radical(3, 2);
            var b12 = new Radical(new Rational(5, 3), 3);
            var actual1 = b11 * b12;
            var expected1 = new Radical(5, 6);
            // 11 * sqrt(4/9) = 22/3
            var b21 = new Radical(11, 1);
            var b22 = new Radical(new Rational(4, 9));
            var actual2 = b21 * b22;
            var expected2 = new Radical(new Rational(22,3), 1);
            // 11 * sqrt(4/9) = 22/3
            var b31 = 11;
            var b32 = new Radical(new Rational(4, 9));
            var actual31 = b31 * b32;
            var actual32 = b32 * b31;
            var expected3 = new Radical(new Rational(22, 3), 1);
            // [(3/2)*sqrt(2) - (7/3)*sqrt(5) + (1/3)*sqrt(2) - (7/5)*sqrt(5)] * (11/4)*sqrt(2)
            // = [(11/6)*sqrt(2) - (56/15)*sqrt(5)] * (11/4)*sqrt(2)
            // = (121/24)*2 - (154/15)*sqrt(10)
            // = (121/12) - (56/15)*sqrt(10)
            var b41 = new Radical(new Rational(3, 2), 2);
            var b42 = new Radical(new Rational(7, 3), 5);
            var b43 = new Radical(new Rational(1, 3), 2);
            var b44 = new Radical(new Rational(7, 5), 5);
            var b45 = new Radical(new Rational(11, 4), 2);
            var actual41 = (b41 - b42 + b43 - b44) * b45;
            var actual42 = b45 * (b41 - b42 + b43 - b44);
            var expected4 = new RadicalSum(new Radical[2] {
                new Radical(new Rational(121, 12), 1),
                new Radical(new Rational(-154, 15), 10) });
            // (3/2)*sqrt(5) * 1 = (3/2)*sqrt(5)
            var b51 = new Radical(new Rational(3, 2), 5);
            var b52 = new Radical(1, 1);
            var b53 = 1;
            var b54 = new Radical(1);
            var b55 = Radical.One;
            var actual51 = b51 * b52;
            var actual52 = b51 * b53;
            var actual53 = b51 * b54;
            var actual54 = b51 * b55;
            var actual55 = b52 * b51;
            var actual56 = b53 * b51;
            var actual57 = b54 * b51;
            var actual58 = b55 * b51;
            var expected5 = new Radical(new Rational(3, 2), 5);
            // (3/2)*sqrt(5) * -1 = (-3/2)*sqrt(5)
            var b61 = new Radical(new Rational(3, 2), 5);
            var b62 = -Radical.One;
            var b63 = -1;
            var b64 = new Radical(-1, 1);
            var actual61 = b61 * b62;
            var actual62 = b61 * b63;
            var actual63 = b61 * b64;
            var actual64 = b62 * b61;
            var actual65 = b63 * b61;
            var actual66 = b64 * b61;
            var expected6 = new Radical(new Rational(-3, 2), 5);
            // (3/2)*sqrt(5) * 0 = 0
            var b71 = new Radical(new Rational(3, 2), 5);
            var b72 = 0;
            var b73 = Radical.Zero;
            var b74 = new Radical(0);
            var b75 = new Radical(0, 0);
            var actual71 = b71 * b72;
            var actual72 = b71 * b73;
            var actual73 = b71 * b74;
            var actual74 = b71 * b75;
            var actual75 = b72 * b71;
            var actual76 = b73 * b71;
            var actual77 = b74 * b71;
            var actual78 = b75 * b71;
            var expected7 = Radical.Zero;
            // (3/2)*sqrt(5) Inverse
            var b8_1 = new Radical((Rational)3 / 2, 5);
            var b8_2 = Radical.Invert(b8_1);
            var actual8 = b8_1 * b8_2;
            var expected8 = Radical.One;
            // Sqrt(2) * Root[3](5) = Root[6](2^3) * Root[6](5^2)
            // = Root[6](8*25) = Root[6](200)
            var b9_1 = new Radical(2);
            var b9_2 = Radical.NthRoot(5, 3);
            var actual9 = b9_1 * b9_2;
            var expected9 = new Radical(1, 200, 6);

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual31);
            Assert.Equal(expected3, actual32);
            Assert.Equal(expected4, actual41);
            Assert.Equal(expected4, actual42);
            Assert.Equal(expected5, actual51);
            Assert.Equal(expected5, actual52);
            Assert.Equal(expected5, actual53);
            Assert.Equal(expected5, actual54);
            Assert.Equal(expected5, actual55);
            Assert.Equal(expected5, actual56);
            Assert.Equal(expected5, actual57);
            Assert.Equal(expected5, actual58);
            Assert.Equal(expected6, actual61);
            Assert.Equal(expected6, actual62);
            Assert.Equal(expected6, actual63);
            Assert.Equal(expected6, actual64);
            Assert.Equal(expected6, actual65);
            Assert.Equal(expected6, actual66);
            Assert.Equal(expected7, actual71);
            Assert.Equal(expected7, actual72);
            Assert.Equal(expected7, actual73);
            Assert.Equal(expected7, actual74);
            Assert.Equal(expected7, actual75);
            Assert.Equal(expected7, actual76);
            Assert.Equal(expected7, actual77);
            Assert.Equal(expected7, actual78);
            Assert.Equal(expected8, actual8);
            Assert.Equal(expected9, actual9);
        }

        [Fact]
        public void DivisionTests()
        {
            // (3*sqrt(2)) / ((5/3)*sqrt(3)) = (9/5)*sqrt(2/3) = (3/5)*sqrt(6)
            var b11 = new Radical(3, 2);
            var b12 = new Radical(new Rational(5, 3), 3);
            var actual1 = b11 / b12;
            var expected1 = new Radical(new Rational(3,5), 6);
            // 11 / sqrt(4/9) = 33/2
            var b21 = new Radical(11, 1);
            var b22 = new Radical(new Rational(4, 9));
            var actual2 = b21 / b22;
            var expected2 = new Radical(new Rational(33, 2), 1);
            // 11 / sqrt(4/9) = 33/2
            var b31 = 11;
            var b32 = new Radical(new Rational(4, 9));
            var actual3 = b31 / b32;
            var expected3 = new Radical(new Rational(33, 2), 1);
            // sqrt(4/9) / 11 = 2/33
            var b41 = new Radical(new Rational(4, 9));
            var b42 = 11;
            var actual4 = b41 / b42;
            var expected4 = new Radical(new Rational(2, 33), 1);
            // [(3/2)*sqrt(2) - (7/3)*sqrt(5) + (1/3)*sqrt(2) - (7/5)*sqrt(5)] / (11/4)*sqrt(2)
            // = [(11/6)*sqrt(2) - (56/15)*sqrt(5)] / (11/4)*sqrt(2)
            // = (2/3) - (224/165)*sqrt(5/2)
            // = (2/3) - (112/165)*sqrt(10)
            var b51 = new Radical(new Rational(3, 2), 2);
            var b52 = new Radical(new Rational(7, 3), 5);
            var b53 = new Radical(new Rational(1, 3), 2);
            var b54 = new Radical(new Rational(7, 5), 5);
            var b55 = new Radical(new Rational(11, 4), 2);
            var actual5 = (b51 - b52 + b53 - b54) / b55;
            var expected5 = new RadicalSum(new Radical[2] {
                new Radical(new Rational(2, 3), 1),
                new Radical(new Rational(-112, 165), 10) });
            // (3/2)*sqrt(5) / 1 = (3/2)*sqrt(5)
            var b61 = new Radical(new Rational(3, 2), 5);
            var b62 = new Radical(1, 1);
            var b63 = 1;
            var b64 = new Radical(1);
            var b65 = Radical.One;
            var actual61 = b61 / b62;
            var actual62 = b61 / b63;
            var actual63 = b61 / b64;
            var actual64 = b61 / b65;
            var expected6 = new Radical(new Rational(3, 2), 5);
            // (3/2)*sqrt(5) / -1 = (-3/2)*sqrt(5)
            var b71 = new Radical(new Rational(3, 2), 5);
            var b72 = new Radical(-1, 1);
            var b73 = -1;
            var b74 = -Radical.One;
            var actual71 = b71 / b72;
            var actual72 = b71 / b73;
            var actual73 = b71 / b74;
            var expected7 = new Radical(new Rational(-3, 2), 5);
            // Sqrt(2) / Root[3](5) = Sqrt(2) * (1/5)*Root[3](5^2) = Root[6](2^3) * (1/5)*Root[6](5^4)
            // = (1/5)*Root[6](8*625) = Root[6](5000)
            var b9_1 = new Radical(2);
            var b9_2 = Radical.NthRoot(5, 3);
            var actual9 = b9_1 / b9_2;
            var expected9 = new Radical((Rational)1/5, 5000, 6);

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
            Assert.Equal(expected4, actual4);
            Assert.Equal(expected5, actual5);
            Assert.Equal(expected6, actual61);
            Assert.Equal(expected6, actual62);
            Assert.Equal(expected6, actual63);
            Assert.Equal(expected6, actual64);
            Assert.Equal(expected7, actual71);
            Assert.Equal(expected7, actual72);
            Assert.Equal(expected7, actual73);
            Assert.Equal(expected9, actual9);
        }
    }
}
