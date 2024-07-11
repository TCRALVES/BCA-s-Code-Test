namespace CAS.Models.DTO
{
    public class CustomResponse
    {
        public int StatusCode { get; set; }

        public dynamic ResponseContent { get; set; }

        public string ResponseMessage { get; set; }

        public Exception Exception { get; set; }

        public CustomResponse(int statusCode, dynamic responseContent, string responseMessage, Exception exception)
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
            ResponseMessage = responseMessage;
            Exception = exception;
        }

        public CustomResponse()
        {
        }
    }
}
