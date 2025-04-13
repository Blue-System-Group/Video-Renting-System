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

        // method to add a new customer to the linked list and database
        public void AddCustomer(Customer customer)
        {
            // Add customer to the linked list
            _customerList.AddCustomer(customer);

            // Insert customer record into the database
            string query = "INSERT INTO Customers (Name, Contact) VALUES (@Name, @Contact)";
            _dataAccess.ExecuteQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Contact", customer.Contact);
            });

            Console.WriteLine("Customer added successfully!");
        }


        // method to remove a customer from the linked list and database
        public void RemoveCustomer(int customerId)
        {
            // Remove customer from the linked list
            bool removed = _customerList.RemoveCustomer(customerId);

            if (!removed)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            // Remove customer record from the database
            string query = "DELETE FROM Customers WHERE CustomerID = @ID";
            _dataAccess.ExecuteQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@ID", customerId);
            });

            Console.WriteLine("Customer removed successfully!");
        }

        // method to display all customers in the linked list
        public void DisplayCustomers()
        {
            _customerList.DisplayCustomers();
        }
    }
}
