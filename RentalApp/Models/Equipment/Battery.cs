using RentalApp.Models.Enums;

namespace RentalApp.Models.Equipment;

public class Battery : Device
{
    public int Capacity { get; set; } // in mah
    public ConnectorType InputType { get; set; }
    public ConnectorType OutputType { get; set; }

    public Battery(string name, int capacity, ConnectorType inputType, ConnectorType outputType) : base(name)
    {
        Capacity = capacity;
        InputType = inputType;
        OutputType = outputType;
    }
}