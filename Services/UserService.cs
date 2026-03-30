using UniEquipmentRental.Models;
namespace UniEquipmentRental.Services;

public class UserService
{
    private readonly List<User> _users;

    public UserService()
    {
        _users = new List<User>();
    }

    public void AddUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        if (user is Student student &&
            _users.OfType<Student>().Any(s => s.Index == student.Index))
        {
            throw new InvalidOperationException("A student with this index already exists.");
        }

        _users.Add(user);
    }

    public IReadOnlyList<User> GetAllUsers()
    {
        return _users.AsReadOnly();
    }

    public User GetUserById(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);

        if (user is null)
            throw new InvalidOperationException($"User with ID {id} was not found.");

        return user;
    }
}