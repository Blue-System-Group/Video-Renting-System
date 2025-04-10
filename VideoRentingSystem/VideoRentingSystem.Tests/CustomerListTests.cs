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

    }
}