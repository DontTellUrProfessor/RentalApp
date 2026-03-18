using RentalApp.Models.Equipment;
using RentalApp.Models.Users;

namespace RentalApp.Models.Rentals;

public class Rental
{
    private static int _idCounter = 0;
    public int Id  { get; set; }
    
    public User User { get; set; }
    public Device Device { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsOverdue => ReturnDate == null && DateTime.Now > DueDate;
    
    public Rental(User user, Device device, int rentalDays)
    {
        _idCounter++;
        Id = _idCounter;
        User = user;
        Device = device;
        RentalDate = DateTime.Now;
        DueDate = RentalDate.AddDays(rentalDays);
    }
}