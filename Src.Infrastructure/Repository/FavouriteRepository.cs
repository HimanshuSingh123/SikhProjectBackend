using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Src.Application.Interfaces;
using Src.Domain.Entities;
using Src.Domain.Favourite;
using Src.Dto.Common;
using Src.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Infrastructure.Repository;

public class FavouriteRepository : IFavouriteRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FavouriteRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<ViewFavouritesResponse>> GetFavouritesAsync(ViewFavouritesRequest request, CancellationToken cancellationToken)
    {
        var favourites = await _dbContext.Favourites.AsNoTracking().Where(f => f.Username == request.Username)
            .Select(f => new ViewFavouritesResponse
            {
                ItemTitle = f.ItemTitle,
                ItemDescription = f.ItemDescription,
                Price = f.Price,
                Category = f.Category,
                Submission_Id = f.SubmissionId,
                FavId = f.FavId
            })
            .ToListAsync(cancellationToken);

        return favourites;
    }

    public async Task<bool> AddToFavouritesAsync(AddToFavouritesRequest request, CancellationToken cancellationToken)
    {

        var favouriteToBeAdded = await SubmissionExtractor(request, cancellationToken);

        if (favouriteToBeAdded == null)
        {
            return false;
        }


        _dbContext.Favourites.Add(favouriteToBeAdded);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;

    }
    public async Task<Favourites?> SubmissionExtractor(AddToFavouritesRequest request, CancellationToken cancellationToken)
    {
        var itemData = await _dbContext.Submission.AsNoTracking().Where(s => s.SubmissionId == request.submissionId).SingleOrDefaultAsync(cancellationToken);
        if(itemData == null)
        {
            return null;
        }
        switch (itemData.Category)
        {
            case ItemCategories.Course:
                return await _dbContext.Course.AsNoTracking().Where(c => c.SubmissionId == request.submissionId).Select(c => new Favourites
                {
                    Username = request.Username,
                    ItemTitle = c.CourseName,
                    ItemDescription = c.Description,
                    Price = c.Price,
                    Category = itemData.Category,
                    SubmissionId = c.SubmissionId
                }).SingleOrDefaultAsync(cancellationToken);
            case ItemCategories.Merch:
                return await _dbContext.Merch.AsNoTracking().Where(m => m.SubmissionId == request.submissionId).Select(m => new Favourites
                {
                    Username = request.Username,
                    ItemTitle = m.Title,
                    ItemDescription = m.Description,
                    Price = m.Price,
                    Category = itemData.Category,
                    SubmissionId = m.SubmissionId
                }).SingleOrDefaultAsync(cancellationToken);
            default:
                return null;
        }
    }

    public async Task<bool> DeleteFromFavouritesAsync(DeleteFavouritesRequest request, CancellationToken cancellationToken)
    {
        var favouritesToDelete = await _dbContext.Favourites.AsNoTracking().Where(f => f.FavId == request.Fav_Id).FirstOrDefaultAsync(cancellationToken);

        if(favouritesToDelete == null)
        {
            return false;
        }

        _dbContext.Favourites.Remove(favouritesToDelete);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
