namespace CXMDIRECT.Models
{
    internal readonly struct ExceptionLog
    {
        public ExceptionLog(long id, string extensionType, DateTime? instanceDate, string? parameters, string message, string? stackTrace)
        { 
            Id = id;
            ExtensionType = extensionType;
            InstanceDate = instanceDate;
            Parameters = parameters;
            Message = message;
            StackTrace = stackTrace;
        }
        internal long Id { get; }
        internal string ExtensionType { get; }
        internal DateTime? InstanceDate { get; }
        internal string? Parameters { get; }
        internal string Message { get; }
        internal string? StackTrace { get; }
    }
}
