using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Core.Request;

namespace UserApp.Data.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<GetUserRequest>> { }
}
