using BlogAppCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogAppCore.Persistence.Configurations
{
    public class PostConfigurations : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(s => s.Slug)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(c => c.Content)
                .IsRequired();

            builder.Property(cr => cr.Created)
                .HasColumnType("datetime");

            builder
                .HasOne(c => c.Category)
                .WithMany(p => p.Posts)
                .HasForeignKey(ci => ci.CategoryId);

            builder.Metadata
                .FindNavigation("PostTags")
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}