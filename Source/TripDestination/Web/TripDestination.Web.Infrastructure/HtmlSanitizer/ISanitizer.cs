namespace TripDestination.Web.Infrastructure.HtmlSanitizer
{
    public interface ISanitizer
    {
        string Sanitize(string html);
    }
}
