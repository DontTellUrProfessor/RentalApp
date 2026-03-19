using RentalApp.Services.DeviceService;
using RentalApp.Services.Penalty;
using RentalApp.Services.RentalService;
using RentalApp.Services.UserService;
using RentalApp.UI;

// Inicjalizacja serwisów
IPenaltyCalculator penaltyCalculator = new DailyPenaltyCalculator(5.00m);
IDeviceService deviceService = new DeviceService();
IUserService userService = new UserService();
IRentalService rentalService = new RentalService(penaltyCalculator, deviceService);

// Uruchomienie menu
var menuHandler = new MenuHandler(userService, deviceService, rentalService);
menuHandler.Start();