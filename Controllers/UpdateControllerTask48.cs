using JoVisionBackendTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Controllers
{
    public class UpdateControllerTask48 : Controller
    {
        UpdateServiceTask48 serviceTask48 = new();
        public async Task<IResult> Update(HttpContext context, string uploadDirectory)
        {
            var form = await context.Request.ReadFormAsync();
            var image = form.Files["image"];
            var owner = form["owner"].ToString();
            return serviceTask48.Update(uploadDirectory, owner, image).Result;
        }
    }
}
