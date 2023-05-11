using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MonteNegRo.Common;
using MonteNegRo.DBContext;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Dtos.UserDtos;
using MonteNegRo.Filters;
using MonteNegRo.Models;
using MonteNegRo.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonteNegRo.Services
{
    public class UserService : IUserService
    {
        private readonly MyDBContext _dbContext;
        private readonly IMapper _mapper;
        public UserService(MyDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<(IEnumerable<UserDto> userDtos,
            PaginationFilter filter,
            int totalRecords)>
            GetAllUserPaged(UserPaginatedQuery query)
        {
            var paginationFilter = QueryParser.ParseQueryForPageFilters(query, 20);

            var mainPredicate = PredicateBuilder.True<User>();
            var usersIQueryable = _dbContext.Users.AsQueryable();
            if (query.Search != null & query.Search != "")
            {

                mainPredicate = mainPredicate.AND(s => s.Username.Contains(query.Search));

            }


            usersIQueryable = _dbContext.Users
                .Where(mainPredicate)
                .OrderBy(s => s.User_ID)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);
            var totalRecords = await usersIQueryable.CountAsync();
            var users = await usersIQueryable
                .Include(s => s.UserType)
                .ToListAsync();

            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserDto>(user);
                userDtos.Add(userDto);
            }
            return (userDtos, paginationFilter, totalRecords);
        }

        public async Task<UserDto> GetUser(long id)
        {
            var user = await _dbContext.Users
                .Include(s => s.UserType)
                .Where(s => s.User_ID == id)
                .SingleOrDefaultAsync();
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async Task<UserDto> CreateUser(UserCreateDto userCreateDto)
        {
            var numberOfDuplicates = await _dbContext.Users
                .Where(s => s.Username == userCreateDto.Username)
                .CountAsync();
            if (numberOfDuplicates > 0)
            {
                return null;
            }

            PasswordHasher passwordHasher = new PasswordHasher();
            var passwordHash = passwordHasher.HashPassword(userCreateDto.Password);

            var newUser = new User()
            {
                Email = userCreateDto.Email,
                Username = userCreateDto.Username,
                PasswordHash = passwordHash,
                UserType_ID = userCreateDto.UserType_ID,
            };
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            var userDto = await GetUser(newUser.User_ID);
            return userDto;
        }

        public async Task<bool> DeleteUser(long id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<UserDto> UpdateUser(long id, UserUpdateDto userUpdateDto)
        {
            var numberOfDuplicates = await _dbContext.Users
                .AsNoTracking()
                .Where(s => s.Username == userUpdateDto.Username && s.User_ID != id)
                .CountAsync();
            if (numberOfDuplicates > 0)
            {
                return null;
            }

            var user = await _dbContext.Users.AsNoTracking()
                .Where(s => s.User_ID == id)
                .SingleOrDefaultAsync();
            string passwordHash;
            if (userUpdateDto.Password == null || userUpdateDto.Password == "")
            {
                passwordHash = user.PasswordHash;
            }
            else
            {
                PasswordHasher passwordHasher = new PasswordHasher();

                passwordHash = passwordHasher.HashPassword(userUpdateDto.Password);
            }



            var newUser = new User()
            {
                User_ID = id,
                Email = userUpdateDto.Email,
                PasswordHash = passwordHash,
                Username = userUpdateDto.Username,
                UserType_ID = userUpdateDto.UserType_ID,
            };
            _dbContext.ChangeTracker.Clear();
            _dbContext.Users.Update(newUser);
            await _dbContext.SaveChangesAsync();


            var userDto = await GetUser(id);
            return userDto;
        }

        public async Task<bool> DeleteUsers(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var user = await _dbContext.Users.FindAsync(id);
                if (user == null)
                {
                    return false;
                }
                _dbContext.Users.Remove(user);
            }
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
