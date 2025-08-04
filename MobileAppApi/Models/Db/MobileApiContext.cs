using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MobileAppApi.Models.Db;

public partial class MobileApiContext : DbContext
{
    public MobileApiContext()
    {
    }

    public MobileApiContext(DbContextOptions<MobileApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<CreditApplicationFieldSubmission> CreditApplicationFieldSubmissions { get; set; }

    public virtual DbSet<CreditApplicationSubmission> CreditApplicationSubmissions { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Initial Catalog=MobileApi");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Account");

            entity.HasIndex(e => e.AccountNumber, "CIX_Account_AccountNumber")
                .IsUnique()
                .IsClustered();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.CurrentDecision)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Status)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValue("registered");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Client");

            entity.HasIndex(e => e.ClientNumber, "CIX_Client_ClientNumber")
                .IsUnique()
                .IsClustered();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CreditApplicationFieldSubmission>(entity =>
        {
            entity.ToTable("CreditApplicationFieldSubmission");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.FieldName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FieldNamespace).HasMaxLength(50);
            entity.Property(e => e.FieldType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FieldValue)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CreditApplication).WithMany(p => p.CreditApplicationFieldSubmissions)
                .HasForeignKey(d => d.CreditApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CreditApplicationFieldSubmission_CreditApplicationSubmission");
        });

        modelBuilder.Entity<CreditApplicationSubmission>(entity =>
        {
            entity.ToTable("CreditApplicationSubmission");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SubmittedAt).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Client).WithMany(p => p.CreditApplicationSubmissions)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CreditApplicationSubmission_Client");

            entity.HasOne(d => d.Device).WithMany(p => p.CreditApplicationSubmissions)
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CreditApplicationSubmission_Device");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.ToTable("Device");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.OperatingSystem)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OperatingSystemVersion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
