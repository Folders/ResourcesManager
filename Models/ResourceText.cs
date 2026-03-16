namespace ResourcesManager.Models
{
    public class ResourceText
    {
        public int Id { get; set; }

        public int ResourceId { get; set; }
        public Resource Resource { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

        /// <summary>
        /// Texte traduit ou source
        /// </summary>
        public string TextValue { get; set; } = string.Empty;

        /// <summary>
        /// Indique si cette version est la traduction préférée
        /// </summary>
        public bool IsPreferred { get; set; }

        /// <summary>
        /// Peut servir plus tard pour Draft / Proposed / Approved
        /// </summary>
        public string Status { get; set; } = "Approved";

        public bool NeedsReview { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? ReplacesTextId { get; set; }
    }
}
