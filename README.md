# Radicals

## Overview
.NET implementation of
[radical expressions](https://en.wikipedia.org/wiki/Nth_root),
where the radicand is a rational number and the index is a positive integer.
Enables arithmetic of radical expressions, string formatting of radical
expressions, and can handle rational radicands of arbitrary precision
(see dependencies). Seamlessly integrates with other numeric types such as
int, double, and BigInteger.

## Usage
This library provides three primary structures to enable radical expression arithmetic:

* Radical
* RadicalSum
* RadicalSumRatio

### Radical
The Radical structure encapsulates a single radical expression with rational radicand and positive
integer index:
```csharp
var sqrt_3_4 = Radical.Sqrt((Rational)3/4);         // Sqrt(3/4)    = (1/2)*Sqrt(3)
var sqrt_1_3 = Radical.Sqrt((Rational)1/3);         // Sqrt(1/3)    = (1/3)*Sqrt(3)
var root_3_1_2 = Radical.NthRoot((Rational)1/2, 3); // Root[3](1/2) = (1/2)*Root[3](4)
```
*Note - see dependencies section for information on the Rational class*

Automatically simplifies radicals to simplest form, as described [here](https://en.wikipedia.org/wiki/Nth_root#Simplified_form_of_a_radical_expression).


## Background
The original inspiration for this project came from working on a program that would recursively generate
Clebsch-Gordan coefficients (https://en.wikipedia.org/wiki/Clebsch%E2%80%93Gordan_coefficients). While
this succeeded in generating the coefficients in decimal form, I wanted it to present them in radical
form similarly to how they're usually presented in tables (e.g.,
[here](https://en.wikipedia.org/wiki/Table_of_Clebsch%E2%80%93Gordan_coefficients). I created this
library to enable radical expression arithmetic during the calculation of the coefficients, and to
string format them as usually presented in tables.

## Dependencies
This project is dependent on the following NuGet packages:

[Rationals](https://www.nuget.org/packages/Rationals/): Encapsulates rational numbers of theoretically
arbitrary precision. Based on [BigInteger](https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2).

[Open.Numeric.Primes](https://www.nuget.org/packages/Open.Numeric.Primes/): Provides methods to get
prime factorization of BigInteger values.
