namespace UniEquipmentRental.Models;

public class Laptop : Equipment
{
    public OperatingSystem OperatingSystem { get; private set; }
    public int RamGB { get; private set; }
    public string Processor { get; private set; }
    public Laptop(string name, string brand, string model, OperatingSystem operatingSystem, int ramGB, string processor) : base(name, brand, model)
    {
        OperatingSystem = operatingSystem;
        RamGB = ramGB;
        Processor = processor;
    }
}