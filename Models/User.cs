namespace UniEquipmentRental.Models;
public abstract class User
{
    public int Id { get; }
    private static int LastId;
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public User(string firstName, string lastName)
    {
        LastId++;
        Id = LastId;
        FirstName = firstName;
        LastName = lastName;
    }
    public override string ToString()
    {
        return $"{Id}: {FirstName} {LastName}";
    }
}