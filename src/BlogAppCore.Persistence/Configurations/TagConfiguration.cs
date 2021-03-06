using BlogAppCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogAppCore.Persistence.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(Tag.MAX_LENGTH);

            builder.Property(s => s.Slug)
                .IsRequired()
                .HasMaxLength(Tag.MAX_LENGTH);

            builder.HasIndex(s => s.Slug)
                .IsUnique()
                .HasName("IX_Tag_Slug");

            builder.Metadata
                .FindNavigation("PostTags")
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}