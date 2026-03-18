using RentalApp.Models.Equipment;

namespace RentalApp.Services.DeviceService;

public interface IDeviceService
{
    Device AddDevice(Device device);
    Device? GetDevice(int id);
    IEnumerable<Device> GetAllDevices();
    void MarkAsUnavailable(int id);
    void MarkAsAvailable(int id);
    IEnumerable<Device> GetAvailableDevices();
    
}