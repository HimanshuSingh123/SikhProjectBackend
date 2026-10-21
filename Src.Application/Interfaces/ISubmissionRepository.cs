using Src.Domain.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Interfaces;

public interface ISubmissionRepository
{
    Task<int> CreateSubmissionHelper(string username, CreateSubmissionRequest request, CancellationToken cancellationToken);
}

