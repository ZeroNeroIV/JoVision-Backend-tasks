namespace JoVisionBackendTasks.Services
{
    public class DeleteServiceTask47
    {
        public IResult Delete(string uploadDirectory, string? fileName, string? fileOwner)
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

                if (!File.Exists(filePath))
                    return Results.BadRequest("File does not exist.");

                string metadataPath = Path.ChangeExtension(filePath, ".json");

                var metadata = System.Text.Json
                    .JsonSerializer
                    .Deserialize<MetadataModel>(
                        File
                        .ReadAllText(metadataPath)
                    ) ??
                    throw new Exception("Metadata not found while file exists.");

                if (!fileOwner.Equals(metadata.Owner, StringComparison.OrdinalIgnoreCase))
                    return Results.Unauthorized();

                File.Delete(filePath);
                File.Delete(metadataPath);

                return Results.Ok("Deleted!");
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(ex.Message);
            }
        }
    }
}
