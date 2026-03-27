namespace UniEquipmentRental.Models;

public class Camera : Equipment
{
    public int Megapixels { get;  private set; }
    public string LensType { get; private set; }
    public Camera(string name, int megapixels, string lensType) : base(name)
    {
        Megapixels = megapixels;
        LensType = lensType;
    }
}