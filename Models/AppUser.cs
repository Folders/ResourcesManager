namespace ResourcesManager.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        public string Login { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// 0 = User, 1 = Advanced, 2 = Admin
        /// </summary>
        public int Level { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        public List<ImportBatch> ImportedBatches { get; set; } = new();

        public List<Resource> CreatedResources { get; set; } = new();

        public List<ResourceText> CreatedResourceTexts { get; set; } = new();
    }
}
