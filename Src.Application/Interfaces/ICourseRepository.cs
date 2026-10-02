using Src.Domain.Course;
using Src.Dto.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Interfaces;

public interface ICourseRepository
{
    public Task<GetCourseResponse?> GetCourse(int submissionId, CancellationToken cancellationToken);
    public Task<bool> CreateCourse(CreateCourseRequest request, CancellationToken cancellationToken);
    public Task<GetDownloadedLessonsResponse?> GetDownloadableLessons(GetDownloadedLessonRequest request, CancellationToken cancellationToken);
    public Task<bool> UploadLesson(AddLessonMaterialRequest request, CancellationToken cancellationToken);
    public Task<IEnumerable<GetCourseResponse>> GetUsersRegisteredCourses(string username, CancellationToken cancellationToken);
    public Task<bool> RegisterUser(string username, int submissionId, CancellationToken cancellationToken);
    public Task<bool> UpdateCourse(UpdateCourseRequest request, CancellationToken cancellationToken);
}

