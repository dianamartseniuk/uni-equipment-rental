using UniEquipmentRental.Models;

namespace UniEquipmentRental.Services;

public class RentalService
{
    private readonly List<Rental> _rentals;
    private readonly UserService _userService;
    private readonly EquipmentService _equipmentService;

    public RentalService(UserService userService, EquipmentService equipmentService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
        _rentals = new List<Rental>();
    }
}