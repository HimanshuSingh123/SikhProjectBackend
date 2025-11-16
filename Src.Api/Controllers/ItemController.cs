using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Src.Application.Features.Query.ItemMetaData;
using Src.Domain.Item;
using Src.Dto.Item;

namespace Src.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ItemController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        //authorize?
        [HttpGet]
        public async Task<ActionResult<ItemMetaDataResponseDto>> GetItemMetaData([FromQuery] ItemMetaDataRequestDto request)
        {
            var result = await _mediator.Send(_mapper.Map<ItemMetaDataGetRequestQuery>(request));
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
