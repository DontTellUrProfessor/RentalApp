using RentalApp.Models.Equipment;
using RentalApp.Models.Rentals;
using RentalApp.Models.Users;
using RentalApp.Services.DeviceService;
using RentalApp.Services.Penalty;

namespace RentalApp.Services.RentalService;

public class RentalService : IRentalService
{
    private List<Rental> _rentals = new();
    private readonly IPenaltyCalculator _penaltyCalculator;
    private readonly IDeviceService _deviceService;
    
    public RentalService(IPenaltyCalculator penaltyCalculator, IDeviceService deviceService)
    {
        _penaltyCalculator = penaltyCalculator;
        _deviceService = deviceService;
    }
    
    public Rental AddRental(User user, Device device, int rentalDays)
    {
        IEnumerable<Rental> currentUser = GetActiveRentals(user);
        Device? currentDevice = _deviceService.GetDevice(device.Id);
        
        if (currentDevice == null) 
            throw new KeyNotFoundException($"Urządzenie o Id {device.Id} nie istnieje");
        
        if (!currentDevice.IsAvailable)
            throw new InvalidOperationException("Urządzenie jest niedostępne");
        
        if (currentUser.Count() >= user.MaxRentals)
            throw new InvalidOperationException($"Użytkownik osiągnął limit {user.MaxRentals} wypożyczeń");
        
        Rental newRental = new Rental(user, device, rentalDays);
        currentDevice.IsAvailable = false;
        _rentals.Add(newRental);
        return newRental;
    }
    
    public Rental? GetRental(int id)
    {
        return _rentals.FirstOrDefault(r => r.Id == id);
    }

    public decimal ReturnDevice(Rental rental)
    {
        Rental? currentRental = _rentals.FirstOrDefault(x => x.Id == rental.Id);
        
        if(currentRental == null) throw new InvalidOperationException($"Brak numeru {rental.Id} na liście wypożyczeń");
        
        currentRental.ReturnDate = DateTime.Now;
        decimal penaltyFee = _penaltyCalculator.Calculate(currentRental);
        _deviceService.MarkAsAvailable(currentRental.Device.Id);
        
        return penaltyFee;
    }

    public IEnumerable<Rental> GetActiveRentals(User user)
    {
        return _rentals.Where(x => x.User.Id == user.Id && x.ReturnDate == null);
    }

    public IEnumerable<Rental> GetAllRentals()
    {
        return _rentals;
    }

    public IEnumerable<Rental> GetOverdueRentals()
    {
        return _rentals.Where(x => x.IsOverdue);
    }
}