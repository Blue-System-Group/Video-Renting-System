using VideoRentingSystem.DataStructures;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.Tests
{
    [TestClass]
    public class UserListTests
    {
        private UserList userList;
        private User adminUser;
        private User customerUser;
        private User regularUser;


        /// <summary>
        /// TestInitialize method to set up the test environment.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            userList = new UserList();
            userList.Clear(); // Clear any existing data before each test

            adminUser = new User
            {
                UserID = 1,
                Username = "admin",
                PasswordHash = "admin123",
                Role = "Admin",
                ReferenceID = "100"
            };

            customerUser = new User
            {
                UserID = 2,
                Username = "customer1",
                PasswordHash = "cust123",
                Role = "Customer",
                ReferenceID = "200"
            };

            regularUser = new User
            {
                UserID = 3,
                Username = "user1",
                PasswordHash = "user123",
                Role = "User",
                ReferenceID = "300"
            };
        }


        /// <summary>
        /// Test method to verify singleton instance of UserList.
        /// </summary>
        [TestMethod]
        public void Instance_ShouldReturnSingletonInstance()
        {
            // Arrange
            var instance1 = UserList.Instance;
            var instance2 = UserList.Instance;

            // Assert
            Assert.AreSame(instance1, instance2, "Instance should return the same object");
        }

        /// <summary>
        /// Test method to add a user to the list.
        /// </summary>
        [TestMethod]
        public void AddUser_ShouldAddUserToTheList()
        {
            // Act
            userList.AddUser(adminUser);

            // Assert
            Assert.IsNotNull(userList.GetUser("admin"), "User should be added to the list");
        }

        /// <summary>
        /// Test method to get a user by username.
        /// </summary>
        [TestMethod]
        public void GetUser_ShouldReturnCorrectUser()
        {
            // Arrange
            userList.AddUser(adminUser);
            userList.AddUser(customerUser);

            // Act
            var result = userList.GetUser("customer1");

            // Assert
            Assert.IsNotNull(result, "User should be found");
            Assert.AreEqual(customerUser.UserID, result.UserID, "User IDs should match");
            Assert.AreEqual(customerUser.Username, result.Username, "Usernames should match");
        }

    }
}
