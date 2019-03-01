# Radicals .NET

*Implementation of square roots where the number under the radical is rational.*

```csharp
var left = new BasicRadical(new Rational(3/4)); // Sqrt(3/4)
var right = new BasicRadical(new Rational(1/3)); // Sqrt(1/3)

var sum = left + right; // Equal to (5/6) * Sqrt(3)
```

## Overview


## Dependencies
This project is dependent on the following NuGet packages:  
[Rationals](https://www.nuget.org/packages/Rationals/)
[Open.Numeric.Primes](https://www.nuget.org/packages/Open.Numeric.Primes/)