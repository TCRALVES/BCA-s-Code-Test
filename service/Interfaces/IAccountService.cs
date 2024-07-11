using CAS.Models.DBEntities;
using CAS.Models.DTO;

namespace service.Interfaces
{
    public interface IAccountService
    {
        Task<UserEntity> CreateAccount(UserEntityDTO user);
        Task<UserEntity> GetAccountByEmail(string email);
        Task<UserEntity> GetAccountById(int Id);
    }
}
