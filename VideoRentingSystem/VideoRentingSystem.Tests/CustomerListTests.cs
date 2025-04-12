using VideoRentingSystem.DataStructures;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.Tests
{
    [TestClass]
    public class CustomerListTests
    {
        private CustomerList customerList;
        private Customer testCustomer1;
        private Customer testCustomer2;
        private Customer testCustomer3;

        [TestInitialize]
        public void TestInitialize()
        {
            customerList = new CustomerList();
            customerList.Clear(); // Clear any existing data before each test

            testCustomer1 = new Customer { CustomerID = 1, Name = "John Doe", Contact = "1234567890" };
            testCustomer2 = new Customer { CustomerID = 2, Name = "Jane Smith", Contact = "0987654321" };
            testCustomer3 = new Customer { CustomerID = 3, Name = "Bob Johnson", Contact = "5555555555" };
        }

        /// <summary>
        /// Tests that the `Instance` property of the `CustomerList` class returns the same singleton instance 
        /// on multiple accesses.
        /// </summary>
        [TestMethod]
        public void Instance_ShouldReturnSingletonInstance()
        {
            // Arrange
            var instance1 = CustomerList.Instance;
            var instance2 = CustomerList.Instance;

            // Assert
            Assert.AreSame(instance1, instance2, "Instance should return the same object");
        }
        /// <summary>
        /// Tests that the 'AddCustomer' method correctly adds a customer to the list.
        /// </summary>
        [TestMethod]
        public void AddCustomer_ShouldAddCustomerToTheList()
        {
            // Act
            customerList.AddCustomer(testCustomer1);

            // Assert
            Assert.AreEqual(1, customerList.Count, "Count should be 1 after adding a customer");
        }

        /// <summary>
        /// Tests that the 'AddCustomer' method correctly adds multiple customers to the list.
        /// </summary>
        [TestMethod]
        public void AddCustomer_ShouldAddMultipleCustomers()
        {
            // Act
            customerList.AddCustomer(testCustomer1);
            customerList.AddCustomer(testCustomer2);
            customerList.AddCustomer(testCustomer3);

            // Assert
            Assert.AreEqual(3, customerList.Count, "Count should be 3 after adding three customers");
        }

        /// <summary>
        /// Tests that the 'RemoveCustomer' method returns true when trying to remove a existing customer.
        /// </summary>
        [TestMethod]
        public void RemoveCustomer_ShouldRemoveExistingCustomer()
        {
            // Arrange
            customerList.AddCustomer(testCustomer1);
            customerList.AddCustomer(testCustomer2);

            // Act
            bool result = customerList.RemoveCustomer(testCustomer1.CustomerID);

            // Assert
            Assert.IsTrue(result, "Remove should return true for existing customer");
            Assert.AreEqual(1, customerList.Count, "Count should be 1 after removing one customer");
        }

        /// <summary>
        /// Tests that the 'RemoveCustomer' method returns false when trying to remove a non-existing customer.
        /// </summary>
        [TestMethod]
        public void RemoveCustomer_ShouldReturnFalseForNonExistingCustomer()
        {
            // Arrange
            customerList.AddCustomer(testCustomer1);

            // Act
            bool result = customerList.RemoveCustomer(999); // Non-existing ID

            // Assert
            Assert.IsFalse(result, "Remove should return false for non-existing customer");
            Assert.AreEqual(1, customerList.Count, "Count should remain 1");
        }

        /// <summary>
        /// Tests that the 'RemoveCustomer' method correctly handles the case when the list is empty.
        /// </summary>
        [TestMethod]
        public void RemoveCustomer_ShouldHandleEmptyList()
        {
            // Act
            bool result = customerList.RemoveCustomer(1);

            // Assert
            Assert.IsFalse(result, "Remove should return false for empty list");
            Assert.AreEqual(0, customerList.Count, "Count should remain 0");
        }

        /// <summary>
        /// Tests that the 'UpdateCustomer' method should update an existing customer.
        /// </summary>
        [TestMethod]
        public void UpdateCustomer_ShouldUpdateExistingCustomer()
        {
            // Arrange
            customerList.AddCustomer(testCustomer1);
            string newName = "Updated Name";
            string newContact = "9999999999";

            // Act
            bool result = customerList.UpdateCustomer(testCustomer1.CustomerID, newName, newContact);

            // Assert
            Assert.IsTrue(result, "Update should return true for existing customer");
        }

        /// <summary>
        /// Tests that the 'UpdateCustomer' method should return false when trying to update a non-existing customer.
        /// </summary>
        [TestMethod]
        public void UpdateCustomer_ShouldReturnFalseForNonExistingCustomer()
        {
            // Arrange
            customerList.AddCustomer(testCustomer1);

            // Act
            bool result = customerList.UpdateCustomer(999, "New Name", "New Contact");

            // Assert
            Assert.IsFalse(result, "Update should return false for non-existing customer");
        }

        /// <summary>
        /// Tests that the 'Count'should return 0 when the list is empty.
        /// </summary>
        [TestMethod]
        public void Count_ShouldReturnZeroForEmptyList()
        {
            // Assert
            Assert.AreEqual(0, customerList.Count, "Count should be 0 for empty list");
        }

        /// <summary>
        /// Tests that the 'Count' should return the correct number of customers in the list.
        /// </summary>
        [TestMethod]
        public void Count_ShouldReturnCorrectCountForNonEmptyList()
        {
            // Arrange
            customerList.AddCustomer(testCustomer1);
            customerList.AddCustomer(testCustomer2);

            // Assert
            Assert.AreEqual(2, customerList.Count, "Count should be 2 after adding two customers");
        }
    }
}