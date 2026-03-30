namespace UniEquipmentRental.Models;

public abstract class Equipment
{
    public int Id { get; }
    private static int LastId;
    public string Name { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public EquipmentStatus Status { get; protected set; }

    public Equipment (string name, string brand, string model)
    {
        LastId++;
        Id = LastId;
        Name = name;
        Brand = brand;
        Model = model;
        Status = EquipmentStatus.Available;
    }

    public void MarkAsBorrowed()
    {
        if (Status != EquipmentStatus.Available)
            throw new InvalidOperationException("Only available equipment can be borrowed.");

        Status = EquipmentStatus.Borrowed;
    }

    public void MarkAsAvailable()
    {
        if (Status == EquipmentStatus.Available)
            throw new InvalidOperationException("The equipment is already available.");

        Status = EquipmentStatus.Available;
    }

    public void MarkAsUnavailable()
    {
        if (Status == EquipmentStatus.Borrowed)
            throw new InvalidOperationException("Borrowed equipment cannot be marked as unavailable.");

        if (Status == EquipmentStatus.Unavailable)
            throw new InvalidOperationException("The equipment is already unavailable.");

        Status = EquipmentStatus.Unavailable;
    }
}