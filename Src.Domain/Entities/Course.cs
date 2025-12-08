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
    public string Type { get; set; } = default!;
    public byte[] UploadedMaterial { get; set; } = default!;
    public double Price { get; set; }

    public Submission Submission { get; set; } = default!;
}

