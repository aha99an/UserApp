using MediatR;
using UserApp.Core.Request;

namespace UserApp.Data.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<GetUserRequest>
    {
        public string UserId { get; set; }

        public GetUserByIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
