namespace JoVisionBackendTasks.Services
{
    public class RetreiveServiceTask48
    {
        public IResult Retreive(string uploadDirectory, string? fileOwner, string? fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileOwner) || string.IsNullOrEmpty(fileName))
                    return Results.BadRequest("Invalid or missing data.");

                string filePath = Path.Combine(uploadDirectory, fileName);

                if (!File.Exists(filePath))
                    return Results.BadRequest("File does not exist.");

                string metadataPath = Path.ChangeExtension(filePath, ".json");

                if (!File.Exists(metadataPath))
                    return Results.BadRequest("Metadata does not exist while its file exists");

                var metadata = System.Text.Json.JsonSerializer.Deserialize<MetadataModel>(File.ReadAllText(metadataPath));

                if (metadata == null)
                    return Results.BadRequest("Metadata does not exist while its file exists");

                if (!metadata.Owner.Equals(fileOwner, StringComparison.OrdinalIgnoreCase))
                    return Results.Unauthorized();

                var fileBytes = File.ReadAllBytes(filePath);
                const string contentType = "image/jpeg";

                return Results.File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(ex.Message);
            }
        }
    }
}
