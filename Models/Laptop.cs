namespace UniEquipmentRental.Models;

public class Laptop : Equipment
{
    public OperatingSystem OperatingSystem { get; private set; }
    public int RamGB { get; private set; }
    public string Processor { get; private set; }
    public Laptop(string name, OperatingSystem operatingSystem, int ramGB, string processor) : base(name)
    {
        OperatingSystem = operatingSystem;
        RamGB = ramGB;
        Processor = processor;
    }
}