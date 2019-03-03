using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSumRatio : IFormattable
    {
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (RadicalSum.One == Denominator)
                return Numerator.ToString(format, formatProvider);
            return "[" + Numerator.ToString(format, formatProvider) + "] / [" + Denominator.ToString(format, formatProvider) + "]";
        }

        public override string ToString()
        {
            return ToString("S", CultureInfo.InvariantCulture);
        }
    }
}
