namespace CXMDIRECT.Models
{
    public readonly struct Node
    {
        public Node(int id, int parentId, string? name, string? description)
        {
            Id = id;
            ParentId = parentId;
            Name = name;
            Description = description;
        }
        public int Id { get; }
        public int ParentId { get; }
        public string? Name { get; }
        public string? Description { get; }
    }
}
