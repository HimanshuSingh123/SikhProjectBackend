using Src.Domain.Item;
using Src.Domain.MerchItems;
using System;

namespace Src.Application.Interfaces;

public interface IMerchItemRepository
{
    Task<GetMerchItemResponse?> GetMerchItem(int submissionId, CancellationToken cancellationToken);
    Task<SearchMerchItemResponse> SearchMerchItem(SearchMerchItemRequest request, CancellationToken cancellationToken);
    Task<bool> SaveMerchItemChanges(SaveMerchItemRequest request, CancellationToken cancellationToken);
    Task<bool> CreateMerchItem(CreateMerchItemRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteMerchItem(int submissionId, CancellationToken cancellationToken);
}

