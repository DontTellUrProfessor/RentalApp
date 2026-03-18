namespace RentalApp.Models.Users;

public class Employee : User
{
    public override int MaxRentals => 5;

    public Employee(string name, string surname, string email) : base(name, surname, email)
    {
        
    }
}