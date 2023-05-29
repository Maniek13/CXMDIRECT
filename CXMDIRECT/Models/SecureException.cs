namespace CXMDIRECT.Models
{
    public class SecureException : System.Exception
    {
        public SecureException() { }
        public SecureException(string message) : base(message) { }
        public SecureException(string message, Exception inner) : base(message, inner) { }
    }
}
