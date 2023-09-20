using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        Task<User> GetByUserId(string userId, bool trackChanges);
        Task<User> GetByEmail(string email, bool trackChanges);
        Task<User> GetByEmailAndPassword(string email, string password, bool trackChanges);
        Task<List<User>> GetUsers();
    }
}
