﻿using Rationals;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xunit;

namespace Radicals.Test
{
    public class RadicalSumRatioTests
    {
        [Fact]
        public void ConstructorTests()
        {
            // (3/4) * sqrt(12)
            // = (2*3/4) * sqrt(3)
            // = (3/2) * sqrt(3)
            var actual1 = new RadicalSumRatio(new Rational(3, 4), 12);
            var expected1 = new RadicalSumRatio(new Rational(3, 2), 3);
            // sqrt(2/9) = (1/3)sqrt(2)
            var actual2 = new RadicalSumRatio(new Rational(2, 9));
            var expected2 = new RadicalSumRatio(new Rational(1, 3), 2);
            // sqrt(1/2) = (1/2)sqrt(2)
            var actual3 = new RadicalSumRatio(new Rational(1, 2));
            var expected3 = new RadicalSumRatio(new Rational(1, 2), 2);
            // 0 = 0
            RadicalSumRatio actual41 = 0;
            var actual42 = new RadicalSumRatio(0);
            var actual43 = new RadicalSumRatio(0, 0);
            var actual44 = new RadicalSumRatio();
            var actual45 = new RadicalSumRatio(3, 0);
            var actual46 = new RadicalSumRatio(0, 5);
            var expected4 = RadicalSumRatio.Zero;
            // 1 = 1
            RadicalSumRatio actual51 = 1;
            var actual52 = new RadicalSumRatio(1, 1);
            var actual53 = new RadicalSumRatio(1);
            var expected5 = RadicalSumRatio.One;
            // [(3/2)*sqrt(7) - (2/3)*sqrt(5)] / [(1/2)*sqrt(7) + (4/5)*sqrt(6)]
            var c61 = new RadicalSum(new Radical[2] {
                new Radical(new Rational(3,2), 7),
                new Radical(new Rational(-2,3), 5)
            });
            var c62 = new RadicalSum(new Radical[2] {
                new Radical(new Rational(1,2), 7),
                new Radical(new Rational(4,5), 6)
            });
            var actual6 = (RadicalSumRatio)c61 / c62;
            var expected6 = new RadicalSumRatio(c61, c62);


            // assert
            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
            Assert.Equal(expected4, actual41);
            Assert.Equal(expected4, actual42);
            Assert.Equal(expected4, actual43);
            Assert.Equal(expected4, actual44);
            Assert.Equal(expected4, actual45);
            Assert.Equal(expected4, actual46);
            Assert.True(actual41.IsZero);
            Assert.True(actual42.IsZero);
            Assert.True(actual43.IsZero);
            Assert.True(actual44.IsZero);
            Assert.True(actual45.IsZero);
            Assert.True(actual46.IsZero);
            Assert.Equal(expected5, actual51);
            Assert.Equal(expected5, actual52);
            Assert.Equal(expected5, actual53);
            Assert.True(actual51.IsOne);
            Assert.True(actual52.IsOne);
            Assert.True(actual53.IsOne);
            Assert.Equal(expected6, actual6);
        }

