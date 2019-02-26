using Rationals;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Radicals
{
    public readonly partial struct BasicRadical 
    {
        // R = c * sqrt(r)
        public readonly Rational c_original;
        public readonly BigInteger r_original;
        private readonly Rational _c;
        private readonly BigInteger _r;

        public Rational C
        {
            get
            {
                if (_c == null)
                    return 0;
                return _c;
            }
        }
        public BigInteger R
        {
            get
            {
                if (_r == null)
                    return 0;
                return _r;
            }
        }

        public BasicRadical(Rational r)
            : this(new Rational(1, r.Denominator), r.Numerator * r.Denominator)
        {
        }

        public BasicRadical(Rational c, BigInteger r)
        {
            c_original = c;
            r_original = r;
            ToSimplestForm(
                c_orig: c,
                r_orig: r,
                c_final: out this._c,
                r_final: out this._r);
        }
        
    }
}
