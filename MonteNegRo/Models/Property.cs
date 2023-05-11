
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonteNegRo.Models
{
    public record Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Property_ID { get; init; }
        public virtual IEnumerable<Photo_Property>? Photo_Properties { get; init; }
        public virtual IEnumerable<City_Property> City_Properties { get; init; }
        public virtual IEnumerable<Benefit_Property>? Benefit_Properties { get; init; }
        public virtual IEnumerable<Local_Property>? Local_Properties { get; init; }
        public virtual IEnumerable<Notification>? Notifications { get; init; }
        public virtual IEnumerable<AdvertisementSetting>? AdvertisementSettings { get; init; }
        public virtual Counterparty? Counterparty { get; init; }
        public long? Counterparty_ID { get; init; }




        public string? SaleCode { get; init; }
        public string? RentCode { get; init; }
        public enum PropertyStatus
        {
            Published,
            NotActive,
            Draft,
            Archived,
        }
        public PropertyStatus SaleStatus { get; init; }
        public PropertyStatus RentStatus { get; init; }
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
        public DateTimeOffset CreatedDate { get; init; }


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
        public enum PropertyType
        {
            Apartment,
            House,
            Land,
            Commercial
        }
        public PropertyType Type { get; init; }
        //public bool IsAHouse { get; init; }
        //public bool IsLand { get; init; }
        //public bool IsCommercial { get; init; }


    }
}
