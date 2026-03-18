using RentalApp.Models.Rentals;

namespace RentalApp.Services;

public interface IPenaltyCalculator
{
    decimal Calculate(Rental rental);
}