public static class Program
{
    public static void Main()
    {
        Rational fraction1 = new Rational(4, 15);
        Rational fraction2 = new Rational(2, 9);
        RationalCalculator rationalCalculator = new RationalCalculator();
        Rational result;


        result = rationalCalculator.AddRational(fraction1, fraction2);
        Console.WriteLine($"результат сложения: {result.ToString()}");

        result = rationalCalculator.SubtractRational(fraction1, fraction2);
        Console.WriteLine($"результат вычитания: {result.ToString()}");

        result = rationalCalculator.MultiplyRational(fraction1, fraction2);
        Console.WriteLine($"результат умножения: {result.ToString()}");

        result = rationalCalculator.DivideRational(fraction1, fraction2);
        Console.WriteLine($"результат деления: {result.ToString()}");

        Console.WriteLine("--------------------------------------");
        
        RationalComparison rationalComparison = new RationalComparison();
        rationalComparison.IsEqual(fraction1, fraction2);
        fraction1 = new Rational(24, 30);
        fraction2 = new Rational(72, 90);
        rationalComparison.IsEqual(fraction1, fraction2);

    }
}

public class Rational
{
    public int Numerator { get; set; }
    public int Denominator { get; set; }
    public int WholeNum { get; set; }

    public Rational()
    {
    
    }
    public Rational(int numerator, int denumerator)
    {
        Numerator = numerator;
        Denominator = denumerator;
    }
    public Rational(int numerator, int denuminator, int wholeNum)
    {
        Numerator = numerator;
        Denominator = denuminator;
        WholeNum = wholeNum;
    }

    public void ReduceFraction()
    {
        GCDandLCMCalculator GCDeg = new GCDandLCMCalculator(); // экземляр класса для вызова метода для НОДа
        int gcd = GCDeg.CalculateGCD(Numerator, Denominator);
        Numerator /= gcd;
        Denominator /= gcd;
    }

    public bool CheckIfImproper()  // проверка неправильная ли дробь
    {
        return Numerator > Denominator;
    }

    public void MakeProperIfNeeded() // из неправильной в смешаную
    {
        if (CheckIfImproper()) 
        {
            MakeProperFraction(Numerator, Denominator);
        }    
    }

    public void MakeProperFraction(int numerator, int denominator) 
    {
        WholeNum = Math.DivRem(numerator, denominator, out numerator);
        Numerator = numerator;
        Denominator = denominator;
    }

    public override string ToString() 
    {
        if (WholeNum > 0)
        {
            return $"{WholeNum}({Numerator}/{Denominator})";
        }
        else
        {
            return $"{Numerator}/{Denominator}";
        }
    }
}

public class RationalCalculator
{
    GCDandLCMCalculator GCDCalculator = new GCDandLCMCalculator(); // экземляр класса для вызова метода для НОДа
    Rational result = new Rational(); // экземляр класса для возвращения реультата
    public void MakeCommonDenominators(Rational num1, Rational num2)
    {
        int lcm = GCDCalculator.CalculateLCM(num1.Denominator, num2.Denominator); // lcm - least common multiply
        
        num1.Numerator = num1.Numerator * (lcm / num1.Denominator);
        num2.Numerator = num2.Numerator * (lcm / num2.Denominator);

        num1.Denominator = lcm;
        num2.Denominator = lcm;
    }

    public Rational AddRational(Rational num1, Rational num2)
    {
        MakeCommonDenominators(num1, num2);
        result.Numerator = num1.Numerator + num2.Numerator;
        result.Denominator = num1.Denominator;
        result.ReduceFraction();
        result.MakeProperIfNeeded();
        return result;
    }
    public Rational SubtractRational(Rational num1, Rational num2)
    {
        MakeCommonDenominators(num1, num2);
        result.Numerator = num1.Numerator - num2.Numerator;
        result.Denominator = num1.Denominator;
        result.ReduceFraction();
        result.MakeProperIfNeeded();
        return result;
    }
    public Rational MultiplyRational(Rational num1, Rational num2) 
    {
        result.Numerator = num1.Numerator * num2.Numerator;
        result.Denominator = num1.Denominator * num2.Denominator;
        result.ReduceFraction();
        result.MakeProperIfNeeded();
        return result;
    }
    public Rational DivideRational(Rational num1, Rational num2) 
    {
        result.Numerator = num1.Numerator * num2.Denominator;
        result.Denominator = num1.Denominator * num2.Numerator;
        result.ReduceFraction();
        result.MakeProperIfNeeded();
        return result;
    }
}

public class RationalComparison 
{
    public void IsEqual(Rational num1, Rational num2) 
    {
        if (num1.Numerator * num2.Denominator == num2.Numerator * num1.Denominator) 
        {
            Console.WriteLine($"{num1.ToString()} равна {num2.ToString()}");
        }
        else Console.WriteLine($"{num1.ToString()} не равна {num2.ToString()}");
    }
}

public class GCDandLCMCalculator
{
    public int CalculateGCD(int num1, int num2)
    {
        if (num1 < 0 || num2 < 0)
        {
            num1 = Math.Abs(num1);
            num2 = Math.Abs(num2);
        }
        if (num1 == 0)
        {
            return num2;
        }
        else
        {
            return CalculateGCD(num2 % num1, num1);
        }
    }

    public int CalculateLCM(int num1, int num2) 
    {
        int gcd = CalculateGCD(num1, num2);
        return (num1 * num2) / gcd;
    }
}