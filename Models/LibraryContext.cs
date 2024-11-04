using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace school.Models;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=library;user=root;password=;sslmode=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mark");

            entity.HasIndex(e => e.StudentId, "student_id");

            entity.HasIndex(e => e.StudentId, "student_id_2").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("created_time");
            entity.Property(e => e.Description)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Marknumber)
                .HasMaxLength(1)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("marknumber");
            entity.Property(e => e.Marktext)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("marktext");
            entity.Property(e => e.StudentId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("student_id");
            entity.Property(e => e.UpdatedTime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("updated_time");

            entity.HasOne(d => d.Student).WithOne(p => p.Mark)
                .HasForeignKey<Mark>(d => d.StudentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("mark_ibfk_1");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("students");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("age");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("created_time");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
