using Azure.Core;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Src.Application.Features.MerchItem.Commands;
using Src.Application.Features.MerchItem.NewFolder;
using Src.Application.Features.MerchItem.Queries;
using Src.Application.Interfaces.Common;
using Src.Domain.MerchItems;
using Src.Dto.MerchItems;

namespace Src.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MerchItemController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _httpCurrentUser;

    public MerchItemController(IMediator mediator, IMapper mapper, ICurrentUser httpCurrentUser)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpCurrentUser = httpCurrentUser ?? throw new ArgumentNullException(nameof(httpCurrentUser));
    }

    [Authorize(Roles = "Vendor,Admin,SysAdmin")]
    [HttpPut("SaveChanges")]
    public async Task<ActionResult<bool>> MerchItemSaveChanges(SaveMerchItemRequestDto request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<SaveMerchItemCommand>((_httpCurrentUser.UserName, request));
        var response = await _mediator.Send(query, cancellationToken);
        return response == true ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError);
    }

    [Authorize(Roles = "Vendor,Admin,SysAdmin")]
    [HttpPost("CreateMerchItem")]
    public async Task<ActionResult<bool>> CreateMerchItem([FromForm] CreateMerchItemRequestDto request, CancellationToken cancellationToken)
    {
        CreateMerchItemCommand query;
        CreateMerchItemRequest convertedRequest;

        if (request.Image != null)
        {
            convertedRequest = new CreateMerchItemRequest
            {
                SubmissionId = request.SubmissionId,
                Title = request.Title,
                Description = request.Description,
                Image = request.Image != null ? await Image2Bytes(request.Image, cancellationToken) : null,
                Size = request.Size,
                QuantityMax = request.QuantityMax,
                QuantityMin = request.QuantityMin,
                Price = request.Price,
                Rating = request.Rating
            };
            query = _mapper.Map<CreateMerchItemCommand>((_httpCurrentUser.UserName, convertedRequest));
        }
        else
        {
            query = _mapper.Map<CreateMerchItemCommand>((_httpCurrentUser.UserName, request));
        }

        var response = await _mediator.Send(query, cancellationToken);
        return response == true ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError);
    }

    [AllowAnonymous]
    [HttpGet("GetMerchItem/{SubmissionId}")]
    public async Task<ActionResult<GetMerchItemResponseDto>> GetMerchItem(int SubmissionId, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetMerchItemQuery>((_httpCurrentUser.UserName, SubmissionId));
        var response = await _mediator.Send(query, cancellationToken);
        return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound);
    }

    [AllowAnonymous]
    [HttpGet("SearchMerchItems")]
    public async Task<ActionResult<SearchMerchItemResponseDto>> SearchForMerchItem(SearchMerchItemRequestDto request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<SearchMerchItemQuery>((_httpCurrentUser.UserName, request));
        var response = await _mediator.Send(query, cancellationToken);
        return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound);
    }

    [Authorize(Roles = "Vendor,Admin,SysAdmin")]
    [HttpDelete("DeleteMerchItem/{submissionId}")]
    public async Task<ActionResult<bool>> DeleteMerchItem(int submissionId, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<DeleteMerchItemCommand>((_httpCurrentUser.UserName, submissionId));
        var response = await _mediator.Send(query, cancellationToken);
        return response != false ? Ok(true) : StatusCode(StatusCodes.Status404NotFound);
    }

    [Authorize(Roles = "Vendor,Admin,SysAdmin")]
    [HttpPost("UploadMerchItemImage")]
    public async Task<ActionResult<bool>> UploadMerchItemImage([FromForm] UploadImageMerchItemRequestDto request, CancellationToken cancellationToken)
    {
        var bytes = await Image2Bytes(request.UploadedImage, cancellationToken);

        var convertedToDO = new UploadImageMerchItemRequest
        {
            SubmissionId = request.SubmissionId,
            UploadedImage = bytes
        };

        var query = _mapper.Map<UploadMerchItemImageCommand>((_httpCurrentUser.UserName, convertedToDO));
        var response = await _mediator.Send(query, cancellationToken);
        return response != false ? Ok(true) : StatusCode(StatusCodes.Status404NotFound);
    }

    private static async Task<byte[]> Image2Bytes(IFormFile image, CancellationToken cancellationToken)
    {
        using var memStream = new MemoryStream();
        await image.CopyToAsync(memStream, cancellationToken);
        return memStream.ToArray();
    }
}