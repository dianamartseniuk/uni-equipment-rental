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

    public IReadOnlyList<Rental> GetAllRentals()
    {
        return _rentals.AsReadOnly();
    }

    public Rental BorrowEquipment(int userId, int equipmentId, int numberOfRentalDays)
    {
        if (numberOfRentalDays <= 0)
            throw new ArgumentException("Number of rental days must be greater than 0.", nameof(numberOfRentalDays));

        var user = _userService.GetUserById(userId);
        var equipment = _equipmentService.GetEquipmentById(equipmentId);

        var rental = new Rental(user, equipment, numberOfRentalDays);

        _rentals.Add(rental);

        return rental;
    }
}