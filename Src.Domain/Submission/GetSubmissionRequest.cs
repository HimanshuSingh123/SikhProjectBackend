using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Domain.Submission;
/* 
 * I think how this is going to work, in the handler is that I'll just retrieve the required type of cateogricla submissions
 * send those back to the handler of this reqeust. each of the records being sent back would have their own type of record which then
 * the handler would pick up and call GET repo methods from the collection's domain instead of doing it in the submission records and built the response object.
 * 
 * 
 * */
public record GetSubmissionRequest
{
    public required String Category { get; init; }
    public required int PageSize { get; init; } = 20;
    public required int Page { get; init; } = 1;
}

