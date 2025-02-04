using MediatR;
using Microsoft.AspNetCore.Identity;
using UserApp.Core.Models;
using ErrorOr;

namespace UserApp.Data.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ErrorOr<GetUserResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserByIdHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<GetUserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return Error.NotFound("User.NotFound", $"User with ID {request.UserId} was not found.");
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
