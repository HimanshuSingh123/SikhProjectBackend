using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Domain.Entities;

public class Course
{
    public int SubmissionId { get; set; }
    public string CourseName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public byte[] Image { get; set; } = default!;
    public string CourseType { get; set; } = default!;
    public double Price { get; set; }

    public Submission Submission { get; set; } = default!;
    public IntroductionMaterial? IntroductionMaterial { get; set; }
    public WritingLessonMaterial? WritingLessonMaterial { get; set; }
    public SpeakingLessonMaterial? SpeakingLessonMaterial { get; set; }
    public ReadingLessonMaterial? ReadingLessonMaterial { get; set; }
    public ConclusionMaterial? ConclusionMaterial { get; set; }
    public ICollection<RegisteredCourses> RegisteredCourses { get; set; } = [];
}

