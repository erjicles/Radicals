using Rationals;
using System.Numerics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Radicals.Test")]

namespace Radicals
{
    internal readonly partial struct BasicRadical 
    {
        // R = c * sqrt(r)
        public readonly Rational c_original;
        public readonly BigInteger r_original;
        public readonly Rational c;
        public readonly BigInteger r;

        public BasicRadical(Rational c, BigInteger r)
        {
            c_original = c;
            r_original = r;
            ToSimplestForm(
                c_orig: c,
                r_orig: r,
                c_final: out this.c,
                r_final: out this.r);
        }
        
    }
}
