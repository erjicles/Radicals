using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSum
    {
        public static implicit operator RadicalSum(Rational value)
        {
            return new RadicalSum(value, 1);
        }

        public static implicit operator RadicalSum(byte value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        public static implicit operator RadicalSum(sbyte value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        public static implicit operator RadicalSum(short value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        public static implicit operator RadicalSum(ushort value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        public static implicit operator RadicalSum(int value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        public static implicit operator RadicalSum(uint value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        public static implicit operator RadicalSum(long value)
        {
            return new RadicalSum(new Rational(value), 1);
        }

        public static implicit operator RadicalSum(ulong value)
        {
            return new RadicalSum(new Rational(value), 1);
        }
    }
}
