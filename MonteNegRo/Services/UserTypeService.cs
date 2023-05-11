using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonteNegRo.DBContext;
using MonteNegRo.Dtos.UserTypeDtos;
using MonteNegRo.Models;
using MonteNegRo.Services.Interfaces;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace MonteNegRo.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly MyDBContext _dbContext;
        private readonly IMapper _mapper;
        public UserTypeService(MyDBContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserTypeDto>> GetAllUserTypes()
        {
            var userTypes = await _dbContext.UserTypes.ToListAsync();
            var userTypeDtos = new List<UserTypeDto>();
            foreach (var userType in userTypes)
            {
                var userTypeDto = _mapper.Map<UserTypeDto>(userType);
                userTypeDtos.Add(userTypeDto);
            }
            return userTypeDtos;
        }

        public async Task<UserTypeDto> GetUserType(long id)
        {
            var userType = await _dbContext.UserTypes.FindAsync(id);
            var userTypeDto = _mapper.Map<UserTypeDto>(userType);
            return userTypeDto;
        }

        public async Task<UserTypeDto> CreateUserType(UserTypeCreateDto userTypeCreateDto)
        {
            var newUserType = new UserType()
            {
                UserTypeName = userTypeCreateDto.UserTypeName,
            };
            _dbContext.UserTypes.Add(newUserType);
            await _dbContext.SaveChangesAsync();

            var userTypeDto = _mapper.Map<UserTypeDto>(newUserType);
            return userTypeDto;
        }

        public async Task<bool> DeleteUserType(long id)
        {
            var userType = await _dbContext.UserTypes.FindAsync(id);
            if (userType == null)
            {
                return false;
            }

            _dbContext.UserTypes.Remove(userType);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserTypeDto> UpdateUserType(long id, UserTypeUpdateDto userTypeUpdateDto)
        {
            var userType = await _dbContext.UserTypes.FindAsync(id);
            if (userType == null)
            {
                return null;
            }

            var newUserType = new UserType()
            {
                UserType_ID = id,
                UserTypeName = userTypeUpdateDto.UserTypeName,
            };
            _dbContext.UserTypes.Update(newUserType);
            await _dbContext.SaveChangesAsync();

            var userTypeDto = _mapper.Map<UserTypeDto>(userType);
            return userTypeDto;
        }
    }
}
