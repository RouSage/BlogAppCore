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
                .HasMaxLength(20);

            builder.Property(s => s.Slug)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(s => s.Slug)
                .IsUnique()
                .HasName("IX_Tag_Slug");

            builder.Property(c => c.Created)
                .HasColumnType("datetime");

            builder.Metadata
                .FindNavigation("PostTags")
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}