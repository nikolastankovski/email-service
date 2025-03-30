using Microsoft.EntityFrameworkCore;
using NxEmailService.Models;

namespace NxEmailService.DbContexts;

public partial class NxEmailServiceDbContext : DbContext
{
    public NxEmailServiceDbContext()
    {
    }

    public NxEmailServiceDbContext(DbContextOptions<NxEmailServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmailHistory> EmailHistories { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //Get connection string from appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<EmailHistory>(entity =>
        {
            entity.HasKey(e => e.EmailHistoryID).HasName("PK_EmailHistory_EmailHistoryID");

            entity.ToTable("EmailHistory");

            entity.Property(e => e.CreatedOnUTC)
                .HasPrecision(3)
                .HasDefaultValueSql("(NOW() AT TIME ZONE 'UTC')");
            entity.Property(e => e.From).HasMaxLength(500);
            entity.Property(e => e.IsSent).HasDefaultValue(false);
            entity.Property(e => e.Template).HasMaxLength(1000);
            entity.Property(e => e.RelatedEntityName).HasMaxLength(1000);
            entity.Property(e => e.RelatedEntityId).HasMaxLength(68);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
