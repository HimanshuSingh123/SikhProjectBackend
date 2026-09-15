using Src.Domain.Course;
using Src.Domain.Couse;
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
}

