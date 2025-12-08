using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Domain.Entities;

public class Newsfeed
{
    public int SubmissionId { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public byte[] Image { get; set; } = default!;
    public bool Alert { get; set; }

    public Submission Submission { get; set; } = default!;
}

