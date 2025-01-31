using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Task45
{
    public class GreetingControllerTask45 : Controller
    {
        public IResult Greeting(HttpContext context, string? name)
        {
            string greeting = "Hello ";
            greeting += string.IsNullOrWhiteSpace(name) ? "anonymous" : name.Trim('\"').Trim('\'').Trim();
            return Results.Ok(greeting);
        }
    }
}
