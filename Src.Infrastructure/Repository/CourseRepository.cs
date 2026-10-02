using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Src.Application.Interfaces;
using Src.Domain.Course;
using Src.Domain.Entities;
using Src.Domain.Entities.AbstractEntities;
using Src.Dto.Course;
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

        var course = await _dbContext.Course
                .Include(c => c.IntroductionMaterial)
                .Include(c => c.WritingLessonMaterial)
                .Include(c => c.ReadingLessonMaterial)
                .Include(c => c.SpeakingLessonMaterial)
                .Include(c => c.ConclusionMaterial)
                .SingleOrDefaultAsync(c => c.SubmissionId == submissionId, cancellationToken);

        if (course == null)
        {
            return null;
        }

        return new GetCourseResponse
        {
            SubmissionId = course.SubmissionId,
            CourseName = course.CourseName,
            Description = course.Description,
            Image = course.Image,
            CourseType = course.CourseType,
            Price = course.Price,
            IntroductionText = course.IntroductionMaterial?.UploadedMaterial,
            IntroductionVideo = course.IntroductionMaterial?.VideoMaterial,
            WritingLessonText = course.WritingLessonMaterial?.UploadedMaterial,
            WritingLessonVideo = course.WritingLessonMaterial?.VideoMaterial,
            ReadingLessonText = course.ReadingLessonMaterial?.UploadedMaterial,
            ReadingLessonVideo = course.ReadingLessonMaterial?.VideoMaterial,
            SpeakingLessonText = course.SpeakingLessonMaterial?.UploadedMaterial,
            SpeakingLessonVideo = course.SpeakingLessonMaterial?.VideoMaterial,
            ConclusionText = course.ConclusionMaterial?.UploadedMaterial,
            ConclusionVideo = course.ConclusionMaterial?.VideoMaterial
        };

    }

    public async Task<GetDownloadedLessonsResponse?> GetDownloadableLessons(GetDownloadedLessonRequest request, CancellationToken cancellationToken)
    {
        var submissionIdExist = await _dbContext.Submission.AsNoTracking().AnyAsync(s => s.SubmissionId == request.SubmissionId, cancellationToken);

        if (!submissionIdExist)
        {
            return null;
        }

        switch (request.LessonType)
        {
            case "Introduction_Material":
                var introductionLessons = await _dbContext.IntroductionMaterial.AsNoTracking().SingleOrDefaultAsync(im => im.SubmissionId == request.SubmissionId);
                return introductionLessons is null ? null : CreateDownloadedLearningLessonResponse(introductionLessons);

            case "WritingLesson_Material":
                var writingLessons = await _dbContext.WritingLessonMaterial.AsNoTracking().SingleOrDefaultAsync(wl => wl.SubmissionId == request.SubmissionId);
                return writingLessons is null ? null : CreateDownloadedLearningLessonResponse(writingLessons);

            case "ReadingLesson_Material":
                var readingLessons = await _dbContext.ReadingLessonMaterial.AsNoTracking().SingleOrDefaultAsync(rl => rl.SubmissionId == request.SubmissionId);
                return readingLessons is null ? null : CreateDownloadedLearningLessonResponse(readingLessons);

            case "SpeakingLesson_Material":
                var speakingLessons = await _dbContext.SpeakingLessonMaterial.AsNoTracking().SingleOrDefaultAsync(sl => sl.SubmissionId == request.SubmissionId);
                return speakingLessons is null ? null : CreateDownloadedLearningLessonResponse(speakingLessons);

            case "Conclusion_Material":
                var conclusionLessons = await _dbContext.ConclusionMaterial.AsNoTracking().SingleOrDefaultAsync(cm => cm.SubmissionId == request.SubmissionId);
                return conclusionLessons is null ? null : CreateDownloadedLearningLessonResponse(conclusionLessons);

            default:
                throw new InvalidOperationException("Wrong key, wrong type of material");
        }
    }

    public async Task<bool> UploadLesson(AddLessonMaterialRequest request, CancellationToken cancellationToken)
    {
        var submissionIdExist = await _dbContext.Submission.AsNoTracking().AnyAsync(s => s.SubmissionId == request.SubmissionId, cancellationToken);

        if (!submissionIdExist)
        {
            return false;
        }
        var materials = request.GetMaterials();
        foreach (var material in materials)
        {
            switch (material.Key)
            {
                case "Introduction_Material":
                    var introductionLessons = await _dbContext.IntroductionMaterial.SingleOrDefaultAsync(im => im.SubmissionId == request.SubmissionId);
                    if (introductionLessons is null)
                    {
                        continue;
                    }
                    UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, introductionLessons);
                    break;

                case "WritingLesson_Material":
                    var writingLessons = await _dbContext.WritingLessonMaterial.SingleOrDefaultAsync(wl => wl.SubmissionId == request.SubmissionId, cancellationToken);
                    if (writingLessons is null)
                    {
                        continue;
                    }
                    UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, writingLessons);
                    break;

                case "ReadingLesson_Material":
                    var readingLessons = await _dbContext.ReadingLessonMaterial.SingleOrDefaultAsync(rl => rl.SubmissionId == request.SubmissionId, cancellationToken);
                    if (readingLessons is null)
                    {
                        continue;
                    }
                    UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, readingLessons);
                    break;

                case "SpeakingLesson_Material":
                    var speakingLessons = await _dbContext.SpeakingLessonMaterial.SingleOrDefaultAsync(sl => sl.SubmissionId == request.SubmissionId, cancellationToken);
                    if (speakingLessons is null)
                    {
                        continue;
                    }
                    UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, speakingLessons);
                    break;

                case "Conclusion_Material":
                    var conclusionLessons = await _dbContext.ConclusionMaterial.SingleOrDefaultAsync(cm => cm.SubmissionId == request.SubmissionId, cancellationToken);
                    if (conclusionLessons is null)
                    {
                        continue;
                    }
                    UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, conclusionLessons);
                    break;

                default:
                    throw new InvalidOperationException("Wrong key, wrong type of material");
            }
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private void UploadOrReplaceCourseLesson(byte[]? NewTextLesson, byte[]? NewVideoLesson, BaseCourseMaterial ExistingMaterial)
    {
        if (NewVideoLesson != null)
        {
            ExistingMaterial.VideoMaterial = NewVideoLesson;
        }
        if (NewTextLesson != null)
        {
            ExistingMaterial.UploadedMaterial = NewTextLesson;
        }
    }

    private GetDownloadedLessonsResponse? CreateDownloadedLearningLessonResponse(BaseCourseMaterial material)
    {
        if(material == null)
        {
            return null;
        }
        var res = new GetDownloadedLessonsResponse
        {
            LessonText = material.UploadedMaterial,
            LessonVideo = material.VideoMaterial,
        };
        return res;
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

    public async Task<IEnumerable<GetCourseResponse>> GetUsersRegisteredCourses(string username, CancellationToken cancellationToken)
    {
        var DoesUserHaveCourses = await _dbContext.RegisteredCourses.AnyAsync(rc => rc.Username == username);


        var registeredCourses = await _dbContext.RegisteredCourses.Where(rc => rc.Username == username).ToListAsync(cancellationToken);
        List<Course> courses = [];

        foreach (var registeredCourse in registeredCourses) {

            var course = await _dbContext.Course
                .Include(c => c.IntroductionMaterial)
                .Include(c => c.WritingLessonMaterial)
                .Include(c => c.ReadingLessonMaterial)
                .Include(c => c.SpeakingLessonMaterial)
                .Include(c => c.ConclusionMaterial)
                .SingleOrDefaultAsync(c => c.SubmissionId == registeredCourse.SubmissionId, cancellationToken);

            if (course == null)
            {
                continue;
            }

            courses.Add(course);
        }

        List<GetCourseResponse> responses = new List<GetCourseResponse>();

        foreach (var course in courses)
        {
            responses.Add(new GetCourseResponse
            {
                SubmissionId = course.SubmissionId,
                CourseName = course.CourseName,
                Description = course.Description,
                Image = course.Image,
                CourseType = course.CourseType,
                Price = course.Price,
                IntroductionText = course.IntroductionMaterial?.UploadedMaterial,
                IntroductionVideo = course.IntroductionMaterial?.VideoMaterial,
                WritingLessonText = course.WritingLessonMaterial?.UploadedMaterial,
                WritingLessonVideo = course.WritingLessonMaterial?.VideoMaterial,
                ReadingLessonText = course.ReadingLessonMaterial?.UploadedMaterial,
                ReadingLessonVideo = course.ReadingLessonMaterial?.VideoMaterial,
                SpeakingLessonText = course.SpeakingLessonMaterial?.UploadedMaterial,
                SpeakingLessonVideo = course.SpeakingLessonMaterial?.VideoMaterial,
                ConclusionText = course.ConclusionMaterial?.UploadedMaterial,
                ConclusionVideo = course.ConclusionMaterial?.VideoMaterial
            });
        }

        return responses;

    }

    public async Task<bool> RegisterUser(string username, int submissionId, CancellationToken cancellationToken)
    {
        var submissionIdExist = await _dbContext.Submission.AsNoTracking().AnyAsync(s => s.SubmissionId == submissionId, cancellationToken);

        if (!submissionIdExist)
        {
            return false;
        }

        var registeredCourse = new RegisteredCourses
        {
            SubmissionId = submissionId,
            Username = username,
        };

        _dbContext.RegisteredCourses.Add(registeredCourse);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> UpdateCourse(UpdateCourseRequest request, CancellationToken cancellationToken)
    {
        var course = await _dbContext.Course
            .Include(c => c.IntroductionMaterial)
            .Include(c => c.WritingLessonMaterial)
            .Include(c => c.ReadingLessonMaterial)
            .Include(c => c.SpeakingLessonMaterial)
            .Include(c => c.ConclusionMaterial)
            .SingleOrDefaultAsync(c => c.SubmissionId == request.SubmissionId, cancellationToken);


        var attributes = request.GetAttributes();
        var materials = request.GetMaterials();

        if (course == null)
        {
            return false;
        }


        foreach (var attribute in attributes)
        {
            if (attribute.Value == null)
            {
                continue;
            }

            if (attribute.Key == "CourseName")
            {
                course.CourseName = (string)attribute.Value;
            }
            else if (attribute.Key == "Description")
            {
                course.Description = (string)attribute.Value;
            }
            else if (attribute.Key == "Image")
            {
                course.Image = (byte[])attribute.Value;
            }
            else if (attribute.Key == "Type")
            {
                course.CourseType = (string)attribute.Value;
            }
            else if (attribute.Key == "Price")
            {
                course.Price = (double)attribute.Value;
            }
        }

        foreach (var material in materials)
        {
            if (material.Value.Text == null && material.Value.Video == null)
            {
                continue;
            }

            if (material.Key == "Introduction_Material")
            {
                course.IntroductionMaterial = UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, course.IntroductionMaterial, course.SubmissionId);
            }
            else if (material.Key == "WritingLesson_Material")
            {
                course.WritingLessonMaterial = UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, course.WritingLessonMaterial, course.SubmissionId);
            }
            else if (material.Key == "ReadingLesson_Material")
            {
                course.ReadingLessonMaterial = UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, course.ReadingLessonMaterial, course.SubmissionId);
            }
            else if (material.Key == "SpeakingLesson_Material")
            {
                course.SpeakingLessonMaterial = UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, course.SpeakingLessonMaterial, course.SubmissionId);
            }
            else if (material.Key == "Conclusion_Material")
            {
                course.ConclusionMaterial = UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, course.ConclusionMaterial, course.SubmissionId);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }


    private T UploadOrReplaceCourseLesson<T>(byte[]? newTextLesson, byte[]? newVideoLesson, T? existingMaterial, int submissionId)
        where T : BaseCourseMaterial, new()
        {
            if (existingMaterial == null)
            {
                existingMaterial = new T
                {
                    SubmissionId = submissionId,
                    UploadedMaterial = newTextLesson ?? [],
                    VideoMaterial = newVideoLesson ?? [],
                    CreatedAt = DateTime.UtcNow
                };

                return existingMaterial;
            }

            UploadOrReplaceCourseLesson(newTextLesson, newVideoLesson, existingMaterial);
            existingMaterial.ModifiedAt = DateTime.UtcNow;

            return existingMaterial;
        }
}

