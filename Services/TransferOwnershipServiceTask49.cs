namespace JoVisionBackendTasks.Services
{
    public class TransferOwnershipServiceTask49
    {
        public IResult TransferOwnership(string uploadDirectory, string? oldOwner, string? newOwner)
        {
            if (string.IsNullOrEmpty(oldOwner) || string.IsNullOrEmpty(newOwner))
                return Results.BadRequest("Invalid or missing data.");

            Func<string, bool> MatchesOldOwner = (file) =>
            {
                try
                {
                    var metadata = System.Text.Json.JsonSerializer.Deserialize<MetadataModel>(File.ReadAllText(file));
                    return metadata?.Owner == oldOwner;
                }
                catch
                {
                    return false;
                }
            };

            Func<string, bool> MatchesNewOwner = (file) =>
            {
                try
                {
                    var metadata = System.Text.Json.JsonSerializer.Deserialize<MetadataModel>(File.ReadAllText(file));
                    return metadata?.Owner == newOwner;
                }
                catch
                {
                    return false;
                }
            };

            Action<string> Transfer = async (file) => {
                try
                {
                    var metadata = System.Text.Json.JsonSerializer.Deserialize<MetadataModel> (File.ReadAllText(file));
                    metadata.Owner = newOwner;
                    metadata.LastModifiedAt = DateTime.UtcNow;
                    await File
                    .WriteAllTextAsync(
                        file,
                        System.Text.Json
                        .JsonSerializer.Serialize(metadata)
                    );
                    return;
                } catch
                {
                    return;
                }
            };

            try
            {
                var files = Directory.GetFiles(uploadDirectory)
                    .Where(MatchesOldOwner)
                    .ToList();

                files.ForEach(Transfer);

                var newOwnerFiles = Directory.GetFiles(uploadDirectory)
                    .Where(MatchesNewOwner)
                    .ToList();

                return Results.Ok(newOwnerFiles);
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(ex.Message);
            }
        }
    }
}
