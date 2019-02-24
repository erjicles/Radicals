using System;
using System.Collections.Generic;
using System.Text;

namespace Radicals
{
    public partial struct Radical
    {
        /// <summary>
        /// Zero
        /// </summary>
        public static readonly Radical Zero = new Radical(0, 0);

        /// <summary>
        /// One
        /// </summary>
        public static readonly Radical One = new Radical(1, 1);
    }
}
