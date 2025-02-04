using MediatR;
using UserApp.Core.FilteringAndPagination;
using ErrorOr;

namespace UserApp.Data.Queries.GetUsers
{
    public sealed record GetUsersQuery(
        int Page,
        int PageSize,
        string Search,
        string SortColumn,
        string SortDirection
    ) : IRequest<ErrorOr<OffsetPaginatedResponse<List<GetUserResponse>>>>;
}
