namespace Link.Slicer.Models
{
    public class UrlCreateResponse
    {
        public string ShortUrl { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
