using System;

namespace Radicals
{
    public readonly partial struct BasicRadical
    {
        public static BasicRadical Negate(BasicRadical value)
        {
            return new BasicRadical(-value.C, value.R);
        }

        public static BasicRadical[] Negate(BasicRadical[] value)
        {
            BasicRadical[] result = new BasicRadical[value.Length];
            for (int i = 0; i < value.Length; i++)
                result[i] = -value[i];
            return result;
        }

        public static BasicRadical AddCompatible(BasicRadical left, BasicRadical right)
        {
            if (left.R != right.R)
                throw new Exception("Trying to add compatible radicals that aren't compatible");
            return new BasicRadical(left.C + right.C, left.R);
        }

        public static BasicRadical[] Add(BasicRadical[] left, BasicRadical[] right)
        {
            if (left == null)
                return right;
            if (right == null)
                return left;
            var z = new BasicRadical[left.Length + right.Length];
            for (int i = 0; i < left.Length; i++)
                z[i] = left[i];
            for (int i = 0; i < right.Length; i++)
                z[i + left.Length] = right[i];
            return SimplifyRadicals(z);
        }

        public static BasicRadical[] Add(BasicRadical left, BasicRadical[] right)
        {
            return Add(new BasicRadical[1] { left }, right);
        }

        public static BasicRadical[] Add(BasicRadical left, BasicRadical right)
        {
            return Add(new BasicRadical[1] { left }, new BasicRadical[1] { right });
        }

        public static BasicRadical[] Subtract(BasicRadical[] left, BasicRadical[] right)
        {
            return Add(left, Negate(right));
        }

        public static BasicRadical[] Subtract(BasicRadical left, BasicRadical[] right)
        {
            return Subtract(new BasicRadical[1] { left }, right);
        }

        public static BasicRadical[] Subtract(BasicRadical left, BasicRadical right)
        {
            return Subtract(new BasicRadical[1] { left }, new BasicRadical[1] { right });
        }

        public static BasicRadical Multiply(BasicRadical left, BasicRadical right)
        {
            return new BasicRadical(left.C * right.C, left.R * right.R);
        }

        public static BasicRadical[] Multiply(BasicRadical[] left, BasicRadical[] right)
        {
            var z = new BasicRadical[left.Length * right.Length];
            for (int i = 0; i < left.Length; i++)
                for (int j = 0; j < right.Length; j++)
                    z[(i * right.Length) + j] = left[i] * right[j];
            return SimplifyRadicals(z);
        }

        public static BasicRadical[] Multiply(BasicRadical left, BasicRadical[] right)
        {
            return Multiply(new BasicRadical[1] { left }, right);
        }

        public static BasicRadical Divide(BasicRadical left, BasicRadical right)
        {
            // r' = [c1 * sqrt(r1)] / [c2 * sqrt(r2)]
            //    = [c1 / c2] * sqrt(r1 / r2)
            //    = [c1 / c2] * sqrt(r1 * r2 / r2 * r2)
            //    = [c1 / (c2 * r2)] * sqrt(r1 * r2)
            return new BasicRadical(left.C / (right.C * right.R), left.R * right.R);
        }

        public static BasicRadical[] Divide(BasicRadical[] left, BasicRadical right)
        {
            var z = new BasicRadical[left.Length];
            for (int i = 0; i < left.Length; i++)
                z[i] = Divide(left[i], right);
            return SimplifyRadicals(z);
        }
        
    }
}
