using Rationals;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Radicals
{
    public readonly partial struct Radical 
    {
        // R = Coefficient * sqrt(Radicand)
        public readonly Rational coefficient_unsimplified;
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

        public Radical(Rational radicand)
            : this(new Rational(1, radicand.Denominator), radicand.Numerator * radicand.Denominator)
        {
        }

        public Radical(Rational coefficient, BigInteger radicand)
        {
            if (radicand < 0)
                throw new ArgumentException("Cannot have negative radicand");
            coefficient_unsimplified = coefficient;
            radicand_unsimplified = radicand;
            ToSimplestForm(
                c_orig: coefficient,
                r_orig: radicand,
                c_final: out this._coefficient,
                r_final: out this._radicand);
        }
        
    }
}
