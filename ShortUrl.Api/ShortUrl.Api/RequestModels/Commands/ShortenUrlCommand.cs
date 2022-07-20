using MediatR;
using ShortUrl.Domain;

namespace ShortUrl.Api.RequestModels.Commands
{
    public class ShortenUrlCommand : IRequest<ShortUrlItem>
    {
        public string Url { get; set; } = null!;
    }
}
