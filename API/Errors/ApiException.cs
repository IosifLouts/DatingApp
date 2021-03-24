namespace API.Errors
{  
    //this is what we are gonna return for every exception
    public class ApiException 
    {
        public ApiException(int statusCode, string messages = null, string details = null)
        {
            StatusCode = statusCode; //we always need a status code. For an exception it will always be 500.
            Messages = messages;
            Details = details;
        }

        public int StatusCode {get; set;}
        public string Messages { get; set; }
        public string Details { get; set; } //this is gonna be the stacktrace that we get
    }
}