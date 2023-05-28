using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CXMDIRECT.Models
{
    public class NodeDbModel
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}

