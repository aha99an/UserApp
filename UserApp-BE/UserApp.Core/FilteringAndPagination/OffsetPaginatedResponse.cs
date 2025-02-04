namespace UserApp.Core.FilteringAndPagination;

public sealed record OffsetPaginatedResponse<TResponse>(
    int TotalCount,
    TResponse Response);
