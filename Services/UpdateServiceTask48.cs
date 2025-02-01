namespace JoVisionBackendTasks.Services
{
    public class UpdateServiceTask48
    {
        public async Task<IResult> Update(string uploadDirectory, string? owner, IFormFile? image)
        {
            try
            {
                if (image == null || owner == null || string.IsNullOrEmpty(owner))
                    return Results.BadRequest("Invalid or missing data.");

                string filePath = Path.Combine(uploadDirectory, image.FileName);
                string metadataPath = Path.ChangeExtension(filePath, ".json");

                if (!File.Exists(filePath))
                    return Results.BadRequest("File does not exist.");

                var metadata = System.Text.Json.JsonSerializer
                    .Deserialize<MetadataModel>(File.ReadAllText(metadataPath))
                    ?? throw new Exception("Metadata file is not found while the file exists.");

                if (!owner.Equals(metadata.Owner, StringComparison.OrdinalIgnoreCase))
                    return Results.Unauthorized();

                using (FileStream stream = new(filePath, FileMode.Truncate))
                {
                    await image.CopyToAsync(stream);
                }

                metadata.LastModifiedAt = DateTime.UtcNow;

                await File
                    .WriteAllTextAsync(
                        metadataPath,
                        System.Text.Json
                        .JsonSerializer.Serialize(metadata)
                    );

                return Results.Ok("Updated!");
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(ex.Message);
            }
        }
    }
}
