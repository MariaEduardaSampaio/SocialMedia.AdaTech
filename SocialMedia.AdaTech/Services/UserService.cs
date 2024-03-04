using Azure.Core;
using Domain.Entities;
using Domain.Interfaces;

namespace SocialMedia.AdaTech.Services
{
    public class UserService : IUserService
    {
        public IRepository _repository {  get; set; }
        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public User AddUser(UserRequest request)
        {
            var user = _repository.AddUser(request);
            return user;
        }

        public User ReadUserByName(string name)
        {
            var user = _repository.ReadUserByName(name);
            if (user is not null)
                return user;
            else
                throw new ArgumentException("Não existe usuário com este nome.");
        }

        public User ReadUserByEmail(string email)
        {
            var user = _repository.ReadUserByEmail(email);
            if (user is not null)
                return user;
            else
                throw new ArgumentException("Não existe usuário com este email.");
        }

        public User ReadUserByID(int id)
        {
            var user = _repository.ReadUserByID(id);
            if (user is not null)
                return user;
            else
                throw new ArgumentException("Não existe usuário com este ID.");
        }

        public IEnumerable<User> ReadAllUsers()
        {
            var users = _repository.ReadAllUsers();
            if (users.Any())
                return users;
            else
                throw new ArgumentException("Não existem usuários registrados.");
        }

        public User UpdateUser(UserRequest request, int id)
        {
            var user = _repository.UpdateUser(request, id);
            if (user is not null)
                return user;
            else
                throw new ArgumentException("Não existe usuário com este ID para salvar alterações.");
        }

        public User DeleteUser(int id)
        {
            var user = _repository.DeleteUser(id);
            if (user is not null)
                return user;
            else
                throw new ArgumentException("Não existe usuário com este ID para ser removido.");
        }
    }
}
