using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentingSystem.Utils
{
    public static class InputHelper
    {
        // method to get integer input from the user
        public static int GetIntInput(string message)
        {
            int result;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (int.TryParse(input, out result))
                {
                    return result;
                }

                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        // mrthod to get string input from the user
        public static string GetStringInput(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }

                Console.WriteLine("Input cannot be empty. Please try again.");
            }
        }
    }
}
