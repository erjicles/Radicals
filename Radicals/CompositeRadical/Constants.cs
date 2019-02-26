using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public readonly partial struct CompositeRadical
    {
        /// <summary>
        /// Zero
        /// </summary>
        public static readonly CompositeRadical Zero = new CompositeRadical(0, 0);

        /// <summary>
        /// One
        /// </summary>
        public static readonly CompositeRadical One = new CompositeRadical(1, 1);
    }
}
