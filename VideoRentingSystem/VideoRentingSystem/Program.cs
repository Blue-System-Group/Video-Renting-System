using System;
using VideoRentingSystem.Data;
using VideoRentingSystem.DataStructures;
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

            // Main menu loop
            while (true)
            {
                if (loggedInUser.Role == "Admin")
                {
                    Console.WriteLine("1. Manage Customers");
                    Console.WriteLine("2. Manage Videos");
                    Console.WriteLine("3. Manage Rentals");
                    Console.WriteLine("4. Manage Users");
                }
                else
                {
                    Console.WriteLine("\n1. Rent Video");
                    Console.WriteLine("2. Return Video");
                    Console.WriteLine("3. Display Pending Returns");
                }

                Console.WriteLine("0. Exit");
                Console.Write("Please enter your choice: ");

                int choice = InputHelper.GetIntInput("");

                if (loggedInUser.Role == "Admin")
                {
                    switch (choice)
                    {
                        case 1: ManageCustomers(); break;
                        case 2: ManageVideos(); break;
                        //case 3: ManageRentals(); break;
                        //case 4: ManageUsers(); break;
                        case 0: Environment.Exit(0); break;
                        default: Console.WriteLine("Invalid selection. Please try again."); break;
                    }
                }
                else if (loggedInUser.Role == "Customer")
                {
                    switch (choice)
                    {
                        //case 1: RentVideoCustomer(int.Parse(loggedInUser.ReferenceID)); break;
                        //case 2: ReturnVideoCustomer(int.Parse(loggedInUser.ReferenceID)); break;
                        //case 3: rentalService.DisplayYetToReturn(); break;
                        case 0: Environment.Exit(0); break;
                        default: Console.WriteLine("Invalid selection. Please try again."); break;
                    }
                }
            }
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

        /// Method to manage videos
        static void ManageVideos()
        {
            Console.WriteLine("1. Add Video");
            Console.WriteLine("2. Remove Video");
            Console.WriteLine("3. Display Videos");
            Console.WriteLine("4. Search Video");
            Console.WriteLine("5. Update Video");
            Console.WriteLine("0. Back");
            Console.Write("Enter your choice: ");
            int choice = InputHelper.GetIntInput("");
            switch (choice)
            {
                case 1: AddVideo(); break;
                case 2: RemoveVideo(); break;
                case 3: DisplayVideos(); break;
                //case 4: SearchVideo(); break;
                //case 5: UpdateVideo(); break;
                case 0: return;
                default: Console.WriteLine("Invalid choice. Please try again."); break;
            }
        }

        /// Method to add a new video
        static void AddVideo()
        {
            string title = InputHelper.GetStringInput("Enter video title: ");
            string genre = InputHelper.GetStringInput("Enter video genre: ");
            string releaseDate = InputHelper.GetStringInput("Enter video release date (yyyy-MM-dd): ");
            Video video = new Video { Title = title, Genre = genre, ReleaseDate = DateTime.Parse(releaseDate), Availability = true };
            videoService.AddVideo(video);
            videoService.LoadData();
        }

        /// Method to remove a video
        static void RemoveVideo()
        {
            DisplayVideos();
            int videoId = InputHelper.GetIntInput("Enter video ID: ");
            videoService.RemoveVideo(videoId);
        }

        /// Method to display all videos
        static void DisplayVideos()
        {
            videoService.DisplayVideos();
        }

        // method to manage customers
        static void ManageCustomers()
        {
            Console.WriteLine("1. Add Customer");
            Console.WriteLine("2. Remove Customer");
            Console.WriteLine("3. Display Customers");
            Console.WriteLine("4. Search Customer");
            Console.WriteLine("5. Update Customer");
            Console.WriteLine("0. Back");
            Console.Write("Enter your choice: ");
            int choice = InputHelper.GetIntInput("");
            switch (choice)
            {
                case 1: AddCustomer(); break;
                case 2: RemoveCustomer(); break;
                case 3: DisplayCustomers(); break;
                case 4: SearchCustomer(); break;
                case 5: UpdateCustomer(); break;
                case 0: return;
                default: Console.WriteLine("Invalid choice. Please try again."); break;
            }
        }

        // method to add customer
        static void AddCustomer()
        {
            string name = InputHelper.GetStringInput("Enter customer name: ");
            string contact = InputHelper.GetStringInput("Enter customer contact: ");
            Customer customer = new Customer { Name = name, Contact = contact };
            customerService.AddCustomer(customer);
            customerService.LoadData();
        }

        // method to remove customer
        static void RemoveCustomer()
        {
            DisplayCustomers();
            int customerId = InputHelper.GetIntInput("Enter customer ID: ");
            customerService.RemoveCustomer(customerId);
        }

        // method to display all customers
        static void DisplayCustomers()
        {
            customerService.DisplayCustomers();
        }

        // method to search customer
        static void SearchCustomer()
        {
            int customerId = InputHelper.GetIntInput("Enter customer ID: ");
            customerService.SearchCustomer(customerId);
        }

        // method to update customer
        static void UpdateCustomer()
        {
            int customerId = InputHelper.GetIntInput("Enter customer ID: ");
            string name = InputHelper.GetStringInput("Enter new name: ");
            string contact = InputHelper.GetStringInput("Enter new contact: ");
            customerService.UpdateCustomer(customerId, name, contact);
        }
    }
}
