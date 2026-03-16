using Microsoft.EntityFrameworkCore;
using ResourcesManager.Models;


namespace ResourcesManager.Datas
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define tables
        public DbSet<Resource> Resources => Set<Resource>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<ResourceText> ResourceTexts => Set<ResourceText>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // RESOURCE
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("resources");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.KeyName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasIndex(e => e.KeyName)
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();
            });

            // LANGUAGE
            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("languages");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasIndex(e => e.Code)
                    .IsUnique();
            });

            // RESOURCE TEXT
            modelBuilder.Entity<ResourceText>(entity =>
            {
                entity.ToTable("resource_texts");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.TextValue)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.HasOne(e => e.Resource)
                    .WithMany(r => r.Texts)
                    .HasForeignKey(e => e.ResourceId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Language)
                    .WithMany(l => l.ResourceTexts)
                    .HasForeignKey(e => e.LanguageId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => new { e.ResourceId, e.LanguageId });
            });
        }

    }
}
