namespace ResourcesManager.Models
{
    public class ImportBatch
    {
        public int Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public DateTime ImportedAt { get; set; } = DateTime.UtcNow;

        public int ImportedByUserId { get; set; }

        public AppUser ImportedByUser { get; set; } = null!;

        /// <summary>
        /// Sysmac group name used in source file, ex: OBJ
        /// </summary>
        public string SourceGroupName { get; set; } = string.Empty;

        /// <summary>
        /// Pending / Completed / Error / Cancelled
        /// </summary>
        public string Status { get; set; } = "Pending";

        public int TotalRows { get; set; }

        public int AddedResources { get; set; }

        public int AddedTexts { get; set; }

        public int UpdatedTexts { get; set; }

        public int WarningsCount { get; set; }

        public int ErrorsCount { get; set; }

        public string? Comment { get; set; }

        public List<Resource> CreatedResources { get; set; } = new();

        public List<ResourceText> CreatedResourceTexts { get; set; } = new();
    }
}
