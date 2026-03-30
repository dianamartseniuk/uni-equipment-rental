using UniEquipmentRental.Interfaces;
using UniEquipmentRental.Models;

namespace UniEquipmentRental.Policies;

public class DefaultUserLimitPolicy : IUserLimitPolicy
{
    public int GetRentalLimit(User user)
    {
        if (user is Student)
            return 2;

        if (user is Employee)
            return 5;

        throw new InvalidOperationException("Unknown user type.");
    }
}