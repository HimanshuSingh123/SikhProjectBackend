using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Src.Application.Interfaces;
using Src.Domain.Course;
using Src.Domain.Couse;
using Src.Domain.Entities;
using Src.Domain.Entities.AbstractEntities;
using Src.Infrastructure.Persistance;

namespace Src.Infrastructure.Repository;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _dbContext;
    public CourseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public async Task<bool> CreateCourse(CreateCourseRequest request, CancellationToken cancellationToken)
    {
        var submissionIdExist = await _dbContext.Submission.AsNoTracking().AnyAsync(s => s.SubmissionId == request.SubmissionId, cancellationToken);

        if (!submissionIdExist)
        {
            return false;
        }

        var course = new Course
        {
            SubmissionId = request.SubmissionId,
            CourseName = request.CourseName,
            Description = request.Description,
            Image = request.Image,
            CourseType = request.Type,
            Price = request.Price
        };

        _dbContext.Course.Add(course);

        HandleLearningMaterial(request);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<GetCourseResponse?> GetCourse(int submissionId, CancellationToken cancellationToken)
    {
        var submissionIdExist = await _dbContext.Submission.AsNoTracking().AnyAsync(s => s.SubmissionId == submissionId, cancellationToken);

        if (!submissionIdExist)
        {
            return null;
        }

        var course = await _dbContext.Course.SingleOrDefaultAsync(c => c.SubmissionId == submissionId, cancellationToken);

        if (course == null)
        {
            return null;
        }

        GetCourseResponse courseToBeReturned = new GetCourseResponse
        {
            SubmissionId = submissionId,
            Description = course.Description,
            Image = course.Image,
            CourseName = course.CourseName,
            CourseType = course.CourseType,
            Price = course.Price
        };

        Dictionary<string, BaseCourseMaterial?> retrievedBaseCourseMaterials = new Dictionary<string, BaseCourseMaterial?>
        {
            { "Introduction_Material", await _dbContext.IntroductionMaterial.SingleOrDefaultAsync(im => im.SubmissionId == submissionId, cancellationToken) },
            { "WritingLesson_Material", await _dbContext.WritingLessonMaterial.SingleOrDefaultAsync(wl => wl.SubmissionId == submissionId, cancellationToken) },
            { "ReadingLesson_Material", await _dbContext.ReadingLessonMaterial.SingleOrDefaultAsync(rl => rl.SubmissionId == submissionId, cancellationToken) },
            { "SpeakingLesson_Material", await _dbContext.SpeakingLessonMaterial.SingleOrDefaultAsync(sl => sl.SubmissionId == submissionId, cancellationToken) },
            { "Conclusion_Material", await _dbContext.ConclusionMaterial.SingleOrDefaultAsync(cm => cm.SubmissionId == submissionId, cancellationToken) }
        };

        foreach (var baseCourseMaterial in retrievedBaseCourseMaterials)
        {
            if (baseCourseMaterial.Value == null)
            {
                continue;
            }

            if (baseCourseMaterial.Value.UploadedMaterial != null || baseCourseMaterial.Value.VideoMaterial != null)
            {
                switch (baseCourseMaterial.Key)
                {
                    case "Introduction_Material":
                        courseToBeReturned.IntroductionText = baseCourseMaterial.Value.UploadedMaterial;
                        courseToBeReturned.IntroductionVideo = baseCourseMaterial.Value.VideoMaterial;
                        break;

                    case "WritingLesson_Material":
                        courseToBeReturned.WritingLessonText = baseCourseMaterial.Value.UploadedMaterial;
                        courseToBeReturned.WritingLessonVideo = baseCourseMaterial.Value.VideoMaterial;
                        break;

                    case "ReadingLesson_Material":
                        courseToBeReturned.ReadingLessonText = baseCourseMaterial.Value.UploadedMaterial;
                        courseToBeReturned.ReadingLessonVideo = baseCourseMaterial.Value.VideoMaterial;
                        break;

                    case "SpeakingLesson_Material":
                        courseToBeReturned.SpeakingLessonText = baseCourseMaterial.Value.UploadedMaterial;
                        courseToBeReturned.SpeakingLessonVideo = baseCourseMaterial.Value.VideoMaterial;
                        break;

                    case "Conclusion_Material":
                        courseToBeReturned.ConclusionText = baseCourseMaterial.Value.UploadedMaterial;
                        courseToBeReturned.ConclusionVideo = baseCourseMaterial.Value.VideoMaterial;
                        break;

                    default:
                        throw new InvalidOperationException("Wrong key, wrong type of material");
                }
            }
        }

        return courseToBeReturned;
    }
    private void HandleLearningMaterial(CreateCourseRequest request)
    {
        var Materials = request.GetMaterials();
        foreach(var material in Materials)
        {
            if(material.Value.Text != null || material.Value.Video != null)
            {
                BaseCourseMaterial courseMaterial = material.Key switch
                {
                    "Introduction_Material" => new IntroductionMaterial { SubmissionId = request.SubmissionId },
                    "WritingLesson_Material" => new WritingLessonMaterial { SubmissionId = request.SubmissionId },
                    "ReadingLesson_Material" => new ReadingLessonMaterial { SubmissionId = request.SubmissionId },
                    "SpeakingLesson_Material" => new SpeakingLessonMaterial { SubmissionId = request.SubmissionId },
                    "Conclusion_Material" => new ConclusionMaterial { SubmissionId = request.SubmissionId },
                    _ => throw new InvalidOperationException("Wrong key, wrong type of material")
                };

                courseMaterial.UploadedMaterial = material.Value.Text != null ? material.Value.Text : [];
                courseMaterial.VideoMaterial = material.Value.Video != null ? material.Value.Video : [];
                courseMaterial.CreatedAt = DateTime.UtcNow;

                _dbContext.Add(courseMaterial);
            }
        }
    }
}

