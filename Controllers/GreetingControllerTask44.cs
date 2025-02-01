using JoVisionBackendTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Controllers
{
    public class GreetingControllerTask44 : Controller
    {
        GreetingServiceTask44 serviceTask44 = new();
        public IResult Greeting(HttpContext context, string? name)
        {
            string greeting = serviceTask44.Greeting(name);
            return Results.Ok(greeting);
        }
    }
}
