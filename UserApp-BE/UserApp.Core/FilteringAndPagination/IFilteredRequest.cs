namespace UserApp.Core.FilteringAndPagination;

public interface IFilteredRequest
{
    public string? SortColumn { get; init; }

    public string? SortDirection { get; init; }

    public string? Search { get; init; }
}