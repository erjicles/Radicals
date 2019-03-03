using Rationals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSum
    {
        public static RadicalSum Negate(RadicalSum value)
        {
            Radical[] radicals = new Radical[value.Radicals.Length];
            for (int i = 0; i < value.Radicals.Length; i++)
                radicals[i] = -value.Radicals[i];
            var result = new RadicalSum(radicals);
            return result;
        }

        public static RadicalSum Add(RadicalSum left, RadicalSum right)
        {
            if (left == null)
                return right;
            if (right == null)
                return left;
            var z = new Radical[left.Radicals.Length + right.Radicals.Length];
            for (int i = 0; i < left.Radicals.Length; i++)
                z[i] = left.Radicals[i];
            for (int i = 0; i < right.Radicals.Length; i++)
                z[i + left.Radicals.Length] = right.Radicals[i];
            return new RadicalSum(z);
        }

        public static RadicalSum Subtract(RadicalSum left, RadicalSum right)
        {
            return Add(left, -right);
        }

        public static RadicalSum Multiply(RadicalSum left, RadicalSum right)
        {
            var z = new Radical[left.Radicals.Length * right.Radicals.Length];
            for (int i = 0; i < left.Radicals.Length; i++)
                for (int j = 0; j < right.Radicals.Length; j++)
                    z[(i * right.Radicals.Length) + j] = left.Radicals[i] * right.Radicals[j];
            return new RadicalSum(z);
        }

        public static RadicalSum Divide(RadicalSum left, Radical right)
        {
            if (right == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            var z = new Radical[left.Radicals.Length];
            for (int i = 0; i < left.Radicals.Length; i++)
                z[i] = left.Radicals[i] / right;
            return new RadicalSum(z);
        }

        public static RadicalSumRatio Divide(RadicalSum left, RadicalSum right)
        {
            if (right == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            return new RadicalSumRatio(left, right);
        }
    }
}
