using RentalApp.Models.Rentals;

namespace RentalApp.Services.Penalty;

public class DailyPenaltyCalculator : IPenaltyCalculator
{
    private decimal _dailyPenaltyFee;
    
    public DailyPenaltyCalculator(decimal dailyPenaltyFee)
    {
        _dailyPenaltyFee = dailyPenaltyFee;
    }

    public decimal Calculate(Rental rental)
    {
        DateTime actualReturn = rental.ReturnDate ?? DateTime.Today;
        TimeSpan delay = actualReturn - rental.DueDate;
        int daysLate = (int)delay.TotalDays;
        
        if (daysLate <= 0) return 0m;
        
        return daysLate * _dailyPenaltyFee;
    }
    
}