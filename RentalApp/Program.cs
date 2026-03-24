using RentalApp.Models.Enums;
using RentalApp.Models.Equipment;
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

// Użytkownicy
var student1 = userService.AddUser("Jan", "Kowalski", "jan.kowalski@email.com", UserType.Student);
var student2 = userService.AddUser("Anna", "Nowak", "anna.nowak@email.com", UserType.Student);
var student3 = userService.AddUser("Piotr", "Wiśniewski", "piotr.wisniewski@email.com", UserType.Student);
var employee1 = userService.AddUser("Marek", "Zielinski", "marek.zielinski@email.com", UserType.Employee);
var employee2 = userService.AddUser("Katarzyna", "Wójcik", "katarzyna.wojcik@email.com", UserType.Employee);

// Laptopy
var laptop1 = deviceService.AddDevice(new Laptop("Dell XPS 15", 15.6, "Windows 11", 16));
var laptop2 = deviceService.AddDevice(new Laptop("MacBook Pro", 14.2, "macOS", 32));
var laptop3 = deviceService.AddDevice(new Laptop("Lenovo ThinkPad", 14.0, "Ubuntu", 8));

// Kamery
var camera1 = deviceService.AddDevice(new Camera("Canon EOS R5", 45, 36, 24));
var camera2 = deviceService.AddDevice(new Camera("Sony A7 III", 24, 35, 23));
var camera3 = deviceService.AddDevice(new Camera("Nikon Z6", 24, 35, 24));

// Baterie
var battery1 = deviceService.AddDevice(new Battery("Anker PowerCore", 20000, ConnectorType.UsbC, ConnectorType.UsbA));
var battery2 = deviceService.AddDevice(new Battery("Baseus 10000", 10000, ConnectorType.UsbC, ConnectorType.UsbC));

// Oznacz jedno urządzenie jako niedostępne (w serwisie)
deviceService.MarkAsUnavailable(laptop3.Id);

// Aktywne wypożyczenia
rentalService.AddRental(student1, laptop1, 7);
rentalService.AddRental(student1, camera1, 3);
rentalService.AddRental(student2, camera2, 14);
rentalService.AddRental(employee1, laptop2, 30);
rentalService.AddRental(employee1, battery1, 5);

// Uruchomienie menu
var menuHandler = new MenuHandler(userService, deviceService, rentalService);
menuHandler.Start();