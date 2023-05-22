namespace Link.Slicer.Application.Settings
{
    /// <summary>
    /// Application settings
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Connection strings
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; set; }
        /// <summary>
        /// Domain address
        /// </summary>
        public string DefaultDomain { get; set; }
    }
}
