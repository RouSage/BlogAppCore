using BlogAppCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogAppCore.Persistence.Configurations
{
    public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            builder.HasKey(i => new { i.PostId, i.TagId });

            builder
                .HasOne(p => p.Post)
                .WithMany(pt => pt.PostTags)
                .HasForeignKey(pi => pi.PostId)
                .IsRequired();

            builder
                .HasOne(t => t.Tag)
                .WithMany(pt => pt.PostTags)
                .HasForeignKey(ti => ti.TagId)
                .IsRequired();
        }
    }
}