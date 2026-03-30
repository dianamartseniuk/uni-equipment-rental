using UniEquipmentRental.Models;
using UniEquipmentRental.Interfaces;

namespace UniEquipmentRental.Services;

public class RentalService
{
    private readonly List<Rental> _rentals;
    private readonly UserService _userService;
    private readonly EquipmentService _equipmentService;
    private readonly IUserLimitPolicy _userLimitPolicy;
    private readonly IPenaltyPolicy _penaltyPolicy;

    public RentalService(
        UserService userService,
        EquipmentService equipmentService,
        IUserLimitPolicy userLimitPolicy,
        IPenaltyPolicy penaltyPolicy)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
        _userLimitPolicy = userLimitPolicy ?? throw new ArgumentNullException(nameof(userLimitPolicy));
        _penaltyPolicy = penaltyPolicy ?? throw new ArgumentNullException(nameof(penaltyPolicy));
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
        var rentalLimit = _userLimitPolicy.GetRentalLimit(user);
        if (activeRentalCount >= rentalLimit)
            throw new InvalidOperationException("The user has reached the rental limit.");

        var rental = new Rental(user, equipment, numberOfRentalDays);

        equipment.MarkAsBorrowed();
        _rentals.Add(rental);

        return rental;
    }

    public decimal ReturnEquipment(int rentalId)
    {
        var rental = GetRentalById(rentalId);

        if (!rental.IsActive())
            throw new InvalidOperationException("This rental has already been returned.");

        rental.ReturnEquipment();
        rental.Equipment.MarkAsAvailable();

        return _penaltyPolicy.CalculatePenalty(rental);
    }

    public IReadOnlyList<Rental> GetActiveRentalsForUser(int userId)
    {
        _userService.GetUserById(userId);

        return _rentals
            .Where(r => r.User.Id == userId && r.IsActive())
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyList<Rental> GetOverdueRentals()
    {
        return _rentals
            .Where(r => r.IsOverdue())
            .ToList()
            .AsReadOnly();
    }

    private int GetActiveRentalCountForUser(int userId)
    {
        return _rentals.Count(r => r.User.Id == userId && r.IsActive());
    }

    private Rental GetRentalById(int rentalId)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == rentalId);

        if (rental is null)
            throw new InvalidOperationException($"Rental with ID {rentalId} was not found.");

        return rental;
    }
}