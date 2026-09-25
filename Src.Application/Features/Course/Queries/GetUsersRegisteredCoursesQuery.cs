using MediatR;
using Src.Domain.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Features.Course.Queries;

public record GetUsersRegisteredCoursesQuery(string Username) : IRequest<IReadOnlyList<GetCourseResponse>>;
    
    

