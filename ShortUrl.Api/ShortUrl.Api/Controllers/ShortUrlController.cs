using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShortUrl.Api.RequestModels.Commands;
using ShortUrl.Api.RequestModels.Queries;
using ShortUrl.Domain;

namespace ShortUrl.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortUrlController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<ShortenUrlCommand> _shortenUrlCommandValidator;

        public ShortUrlController(IMediator mediator, IValidator<ShortenUrlCommand> shortenUrlCommandValidator)
        {
            _mediator = mediator;
            _shortenUrlCommandValidator = shortenUrlCommandValidator;
        }

        [HttpPost(Name = "CreateShortUrl")]
        public async Task<IActionResult> CreateShortUrl([FromBody] ShortenUrlCommand shortenUrl, CancellationToken cancellationToken)
        {
            if (!(await _shortenUrlCommandValidator.ValidateAsync(shortenUrl, cancellationToken)).IsValid)
            {
                return BadRequest();
            }

            ShortUrlItem shortUrlItem = await _mediator.Send(shortenUrl, cancellationToken);

            return Ok(shortUrlItem);
        }

        [HttpGet("{shortenUrl}")]
        public async Task<IActionResult> GetByShort(string shortenUrl, CancellationToken cancellationToken)
        {
            ShortUrlItem? shortUrlItem = await _mediator.Send(new GetByShortUrlQuery(shortenUrl), cancellationToken);
            if (shortUrlItem == null)
            {
                return NotFound();
            }

            return Ok(shortUrlItem);
        }
    }
}
