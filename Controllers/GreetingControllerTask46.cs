using JoVisionBackendTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Controllers
{
    public class GreetingControllerTask46 : Controller
    {
        GreetingServiceTask46 serviceTask46 = new();
        public async Task<IResult> Greeting(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();
            string name = form["name"]
                .ToString()
                .Trim([
                    '\'',
                    '\"',
                    ' '
                ]) ?? "anonymous";

            string greeting = serviceTask46.Greeting(name);
            return Results.Ok(greeting);
        }
    }
}
