using System;
using VideoRentingSystem.Data;
using VideoRentingSystem.DataStructures;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.Services
{
    public class RentalService
    {
        private readonly DataAccess _dataAccess; // Constructor to initialize data access
        private readonly RentalHashTable _rentalTable; // Hash table to store rentals
        private static VideoService videoService = new VideoService(); // Constructor to initialize video service

        // Constructor to initialize the rental service
        public RentalService()
        {
            _dataAccess = new DataAccess();
            _rentalTable = RentalHashTable.Instance;
        }

        // method to load rental data from the database
        public void LoadData()
        {
            //before loading data, clear the hash table
            _rentalTable.Clear();
            // Load rental records from the database
            var rentalTable = _dataAccess.GetData("SELECT * FROM Rentals");
            foreach (System.Data.DataRow row in rentalTable.Rows)
            {
                Rental rental = new Rental
                {
                    RentalID = Convert.ToInt32(row["RentalID"]),
                    CustomerID = Convert.ToInt32(row["CustomerID"]),
                    VideoID = Convert.ToInt32(row["VideoID"]),
                    RentDate = Convert.ToDateTime(row["RentalDate"]),
                    ReturnDate = row["ReturnDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["ReturnDate"]),
                    Status = row["Status"].ToString()
                };
                _rentalTable.AddRental(rental);
            }
            //Console.WriteLine("Data loaded successfully!");
        }
    }
}
