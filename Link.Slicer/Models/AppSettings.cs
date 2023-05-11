namespace Link.Slicer.Models
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public string DefaultDomain { get; set; }
    }
    
    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
}