        [Fact]
        public void AdditionTests()
        {
            // sqrt(2) + sqrt(3)
            var b11 = new RadicalSumRatio(1, 2);
            var b12 = new RadicalSumRatio(1, 3);
            var actual1 = b11 + b12;
            var expected1 = new RadicalSumRatio(new Radical[2] { new Radical(1, 2), new Radical(1, 3) });
            // sqrt(2) + 2 * sqrt(2) = 3 * sqrt(2)
            var b21 = new RadicalSumRatio(1, 2);
            var b22 = new RadicalSumRatio(2, 2);
            var actual2 = b21 + b22;
            var expected2 = new RadicalSumRatio(new Radical(3, 2));
            // 5*sqrt(27) + 7*sqrt(12) = 15*sqrt(3) + 14*sqrt(3) = 29*sqrt(3)
            var b31 = new RadicalSumRatio(5, 27);
            var b32 = new RadicalSumRatio(7, 12);
            var actual3 = b31 + b32;
            var expected3 = new RadicalSumRatio(new Radical(29, 3));
            // 3*sqrt(2) + 2*sqrt(3)
            var b41 = new RadicalSumRatio(3, 2);
            var b42 = new RadicalSumRatio(2, 3);
            var actual4 = b41 + b42;
            var expected4 = new RadicalSumRatio(new Radical[2] { new Radical(3, 2), new Radical(2, 3) });
            // 2*sqrt(2) + 5*sqrt(28) + sqrt(1/2) + 3 + sqrt(7/9) + 11*sqrt(4) = 25 + (5/2)sqrt(2) + (31/3)*sqrt(7)
            var b51 = new RadicalSumRatio(2, 2);
            var b52 = new RadicalSumRatio(5, 28);
            var b53 = new RadicalSumRatio(new Rational(1, 2));
            var b54 = new RadicalSumRatio(3, 1);
            var b55 = new RadicalSumRatio(new Rational(7, 9));
            var b56 = new RadicalSumRatio(11, 4);
            var actual5 = b51 + b52 + b53 + b54 + b55 + b56;
            var expected5 = new RadicalSumRatio(new Radical[3] {
                new Radical(25, 1),
                new Radical(new Rational(5,2),2),
                new Radical(new Rational(31,3),7)
            });
            // (3/2)*sqrt(5) + 0 = (3/2)*sqrt(5)
            // 0 + (3/2)*sqrt(5) = (3/2)*sqrt(5)
            var b61 = new RadicalSumRatio(new Rational(3, 2), 5);
            var b62 = RadicalSumRatio.Zero;
            var actual61 = b61 + b62;
            var actual62 = b62 + b61;
            var expected6 = new RadicalSumRatio(new Radical(new Rational(3, 2), 5));
            // (3/2)*sqrt(5) + (-3/2)*sqrt(5) = 0
            var b71 = new RadicalSumRatio(new Rational(3, 2), 5);
            var b72 = new RadicalSumRatio(new Rational(-3, 2), 5);
            var actual7 = b71 + b72;
            var expected7 = RadicalSumRatio.Zero;
            // {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]} + {[(5/6)*sqrt(3) + (7/3)*sqrt(2)] / [(4/3)*sqrt(3) - (5/7)*sqrt(2)]}
            // = [(8/9)*3 - (10/21)*sqrt(6) - (12/6)*sqrt(6) + (15/14)*2 + (10/18)*3 + (14/9)*sqrt(6) + (15/12)*sqrt(6) + (21/6)*2] / [(8/9)*3 - (10/21)*sqrt(6) + (12/6)*sqrt(6) - (15/14)*2]
            // = [(8/3) + (83/252)*sqrt(6) + (15/7) + (10/6) + (21/3)] / [(8/3) + (32/21)*sqrt(6) - (15/7)]
            // = [(283/21) + (83/252)*sqrt(6)] / [(11/21) + (32/21)*sqrt(6)]
            // = [(283) + (83/12)*sqrt(6)] / [(11) + 32*sqrt(6)]
            var b81 = new Radical(new Rational(2, 3), 3);
            var b82 = new Radical(new Rational(3, 2), 2);
            var b83 = new Radical(new Rational(2, 3), 3);
            var b84 = new Radical(new Rational(3, 2), 2);
            var b85 = new Radical(new Rational(5, 6), 3);
            var b86 = new Radical(new Rational(7, 3), 2);
            var b87 = new Radical(new Rational(4, 3), 3);
            var b88 = new Radical(new Rational(5, 7), 2);
            var c81 = b81 - b82;
            var c82 = b83 + b84;
            var c83 = b85 + b86;
            var c84 = b87 - b88;
            var cr81 = new RadicalSumRatio(c81, c82);
            var cr82 = new RadicalSumRatio(c83, c84);
            var actual8 = cr81 + cr82;
            var expected8 = new RadicalSumRatio(
                new RadicalSum(new Radical[2] {
                    new Radical(new Rational(283), 1),
                    new Radical(new Rational(83,12), 6)
                }),
                new RadicalSum(new Radical[2] {
                    new Radical(new Rational(11),1),
                    new Radical(new Rational(32), 6)
                }));
            // (2/3) + (1/3) = 1
            var b91 = new Radical(2, 1);
            var b92 = new Radical(3, 1);
            var b93 = new Radical(1, 1);
            var b94 = new Radical(3, 1);
            var c91 = new RadicalSum(b91);
            var c92 = new RadicalSum(b92);
            var c93 = new RadicalSum(b93);
            var c94 = new RadicalSum(b94);
            var cr91 = new RadicalSumRatio(c91, c92);
            var cr92 = new RadicalSumRatio(c93, c94);
            var actual9 = cr91 + cr92;
            var expected9 = RadicalSumRatio.One;

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
            Assert.Equal(expected4, actual4);
            Assert.Equal(expected5, actual5);
            Assert.Equal(expected6, actual61);
            Assert.Equal(expected6, actual62);
            Assert.Equal(expected7, actual7);
            Assert.Equal(expected8, actual8);
            Assert.Equal(expected9, actual9);
        }

