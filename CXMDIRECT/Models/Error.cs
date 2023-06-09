namespace CXMDIRECT.Models
{
    internal readonly struct Error
    {
        internal Error(string message) 
        {
            Message = message;
        }
        internal string Message { get; }
    }
}
