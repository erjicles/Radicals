using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadicalRatio
    {
        public bool IsRational()
        {
            if (Numerator.Radicals.Length == 1)
                if (Denominator.Radicals.Length == 1)
                    if (Numerator.Radicals[0].R == 1)
                        if (Denominator.Radicals[0].R == 1)
                            return true;
            return false;
        }
    }
}
