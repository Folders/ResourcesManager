namespace ResourcesManager.Models
{
    public class Resource
    {
        public int Id { get; set; }

        /// <summary>
        /// Resource key name, unique, used for lookup and as a base for translations.
        /// Exemple : A_Acceleration
        /// </summary>
        public string KeyName { get; set; } = string.Empty;

        /// <summary>
        /// Family code of the resource, used for grouping and categorization.
        /// Is the first part of the KeyName, before the first underscore. Exemple : A
        /// </summary>
        public string FamilyCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the resource associated with this instance.
        /// Is the second part of the KeyName, after the first underscore. Exemple : Acceleration
        /// </summary>
        public string ResourceName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CreatedByUserId { get; set; }

        public AppUser CreatedByUser { get; set; } = null!;

        public int? CreatedImportBatchId { get; set; }

        public ImportBatch? CreatedImportBatch { get; set; }

        /// <summary>
        /// Number of times this resource has been seen in imports.
        /// </summary>
        public int SeenCount { get; set; } = 1;

        public bool IsActive { get; set; } = true;

        public List<ResourceText> Texts { get; set; } = new();
    }
}
