using CAS.Models.DTO;

namespace service.Interfaces
{
    public interface IResponseService
    {
        CustomResponse Error(int statusCode, string message);
        CustomResponse Ok(dynamic content);
        CustomResponse Ok();
        CustomResponse UnknownError(string trace = "", Exception exception = null);
    }
}
