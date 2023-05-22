namespace Link.Slicer.Application.Models
{
    /// <summary>
    /// A  class for a reponse of creating an url.
    /// </summary>
    /// <param name="ShortUrl">Short url.</param>
    /// <param name="Comment">Comment.</param>
    /// <param name="CreatedAt">Date and time of url created.</param>
    public record CreateUrlCommandResponse(string ShortUrl, string Comment, DateTimeOffset CreatedAt)
    {
    }
}
