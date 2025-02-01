using JoVisionBackendTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Controllers
{
    public class CreateControllerTask47 : Controller
    {
        CreateServiceTask47 serviceTask47 = new();
        public async Task<IResult> Create(
            HttpContext context,
            string uploadDirectory
            )
        {
            var form = await context.Request.ReadFormAsync();
            var owner = form["owner"].ToString();
            var file = form.Files["image"];

            return serviceTask47.Create(uploadDirectory, owner, file).Result;
        }
    }
}
