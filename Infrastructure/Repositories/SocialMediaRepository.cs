using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class SocialMediaRepository: IRepository
    {
        protected readonly SocialMediaContext _socialMediaContext;

        public SocialMediaRepository(SocialMediaContext socialMediaContext)
        {
            _socialMediaContext = socialMediaContext;
        }

        public User AddUser(User user)
        {
            _socialMediaContext.Users.Add(user);
            _socialMediaContext.SaveChanges();
            return user;
        }

        public User? DeleteUser(int id)
        {
            var user = _socialMediaContext.Users.Find(id);
            _socialMediaContext.Users.Remove(user);
            return user;
        }

        public IEnumerable<User>? ReadAllUsers()
        {
            return _socialMediaContext.Users.ToList();
        }

        public User? ReadUserByEmail(string email)
        {
            return _socialMediaContext.Users.FirstOrDefault(user => user.Email == email);
        }

        public User? ReadUserByID(int id)
        {
            return _socialMediaContext.Users.Find(id);
        }

        public User? ReadUserByName(string name)
        {
            return _socialMediaContext.Users.FirstOrDefault(user => user.Name == name);
        }

        public User? UpdateUser(UserRequest request, int id)
        {
            var user = _socialMediaContext.Users.Find(id);

            if (user is not null)
            {
                user.Name = request.Name ?? user.Name;
                user.Email = request.Email ?? user.Email;
                user.Password = request.Password ?? user.Password;
            }

            _socialMediaContext.SaveChanges();

            return user;
        }
    }
}
