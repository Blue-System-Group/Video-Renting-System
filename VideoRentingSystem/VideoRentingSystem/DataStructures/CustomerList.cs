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
        // Singleton instance
        private static readonly CustomerList instance = new CustomerList();
        private CustomerNode head;

        // Private constructor to prevent instantiation
        public CustomerList() { }

        // Public property to access the singleton instance
        public static CustomerList Instance
        {
            get { return instance; }
        }

        // method to clear the list
        public void Clear()
        {
            head = null;
        }

        //method to add a customer
        public void AddCustomer(Customer customer)
        {
            CustomerNode newNode = new CustomerNode(customer);
            newNode.Next = head;
            head = newNode;
        }


        // method to display all customers
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

        // method to display a specific customer
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


        // method to search for a customer
        public void SearchCustomer(int id)
        {
            CustomerNode current = head;
            Console.WriteLine("Searching for customer with ID: " + id);
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

        // method to remove a customer
        public bool RemoveCustomer(int id)
        {
            if (head == null)
            {
                Console.WriteLine("Customer list is empty.");
                return false;
            }

            Console.WriteLine("Removing customer with ID: " + id);

            CustomerNode current = head, prev = null;

            while (current != null)
            {
                if (current.Data.CustomerID == id)
                {
                    if (prev == null)
                        head = current.Next;
                    else
                        prev.Next = current.Next;
                    return true;
                }
                prev = current;
                current = current.Next;
            }

            Console.WriteLine("Customer with ID " + id + " not found.");
            return false;
        }

        // method to update a customer
        public bool UpdateCustomer(int id, string customerName, string customerContact)
        {
            CustomerNode current = head;
            Console.WriteLine("Updating customer with ID: " + id);
            while (current != null)
            {
                if (current.Data.CustomerID == id)
                {
                    current.Data.Name = customerName;
                    current.Data.Contact = customerContact;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        //method to count the number of customers
        public int Count
        {
            get
            {
                int count = 0;
                CustomerNode current = head;
                while (current != null)
                {
                    count++;
                    current = current.Next;
                }
                return count;
            }
        }
    }
}
