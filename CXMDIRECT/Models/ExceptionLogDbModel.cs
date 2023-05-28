using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CXMDIRECT.Models
{
    public class ExceptionLogDbModel
    {
        [Key]
        public long Id { get; set; }
        public string? ExtensionType { get; set; }
        public DateTime InstanceData { get; set; }
        public string? Parameters { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
