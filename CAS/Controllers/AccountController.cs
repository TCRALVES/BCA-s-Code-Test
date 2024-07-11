namespace CAS.Controllers
{
    using CAS.Models.DTO;
    using Microsoft.AspNetCore.Mvc;
    using service.Interfaces;

    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IResponseService _responseService;

        public AccountController(IAccountService service, IResponseService responseService)
        {
            _accountService = service;
            _responseService = responseService;
        }

        [HttpGet]
        [Route("get-by-email")]
        public async Task<CustomResponse> GetUser(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return _responseService.Error(400, "Bad Request - Email cannot be null or empty.");

                var dbUser = await _accountService.GetAccountByEmail(email);

                if (dbUser is null)
                    return _responseService.Error(404, $"Not Found - User not found with email: {email}");

                return _responseService.Ok(dbUser);
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError("Unknown Error occurred while trying to retrieve user by email.", ex);
                //Implement Logging of exception.
            }
        }

        [HttpPost]
        [Route("create-user")]
        public async Task<CustomResponse> CreateAccount(UserEntityDTO user)
        {
            try
            {
                if (await _accountService.GetAccountByEmail(user.Email) is not null)
                {
                    return _responseService.Error(409, $"Conflict - There is already an user registered with email: {user.Email}");
                }

                var result = await _accountService.CreateAccount(user);

                if (result is null)
                    return _responseService.Error(400, "Bad Request - All fields must be filled out to create an account");

                return _responseService.Ok(result);
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError("Unknown Error occurred while trying to Create an Account", ex);
            }
        }
    }
}
