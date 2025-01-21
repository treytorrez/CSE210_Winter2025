using System;

class Program
{
    static void Main(string[] args)
    {
        int secretNum;
        System.Console.WriteLine("What is the magic number?");
        secretNum = int.Parse(Console.ReadLine());
        int guess = 0;
        System.Console.WriteLine("Guess the magic number!");
        while (guess != secretNum)
        {
            Console.Write("Enter your guess: ");
            guess = int.Parse(Console.ReadLine());
            if (guess < secretNum)
            {
                System.Console.WriteLine("Higher!");
                System.Console.WriteLine();
            }
            else if (guess > secretNum)
            {
                System.Console.WriteLine("Lower!");
                System.Console.WriteLine();
            }
        }
        System.Console.WriteLine("You guessed the magic number!");
    }
}