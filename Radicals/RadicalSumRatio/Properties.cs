using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSumRatio
    {
        public bool IsRational()
        {
            if (Numerator.Radicals.Length == 1)
                if (Denominator.Radicals.Length == 1)
                    if (Numerator.Radicals[0].Radicand == 1)
                        if (Denominator.Radicals[0].Radicand == 1)
                            return true;
            return false;
        }
    }
}
