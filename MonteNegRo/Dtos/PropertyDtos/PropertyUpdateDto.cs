using Microsoft.AspNetCore.Http;
using MonteNegRo.Dtos.BenefitDtos;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.Local_PropertyDtos;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.PropertyDtos
{
    public record PropertyUpdateDto
    {
        public PropertyUpdateDto()
        {
            City_IDs = new List<int>();
            Benefit_IDs = new List<int>();
            Photos = new List<PhotoPositionValue>();
            Local_Properties = new List<LocalPropertyValue>();
        }
        public virtual IEnumerable<PhotoPositionValue>? Photos { get; init; }
        public virtual IEnumerable<int> City_IDs { get; init; }
        public virtual IEnumerable<int>? Benefit_IDs { get; init; }
        public virtual IEnumerable<LocalPropertyValue> Local_Properties { get; init; }
        public long? Counterparty_ID { get; init; }
        public string? SaleCode { get; init; }
        public string? RentCode { get; init; }
        public string SaleStatus { get; init; }
        public string RentStatus { get; init; }
        public string? Notes { get; init; }
        public int? FloorsInABuilding { get; init; }
        public int? ConstructionYear { get; init; }
        public int? RoomCount { get; init; }
        public int? RentPrice { get; init; }
        public int? RentPriceBeforeSeason { get; init; }
        public int? RentPriceFullSeason { get; init; }
        public int? SalePrice { get; init; }
        public int? CounterAgentNumber { get; init; }
        public string? VideoID { get; init; }
        public bool RentPromoteStatus { get; init; }
        public bool SalePromoteStatus { get; init; }
        public bool Pets { get; init; }
        public bool TurkishKebabs { get; init; }
        public string? AdditionalInfo { get; init; }



        public double? CoordinateX { get; init; }
        public double? CoordinateY { get; init; }
        public int? BedroomCount { get; init; }
        public int? HouseAreaSquare { get; init; }
        public int? LandAreaSquare { get; init; }
        public int? Floor { get; init; }
        public int? BathroomCount { get; init; }
        //public int Price { get; init; }
        //public bool IsLongTerm { get; init; }
        public bool IsForSale { get; init; }
        public string Type { get; init; }
        //public bool IsAHouse { get; init; }
        //public bool IsLand { get; init; }
        //public bool IsCommercial { get; init; }
    }
}
