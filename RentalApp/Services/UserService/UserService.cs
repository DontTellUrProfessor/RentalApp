using RentalApp.Models.Enums;
using RentalApp.Models.Users;

namespace RentalApp.Services.UserService;

public class UserService : IUserService
{
    private List<User> _users = new();
    
    public User AddUser(string name, string surname, string email, UserType userType)
    {
        User newUser = userType switch
        {
            UserType.Student  => new Student(name, surname, email),
            UserType.Employee => new Employee(name, surname, email),
            _ => throw new ArgumentException("Nieznany typ użytkownika")
        };
        _users.Add(newUser);
        return newUser;
    }
    
    public IEnumerable<User> GetAllUsers()
    {
        return _users;
    }

    public User? GetUser(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id); //składnia LINQ
    }
}