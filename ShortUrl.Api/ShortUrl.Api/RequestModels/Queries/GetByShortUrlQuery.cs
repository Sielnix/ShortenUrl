using MediatR;
using ShortUrl.Domain;

namespace ShortUrl.Api.RequestModels.Queries
{
    public class GetByShortUrlQuery : IRequest<ShortUrlItem?>
    {
        public GetByShortUrlQuery(string shortUrl)
        {
            ShortUrl = shortUrl;
        }

        public string ShortUrl { get; }
    }
}
