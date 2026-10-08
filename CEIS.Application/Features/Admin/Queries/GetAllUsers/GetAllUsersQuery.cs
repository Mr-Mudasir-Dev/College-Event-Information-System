using CEIS.Application.Common.Models;
using CEIS.Application.Features.Feedbacks.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Admin.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<Result<IEnumerable<UserListDto>>>
    {
    }
}
