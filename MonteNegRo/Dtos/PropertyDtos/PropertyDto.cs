using MonteNegRo.Dtos.Benefit_PropertyDtos;
using MonteNegRo.Dtos.BenefitDtos;
using MonteNegRo.Dtos.City_PropertyDtos;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.CounterpartyDtos;
using MonteNegRo.Dtos.Local_PropertyDtos;
using MonteNegRo.Dtos.NotificationDtos;
using MonteNegRo.Dtos.Photo_PropertyDtos;
using MonteNegRo.Dtos.PhotoDtos;
using MonteNegRo.Models;
using System;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.PropertyDtos
{
    public record PropertyDto
    {
        public long Property_ID { get; init; }
        public virtual IEnumerable<PhotoDto>? Photos { get; init; }
        public virtual IEnumerable<CityDto> Cities { get; init; }
        public virtual IEnumerable<BenefitDto>? Benefits { get; init; }
        public virtual IEnumerable<Local_PropertyDto> Local_Properties { get; init; }
        public virtual IEnumerable<NotificationDto>? Notifications { get; init; }
        public virtual CounterpartyDto? Counterparty { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
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
        //public bool IsAHouse { get; init; }
        //public bool IsLand { get; init; }
        //public bool IsCommercial { get; init; }
        public string Type { get; init; }
    }
}
