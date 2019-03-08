using Open.Numeric.Primes;
using Rationals;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Radicals.Polynomials
{
    internal readonly struct Polynomial
        : IFormattable
    {
        private readonly PolynomialTerm[] _terms;
        public PolynomialTerm[] Terms
        {
            get
            {
                if (_terms == null)
                    return new PolynomialTerm[1] { PolynomialTerm.Zero };
                return _terms;
            }
        }

        public Polynomial(RadicalSum constant)
            : this(new PolynomialTerm[1] { new PolynomialTerm(constant)})
        {
        }

        public Polynomial(PolynomialTerm term)
            : this(new PolynomialTerm[1] { term })
        {
        }

        public Polynomial(PolynomialTerm[] terms)
        {
            if (terms == null)
                throw new ArgumentNullException(nameof(terms));
            ToSimplestForm(terms_in: terms, terms_out: out _terms);
        }

        private static void ToSimplestForm(PolynomialTerm[] terms_in, out PolynomialTerm[] terms_out)
        {
            var termDictionary = new Dictionary<int, PolynomialTerm>();
            for (int i = 0; i < terms_in.Length; i++)
            {
                var degree = terms_in[i].Degree;
                if (termDictionary.ContainsKey(degree))
                {
                    var currentTerm = termDictionary[degree];
                    termDictionary[degree] = new PolynomialTerm(currentTerm.Coefficient + terms_in[i].Coefficient, degree);
                }
                else if (terms_in[i] != PolynomialTerm.Zero)
                {
                    termDictionary.Add(terms_in[i].Degree, terms_in[i]);
                }
            }
            var result = 
                termDictionary.Values
                .Where(t => t != PolynomialTerm.Zero)
                .OrderBy(t => t.Degree)
                .ToArray();
            if (result.Length == 0)
                result = new PolynomialTerm[1] { PolynomialTerm.Zero };
            terms_out = result;
        }

        public void ExtractTermsContainingNthRoot(
            BigInteger radicand, 
            BigInteger index,
            out Polynomial p_reduced, 
            out Polynomial p_extract)
        {
            p_reduced = Polynomial.Zero;
            p_extract = Polynomial.Zero;
            for (int i = 0; i < Terms.Length; i++)
            {
                var coeff = Terms[i].Coefficient;
                RadicalSum coeff_reduced = coeff;
                RadicalSum coeff_target = RadicalSum.Zero;
                for (int j = 0; j < coeff.Radicals.Length; j++)
                {
                    if (coeff.Radicals[j].Index == index)
                    {
                        if (coeff.Radicals[j].Radicand % radicand == 0)
                        {
                            coeff_target += coeff.Radicals[j];
                            coeff_reduced -= coeff.Radicals[j];
                        }
                    }
                }
                p_reduced += new PolynomialTerm(coeff_reduced, Terms[i].Degree);
                p_extract += new PolynomialTerm(coeff_target, Terms[i].Degree);
            }
        }

        public RadicalSum GetConstantTerm()
        {
            for (int i = 0; i < Terms.Length; i++)
                if (Terms[i].Degree == 0)
                    return Terms[i].Coefficient;
            return RadicalSum.Zero;
        }

        public Tuple<BigInteger,BigInteger>[] GetUniquePrimeNthRoots()
        {
            // Set of found index/radicand pairs
            var foundPrimeNthRoots = new HashSet<Tuple<BigInteger,BigInteger>>();
            for (int i = 0; i < Terms.Length; i++)
            {
                for (int j = 0; j < Terms[i].Coefficient.Radicals.Length; j++)
                {
                    var r = Terms[i].Coefficient.Radicals[j];
                    foreach (BigInteger primeFactor in Prime.Factors(r.Radicand))
                    {
                        var key = new Tuple<BigInteger, BigInteger>(r.Index, primeFactor);
                        if (!foundPrimeNthRoots.Contains(key))
                            foundPrimeNthRoots.Add(key);
                    }
                }
            }
            return foundPrimeNthRoots.OrderBy(k => k.Item1).ThenBy(k => k.Item2).ToArray();
        }

        public BigInteger GetLargestRadicand()
        {
            BigInteger largestRadicand = 0;
            for (int i = 0; i < Terms.Length; i++)
            {
                for (int j = 0; j < Terms[i].Coefficient.Radicals.Length; j++)
                {
                    if (Terms[i].Coefficient.Radicals[j].Radicand > largestRadicand)
                        largestRadicand = Terms[i].Coefficient.Radicals[j].Radicand;
                }
            }
            return largestRadicand;
        }

        public bool IsDivisibleByX
        {
            get
            {
                for (int i = 0; i < Terms.Length; i++)
                {
                    if (Terms[i].Degree == 0)
                        return false;
                }
                return true;
            }
        }

        public static Polynomial Zero = new Polynomial(new PolynomialTerm[1] { PolynomialTerm.Zero });
        public static Polynomial One = new Polynomial(new PolynomialTerm[1] { PolynomialTerm.One });

        public static Polynomial Negate(Polynomial value)
        {
            var terms = new PolynomialTerm[value.Terms.Length];
            for (int i = 0; i < value.Terms.Length; i++)
                terms[i] = -value.Terms[i];
            var result = new Polynomial(terms);
            return result;
        }

        public static Polynomial Add(Polynomial left, Polynomial right)
        {
            var terms = new PolynomialTerm[left.Terms.Length + right.Terms.Length];
            left.Terms.CopyTo(terms, 0);
            right.Terms.CopyTo(terms, left.Terms.Length);
            var result = new Polynomial(terms);
            return result;
        }

        public static Polynomial Subtract(Polynomial left, Polynomial right)
        {
            return Add(left, -right);
        }

        public static Polynomial Multiply(Polynomial left, Polynomial right)
        {
            var terms = new PolynomialTerm[left.Terms.Length * right.Terms.Length];
            for (int i = 0; i < left.Terms.Length; i++)
                for (int j = 0; j < right.Terms.Length; j++)
                    terms[(i * right.Terms.Length) + j] = left.Terms[i] * right.Terms[j];
            return new Polynomial(terms);
        }

        public static Polynomial Divide(Polynomial left, Radical right)
        {
            var terms = new PolynomialTerm[left.Terms.Length];
            for (int i = 0; i < left.Terms.Length; i++)
                terms[i] = new PolynomialTerm(left.Terms[i].Coefficient / right, left.Terms[i].Degree);
            return new Polynomial(terms);
        }

        public static Polynomial DivideByX(Polynomial value)
        {
            var terms = new PolynomialTerm[value.Terms.Length];
            for (int i = 0; i < value.Terms.Length; i++)
                terms[i] = new PolynomialTerm(value.Terms[i].Coefficient, value.Terms[i].Degree - 1);
            var result = new Polynomial(terms);
            return result;
        }

        public static Polynomial Pow(Polynomial value, BigInteger exponent)
        {
            // This algorithm is SLOW
            // Speed up using multinomial theorem:
            // https://en.wikipedia.org/wiki/Multinomial_theorem
            // Need to partition exponent as a sum of at most value.Terms.Length integers
            var result = One;
            for (BigInteger i = 0; i < exponent; i++)
                result *= value;
            return result;
        }

        public static Polynomial operator -(Polynomial value)
        {
            return Negate(value);
        }

        public static Polynomial operator +(Polynomial value)
        {
            return value;
        }

        public static Polynomial operator +(Polynomial left, Polynomial right)
        {
            return Add(left, right);
        }

        public static Polynomial operator +(Polynomial left, PolynomialTerm right)
        {
            return Add(left, new Polynomial(right));
        }

        public static Polynomial operator +(PolynomialTerm left, Polynomial right)
        {
            return Add(new Polynomial(left), right);
        }

        public static Polynomial operator -(Polynomial left, Polynomial right)
        {
            return Subtract(left, right);
        }

        public static Polynomial operator -(Polynomial left, PolynomialTerm right)
        {
            return Subtract(left, new Polynomial(right));
        }

        public static Polynomial operator -(PolynomialTerm left, Polynomial right)
        {
            return Subtract(new Polynomial(left), right);
        }

        public static Polynomial operator *(Polynomial left, Polynomial right)
        {
            return Multiply(left, right);
        }

        public static Polynomial operator *(Polynomial left, PolynomialTerm right)
        {
            return Multiply(left, new Polynomial(right));
        }

        public static Polynomial operator *(PolynomialTerm left, Polynomial right)
        {
            return Multiply(new Polynomial(left), right);
        }

        public static Polynomial operator *(Polynomial left, Rational right)
        {
            return Multiply(left, new Polynomial(right));
        }

        public static Polynomial operator *(Rational left, Polynomial right)
        {
            return Multiply(new Polynomial(left), right);
        }

        public static Polynomial operator /(Polynomial left, Radical right)
        {
            return Divide(left, right);
        }

        public RadicalSum EvaluateAt(RadicalSum value)
        {
            RadicalSum result = RadicalSum.Zero;
            for (int i = 0; i < Terms.Length; i++)
            {
                RadicalSum termValue = Terms[i].Coefficient;
                for (int j = 0; j < Terms[i].Degree; j++)
                    termValue *= value;
                result += termValue;
            }
            return result;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var result = new StringBuilder();
            for (int i = 0; i < Terms.Length; i++)
            {
                if (result.Length > 0)
                    result.Append(" + ");
                result.Append(Terms[i].ToString(format, formatProvider));
            }
            return result.ToString();
        }

        public override string ToString()
        {
            return ToString(null, CultureInfo.InvariantCulture);
        }

    }
}
