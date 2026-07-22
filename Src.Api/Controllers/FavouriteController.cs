using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Src.Application.Features.Favourite.Queries;
using Src.Domain.Favourite;
using Src.Domain.Item;
using Src.Dto.Favourite;
using Src.Dto.Item;

namespace Src.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public FavouriteController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        //authorize?
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewFavouritesResponseDto>>> GetFavouritesData(ViewFavouritesRequestDto request)
        {
            var query = _mapper.Map<ViewFavouriteGetRequestQuery>(request);
            var result = await _mediator.Send(query);
            return Ok(_mapper.Map<ViewFavouritesResponseDto>(result));
        }

    }
}
