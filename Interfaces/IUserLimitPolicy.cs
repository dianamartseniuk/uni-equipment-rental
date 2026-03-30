using UniEquipmentRental.Models;

namespace UniEquipmentRental.Interfaces;

public interface IUserLimitPolicy
{
    int GetRentalLimit(User user);
}