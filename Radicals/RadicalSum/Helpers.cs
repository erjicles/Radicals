using Open.Numeric.Primes;
using Rationals;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Radicals
{
    public readonly partial struct RadicalSum
    {
        private void GetCommonFactors(out BigInteger[] upstairs, out BigInteger[] downstairs)
        {
            var numerators = new BigInteger[Radicals.Length];
            var denominators = new BigInteger[Radicals.Length];
            for (int i = 0; i < Radicals.Length; i++)
            {
                numerators[i] = Radicals[i].Coefficient.Numerator;
                denominators[i] = Radicals[i].Coefficient.Denominator;
            }
            var numeratorFactors = new List<BigInteger>();
            var denominatorFactors = new List<BigInteger>();
            foreach (BigInteger factor in Prime.CommonFactors(numerators))
                numeratorFactors.Add(factor);
            foreach (BigInteger factor in Prime.CommonFactors(denominators))
                denominatorFactors.Add(factor);
            // Prime.CommonFactors doesn't get -1, account for this:
            BigInteger sign = -1;
            foreach (BigInteger numerator in numerators)
                if (numerator >= 0)
                    sign = 1;
            if (sign == -1)
                numeratorFactors.Add(sign);

            numeratorFactors.Sort();
            denominatorFactors.Sort();
            upstairs = numeratorFactors.ToArray();
            downstairs = denominatorFactors.ToArray();
        }

        public Rational GetCommonFactor()
        {
            BigInteger[] upstairs = new BigInteger[0];
            BigInteger[] downstairs = new BigInteger[0];
            GetCommonFactors(upstairs: out upstairs, downstairs: out downstairs);
            var result = new Rational(Utilities.CombineFactors(upstairs), Utilities.CombineFactors(downstairs));
            return result.CanonicalForm;
        }

        /// <summary>
        /// Returns multiplicative inverse, i.e., B such that this * B = 1
        /// </summary>
        /// <returns></returns>
        public static RadicalSum GetRationalizer(RadicalSum value)
        {
            // Method derived from:
            // Rationalizing Denominators
            // Allan Berele and Stefan Catoiu
            // https://www.jstor.org/stable/10.4169/mathmaga.88.issue-2
            // See Example 9

            if (value == 0)
                throw new Exception("Cannot get rationalization of zero");

            // Solve for the largest field extensions in turn
            // S = r_1 + r_2 + ... + r_n
            // r_n = S - r_1 - r_2 - ... - r_(n-1)
            // r_n^2 = [S - r_1 - r_2 - ... - r_(n-1)]^2

            
            // Create polynomial in S
            // S - r_1 - r_2 - ... - r_n = 0
            var term1 = new Polynomials.PolynomialTerm(1, 1);
            var term0 = new Polynomials.PolynomialTerm(-value, 0);
            var p = new Polynomials.Polynomial(new Polynomials.PolynomialTerm[2] { term0, term1 });

            // Get the set of unique radicands
            var uniqueRadicands = p.GetUniquePrimeRadicands();

            // Main loop
            for (int i = uniqueRadicands.Length - 1; i >= 0; i--)
            {
                var currentRadicand = uniqueRadicands[i];
                if (currentRadicand < 2)
                    break;
                // Solve for largest radicand
                // p = p_reduced + p_extract = 0
                // p_extract = sqrt(largestRadicand) * p' for some p'
                Polynomials.Polynomial p_reduced;
                Polynomials.Polynomial p_extract;
                p.ExtractTermsContainingRadicand(
                    radicand: currentRadicand,
                    p_reduced: out p_reduced,
                    p_extract: out p_extract);

                // Remove sqrt(largestRadicand)
                var r = new Radical(currentRadicand);
                p_extract /= r;

                // p_reduced = -sqrt(currentRadicand) * p_extract
                // p_reduced ^2 = currentRadicand * p_extract^2
                // p_reduced^2 - currentRadicand * p_extract^2 = 0
                // p === p_reduced^2 - currentRadicand * p_extract^2
                var left = p_reduced * p_reduced;
                var right = p_extract * p_extract * currentRadicand;
                p = left - right;
            }

            // We now have a polynomial in S with all radicals removed:
            // a_0 + a_1 * S + a_2 * S^2 + ... + a_n * S^n = 0
            // Radicalizer then becomes:
            // -a_0 / S = a_1 + a_2 * S + ... + a_n * S^(n-1)
            //
            // 1   a_1 + a_2 * S + ... + a_n * S^(n-1)
            // - = -----------------------------------
            // S                 -a_0                 
            var constantTerm = p.GetConstantTerm();
            var constantP = new Polynomials.Polynomial(constantTerm);
            p -= constantP;
            if (!p.IsDivisibleByX)
                throw new Exception("All terms should have at least degree 1");
            if (!constantTerm.IsRational())
                throw new Exception("No radicals should remain in any terms");
            p = Polynomials.Polynomial.DivideByX(p);
            p /= (Rational)(-constantTerm);

            var rationalizer = p.EvaluateAt(value);

            return rationalizer;
        }

        
    }
}
