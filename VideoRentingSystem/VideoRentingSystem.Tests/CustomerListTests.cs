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
    }
}