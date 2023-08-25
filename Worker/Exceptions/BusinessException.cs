using System.Net;

namespace Worker.Exceptions
{
    public class BusinessException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Reason { get; set; }

        public BusinessException(HttpStatusCode statusCode, string reason)
        {
            StatusCode = statusCode;
            Reason = reason;
        }
    }
}
