using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonteNegRo.Common;
using MonteNegRo.DBContext;
using MonteNegRo.Dtos.NotificationDtos;
using MonteNegRo.Dtos.PartnerDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Filters;
using MonteNegRo.Models;
using MonteNegRo.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonteNegRo.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly MyDBContext _dbContext;
        private readonly IMapper _mapper;
        public PartnerService(MyDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PartnerDto>> GetAllPartners()
        {
            var partners = await _dbContext.Partners.ToListAsync();
            var partnerDtos = new List<PartnerDto>();
            foreach (var partner in partners)
            {
                var partnerDto = _mapper.Map<PartnerDto>(partner);
                partnerDtos.Add(partnerDto);
            }
            return partnerDtos;
        }

        public async Task<(IEnumerable<PartnerDto> partnerDtos,
            PaginationFilter filter,
            int totalRecords)>
            GetAllPartnersPaged(PartnerPaginatedQuery query)
        {
            var paginationFilter = QueryParser.ParseQueryForPageFilters(query, 40);




            var partnersIQueryable = _dbContext.Partners
                .OrderBy(s => s.Partner_ID)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);
            var totalRecords = await partnersIQueryable.CountAsync();

            var partners = await partnersIQueryable
                .ToListAsync();

            var partnerDtos = new List<PartnerDto>();
            foreach (var partner in partners)
            {
                var partnerDto = _mapper.Map<PartnerDto>(partner);
                partnerDtos.Add(partnerDto);
            }
            return (partnerDtos, paginationFilter, totalRecords);
        }

        public async Task<PartnerDto> GetPartner(long id)
        {
            var partner = await _dbContext.Partners.Where(s => s.Partner_ID == id).SingleOrDefaultAsync();
            if (partner == null)
            {
                return null;
            }
            var partnerDto = _mapper.Map<PartnerDto>(partner);
            return partnerDto;
        }

        public async Task<PartnerDto> CreatePartner(PartnerCreateDto partnerCreateDto)
        {
            var newPartner = new Partner()
            {
                IsActiveRent = partnerCreateDto.IsActiveRent,
                IsActiveSale = partnerCreateDto.IsActiveSale,
                Email = partnerCreateDto.Email,
                IsSubscribedRent = partnerCreateDto.IsSubscribedRent,
                IsSubscribedSale = partnerCreateDto.IsSubscribedSale,
                Notes = partnerCreateDto.Notes,
                Website = partnerCreateDto.Website,
            };
            _dbContext.Partners.Add(newPartner);

            foreach (var partnerPhone in partnerCreateDto.PartnerPhones)
            {
                var newPartnerPhone = new PartnerPhone()
                {
                    Note = partnerPhone.Note,
                    PhoneNumber = partnerPhone.PhoneNumber,
                    Partner = newPartner
                };
                _dbContext.PartnerPhones.Add(newPartnerPhone);
            }

            foreach (var cityID in partnerCreateDto.CityIDs)
            {
                var newPartner_City = new Partner_City()
                {
                    City_ID = cityID,
                    Partner = newPartner,

                };
                _dbContext.Partner_Cities.Add(newPartner_City);
            }
            await _dbContext.SaveChangesAsync();
            var partnerDto = await GetPartner(newPartner.Partner_ID);
            return partnerDto;
        }

        public async Task<bool> DeletePartner(long id)
        {
            var partner = await _dbContext.Partners.Where(s => s.Partner_ID == id).SingleOrDefaultAsync();
            if (partner == null)
            {
                return false;
            }
            _dbContext.Partners.Remove(partner);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<PartnerDto> UpdatePartner(long id, PartnerUpdateDto partnerUpdateDto)
        {
            var partner = await _dbContext.Partners.AsNoTracking().Where(s => s.Partner_ID == id)
                .Include(s => s.PartnerPhones)
                .Include(s => s.Partner_Cities)
                .SingleOrDefaultAsync();
            if (partner == null)
            {
                return null;
            }
            foreach (var partnerPhone in partner.PartnerPhones)
            {
                _dbContext.PartnerPhones.Remove(partnerPhone);
            }
            foreach (var partner_City in partner.Partner_Cities)
            {
                _dbContext.Partner_Cities.Remove(partner_City);
            }

            var newPartner = new Partner()
            {
                IsActiveRent = partner.IsActiveRent,
                IsActiveSale = partner.IsActiveSale,
                Email = partner.Email,
                IsSubscribedRent = partner.IsSubscribedRent,
                IsSubscribedSale = partner.IsSubscribedSale,
                Notes = partner.Notes,
                Partner_ID = partner.Partner_ID,
                Website = partner.Website,

            };
            _dbContext.Partners.Update(newPartner);

            foreach (var partnerPhone in partnerUpdateDto.PartnerPhones)
            {
                var newPartnerPhone = new PartnerPhone()
                {
                    Note = partnerPhone.Note,
                    PhoneNumber = partnerPhone.PhoneNumber,
                    Partner = newPartner
                };
                _dbContext.PartnerPhones.Add(newPartnerPhone);
            }

            foreach (var cityID in partnerUpdateDto.CityIDs)
            {
                var newPartner_City = new Partner_City()
                {
                    Partner = newPartner,
                    City_ID = cityID
                };
                _dbContext.Partner_Cities.Add(newPartner_City);
            }

            var partnerDto = await GetPartner(id);
            return partnerDto;
        }

        public async Task<bool> DeletePartners(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var partner = await _dbContext.Partners.FindAsync(id);
                if (partner == null)
                {
                    return false;
                }
                _dbContext.Partners.Remove(partner);
            }
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
