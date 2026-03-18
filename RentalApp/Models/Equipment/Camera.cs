namespace RentalApp.Models.Equipment;

public class Camera : Device
{
    public int Mpix { get; set; }
    public int SensorX { get; set; } //value in mm
    public int SensorY  { get; set; } //value in mm

    public Camera(string name, int mpix, int sensorX, int sensorY) : base(name)
    {
        Mpix = mpix;
        SensorX = sensorX;
        SensorY = sensorY;
    }
}