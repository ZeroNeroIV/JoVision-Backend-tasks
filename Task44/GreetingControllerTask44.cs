namespace JoVisionBackendTasks.Task44
{
    public static class GreetingControllerTask44
    {
        public static IResult Greeting(HttpContext context, string? name)
        {
            string greeting = "Hello ";
            greeting += string.IsNullOrWhiteSpace(name) ? "anonymous" : name.Trim('\"').Trim('\'').Trim();
            return Results.Ok(greeting);
        }
    }
}
