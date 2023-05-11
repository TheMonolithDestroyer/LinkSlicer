namespace Link.Slicer.Entities
{
    public class Url
    {
        public Guid UrlId { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public long? DeletedAt { get; set; }
        public long? ExpiresIn { get; set; }
        public string Protocol { get; set; }
        public string DomainName { get; set; }
        public string Address { get; set; }
        public string Target { get; set; }
        public string Description { get; set; }
    }
}
