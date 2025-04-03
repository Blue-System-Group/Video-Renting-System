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
    }
}
