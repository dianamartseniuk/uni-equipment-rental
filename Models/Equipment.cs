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
}