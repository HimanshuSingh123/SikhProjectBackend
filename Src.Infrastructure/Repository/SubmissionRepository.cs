using Microsoft.EntityFrameworkCore;
using Src.Application.Interfaces;
using Src.Domain.Entities;
using Src.Domain.Submission;
using Src.Infrastructure.Persistance;

namespace Src.Infrastructure.Repository;

public class SubmissionRepository : ISubmissionRepository
{
    private readonly ApplicationDbContext _dbContext;
    public SubmissionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public async Task<int> CreateSubmissionHelper(string username, CreateSubmissionRequest request, CancellationToken cancellationToken)
    {
        Submission submission = new Submission
        {
            Username = username,
            DateSubmitted = DateTime.UtcNow,
            Category = request.Category,
            Status = request.Status,
        };

        await _dbContext.Submission.AddAsync(submission);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return submission.SubmissionId;
    }

    public async Task<bool> DeleteSubmission(int submissionId, CancellationToken cancellationToken)
    {
        var submissionIdFetched = await _dbContext.Submission.SingleOrDefaultAsync(s => s.SubmissionId == submissionId);

        if (submissionIdFetched == null)
        {
            return false;
        }

        _dbContext.Submission.Remove(submissionIdFetched);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}

