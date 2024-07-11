using CAS.Models.DBEntities;

namespace repository.Interfaces
{
    public interface IAccountRepository : IRepository<UserEntity>
    {
        Task<UserEntity?> GetUserByEmailAsync(string email);
    }
}
