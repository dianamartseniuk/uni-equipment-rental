using UniEquipmentRental.Interfaces;
using UniEquipmentRental.Models;

namespace UniEquipmentRental.Policies;

public class DefaultPenaltyPolicy : IPenaltyPolicy
{
    public decimal CalculatePenalty(Rental rental)
    {
        if (rental is null)
            throw new ArgumentNullException(nameof(rental));

        if (rental.IsActive())
            return 0;

        if (rental.WasReturnedOnTime())
            return 0;

        var daysLate = (rental.ActualReturnDate!.Value.Date - rental.DueDate.Date).Days;
        return daysLate * 10m;
    }
}