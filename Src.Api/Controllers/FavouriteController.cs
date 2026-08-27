using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Src.Application.Features.Favourite.Commands;
using Src.Application.Features.Favourite.Queries;
using Src.Application.Interfaces.Common;
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
        private readonly ICurrentUser _currentUser;
        public FavouriteController(IMediator mediator, IMapper mapper, ICurrentUser currentUser)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        //authorize?
        [HttpGet("favourites")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<ViewFavouritesResponseDto>>> GetFavouritesData(ViewFavouritesRequestDto request)
        {
            var query = _mapper.Map<ViewFavouriteGetRequestQuery>(request);
            var result = await _mediator.Send(query);
            return Ok(_mapper.Map<IEnumerable<ViewFavouritesResponseDto>>(result));
        }

        [HttpPost("AddToFavourites/{submissionId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<bool>> AddToFavourites(int submissionId, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<AddToFavouritesCommand>((submissionId, _currentUser.UserName));
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost("DeleteFromFavourites/{Fav_id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<bool>> DeleteFromFavourites(int Fav_id, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<DeleteFromFavouritesCommand>((Fav_id, _currentUser.UserName));
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
