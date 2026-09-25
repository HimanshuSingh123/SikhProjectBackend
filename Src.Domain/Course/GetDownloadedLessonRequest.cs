using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Domain.Course;

public record GetDownloadedLessonRequest
{
    public required int SubmissionId { get; init; }
    public required string LessonType { get; init; }
}


