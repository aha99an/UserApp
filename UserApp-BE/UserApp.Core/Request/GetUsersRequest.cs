using UserApp.Core.FilteringAndPagination;

namespace UserApp.Core.Request;

public class GetUsersRequest : IOffsetPaginationRequest, IFilteredRequest
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SortColumn { get; init; }
    public string? SortDirection { get; init; }
    public string? Search { get; init; }
}