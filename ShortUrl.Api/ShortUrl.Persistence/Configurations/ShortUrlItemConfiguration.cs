using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShortUrl.Domain;

namespace ShortUrl.Persistence.Configurations
{
    internal class ShortUrlItemConfiguration : IEntityTypeConfiguration<ShortUrlItem>
    {
        public void Configure(EntityTypeBuilder<ShortUrlItem> builder)
        {
            builder
                .Property(it => it.Shortcut)
                .HasMaxLength(ShortUrlItem.ShortcutMaxLength);

            builder
                .Property(it => it.Url)
                .HasMaxLength(ShortUrlItem.UrlMaxLength);

            builder.HasKey(it => it.Shortcut);
            builder.HasIndex(it => it.Url).IsUnique(true);
        }
    }
}
