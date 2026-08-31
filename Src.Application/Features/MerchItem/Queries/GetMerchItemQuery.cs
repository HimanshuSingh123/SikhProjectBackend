using MediatR;
using Src.Domain.MerchItems;

namespace Src.Application.Features.MerchItem.NewFolder;

public record GetMerchItemQuery(string User, int SubmissionId) : IRequest<GetMerchItemResponse?>;

