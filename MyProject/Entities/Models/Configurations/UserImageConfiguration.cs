using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations
{
    public class UserImageConfiguration : IEntityTypeConfiguration<UserImage>
    {
        public void Configure(EntityTypeBuilder<UserImage> builder)
        {
            builder.HasKey(i => i.Id).HasName("pk_constraint_userimage");

            builder.Property(i => i.Path).HasColumnName("Path");
            builder.Property(i => i.UserId).HasColumnName("userid").IsRequired();
        }
    }
}
