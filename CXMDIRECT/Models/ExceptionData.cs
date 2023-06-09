namespace CXMDIRECT.Models
{
    public readonly struct ExceptionData
    {
        public ExceptionData(string message)
        {
            Message = message;
        }    
        public string Message { get; }
    }
}
