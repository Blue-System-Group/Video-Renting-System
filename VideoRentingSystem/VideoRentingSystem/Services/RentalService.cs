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


        // method to rent a video by customer ID and video ID
        public void RentVideo(int customerId, int videoId)
        {
            // Check if the video is available
            string checkQuery = $"SELECT Availability FROM Videos WHERE VideoID = {videoId}";
            var result = _dataAccess.GetData(checkQuery);

            if (result.Rows.Count == 0 || !(bool)result.Rows[0]["Availability"])
            {
                Console.WriteLine("Video is not available for rent.");
                return;
            }

            // Insert rental record into the database
            string rentQuery = "INSERT INTO Rentals (CustomerID, VideoID, RentalDate, ReturnDate,Status) " +
                               "VALUES (@CustomerID, @VideoID, @RentDate, NULL,'Rented'); " +
                               "UPDATE Videos SET Availability = 0 WHERE VideoID = @VideoID;";
            _dataAccess.ExecuteQuery(rentQuery, cmd =>
            {
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                cmd.Parameters.AddWithValue("@VideoID", videoId);
                cmd.Parameters.AddWithValue("@RentDate", DateTime.Now);
            });

            // Add rental to hash table
            Rental rental = new Rental
            {
                RentalID = GenerateRentalID(),
                CustomerID = customerId,
                VideoID = videoId,
                RentDate = DateTime.Now,
                ReturnDate = null
            };
            _rentalTable.AddRental(rental);
            videoService.UpdateVideoAvailability(videoId, false);

            Console.WriteLine("Video rented successfully!");
        }

        //method to generate rental ID
        private int GenerateRentalID()
        {
            // A simple random generator for Rental ID (for demo purposes)
            Random random = new Random();
            return random.Next(1000, 9999);
        }

        public void DisplayYetToReturn()
        {
            _rentalTable.DisplayYetToReturn();
        }

        public void DisplayYetToReturn(int customerId)
        {
            _rentalTable.DisplayCustomerYetToReturn(customerId);
        }

        public int GetYetToReturnCount()
        {
            return _rentalTable.CountYetToReturn();
        }

        public int GetYetToReturnCount(int customerId)
        {
            return _rentalTable.CountYetToReturn(customerId);
        }

        public void ReturnVideo(int rentalId)
        {
            Rental rental = _rentalTable.GetRental(rentalId);

            if (rental == null)
            {
                Console.WriteLine("Rental record not found.");
                return;
            }

            // Update rental return date and set video as available
            string returnQuery = "UPDATE Rentals SET ReturnDate = @ReturnDate WHERE RentalID = @RentalID; " +
                                 "UPDATE Videos SET Availability = 1 WHERE VideoID = @VideoID;" +
                                 "UPDATE Rentals SET Status = 'Returned' WHERE RentalID = @RentalID;";
            _dataAccess.ExecuteQuery(returnQuery, cmd =>
            {
                cmd.Parameters.AddWithValue("@ReturnDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@RentalID", rentalId);
                cmd.Parameters.AddWithValue("@VideoID", rental.VideoID);
            });
            _rentalTable.ReturnVideo(rentalId);
            videoService.UpdateVideoAvailability(rental.VideoID, true);

            Console.WriteLine("Video returned successfully!");
        }
    }
}
