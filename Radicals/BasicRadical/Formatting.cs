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
            if (C.IsZero)
                return "0";
            if (R.IsZero)
                return "0";

            string cPart = "";
            string rPart = "";

            if ("S".Equals(format))
            {
                if (!C.IsOne)
                    cPart = C.ToString();
                if (!R.IsOne)
                    rPart = "Sqrt(" + R.ToString() + ")";
                if (this == One)
                    cPart = "1";
                if (cPart.Length > 0 && rPart.Length > 0)
                {
                    if (C.Denominator == 1)
                        cPart = cPart + " * ";
                    else
                        cPart = "(" + cPart + ") * ";
                }
            } else if ("R".Equals(format))
            {
                if (R.IsOne)
                    return C.ToString();
                Rational r = new Rational(C.Numerator * C.Numerator * R, C.Denominator * C.Denominator);
                int sign = (C >= 0) ? 1 : -1;
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
