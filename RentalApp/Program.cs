using RentalApp.Models.Equipment;

var laptop = new Laptop("Dell XPS", 14.2, "Windows 11", 16);
Console.WriteLine(laptop.IsAvailable); // true
laptop.IsAvailable = false;
Console.WriteLine(laptop.Id + ". " +  laptop.Name + " " + laptop.OperatingSystem + " " + laptop.Ram + " " + laptop.IsAvailable);
