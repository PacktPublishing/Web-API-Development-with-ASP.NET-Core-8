using System;
using System.Collections.Generic;
using EfCoreReverseEngineeringDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreReverseEngineeringDemo.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieActor> MovieActors { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Initial Catalog=EfCoreRelationshipsDemoDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Actors_Name").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(32);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasIndex(e => e.ContactId, "IX_Addresses_ContactId").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Contact).WithOne(p => p.Address).HasForeignKey<Address>(d => d.ContactId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(32);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasIndex(e => e.InvoiceNumber, "IX_Invoices_InvoiceNumber").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ContactName).HasMaxLength(32);
            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasMaxLength(16);
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasIndex(e => e.InvoiceId, "IX_InvoiceItems_InvoiceId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(64);
            entity.Property(e => e.Quantity).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceItems).HasForeignKey(d => d.InvoiceId);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasIndex(e => e.Title, "IX_Movies_Title").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(512);
            entity.Property(e => e.Title).HasMaxLength(128);
        });

        modelBuilder.Entity<MovieActor>(entity =>
        {
            entity.HasKey(e => new { e.MovieId, e.ActorId });

            entity.ToTable("MovieActor");

            entity.HasIndex(e => e.ActorId, "IX_MovieActor_ActorId");

            entity.Property(e => e.UpdateTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Actor).WithMany(p => p.MovieActors).HasForeignKey(d => d.ActorId);

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieActors).HasForeignKey(d => d.MovieId);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Posts_CategoryId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Content).HasMaxLength(256);
            entity.Property(e => e.Title).HasMaxLength(32);

            entity.HasOne(d => d.Category).WithMany(p => p.Posts).HasForeignKey(d => d.CategoryId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
