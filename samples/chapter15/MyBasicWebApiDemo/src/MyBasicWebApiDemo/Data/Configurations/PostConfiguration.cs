using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MyBasicWebApiDemo.Models;

namespace MyBasicWebApiDemo.Data.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Title).HasColumnName("Title").HasMaxLength(32).IsRequired();
        builder.Property(p => p.Content).HasColumnName("Content").HasMaxLength(256).IsRequired();
        builder.Property(p => p.CategoryId).HasColumnName("CategoryId");
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
