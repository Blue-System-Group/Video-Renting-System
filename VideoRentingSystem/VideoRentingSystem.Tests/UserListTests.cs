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

        /// <summary>
        /// Test method to  get non-existing user by username.
        /// </summary>
        [TestMethod]
        public void GetUser_ShouldReturnNullForNonExistingUser()
        {
            // Arrange
            userList.AddUser(adminUser);

            // Act
            var result = userList.GetUser("nonexistent");

            // Assert
            Assert.IsNull(result, "Should return null for non-existing user");
        }

        /// <summary>
        /// Test method to validate a user with correct credentials.
        /// </summary>
        [TestMethod]
        public void ValidateUser_ShouldReturnUserForValidCredentials()
        {
            // Arrange
            userList.AddUser(adminUser);

            // Act
            var result = userList.ValidateUser("admin", "admin123");

            // Assert
            Assert.IsNotNull(result, "Valid user should be returned");
            Assert.AreEqual(adminUser.Username, result.Username, "Usernames should match");
        }

        /// <summary>
        /// Test method to validate a user with incorrect password
        /// </summary>
        [TestMethod]
        public void ValidateUser_ShouldReturnNullForInvalidPassword()
        {
            // Arrange
            userList.AddUser(adminUser);

            // Act
            var result = userList.ValidateUser("admin", "wrongpassword");

            // Assert
            Assert.IsNull(result, "Should return null for invalid password");
        }

        /// <summary>
        /// Test method to validate a user with incorrect username
        /// </summary>
        [TestMethod]
        public void ValidateUser_ShouldReturnNullForInvalidUsername()
        {
            // Arrange
            userList.AddUser(adminUser);

            // Act
            var result = userList.ValidateUser("wronguser", "admin123");

            // Assert
            Assert.IsNull(result, "Should return null for invalid username");
        }

        /// <summary>
        /// Test method to check isAdmin for a existing admin user.
        /// </summary>
        [TestMethod]
        public void IsAdmin_ShouldReturnTrueForAdminUser()
        {
            // Arrange
            userList.AddUser(adminUser);

            // Act
            var result = userList.IsAdmin("admin");

            // Assert
            Assert.IsTrue(result, "Should return true for admin user");
        }

        /// <summary>
        /// Test method to check isAdmin for a non-existing user.
        /// </summary>
        [TestMethod]
        public void IsAdmin_ShouldReturnFalseForNonAdminUser()
        {
            // Arrange
            userList.AddUser(customerUser);

            // Act
            var result = userList.IsAdmin("customer1");

            // Assert
            Assert.IsFalse(result, "Should return false for non-admin user");
        }

        /// <summary>
        /// Test method to check isAdmin for a non-existing user.
        /// </summary>
        [TestMethod]
        public void IsAdmin_ShouldReturnFalseForNonExistingUser()
        {
            // Act
            var result = userList.IsAdmin("nonexistent");

            // Assert
            Assert.IsFalse(result, "Should return false for non-existing user");
        }

        /// <summary>
        /// Test method to check isCustomer for a existing customer user.
        /// </summary>
        [TestMethod]
        public void IsCustomer_ShouldReturnTrueForCustomerUser()
        {
            // Arrange
            userList.AddUser(customerUser);

            // Act
            var result = userList.IsCustomer("customer1");

            // Assert
            Assert.IsTrue(result, "Should return true for customer user");
        }
        /// <summary>
        /// Test method to check isCustomer for a non-existing user.
        /// </summary>
        [TestMethod]
        public void IsCustomer_ShouldReturnFalseForNonCustomerUser()
        {
            // Arrange
            userList.AddUser(adminUser);

            // Act
            var result = userList.IsCustomer("admin");

            // Assert
            Assert.IsFalse(result, "Should return false for non-customer user");
        }
        /// <summary>
        /// Test method to check remove user for a existing user.
        /// </summary>
        [TestMethod]
        public void RemoveUser_ShouldRemoveExistingUser()
        {
            // Arrange
            userList.AddUser(adminUser);
            userList.AddUser(customerUser);

            // Act
            bool result = userList.RemoveUser(adminUser.UserID);

            // Assert
            Assert.IsTrue(result, "Should return true when user is removed");
            Assert.IsNull(userList.GetUser("admin"), "User should no longer exist in the list");
        }
        /// <summary>
        /// Test method to check remove user for a non-existing user.
        /// </summary>
        [TestMethod]
        public void RemoveUser_ShouldReturnFalseForNonExistingUser()
        {
            // Arrange
            userList.AddUser(adminUser);

            // Act
            bool result = userList.RemoveUser(999); // Non-existing ID

            // Assert
            Assert.IsFalse(result, "Should return false for non-existing user");
        }
    }
}
