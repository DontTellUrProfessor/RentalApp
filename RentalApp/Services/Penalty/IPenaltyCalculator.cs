using RentalApp.Models.Rentals;

namespace RentalApp.Services.Penalty;

public interface IPenaltyCalculator
{
    decimal Calculate(Rental rental);
}