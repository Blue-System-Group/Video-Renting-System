using System;
using VideoRentingSystem.Models;
using VideoRentingSystem.Services;
using VideoRentingSystem.Utils;

namespace VideoRentingSystem
{
    class Program
    {
        private static CustomerService customerService = new CustomerService(); // CustomerService instance
        private static VideoService videoService = new VideoService(); // VideoService instance
        private static RentalService rentalService = new RentalService(); // RentalService instance
        private static UserService userService = new UserService(); // UserService instance

        static void Main(string[] args)
        {
            // Load data from the database
            LoadData();

            // Display welcome message
            Console.WriteLine("=====================================");
            Console.WriteLine("          Welcome to the System");
            Console.WriteLine("=====================================");

            // Prompt user for login
            User loggedInUser = Login();

            // Check if login was successful
            if (loggedInUser == null)
            {
                Console.WriteLine("=====================================");
                Console.WriteLine("    Authentication Failed. Exiting...");
                Console.WriteLine("=====================================");
                return;
            }

            // Display user information
            Console.WriteLine("=====================================");
            Console.WriteLine($"   Welcome, {loggedInUser.Username}!");
            Console.WriteLine($"   Role: {loggedInUser.Role}");
            Console.WriteLine("=====================================");

        }

        /// Method to load data from the database
        static void LoadData()
        {
            Console.WriteLine("Loading application data. Please wait...");

            customerService.LoadData();
            videoService.LoadData();
            rentalService.LoadData();
            userService.LoadData();

            Console.WriteLine("Data successfully loaded.");
        }

        // Method to handle user login
        static User Login()
        {
            string username = InputHelper.GetStringInput("Enter your username: ");
            string password = InputHelper.GetStringInput("Enter your password: ");

            User user = userService.AuthenticateUser(username, password);

            if (user != null)
            {
                Console.WriteLine("=====================================");
                Console.WriteLine("        Login Successful!");
                Console.WriteLine("=====================================");
                return user;
            }

            Console.WriteLine("=====================================");
            Console.WriteLine("    Invalid username or password.");
            Console.WriteLine("=====================================");
            return null;
        }
    }
}
