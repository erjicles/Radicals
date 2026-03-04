using Radicals.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Radicals.Polynomials;

/// <summary>
/// Sparse‑aware polynomial with generic exact coefficients.
/// Coefficients are stored as a sparse dictionary: degree → coefficient.
/// </summary>
internal class Polynomial<T>
    :
        IAdditionOperators<Polynomial<T>, Polynomial<T>, Polynomial<T>>,
        IAdditiveIdentity<Polynomial<T>, Polynomial<T>>,
        IMultiplicativeIdentity<Polynomial<T>, Polynomial<T>>,
        IMultiplyOperators<Polynomial<T>, Polynomial<T>, Polynomial<T>>,
        ISubtractionOperators<Polynomial<T>, Polynomial<T>, Polynomial<T>>
    where T :
        IAdditionOperators<T, T, T>,
        IAdditiveIdentity<T, T>,
        IComparisonOperators<T, T, bool>,
        IMultiplicativeIdentity<T, T>,
        IMultiplyOperators<T, T, T>,
        ISubtractionOperators<T, T, T>
{
    private readonly Dictionary<int, T> _coefficientsByDegree;

    public Polynomial(Dictionary<int, T> coefficientsByDegree)
    {
        _coefficientsByDegree = coefficientsByDegree.ToDictionary();

        ToSimplestForm();
    }

    public static Polynomial<T> One => CreateConstant(T.MultiplicativeIdentity);

    public static Polynomial<T> Zero => CreateConstant(T.AdditiveIdentity);

    public IReadOnlyDictionary<int, T> Coefficients => _coefficientsByDegree;

    public int Degree => _coefficientsByDegree.Count == 0 ? -1 : _coefficientsByDegree.Keys.Max();
    
    public static Polynomial<T> CreateConstant(T value) => new(new Dictionary<int, T> { [0] = value });

    public T GetConstantTerm()
    {
        return Coefficients.GetValueOrDefault(0);
    }

    public Polynomial<T> Add(Polynomial<T> other)
    {
        var res = new Dictionary<int, T>(_coefficientsByDegree);
        foreach (var (deg, coeff) in other._coefficientsByDegree)
        {
            res.TryGetValue(deg, out var existing);
            var sum = existing + coeff;
            if (sum == T.AdditiveIdentity)
                res.Remove(deg);
            else
                res[deg] = sum;
        }
        return new Polynomial<T>(res);
    }

    public Polynomial<T> DivideByX()
    {
        return new(_coefficientsByDegree.ToDictionary(kvp => kvp.Key - 1, kvp => kvp.Value));
    }

    public T EvaluateAt(T value)
    {
        T result = T.AdditiveIdentity;

        var evaluatedPowers = new Dictionary<string, T>();

        foreach (var (degree, coefficient) in _coefficientsByDegree)
        {
            result += coefficient * value.Pow(degree);
        }

        return result;
    }

    public Polynomial<T> Multiply(Polynomial<T> other)
    {
        if (IsZero || other.IsZero)
        {
            return Zero;
        }

        /// Sparse O(m·exp) convolution.
        var prod = new Dictionary<int, T>();
        foreach (var (i, a) in _coefficientsByDegree)
        {
            foreach (var (j, b) in other._coefficientsByDegree)
            {
                int deg = i + j;
                prod.TryGetValue(deg, out var existing);
                var sum = existing + a * b;
                if (sum == T.AdditiveIdentity)
                {
                    prod.Remove(deg);
                }
                else
                {
                    prod[deg] = sum;
                }
            }
        }

        return new Polynomial<T>(prod);
    }

    public Polynomial<T> Subtract(Polynomial<T> other)
    {
        var resultCoefficients = new Dictionary<int, T>();

        foreach (var (degree, coefficient) in _coefficientsByDegree)
        {
            resultCoefficients[degree] = coefficient;
        }

        foreach (var (degree, coefficient) in other.Coefficients)
        {
            resultCoefficients.TryAdd(degree, T.AdditiveIdentity);
            resultCoefficients[degree] -= coefficient;
        }

        return new(resultCoefficients);
    }

    public static Polynomial<T> operator +(Polynomial<T> left, Polynomial<T> right)
    {
        return left.Add(right);
    }

    public static Polynomial<T> operator *(Polynomial<T> left, Polynomial<T> right)
    {
        return left.Multiply(right);
    }

    public static Polynomial<T> operator -(Polynomial<T> left, Polynomial<T> right)
    {
        return left.Subtract(right);
    }

    /* ---------- Helpers ---------- */

    public bool IsDivisibleByX
    {
        get
        {
            return _coefficientsByDegree.GetValueOrDefault(0) == T.AdditiveIdentity;
        }
    }

    public bool IsZero => _coefficientsByDegree.All(kvp => kvp.Value == T.AdditiveIdentity);

    public static Polynomial<T> AdditiveIdentity => Zero;

    public static Polynomial<T> MultiplicativeIdentity => One;

    public override string ToString()
    {
        if (IsZero) return "0";
        var terms = new List<string>();
        foreach (var kv in _coefficientsByDegree.OrderByDescending(kv => kv.Key))
        {
            int i = kv.Key;
            var coeff = kv.Value;
            string monomial = i switch
            {
                0 => $"{coeff}",
                1 => $"{coeff}·x",
                _ => $"{coeff}·x^{i}"
            };
            terms.Add(monomial);
        }
        return string.Join(" + ", terms);
    }

    private void ToSimplestForm()
    {
        var degreesToRemove = _coefficientsByDegree
            .Where(kvp => kvp.Value == T.AdditiveIdentity)
            .Select(kv => kv.Key)
            .ToList();

        foreach (var degree in degreesToRemove)
        {
            _coefficientsByDegree.Remove(degree);
        }
    }
}