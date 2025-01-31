using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Task44
{
    public class GreetingControllerTask44 : Controller
    {
        public IResult Greeting(HttpContext context, string? name)
        {
            string greeting = "Hello ";
            greeting += string.IsNullOrWhiteSpace(name) ? "anonymous" : name.Trim('\"').Trim('\'').Trim();
            return Results.Ok(greeting);
        }
    }
}
