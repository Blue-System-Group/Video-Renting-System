using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.DataStructures
{
        public class CustomerNode
        {
            public Customer Data;
            public CustomerNode Next;

            public CustomerNode(Customer data)
            {
                Data = data;
                Next = null;
            }
        }

        /// Linked List to store customers
        public class CustomerList
        {
        private CustomerNode head;

        public CustomerList() { }

        public void AddCustomer(Customer customer)
        {
            CustomerNode newNode = new CustomerNode(customer);
            newNode.Next = head;
            head = newNode;
        }

        /// Method to display customers sorted by CustomerID
        public void DisplayCustomers()
        {
            List<Customer> customers = new List<Customer>();
            CustomerNode current = head;

            // Collect customers in a list
            while (current != null)
            {
                customers.Add(current.Data);
                current = current.Next;
            }

            // Sort by CustomerID
            customers.Sort((a, b) => a.CustomerID.CompareTo(b.CustomerID));

            // Display sorted customers
            Console.WriteLine("Customers:");
            foreach (var customer in customers)
            {
                Console.WriteLine($"Customer ID: {customer.CustomerID} | Name: {customer.Name} | Contact: {customer.Contact}");
            }
        }

        /// Method to display a specific customer by ID
        public void DisplayCustomer(int id)
        {
            CustomerNode current = head;
            Console.WriteLine("Displaying customer with ID: " + id);
            while (current != null)
            {
                if (current.Data.CustomerID == id)
                {
                    Console.WriteLine($"Customer ID: {current.Data.CustomerID} | Name: {current.Data.Name} | Contact: {current.Data.Contact}");
                    return;
                }
                current = current.Next;
            }
            Console.WriteLine("Customer not found.");
        }
    }
}
