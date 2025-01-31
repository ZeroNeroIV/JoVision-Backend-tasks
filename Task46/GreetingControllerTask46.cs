using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Task46
{
    public class GreetingControllerTask46 : Controller
    {
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
            
            string greeting = $"Hello {name}";
            return Results.Ok(greeting);
        }
    }
}
