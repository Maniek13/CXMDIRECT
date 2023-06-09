namespace CXMDIRECT.Models
{
    public record Response<T>
    {
        public Response(string? type, long id, T? data)
        {
            Type = type;
            Id = id;
            Data = data;
        }
        public string? Type { get; }
        public long Id { get; }
        public T? Data { get; }
    }
}
