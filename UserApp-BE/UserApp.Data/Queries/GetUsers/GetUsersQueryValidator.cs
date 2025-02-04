using FluentValidation;

namespace UserApp.Data.Queries.GetUsers;

public class GetUsersQueryValidator :AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
        RuleFor(query => query.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page must be at least 1.");

        RuleFor(query => query.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than 0.");

        RuleFor(query => query.SortColumn)
            .Must(column => new[] { "Email", "FirstName", "LastName" }
                .Any(validColumn => string.Equals(column, validColumn, StringComparison.OrdinalIgnoreCase)))
            .WithMessage("Sort column must be one of: " +
                         "Email, FirstName, LastName.")
            .When(query => query.SortColumn is not null);

        RuleFor(query => query.SortDirection)
            .Must(direction => string.IsNullOrEmpty(direction) || direction == "asc" || direction == "desc")
            .WithMessage("SortDirection must be 'asc' or 'desc'.");
    }
}