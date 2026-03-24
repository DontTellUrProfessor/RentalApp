using RentalApp.Models.Equipment;
using RentalApp.Models.Rentals;
using RentalApp.Models.Users;

namespace RentalApp.Services.RentalService;

public interface IRentalService
{
    Rental AddRental(User user, Device device, int rentalDays); 
    Rental? GetRental(int id);
    decimal ReturnDevice(Rental rental);
    IEnumerable<Rental> GetAllRentals();
    IEnumerable<Rental> GetActiveRentals(User user);
    IEnumerable<Rental> GetOverdueRentals();
}