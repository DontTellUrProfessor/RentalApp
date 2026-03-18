namespace RentalApp.Models.Equipment;

public abstract class Device
{
    private static int _idCounter = 0;
    
    public int Id { get; private set; }
    public string Name { get; set; }
    public bool IsAvailable { get; set; } = true;

    protected Device(string name)
    {
        _idCounter++;
        Id = _idCounter;
        Name = name;
        IsAvailable = true;
    }
}