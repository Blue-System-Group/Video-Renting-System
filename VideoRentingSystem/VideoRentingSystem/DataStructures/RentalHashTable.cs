using System;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.DataStructures
{
    public class RentalHashTable
    {
        private class RentalEntry
        {
            public int RentalID;
            public Rental Data;
            public RentalEntry Next;

            public RentalEntry(int rentalID, Rental data)
            {
                RentalID = rentalID;
                Data = data;
                Next = null;
            }
        }

        private RentalEntry[] table;
        private int size;

        public RentalHashTable(int size = 100)
        {
            this.size = size;
            table = new RentalEntry[size];
        }

        private int Hash(int rentalID)
        {
            return rentalID % size;
        }
        // add rental to the hash table
        public void AddRental(Rental rental)
        {
            int newRentalID = GetNextRentalID();
            rental.RentalID = newRentalID;

            if (string.IsNullOrEmpty(rental.Status))
            {
                rental.Status = "Rented";
            }

            int index = newRentalID;
            RentalEntry newEntry = new RentalEntry(newRentalID, rental);
            newEntry.Next = table[index];
            table[index] = newEntry;
        }

        //get next rental ID
        private int GetNextRentalID()
        {
            int maxID = 0;

            foreach (var entry in table)
            {
                RentalEntry current = entry;
                while (current != null)
                {
                    if (current.RentalID > maxID)
                    {
                        maxID = current.RentalID;
                    }
                    current = current.Next;
                }
            }

            return maxID + 1;
        }
        // return rental by rental ID
        public void ReturnVideo(int rentalID)
        {
            int index = Hash(rentalID);
            RentalEntry entry = table[index];
            while (entry != null)
            {
                if (entry.RentalID == rentalID)
                {
                    entry.Data.Status = "Returned";
                    entry.Data.ReturnDate = DateTime.Now;
                    break;
                }
                entry = entry.Next;
            }
        }

        // get rental by rental ID
        public Rental GetRental(int rentalID)
        {
            int index = Hash(rentalID);
            RentalEntry entry = table[index];

            while (entry != null)
            {
                if (entry.RentalID == rentalID)
                    return entry.Data;
                entry = entry.Next;
            }
            return null;
        }

        // get all rentals
        public Rental Display()
        {
            Console.WriteLine("Rentals:");
            for (int i = 0; i < size; i++)
            {
                RentalEntry entry = table[i];
                while (entry != null)
                {
                    Console.WriteLine($"Rental ID: {entry.RentalID}, Customer ID: {entry.Data.CustomerID}, Video ID: {entry.Data.VideoID}, Rent Date: {entry.Data.RentDate}, Return Date: {entry.Data.ReturnDate}, Status: {entry.Data.Status}");
                    entry = entry.Next;
                }
            }
            return null;
        }
    }
}
