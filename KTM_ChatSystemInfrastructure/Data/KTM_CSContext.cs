using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Core.Models;
namespace Infrastructure.Data;

public partial class KTM_CSContext : DbContext
{
    readonly IConfiguration _config;
    public KTM_CSContext(IConfiguration config)
    {
        _config = config;
    }

    public KTM_CSContext(DbContextOptions<KTM_CSContext> options, IConfiguration config)
        : base(options)
    {
        _config = config;
    }

    public virtual DbSet<Core.Models.File> Files { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Otpauthentication> Otpauthentications { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_config.GetConnectionString("KTM_CS_DBConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Core.Models.File>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PK__File__6F0F98BFAFF4DD47");

            entity.ToTable("File");

            entity.Property(e => e.FileName).HasMaxLength(100);
            entity.Property(e => e.FilePath).HasMaxLength(1024);
            entity.Property(e => e.FileType).HasMaxLength(5);
            entity.Property(e => e.Message).HasMaxLength(1000);

            entity.HasOne(d => d.User).WithMany(p => p.Files)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_File_User");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Message__C87C0C9C0D0718FD");

            entity.ToTable("Message");

            entity.Property(e => e.Content).HasMaxLength(2000);

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_User");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12ED0BD996");

            entity.ToTable("Notification");

            entity.Property(e => e.Message).HasMaxLength(200);

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notification_User");
        });

        modelBuilder.Entity<Otpauthentication>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OTPAuthentication");

            entity.Property(e => e.Otp).HasColumnName("OTP");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTPAuthentication_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CDD4D70F0");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D105344F34FB78").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
