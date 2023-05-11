using MonteNegRo.Dtos.UserDtos;
using MonteNegRo.Dtos.UserTypeDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonteNegRo.Services.Interfaces
{
    public interface IUserTypeService
    {
        public Task<UserTypeDto> GetUserType(long id);
        public Task<IEnumerable<UserTypeDto>> GetAllUserTypes();
        public Task<UserTypeDto> CreateUserType(UserTypeCreateDto userTypeCreateDto);
        public Task<UserTypeDto> UpdateUserType(long id, UserTypeUpdateDto userTypeUpdateDto);
        public Task<bool> DeleteUserType(long id);
    }
}
