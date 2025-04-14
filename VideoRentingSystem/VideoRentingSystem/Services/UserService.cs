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
    }
}
