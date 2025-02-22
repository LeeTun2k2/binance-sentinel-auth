using BinanceSential.Auth.Core.UserAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BinanceSential.Auth.Infrastructure.Data.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable(nameof(User));
    builder.HasKey(u => u.Id);
  }
}
