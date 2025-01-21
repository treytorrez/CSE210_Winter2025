using System;

class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("What is your grade? ");
        int grade = int.Parse(Console.ReadLine());
        bool isPassing = grade >= 60;
        string letter = "";
        switch (grade)
        {
            case >= 90:
                letter = "A";
                break;
            case >= 80:
                letter = "B";
                break;
            case >= 70:
                letter = "C";
                break;
            case >= 60:
                letter = "D";
                break;
            default:
                letter = "F";
                break;
        }
        System.Console.WriteLine($"Your grade is {letter}");
        System.Console.WriteLine(isPassing ? "You are passing" : "You are failing");
    }
}