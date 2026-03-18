using RentalApp.Models.Enums;
using RentalApp.Models.Users;

namespace RentalApp.Services.UserService;

public interface IUserService
{
    User AddUser(string name, string surname, string email, UserType userType);
    User? GetUser(int id);
    IEnumerable<User> GetAllUsers();
}