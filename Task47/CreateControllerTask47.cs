using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackendTasks.Task47 {
    public class CreateControllerTask47 : Controller {
        public async Task<IResult> Create(
            HttpContext context, 
            string uploadDirectory
            ) {
            try {
                var form = await context.Request.ReadFormAsync();
                var owner = form["owner"].ToString();
                var file = form.Files["image"];

                if (file == null || string.IsNullOrEmpty(owner))
                    return Results.BadRequest("Invalid or missing data");

                if (Path.GetExtension(file.FileName).ToLower() != ".jpg")
                    return Results.BadRequest("Invalid image type.");

                string filePath = Path.Combine(uploadDirectory, file.FileName);
                string metadataPath = Path.ChangeExtension(filePath, ".json");

                if (System.IO.File.Exists(filePath))
                    return Results.BadRequest("File already exists.");

                using (var stream = new FileStream(filePath, FileMode.Create)) {
                    await file.CopyToAsync(stream);
                }

                var metadata = new
                {
                    Owner = owner,
                    CreateAt = DateTime.UtcNow,
                    LastModifiedAt = DateTime.UtcNow,
                };

                await System.IO.File
                    .WriteAllTextAsync(
                        metadataPath,
                        System.Text.Json
                        .JsonSerializer
                        .Serialize(metadata));

                return Results.Created();
            } catch (Exception ex) {
                return Results.InternalServerError(ex.Message);
            }
        }
    }
}
