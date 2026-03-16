namespace ResourcesManager.Models
{
    public class Language
    {
        public int Id { get; set; }

        /// <summary>
        /// Code langue, ex: en-US, fr-CH
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Nom lisible, ex: English (United States)
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public List<ResourceText> ResourceTexts { get; set; } = new();
    }
}
