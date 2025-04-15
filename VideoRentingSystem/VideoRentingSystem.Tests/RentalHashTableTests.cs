using VideoRentingSystem.DataStructures;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.Tests
{
    [TestClass]
    public class RentalHashTableTests
    {
        private RentalHashTable rentalTable;
        private Rental rental1;
        private Rental rental2;
        private Rental rental3;

        [TestInitialize]
        public void TestInitialize()
        {
            rentalTable = new RentalHashTable();
            rentalTable.Clear();

            rental1 = new Rental
            {
                CustomerID = 1,
                VideoID = 101,
                RentDate = DateTime.Now.AddDays(-5),
                Status = "Rented"
            };

            rental2 = new Rental
            {
                CustomerID = 2,
                VideoID = 102,
                RentDate = DateTime.Now.AddDays(-3),
                Status = "Rented"
            };

            rental3 = new Rental
            {
                CustomerID = 1,
                VideoID = 103,
                RentDate = DateTime.Now.AddDays(-1),
                Status = "Returned",
                ReturnDate = DateTime.Now
            };
        }

        [TestMethod]
        public void Instance_ShouldReturnSingletonInstance()
        {
            // Arrange
            var instance1 = RentalHashTable.Instance;
            var instance2 = RentalHashTable.Instance;

            // Assert
            Assert.AreSame(instance1, instance2, "Instance should return the same object");
        }

        [TestMethod]
        public void AddRental_ShouldAddNewRental()
        {
            // Act
            rentalTable.AddRental(rental1);

            // Assert
            Assert.AreEqual(1, rentalTable.Count(), "Count should be 1 after adding a rental");
        }

        [TestMethod]
        public void AddRental_ShouldSetDefaultStatusWhenEmpty()
        {
            // Arrange
            var newRental = new Rental
            {
                CustomerID = 3,
                VideoID = 104,
                RentDate = DateTime.Now
            };

            // Act
            rentalTable.AddRental(newRental);

            // Assert
            var addedRental = rentalTable.GetRental(newRental.RentalID);
            Assert.AreEqual("Rented", addedRental.Status, "Should set default status to 'Rented'");
        }

        [TestMethod]
        public void AddRental_ShouldAutoIncrementRentalID()
        {
            // Act
            rentalTable.AddRental(rental1);
            rentalTable.AddRental(rental2);
            rentalTable.AddRental(rental3);

            // Assert
            Assert.AreEqual(1, rental1.RentalID, "First rental should have ID 1");
            Assert.AreEqual(2, rental2.RentalID, "Second rental should have ID 2");
            Assert.AreEqual(3, rental3.RentalID, "Third rental should have ID 3");
        }

        [TestMethod]
        public void GetRental_ShouldReturnCorrectRental()
        {
            // Arrange
            rentalTable.AddRental(rental1);

            // Act
            var result = rentalTable.GetRental(rental1.RentalID);

            // Assert
            Assert.IsNotNull(result, "Should return the rental");
            Assert.AreEqual(rental1.CustomerID, result.CustomerID, "CustomerID should match");
            Assert.AreEqual(rental1.VideoID, result.VideoID, "VideoID should match");
        }

        [TestMethod]
        public void GetRental_ShouldReturnNullForNonExistingRental()
        {
            // Act
            var result = rentalTable.GetRental(999);

            // Assert
            Assert.IsNull(result, "Should return null for non-existing rental");
        }

        [TestMethod]
        public void ReturnVideo_ShouldUpdateStatusAndReturnDate()
        {
            // Arrange
            rentalTable.AddRental(rental1);
            var beforeReturn = DateTime.Now;

            // Act
            rentalTable.ReturnVideo(rental1.RentalID);

            // Assert
            var returnedRental = rentalTable.GetRental(rental1.RentalID);
            Assert.AreEqual("Returned", returnedRental.Status, "Status should be updated to 'Returned'");
            Assert.IsTrue(returnedRental.ReturnDate >= beforeReturn, "ReturnDate should be set to current time or later");
        }

        [TestMethod]
        public void ReturnVideo_ShouldDoNothingForNonExistingRental()
        {
            // Act
            rentalTable.ReturnVideo(999); // Non-existing ID

            // Assert - No exception should be thrown
        }

        [TestMethod]
        public void Count_ShouldReturnCorrectCount()
        {
            // Arrange
            rentalTable.AddRental(rental1);
            rentalTable.AddRental(rental2);
            rentalTable.AddRental(rental3);

            // Act
            var count = rentalTable.Count();

            // Assert
            Assert.AreEqual(3, count, "Should count all rentals");
        }

        [TestMethod]
        public void CountYetToReturn_ShouldCountOnlyRentedVideos()
        {
            // Arrange
            rentalTable.AddRental(rental1); // Rented
            rentalTable.AddRental(rental2); // Rented
            rentalTable.AddRental(rental3); // Returned

            // Act
            var count = rentalTable.CountYetToReturn();

            // Assert
            Assert.AreEqual(2, count, "Should count only rented videos");
        }

        [TestMethod]
        public void CountYetToReturn_WithCustomerID_ShouldCountCorrectly()
        {
            // Arrange
            rentalTable.AddRental(rental1); // Customer 1, Rented
            rentalTable.AddRental(rental2); // Customer 2, Rented
            rentalTable.AddRental(rental3); // Customer 1, Returned

            // Act
            var count = rentalTable.CountYetToReturn(1);

            // Assert
            Assert.AreEqual(1, count, "Should count only rented videos for customer 1");
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllRentals()
        {
            // Arrange
            rentalTable.AddRental(rental1);
            rentalTable.AddRental(rental2);

            // Act
            rentalTable.Clear();

            // Assert
            Assert.AreEqual(0, rentalTable.Count(), "Count should be 0 after clear");
        }

        [TestMethod]
        public void DisplayMethods_ShouldNotThrowExceptions()
        {
            // Arrange
            rentalTable.AddRental(rental1);
            rentalTable.AddRental(rental2);
            rentalTable.AddRental(rental3);

            // Act & Assert
            try
            {
                rentalTable.Display();
                rentalTable.DisplayYetToReturn();
                rentalTable.DisplayCustomerRentals(1);
                rentalTable.DisplayCustomerYetToReturn(1);
            }
            catch
            {
                Assert.Fail("Display methods should not throw exceptions");
            }
        }
    }
}
