using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
    {
        public static implicit operator CompositeRadicalRatio(Rational value)
        {
            return new CompositeRadicalRatio(value, 1);
        }

        public static implicit operator CompositeRadicalRatio(byte value)
        {
            return new CompositeRadicalRatio(new Rational(value), 1);
        }

        public static implicit operator CompositeRadicalRatio(sbyte value)
        {
            return new CompositeRadicalRatio(new Rational(value), 1);
        }

        public static implicit operator CompositeRadicalRatio(short value)
        {
            return new CompositeRadicalRatio(new Rational(value), 1);
        }

        public static implicit operator CompositeRadicalRatio(ushort value)
        {
            return new CompositeRadicalRatio(new Rational(value), 1);
        }

        public static implicit operator CompositeRadicalRatio(int value)
        {
            return new CompositeRadicalRatio(new Rational(value), 1);
        }

        public static implicit operator CompositeRadicalRatio(uint value)
        {
            return new CompositeRadicalRatio(new Rational(value), 1);
        }

        public static implicit operator CompositeRadicalRatio(long value)
        {
            return new CompositeRadicalRatio(new Rational(value), 1);
        }

        public static implicit operator CompositeRadicalRatio(ulong value)
        {
            return new CompositeRadicalRatio(new Rational(value), 1);
        }
    }
}
