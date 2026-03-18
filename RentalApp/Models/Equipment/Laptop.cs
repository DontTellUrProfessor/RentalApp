namespace RentalApp.Models.Equipment;

public class Laptop : Device
{
    
    public string Model { get; set; }
    public string OperatingSystem { get; set; }
    public int Ram { get; set; }
    
    
    
    public Laptop(string name, string model, string operatingSystem, int ram) : base(name)
    {
        Model = model;
        OperatingSystem = operatingSystem;
        Ram = ram;
    }
    
}