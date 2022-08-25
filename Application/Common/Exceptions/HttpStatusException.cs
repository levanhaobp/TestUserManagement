namespace Application.Common.Exceptions
{
    public class HttpStatusException : Exception
    {
        public int StatusCode { get; private set; }

        public HttpStatusException(string msg, int statusCode = 501) : base(msg)
        {
            StatusCode = statusCode;
        }
    }
}
