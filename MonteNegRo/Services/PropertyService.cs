using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonteNegRo.Common;
using MonteNegRo.DBContext;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Filters;
using MonteNegRo.Models;
using MonteNegRo.Models.Azure;
using MonteNegRo.Services.Interfaces;
using MonteNegRo.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static MonteNegRo.Models.Local_Property;
using static MonteNegRo.Models.Property;

namespace MonteNegRo.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly MyDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAzureStorageService _azureStorageService;
        public PropertyService(MyDBContext dbContext, IMapper mapper, IAzureStorageService azureStorageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _azureStorageService = azureStorageService;
        }
        public async Task<IEnumerable<PropertyDto>> GetAllProperties()
        {
            var properties = await GetPropertiesWithAllInformation(_dbContext.Properties);
            var propertyDtos = new List<PropertyDto>();
            foreach (var property in properties)
            {
                var propertyDto = _mapper.Map<PropertyDto>(property);
                propertyDtos.Add(propertyDto);
            }
            return propertyDtos;
        }
        private async Task<IEnumerable<Property>> GetPropertiesWithAllInformation(IQueryable<Property> propertiesIQueryable)
        {
            return await _dbContext.Properties
                .Include(s => s.Benefit_Properties).ThenInclude(s => s.Benefit)
                .ThenInclude(s => s.Local_Benefits).ThenInclude(s => s.Local)
                .Include(s => s.City_Properties).ThenInclude(s => s.City)
                .ThenInclude(s => s.Local_Cities).ThenInclude(s => s.Local)
                .Include(s => s.Photo_Properties).ThenInclude(s => s.Photo)
                .Include(s => s.Local_Properties).ThenInclude(s => s.Local)
                .Include(s => s.City_Properties).ThenInclude(s => s.City)
                .ThenInclude(s => s.Neighborhoods).ThenInclude(s => s.Local_Neighborhoods)
                .ThenInclude(s => s.Local)
                .ToListAsync();
        }
        private async Task<Property> GetPropertyWithAllInformation(long id)
        {
            return await _dbContext.Properties
                .Include(s => s.Benefit_Properties).ThenInclude(s => s.Benefit)
                .ThenInclude(s => s.Local_Benefits).ThenInclude(s => s.Local)
                .Include(s => s.City_Properties).ThenInclude(s => s.City)
                .ThenInclude(s => s.Local_Cities).ThenInclude(s => s.Local)
                .Include(s => s.Photo_Properties).ThenInclude(s => s.Photo)
                .Include(s => s.Local_Properties).ThenInclude(s => s.Local)
                .Include(s => s.City_Properties).ThenInclude(s => s.City)
                .ThenInclude(s => s.Neighborhoods).ThenInclude(s => s.Local_Neighborhoods)
                .ThenInclude(s => s.Local)
                .Where(s => s.Property_ID == id)
                .SingleOrDefaultAsync();
        }
        public async Task<(IEnumerable<PropertyDto> propertyDtos,
            PaginationFilter filter,
            int totalRecords)>
            GetAllPropertiesPaged(PropertyPaginatedQuery query)
        {
            var paginationFilter = QueryParser.ParseQueryForPageFilters(query, 40);






            var propertiesIQueryable = _dbContext.Properties
                .OrderBy(s => s.Property_ID)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);
            var totalRecords = await propertiesIQueryable.CountAsync();

            var properties = await GetPropertiesWithAllInformation(propertiesIQueryable);

            var propertyDtos = new List<PropertyDto>();
            foreach (var property in properties)
            {
                var propertyDto = _mapper.Map<PropertyDto>(property);
                propertyDtos.Add(propertyDto);
            }
            return (propertyDtos, paginationFilter, totalRecords);
        }

        public async Task<PropertyDto> GetProperty(long id)
        {
            var property = await GetPropertyWithAllInformation(id);
            var propertyDto = _mapper.Map<PropertyDto>(property);
            return propertyDto;
        }
        public async Task<IEnumerable<PropertyDto>> GetSimilarProperties(long id)
        {
            var currentProperty = await _dbContext.Properties
                .Include(s => s.City_Properties).ThenInclude(s => s.City)
                .SingleOrDefaultAsync(s => s.Property_ID == id);

            var mainPredicate = PredicateBuilder.True<Property>();

            var cityID = currentProperty.City_Properties.First().City_ID;
            mainPredicate = mainPredicate.AND(s => s.City_Properties.First().City_ID == cityID);
            mainPredicate = mainPredicate.AND(s => s.Type == currentProperty.Type);


            if (currentProperty.IsForSale)
            {
                var predicateRentalPeriod = PredicateBuilder.False<Property>();

                predicateRentalPeriod = predicateRentalPeriod.OR(s => s.SalePrice <= currentProperty.SalePrice * 2);
                predicateRentalPeriod = predicateRentalPeriod.OR(s => s.SalePrice > currentProperty.SalePrice / 2);
                predicateRentalPeriod = predicateRentalPeriod.OR(s => s.IsForSale == true);

                mainPredicate = mainPredicate.AND(predicateRentalPeriod);
                mainPredicate = mainPredicate.AND(s => s.IsForSale == true);
            }
            else
            {
                var predicateRentalPeriod = PredicateBuilder.False<Property>();

                predicateRentalPeriod = predicateRentalPeriod.OR(s => s.RentPrice <= currentProperty.RentPrice * 2);
                predicateRentalPeriod = predicateRentalPeriod.OR(s => s.RentPrice > currentProperty.RentPrice / 2);


                predicateRentalPeriod = predicateRentalPeriod.OR(s => s.RentPriceFullSeason <= currentProperty.RentPriceFullSeason * 2);
                predicateRentalPeriod = predicateRentalPeriod.OR(s => s.RentPriceFullSeason > currentProperty.RentPriceFullSeason / 2);


                predicateRentalPeriod = predicateRentalPeriod.OR(s => s.RentPriceFullSeason <= currentProperty.RentPriceFullSeason * 2);
                predicateRentalPeriod = predicateRentalPeriod.OR(s => s.RentPriceFullSeason > currentProperty.RentPriceFullSeason / 2);


                mainPredicate = mainPredicate.AND(predicateRentalPeriod);
                mainPredicate = mainPredicate.AND(s => s.IsForSale == false);
            }


            var properties = await GetPropertiesWithAllInformation(_dbContext.Properties.Where(mainPredicate));



            var propertiesDtos = new List<PropertyDto>();
            foreach (var property in properties)
            {
                var propertyDto = _mapper.Map<PropertyDto>(property);
                propertiesDtos.Add(propertyDto);
            }
            return propertiesDtos;
        }
        public async Task<PropertyDto> CreateProperty(PropertyCreateDto propertyCreateDto)
        {
            var newProperty = new Property()
            {
                HouseAreaSquare = propertyCreateDto.HouseAreaSquare,
                Type = Enum.Parse<PropertyType>(propertyCreateDto.Type),
                LandAreaSquare = propertyCreateDto.LandAreaSquare,
                BathroomCount = propertyCreateDto.BathroomCount,
                BedroomCount = propertyCreateDto.BedroomCount,
                Floor = propertyCreateDto.Floor,
                IsForSale = propertyCreateDto.IsForSale,
                AdditionalInfo = propertyCreateDto.AdditionalInfo,
                CounterAgentNumber = propertyCreateDto.CounterAgentNumber,
                FloorsInABuilding = propertyCreateDto.FloorsInABuilding,
                ConstructionYear = propertyCreateDto.ConstructionYear,
                Notes = propertyCreateDto.Notes,
                Pets = propertyCreateDto.Pets,
                RentCode = propertyCreateDto.RentCode,
                RentPrice = propertyCreateDto.RentPrice,
                RentPriceBeforeSeason = propertyCreateDto.RentPriceBeforeSeason,
                RentPriceFullSeason = propertyCreateDto.RentPriceFullSeason,
                RentPromoteStatus = propertyCreateDto.RentPromoteStatus,
                RentStatus = Enum.Parse<PropertyStatus>(propertyCreateDto.RentStatus),
                SaleCode = propertyCreateDto.SaleCode,
                RoomCount = propertyCreateDto.RoomCount,
                SalePrice = propertyCreateDto.SalePrice,
                SaleStatus = Enum.Parse<PropertyStatus>(propertyCreateDto.SaleStatus),
                SalePromoteStatus = propertyCreateDto.SalePromoteStatus,
                TurkishKebabs = propertyCreateDto.TurkishKebabs,
                VideoID = propertyCreateDto.VideoID,
                //IsLongTerm = propertyCreateDto.IsLongTerm,
                //Price = propertyCreateDto.Price,
                CoordinateX = propertyCreateDto.CoordinateX,
                CoordinateY = propertyCreateDto.CoordinateY,
                Counterparty_ID = propertyCreateDto.Counterparty_ID,
                CreatedDate = DateTimeOffset.Now
            };
            _dbContext.Properties.Add(newProperty);


            // ADD ALL CITIES
            foreach (var City_ID in propertyCreateDto.City_IDs)
            {
                var newCity_Property = new City_Property()
                {
                    City_ID = City_ID,
                    Property = newProperty
                };
                _dbContext.City_Properties.Add(newCity_Property);
            }
            // ADD ALL BENEFITS
            foreach (var Benefit_ID in propertyCreateDto.Benefit_IDs)
            {
                var newBenefit_Property = new Benefit_Property()
                {
                    Benefit_ID = Benefit_ID,
                    Property = newProperty
                };
                _dbContext.Benefits_Properties.Add(newBenefit_Property);
            }
            // ADD ALL LOCAL_PROPERTIES
            foreach (var local_Property in propertyCreateDto.Local_Properties)
            {
                var newLocal_Property = new Local_Property()
                {
                    Property = newProperty,
                    LocalPropertyDescription = local_Property.LocalPropertyDescription,
                    Type = Enum.Parse<LocalPropertyStatus>(local_Property.LocalPropertyType),
                    LocalPropertyTitle = local_Property.LocalPropertyTitle,
                    Local_ID = local_Property.Local_ID,
                };
                _dbContext.Local_Properties.Add(newLocal_Property);
            }
            // ADD ALL PHOTOS 
            var uploadTasks = new List<Task<BlobResponseDto>>();
            foreach (var photoPositionValue in propertyCreateDto.Photos)
            {
                if (FormFileExtensions.IsImage(photoPositionValue.Photo))
                {
                    var uploadTask = _azureStorageService.UploadAsync(photoPositionValue.Photo);
                    uploadTasks.Add(uploadTask);
                }
            }
            await Task.WhenAll(uploadTasks);
            var i = 0;
            foreach (var uploadTask in uploadTasks)
            {

                var photoUrl = uploadTask.Result.Blob.Uri.ToString();
                var photoName = uploadTask.Result.Blob.Name.ToString();
                var newPhoto = new Photo()
                {
                    PhotoUrl = photoUrl,
                    PhotoName = photoName
                };
                _dbContext.Photos.Add(newPhoto);
                var newPhoto_Properties = new Photo_Property()
                {
                    Photo = newPhoto,
                    Property = newProperty,
                    Position = propertyCreateDto.Photos.ElementAt(i).Position,

                };
                _dbContext.Photos_Properties.Add(newPhoto_Properties);
                i++;
            }




            await _dbContext.SaveChangesAsync();

            var propertyDto = await GetProperty(newProperty.Property_ID);
            return propertyDto;
        }

        public async Task<bool> DeleteProperty(long id)
        {
            var property = await _dbContext.Properties.FindAsync(id);
            if (property == null)
            {
                return false;
            }

            var photo_Properties = await _dbContext.Photos_Properties.Where(s => s.Property_ID == id)
                .Include(s => s.Photo)
                .ToListAsync();
            var deletePhotoTasks = new List<Task>();
            foreach (var photo_Property in photo_Properties)
            {
                var deletePhotoTask = _azureStorageService.DeleteAsync(photo_Property.Photo.PhotoName);
            }
            await Task.WhenAll(deletePhotoTasks);


            _dbContext.Properties.Remove(property);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteProperties(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var property = await _dbContext.Properties.FindAsync(id);
                if (property == null)
                {
                    return false;
                }

                var photo_Properties = await _dbContext.Photos_Properties.Where(s => s.Property_ID == id)
                    .Include(s => s.Photo)
                    .ToListAsync();
                var deletePhotoTasks = new List<Task>();
                foreach (var photo_Property in photo_Properties)
                {
                    var deletePhotoTask = _azureStorageService.DeleteAsync(photo_Property.Photo.PhotoName);
                }
                _dbContext.Properties.Remove(property);
                await Task.WhenAll(deletePhotoTasks);
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<PropertyDto> UpdateProperty(long id, PropertyUpdateDto propertyUpdateDto)
        {
            var property = await _dbContext.Properties.AsNoTracking()
                .Where(s => s.Property_ID == id)
                .SingleOrDefaultAsync();
            if (property == null)
            {
                return null;
            }
            var newProperty = new Property()
            {
                Property_ID = id,
                HouseAreaSquare = propertyUpdateDto.HouseAreaSquare,
                Type = Enum.Parse<PropertyType>(propertyUpdateDto.Type),
                LandAreaSquare = propertyUpdateDto.LandAreaSquare,
                BathroomCount = propertyUpdateDto.BathroomCount,
                BedroomCount = propertyUpdateDto.BedroomCount,
                Floor = propertyUpdateDto.Floor,
                IsForSale = propertyUpdateDto.IsForSale,
                AdditionalInfo = propertyUpdateDto.AdditionalInfo,
                CounterAgentNumber = propertyUpdateDto.CounterAgentNumber,
                FloorsInABuilding = propertyUpdateDto.FloorsInABuilding,
                ConstructionYear = propertyUpdateDto.ConstructionYear,
                Notes = propertyUpdateDto.Notes,
                Pets = propertyUpdateDto.Pets,
                RentCode = propertyUpdateDto.RentCode,
                RentPrice = propertyUpdateDto.RentPrice,
                RentPriceBeforeSeason = propertyUpdateDto.RentPriceBeforeSeason,
                RentPriceFullSeason = propertyUpdateDto.RentPriceFullSeason,
                RentPromoteStatus = propertyUpdateDto.RentPromoteStatus,
                RentStatus = Enum.Parse<PropertyStatus>(propertyUpdateDto.RentStatus),
                SaleCode = propertyUpdateDto.SaleCode,
                RoomCount = propertyUpdateDto.RoomCount,
                SalePrice = propertyUpdateDto.SalePrice,
                SaleStatus = Enum.Parse<PropertyStatus>(propertyUpdateDto.SaleStatus),
                SalePromoteStatus = propertyUpdateDto.SalePromoteStatus,
                TurkishKebabs = propertyUpdateDto.TurkishKebabs,
                VideoID = propertyUpdateDto.VideoID,
                //IsLongTerm = propertyCreateDto.IsLongTerm,
                //Price = propertyCreateDto.Price,
                CoordinateX = propertyUpdateDto.CoordinateX,
                CoordinateY = propertyUpdateDto.CoordinateY,
                Counterparty_ID = propertyUpdateDto.Counterparty_ID,
                CreatedDate = DateTimeOffset.Now
            };
            _dbContext.Properties.Update(newProperty);

            // DELETE ALL CITIES
            var city_Properties = await _dbContext.City_Properties
                .Where(s => s.Property_ID == id)
                .ToListAsync();
            foreach (var city_Property in city_Properties)
            {
                _dbContext.City_Properties.Remove(city_Property);
            }
            // DELETE ALL BENEFITS
            var benefit_Properties = await _dbContext.Benefits_Properties
                .Where(s => s.Property_ID == id)
                .ToListAsync();
            foreach (var benefit_Property in benefit_Properties)
            {
                _dbContext.Benefits_Properties.Remove(benefit_Property);
            }
            // DELETE ALL LOCAL_PROPERTIES
            var local_Properties = await _dbContext.Local_Properties
                .Where(s => s.Property_ID == id)
                .ToListAsync();
            foreach (var local_Property in local_Properties)
            {
                _dbContext.Local_Properties.Remove(local_Property);
            }
            // DELETE ALL PHOTOS
            var deletePhotoTasks = new List<Task>();
            foreach (var photoPositionValue in propertyUpdateDto.Photos)
            {
                var deletePhotoTask = _azureStorageService.DeleteAsync(photoPositionValue.Photo.FileName);
            }
            await Task.WhenAll(deletePhotoTasks);




            // ADD ALL CITIES
            foreach (var City_ID in propertyUpdateDto.City_IDs)
            {
                var newCity_Property = new City_Property()
                {
                    City_ID = City_ID,
                    Property = newProperty
                };
                _dbContext.City_Properties.Add(newCity_Property);
            }
            // ADD ALL BENEFITS
            foreach (var Benefit_ID in propertyUpdateDto.Benefit_IDs)
            {
                var newBenefit_Property = new Benefit_Property()
                {
                    Benefit_ID = Benefit_ID,
                    Property = newProperty
                };
                _dbContext.Benefits_Properties.Add(newBenefit_Property);
            }
            // ADD ALL LOCAL_PROPERTIES
            foreach (var local_Property in propertyUpdateDto.Local_Properties)
            {
                var newLocal_Property = new Local_Property()
                {
                    Property = newProperty,
                    LocalPropertyDescription = local_Property.LocalPropertyDescription,
                    Type = Enum.Parse<LocalPropertyStatus>(local_Property.LocalPropertyType),
                    LocalPropertyTitle = local_Property.LocalPropertyTitle,
                    Local_ID = local_Property.Local_ID,
                };
                _dbContext.Local_Properties.Add(newLocal_Property);
            }
            // ADD ALL PHOTOS 
            var uploadTasks = new List<Task<BlobResponseDto>>();
            foreach (var photoPositionValue in propertyUpdateDto.Photos)
            {
                if (FormFileExtensions.IsImage(photoPositionValue.Photo))
                {
                    var uploadTask = _azureStorageService.UploadAsync(photoPositionValue.Photo);
                    uploadTasks.Add(uploadTask);
                }
            }
            await Task.WhenAll(uploadTasks);

            var i = 0;
            foreach (var uploadTask in uploadTasks)
            {
                var photoUrl = uploadTask.Result.Blob.Uri.ToString();
                var photoName = uploadTask.Result.Blob.Name.ToString();
                var newPhoto = new Photo()
                {
                    PhotoUrl = photoUrl,
                    PhotoName = photoName
                };
                _dbContext.Photos.Add(newPhoto);
                var newPhoto_Properties = new Photo_Property()
                {
                    Photo = newPhoto,
                    Property = newProperty,
                    Position = propertyUpdateDto.Photos.ElementAt(i).Position,
                };
                _dbContext.Photos_Properties.Add(newPhoto_Properties);
                i++;
            }







            await _dbContext.SaveChangesAsync();

            var propertyDto = await GetProperty(newProperty.Property_ID);
            return propertyDto;
        }

        public async Task<(IEnumerable<PropertyDto> propertyDtos, PaginationFilter filter, int totalRecords)> SearchProperties(PropertySearchPaginatedQuery query)
        {
            var paginationFilter = QueryParser.ParseQueryForPageFilters(query, 20);



            var mainPredicate = PredicateBuilder.True<Property>();



            if (query.SalePromoteStatus is not null)
            {
                var predicateSalePromoteStatus = PredicateBuilder.False<Property>();

                predicateSalePromoteStatus = predicateSalePromoteStatus.OR(s => s.SalePromoteStatus == query.SalePromoteStatus);

                mainPredicate = mainPredicate.AND(predicateSalePromoteStatus);
            }
            if (query.RentPromoteStatus is not null)
            {
                var predicateRentPromoteStatus = PredicateBuilder.False<Property>();

                predicateRentPromoteStatus = predicateRentPromoteStatus.OR(s => s.RentPromoteStatus == query.SalePromoteStatus);

                mainPredicate = mainPredicate.AND(predicateRentPromoteStatus);
            }
            if (query.IsForSale is not null && query.IsForSale == true)
            {
                var predicateIsForSale = PredicateBuilder.False<Property>();

                predicateIsForSale = predicateIsForSale.OR(s => s.IsForSale == true);

                mainPredicate = mainPredicate.AND(predicateIsForSale);
            }
            if (query.RentalPeriod is not null)
            {
                var predicateRentalPeriod = PredicateBuilder.False<Property>();


                if (query.RentalPeriod == "any")
                {
                    predicateRentalPeriod = predicateRentalPeriod.OR(s => s.RentPrice != null);
                    predicateRentalPeriod = predicateRentalPeriod.OR(s => s.RentPriceFullSeason != null);
                }
                else if (query.RentalPeriod == "long-term")
                {
                    predicateRentalPeriod = predicateRentalPeriod.OR(s => s.RentPrice != null);
                }
                else if (query.RentalPeriod == "whole-season")
                {
                    predicateRentalPeriod = predicateRentalPeriod.OR(s => s.RentPriceFullSeason != null);
                }
                mainPredicate = mainPredicate.AND(predicateRentalPeriod);
            }
            if (query.BedroomCounts is not null)
            {
                var predicateBedroomCount = PredicateBuilder.False<Property>();

                foreach (var bedroomCount in query.BedroomCounts)
                {
                    predicateBedroomCount = predicateBedroomCount.OR(s => s.BedroomCount == bedroomCount);
                }
                mainPredicate = mainPredicate.AND(predicateBedroomCount);
            }
            if (query.City_IDs is not null)
            {
                var predicateCity = PredicateBuilder.False<Property>();
                foreach (var city_ID in query.City_IDs)
                {
                    predicateCity = predicateCity.OR(s => s.City_Properties.Any(w => w.City.City_ID == city_ID));
                }
                mainPredicate = mainPredicate.AND(predicateCity);
            }
            if (query.Benefit_IDs is not null)
            {
                var predicateCity = PredicateBuilder.False<Property>();
                foreach (var benefit_ID in query.Benefit_IDs)
                {
                    predicateCity = predicateCity.OR(s => s.Benefit_Properties.Any(w => w.Benefit.Benefit_ID == benefit_ID));
                }
                mainPredicate = mainPredicate.AND(predicateCity);
            }
            if (query.PriceFrom is not null)
            {
                var predicatePriceFrom = PredicateBuilder.False<Property>();

                predicatePriceFrom = predicatePriceFrom.OR(s => s.RentPrice >= query.PriceFrom);
                predicatePriceFrom = predicatePriceFrom.OR(s => s.RentPriceBeforeSeason >= query.PriceFrom);
                predicatePriceFrom = predicatePriceFrom.OR(s => s.RentPriceFullSeason >= query.PriceFrom);
                predicatePriceFrom = predicatePriceFrom.OR(s => s.SalePrice >= query.PriceFrom);

                mainPredicate = mainPredicate.AND(predicatePriceFrom);
            }
            if (query.PriceTo is not null)
            {
                var predicatePriceTo = PredicateBuilder.False<Property>();

                predicatePriceTo = predicatePriceTo.OR(s => s.RentPrice <= query.PriceTo);
                predicatePriceTo = predicatePriceTo.OR(s => s.RentPriceBeforeSeason <= query.PriceTo);
                predicatePriceTo = predicatePriceTo.OR(s => s.RentPriceFullSeason <= query.PriceTo);
                predicatePriceTo = predicatePriceTo.OR(s => s.SalePrice <= query.PriceTo);

                mainPredicate = mainPredicate.AND(predicatePriceTo);
            }

            if (query.LandFootageFrom is not null)
            {
                var predicateLandFootageFrom = PredicateBuilder.False<Property>();

                predicateLandFootageFrom = predicateLandFootageFrom.OR(s => s.LandAreaSquare > query.LandFootageFrom);

                mainPredicate = mainPredicate.AND(predicateLandFootageFrom);
            }
            if (query.LandFootageTo is not null)
            {
                var predicateLandFootageTo = PredicateBuilder.False<Property>();

                predicateLandFootageTo = predicateLandFootageTo.OR(s => s.LandAreaSquare <= query.LandFootageTo);

                mainPredicate = mainPredicate.AND(predicateLandFootageTo);
            }
            if (query.Type is not null)
            {
                var predicateType = PredicateBuilder.False<Property>();

                switch (query.Type)
                {
                    case "land":
                        {
                            predicateType = predicateType.OR(s => s.Type == PropertyType.Land);
                            break;
                        }
                    case "commercial":
                        {
                            predicateType = predicateType.OR(s => s.Type == PropertyType.Commercial);
                            break;
                        }
                    case "house":
                        {
                            predicateType = predicateType.OR(s => s.Type == PropertyType.House);
                            break;
                        }
                }



                mainPredicate = mainPredicate.AND(predicateType);
            }


            var totalRecords = await _dbContext.Properties.Where(mainPredicate).CountAsync();
            var properties = await GetPropertiesWithAllInformation(_dbContext.Properties.Where(mainPredicate));



            var propertiesDtos = new List<PropertyDto>();
            foreach (var property in properties)
            {
                var propertyDto = _mapper.Map<PropertyDto>(property);
                propertiesDtos.Add(propertyDto);
            }



            return (propertiesDtos, paginationFilter, totalRecords);
        }

    }
}
