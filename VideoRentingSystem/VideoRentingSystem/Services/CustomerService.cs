using System.Data;
using System;
using VideoRentingSystem.Data;
using VideoRentingSystem.DataStructures;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.Services
{
    public class CustomerService
    {
        private readonly DataAccess _dataAccess; // Data access layer for database operations
        private readonly CustomerList _customerList; // Linked List for customer management

        // constructor
        public CustomerService()
        {
            _dataAccess = new DataAccess();
            _customerList = CustomerList.Instance;
        }

        // method to load customer data from the database into the linked list.
        public void LoadData()
        {
            //before loading data, clear the linked list
            _customerList.Clear();
            DataTable CustomerTable = _dataAccess.GetData("SELECT * FROM Customers");
            foreach (DataRow row in CustomerTable.Rows)
            {
                Customer customer = new Customer
                {
                    CustomerID = Convert.ToInt32(row["CustomerID"]),
                    Name = row["Name"].ToString(),
                    Contact = row["Contact"].ToString()
                };
                _customerList.AddCustomer(customer);  // Add to linked list
            }
            //Console.WriteLine("Data loaded successfully!");
        }
    }
}
