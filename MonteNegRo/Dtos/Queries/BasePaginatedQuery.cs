using Microsoft.AspNetCore.Mvc;

namespace MonteNegRo.Dtos.Queries
{
    public abstract class BasePaginatedQuery
    {
        [FromQuery(Name = "page-size")]
        public int PageSize { get; init; }

        [FromQuery(Name = "page-number")]
        public int PageNumber { get; init; }
        [FromQuery(Name = "search")]
        public string? Search { get; init; }

    }
}
