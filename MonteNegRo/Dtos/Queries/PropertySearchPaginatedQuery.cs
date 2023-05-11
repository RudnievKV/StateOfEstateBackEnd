using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.Queries
{
    public class PropertySearchPaginatedQuery : BasePaginatedQuery
    {
        [FromQuery(Name = "rental-period")]
        public string? RentalPeriod { get; init; }
        [FromQuery(Name = "cities")]
        public IEnumerable<int>? City_IDs { get; init; }
        [FromQuery(Name = "bedroom-number")]
        public IEnumerable<int>? BedroomCounts { get; init; }
        [FromQuery(Name = "benefits")]
        public IEnumerable<int>? Benefit_IDs { get; init; }
        [FromQuery(Name = "price-from")]
        public int? PriceFrom { get; init; }
        [FromQuery(Name = "price-to")]
        public int? PriceTo { get; init; }
        [FromQuery(Name = "type")]
        public string? Type { get; init; }
        [FromQuery(Name = "land-footage-from")]
        public int? LandFootageFrom { get; init; }
        [FromQuery(Name = "land-footage-to")]
        public int? LandFootageTo { get; init; }
        [FromQuery(Name = "for-sale")]
        public bool? IsForSale { get; init; }
        [FromQuery(Name = "sale-promote-status")]
        public bool? SalePromoteStatus { get; init; }
        [FromQuery(Name = "rent-promote-status")]
        public bool? RentPromoteStatus { get; init; }

    }
}
