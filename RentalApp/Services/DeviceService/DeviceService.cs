using RentalApp.Models.Equipment;

namespace RentalApp.Services.DeviceService;

public class DeviceService : IDeviceService
{
    private List<Device> _devices = new();
    
    public Device AddDevice(Device device)
    {
        _devices.Add(device);
        return device;
    }

    public Device? GetDevice(int id)
    {
        return _devices.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Device> GetAllDevices()
    {
        return _devices;
    }

    public void MarkAsUnavailable(int id)
    {
        Device? device = GetDevice(id);
        if (device == null) throw new KeyNotFoundException($"Urządzenie o Id {id} nie istnieje"); 
        device.IsAvailable = false;
    }

    public void MarkAsAvailable(int id)
    {
        Device? device = GetDevice(id);
        if (device == null) throw new KeyNotFoundException($"Urządzenie o Id {id} nie istnieje"); 
        device.IsAvailable = true;
    }
    
    public IEnumerable<Device> GetAvailableDevices()
    {
        return _devices.Where(a => a.IsAvailable);
    }
}