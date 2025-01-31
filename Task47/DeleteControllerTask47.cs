using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Task47
{
    public class DeleteControllerTask47 : Controller
    {
        public IResult Delete(
            HttpContext context,
            string uploadDirectory,
            string? fileName,
            string? fileOwner
        )
        {
            try
            {
                if (
                    fileName == null ||
                    string.IsNullOrEmpty(fileName) ||
                    fileOwner == null ||
                    string.IsNullOrEmpty(fileOwner)
                    )
                    return Results.BadRequest("Invalid or missing data.");

                string filePath = Path.Combine(uploadDirectory, fileName);

                if (!System.IO.File.Exists(filePath))
                    return Results.BadRequest("File does not exist.");

                string metadataPath = Path.ChangeExtension(filePath, ".json");

                var metadata = System.Text.Json
                    .JsonSerializer
                    .Deserialize<Dictionary<string, Object>>(
                        System.IO.File
                        .ReadAllText(metadataPath)
                    ) ??
                    throw new Exception("Metadata not found while file exists.");
                if (!fileOwner.Equals(metadata["Owner"].ToString(), StringComparison.OrdinalIgnoreCase))
                    return Results.Unauthorized();

                System.IO.File.Delete(filePath);
                System.IO.File.Delete(metadataPath);

                return Results.Ok("Deleted!");
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(ex.Message);
            }
        }
    }
}
