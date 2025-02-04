using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserApp.Core.Models;
using UserApp.Data.Queries;

namespace UserApp.Data.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ErrorOr<GetUserResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<GetUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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

            return new GetUserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
