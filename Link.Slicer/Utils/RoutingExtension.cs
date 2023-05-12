namespace Link.Slicer.Utils
{
    public static class RoutingExtension
    {
        public static void UseRoutingEnpoints(this WebApplication app)
        {
            app.UseEndpoints(endpoint =>
            {
                // Применить Route Constraints, когда будут определены виды паттернов.
                endpoint.MapControllerRoute
                (
                    name: "redirect_incoming_url",
                    pattern: "/{shortening}",
                    defaults: new
                    {
                        controller = "UrlController",
                        action = "Redirect"
                    }
                );

                endpoint.MapControllers();
            });
        }

        // Route templates define the structure of known URLs in your applion.
        // They're strings with placeholders for variables that can contain optional values and map to controllers and actions.

        // A segment is a small contiguous section of a URL. It’s separated
        // from other URL segments by at least one character, often by the / character.
        // Routing involves matching the segments of a URL to a route template.

        // It’s important to consider the order of your conventional routes. You
        // should list the most specific routes first, in order of decreasing specificity,
        // until you get to the most broad/ general route.

        // WARNING: Don’t use route constraints to validate general input, for example
        //          to check that an email address is valid.Doing so will result in 404 “Page not
        //          found” errors, which will be confusing for the user.
    }
}
