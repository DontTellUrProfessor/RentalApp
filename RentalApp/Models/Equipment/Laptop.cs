namespace RentalApp.Models.Equipment;

public class Laptop : Device
{
    
    public double ScreenSize { get; set; }
    public string OperatingSystem { get; set; }
    public int Ram { get; set; }
    
    
    
    public Laptop(string name, double screenSize, string operatingSystem, int ram) : base(name)
    {
        ScreenSize = screenSize;
        OperatingSystem = operatingSystem;
        Ram = ram;
    }
    
}