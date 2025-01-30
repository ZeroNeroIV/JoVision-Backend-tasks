namespace JoVisionBackendTasks.Task46
{
    public static class GreetingControllerTask46
    {
        public static async Task<IResult> Greeting(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();
            string name = form["name"]
                .ToString()
                .Trim([
                    '\'',
                    '\"',
                    ' '
                ]) ?? "anonymous";
            
            string greeting = $"Hello {name}";
            return Results.Ok(greeting);
        }
    }
}
