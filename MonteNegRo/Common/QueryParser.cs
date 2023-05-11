using Microsoft.AspNetCore.Builder;
using MonteNegRo.Filters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using System.Web;
using System;
using System.Transactions;
using MonteNegRo.Dtos.Queries;

namespace MonteNegRo.Common
{
    public static class QueryParser
    {
        public static PaginationFilter ParseQueryForPageFilters(BasePaginatedQuery query, int defaultPageSize)
        {
            int pageSize = defaultPageSize;
            int pageNumber = query.PageNumber;
            if (query.PageSize > 0)
            {
                pageSize = query.PageSize;
            }

            var paginationFilter = new PaginationFilter(pageNumber, pageSize);
            return paginationFilter;
        }
    }
}
