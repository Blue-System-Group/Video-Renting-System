using System;
using VideoRentingSystem.Services;

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

        }

        /// Method to load data from the database
        static void LoadData()
        {
            Console.WriteLine("Loading application data. Please wait...");

            customerService.LoadData();
            videoService.LoadData();
            rentalService.LoadData();
            //userService.LoadData();

            Console.WriteLine("Data successfully loaded.");
        }
    }
}
