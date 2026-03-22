using RentalApp.Models.Enums;
using RentalApp.Models.Equipment;
using RentalApp.Services.DeviceService;

namespace RentalApp.UI;

public class DeviceMenu
{
    private readonly IDeviceService _deviceService;
    
    public DeviceMenu(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    private void AddDevice()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Device Type ===");
            Console.WriteLine("1. Laptop");
            Console.WriteLine("2. Kamera");
            Console.WriteLine("3. Bateria");
            Console.WriteLine("0. Powrót");
            
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddLaptop();
                    break;
                case "2":
                    break;
                case "3":
                    AddBattery();
                    break;
                default:
                    Console.WriteLine("Nieznana opcja, spróbuj ponownie");
                    break;
            }
            
        }
    }

    private void AddLaptop()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Laptop ===");
            Console.Write("Podaj nazwę: ");
            var name = Console.ReadLine();
            Console.Write("Podaj przekątną ekranu: ");
            
            if (!double.TryParse(Console.ReadLine(), out double screenSize))
            {
                Console.WriteLine("Podaj poprawną liczbę!");
                continue; // wróć do początku pętli
            }
            
            Console.Write("Podaj system operacyjny: ");
            var system = Console.ReadLine();
            Console.Write("Podaj ilość RAM: ");
            
            if (!int.TryParse(Console.ReadLine(), out int ramSize))
            {
                Console.WriteLine("Podaj poprawną liczbę!");
                continue; // wróć do początku pętli
            }
            
            _deviceService.AddDevice(new Laptop(name, screenSize, system, ramSize));
            
            Console.WriteLine($"Dodano laptop: {name}");
            Console.WriteLine("Naciśnij enter aby kontynuować");
            Console.ReadLine();
            return;

        }
    }

    private void AddBattery()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Bateria ===");
            Console.Write("Podaj nazwę: ");
            var name = Console.ReadLine();
            Console.Write("Podaj pojemność (w mah): ");
            
            if (!int.TryParse(Console.ReadLine(), out int capacity))
            {
                Console.WriteLine("Podaj poprawną liczbę!");
                continue; // wróć do początku pętli
            }
            
            var connectorIn = SelectConnectorType("input");
            var connectorOut = SelectConnectorType("output");

            _deviceService.AddDevice(new Battery(name, capacity, connectorIn, connectorOut));
            
            Console.WriteLine($"Dodano baterię: {name}");
            Console.WriteLine("Naciśnij enter aby kontynuować");
            Console.ReadLine();
            return;

        }
    }
    
    private ConnectorType SelectConnectorType(string label)
    {
        while (true)
        {
            Console.WriteLine($"Podaj typ złącza {label} (tylko numer):");
            Console.WriteLine("1. UsbA");
            Console.WriteLine("2. UsbC");
            Console.WriteLine("3. MicroUsb");
            Console.WriteLine("4. Lightning");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": return ConnectorType.UsbA;
                case "2": return ConnectorType.UsbC;
                case "3": return ConnectorType.MicroUsb;
                case "4": return ConnectorType.Lightning;
                default:
                    Console.WriteLine("Nieznana opcja, spróbuj ponownie");
                    break;
            }
        }
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Device Menu ===");
            Console.WriteLine("1. Dodaj urządzenie");
            Console.WriteLine("2. Oznacz jako niedostępny");
            Console.WriteLine("3. Oznacz jako dostępny");
            Console.WriteLine("0. Powrót");
            
            
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddDevice();
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
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