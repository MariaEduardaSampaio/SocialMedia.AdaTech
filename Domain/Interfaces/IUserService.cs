using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserService
    {

        User AddUser(UserRequest request);
        User ReadUserByName(string name);
        User ReadUserByEmail(string email);
        User ReadUserByID(int id);
        IEnumerable<User> ReadAllUsers();
        User UpdateUser(UserRequest request, int id);
        User DeleteUser(int id);
    }
}
