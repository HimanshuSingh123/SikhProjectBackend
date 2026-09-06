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

    public async Task<bool> SaveMerchItemChanges(SaveMerchItemRequest request, CancellationToken cancellationToken)
    {
        var MerchItemToUpdate = await _dbContext.Merch.AsNoTracking().FirstOrDefaultAsync((m => m.SubmissionId == request.SubmissionId), cancellationToken);

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

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> CreateMerchItem(CreateMerchItemRequest request, CancellationToken cancellationToken)
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

        var result =  await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<GetMerchItemResponse?> GetMerchItem(int submissionId, CancellationToken cancellationToken)
    {
        var merchItem = await _dbContext.Merch.AsNoTracking().FirstOrDefaultAsync((m => m.SubmissionId == submissionId), cancellationToken);

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

    public async Task<SearchMerchItemResponse> SearchMerchItem(SearchMerchItemRequest request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Merch.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            query = query.Where(x => x.Title == request.Title);
        }
        if (request.Price.HasValue)
        {
            query = query.Where(x => x.Price ==  request.Price);
        }
        if (request.Rating.HasValue)
        {
            query = query.Where(x => x.Rating ==  request.Rating);
        }
        if(request.Size != null)
        {
            query = query.Where(x => x.Size == x.Size);
        }

        var totalItems = await query.CountAsync();

        var items = await query.OrderBy(x => x.SubmissionId).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);

        var totalPages = (int)Math.Ceiling((double)(totalItems / request.PageSize));

        return new SearchMerchItemResponse
        {
            MerchItems = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalItems,
            TotalPages = totalPages
        };
    }

    public async Task<bool> DeleteMerchItem(int submissionId, CancellationToken cancellationToken)
    {

        var merchItemExists = await _dbContext.Merch.AnyAsync(m => m.SubmissionId == submissionId, cancellationToken);

        if (!merchItemExists)
        {
            return false;
        }

        var inCart = await _dbContext.Cart.AnyAsync(c => c.SubmissionId == submissionId, cancellationToken);
        var inFavourites = await _dbContext.Favourites.AnyAsync(f => f.SubmissionId == submissionId, cancellationToken);

        if (inCart)
        {
            var merchItemsToRemoveFromCart = await _dbContext.Cart.Where(s => s.SubmissionId == submissionId).ToListAsync(cancellationToken);
            _dbContext.Cart.RemoveRange(merchItemsToRemoveFromCart);
        }

        if (inFavourites)
        {
            var merchItemsToRemoveFromFavourites = await _dbContext.Favourites.Where(s=>s.SubmissionId==submissionId).ToListAsync(cancellationToken);
            _dbContext.Favourites.RemoveRange(merchItemsToRemoveFromFavourites);
        }

         var merchItemToDelete = await _dbContext.Merch.SingleOrDefaultAsync(m => m.SubmissionId == submissionId, cancellationToken);

        if(merchItemToDelete == null)
        {
            return false;
        }

        _dbContext.Merch.Remove(merchItemToDelete);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}

