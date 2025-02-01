using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Core.Request;

namespace UserApp.Data.Commands
{
    public class CreateUserCommand : IRequest<ErrorOr<GetUserRequest>>
    {
        public RegisterUserRequest User { get; set; }

        public CreateUserCommand(RegisterUserRequest user)
        {
            User = user;
        }
    }
}
