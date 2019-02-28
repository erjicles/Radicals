using System;

namespace Radicals
{
    public readonly partial struct BasicRadical
    {
        public static BasicRadical Negate(BasicRadical value)
        {
            return new BasicRadical(-value.C, value.R);
        }

        public static BasicRadical AddCompatible(BasicRadical left, BasicRadical right)
        {
            if (left.R != right.R)
                throw new Exception("Trying to add compatible radicals that aren't compatible");
            return new BasicRadical(left.C + right.C, left.R);
        }

        public static CompositeRadical Add(BasicRadical left, BasicRadical right)
        {
            return new CompositeRadical(new BasicRadical[2] { left, right });
        }

        public static CompositeRadical Subtract(BasicRadical left, BasicRadical right)
        {
            return Add(left, Negate(right));
        }

        public static BasicRadical Multiply(BasicRadical left, BasicRadical right)
        {
            return new BasicRadical(left.C * right.C, left.R * right.R);
        }

        public static BasicRadical Divide(BasicRadical left, BasicRadical right)
        {
            // r' = [c1 * sqrt(r1)] / [c2 * sqrt(r2)]
            //    = [c1 / c2] * sqrt(r1 / r2)
            //    = [c1 / c2] * sqrt(r1 * r2 / r2 * r2)
            //    = [c1 / (c2 * r2)] * sqrt(r1 * r2)
            if (right == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            return new BasicRadical(left.C / (right.C * right.R), left.R * right.R);
        }
        
    }
}
