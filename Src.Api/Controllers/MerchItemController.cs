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
    [HttpPost("SaveChanges")]
    public async Task<ActionResult<bool>> MerchItemSaveChanges(SaveMerchItemRequestDto request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<SaveMerchItemCommand>((_httpCurrentUser.UserName, request));
        var response = await _mediator.Send(query, cancellationToken);
        return response == true ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError);
    }

    [Authorize(Roles = "Vendor,Admin,SysAdmin")]
    [HttpPost]
    public async Task<ActionResult<bool>> CreateMerchItem(CreateMerchItemRequestDto request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<CreateMerchItemCommand>((_httpCurrentUser.UserId, request));
        var response = await _mediator.Send(query, cancellationToken);
        return response == true ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError);
    }

    [AllowAnonymous]
    [HttpGet("{SubmissionId}")]
    public async Task<ActionResult<GetMerchItemResponseDto>> GetMerchItem(int SubmissionId, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetMerchItemQuery>((_httpCurrentUser.UserName, SubmissionId));
        var response = await _mediator.Send(query, cancellationToken);
        return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound);
    }

    [AllowAnonymous]
    [HttpGet("Search")]
    public async Task<ActionResult<SearchMerchItemResponseDto>> SearchForMerchItem(SearchMerchItemRequestDto request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<SearchMerchItemQuery>((_httpCurrentUser.UserName, request));
        var response = await _mediator.Send(query, cancellationToken);
        return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound);
    }

    [Authorize(Roles = "Vendor,Admin,SysAdmin")]
    [HttpPost("Delete/{submissionId}")]
    public async Task<ActionResult<bool>> DeleteMerchItem(int submissionId, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<DeleteMerchItemCommand>((_httpCurrentUser.UserId, submissionId));
        var response = await _mediator.Send(query, cancellationToken);
        return response != false ? Ok(true) : StatusCode(StatusCodes.Status404NotFound);
    }

    [Authorize(Roles = "Vendor,Admin,SysAdmin")]
    [HttpPost("Upload")]
    public async Task<ActionResult<bool>> UploadMerchItemImage([FromForm] UploadImageMerchItemRequestDto request, CancellationToken cancellationToken)
    {
        using var memStream = new MemoryStream();
        await request.UploadedImage.CopyToAsync(memStream, cancellationToken);
        var bytes = memStream.ToArray();

        var convertedToDO = new UploadImageMerchItemRequest
        {
            SubmissionId = request.SubmissionId,
            UploadedImage = bytes
        };

        var query = _mapper.Map<UploadMerchItemImageCommand>((_httpCurrentUser.UserId, convertedToDO));
        var response = await _mediator.Send(query, cancellationToken);
        return response != false ? Ok(true) : StatusCode(StatusCodes.Status404NotFound);
    }
}