using MediatR;
using Microsoft.EntityFrameworkCore;
using ShortUrl.Api.RequestModels.Queries;
using ShortUrl.Domain;
using ShortUrl.Persistence;

namespace ShortUrl.Api.Handlers
{
    public class GetByShortUrlQueryHandler : IRequestHandler<GetByShortUrlQuery, ShortUrlItem?>
    {
        private readonly ShortUrlContext _dbContext;

        public GetByShortUrlQueryHandler(ShortUrlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShortUrlItem?> Handle(GetByShortUrlQuery request, CancellationToken cancellationToken)
        {
            ShortUrlItem? item = await _dbContext.ShortUrlItems.SingleOrDefaultAsync(it => it.Shortcut == request.ShortUrl, cancellationToken);

            return item;
        }
    }
}
