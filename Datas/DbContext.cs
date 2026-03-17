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
        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<ImportBatch> ImportBatches => Set<ImportBatch>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<Resource> Resources => Set<Resource>();
        public DbSet<ResourceText> ResourceTexts => Set<ResourceText>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // APP USER
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Level)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.Property(e => e.IsActive)
                    .IsRequired();

                entity.HasIndex(e => e.Login)
                    .IsUnique();
            });

            // IMPORT BATCH
            modelBuilder.Entity<ImportBatch>(entity =>
            {
                entity.ToTable("import_batches");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ImportedAt)
                    .IsRequired();

                entity.Property(e => e.SourceGroupName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Comment)
                    .HasMaxLength(2000);

                entity.HasOne(e => e.ImportedByUser)
                    .WithMany(u => u.ImportedBatches)
                    .HasForeignKey(e => e.ImportedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);
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

                entity.Property(e => e.IsActive)
                    .IsRequired();

                entity.HasIndex(e => e.Code)
                    .IsUnique();
            });


            // RESOURCE
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("resources");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.KeyName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FamilyCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ResourceName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.Property(e => e.SeenCount)
                    .IsRequired();

                entity.Property(e => e.IsActive)
                    .IsRequired();

                entity.HasIndex(e => e.KeyName)
                    .IsUnique();

                entity.HasIndex(e => e.FamilyCode);

                entity.HasIndex(e => e.ResourceName);

                entity.HasOne(e => e.CreatedByUser)
                    .WithMany(u => u.CreatedResources)
                    .HasForeignKey(e => e.CreatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.CreatedImportBatch)
                    .WithMany(b => b.CreatedResources)
                    .HasForeignKey(e => e.CreatedImportBatchId)
                    .OnDelete(DeleteBehavior.SetNull);
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

                entity.Property(e => e.IsPreferred)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.Property(e => e.SeenCount)
                    .IsRequired();

                entity.Property(e => e.NeedsReview)
                    .IsRequired();

                entity.HasIndex(e => new { e.ResourceId, e.LanguageId });

                entity.HasOne(e => e.Resource)
                    .WithMany(r => r.Texts)
                    .HasForeignKey(e => e.ResourceId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Language)
                    .WithMany(l => l.ResourceTexts)
                    .HasForeignKey(e => e.LanguageId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.CreatedByUser)
                    .WithMany(u => u.CreatedResourceTexts)
                    .HasForeignKey(e => e.CreatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.CreatedImportBatch)
                    .WithMany(b => b.CreatedResourceTexts)
                    .HasForeignKey(e => e.CreatedImportBatchId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.ReplacesText)
                    .WithMany(t => t.ReplacedByTexts)
                    .HasForeignKey(e => e.ReplacesTextId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
