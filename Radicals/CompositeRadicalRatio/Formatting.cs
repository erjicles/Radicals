﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio : IFormattable
    {
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public override string ToString()
        {
            if (CompositeRadical.One == Denominator)
                return Numerator.ToString();
            return "[" + Numerator.ToString() + "] / [" + Denominator.ToString() + "]";
        }
    }
}
