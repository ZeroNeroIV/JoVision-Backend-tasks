using System.Runtime.Serialization;

namespace JoVisionBackendTasks.Services
{
    public class FilterServiceTask49
    {
        enum FilterType
        {
            ByModificationDate,
            ByCreationDateDescending,
            ByCreationDateAscending,
            ByOwner,
        };
        public IResult Filter(string uploadDirectory, string? creationDate, string? modificationDate, string? owner, string? filterTypeString)
        {
            try
            {
                FilterType? filterType = DetectFilterType(filterTypeString);

                switch (filterType)
                {
                    case FilterType.ByModificationDate:
                        return Results.Ok(FilterByModificationDate(uploadDirectory, modificationDate));
                    case FilterType.ByCreationDateDescending:
                        return Results.Ok(FilterByCreationDate(uploadDirectory, creationDate, descending: true));
                    case FilterType.ByCreationDateAscending:
                        return Results.Ok(FilterByCreationDate(uploadDirectory, creationDate));
                    case FilterType.ByOwner:
                        return Results.Ok(FilterByOwner(uploadDirectory, owner));
                    default:
                        return Results.BadRequest("Filter type is invalid.");
                }

            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(ex.Message);
            }
        }

        private List<Object> FilterByModificationDate(string uploadDirectory, string? modificationDateString)
        {
            if (string.IsNullOrEmpty(modificationDateString))
                throw new ArgumentException("Required data are missing.");

            DateTime modificationDate;
            DateTime.TryParse(modificationDateString, out modificationDate);

            Func<string, bool> MatchesModificationDate = (file) =>
            {
                try
                {
                    var metadata = System.Text.Json.JsonSerializer.Deserialize<MetadataModel>(File.ReadAllText(file));
                    DateTime metadataModificationDate = metadata?.LastModifiedAt ?? throw new Exception();

                    return metadataModificationDate < modificationDate;
                }
                catch
                {
                    return false;
                }
            };

            List<string> results = Directory.GetFiles(uploadDirectory)
                .Where(MatchesModificationDate)
                .ToList();

            return CreateFinalResult(results);
        }

        private List<Object> FilterByCreationDate(string uploadDirectory, string? creationDateString, bool descending = false)
        {
            if (string.IsNullOrEmpty(creationDateString))
                throw new ArgumentException("Required data are missing.");

            DateTime creationDate;
            DateTime.TryParse(creationDateString, out creationDate);

            Func<string, bool> MatchesCreationDate = (file) =>
            {
                try
                {

                    var metadata = System.Text.Json.JsonSerializer.Deserialize<MetadataModel>(File.ReadAllText(file));
                    DateTime metadataCreationDate = metadata?.CreatedAt ?? throw new Exception();

                    return metadataCreationDate > creationDate;
                }
                catch
                {
                    return false;
                }
            };


            List<string> results = Directory.GetFiles(uploadDirectory)
                .Where(MatchesCreationDate)
                .ToList();

            Comparison<string> comparison = (file1, file2) =>
            {
                try
                {
                    var metadata1 = System.Text.Json.JsonSerializer.Deserialize<MetadataModel>(File.ReadAllText(Path.ChangeExtension(file1, ".json")));
                    var metadata2 = System.Text.Json.JsonSerializer.Deserialize<MetadataModel>(File.ReadAllText(Path.ChangeExtension(file2, ".json")));

                    DateTime m1 = metadata1?.CreatedAt ?? throw new Exception();
                    DateTime m2 = metadata2?.CreatedAt ?? throw new Exception();

                    int val = 0;
                    if (m1.Equals(m2))
                        val = 0;
                    else if (m1 > m2)
                        val = -1;
                    else
                        val = 1;

                    return (descending ? 1 : -1) * val;
                }
                catch
                {
                    return 0;
                }
            };

            results.Sort(comparison);

            return CreateFinalResult(results);
        }

        private List<Object> FilterByOwner(string uploadDirectory, string? owner)
        {
            if (string.IsNullOrEmpty(owner)) throw new ArgumentException("Required data are missing.");

            Func<string, bool> MatchesOwner = (file) =>
            {
                try
                {
                    var metadata = System.Text.Json.JsonSerializer.Deserialize<MetadataModel>(File.ReadAllText(file));
                    return owner.Equals(metadata?.Owner);
                }
                catch
                {
                    return false;
                }
            };


            List<string> results = Directory.GetFiles(uploadDirectory)
                .Where(MatchesOwner)
                .ToList();

            return CreateFinalResult(results);
        }

        private FilterType? DetectFilterType(string? filterType)
        {
            if (string.IsNullOrEmpty(filterType)) return null;

            switch (filterType.ToLower())
            {
                case "bymodificationdate":
                    return FilterType.ByModificationDate;
                case "bycreationdatedescending":
                    return FilterType.ByCreationDateDescending;
                case "bycreationdateascending":
                    return FilterType.ByCreationDateAscending;
                case "byowner":
                    return FilterType.ByOwner;
            }
            return null;
        }

        private string FormatingDateToString(DateTime date)
        {
            return $"{date.Year}-{date.Month}-{date.Day}";
        }

        private List<Object> CreateFinalResult(List<string> results)
        {
            List<Object> finalResult = new();
            foreach (var result in results)
            {
                if (string.IsNullOrEmpty(result)) continue;
                try
                {
                    var metadata = System.Text.Json.JsonSerializer.Deserialize<MetadataModel>(File.ReadAllText(result));
                    finalResult.Add(new
                    {
                        Owner = metadata?.Owner,
                        FileName = new FileInfo(result).Name,
                    });
                }
                catch
                {
                    throw new Exception("Metadata file does not exist while its file exists.");
                }
            }
            return finalResult;
        }
    }
}