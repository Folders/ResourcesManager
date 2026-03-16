namespace ResourcesManager.Models
{
    public class Resource
    {
        public int Id { get; set; }

        /// <summary>
        /// Nom unique interne de la ressource, sans groupe Sysmac.
        /// Exemple : A_Acceleration
        /// </summary>
        public string KeyName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<ResourceText> Texts { get; set; } = new();
    }
}
