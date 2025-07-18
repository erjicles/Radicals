using Radicals;
using Rationals;
using System;
using System.Numerics;
using System.Text.Json;
using Xunit.Sdk;

[assembly: RegisterXunitSerializer(typeof(Radicals.Test.Serializers.RadicalXUnitSerializer), typeof(Radical))]

namespace Radicals.Test.Serializers;

public class RadicalXUnitSerializer : IXunitSerializer
{
    public object Deserialize(Type type, string serializedValue)
    {
        if (type != typeof(Radical))
        {
            throw new InvalidOperationException($"{nameof(RadicalXUnitSerializer)} cannot deserialize type {type}.");
        }

        var deserializedHolder = JsonSerializer.Deserialize<RadicalSerialForm>(serializedValue);
        return deserializedHolder.ToRadical();
    }

    public bool IsSerializable(Type type, object value, out string failureReason)
    {
        if (type != typeof(Radical))
        {
            failureReason = $"Type {type} is not a {nameof(Radical)}.";
            return false;
        }

        failureReason = null;
        return true;
    }

    public string Serialize(object value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is not Radical radical)
        {
            throw new InvalidOperationException($"{nameof(RadicalXUnitSerializer)} cannot serialize type {value.GetType()}.");
        }

        return JsonSerializer.Serialize(radical.ToSerialForm());
    }
}

public static class RadicalExtensions
{
    public static RadicalSerialForm ToSerialForm(this Radical radical)
        => new(radical.CoefficientUnsimplified.Numerator, radical.CoefficientUnsimplified.Denominator, radical.RadicandUnsimplified, radical.IndexUnsimplified);
}

public static class RadicalSerialFormExtensions
{
    public static Radical ToRadical(this RadicalSerialForm radicalSerialForm)
        => new(new Rational(radicalSerialForm.CoefficientNumerator, radicalSerialForm.CoefficientDenominator), radicalSerialForm.Radicand, radicalSerialForm.Index);
}

public record RadicalSerialForm(BigInteger CoefficientNumerator, BigInteger CoefficientDenominator, BigInteger Radicand, int Index);
