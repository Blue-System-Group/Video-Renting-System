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
        private UserNode head;
        public UserList() { }

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
    }
}
