using System.ComponentModel.DataAnnotations;

namespace CXMDIRECT.Models
{
    public class ExceptionLog
    {
        public long Id { get; set; }
        public string? ExtensionType { get; set; }
        public DateTime InstanceDate { get; set; }
        public string? Parameters { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
