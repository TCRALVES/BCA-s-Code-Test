using CAS.Models.DTO;
using service.Interfaces;

namespace service.Implementations
{
    public class ResponseService : IResponseService
    {
        public CustomResponse Ok(dynamic content)
        {
            return new CustomResponse
            {
                ResponseMessage = "[200]: OK",
                StatusCode = 200,
                ResponseContent = content
            };
        }

        public CustomResponse Ok()
        {
            return new CustomResponse
            {
                ResponseMessage = "[200]: OK",
                StatusCode = 200
            };
        }

        public CustomResponse Error(int statusCode, string message)
        {
            return new CustomResponse
            {
                ResponseMessage = message,
                StatusCode = statusCode
            };
        }

        public CustomResponse UnknownError(string trace = "", Exception exception = null)
        {
            return new CustomResponse
            {
                ResponseMessage = "[500?]: Unknown error occcured in " + trace,
                StatusCode = 500,
                Exception = exception
            };
        }
    }
}
