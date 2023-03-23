// See https://aka.ms/new-console-template for more information
using BankApp;
using System;
using System.IO;
using System.Text;

namespace ConsoleBankingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello There!");

            //while (true)
            //{
            //    Console.WriteLine("Enter 1 to Sign Up or 2 to Login: ");
            //    string choice = Console.ReadLine();

            //    if (choice == "1")
            //    {
            //        SignUp();
            //    }
            //    else if (choice == "2")
            //    {
            //        Login();
            //    }
            //    else
            //    {
            //        Console.WriteLine("Invalid choice. Please try again.");
            //    }
            //}
            var bank = new BankingApplication();
            bank.Start();
        }

      
    }
}

            