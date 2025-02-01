using JoVisionBackendTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Controllers
{
    public class FilterControllerTask49 : Controller
    {
        FilterServiceTask49 serviceTask49 = new();
        public async Task<IResult> Filter(HttpContext context, string uploadDirectory)
        {
            var form = await context.Request.ReadFormAsync();
            string? creationDate = form["creationDate"].ToString();
            string? modificationDate = form["modificationDate"].ToString();
            string? owner = form["owner"].ToString();
            string? filterTypeString = form["filterType"].ToString();

            return serviceTask49.Filter(uploadDirectory, creationDate, modificationDate, owner, filterTypeString);
        }
    }
}
