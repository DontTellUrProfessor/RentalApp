using RentalApp.Models.Users;
using RentalApp.Services.DeviceService;
using RentalApp.Services.RentalService;
using RentalApp.Services.UserService;

namespace RentalApp.UI;

public class ReportMenu
{
    private readonly IRentalService _rentalService;
    private readonly IDeviceService _deviceService;
    private readonly IUserService _userService;

    public ReportMenu(IRentalService rentalService, IDeviceService deviceService, IUserService userService)
    {
        _rentalService = rentalService;
        _deviceService = deviceService;
        _userService = userService;
    }

    private void DisplayActiveRentals()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Lista użytkowników ===");
            foreach (var user in _userService.GetAllUsers())
            {
                Console.WriteLine($"{user.Id}. {user.Name} {user.Surname} {user.Email}");
            }
            Console.WriteLine("\nPodaj Id użytkownika, którego aktywne wypożyczenia chcesz zobaczyć: ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Podaj poprawną liczbę!");
                continue;
            }
            
            User? selectedUser = _userService.GetUser(userId);
            if (selectedUser == null)
            {
                Console.WriteLine($"Użytkownik o Id {userId} nie istnieje!");
                continue;
            }
            
            Console.Clear();
            Console.WriteLine("=== Aktywne wypożyczenia ===");

            foreach (var rental in _rentalService.GetActiveRentals(selectedUser))
            {
                Console.WriteLine($"{rental.Id}. {rental.User.Name} {rental.User.Surname} - {rental.Device.Name} - do {rental.DueDate:dd.MM.yyyy} {(rental.IsOverdue ? "⚠ PRZETERMINOWANE" : "")}");
            }
            
            Console.WriteLine("\nNaciśnij Enter aby kontynuować");
            Console.ReadLine();
            return;
        }

    }

    private void DisplayOverdueRentals()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Przeterminowane wypożyczenia ===");
            foreach (var overdueRental in _rentalService.GetOverdueRentals())
            {
                Console.WriteLine($"{overdueRental.Id}. {overdueRental.User.Name} {overdueRental.User.Surname} - {overdueRental.Device.Name} - do {overdueRental.DueDate:dd.MM.yyyy}");
            }
            
            Console.WriteLine("\nNaciśnij Enter aby kontynuować");
            Console.ReadLine();
            return;
        }
    }

    private void DisplaySummaryReport()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Raport podsumowujący ===");
            
            Console.WriteLine("\nSPRZĘT:");
            var allDevices = _deviceService.GetAllDevices();
            Console.WriteLine($"- Wszystkich urządzeń: {allDevices.Count()}");
            Console.WriteLine($"- Dostępnych: {allDevices.Count(d => d.IsAvailable)}");
            Console.WriteLine($"- Niedostępnych: {allDevices.Count(d => !d.IsAvailable)}");
            
            Console.WriteLine("\nUŻYTKOWNICY:");
            var allUsers = _userService.GetAllUsers();
            Console.WriteLine($"- Wszyscy użytkownicy: {allUsers.Count()}");
            Console.WriteLine($"- Studenci: {allUsers.Count(u => u is Student)}");
            Console.WriteLine($"- Pracownicy: {allUsers.Count(u => u is Employee)}");
            
            Console.WriteLine("\nWYPOŻYCZENIA:");
            var allRentals = _rentalService.GetAllRentals();
            Console.WriteLine($"- Wszystkich wypożyczeń: {allRentals.Count()}");
            Console.WriteLine($"- Aktywnych: {allRentals.Count(r => r.ReturnDate == null)}");
            Console.WriteLine($"- Przeterminowanych: {_rentalService.GetOverdueRentals().Count()}");
            Console.WriteLine("\nNaciśnij enter aby kontynuować");
            Console.ReadLine();
            return;

        }
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Report Menu ===");
            Console.WriteLine("1. Lista całego sprzętu");
            Console.WriteLine("2. Lista dostępnego sprzętu");
            Console.WriteLine("3. Aktywne wypożyczenia użytkownika");
            Console.WriteLine("4. Przeterminowane wypożyczenia");
            Console.WriteLine("5. Raport podsumowujący");
            Console.WriteLine("0. Powrót");
            Console.Write("\nWybierz opcję: "); 
            
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    foreach (var device in _deviceService.GetAllDevices())
                    {
                        Console.WriteLine($"{device.Id}. [{device.GetType().Name}] {device.Name} - {(device.IsAvailable ? "dostępny" : "niedostępny")}");
                    }
                    Console.WriteLine("\nNaciśnij enter aby kontynuować");
                    Console.ReadLine();
                    break;
                case "2":
                    foreach (var device in _deviceService.GetAllDevices())
                    {
                        if (device.IsAvailable == true)
                        {
                            Console.WriteLine($"{device.Id}. [{device.GetType().Name}] {device.Name}");
                        }
                    }
                    Console.WriteLine("\nNaciśnij enter aby kontynuować");
                    Console.ReadLine();
                    break;
                case "3":
                    DisplayActiveRentals();
                    break;
                case "4":
                    DisplayOverdueRentals();
                    break;
                case "5":
                    DisplaySummaryReport();
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