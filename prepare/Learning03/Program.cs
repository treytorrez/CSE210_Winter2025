using System;

class Program
{
    static void Main(string[] args)
    {


        Fraction frac1 = new();
        System.Console.WriteLine($"Fraction 1: {frac1.GetFractionString()} Decimal Value: {frac1.GetDecimalValue()}");
        Fraction frac2 = new(2);
        System.Console.WriteLine($"Fraction 2: {frac2.GetFractionString()} Decimal Value: {frac2.GetDecimalValue()}");
        Fraction frac3 = new(3, 5);
        // System.Console.WriteLine($"Fraction 3: {frac3.GetFractionString()} Decimal Value: {frac3.GetDecimalValue()}");

        int frac3Top = frac3.GetTop();
        int frac3Bottom = frac3.GetBottom();
        System.Console.WriteLine($"Fraction 3 top: {frac3Top} bottom: {frac3Bottom}");
        System.Console.WriteLine($"Decimal Value: {frac3.GetDecimalValue()}");        
        frac3.SetTop(1);
        frac3.SetBottom(6);
        System.Console.WriteLine($"Fraction 3 top after set: {frac3.GetTop()} bottom: {frac3.GetBottom()}");
        Console.WriteLine($"Fraction: {frac3.GetFractionString()} Decimal Value: {frac3.GetDecimalValue()}");
        string frac3String = frac3.GetFractionString();
        double frac3Decimal = frac3.GetDecimalValue();



    }
}