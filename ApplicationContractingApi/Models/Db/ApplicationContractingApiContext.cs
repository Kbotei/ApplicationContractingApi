using Microsoft.EntityFrameworkCore;

namespace ApplicationContractingApi.Models.Db;

public partial class ApplicationContractingApiContext : DbContext
{
    public ApplicationContractingApiContext()
    {
    }

    public ApplicationContractingApiContext(DbContextOptions<ApplicationContractingApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationFieldSubmission> ApplicationFieldSubmissions { get; set; }

    public virtual DbSet<ApplicationMobileSubmission> ApplicationMobileSubmissions { get; set; }

    public virtual DbSet<ApplicationSubmission> ApplicationSubmissions { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ContractingFieldSubmission> ContractingFieldSubmissions { get; set; }

    public virtual DbSet<ContractingSubmission> ContractingSubmissions { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:LocalDatabaseConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationFieldSubmission>(entity =>
        {
            entity.ToTable("ApplicationFieldSubmission");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.FieldName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FieldNamespace).HasMaxLength(50);
            entity.Property(e => e.FieldValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LabelText)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SelectedItemText)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationFieldSubmissions)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationFieldSubmission_ApplicationSubmission");
        });

        modelBuilder.Entity<ApplicationMobileSubmission>(entity =>
        {
            entity.ToTable("ApplicationMobileSubmission");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.ApplicationSubmission).WithMany(p => p.ApplicationMobileSubmissions)
                .HasForeignKey(d => d.ApplicationSubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationMobileSubmission_ApplicationSubmission");

            entity.HasOne(d => d.Device).WithMany(p => p.ApplicationMobileSubmissions)
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationMobileSubmission_Device");
        });

        modelBuilder.Entity<ApplicationSubmission>(entity =>
        {
            entity.ToTable("ApplicationSubmission");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ApplicationType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SubmittedAt).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.User).WithMany(p => p.ApplicationSubmissions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationSubmission_User");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Client");

            entity.HasIndex(e => e.ClientNumber, "CIX_Client_ClientNumber")
                .IsUnique()
                .IsClustered();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.A2countryCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValue("US")
                .IsFixedLength()
                .HasColumnName("A2CountryCode");
            entity.Property(e => e.ClientNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ContractingFieldSubmission>(entity =>
        {
            entity.ToTable("ContractingFieldSubmission");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FieldName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FieldNamespace).HasMaxLength(50);
            entity.Property(e => e.FieldValue)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Contracting).WithMany(p => p.ContractingFieldSubmissions)
                .HasForeignKey(d => d.ContractingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContractingFieldSubmission_ContractingSubmission");
        });

        modelBuilder.Entity<ContractingSubmission>(entity =>
        {
            entity.ToTable("ContractingSubmission");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Client).WithMany(p => p.ContractingSubmissions)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContractingSubmission_Client");

            entity.HasOne(d => d.Device).WithMany(p => p.ContractingSubmissions)
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContractingSubmission_Device");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.ToTable("Device");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.LastActiveAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.OperatingSystem)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OperatingSystemVersion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Devices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Device_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC072B1033F1");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Client).WithMany(p => p.Users)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Client");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