        [Fact]
        public void SubtractionTests()
        {
            // sqrt(2) - sqrt(3)
            var b11 = new RadicalSumRatio(1, 2);
            var b12 = new RadicalSumRatio(1, 3);
            var actual1 = b11 - b12;
            var expected1 = new RadicalSumRatio(new Radical[2] { new Radical(1, 2), new Radical(-1, 3) });
            // sqrt(2) - 2 * sqrt(2) = -1 * sqrt(2)
            var b21 = new RadicalSumRatio(1, 2);
            var b22 = new RadicalSumRatio(2, 2);
            var actual2 = b21 - b22;
            var expected2 = new RadicalSumRatio(new Radical(-1, 2));
            // 5*sqrt(27) - 7*sqrt(12) = 15*sqrt(3) - 14*sqrt(3) = 1*sqrt(3)
            var b31 = new RadicalSumRatio(5, 27);
            var b32 = new RadicalSumRatio(7, 12);
            var actual3 = b31 - b32;
            var expected3 = new RadicalSumRatio(new Radical(1, 3));
            // 3*sqrt(2) - 2*sqrt(3)
            var b41 = new RadicalSumRatio(3, 2);
            var b42 = new RadicalSumRatio(2, 3);
            var actual4 = b41 - b42;
            var expected4 = new RadicalSumRatio(new Radical[2] { new Radical(3, 2), new Radical(-2, 3) });
            // 2*sqrt(2) + 5*sqrt(28) - sqrt(1/2) + 3 - sqrt(7/9) + 11*sqrt(4) = 25 + (3/2)sqrt(2) + (29/3)*sqrt(7)
            var b51 = new RadicalSumRatio(2, 2);
            var b52 = new RadicalSumRatio(5, 28);
            var b53 = new RadicalSumRatio(new Rational(1, 2));
            var b54 = new RadicalSumRatio(3, 1);
            var b55 = new RadicalSumRatio(new Rational(7, 9));
            var b56 = new RadicalSumRatio(11, 4);
            var actual5 = b51 + b52 - b53 + b54 - b55 + b56;
            var expected5 = new RadicalSumRatio(new Radical[3] {
                new Radical(25, 1),
                new Radical(new Rational(3,2),2),
                new Radical(new Rational(29,3),7)
            });
            // (3/2)*sqrt(5) - 0 = (3/2)*sqrt(5)
            var b61 = new RadicalSumRatio(new Rational(3, 2), 5);
            var b62 = RadicalSumRatio.Zero;
            var actual6 = b61 - b62;
            var expected6 = new RadicalSumRatio(new Radical(new Rational(3, 2), 5));
            // 0 - (3/2)*sqrt(5) = (-3/2)*sqrt(5)
            var b71 = RadicalSumRatio.Zero;
            var b72 = new RadicalSumRatio(new Rational(3, 2), 5);
            var actual7 = b71 - b72;
            var expected7 = new RadicalSumRatio(new Radical(new Rational(-3, 2), 5));
            // (3/2)*sqrt(5) - (3/2)*sqrt(5) = 0
            var b81 = new RadicalSumRatio(new Rational(3, 2), 5);
            var b82 = new RadicalSumRatio(new Rational(3, 2), 5);
            var actual8 = b81 - b82;
            var expected8 = RadicalSumRatio.Zero;
            // {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]} - {[(5/6)*sqrt(3) + (7/3)*sqrt(2)] / [(4/3)*sqrt(3) - (5/7)*sqrt(2)]}
            // = [(8/9)*3 - (10/21)*sqrt(6) - (12/6)*sqrt(6) + (15/14)*2 - (10/18)*3 - (14/9)*sqrt(6) - (15/12)*sqrt(6) - (21/6)*2] / [(8/9)*3 - (10/21)*sqrt(6) + (12/6)*sqrt(6) - (15/14)*2]
            // = [(8/3) - (1331/252)*sqrt(6) + (15/7) - (10/6) - (21/3)] / [(8/3) + (32/21)*sqrt(6) - (15/7)]
            // = [(-27/7) - (1331/252)*sqrt(6)] / [(11/21) + (32/21)*sqrt(6)]
            // = [(-81) - (1331/12)*sqrt(6)] / [(11) + 32*sqrt(6)]
            var b91 = new Radical(new Rational(2, 3), 3);
            var b92 = new Radical(new Rational(3, 2), 2);
            var b93 = new Radical(new Rational(2, 3), 3);
            var b94 = new Radical(new Rational(3, 2), 2);
            var b95 = new Radical(new Rational(5, 6), 3);
            var b96 = new Radical(new Rational(7, 3), 2);
            var b97 = new Radical(new Rational(4, 3), 3);
            var b98 = new Radical(new Rational(5, 7), 2);
            var c91 = b91 - b92;
            var c92 = b93 + b94;
            var c93 = b95 + b96;
            var c94 = b97 - b98;
            var cr91 = new RadicalSumRatio(c91, c92);
            var cr92 = new RadicalSumRatio(c93, c94);
            var actual9 = cr91 - cr92;
            var expected9 = new RadicalSumRatio(
                new RadicalSum(new Radical[2] {
                    new Radical(new Rational(-81), 1),
                    new Radical(new Rational(-1331,12), 6)
                }),
                new RadicalSum(new Radical[2] {
                    new Radical(new Rational(11),1),
                    new Radical(new Rational(32), 6)
                }));
            // (2/3) - (1/3) = 1/3
            var b101 = new Radical(2, 1);
            var b102 = new Radical(3, 1);
            var b103 = new Radical(1, 1);
            var b104 = new Radical(3, 1);
            var c101 = new RadicalSum(b101);
            var c102 = new RadicalSum(b102);
            var c103 = new RadicalSum(b103);
            var c104 = new RadicalSum(b104);
            var cr101 = new RadicalSumRatio(c101, c102);
            var cr102 = new RadicalSumRatio(c103, c104);
            var actual10 = cr101 - cr102;
            var expected10 = new RadicalSumRatio(
                new RadicalSum(new Radical(1,1)),
                new RadicalSum(new Radical(3,1))
                );

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
            Assert.Equal(expected4, actual4);
            Assert.Equal(expected5, actual5);
            Assert.Equal(expected6, actual6);
            Assert.Equal(expected7, actual7);
            Assert.Equal(expected8, actual8);
            Assert.Equal(expected9, actual9);
            Assert.Equal(expected10, actual10);
        }

