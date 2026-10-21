using MediatR;
using Src.Domain.Submission;

namespace Src.Application.Features.Submission.Commands;

public record CreateSubmissionHelperCommand(string Username, CreateSubmissionRequest Request) : IRequest<int>;

