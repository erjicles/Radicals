using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadical
    {
        public static implicit operator CompositeRadical(Rational value)
        {
            return new CompositeRadical(value, 1);
        }

        public static implicit operator CompositeRadical(byte value)
        {
            return new CompositeRadical(new Rational(value), 1);
        }

        public static implicit operator CompositeRadical(sbyte value)
        {
            return new CompositeRadical(new Rational(value), 1);
        }

        public static implicit operator CompositeRadical(short value)
        {
            return new CompositeRadical(new Rational(value), 1);
        }

        public static implicit operator CompositeRadical(ushort value)
        {
            return new CompositeRadical(new Rational(value), 1);
        }

        public static implicit operator CompositeRadical(int value)
        {
            return new CompositeRadical(new Rational(value), 1);
        }

        public static implicit operator CompositeRadical(uint value)
        {
            return new CompositeRadical(new Rational(value), 1);
        }

        public static implicit operator CompositeRadical(long value)
        {
            return new CompositeRadical(new Rational(value), 1);
        }

        public static implicit operator CompositeRadical(ulong value)
        {
            return new CompositeRadical(new Rational(value), 1);
        }
    }
}