        [Fact]
        public void MultiplicationTests()
        {
            // (3*sqrt(2)) * ((5/3)*sqrt(3)) = (15/3)*sqrt(6) = 5*sqrt(6)
            var b11 = new RadicalSumRatio(3, 2);
            var b12 = new RadicalSumRatio(new Rational(5, 3), 3);
            var actual1 = b11 * b12;
            var expected1 = new RadicalSumRatio(5, 6);
            // 11 * sqrt(4/9) = 22/3
            var b21 = new RadicalSumRatio(11, 1);
            var b22 = new RadicalSumRatio(new Rational(4, 9));
            var actual2 = b21 * b22;
            var expected2 = new RadicalSumRatio(new Rational(22, 3), 1);
            // 11 * sqrt(4/9) = 22/3
            var b31 = 11;
            var b32 = new RadicalSumRatio(new Rational(4, 9));
            var actual31 = b31 * b32;
            var actual32 = b32 * b31;
            var expected3 = new RadicalSumRatio(new Rational(22, 3), 1);
            // [(3/2)*sqrt(2) - (7/3)*sqrt(5) + (1/3)*sqrt(2) - (7/5)*sqrt(5)] * (11/4)*sqrt(2)
            // = [(11/6)*sqrt(2) - (56/15)*sqrt(5)] * (11/4)*sqrt(2)
            // = (121/24)*2 - (154/15)*sqrt(10)
            // = (121/12) - (56/15)*sqrt(10)
            var b41 = new RadicalSumRatio(new Rational(3, 2), 2);
            var b42 = new RadicalSumRatio(new Rational(7, 3), 5);
            var b43 = new RadicalSumRatio(new Rational(1, 3), 2);
            var b44 = new RadicalSumRatio(new Rational(7, 5), 5);
            var b45 = new RadicalSumRatio(new Rational(11, 4), 2);
            var actual41 = (b41 - b42 + b43 - b44) * b45;
            var actual42 = b45 * (b41 - b42 + b43 - b44);
            var expected4 = new RadicalSumRatio(new Radical[2] {
                new Radical(new Rational(121, 12), 1),
                new Radical(new Rational(-154, 15), 10) });
            // (3/2)*sqrt(5) * 1 = (3/2)*sqrt(5)
            var b51 = new RadicalSumRatio(new Rational(3, 2), 5);
            var b52 = new RadicalSumRatio(1, 1);
            var b53 = 1;
            var b54 = new RadicalSumRatio(1);
            var b55 = RadicalSumRatio.One;
            var actual51 = b51 * b52;
            var actual52 = b51 * b53;
            var actual53 = b51 * b54;
            var actual54 = b51 * b55;
            var actual55 = b52 * b51;
            var actual56 = b53 * b51;
            var actual57 = b54 * b51;
            var actual58 = b55 * b51;
            var expected5 = new RadicalSumRatio(new Rational(3, 2), 5);
            // (3/2)*sqrt(5) * -1 = (-3/2)*sqrt(5)
            var b61 = new RadicalSumRatio(new Rational(3, 2), 5);
            var b62 = -RadicalSumRatio.One;
            var b63 = -1;
            var b64 = new RadicalSumRatio(-1, 1);
            var actual61 = b61 * b62;
            var actual62 = b61 * b63;
            var actual63 = b61 * b64;
            var actual64 = b62 * b61;
            var actual65 = b63 * b61;
            var actual66 = b64 * b61;
            var expected6 = new RadicalSumRatio(new Rational(-3, 2), 5);
            // (3/2)*sqrt(5) * 0 = 0
            var b71 = new RadicalSumRatio(new Rational(3, 2), 5);
            var b72 = 0;
            var b73 = RadicalSumRatio.Zero;
            var b74 = new RadicalSumRatio(0);
            var b75 = new RadicalSumRatio(0, 0);
            var actual71 = b71 * b72;
            var actual72 = b71 * b73;
            var actual73 = b71 * b74;
            var actual74 = b71 * b75;
            var actual75 = b72 * b71;
            var actual76 = b73 * b71;
            var actual77 = b74 * b71;
            var actual78 = b75 * b71;
            var expected7 = RadicalSumRatio.Zero;
            // [(5/3)*sqrt(7) - (2/9)*sqrt(11) - 6*sqrt(13)] * [2*sqrt(6) - 3*sqrt(8)]
            // = (10/3)*sqrt(42) - (4/9)*sqrt(66) - 12*sqrt(78) - 5*sqrt(56) + (2/3)*sqrt(88) + 18*sqrt(104)
            // = (10/3)*sqrt(42) - 5*sqrt(56) - (4/9)*sqrt(66) - (34/3)*sqrt(88) + 18*sqrt(104) - 12*sqrt(78)
            // = -10*sqrt(14) + (4/3)*sqrt(22) + 36*sqrt(26) + (10/3)*sqrt(42) - (4/9)*sqrt(66) - 12*sqrt(78)
            var b81 = new RadicalSumRatio(new Rational(5, 3), 7);
            var b82 = new RadicalSumRatio(new Rational(2, 9), 11);
            var b83 = new RadicalSumRatio(6, 13);
            var b84 = new RadicalSumRatio(2, 6);
            var b85 = new RadicalSumRatio(3, 8);
            var c81 = b81 - b82 - b83;
            var c82 = b84 - b85;
            var actual8 = c81 * c82;
            var expected8 = new RadicalSumRatio(new Radical[6] {
                new Radical(-10, 14),
                new Radical(new Rational(4,3),22),
                new Radical(36, 26),
                new Radical(new Rational(10,3),42),
                new Radical(new Rational(-4,9),66),
                new Radical(-12,78)
            });
            // {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]} * {[(5/6)*sqrt(3) + (7/3)*sqrt(2)] / [(4/3)*sqrt(3) - (5/7)*sqrt(2)]}
            // = [(10/18)*3 + (14/9)*sqrt(6) - (15/12)*sqrt(6) - (21/6)*2] / [(8/9)*3 - (10/21)*sqrt(6) + (12/6)*sqrt(6) - (15/14)*2]
            // = [(-16/3) + (11/36)*sqrt(6)] / [(11/21) + (32/21)*sqrt(6)]
            // = [(-112) + (77/12)*sqrt(6)] / [11 + 32*sqrt(6)]
            var b91 = new Radical(new Rational(2, 3), 3);
            var b92 = new Radical(new Rational(3, 2), 2);
            var b93 = new Radical(new Rational(2, 3), 3);
            var b94 = new Radical(new Rational(3, 2), 2);
            var b95 = new Radical(new Rational(5, 6), 3);
            var b96 = new Radical(new Rational(7, 3), 2);
            var b97 = new Radical(new Rational(4, 3), 3);
            var b98 = new Radical(new Rational(5, 7), 2);
            var c91 = b91 - b92;
            var c92 = b93 + b94;
            var c93 = b95 + b96;
            var c94 = b97 - b98;
            var cr91 = new RadicalSumRatio(c91, c92);
            var cr92 = new RadicalSumRatio(c93, c94);
            var actual9 = cr91 * cr92;
            var expected9 = new RadicalSumRatio(
                new RadicalSum(new Radical[2] {
                    new Radical(new Rational(-112), 1),
                    new Radical(new Rational(77,12), 6)
                }),
                new RadicalSum(new Radical[2] {
                    new Radical(new Rational(11),1),
                    new Radical(new Rational(32), 6)
                }));

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
            var b11 = new RadicalSumRatio(3, 2);
            var b12 = new RadicalSumRatio(new Rational(5, 3), 3);
            var actual1 = b11 / b12;
            var expected1 = new RadicalSumRatio(new Rational(3, 5), 6);
            // 11 / sqrt(4/9) = 33/2
            var b21 = new RadicalSumRatio(11, 1);
            var b22 = new RadicalSumRatio(new Rational(4, 9));
            var actual2 = b21 / b22;
            var expected2 = new RadicalSumRatio(new Rational(33, 2), 1);
            // sqrt(4/9) / 11 = 2/33
            var b41 = new RadicalSumRatio(new Rational(4, 9));
            var b42 = 11;
            var actual4 = b41 / b42;
            var expected4 = new RadicalSumRatio(new Rational(2, 33), 1);
            // [(3/2)*sqrt(2) - (7/3)*sqrt(5) + (1/3)*sqrt(2) - (7/5)*sqrt(5)] / (11/4)*sqrt(2)
            // = [(11/6)*sqrt(2) - (56/15)*sqrt(5)] / (11/4)*sqrt(2)
            // = (2/3) - (224/165)*sqrt(5/2)
            // = (2/3) - (112/165)*sqrt(10)
            var b51 = new RadicalSumRatio(new Rational(3, 2), 2);
            var b52 = new RadicalSumRatio(new Rational(7, 3), 5);
            var b53 = new RadicalSumRatio(new Rational(1, 3), 2);
            var b54 = new RadicalSumRatio(new Rational(7, 5), 5);
            var b55 = new RadicalSumRatio(new Rational(11, 4), 2);
            var actual5 = (b51 - b52 + b53 - b54) / b55;
            var expected5 = new RadicalSumRatio(new Radical[2] {
                new Radical(new Rational(2, 3), 1),
                new Radical(new Rational(-112, 165), 10) });
            // (3/2)*sqrt(5) / 1 = (3/2)*sqrt(5)
            var b61 = new RadicalSumRatio(new Rational(3, 2), 5);
            var b62 = new RadicalSumRatio(1, 1);
            var b63 = 1;
            var b64 = new Radical(1);
            var b65 = Radical.One;
            var actual61 = b61 / b62;
            var actual62 = b61 / b63;
            var actual63 = b61 / b64;
            var actual64 = b61 / b65;
            var expected6 = new RadicalSumRatio(new Rational(3, 2), 5);
            // (3/2)*sqrt(5) / -1 = (-3/2)*sqrt(5)
            var b71 = new RadicalSumRatio(new Rational(3, 2), 5);
            var b72 = new Radical(-1, 1);
            var b73 = -1;
            var b74 = -Radical.One;
            var actual71 = b71 / b72;
            var actual72 = b71 / b73;
            var actual73 = b71 / b74;
            var expected7 = new RadicalSumRatio(new Rational(-3, 2), 5);
            // {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]} / {[(5/6)*sqrt(3) + (7/3)*sqrt(2)] / [(4/3)*sqrt(3) - (5/7)*sqrt(2)]}
            // = {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]} * {[(4/3)*sqrt(3) - (5/7)*sqrt(2)] / [(5/6)*sqrt(3) + (7/3)*sqrt(2)]}
            // = [(8/9)*3 - (10/21)*sqrt(6) - (12/6)*sqrt(6) + (15/14)*2] / [(10/18)*3 + (14/9)*sqrt(6) + (15/12)*sqrt(6) + (21/6)*2]
            // = [(101/21) - (52/21)*sqrt(6)] / [(26/3) + (101/36)*sqrt(6)]
            // = [(101/7) - (52/7)*sqrt(6)] / [26 + (101/12)*sqrt(6)]
            var b91 = new Radical(new Rational(2, 3), 3);
            var b92 = new Radical(new Rational(3, 2), 2);
            var b93 = new Radical(new Rational(2, 3), 3);
            var b94 = new Radical(new Rational(3, 2), 2);
            var b95 = new Radical(new Rational(5, 6), 3);
            var b96 = new Radical(new Rational(7, 3), 2);
            var b97 = new Radical(new Rational(4, 3), 3);
            var b98 = new Radical(new Rational(5, 7), 2);
            var c91 = b91 - b92;
            var c92 = b93 + b94;
            var c93 = b95 + b96;
            var c94 = b97 - b98;
            var cr91 = new RadicalSumRatio(c91, c92);
            var cr92 = new RadicalSumRatio(c93, c94);
            var actual9 = cr91 / cr92;
            var expected9 = new RadicalSumRatio(
                new RadicalSum(new Radical[2] {
                    new Radical(new Rational(101,7), 1),
                    new Radical(new Rational(-52,7), 6)
                }),
                new RadicalSum(new Radical[2] {
                    new Radical(new Rational(26),1),
                    new Radical(new Rational(101,12), 6)
                }));

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            //Assert.Equal(expected3, actual3);
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

        [Fact]
        public void ConversionTests()
        {
            // [3*sqrt(2) + (7/2)*sqrt(3)] / [(-9/3)*sqrt(2) + (-14/4)*sqrt(3)] = -1
            var b11 = new Radical(3, 2);
            var b12 = new Radical(new Rational(7, 2), 3);
            var b13 = new Radical(new Rational(-9, 3), 2);
            var b14 = new Radical(new Rational(-14, 4), 3);
            var c11 = new RadicalSum(new Radical[2] { b11, b12 });
            var c12 = new RadicalSum(new Radical[2] { b13, b14 });
            var cr1 = new RadicalSumRatio(c11, c12);
            bool actual11 = cr1.IsRational;
            var actual12 = cr1.ToRational();
            bool expected11 = true;
            var expected12 = new Rational(-1);

            // (3*sqrt(2)) / ((5/3)*sqrt(3)) = (9/5)*sqrt(2/3) = (3/5)*sqrt(6)
            var b21 = new RadicalSumRatio(3, 2);
            var b22 = new RadicalSumRatio(new Rational(5, 3), 3);
            var b23 = b21 / b22;
            var actual21 = b23.IsRational;
            var actual22 = b23.ToDouble();
            var expected21 = false;
            double expected22 = 1.4696938456699068589183704448235;

            Assert.Equal(expected11, actual11);
            Assert.Equal(expected12, actual12);
            Assert.Equal(expected21, actual21);
            Assert.Equal(expected22.ToString("0.000000"), actual22.ToString("0.000000"));
        }
    }
}
