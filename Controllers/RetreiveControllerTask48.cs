using JoVisionBackendTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Controllers
{
    public class RetreiveControllerTask48 : Controller
    {
        RetreiveServiceTask48 serviceTask48 = new();
        public IResult Retrieve(
            HttpContext context,
            string uploadDirectory,
            string? fileName,
            string? fileOwner
        )
        {
            return serviceTask48.Retreive(uploadDirectory, fileOwner, fileName);
        }
    }
}
