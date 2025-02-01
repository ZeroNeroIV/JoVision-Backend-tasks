using JoVisionBackendTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Controllers
{
    public class GreetingControllerTask45 : Controller
    {
        GreetingServiceTask45 serviceTask45 = new();
        public IResult Greeting(HttpContext context, string? name)
        {
            string greeting = serviceTask45.Greeting(name);
            return Results.Ok(greeting);
        }
    }
}
