using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Src.Domain.Item;
using Src.Dto.Item;

namespace Src.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //authorize?
        [HttpGet]
        public async Task<ActionResult<ItemMetaDataResponseDto>> GetItemMetaData([FromQuery] ItemMetaDataRequestDto request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        //authorize?
        [HttpGet("ViewItems")]
        public async Task<ActionResult<IEnumerable<ViewItemsResponseDto>>> GetViewItems([FromQuery] ViewItemsRequestDto request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("ViewImages")]
        public async Task<ActionResult<IEnumerable<ViewImageResponseDto>>> GetViewImages([FromQuery] ViewImageResponseDto request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);        }

    }
}
