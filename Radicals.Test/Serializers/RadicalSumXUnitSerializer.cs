using Radicals;
using Rationals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Sdk;

[assembly: RegisterXunitSerializer(typeof(Radicals.Test.Serializers.RadicalSumXUnitSerializer), typeof(RadicalSum))]

namespace Radicals.Test.Serializers;

public class RadicalSumXUnitSerializer : IXunitSerializer
{
    public object Deserialize(Type type, string serializedValue)
    {
        if (type != typeof(RadicalSum))
        {
            throw new InvalidOperationException($"{nameof(RadicalSumXUnitSerializer)} cannot deserialize type {type}.");
        }

        var deserializedHolder = JsonSerializer.Deserialize<RadicalSumSerialForm>(serializedValue);
        return deserializedHolder.ToRadicalSum();
    }

    public bool IsSerializable(Type type, object value, out string failureReason)
    {
        if (type != typeof(RadicalSum))
        {
            failureReason = $"Type {type} is not a {nameof(RadicalSum)}.";
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

        if (value is not RadicalSum radicalSum)
        {
            throw new InvalidOperationException($"{nameof(RadicalSumXUnitSerializer)} cannot serialize type {value.GetType()}.");
        }

        return JsonSerializer.Serialize(radicalSum.ToSerialForm());
    }
}

public static class RadicalSumExtensions
{
    public static RadicalSumSerialForm ToSerialForm(this RadicalSum radicalSum)
        => new([.. radicalSum.Radicals.Select(r => r.ToSerialForm())]);
}

public static class RadicalSumSerialFormExtensions
{
    public static RadicalSum ToRadicalSum(this RadicalSumSerialForm radicalSumSerialForm)
        => new([.. radicalSumSerialForm.Radicals.Select(r => r.ToRadical())]);
}

public record RadicalSumSerialForm(RadicalSerialForm[] Radicals);