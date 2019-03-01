using Rationals;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xunit;

namespace Radicals.Test
{
    public class CompositeRadicalRatioTests
    {
        [Fact]
        public void ConstructorTests()
        {
            // (3/4) * sqrt(12)
            // = (2*3/4) * sqrt(3)
            // = (3/2) * sqrt(3)
            var actual1 = new CompositeRadicalRatio(new Rational(3, 4), 12);
            var expected1 = new CompositeRadicalRatio(new Rational(3, 2), 3);
            // sqrt(2/9) = (1/3)sqrt(2)
            var actual2 = new CompositeRadicalRatio(new Rational(2, 9));
            var expected2 = new CompositeRadicalRatio(new Rational(1, 3), 2);
            // sqrt(1/2) = (1/2)sqrt(2)
            var actual3 = new CompositeRadicalRatio(new Rational(1, 2));
            var expected3 = new CompositeRadicalRatio(new Rational(1, 2), 2);
            // 0 = 0
            CompositeRadicalRatio actual41 = 0;
            var actual42 = new CompositeRadicalRatio(0);
            var actual43 = new CompositeRadicalRatio(0, 0);
            var actual44 = new CompositeRadicalRatio();
            var actual45 = new CompositeRadicalRatio(3, 0);
            var actual46 = new CompositeRadicalRatio(0, 5);
            var expected4 = CompositeRadicalRatio.Zero;
            // 1 = 1
            CompositeRadicalRatio actual51 = 1;
            var actual52 = new CompositeRadicalRatio(1, 1);
            var actual53 = new CompositeRadicalRatio(1);
            var expected5 = CompositeRadicalRatio.One;
            // [(3/2)*sqrt(7) - (2/3)*sqrt(5)] / [(1/2)*sqrt(7) + (4/5)*sqrt(6)]
            var c61 = new CompositeRadical(new BasicRadical[2] {
                new BasicRadical(new Rational(3,2), 7),
                new BasicRadical(new Rational(-2,3), 5)
            });
            var c62 = new CompositeRadical(new BasicRadical[2] {
                new BasicRadical(new Rational(1,2), 7),
                new BasicRadical(new Rational(4,5), 6)
            });
            var actual6 = c61 / c62;
            var expected6 = new CompositeRadicalRatio(c61, c62);


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
            Assert.Equal(expected5, actual51);
            Assert.Equal(expected5, actual52);
            Assert.Equal(expected5, actual53);
            Assert.Equal(expected6, actual6);
        }

