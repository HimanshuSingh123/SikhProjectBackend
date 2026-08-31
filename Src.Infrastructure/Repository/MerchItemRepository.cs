using Microsoft.EntityFrameworkCore;
using Src.Application.Interfaces;
using Src.Domain.Entities;
using Src.Domain.MerchItems;
using Src.Infrastructure.Persistance;

namespace Src.Infrastructure.Repository;

public class MerchItemRepository : IMerchItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public MerchItemRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<bool> SaveMerchItemChanges(SaveMerchItemRequest request)
    {
        var MerchItemToUpdate = await _dbContext.Merch.AsNoTracking().FirstOrDefaultAsync(m => m.SubmissionId == request.SubmissionId);

        if (MerchItemToUpdate == null)
        {
            return false;
        }

        Dictionary<string, object?> attributes = request.AttributesToDictionary();

        foreach (var attribute in attributes)
        {
            var key = attribute.Key;
            var value = attribute.Value;

            if(value == null)
            {
                continue;
            }

             _dbContext.Entry<Merch>(MerchItemToUpdate)
                .Property(key)
                .CurrentValue = value;
        }

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CreateMerchItem(CreateMerchItemRequest request)
    {
        Merch newMerch = new Merch
        {
            SubmissionId = request.SubmissionId,
            Title = request.Title,
            Description = request.Description!,
            Image = request.Image!,
            Size = request.Size,
            QuantityMax = request.QuantityMax,
            QuantityMin = request.QuantityMin,
            Price = request.Price,
            Rating = request.Rating ?? 0
        };

        _dbContext.Merch.Add(newMerch);

        var result =  await _dbContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<GetMerchItemResponse?> GetMerchItem(int submissionId)
    {
        var merchItem = await _dbContext.Merch.AsNoTracking().FirstOrDefaultAsync(m => m.SubmissionId == submissionId);

        if (merchItem == null)
        {
            return null;
        }

        return new GetMerchItemResponse
        {
            SubmissionId = merchItem.SubmissionId,
            Title = merchItem.Title,
            Description = merchItem.Description,
            Image = merchItem.Image,
            Size = merchItem.Size,
            QuantityMax = merchItem.QuantityMax,
            QuantityMin = merchItem.QuantityMin,
            Price = merchItem.Price,
            Rating = merchItem.Rating
        };
    }
}

