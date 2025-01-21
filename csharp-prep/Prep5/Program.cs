using System;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        //DisplayWelcome - Displays the message, "Welcome to the Program!"
        static void DisplayWelcome()
        {
            System.Console.WriteLine("Welcome to the Program!");
        }

        //PromptUserName - Asks for and returns the user's name (as a string)
        static string PromptUserName()
        {
            System.Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            return username;
        }

        //PromptUserNumber - Asks for and returns the user's favorite number (as an integer)
        static int PromptUserNumber()
        {
            System.Console.Write("Enter your favorite number: ");
            int number = int.Parse(Console.ReadLine());
            return number;
        }

        //SquareNumber - Accepts an integer as a parameter and returns that number squared (as an integer)
        static int SquareNumber(int num)
        {
            return (int)Math.Pow(num, 2);
        }

        //DisplayResult - Accepts the user's name and the squared number and displays them.
        static void DisplayResult(string name, int squaredNumber)
        {
            System.Console.WriteLine($"Hello, {name}! Your squared number is {squaredNumber}.");
        }

        DisplayWelcome();
        DisplayResult(PromptUserName(), SquareNumber(PromptUserNumber()));

    }
}