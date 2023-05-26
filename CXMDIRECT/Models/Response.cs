namespace CXMDIRECT.Models
{
    public class Response<T>
    {
        public string Type { get; set; }
        public long Id { get; set; }
        public T? Data { get; set; }

    }
}
