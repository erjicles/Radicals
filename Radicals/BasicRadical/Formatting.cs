using Rationals;
using System;
using System.Globalization;

namespace Radicals
{
    public readonly partial struct BasicRadical : IFormattable
    {
        /// <summary>
        /// S = simplest form, R = all under the radical
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (Coefficient.IsZero)
                return "0";
            if (Radicand.IsZero)
                return "0";

            string cPart = "";
            string rPart = "";

            if ("S".Equals(format))
            {
                if (!Coefficient.IsOne)
                    cPart = Coefficient.ToString();
                if (!Radicand.IsOne)
                    rPart = "Sqrt(" + Radicand.ToString() + ")";
                if (this == One)
                    cPart = "1";
                if (cPart.Length > 0 && rPart.Length > 0)
                {
                    if (Coefficient.Denominator == 1)
                        cPart = cPart + " * ";
                    else
                        cPart = "(" + cPart + ") * ";
                }
            } else if ("R".Equals(format))
            {
                if (Radicand.IsOne)
                    return Coefficient.ToString();
                Rational r = new Rational(Coefficient.Numerator * Coefficient.Numerator * Radicand, Coefficient.Denominator * Coefficient.Denominator);
                int sign = (Coefficient >= 0) ? 1 : -1;
                if (sign == -1)
                    rPart += "-";
                rPart += "Sqrt(" + r.CanonicalForm.ToString() + ")";
            }
            
            return cPart + rPart;
        }

        public override string ToString()
        {
            return ToString("S", CultureInfo.InvariantCulture);
        }
    }
}
