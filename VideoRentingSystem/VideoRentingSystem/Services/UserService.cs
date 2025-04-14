using System.Data;
using System;
using VideoRentingSystem.Data;
using VideoRentingSystem.DataStructures;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.Services
{
    public class UserService
    {
        private readonly DataAccess _dataAccess; // Data access instance
        private readonly UserList _userList; // User list instance

        /// Constructor
        public UserService()
        {
            _dataAccess = new DataAccess();
            _userList = UserList.Instance;
        }

        // method to load user data from the database
        public void LoadData()
        {
            //before loading data, clear the linked list
            _userList.Clear();
            DataTable UserTable = _dataAccess.GetData("SELECT * FROM Users");
            foreach (DataRow row in UserTable.Rows)
            {
                User user = new User
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    Username = row["Username"].ToString(),
                    PasswordHash = row["PasswordHash"].ToString(),
                    Role = row["Role"].ToString(),
                    ReferenceID = row["ReferenceID"].ToString()
                };
                _userList.AddUser(user);  // Add to linked list
            }
            //Console.WriteLine("Data loaded successfully!");
        }

        // method to add a user to the linked list and database
        public void AddUser(User user)
        {
            _userList.AddUser(user);  // Add to linked list

            // Insert user record into the database
            string query = "INSERT INTO Users (Username, PasswordHash, Role, ReferenceID) VALUES (@Username, @PasswordHash, @Role, @ReferenceID)";
            _dataAccess.ExecuteQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@ReferenceID", user.ReferenceID);
            });

            Console.WriteLine("User added successfully!");
        }

        // method to remove user from the linked list and database
        public void RemoveUser(int userId)
        {
            // Remove user from the linked list
            bool removed = _userList.RemoveUser(userId);
            if (!removed)
            {
                Console.WriteLine("User not found.");
            }
            else
            {
                // Delete user record from the database
                string query = "DELETE FROM Users WHERE UserID = @UserID";
                _dataAccess.ExecuteQuery(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                });
                Console.WriteLine("User removed successfully!");
            }
        }

        // method to update user in the linked list and database
        public void UpdateUser(int userId, string username, string passwordHash, string role, string referenceID)
        {
            // Update user in the linked list
            bool updated = _userList.UpdateUser(new User
            {
                UserID = userId,
                Username = username,
                PasswordHash = passwordHash,
                Role = role,
                ReferenceID = referenceID
            });
            if (!updated)
            {
                Console.WriteLine("User not found.");
            }
            else
            {
                // Update user record in the database
                string query = "UPDATE Users SET Username = @Username, PasswordHash = @PasswordHash, Role = @Role, ReferenceID = @ReferenceID WHERE UserID = @UserID";
                _dataAccess.ExecuteQuery(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.Parameters.AddWithValue("@ReferenceID", referenceID);
                });
                Console.WriteLine("User updated successfully!");
            }
        }
        // method to display all users in the linked list
        public void DisplayUsers()
        {
            _userList.DisplayUsers();
        }
    }
}
