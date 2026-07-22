using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Src.Application.Features.ItemMetaData.Queries;
using Src.Application.Interfaces.Common;
using Src.Dto.Item;

namespace Src.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _httpCurrentUser;
        public ItemController(IMediator mediator, IMapper mapper, ICurrentUser httpCurrentUser)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpCurrentUser = httpCurrentUser ?? throw new ArgumentNullException(nameof(httpCurrentUser));
        }

        //authorize?
        [HttpGet("MetaData")]
        public async Task<ActionResult<ItemMetaDataResponseDto>> GetItemMetaData([FromQuery] ItemMetaDataRequestDto request)
        {
            var result = await _mediator.Send(_mapper.Map<ItemMetaDataGetRequestQuery>(request));
            Console.WriteLine($"User Information: {_httpCurrentUser.Email}, {_httpCurrentUser.UserName}, {_httpCurrentUser.UserId}, {string.Join(", ", _httpCurrentUser.AccountType)}");
            return Ok(_mapper.Map<ItemMetaDataResponseDto>(result));
        }

        //authorize?
        [HttpGet("ViewItems")]
        public async Task<ActionResult<IEnumerable<ViewItemsResponseDto>>> GetViewItems([FromQuery] ViewItemsRequestDto request)
        {
            var result = await _mediator.Send(_mapper.Map<ViewItemsGetRequestQuery>(request));
            return Ok(_mapper.Map<IEnumerable<ViewItemsResponseDto>>(result));
        }

        [HttpGet("ViewImages")]
        public async Task<ActionResult<IEnumerable<ViewImageResponseDto>>> GetViewImages([FromQuery] ViewImageRequestDto request)
        {
            var result = await _mediator.Send(_mapper.Map<ViewImageGetRequestQuery>(request));
            return Ok(_mapper.Map<IEnumerable<ViewImageResponseDto>>(result));        }

    }
}
