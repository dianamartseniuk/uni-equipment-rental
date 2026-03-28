namespace UniEquipmentRental.Models;

public class Student : User
{
    public string Index { get; private set; }
    public Student(string firstName, string lastName, string index) : base(firstName, lastName)
    {
        Index = index;
    }
}