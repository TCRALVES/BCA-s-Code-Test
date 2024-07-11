using CAS.Models.DBEntities;
using CAS.Models.DTO;
using repository.Interfaces;
using service.Interfaces;

namespace service.Implementations
{
    public class AccountService : IAccountService
    {
        public readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserEntity> CreateAccount(UserEntityDTO user)
        {
            if (!HasRequiredFields(user))
                return null;

            return await _repository.AddAsync(user.ToEntity());
        }

        public async Task<UserEntity> GetAccountByEmail(string email)
        {
            return await _repository.GetUserByEmailAsync(email);
        }

        public async Task<UserEntity> GetAccountById(int Id)
        {
            return await _repository.GetAsync(Id);
        }

        private bool HasRequiredFields(UserEntityDTO user)
        {
            return (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(user.UserName));
        }
    }
}
