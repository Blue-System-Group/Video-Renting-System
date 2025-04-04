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

    }
}
