using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct Radical
    {
        public bool IsRational()
        {
            if (Radicand < 2)
                return true;
            return false;
        }
    }
}
