using RentalApp.Models.Enums;
using RentalApp.Models.Users;
using RentalApp.Services.UserService;

namespace RentalApp.UI;

public class UserMenu
{
    private readonly IUserService _userService;
    
    public  UserMenu(IUserService userService)
    {
        _userService = userService;
    }

    private void AddUser()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== User Type ===");
            Console.WriteLine("1. Student");
            Console.WriteLine("2. Employee");
            Console.WriteLine("0. Powrót");
            
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "0":
                    return;
                case "1":
                    Console.Clear();
                    Console.WriteLine("=== Student ===");
                    Console.Write("Podaj imie: ");
                    var name = Console.ReadLine();
                    Console.Write("Podaj nazwisko: ");
                    var surname = Console.ReadLine();
                    Console.Write("Podaj email: ");
                    var email = Console.ReadLine();
                    
                    _userService.AddUser(name, surname, email, UserType.Student);
                    Console.WriteLine($"Dodano użytkownika: {name} {surname}");
                    Console.WriteLine("Naciśnij enter aby kontynuować");
                    Console.ReadLine();
                    return;
                
                case "2":
                    Console.Clear();
                    Console.WriteLine("=== Employee ===");
                    Console.Write("Podaj imie: ");
                    var name1 = Console.ReadLine();
                    Console.Write("Podaj nazwisko: ");
                    var surname1 = Console.ReadLine();
                    Console.Write("Podaj email: ");
                    var email1 = Console.ReadLine();
                    
                    _userService.AddUser(name1, surname1, email1, UserType.Employee);
                    Console.WriteLine($"Dodano użytkownika: {name1} {surname1}");
                    Console.WriteLine("Naciśnij enter aby kontynuować");
                    Console.ReadLine();
                    return;
                default:
                    Console.WriteLine("Nieznana opcja");
                    break;
            }
        }
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== User Menu ===");
            Console.WriteLine("1. Dodaj użytkownika");
            Console.WriteLine("0. Powrót");
            
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddUser();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Nieznana opcja");
                    break;
            }
        }

    }
}