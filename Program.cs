using UniEquipmentRental.Models;
using UniEquipmentRental.Services;
using UniEquipmentRental.Policies;

var userService = new UserService();
var equipmentService = new EquipmentService();
var userLimitPolicy = new DefaultUserLimitPolicy();
var penaltyPolicy = new DefaultPenaltyPolicy();

var rentalService = new RentalService(
    userService,
    equipmentService,
    userLimitPolicy,
    penaltyPolicy
);
var reportService = new ReportService(userService, equipmentService, rentalService);

try
{
    // USERS
    var student1 = new Student("Anna", "Nowak", "s12345");
    var student2 = new Student("Jan", "Kowalski", "s54321");
    var employee1 = new Employee("Maria", "Wisniewska", "Księgowość");

    userService.AddUser(student1);
    userService.AddUser(student2);
    userService.AddUser(employee1);

    Console.WriteLine("=== USERS ===");
    foreach (var user in userService.GetAllUsers())
    {
        Console.WriteLine(user);
    }

    // EQUIPMENT
    var laptop1 = new Laptop(
        "Dell Latitude",
        "Dell",
        "7420",
        OperatingSystem.Windows,
        16,
        "Intel i7"
    );

    var laptop2 = new Laptop(
        "MacBook Pro",
        "Apple",
        "M2 2023",
        OperatingSystem.macOS,
        16,
        "Apple M2"
    );

    var projector1 = new Projector(
        "Epson Projector",
        "Epson",
        "X1",
        3500,
        "Full HD"
    );

    var camera1 = new Camera(
        "Canon Camera",
        "Canon",
        "EOS 200D",
        24,
        "Zoom"
    );

    equipmentService.AddEquipment(laptop1);
    equipmentService.AddEquipment(laptop2);
    equipmentService.AddEquipment(projector1);
    equipmentService.AddEquipment(camera1);

    Console.WriteLine();
    Console.WriteLine("=== ALL EQUIPMENT ===");
    foreach (var item in equipmentService.GetAllEquipment())
    {
        Console.WriteLine(item);
    }

    // BORROW
    Console.WriteLine();
    Console.WriteLine("=== BORROW EQUIPMENT ===");
    var rental1 = rentalService.BorrowEquipment(student1.Id, laptop1.Id, 7);
    Console.WriteLine(rental1);

    // TRY TO BORROW THE SAME EQUIPMENT AGAIN
    Console.WriteLine();
    Console.WriteLine("=== TRY TO BORROW UNAVAILABLE EQUIPMENT ===");
    try
    {
        rentalService.BorrowEquipment(student2.Id, laptop1.Id, 3);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Expected error: {ex.Message}");
    }

    // ACTIVE RENTALS FOR USER
    Console.WriteLine();
    Console.WriteLine("=== ACTIVE RENTALS FOR STUDENT 1 ===");
    foreach (var rental in rentalService.GetActiveRentalsForUser(student1.Id))
    {
        Console.WriteLine(rental);
    }

    // AVAILABLE EQUIPMENT
    Console.WriteLine();
    Console.WriteLine("=== AVAILABLE EQUIPMENT ===");
    foreach (var item in equipmentService.GetAvailableEquipment())
    {
        Console.WriteLine(item);
    }

    // RETURN
    Console.WriteLine();
    Console.WriteLine("=== RETURN EQUIPMENT ===");
    decimal penalty = rentalService.ReturnEquipment(rental1.Id);
    Console.WriteLine($"Returned rental {rental1.Id}, penalty = {penalty}");

    // MARK AS UNAVAILABLE
    Console.WriteLine();
    Console.WriteLine("=== MARK EQUIPMENT AS UNAVAILABLE ===");
    equipmentService.MarkAsUnavailable(projector1.Id);
    Console.WriteLine($"{projector1.Name} new status: {projector1.Status}");

    // OVERDUE RENTALS
    Console.WriteLine();
    Console.WriteLine("=== OVERDUE RENTALS ===");
    foreach (var rental in rentalService.GetOverdueRentals())
    {
        Console.WriteLine(rental);
    }

    // FINAL EQUIPMENT STATE
    Console.WriteLine();
    Console.WriteLine("=== FINAL EQUIPMENT STATE ===");
    foreach (var item in equipmentService.GetAllEquipment())
    {
        Console.WriteLine(item);
    }

    // REPORT
    Console.WriteLine();
    Console.WriteLine(reportService.GenerateSummaryReport());
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}