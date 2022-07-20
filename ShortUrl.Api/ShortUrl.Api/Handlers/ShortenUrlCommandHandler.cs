using MediatR;
using Microsoft.EntityFrameworkCore;
using ShortUrl.Api.RequestModels.Commands;
using ShortUrl.Domain;
using ShortUrl.Persistence;

namespace ShortUrl.Api.Handlers
{
    public class ShortenUrlCommandHandler : IRequestHandler<ShortenUrlCommand, ShortUrlItem>
    {
        private readonly ShortUrlContext _dbContext;

        public ShortenUrlCommandHandler(ShortUrlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShortUrlItem> Handle(ShortenUrlCommand request, CancellationToken cancellationToken)
        {
            ShortUrlItem? item = await _dbContext.ShortUrlItems
                .SingleOrDefaultAsync(it => it.Url == request.Url, cancellationToken);

            if (item != null)
            {
                return item;
            }

            string shortcut = await GetNewShortcut(cancellationToken);

            ShortUrlItem newItem = new(shortcut, request.Url);

            _dbContext.ShortUrlItems.Add(newItem);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newItem;
        }

        private async Task<string> GetNewShortcut(CancellationToken cancellationToken)
        {

            while (true)
            {
                string shortcut = Guid.NewGuid().ToString("N").Substring(0, ShortUrlItem.ShortcutMaxLength);

                bool shortcutExists = await _dbContext.ShortUrlItems
                    .AnyAsync(it => it.Shortcut == shortcut, cancellationToken);

                if (shortcutExists)
                {
                    // try different one
                    continue;
                }

                return shortcut;
            }
        }
    }
}
