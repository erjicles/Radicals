using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSum
    {
        public static Radical[] SimplifyRadicals(Radical[] basicRadicals)
        {
            if (basicRadicals == null)
                return null;
            if (basicRadicals.Length == 0)
                return new Radical[0];
            if (basicRadicals.Length == 1)
                return basicRadicals;

            Dictionary<BigInteger, Radical> uniqueRadicals = new Dictionary<BigInteger, Radical>();
            for (int i = 0; i < basicRadicals.Length; i++)
            {
                Radical b = basicRadicals[i];
                if (uniqueRadicals.ContainsKey(b.Radicand))
                    uniqueRadicals[b.Radicand] = Radical.AddCompatible(uniqueRadicals[b.Radicand], basicRadicals[i]);
                else if (Radical.Zero != b)
                    uniqueRadicals[b.Radicand] = b;
            }

            Radical[] result =
                uniqueRadicals.Values
                .Where(f => f.Coefficient != 0 && f.Radicand != 0)
                .OrderBy(f => f.Radicand)
                .ToArray();
            if (result.Length == 0)
                result = new Radical[1] { Radical.Zero };
            return result;
        }
    }
}
