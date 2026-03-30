using System.Text;
using UniEquipmentRental.Models;

namespace UniEquipmentRental.Services;

public class ReportService
{
    private readonly UserService _userService;
    private readonly EquipmentService _equipmentService;
    private readonly RentalService _rentalService;

    public ReportService(
        UserService userService,
        EquipmentService equipmentService,
        RentalService rentalService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
        _rentalService = rentalService ?? throw new ArgumentNullException(nameof(rentalService));
    }

    public string GenerateSummaryReport()
    {
        var allUsers = _userService.GetAllUsers();
        var allEquipment = _equipmentService.GetAllEquipment();
        var allRentals = _rentalService.GetAllRentals();
        var availableEquipment = _equipmentService.GetAvailableEquipment();
        var overdueRentals = _rentalService.GetOverdueRentals();

        int borrowedEquipmentCount = allEquipment.Count(e => e.Status == EquipmentStatus.Borrowed);
        int unavailableEquipmentCount = allEquipment.Count(e => e.Status == EquipmentStatus.Unavailable);
        int activeRentalsCount = allRentals.Count(r => r.IsActive());

        var sb = new StringBuilder();

        sb.AppendLine("=== RENTAL SYSTEM SUMMARY REPORT ===");
        sb.AppendLine($"Total users: {allUsers.Count}");
        sb.AppendLine($"Total equipment items: {allEquipment.Count}");
        sb.AppendLine($"Available equipment: {availableEquipment.Count}");
        sb.AppendLine($"Borrowed equipment: {borrowedEquipmentCount}");
        sb.AppendLine($"Unavailable equipment: {unavailableEquipmentCount}");
        sb.AppendLine($"Total rentals: {allRentals.Count}");
        sb.AppendLine($"Active rentals: {activeRentalsCount}");
        sb.AppendLine($"Overdue rentals: {overdueRentals.Count}");

        return sb.ToString();
    }
}