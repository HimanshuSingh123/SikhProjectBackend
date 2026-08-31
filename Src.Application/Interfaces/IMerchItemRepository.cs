using Src.Domain.Item;
using Src.Domain.MerchItems;
using System;

namespace Src.Application.Interfaces;

public interface IMerchItemRepository
{
    Task<GetMerchItemResponse?> GetMerchItem(int submissionId);
    Task<bool> SaveMerchItemChanges(SaveMerchItemRequest request);
    Task<bool> CreateMerchItem(CreateMerchItemRequest request);
}

