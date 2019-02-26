using System;

namespace Radicals
{
    internal readonly partial struct BasicRadical
    {
        public static BasicRadical AddCompatible(BasicRadical left, BasicRadical right)
        {
            if (left.R != right.R)
                throw new Exception("Trying to add compatible radicals that aren't compatible");
            return new BasicRadical(left.C + right.C, left.R);
        }

        public static BasicRadical[] Add(BasicRadical left, BasicRadical right)
        {
            BasicRadical[] result;
            if (left.IsCompatibleRadical(right))
            {
                result = new BasicRadical[1];
                result[0] = AddCompatible(left, right);
            }
            else
            {
                result = new BasicRadical[2];
                result[0] = left;
                result[1] = right;
            }
            return result;
        }

        public static BasicRadical[] Subtract(BasicRadical left, BasicRadical right)
        {
            return Add(left, -right);
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
            return new BasicRadical(left.C / (right.C * right.R), left.R * right.R);
        }
        
    }
}
