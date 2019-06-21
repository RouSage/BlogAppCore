using BlogAppCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogAppCore.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(Category.MAX_LENGTH);

            builder.Property(s => s.Slug)
                .IsRequired()
                .HasMaxLength(Category.MAX_LENGTH);

            builder.HasIndex(s => s.Slug)
                .IsUnique()
                .HasName("IX_Category_Slug");

            builder.Metadata
                .FindNavigation("Posts")
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}