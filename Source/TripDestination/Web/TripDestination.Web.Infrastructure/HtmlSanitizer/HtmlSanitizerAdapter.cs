﻿namespace TripDestination.Web.Infrastructure.HtmlSanitizer
{
    using Ganss.XSS;


    public class HtmlSanitizerAdapter : ISanitizer
    {
        public string Sanitize(string html)
        {
            var sanitizer = new HtmlSanitizer();
            var result = sanitizer.Sanitize(html);
            return result;
        }
    }
}
