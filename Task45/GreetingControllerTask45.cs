namespace JoVisionBackendTasks.Task45
{
    public static class GreetingControllerTask45
    {
        public static IResult Greeting(HttpContext context, string? name)
        {
            string greeting = "Hello ";
            greeting += string.IsNullOrWhiteSpace(name) ? "anonymous" : name.Trim('\"').Trim('\'').Trim();
            return Results.Ok(greeting);
        }
    }
}
