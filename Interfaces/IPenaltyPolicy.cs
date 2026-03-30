using UniEquipmentRental.Models;

namespace UniEquipmentRental.Interfaces;

public interface IPenaltyPolicy
{
    decimal CalculatePenalty(Rental rental);
}