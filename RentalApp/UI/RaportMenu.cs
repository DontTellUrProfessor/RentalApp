using RentalApp.Services.DeviceService;
using RentalApp.Services.RentalService;
using RentalApp.Services.UserService;

namespace RentalApp.UI;

public class RaportMenu
{
    private readonly IRentalService _rentalService;
    private readonly IDeviceService _deviceService;
    private readonly IUserService _userService;

    public RaportMenu(IRentalService rentalService, IDeviceService deviceService, IUserService userService)
    {
        _rentalService = rentalService;
        _deviceService = deviceService;
        _userService = userService;
    }

    public void Show()
    {
        
    }
}