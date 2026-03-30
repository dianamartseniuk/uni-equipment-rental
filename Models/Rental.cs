namespace UniEquipmentRental.Models;
public class Rental
{
    public int Id { get; }
    private static int LastId;
    public User User { get; private set; }
    public Equipment Equipment { get; private set; }
    public DateTime RentalDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ActualReturnDate { get; private set; } = null;
    public Rental(User user, Equipment equipment, int numberOfRentalDays)
    {
        LastId++;
        Id = LastId;
        User = user;
        Equipment = equipment;
        RentalDate = DateTime.Now;
        DueDate = RentalDate.AddDays(numberOfRentalDays);
    }

    public bool IsActive() => ActualReturnDate == null;
    public bool IsOverdue() => IsActive() && DueDate.Date < DateTime.Now.Date;
    public bool WasReturnedOnTime() => !IsActive() && DueDate.Date >= ActualReturnDate?.Date;
    public void ReturnEquipment()
    {
        if (IsActive()) ActualReturnDate = DateTime.Now;
        else throw new Exception("Item has been already returned");
    }
}