        [Fact]
        public void AdditionTests()
        {
            // sqrt(2) + sqrt(3)
            var b11 = new CompositeRadicalRatio(1, 2);
            var b12 = new CompositeRadicalRatio(1, 3);
            var actual1 = b11 + b12;
            var expected1 = new CompositeRadicalRatio(new BasicRadical[2] { new BasicRadical(1, 2), new BasicRadical(1, 3) });
            // sqrt(2) + 2 * sqrt(2) = 3 * sqrt(2)
            var b21 = new CompositeRadicalRatio(1, 2);
            var b22 = new CompositeRadicalRatio(2, 2);
            var actual2 = b21 + b22;
            var expected2 = new CompositeRadicalRatio(new BasicRadical(3, 2));
            // 5*sqrt(27) + 7*sqrt(12) = 15*sqrt(3) + 14*sqrt(3) = 29*sqrt(3)
            var b31 = new CompositeRadicalRatio(5, 27);
            var b32 = new CompositeRadicalRatio(7, 12);
            var actual3 = b31 + b32;
            var expected3 = new CompositeRadicalRatio(new BasicRadical(29, 3));
            // 3*sqrt(2) + 2*sqrt(3)
            var b41 = new CompositeRadicalRatio(3, 2);
            var b42 = new CompositeRadicalRatio(2, 3);
            var actual4 = b41 + b42;
            var expected4 = new CompositeRadicalRatio(new BasicRadical[2] { new BasicRadical(3, 2), new BasicRadical(2, 3) });
            // 2*sqrt(2) + 5*sqrt(28) + sqrt(1/2) + 3 + sqrt(7/9) + 11*sqrt(4) = 25 + (5/2)sqrt(2) + (31/3)*sqrt(7)
            var b51 = new CompositeRadicalRatio(2, 2);
            var b52 = new CompositeRadicalRatio(5, 28);
            var b53 = new CompositeRadicalRatio(new Rational(1, 2));
            var b54 = new CompositeRadicalRatio(3, 1);
            var b55 = new CompositeRadicalRatio(new Rational(7, 9));
            var b56 = new CompositeRadicalRatio(11, 4);
            var actual5 = b51 + b52 + b53 + b54 + b55 + b56;
            var expected5 = new CompositeRadicalRatio(new BasicRadical[3] {
                new BasicRadical(25, 1),
                new BasicRadical(new Rational(5,2),2),
                new BasicRadical(new Rational(31,3),7)
            });
            // (3/2)*sqrt(5) + 0 = (3/2)*sqrt(5)
            // 0 + (3/2)*sqrt(5) = (3/2)*sqrt(5)
            var b61 = new CompositeRadicalRatio(new Rational(3, 2), 5);
            var b62 = CompositeRadicalRatio.Zero;
            var actual61 = b61 + b62;
            var actual62 = b62 + b61;
            var expected6 = new CompositeRadicalRatio(new BasicRadical(new Rational(3, 2), 5));
            // (3/2)*sqrt(5) + (-3/2)*sqrt(5) = 0
            var b71 = new CompositeRadicalRatio(new Rational(3, 2), 5);
            var b72 = new CompositeRadicalRatio(new Rational(-3, 2), 5);
            var actual7 = b71 + b72;
            var expected7 = CompositeRadicalRatio.Zero;
            // {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]} + {[(5/6)*sqrt(3) + (7/3)*sqrt(2)] / [(4/3)*sqrt(3) - (5/7)*sqrt(2)]}
            // = [(8/9)*3 - (10/21)*sqrt(6) - (12/6)*sqrt(6) + (15/14)*2 + (10/18)*3 + (14/9)*sqrt(6) + (15/12)*sqrt(6) + (21/6)*2] / [(8/9)*3 - (10/21)*sqrt(6) + (12/6)*sqrt(6) - (15/14)*2]
            // = [(8/3) + (83/252)*sqrt(6) + (15/7) + (10/6) + (21/3)] / [(8/3) + (32/21)*sqrt(6) - (15/7)]
            // = [(283/21) + (83/252)*sqrt(6)] / [(11/21) + (32/21)*sqrt(6)]
            // = [(283) + (83/12)*sqrt(6)] / [(11) + 32*sqrt(6)]
            var b81 = new BasicRadical(new Rational(2, 3), 3);
            var b82 = new BasicRadical(new Rational(3, 2), 2);
            var b83 = new BasicRadical(new Rational(2, 3), 3);
            var b84 = new BasicRadical(new Rational(3, 2), 2);
            var b85 = new BasicRadical(new Rational(5, 6), 3);
            var b86 = new BasicRadical(new Rational(7, 3), 2);
            var b87 = new BasicRadical(new Rational(4, 3), 3);
            var b88 = new BasicRadical(new Rational(5, 7), 2);
            var c81 = b81 - b82;
            var c82 = b83 + b84;
            var c83 = b85 + b86;
            var c84 = b87 - b88;
            var cr81 = new CompositeRadicalRatio(c81, c82);
            var cr82 = new CompositeRadicalRatio(c83, c84);
            var actual8 = cr81 + cr82;
            var expected8 = new CompositeRadicalRatio(
                new CompositeRadical(new BasicRadical[2] {
                    new BasicRadical(new Rational(283), 1),
                    new BasicRadical(new Rational(83,12), 6)
                }),
                new CompositeRadical(new BasicRadical[2] {
                    new BasicRadical(new Rational(11),1),
                    new BasicRadical(new Rational(32), 6)
                }));
            // (2/3) + (1/3) = 1
            var b91 = new BasicRadical(2, 1);
            var b92 = new BasicRadical(3, 1);
            var b93 = new BasicRadical(1, 1);
            var b94 = new BasicRadical(3, 1);
            var c91 = new CompositeRadical(b91);
            var c92 = new CompositeRadical(b92);
            var c93 = new CompositeRadical(b93);
            var c94 = new CompositeRadical(b94);
            var cr91 = new CompositeRadicalRatio(c91, c92);
            var cr92 = new CompositeRadicalRatio(c93, c94);
            var actual9 = cr91 + cr92;
            var expected9 = CompositeRadicalRatio.One;

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
            var b11 = new CompositeRadicalRatio(1, 2);
            var b12 = new CompositeRadicalRatio(1, 3);
            var actual1 = b11 - b12;
            var expected1 = new CompositeRadicalRatio(new BasicRadical[2] { new BasicRadical(1, 2), new BasicRadical(-1, 3) });
            // sqrt(2) - 2 * sqrt(2) = -1 * sqrt(2)
            var b21 = new CompositeRadicalRatio(1, 2);
            var b22 = new CompositeRadicalRatio(2, 2);
            var actual2 = b21 - b22;
            var expected2 = new CompositeRadicalRatio(new BasicRadical(-1, 2));
            // 5*sqrt(27) - 7*sqrt(12) = 15*sqrt(3) - 14*sqrt(3) = 1*sqrt(3)
            var b31 = new CompositeRadicalRatio(5, 27);
            var b32 = new CompositeRadicalRatio(7, 12);
            var actual3 = b31 - b32;
            var expected3 = new CompositeRadicalRatio(new BasicRadical(1, 3));
            // 3*sqrt(2) - 2*sqrt(3)
            var b41 = new CompositeRadicalRatio(3, 2);
            var b42 = new CompositeRadicalRatio(2, 3);
            var actual4 = b41 - b42;
            var expected4 = new CompositeRadicalRatio(new BasicRadical[2] { new BasicRadical(3, 2), new BasicRadical(-2, 3) });
            // 2*sqrt(2) + 5*sqrt(28) - sqrt(1/2) + 3 - sqrt(7/9) + 11*sqrt(4) = 25 + (3/2)sqrt(2) + (29/3)*sqrt(7)
            var b51 = new CompositeRadicalRatio(2, 2);
            var b52 = new CompositeRadicalRatio(5, 28);
            var b53 = new CompositeRadicalRatio(new Rational(1, 2));
            var b54 = new CompositeRadicalRatio(3, 1);
            var b55 = new CompositeRadicalRatio(new Rational(7, 9));
            var b56 = new CompositeRadicalRatio(11, 4);
            var actual5 = b51 + b52 - b53 + b54 - b55 + b56;
            var expected5 = new CompositeRadicalRatio(new BasicRadical[3] {
                new BasicRadical(25, 1),
                new BasicRadical(new Rational(3,2),2),
                new BasicRadical(new Rational(29,3),7)
            });
            // (3/2)*sqrt(5) - 0 = (3/2)*sqrt(5)
            var b61 = new CompositeRadicalRatio(new Rational(3, 2), 5);
            var b62 = CompositeRadicalRatio.Zero;
            var actual6 = b61 - b62;
            var expected6 = new CompositeRadicalRatio(new BasicRadical(new Rational(3, 2), 5));
            // 0 - (3/2)*sqrt(5) = (-3/2)*sqrt(5)
            var b71 = CompositeRadicalRatio.Zero;
            var b72 = new CompositeRadicalRatio(new Rational(3, 2), 5);
            var actual7 = b71 - b72;
            var expected7 = new CompositeRadicalRatio(new BasicRadical(new Rational(-3, 2), 5));
            // (3/2)*sqrt(5) - (3/2)*sqrt(5) = 0
            var b81 = new CompositeRadicalRatio(new Rational(3, 2), 5);
            var b82 = new CompositeRadicalRatio(new Rational(3, 2), 5);
            var actual8 = b81 - b82;
            var expected8 = CompositeRadicalRatio.Zero;
            // {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]} - {[(5/6)*sqrt(3) + (7/3)*sqrt(2)] / [(4/3)*sqrt(3) - (5/7)*sqrt(2)]}
            // = [(8/9)*3 - (10/21)*sqrt(6) - (12/6)*sqrt(6) + (15/14)*2 - (10/18)*3 - (14/9)*sqrt(6) - (15/12)*sqrt(6) - (21/6)*2] / [(8/9)*3 - (10/21)*sqrt(6) + (12/6)*sqrt(6) - (15/14)*2]
            // = [(8/3) - (1331/252)*sqrt(6) + (15/7) - (10/6) - (21/3)] / [(8/3) + (32/21)*sqrt(6) - (15/7)]
            // = [(-27/7) - (1331/252)*sqrt(6)] / [(11/21) + (32/21)*sqrt(6)]
            // = [(-81) - (1331/12)*sqrt(6)] / [(11) + 32*sqrt(6)]
            var b91 = new BasicRadical(new Rational(2, 3), 3);
            var b92 = new BasicRadical(new Rational(3, 2), 2);
            var b93 = new BasicRadical(new Rational(2, 3), 3);
            var b94 = new BasicRadical(new Rational(3, 2), 2);
            var b95 = new BasicRadical(new Rational(5, 6), 3);
            var b96 = new BasicRadical(new Rational(7, 3), 2);
            var b97 = new BasicRadical(new Rational(4, 3), 3);
            var b98 = new BasicRadical(new Rational(5, 7), 2);
            var c91 = b91 - b92;
            var c92 = b93 + b94;
            var c93 = b95 + b96;
            var c94 = b97 - b98;
            var cr91 = new CompositeRadicalRatio(c91, c92);
            var cr92 = new CompositeRadicalRatio(c93, c94);
            var actual9 = cr91 - cr92;
            var expected9 = new CompositeRadicalRatio(
                new CompositeRadical(new BasicRadical[2] {
                    new BasicRadical(new Rational(-81), 1),
                    new BasicRadical(new Rational(-1331,12), 6)
                }),
                new CompositeRadical(new BasicRadical[2] {
                    new BasicRadical(new Rational(11),1),
                    new BasicRadical(new Rational(32), 6)
                }));
            // (2/3) - (1/3) = 1/3
            var b101 = new BasicRadical(2, 1);
            var b102 = new BasicRadical(3, 1);
            var b103 = new BasicRadical(1, 1);
            var b104 = new BasicRadical(3, 1);
            var c101 = new CompositeRadical(b101);
            var c102 = new CompositeRadical(b102);
            var c103 = new CompositeRadical(b103);
            var c104 = new CompositeRadical(b104);
            var cr101 = new CompositeRadicalRatio(c101, c102);
            var cr102 = new CompositeRadicalRatio(c103, c104);
            var actual10 = cr101 - cr102;
            var expected10 = new CompositeRadicalRatio(
                new CompositeRadical(new BasicRadical(1,1)),
                new CompositeRadical(new BasicRadical(3,1))
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
            var b11 = new CompositeRadicalRatio(3, 2);
            var b12 = new CompositeRadicalRatio(new Rational(5, 3), 3);
            var actual1 = b11 * b12;
            var expected1 = new CompositeRadicalRatio(5, 6);
            // 11 * sqrt(4/9) = 22/3
            var b21 = new CompositeRadicalRatio(11, 1);
            var b22 = new CompositeRadicalRatio(new Rational(4, 9));
            var actual2 = b21 * b22;
            var expected2 = new CompositeRadicalRatio(new Rational(22, 3), 1);
            // 11 * sqrt(4/9) = 22/3
            var b31 = 11;
            var b32 = new CompositeRadicalRatio(new Rational(4, 9));
            var actual31 = b31 * b32;
            var actual32 = b32 * b31;
            var expected3 = new CompositeRadicalRatio(new Rational(22, 3), 1);
            // [(3/2)*sqrt(2) - (7/3)*sqrt(5) + (1/3)*sqrt(2) - (7/5)*sqrt(5)] * (11/4)*sqrt(2)
            // = [(11/6)*sqrt(2) - (56/15)*sqrt(5)] * (11/4)*sqrt(2)
            // = (121/24)*2 - (154/15)*sqrt(10)
            // = (121/12) - (56/15)*sqrt(10)
            var b41 = new CompositeRadicalRatio(new Rational(3, 2), 2);
            var b42 = new CompositeRadicalRatio(new Rational(7, 3), 5);
            var b43 = new CompositeRadicalRatio(new Rational(1, 3), 2);
            var b44 = new CompositeRadicalRatio(new Rational(7, 5), 5);
            var b45 = new CompositeRadicalRatio(new Rational(11, 4), 2);
            var actual41 = (b41 - b42 + b43 - b44) * b45;
            var actual42 = b45 * (b41 - b42 + b43 - b44);
            var expected4 = new CompositeRadicalRatio(new BasicRadical[2] {
                new BasicRadical(new Rational(121, 12), 1),
                new BasicRadical(new Rational(-154, 15), 10) });
            // (3/2)*sqrt(5) * 1 = (3/2)*sqrt(5)
            var b51 = new CompositeRadicalRatio(new Rational(3, 2), 5);
            var b52 = new CompositeRadicalRatio(1, 1);
            var b53 = 1;
            var b54 = new CompositeRadicalRatio(1);
            var b55 = CompositeRadicalRatio.One;
            var actual51 = b51 * b52;
            var actual52 = b51 * b53;
            var actual53 = b51 * b54;
            var actual54 = b51 * b55;
            var actual55 = b52 * b51;
            var actual56 = b53 * b51;
            var actual57 = b54 * b51;
            var actual58 = b55 * b51;
            var expected5 = new CompositeRadicalRatio(new Rational(3, 2), 5);
            // (3/2)*sqrt(5) * -1 = (-3/2)*sqrt(5)
            var b61 = new CompositeRadicalRatio(new Rational(3, 2), 5);
            var b62 = -CompositeRadicalRatio.One;
            var b63 = -1;
            var b64 = new CompositeRadicalRatio(-1, 1);
            var actual61 = b61 * b62;
            var actual62 = b61 * b63;
            var actual63 = b61 * b64;
            var actual64 = b62 * b61;
            var actual65 = b63 * b61;
            var actual66 = b64 * b61;
            var expected6 = new CompositeRadicalRatio(new Rational(-3, 2), 5);
            // (3/2)*sqrt(5) * 0 = 0
            var b71 = new CompositeRadicalRatio(new Rational(3, 2), 5);
            var b72 = 0;
            var b73 = CompositeRadicalRatio.Zero;
            var b74 = new CompositeRadicalRatio(0);
            var b75 = new CompositeRadicalRatio(0, 0);
            var actual71 = b71 * b72;
            var actual72 = b71 * b73;
            var actual73 = b71 * b74;
            var actual74 = b71 * b75;
            var actual75 = b72 * b71;
            var actual76 = b73 * b71;
            var actual77 = b74 * b71;
            var actual78 = b75 * b71;
            var expected7 = CompositeRadicalRatio.Zero;
            // [(5/3)*sqrt(7) - (2/9)*sqrt(11) - 6*sqrt(13)] * [2*sqrt(6) - 3*sqrt(8)]
            // = (10/3)*sqrt(42) - (4/9)*sqrt(66) - 12*sqrt(78) - 5*sqrt(56) + (2/3)*sqrt(88) + 18*sqrt(104)
            // = (10/3)*sqrt(42) - 5*sqrt(56) - (4/9)*sqrt(66) - (34/3)*sqrt(88) + 18*sqrt(104) - 12*sqrt(78)
            // = -10*sqrt(14) + (4/3)*sqrt(22) + 36*sqrt(26) + (10/3)*sqrt(42) - (4/9)*sqrt(66) - 12*sqrt(78)
            var b81 = new CompositeRadicalRatio(new Rational(5, 3), 7);
            var b82 = new CompositeRadicalRatio(new Rational(2, 9), 11);
            var b83 = new CompositeRadicalRatio(6, 13);
            var b84 = new CompositeRadicalRatio(2, 6);
            var b85 = new CompositeRadicalRatio(3, 8);
            var c81 = b81 - b82 - b83;
            var c82 = b84 - b85;
            var actual8 = c81 * c82;
            var expected8 = new CompositeRadicalRatio(new BasicRadical[6] {
                new BasicRadical(-10, 14),
                new BasicRadical(new Rational(4,3),22),
                new BasicRadical(36, 26),
                new BasicRadical(new Rational(10,3),42),
                new BasicRadical(new Rational(-4,9),66),
                new BasicRadical(-12,78)
            });
            // {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]} * {[(5/6)*sqrt(3) + (7/3)*sqrt(2)] / [(4/3)*sqrt(3) - (5/7)*sqrt(2)]}
            // = [(10/18)*3 + (14/9)*sqrt(6) - (15/12)*sqrt(6) - (21/6)*2] / [(8/9)*3 - (10/21)*sqrt(6) + (12/6)*sqrt(6) - (15/14)*2]
            // = [(-16/3) + (11/36)*sqrt(6)] / [(11/21) + (32/21)*sqrt(6)]
            // = [(-112) + (77/12)*sqrt(6)] / [11 + 32*sqrt(6)]
            var b91 = new BasicRadical(new Rational(2, 3), 3);
            var b92 = new BasicRadical(new Rational(3, 2), 2);
            var b93 = new BasicRadical(new Rational(2, 3), 3);
            var b94 = new BasicRadical(new Rational(3, 2), 2);
            var b95 = new BasicRadical(new Rational(5, 6), 3);
            var b96 = new BasicRadical(new Rational(7, 3), 2);
            var b97 = new BasicRadical(new Rational(4, 3), 3);
            var b98 = new BasicRadical(new Rational(5, 7), 2);
            var c91 = b91 - b92;
            var c92 = b93 + b94;
            var c93 = b95 + b96;
            var c94 = b97 - b98;
            var cr91 = new CompositeRadicalRatio(c91, c92);
            var cr92 = new CompositeRadicalRatio(c93, c94);
            var actual9 = cr91 * cr92;
            var expected9 = new CompositeRadicalRatio(
                new CompositeRadical(new BasicRadical[2] {
                    new BasicRadical(new Rational(-112), 1),
                    new BasicRadical(new Rational(77,12), 6)
                }),
                new CompositeRadical(new BasicRadical[2] {
                    new BasicRadical(new Rational(11),1),
                    new BasicRadical(new Rational(32), 6)
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
            var b11 = new CompositeRadicalRatio(3, 2);
            var b12 = new CompositeRadicalRatio(new Rational(5, 3), 3);
            var actual1 = b11 / b12;
            var expected1 = new CompositeRadicalRatio(new Rational(3, 5), 6);
            // 11 / sqrt(4/9) = 33/2
            var b21 = new CompositeRadicalRatio(11, 1);
            var b22 = new CompositeRadicalRatio(new Rational(4, 9));
            var actual2 = b21 / b22;
            var expected2 = new CompositeRadicalRatio(new Rational(33, 2), 1);
            // sqrt(4/9) / 11 = 2/33
            var b41 = new CompositeRadicalRatio(new Rational(4, 9));
            var b42 = 11;
            var actual4 = b41 / b42;
            var expected4 = new CompositeRadicalRatio(new Rational(2, 33), 1);
            // [(3/2)*sqrt(2) - (7/3)*sqrt(5) + (1/3)*sqrt(2) - (7/5)*sqrt(5)] / (11/4)*sqrt(2)
            // = [(11/6)*sqrt(2) - (56/15)*sqrt(5)] / (11/4)*sqrt(2)
            // = (2/3) - (224/165)*sqrt(5/2)
            // = (2/3) - (112/165)*sqrt(10)
            var b51 = new CompositeRadicalRatio(new Rational(3, 2), 2);
            var b52 = new CompositeRadicalRatio(new Rational(7, 3), 5);
            var b53 = new CompositeRadicalRatio(new Rational(1, 3), 2);
            var b54 = new CompositeRadicalRatio(new Rational(7, 5), 5);
            var b55 = new CompositeRadicalRatio(new Rational(11, 4), 2);
            var actual5 = (b51 - b52 + b53 - b54) / b55;
            var expected5 = new CompositeRadicalRatio(new BasicRadical[2] {
                new BasicRadical(new Rational(2, 3), 1),
                new BasicRadical(new Rational(-112, 165), 10) });
            // (3/2)*sqrt(5) / 1 = (3/2)*sqrt(5)
            var b61 = new CompositeRadicalRatio(new Rational(3, 2), 5);
            var b62 = new CompositeRadicalRatio(1, 1);
            var b63 = 1;
            var b64 = new BasicRadical(1);
            var b65 = BasicRadical.One;
            var actual61 = b61 / b62;
            var actual62 = b61 / b63;
            var actual63 = b61 / b64;
            var actual64 = b61 / b65;
            var expected6 = new CompositeRadicalRatio(new Rational(3, 2), 5);
            // (3/2)*sqrt(5) / -1 = (-3/2)*sqrt(5)
            var b71 = new CompositeRadicalRatio(new Rational(3, 2), 5);
            var b72 = new BasicRadical(-1, 1);
            var b73 = -1;
            var b74 = -BasicRadical.One;
            var actual71 = b71 / b72;
            var actual72 = b71 / b73;
            var actual73 = b71 / b74;
            var expected7 = new CompositeRadicalRatio(new Rational(-3, 2), 5);
            // {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]} / {[(5/6)*sqrt(3) + (7/3)*sqrt(2)] / [(4/3)*sqrt(3) - (5/7)*sqrt(2)]}
            // = {[(2/3)*sqrt(3) - (3/2)*sqrt(2)] / [(2/3)*sqrt(3) + (3/2)*sqrt(2)]} * {[(4/3)*sqrt(3) - (5/7)*sqrt(2)] / [(5/6)*sqrt(3) + (7/3)*sqrt(2)]}
            // = [(8/9)*3 - (10/21)*sqrt(6) - (12/6)*sqrt(6) + (15/14)*2] / [(10/18)*3 + (14/9)*sqrt(6) + (15/12)*sqrt(6) + (21/6)*2]
            // = [(101/21) - (52/21)*sqrt(6)] / [(26/3) + (101/36)*sqrt(6)]
            // = [(101/7) - (52/7)*sqrt(6)] / [26 + (101/12)*sqrt(6)]
            var b91 = new BasicRadical(new Rational(2, 3), 3);
            var b92 = new BasicRadical(new Rational(3, 2), 2);
            var b93 = new BasicRadical(new Rational(2, 3), 3);
            var b94 = new BasicRadical(new Rational(3, 2), 2);
            var b95 = new BasicRadical(new Rational(5, 6), 3);
            var b96 = new BasicRadical(new Rational(7, 3), 2);
            var b97 = new BasicRadical(new Rational(4, 3), 3);
            var b98 = new BasicRadical(new Rational(5, 7), 2);
            var c91 = b91 - b92;
            var c92 = b93 + b94;
            var c93 = b95 + b96;
            var c94 = b97 - b98;
            var cr91 = new CompositeRadicalRatio(c91, c92);
            var cr92 = new CompositeRadicalRatio(c93, c94);
            var actual9 = cr91 / cr92;
            var expected9 = new CompositeRadicalRatio(
                new CompositeRadical(new BasicRadical[2] {
                    new BasicRadical(new Rational(101,7), 1),
                    new BasicRadical(new Rational(-52,7), 6)
                }),
                new CompositeRadical(new BasicRadical[2] {
                    new BasicRadical(new Rational(26),1),
                    new BasicRadical(new Rational(101,12), 6)
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
            var b11 = new BasicRadical(3, 2);
            var b12 = new BasicRadical(new Rational(7, 2), 3);
            var b13 = new BasicRadical(new Rational(-9, 3), 2);
            var b14 = new BasicRadical(new Rational(-14, 4), 3);
            var c11 = new CompositeRadical(new BasicRadical[2] { b11, b12 });
            var c12 = new CompositeRadical(new BasicRadical[2] { b13, b14 });
            var cr1 = new CompositeRadicalRatio(c11, c12);
            bool actual11 = cr1.IsRational();
            var actual12 = cr1.ToRational();
            bool expected11 = true;
            var expected12 = new Rational(-1);

            // (3*sqrt(2)) / ((5/3)*sqrt(3)) = (9/5)*sqrt(2/3) = (3/5)*sqrt(6)
            var b21 = new CompositeRadicalRatio(3, 2);
            var b22 = new CompositeRadicalRatio(new Rational(5, 3), 3);
            var b23 = b21 / b22;
            var actual21 = b23.IsRational();
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
