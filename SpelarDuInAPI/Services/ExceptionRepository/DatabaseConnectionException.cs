namespace SpelarDuInAPI.Services.ExceptionRepository
{
    public class DatabaseConnectionException:Exception
    {  
            public DatabaseConnectionException() { }

        public int StatusCode { get; set; }
        public DatabaseConnectionException(int statusCode, string message) : base(message)
            {
                StatusCode = statusCode;
            }
    }
}
