namespace JoVisionBackendTasks.Services
{
    public class CreateServiceTask47
    {
        public async Task<IResult> Create(string uploadDirectory, string? owner, IFormFile? file)
        {
            try
            {
                if (file == null || string.IsNullOrEmpty(owner))
                    return Results.BadRequest("Invalid or missing data");

                if (Path.GetExtension(file.FileName).ToLower() != ".jpg")
                    return Results.BadRequest("Invalid image type.");

                string filePath = Path.Combine(uploadDirectory, file.FileName);
                string metadataPath = Path.ChangeExtension(filePath, ".json");

                if (File.Exists(filePath))
                    return Results.BadRequest("File already exists.");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var metadata = new MetadataModel
                {
                    Owner = owner,
                    CreatedAt = DateTime.Now,
                    LastModifiedAt = DateTime.Now,
                };

                await File
                    .WriteAllTextAsync(
                        metadataPath,
                        System.Text.Json
                        .JsonSerializer
                        .Serialize(metadata));

                return Results.Created();
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(ex.Message);
            }
        }
    }
}
