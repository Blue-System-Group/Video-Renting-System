using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.DataStructures
{
    public class UserNode
    {
        public User Data;
        public UserNode Next;

        public UserNode(User data)
        {
            Data = data;
            Next = null;
        }
    }
    public class UserList
    {
        private static readonly UserList instance = new UserList();
        private UserNode head;
        public UserList() { }

        // Singleton pattern to ensure only one instance of UserList exists
        public static UserList Instance
        {
            get { return instance; }
        }

        // method to clear the list
        public void Clear()
        {
            head = null;
        }

        // method to add a user to the list
        public void AddUser(User user)
        {
            UserNode newNode = new UserNode(user);
            newNode.Next = head;
            head = newNode;
        }

        //method to display all users in the list
        public void DisplayUsers()
        {
            List<User> users = new List<User>();
            UserNode current = head;
            // Collect users in a list
            while (current != null)
            {
                users.Add(current.Data);
                current = current.Next;
            }
            // Sort by UserID
            users.Sort((a, b) => a.UserID.CompareTo(b.UserID));
            // Display sorted users
            Console.WriteLine("Users:");
            foreach (var user in users)
            {
                Console.WriteLine($"User ID: {user.UserID} | Username: {user.Username} | Role: {user.Role}");
            }
        }

        // method to get a user by username
        public User GetUser(string username)
        {
            UserNode current = head;
            while (current != null)
            {
                if (current.Data.Username == username)
                {
                    return current.Data;
                }
                current = current.Next;
            }
            return null;
        }

        // method to validate a user by username and password
        public User ValidateUser(string username, string password)
        {
            UserNode current = head;
            while (current != null)
            {
                if (current.Data.Username == username && current.Data.PasswordHash == password)
                {
                    return current.Data;
                }
                current = current.Next;
            }
            return null;
        }

        // method to check if a user is an admin
        public bool IsAdmin(string username)
        {
            UserNode current = head;
            while (current != null)
            {
                if (current.Data.Username == username && current.Data.Role == "Admin")
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        // method to check if a user is a customer
        public bool IsCustomer(string username)
        {
            UserNode current = head;
            while (current != null)
            {
                if (current.Data.Username == username && current.Data.Role == "Customer")
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        // method to remove a user by user ID
        public bool RemoveUser(int userId)
        {
            UserNode current = head;
            UserNode previous = null;
            while (current != null)
            {
                if (current.Data.UserID == userId)
                {
                    if (previous == null)
                    {
                        head = current.Next;
                    }
                    else
                    {
                        previous.Next = current.Next;
                    }
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }

        // method to update a user by user ID
        public bool UpdateUser(User user)
        {
            UserNode current = head;
            while (current != null)
            {
                if (current.Data.UserID == user.UserID)
                {
                    current.Data.Username = user.Username;
                    current.Data.PasswordHash = user.PasswordHash;
                    current.Data.Role = user.Role;
                    current.Data.ReferenceID = user.ReferenceID;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
    }
}
