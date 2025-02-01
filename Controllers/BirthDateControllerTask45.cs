using JoVisionBackendTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Controllers
{
    public class BirthDateControllerTask45 : Controller
    {
        BirthDateServiceTask46 serviceTask45 = new();
        public IResult BirthDate(
            HttpContext context,
            string? name,
            int? years,
            int? months,
            int? days)
        {
            string greeting = serviceTask45.BirthDate(name, years, months, days);
            return Results.Ok(greeting);
        }
    }
}
