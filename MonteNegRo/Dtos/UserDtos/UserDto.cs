using MonteNegRo.Dtos.UserTypeDtos;

namespace MonteNegRo.Dtos.UserDtos
{
    public record UserDto
    {
        public long User_ID { get; init; }
        public string Email { get; init; }
        public string Username { get; init; }
        public string PasswordHash { get; init; }
        public virtual UserTypeDto UserType { get; init; }
    }
}
