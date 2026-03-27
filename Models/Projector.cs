namespace UniEquipmentRental.Models;

public class Projector : Equipment
{
    public int BrightnessLumens { get; private set; }
    public string Resolution { get; private set; }
    public Projector(string name, int brightnessLumes, string resolution) : base(name)
    {
        BrightnessLumens = brightnessLumes;
        Resolution = resolution;
    }
}