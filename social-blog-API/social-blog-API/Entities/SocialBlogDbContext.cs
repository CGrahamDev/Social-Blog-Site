using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace social_blog_API.Entities;

public partial class SocialBlogDbContext : DbContext
{
    public SocialBlogDbContext()
    {
    }

    public SocialBlogDbContext(DbContextOptions<SocialBlogDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC079D59CE98");

            entity.Property(e => e.Content)
                .HasMaxLength(256)
                .IsUnicode(false);

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Comments__PostId__4222D4EF");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Comments__UserId__412EB0B6");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Posts__3214EC07A00DF2A1");

            entity.Property(e => e.Content)
                .HasMaxLength(1600)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(128)
                .IsUnicode(false);

            entity.HasOne(d => d.Author).WithMany(p => p.Posts)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Posts__AuthorId__3E52440B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07E3F28478");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(128);
            entity.Property(e => e.Username)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
