using JoVisionBackendTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Controllers
{
    public class DeleteControllerTask47 : Controller
    {
        DeleteServiceTask47 serviceTask47 = new();
        public IResult Delete(
            HttpContext context,
            string uploadDirectory,
            string? fileName,
            string? fileOwner
        )
        {
            return serviceTask47.Delete(uploadDirectory, fileName, fileOwner);
        }
    }
}
