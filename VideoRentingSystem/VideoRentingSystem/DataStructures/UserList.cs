using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.DataStructures
{
    public class UserNode
    {
        public User Data;
        public UserNode Next;

        public UserNode(User data)
        {
            Data = data;
            Next = null;
        }
    }
    public class UserList
    {
        private UserNode head;
        public UserList() { }

        public void AddUser(User user)
        {
            UserNode newNode = new UserNode(user);
            newNode.Next = head;
            head = newNode;
        }
    }
}
