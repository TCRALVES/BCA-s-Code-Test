using CAS.Models.DBEntities;

namespace CAS.Models.DTO
{
    public class UserEntityDTO
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public UserEntityDTO(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
        }

        public UserEntityDTO()
        {
        }

        public UserEntity ToEntity()
        {
            return new UserEntity
            {
                UserName = UserName,
                Password = Password,
                Email = Email
            };
        }
    }
}
