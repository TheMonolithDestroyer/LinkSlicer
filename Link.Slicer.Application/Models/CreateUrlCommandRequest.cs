namespace Link.Slicer.Application.Models
{
    /// <summary>
    /// A class for a request of creating an url.
    /// </summary>
    /// <param name="Shortening">A shortening.</param>
    /// <param name="Url">An url./</param>
    /// <param name="Comment">A comment.</param>
    public record CreateUrlCommandRequest(string Shortening, string Url, string Comment)
    {
        
    }
}
