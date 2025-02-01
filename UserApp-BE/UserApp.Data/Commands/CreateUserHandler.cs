using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Core.Models;
using UserApp.Core.Request;

namespace UserApp.Data.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ErrorOr<GetUserRequest>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<GetUserRequest>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                Email = request.User.Email,
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                UserName= request.User.Email
            };

            var result = await _userManager.CreateAsync(user, request.User.Password);
            if (!result.Succeeded)
            {
                var identityErrors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
                return identityErrors;
            }

            return new GetUserRequest
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
