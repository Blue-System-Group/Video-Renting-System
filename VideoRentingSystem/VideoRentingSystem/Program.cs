using System;
using VideoRentingSystem.Models;
using VideoRentingSystem.Services;
using VideoRentingSystem.Utils;

namespace VideosRentingSystem
{
    class Program
    {
        private static CustomerService customerService = new CustomerService();
        private static VideoService videoService = new VideoService();
        private static RentalService rentalService = new RentalService();
        private static UserService userService = new UserService();

        static void Main(string[] args)
        {
            // Load data from the database
            LoadData();

            // Login Process
            Console.WriteLine("=====================================");
            Console.WriteLine("          Welcome to the System");
            Console.WriteLine("=====================================");


            User loggedInUser = Login();

            if (loggedInUser == null)
            {
                Console.WriteLine("=====================================");
                Console.WriteLine("    Authentication Failed. Exiting...");
                Console.WriteLine("=====================================");
                return;
            }


            Console.WriteLine("=====================================");
            Console.WriteLine($"   Welcome, {loggedInUser.Username}!");
            Console.WriteLine($"   Role: {loggedInUser.Role}");
            Console.WriteLine("=====================================");


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
                        case 3: ManageRentals(); break;
                        case 4: ManageUsers(); break;
                        case 0: Environment.Exit(0); break;
                        default: Console.WriteLine("Invalid selection. Please try again."); break;
                    }
                }
                else if (loggedInUser.Role == "Customer")
                {
                    switch (choice)
                    {
                        case 1: RentVideoCustomer(int.Parse(loggedInUser.ReferenceID)); break;
                        case 2: ReturnVideoCustomer(int.Parse(loggedInUser.ReferenceID)); break;
                        case 3: rentalService.DisplayYetToReturn(); break;
                        case 0: Environment.Exit(0); break;
                        default: Console.WriteLine("Invalid selection. Please try again."); break;
                    }
                }
            }

        }

        static void LoadData()
        {
            Console.WriteLine("Loading application data. Please wait...");

            customerService.LoadData();
            videoService.LoadData();
            rentalService.LoadData();
            userService.LoadData();

            Console.WriteLine("Data successfully loaded.");
        }


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
                case 4: SearchVideo(); break;
                case 5: UpdateVideo(); break;
                case 0: return;
                default: Console.WriteLine("Invalid choice. Please try again."); break;
            }
        }

        static void ManageRentals()
        {
            Console.WriteLine("1. Rent Video");
            Console.WriteLine("2. Return Video");
            Console.WriteLine("3. Display Yet To Return Videos");
            Console.WriteLine("0. Back");
            Console.Write("Enter your choice: ");
            int choice = InputHelper.GetIntInput("");
            switch (choice)
            {
                case 1: RentVideo(); break;
                case 2: ReturnVideo(); break;
                case 3: rentalService.DisplayYetToReturn(); break;
                case 0: return;
                default: Console.WriteLine("Invalid choice. Please try again."); break;
            }
        }

        static void ManageUsers()
        {
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. Remove User");
            Console.WriteLine("3. Display Users");
            Console.WriteLine("4. Search User");
            Console.WriteLine("5. Update User");
            Console.WriteLine("0. Back");
            Console.Write("Enter your choice: ");
            int choice = InputHelper.GetIntInput("");
            switch (choice)
            {
                case 1: AddUser(); break;
                case 2: RemoveUser(); break;
                case 3: userService.DisplayUsers(); break;
                case 4: SearchUser(); break;
                case 5: UpdateUser(); break;
                case 0: return;
                default: Console.WriteLine("Invalid choice. Please try again."); break;
            }
        }

        static void AddCustomer()
        {
            string name = InputHelper.GetStringInput("Enter customer name: ");
            string contact = InputHelper.GetStringInput("Enter customer contact: ");
            Customer customer = new Customer { Name = name, Contact = contact };
            customerService.AddCustomer(customer);
            customerService.LoadData();
        }

        static void RemoveCustomer()
        {
            DisplayCustomers();
            int customerId = InputHelper.GetIntInput("Enter customer ID: ");
            customerService.RemoveCustomer(customerId);
        }

        static void DisplayCustomers()
        {
            customerService.DisplayCustomers();
        }

        static void SearchCustomer()
        {
            int customerId = InputHelper.GetIntInput("Enter customer ID: ");
            customerService.SearchCustomer(customerId);
        }

        static void UpdateCustomer()
        {
            int customerId = InputHelper.GetIntInput("Enter customer ID: ");
            string name = InputHelper.GetStringInput("Enter new name: ");
            string contact = InputHelper.GetStringInput("Enter new contact: ");
            customerService.UpdateCustomer(customerId, name, contact);
        }

        static void AddVideo()
        {
            string title = InputHelper.GetStringInput("Enter video title: ");
            string genre = InputHelper.GetStringInput("Enter video genre: ");
            string releaseDate = InputHelper.GetStringInput("Enter video release date (yyyy-MM-dd): ");
            Video video = new Video { Title = title, Genre = genre, ReleaseDate = DateTime.Parse(releaseDate), Availability = true };
            videoService.AddVideo(video);
            videoService.LoadData();
        }

        static void RemoveVideo()
        {
            DisplayVideos();
            int videoId = InputHelper.GetIntInput("Enter video ID: ");
            videoService.RemoveVideo(videoId);
        }

        static void DisplayVideos()
        {
            videoService.DisplayVideos();
        }

        static void SearchVideo()
        {
            int videoId = InputHelper.GetIntInput("Enter video ID: ");
            videoService.SearchVideo(videoId);
        }

        static void UpdateVideo()
        {
            int videoId = InputHelper.GetIntInput("Enter video ID: ");
            string title = InputHelper.GetStringInput("Enter new title: ");
            string genre = InputHelper.GetStringInput("Enter new genre: ");
            string releaseDate = InputHelper.GetStringInput("Enter new release date (yyyy-MM-dd): ");
            videoService.UpdateVideo(videoId, title, genre, DateTime.Parse(releaseDate));
        }

        static void RentVideo()
        {
            DisplayCustomers();
            int customerId = InputHelper.GetIntInput("Enter customer ID: ");
            DisplayVideos();
            int videoId = InputHelper.GetIntInput("Enter video ID: ");
            rentalService.RentVideo(customerId, videoId);
            rentalService.LoadData();
        }

        static void RentVideoCustomer(int customerId)
        {
            DisplayVideos();
            int videoId = InputHelper.GetIntInput("Enter video ID: ");
            rentalService.RentVideo(customerId, videoId);
            rentalService.LoadData();
        }

        static void ReturnVideoCustomer(int customerId)
        {
            if (rentalService.GetYetToReturnCount(customerId) == 0)
            {
                Console.WriteLine("No rentals available to return.");
                return;
            }
            rentalService.DisplayYetToReturn(customerId);
            int rentalId = InputHelper.GetIntInput("Enter rental ID: ");
            rentalService.ReturnVideo(rentalId);
        }

        static void ReturnVideo()
        {
            if (rentalService.GetYetToReturnCount() == 0)
            {
                Console.WriteLine("No rentals available to return.");
                return;
            }
            rentalService.DisplayYetToReturn();
            int rentalId = InputHelper.GetIntInput("Enter rental ID: ");
            rentalService.ReturnVideo(rentalId);
        }

        static void AddUser()
        {
            string username = InputHelper.GetStringInput("Enter username: ");
            string password = InputHelper.GetStringInput("Enter password: ");
            string role;
            do
            {
                role = InputHelper.GetStringInput("Enter role (Admin/Customer): ");
                if (role != "Admin" && role != "Customer")
                {
                    Console.WriteLine("Invalid role. Please enter 'Admin' or 'Customer'.");
                }
            } while (role != "Admin" && role != "Customer");
            DisplayCustomers();
            string referenceID = InputHelper.GetStringInput("Enter reference ID: ");
            User user = new User { Username = username, PasswordHash = password, Role = role, ReferenceID = referenceID };
            userService.AddUser(user);
            userService.LoadData();
        }

        static void RemoveUser()
        {
            userService.DisplayUsers();
            int userId = InputHelper.GetIntInput("Enter user ID: ");
            userService.RemoveUser(userId);
        }

        static void SearchUser()
        {
            string username = InputHelper.GetStringInput("Enter username: ");
            userService.SearchUser(username);
        }

        static void UpdateUser()
        {
            userService.DisplayUsers();
            int userId = InputHelper.GetIntInput("Enter user ID: ");
            string username = InputHelper.GetStringInput("Enter new username: ");
            string password = InputHelper.GetStringInput("Enter new password: ");
            string role;
            do
            {
                role = InputHelper.GetStringInput("Enter role (Admin/Customer): ");
                if (role != "Admin" && role != "Customer")
                {
                    Console.WriteLine("Invalid role. Please enter 'Admin' or 'Customer'.");
                }
            } while (role != "Admin" && role != "Customer");
            DisplayCustomers();
            string referenceID = InputHelper.GetStringInput("Enter new reference ID: ");
            userService.UpdateUser(userId, username, password, role, referenceID);
        }
    }
}