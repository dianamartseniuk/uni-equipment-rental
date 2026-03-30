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

        if (equipment.Status != EquipmentStatus.Available)
            throw new InvalidOperationException("This equipment is not available for rental.");

        var activeRentalCount = GetActiveRentalCountForUser(user.Id);
        var rentalLimit = GetUserRentalLimit(user);

        if (activeRentalCount >= rentalLimit)
            throw new InvalidOperationException("The user has reached the rental limit.");

        var rental = new Rental(user, equipment, numberOfRentalDays);

        equipment.MarkAsBorrowed();
        _rentals.Add(rental);

        return rental;
    }

    private int GetActiveRentalCountForUser(int userId)
    {
        return _rentals.Count(r => r.User.Id == userId && r.IsActive());
    }

    private int GetUserRentalLimit(User user)
    {
        if (user is Student)
            return 2;

        if (user is Employee)
            return 5;

        throw new InvalidOperationException("Unknown user type.");
    }

    private Rental GetRentalById(int rentalId)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == rentalId);

        if (rental is null)
            throw new InvalidOperationException($"Rental with ID {rentalId} was not found.");

        return rental;
    }

    private decimal CalculatePenalty(Rental rental)
    {
        if (!rental.IsActive())
            return 0;

        if (rental.WasReturnedOnTime())
            return 0;

        var daysLate = (rental.ActualReturnDate!.Value.Date - rental.DueDate.Date).Days;
        return daysLate * 10m;
    }
}