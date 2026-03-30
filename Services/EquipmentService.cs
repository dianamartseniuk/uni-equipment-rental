using UniEquipmentRental.Models;

namespace UniEquipmentRental.Services;

public class EquipmentService
{
    private readonly List<Equipment> _equipmentItems;

    public EquipmentService()
    {
        _equipmentItems = new List<Equipment>();
    }

    public void AddEquipment(Equipment equipment)
    {
        ArgumentNullException.ThrowIfNull(equipment);
        
        _equipmentItems.Add(equipment);
    }

    public IReadOnlyList<Equipment> GetAllEquipment()
    {
        return _equipmentItems.AsReadOnly();
    }

    public IReadOnlyList<Equipment> GetAvailableEquipment()
    {
        return _equipmentItems
            .Where(e => e.Status == EquipmentStatus.Available)
            .ToList()
            .AsReadOnly();
    }

    public Equipment GetEquipmentById(int id)
    {
        var equipment = _equipmentItems.FirstOrDefault(e => e.Id == id);

        ArgumentNullException.ThrowIfNull(equipment);

        return equipment;
    }

    public void MarkEquipmentAsUnavailable(int id)
    {
        var equipment = GetEquipmentById(id);
        equipment.MarkAsUnavailable();
    }
}