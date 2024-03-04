using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(l => l.Name).IsRequired();
            builder.Property(l => l.Email).IsRequired();
            builder.Property(l => l.Password).IsRequired();
        }
    }
}
