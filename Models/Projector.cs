namespace UniEquipmentRental.Models;

public class Projector : Equipment
{
    public int BrightnessLumens { get; private set; }
    public string Resolution { get; private set; }
    public Projector(string name, string brand, string model, int brightnessLumes, string resolution) : base(name, brand, model)
    {
        BrightnessLumens = brightnessLumes;
        Resolution = resolution;
    }

    public override string ToString()
    {
        return base.ToString() + $" | Brightness: {BrightnessLumens} lm | Resolution: {Resolution}";
    }
}