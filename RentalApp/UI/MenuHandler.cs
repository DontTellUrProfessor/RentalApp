using RentalApp.Services.DeviceService;
using RentalApp.Services.RentalService;
using RentalApp.Services.UserService;

namespace RentalApp.UI;

public class MenuHandler
{
    private readonly IDeviceService _deviceService;
    private readonly IUserService _userService;
    private readonly IRentalService _rentalService;

    public MenuHandler(IUserService userService, IDeviceService deviceService,  IRentalService rentalService)
    {
        _userService = userService;
        _deviceService = deviceService;
        _rentalService = rentalService;
    }

    public void Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Rental App ===");
            Console.WriteLine("1. Zarządzanie użytkownikiem");
            Console.WriteLine("2. Zarządzanie urządzeniami");
            Console.WriteLine("3. Zarządzanie wypożyczeniami");
            Console.WriteLine("4. Raporty");
            Console.WriteLine("0. Wyjście");
            Console.Write("\nWybierz opcję: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    new UserMenu(_userService).Show();
                    break;
                case "2":
                    new DeviceMenu(_deviceService).Show();
                    break;
                case "3":
                    new RentalMenu(_rentalService, _userService, _deviceService).Show();
                    break;
                case "4":
                    //new ReportMenu(_rentalService, _deviceService, _userService).Show();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Nieznana opcja");
                    break;
            }
        }
    }
}