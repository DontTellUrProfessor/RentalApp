namespace RentalApp.Models.Users;

public abstract class User
{
    private static int _idCounter = 0;
    public int Id  { get; set; }
    
    public string Name  { get; set; }
    public string Surname  { get; set; }
    public string Email  { get; set; }
    public abstract int MaxRentals { get; }

    protected User(string name, string surname, string email)
    {
        _idCounter++;
        Id = _idCounter;
        Name = name;
        Surname = surname;
        Email = email;
    }
}