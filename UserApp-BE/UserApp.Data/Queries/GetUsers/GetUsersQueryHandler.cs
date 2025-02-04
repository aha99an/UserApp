using MediatR;
using Microsoft.EntityFrameworkCore;
using UserApp.Core.FilteringAndPagination;
using UserApp.Data.Context;
using System.Linq.Dynamic.Core;
using ErrorOr;

namespace UserApp.Data.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<OffsetPaginatedResponse<List<GetUserResponse>>>>
    {
        private readonly ApplicationDbContext _context;

        public GetUsersQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<OffsetPaginatedResponse<List<GetUserResponse>>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(u => u.FirstName.Contains(request.Search) ||
                                         u.LastName.Contains(request.Search) ||
                                         u.Email.Contains(request.Search));
            }

            if (!string.IsNullOrWhiteSpace(request.SortColumn) && !string.IsNullOrWhiteSpace(request.SortDirection))
            {
                query = query.OrderBy($"{request.SortColumn} {request.SortDirection}");
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var users = await query.Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new GetUserResponse
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                })
                .ToListAsync(cancellationToken);

            return new OffsetPaginatedResponse<List<GetUserResponse>>(totalCount, users);
        }
    }
}
