using RentalApp.Services.DeviceService;
using RentalApp.Services.RentalService;
using RentalApp.Services.UserService;

namespace RentalApp.UI;

public class RentalMenu
{
    
    private readonly IDeviceService _deviceService;
    private readonly IUserService _userService;
    private readonly IRentalService _rentalService;
    
    public RentalMenu(IRentalService rentalService, IUserService userService, IDeviceService deviceService)
    {
        _rentalService = rentalService;
        _userService = userService;
        _deviceService = deviceService;
    }

    private void RentDevice()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Wypożycz sprzęt ===");
            Console.WriteLine("Użytkownicy: ");
        
            foreach (var user in _userService.GetAllUsers())
            {
                Console.WriteLine($"{user.Id}. {user.Name} {user.Surname} {user.Email}");
            }
        
            Console.WriteLine("\nPodaj Id użytkownika dla którego chcesz wypożyczyć sprzęt: ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Podaj poprawną liczbę!");
                continue; // wróć do początku pętli
            }

            var selectedUser = _userService.GetUser(userId);
            if (selectedUser == null)
            {
                Console.WriteLine($"Użytkownik o Id {userId} nie istnieje!");
                continue;
            }
            
            Console.WriteLine("Dostępne urządzenia: ");
            foreach (var device in _deviceService.GetAvailableDevices())
            {
                Console.WriteLine($"{device.Id}. {device.Name}");
            }
            
            Console.WriteLine("\nPodaj Id urządzenia które chcesz wypożyczyć: ");
            if (!int.TryParse(Console.ReadLine(), out int deviceId))
            {
                Console.WriteLine("Podaj poprawną liczbę!");
                continue; // wróć do początku pętli
            }
            var selectedDevice = _deviceService.GetDevice(deviceId);
            if (selectedDevice == null)
            {
                Console.WriteLine($"Urządzenie o Id {deviceId} nie istnieje!");
                continue;
            }
            
            Console.WriteLine("Na ile dni chcesz wypożyczyć urządzenie: ");
            if (!int.TryParse(Console.ReadLine(), out int days))
            {
                Console.WriteLine("Podaj poprawną liczbę!");
                continue; // wróć do początku pętli
            }
            
            try
            {
                _rentalService.AddRental(selectedUser, selectedDevice, days);
                Console.WriteLine($"Wypożyczono {selectedDevice.Id}. {selectedDevice.Name} pomyślnie!");
                Console.WriteLine("Naciśnij Enter aby kontynuować");
                Console.ReadLine();
                return;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                Console.ReadLine();
                return;
            }
        }
    }

    private void ReturnDevice()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Zwróć sprzęt ===");
            Console.WriteLine("Wypożyczenia: ");
            
            foreach (var rental in _rentalService.GetAllRentals()
                         .Where(r => r.ReturnDate == null))
            {
                Console.WriteLine($"{rental.Id}. {rental.User.Name} - {rental.Device.Name} (do {rental.DueDate:dd.MM.yyyy})");
            }
            
            Console.WriteLine("Podaj Id wypożyczenia które chcesz zakończyć");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Podaj poprawną liczbę!");
                continue; // wróć do początku pętli
            }
            
            var rent = _rentalService.GetRental(id);
            if (rent == null)
            {
                Console.WriteLine($"Wypożyczenie o Id {id} nie istnieje!");
                continue;
            }
            
            decimal penalty = _rentalService.ReturnDevice(rent);
            if (penalty > 0)
                Console.WriteLine($"Naliczono karę: {penalty:C}");
            else
                Console.WriteLine("Zwrot w terminie, brak kary!");
            
            Console.WriteLine("Naciśnij Enter aby kontynuować");
            Console.ReadLine();
            return;
        }
    }
    

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Rental Menu ===");
            Console.WriteLine("1. Wypożycz sprzęt");
            Console.WriteLine("2. Zwróć sprzęt");
            Console.WriteLine("0. Powrót");
            
            
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RentDevice();
                    break;
                case "2":
                    ReturnDevice();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Nieznana opcja, spróbuj ponownie");
                    break;
            }
        }
    }
}