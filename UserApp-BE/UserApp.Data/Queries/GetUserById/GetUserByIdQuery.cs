using MediatR;
using ErrorOr;

namespace UserApp.Data.Queries.GetUserById
{
    public sealed record GetUserByIdQuery(string UserId) : IRequest<ErrorOr<GetUserResponse>>;

}
