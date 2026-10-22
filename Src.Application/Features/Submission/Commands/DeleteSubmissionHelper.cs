using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Features.Submission.Commands;

public record DeleteSubmissionHelper(String Username, int SubmissionId) : IRequest<bool>;

