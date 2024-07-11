using Abp.Domain.Entities;
using CAS.Models.DTO;

namespace CAS.Models.DBEntities
{
    public class UserEntity : Entity, IEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public UserEntity(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
        }

        public UserEntity()
        {

        }

        public UserEntityDTO ToDTO()
        {
            return new UserEntityDTO
            {
                UserName = UserName,
                Password = Password,
                Email = Email
            };
        }
    }
}
