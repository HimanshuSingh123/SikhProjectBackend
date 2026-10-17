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
            Image = previewBytes(course.Image),
            CourseType = course.CourseType,
            Price = course.Price,
            IntroductionText = course.IntroductionMaterial?.UploadedMaterial is { } introductionText ? previewBytes(introductionText) : null,
            IntroductionVideo = course.IntroductionMaterial?.VideoMaterial is { } introductionVideo ? previewBytes(introductionVideo) : null,
            WritingLessonText = course.WritingLessonMaterial?.UploadedMaterial is { } writingText ? previewBytes(writingText) : null,
            WritingLessonVideo = course.WritingLessonMaterial?.VideoMaterial is { } writingVideo ? previewBytes(writingVideo) : null,
            ReadingLessonText = course.ReadingLessonMaterial?.UploadedMaterial is { } readingText ? previewBytes(readingText) : null,
            ReadingLessonVideo = course.ReadingLessonMaterial?.VideoMaterial is { } readingVideo ? previewBytes(readingVideo) : null,
            SpeakingLessonText = course.SpeakingLessonMaterial?.UploadedMaterial is { } speakingText ? previewBytes(speakingText) : null,
            SpeakingLessonVideo = course.SpeakingLessonMaterial?.VideoMaterial is { } speakingVideo ? previewBytes(speakingVideo) : null,
            ConclusionText = course.ConclusionMaterial?.UploadedMaterial is { } conclusionText ? previewBytes(conclusionText) : null,
            ConclusionVideo = course.ConclusionMaterial?.VideoMaterial is { } conclusionVideo ? previewBytes(conclusionVideo) : null
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
            bool createdNow = false;
            switch (material.Key)
            {
                case "Introduction_Material":
                    if(material.Value.Text == null && material.Value.Video == null)
                    {
                        continue;
                    }
                    var introductionLessons = await _dbContext.IntroductionMaterial.SingleOrDefaultAsync(im => im.SubmissionId == request.SubmissionId);
                    var resultIntroductionMaterial = BaseCourseMaterialCheck<IntroductionMaterial>(introductionLessons!, request.SubmissionId, createdNow);
                    UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, resultIntroductionMaterial.newCourseMaterial, resultIntroductionMaterial.createdNow);
                    if (resultIntroductionMaterial.createdNow)
                    {
                        _dbContext.Add(resultIntroductionMaterial.newCourseMaterial);
                    }
                    break;

                case "WritingLesson_Material":
                    if (material.Value.Text == null && material.Value.Video == null)
                    {
                        continue;
                    }
                    var writingLessons = await _dbContext.WritingLessonMaterial.SingleOrDefaultAsync(wl => wl.SubmissionId == request.SubmissionId, cancellationToken);
                    var resultWritingLessonMaterial = BaseCourseMaterialCheck<WritingLessonMaterial>(writingLessons!, request.SubmissionId, createdNow);
                    UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, resultWritingLessonMaterial.newCourseMaterial, resultWritingLessonMaterial.createdNow);
                    if (resultWritingLessonMaterial.createdNow)
                    {
                        _dbContext.Add(resultWritingLessonMaterial.newCourseMaterial);
                    }
                    break;

                case "ReadingLesson_Material":
                    if (material.Value.Text == null && material.Value.Video == null)
                    {
                        continue;
                    }
                    var readingLessons = await _dbContext.ReadingLessonMaterial.SingleOrDefaultAsync(rl => rl.SubmissionId == request.SubmissionId, cancellationToken);
                    var resultReadingLessonMaterial = BaseCourseMaterialCheck<ReadingLessonMaterial>(readingLessons!, request.SubmissionId, createdNow);
                    UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, resultReadingLessonMaterial.newCourseMaterial, resultReadingLessonMaterial.createdNow);
                    if (resultReadingLessonMaterial.createdNow)
                    {
                        _dbContext.Add(resultReadingLessonMaterial.newCourseMaterial);
                    }
                    break;

                case "SpeakingLesson_Material":
                    if (material.Value.Text == null && material.Value.Video == null)
                    {
                        continue;
                    }
                    var speakingLessons = await _dbContext.SpeakingLessonMaterial.SingleOrDefaultAsync(sl => sl.SubmissionId == request.SubmissionId, cancellationToken);
                    var resultSpeakingLessonMaterial = BaseCourseMaterialCheck<SpeakingLessonMaterial>(speakingLessons!, request.SubmissionId, createdNow);
                    UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, resultSpeakingLessonMaterial.newCourseMaterial, resultSpeakingLessonMaterial.createdNow);
                    if (resultSpeakingLessonMaterial.createdNow)
                    {
                        _dbContext.Add(resultSpeakingLessonMaterial.newCourseMaterial);
                    }
                    break;

                case "Conclusion_Material":
                    if (material.Value.Text == null && material.Value.Video == null)
                    {
                        continue;
                    }
                    var conclusionLessons = await _dbContext.ConclusionMaterial.SingleOrDefaultAsync(cm => cm.SubmissionId == request.SubmissionId, cancellationToken);
                    var resultConclusionMaterial = BaseCourseMaterialCheck<ConclusionMaterial>(conclusionLessons!, request.SubmissionId, createdNow);
                    UploadOrReplaceCourseLesson(material.Value.Text, material.Value.Video, resultConclusionMaterial.newCourseMaterial, resultConclusionMaterial.createdNow);
                    if (resultConclusionMaterial.createdNow)
                    {
                        _dbContext.Add(resultConclusionMaterial.newCourseMaterial);
                    }
                    break;

                default:
                    throw new InvalidOperationException("Wrong key, wrong type of material");
            }
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private (T newCourseMaterial, bool createdNow) BaseCourseMaterialCheck<T>(BaseCourseMaterial courseMaterial, int submissionId, bool createdNow) where T : BaseCourseMaterial, new()
    {
        if (courseMaterial is null)
        {
            T newCourseMaterial = new T
            {
                SubmissionId = submissionId,
                CreatedAt = DateTime.UtcNow,
            };
            createdNow = true;
            return (newCourseMaterial, createdNow);
        }
        return ((T)courseMaterial, false);
    }

    private void UploadOrReplaceCourseLesson(byte[]? NewTextLesson, byte[]? NewVideoLesson, BaseCourseMaterial ExistingMaterial, bool createdNow)
    {
        if (NewVideoLesson != null)
        {
            //testing purposes
            if (ExistingMaterial.VideoMaterial.SequenceEqual(NewVideoLesson))
            {
                System.Diagnostics.Debug.WriteLine("existing video material is the same as new video lesson");
            }
            ExistingMaterial.VideoMaterial = NewVideoLesson;
            if (!createdNow)
            {
                ExistingMaterial.ModifiedAt = DateTime.UtcNow;
            }
        }
        if (NewTextLesson != null)
        {
            if (ExistingMaterial.UploadedMaterial.SequenceEqual(NewTextLesson))
            {
                System.Diagnostics.Debug.WriteLine("existing Text material is the same as new Text lesson");
            }
            ExistingMaterial.UploadedMaterial = NewTextLesson;
            //testing purposes
            if (!createdNow)
            {
                ExistingMaterial.ModifiedAt = DateTime.UtcNow;
            }
        }
    }

    private GetDownloadedLessonsResponse? CreateDownloadedLearningLessonResponse(BaseCourseMaterial material)
    {
        if (material == null)
        {
            return null;
        }
        var res = new GetDownloadedLessonsResponse
        {
            LessonText = material.UploadedMaterial is { } lessonText ? previewBytes(lessonText) : null,
            LessonVideo = material.VideoMaterial is { } lessonVideo ? previewBytes(lessonVideo) : null,
        };
        return res;
    }

    private void HandleLearningMaterial(CreateCourseRequest request)
    {
        var Materials = request.GetMaterials();
        foreach (var material in Materials)
        {
            if (material.Value.Text != null || material.Value.Video != null)
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

        foreach (var registeredCourse in registeredCourses)
        {

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
                Image = previewBytes(course.Image),
                CourseType = course.CourseType,
                Price = course.Price,
                IntroductionText = course.IntroductionMaterial?.UploadedMaterial is { } introductionText ? previewBytes(introductionText) : null,
                IntroductionVideo = course.IntroductionMaterial?.VideoMaterial is { } introductionVideo ? previewBytes(introductionVideo) : null,
                WritingLessonText = course.WritingLessonMaterial?.UploadedMaterial is { } writingText ? previewBytes(writingText) : null,
                WritingLessonVideo = course.WritingLessonMaterial?.VideoMaterial is { } writingVideo ? previewBytes(writingVideo) : null,
                ReadingLessonText = course.ReadingLessonMaterial?.UploadedMaterial is { } readingText ? previewBytes(readingText) : null,
                ReadingLessonVideo = course.ReadingLessonMaterial?.VideoMaterial is { } readingVideo ? previewBytes(readingVideo) : null,
                SpeakingLessonText = course.SpeakingLessonMaterial?.UploadedMaterial is { } speakingText ? previewBytes(speakingText) : null,
                SpeakingLessonVideo = course.SpeakingLessonMaterial?.VideoMaterial is { } speakingVideo ? previewBytes(speakingVideo) : null,
                ConclusionText = course.ConclusionMaterial?.UploadedMaterial is { } conclusionText ? previewBytes(conclusionText) : null,
                ConclusionVideo = course.ConclusionMaterial?.VideoMaterial is { } conclusionVideo ? previewBytes(conclusionVideo) : null
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

        var userAlreadyRegistered = await _dbContext.RegisteredCourses.AnyAsync(rc => rc.Username == username && rc.SubmissionId == submissionId);

        if (userAlreadyRegistered)
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
                course.IntroductionMaterial = CreateOrUpdateLessonMaterial(material.Value.Text, material.Value.Video, course.IntroductionMaterial, course.SubmissionId);
            }
            else if (material.Key == "WritingLesson_Material")
            {
                course.WritingLessonMaterial = CreateOrUpdateLessonMaterial(material.Value.Text, material.Value.Video, course.WritingLessonMaterial, course.SubmissionId);
            }
            else if (material.Key == "ReadingLesson_Material")
            {
                course.ReadingLessonMaterial = CreateOrUpdateLessonMaterial(material.Value.Text, material.Value.Video, course.ReadingLessonMaterial, course.SubmissionId);
            }
            else if (material.Key == "SpeakingLesson_Material")
            {
                course.SpeakingLessonMaterial = CreateOrUpdateLessonMaterial(material.Value.Text, material.Value.Video, course.SpeakingLessonMaterial, course.SubmissionId);
            }
            else if (material.Key == "Conclusion_Material")
            {
                course.ConclusionMaterial = CreateOrUpdateLessonMaterial(material.Value.Text, material.Value.Video, course.ConclusionMaterial, course.SubmissionId);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    private byte[] previewBytes(byte[] bytes)
    {
        if(bytes.Length == 0)
        {
            return [];
        }
        int length = bytes.Length;
        return bytes[..Math.Min(length, 10)];
    }


    private T CreateOrUpdateLessonMaterial<T>(byte[]? newTextLesson, byte[]? newVideoLesson, T? existingMaterial, int submissionId)
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

        UploadOrReplaceCourseLesson(newTextLesson, newVideoLesson, existingMaterial, false);

        return existingMaterial;
    }
}