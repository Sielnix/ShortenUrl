using FluentValidation;
using ShortUrl.Api.RequestModels.Commands;
using ShortUrl.Domain;

namespace ShortUrl.Api.Validators
{
    public class ShortenUrlCommandValidator : AbstractValidator<ShortenUrlCommand>
    {
        public ShortenUrlCommandValidator()
        {
            RuleFor(r => r.Url)
                .NotEmpty()
                .Length(1, ShortUrlItem.UrlMaxLength)
                .Must(u => Uri.IsWellFormedUriString(u, UriKind.Absolute));
        }
    }
}
