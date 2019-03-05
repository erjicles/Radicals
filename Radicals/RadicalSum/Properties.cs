using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSum
    {
        public bool IsRational()
        {
            for (int i = 0; i < Radicals.Length; i++)
                if (Radicals[i].Radicand > 1)
                    return false;
            return true;
        }
    }
}
