using VideoRentingSystem.Services;

namespace VideoRentingSystem
{
    class Program
    {
        private static CustomerService customerService = new CustomerService(); // CustomerService instance
        private static VideoService videoService = new VideoService(); // VideoService instance
        private static RentalService rentalService = new RentalService(); // RentalService instance
        private static UserService userService = new UserService(); // UserService instance

        static void Main(string[] args)
        {
        }
    }
}
