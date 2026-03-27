namespace UniEquipmentRental.Models;

public class Camera : Equipment
{
    public int Megapixels { get;  private set; }
    public string LensType { get; private set; }
    public Camera(string name, string brand, string model, int megapixels, string lensType) : base(name, brand, model)
    {
        Megapixels = megapixels;
        LensType = lensType;
    }
}