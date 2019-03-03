using Rationals;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Radicals
{
    public readonly partial struct BasicRadical 
    {
        // R = Coefficient * sqrt(Radicand)
        public readonly Rational c_original;
        public readonly BigInteger radicand_unsimplified;
        private readonly Rational _coefficient;
        private readonly BigInteger _radicand;

        public Rational Coefficient
        {
            get
            {
                if (_coefficient == null)
                    return 0;
                return _coefficient;
            }
        }
        public BigInteger Radicand
        {
            get
            {
                if (_radicand == null)
                    return 0;
                return _radicand;
            }
        }

        public BasicRadical(Rational r)
            : this(new Rational(1, r.Denominator), r.Numerator * r.Denominator)
        {
        }

        public BasicRadical(Rational c, BigInteger r)
        {
            c_original = c;
            radicand_unsimplified = r;
            ToSimplestForm(
                c_orig: c,
                r_orig: r,
                c_final: out this._coefficient,
                r_final: out this._radicand);
        }
        
    }
}
