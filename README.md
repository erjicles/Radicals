# Radicals

## Overview
.NET implementation of
[radical expressions](https://en.wikipedia.org/wiki/Nth_root),
where the radicand is a rational number and the index is a positive integer.
Enables arithmetic and string formatting of radical expressions, and can 
handle rational radicands of arbitrary precision (see dependencies).
Seamlessly integrates with other numeric types such as int, long, and BigInteger.

NuGet package for this library is published [here](https://www.nuget.org/packages/DelSquared.Radicals/).

## Usage
This library provides three structures to enable radical expression arithmetic:

* Radical
* RadicalSum
* RadicalSumRatio

### Radical
The Radical structure encapsulates a single radical expression with rational radicand and positive
integer index:
```csharp
var sqrt2 = Radical.Sqrt(2);                        // Sqrt(2)
var sqrt_1_3 = Radical.Sqrt((Rational)1/3);         // Sqrt(1/3)    = (1/3)*Sqrt(3)
var root_3_1_2 = Radical.NthRoot((Rational)1/2, 3); // Root[3](1/2) = (1/2)*Root[3](4)
```
*Note - see dependencies section for information on the Rational structure*

The structure automatically simplifies radicals to simplest form, as described [here](https://en.wikipedia.org/wiki/Nth_root#Simplified_form_of_a_radical_expression).

Radicals can be multiplied and divided by other radicals, returning new Radicals:

```csharp
var result1 = Radical.Sqrt(2) * Radical.Sqrt(3/4);  // Sqrt(2) * Sqrt(3/4)       = (1/2)*Sqrt(6)
var result2 = result1 * Radical.Sqrt(2);            // (1/2)*Sqrt(6)*Sqrt(2)     = Sqrt(3)
var result3 = result2 / result1;                    // Sqrt(3) / [(1/2)*Sqrt(6)] = Sqrt(2)
var result4 = result3 / Radical.Sqrt(5);            // Sqrt(2) / Sqrt(5)         = (1/5)*Sqrt(10)
```

They can also be multiplied and divided by most numeric types, returning new Radicals:

```csharp
var result1 = Radical.Sqrt(2) * 3;                  // 3 * Sqrt(2)
var result2 = 4 * result1;                          // 4 * 3 * Sqrt(2)   = 12 * Sqrt(2)
var result3 = result2 / 3;                          // 12 * Sqrt(2) / 3  = 4 * Sqrt(2)
var result4 = 5 / result3;                          // 5 / (4 * Sqrt(2)) = (5/8)*Sqrt(2)
```

Additionally, Radicals of differing indices can be multiplied and divided:

```csharp
var result1 = Radical.Sqrt(2) 
    * Radical.NthRoot(2,3);                         // Sqrt(2) * Root[3](2)     = Root[6](32)
var result2 = result1 / Root[3](5);                 // Root[6](32) / Root[3](5) = (1/5)*Root[6](20000)
```

### RadicalSum
Radicals can also be added and subtracted from each other. However, unless they have
identical indices and radicands, the simplest form result is not another radical, but
instead is a sum of radicals. The RadicalSum structure encapsulates this notion and 
enables Radical addition and subtraction:

```csharp
var result1 = Radical.Sqrt(2) + Radical.Sqrt(3);    // Sqrt(2) + Sqrt(3)
var result2 = result1 + Radical.Sqrt(2);            // [Sqrt(2) + Sqrt(3)] + Sqrt(2) = 2*Sqrt(2) + Sqrt(3)
var result3 = result1 - Radical.NthRoot(5,3);       // 2*Sqrt(2) + Sqrt(3) + (-1)*Root[3](5)
var result4 = result3 + 11;                         // 11 + 2*Sqrt(2) + Sqrt(3) + (-1)*Root[3](5)
```

Similar to Radicals, RadicalSums can be multiplied by most numeric types, Radicals, and
other RadicalSums to return new RadicalSums...

```csharp
var result1 = 
    (Radical.Sqrt(2) + Radical.Sqrt(3)) 
        * Radical.Sqrt(2);                          // [Sqrt(2) + Sqrt(3)] * Sqrt(2) = 2 + Sqrt(6)
var result2 = 
    result1
        * (Radical.Sqrt(5) + Radical.Sqrt(7));      // [2 + Sqrt(6)] * [Sqrt(5) + Sqrt(7)]
                                                    // = 2*Sqrt(5) + 2*Sqrt(7) + Sqrt(30) + Sqrt(35)
                                                    
var result3 = result2 * 3;                          // 6*Sqrt(5) + 6*Sqrt(7) + 3*Sqrt(30) + 3*Sqrt(35)
```
...as well as divided by most numeric types and Radicals to return new RadicalSums:
```csharp
var result4 = result3 / 2;                          // 3*Sqrt(5) + 3*Sqrt(7) + (3/2)*Sqrt(30) + (3/2)*Sqrt(35)
var result5 = result4 / Radical.Sqrt(2);            // (3/2)*Sqrt(10) + (3/2)*Sqrt(14) + (3/2)*Sqrt(15) + (3/4)*Sqrt(70)
```

### RadicalSumRatio
RadicalSums can also be divided by other RadicalSums. However the result is not
another RadicalSum, but is instead a RadicalSumRatio where the numerator and
denominator are both RadicalSums:

```csharp
var result1 =
 (Radical.Sqrt(2) + Radical.Sqrt(3))
     / (Radical.Sqrt(2) - Radical.Sqrt(3));         // Sqrt(2) + Sqrt(3)
                                                    // -----------------
                                                    // Sqrt(2) - Sqrt(3)
```

All arithmetic operators between RadicalSumRatios and numeric types, Radicals, and RadicalSums
will return new RadicalSumRatios.

## Performance
This project was intended as a small fun side project. As such, I haven't put much
effort or thought into optimization. While it suited my needs and should be okay for
most casual implementations, don't expect optimal performance and **use at your own
risk!** Of course, I'm always open to suggestions and improvements. :-)

## Background
The original inspiration for this project came from working on a program that recursively calculates
[Clebsch-Gordan coefficients](https://en.wikipedia.org/wiki/Clebsch%E2%80%93Gordan_coefficients). While
this succeeded in generating the coefficients in decimal form, I wanted it to present them in radical
form similarly to how they're usually presented in tables (e.g.,
[here](https://en.wikipedia.org/wiki/Table_of_Clebsch%E2%80%93Gordan_coefficients)). I created this
library to enable radical expression arithmetic during the calculation of the coefficients, and to
format the result as usually presented in tables.

My Clebsch-Gordan coefficient calculator utilizing the Radicals package can be found
[here](https://github.com/erjicles/ClebschGordanCoefficientCalculator).

## Dependencies
This project is dependent on the following NuGet packages:

[Rationals](https://www.nuget.org/packages/Rationals/): Encapsulates rational numbers of theoretically
arbitrary precision. Based on [BigInteger](https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2).

[Open.Numeric.Primes](https://www.nuget.org/packages/Open.Numeric.Primes/): Provides methods to get
prime factorization of BigInteger values.
