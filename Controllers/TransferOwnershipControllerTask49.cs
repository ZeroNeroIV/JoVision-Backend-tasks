using JoVisionBackendTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Controllers
{
    public class TransferOwnershipControllerTask49 : Controller
    {
        TransferOwnershipServiceTask49 serviceTask49 = new();
        public IResult TransferOwnership(HttpContext context, string uploadDirectory, string? oldOwner, string? newOwner)
        {
            return serviceTask49.TransferOwnership(uploadDirectory, oldOwner, newOwner);
        }
    }
}
