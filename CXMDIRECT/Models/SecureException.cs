namespace CXMDIRECT.Models
{
    public class SecureException : System.Exception
    {
        public SecureException() { }
        public SecureException(string message) : base(message) { }
        public SecureException(string message, System.Exception inner) : base(message, inner) { }
    }
}
