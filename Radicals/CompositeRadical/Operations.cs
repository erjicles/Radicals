using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadical
    {
        public static CompositeRadical Negate(CompositeRadical value)
        {
            var result = BasicRadical.Negate(value.Radicals);
            return new CompositeRadical(result);
        }

        public static CompositeRadical Add(CompositeRadical left, CompositeRadical right)
        {
            var z = new BasicRadical[left.Radicals.Length + right.Radicals.Length];
            for (int i = 0; i < left.Radicals.Length; i++)
                z[i] = left.Radicals[i];
            for (int i = 0; i < right.Radicals.Length; i++)
                z[i + left.Radicals.Length] = right.Radicals[i];
            return new CompositeRadical(z);
        }

        public static CompositeRadical Subtract(CompositeRadical left, CompositeRadical right)
        {
            return Add(left, -right);
        }

        public static CompositeRadical Multiply(CompositeRadical left, CompositeRadical right)
        {
            var z = BasicRadical.Multiply(left.Radicals, right.Radicals);
            return new CompositeRadical(z);
        }

        public static CompositeRadical Divide(CompositeRadical left, BasicRadical right)
        {
            var z = BasicRadical.Divide(left.Radicals, right);
            return new CompositeRadical(z);
        }
    }
}
