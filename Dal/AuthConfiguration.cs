using Dal.User.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal;

public class AuthConfiguration : IEntityTypeConfiguration<UserDal>
{
    public void Configure(EntityTypeBuilder<UserDal> builder)
    {
        builder.HasKey(x => x.Id);
    }
}