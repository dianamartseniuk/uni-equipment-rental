namespace UniEquipmentRental.Models;

public class Employee : User
{
    public string Department { get; private set; }
    public Employee(string firstName, string lastName, string department) : base(firstName, lastName)
    {
        Department = department;
    }
    public override string ToString()
    {
        return base.ToString() + $" | Department: {Department}";
    }
}