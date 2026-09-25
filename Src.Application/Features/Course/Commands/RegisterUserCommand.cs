using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Features.Course.Commands;

public record RegisterUserCommand(string Username, int SubmissionId) : IRequest<bool>;

