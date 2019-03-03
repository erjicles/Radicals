using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSumRatio
    {
        public static implicit operator RadicalSumRatio(Rational value)
        {
            return new RadicalSumRatio(value, 1);
        }

        public static implicit operator RadicalSumRatio(byte value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(sbyte value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(short value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(ushort value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(int value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(uint value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(long value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }

        public static implicit operator RadicalSumRatio(ulong value)
        {
            return new RadicalSumRatio(new Rational(value), 1);
        }
    }
}
