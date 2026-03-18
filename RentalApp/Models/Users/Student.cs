namespace RentalApp.Models.Users;

public class Student : User
{
    public override int MaxRentals => 2;

    public Student(string name, string surname, string email) : base(name, surname, email)
    {
        
    }
}