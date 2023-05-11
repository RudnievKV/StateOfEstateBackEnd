using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Dtos.UserDtos;
using MonteNegRo.Filters;
using MonteNegRo.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonteNegRo.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetUser(long id);
        public Task<(IEnumerable<UserDto> userDtos, PaginationFilter filter, int totalRecords)> GetAllUserPaged(UserPaginatedQuery query);
        public Task<UserDto> CreateUser(UserCreateDto userCreateDto);
        public Task<UserDto> UpdateUser(long id, UserUpdateDto userUpdateDto);
        public Task<bool> DeleteUser(long id);
        public Task<bool> DeleteUsers(IEnumerable<long> ids);
    }
}
