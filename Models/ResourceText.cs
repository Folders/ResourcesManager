namespace ResourcesManager.Models
{
    public class ResourceText
    {
        public int Id { get; set; }

        public int ResourceId { get; set; }

        public Resource Resource { get; set; } = null!;

        public int LanguageId { get; set; }

        public Language Language { get; set; } = null!;

        public string TextValue { get; set; } = string.Empty;

        /// <summary>
        /// Proposed / Approved / Rejected
        /// </summary>
        public string Status { get; set; } = "Approved";

        /// <summary>
        /// True if this is the preferred/current text for this resource and language.
        /// </summary>
        public bool IsPreferred { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CreatedByUserId { get; set; }

        public AppUser CreatedByUser { get; set; } = null!;

        public int? CreatedImportBatchId { get; set; }

        public ImportBatch? CreatedImportBatch { get; set; }

        /// <summary>
        /// Number of times this exact text has been seen in imports.
        /// </summary>
        public int SeenCount { get; set; } = 1;

        public bool NeedsReview { get; set; }

        /// <summary>
        /// Previous text replaced by this one, if any.
        /// </summary>
        public int? ReplacesTextId { get; set; }

        public ResourceText? ReplacesText { get; set; }

        public List<ResourceText> ReplacedByTexts { get; set; } = new();

    }
}
