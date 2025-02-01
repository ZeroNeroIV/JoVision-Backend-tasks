using JoVisionBackendTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Controllers
{
    public class BirthDateControllerTask46 : Controller
    {
        BirthDateServiceTask46 ServiceTask46 = new();
        public async Task<IResult> BirthDate(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();

            string name = form["name"].ToString() ?? "anonymous";
            int years = int.Parse(form["years"].ToString());
            int months = int.Parse(form["months"].ToString());
            int days = int.Parse(form["days"].ToString());

            string greeting = ServiceTask46.BirthDate(name, years, months, days);

            return Results.Ok(greeting);
        }
    }
}
