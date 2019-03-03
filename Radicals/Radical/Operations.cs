using System;

namespace Radicals
{
    public readonly partial struct Radical
    {
        public static Radical Negate(Radical value)
        {
            return new Radical(-value.Coefficient, value.Radicand);
        }

        public static Radical AddCompatible(Radical left, Radical right)
        {
            if (left.Radicand != right.Radicand)
                throw new Exception("Trying to add compatible radicals that aren't compatible");
            return new Radical(left.Coefficient + right.Coefficient, left.Radicand);
        }

        public static RadicalSum Add(Radical left, Radical right)
        {
            return new RadicalSum(new Radical[2] { left, right });
        }

        public static RadicalSum Subtract(Radical left, Radical right)
        {
            return Add(left, Negate(right));
        }

        public static Radical Multiply(Radical left, Radical right)
        {
            return new Radical(left.Coefficient * right.Coefficient, left.Radicand * right.Radicand);
        }

        public static Radical Divide(Radical left, Radical right)
        {
            // r' = [c1 * sqrt(r1)] / [c2 * sqrt(r2)]
            //    = [c1 / c2] * sqrt(r1 / r2)
            //    = [c1 / c2] * sqrt(r1 * r2 / r2 * r2)
            //    = [c1 / (c2 * r2)] * sqrt(r1 * r2)
            if (right == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            return new Radical(left.Coefficient / (right.Coefficient * right.Radicand), left.Radicand * right.Radicand);
        }
        
    }
}
