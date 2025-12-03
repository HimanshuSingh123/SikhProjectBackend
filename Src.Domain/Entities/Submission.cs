using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Domain.Entities;

public class Submission
{
    public int SubmissionId { get; set; }
    public string Username { get; set; } = default!;
    public DateTime DateSubmitted { get; set; }
    public string Status { get; set; } = default!;
    public string Category { get; set; } = default!;
}

