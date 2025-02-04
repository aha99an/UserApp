namespace UserApp.Core.FilteringAndPagination;

public interface IOffsetPaginationRequest
{
    public int Page { get; init; }

    public int PageSize { get; init; }
}