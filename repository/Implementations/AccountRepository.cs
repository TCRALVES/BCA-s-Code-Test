using CAS.Models.DBEntities;
using Microsoft.EntityFrameworkCore;
using repository.BaseClasses;
using repository.Interfaces;

namespace repository.Implementations
{
    public class AccountRepository : RepositoryBase<UserEntity>, IRepository<UserEntity>, IAccountRepository
    {
        private readonly DbSet<UserEntity> _db;

        public AccountRepository(CASContext context) : base(context)
        {
            _db = context.Set<UserEntity>();
        }

        public async Task<UserEntity?> GetUserByEmailAsync(string email)
            => await _db.Where(x => x.Email == email).FirstOrDefaultAsync();
    }
}
