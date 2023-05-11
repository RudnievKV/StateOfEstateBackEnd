namespace MonteNegRo.Dtos.UserDtos
{
    public record UserUpdateDto
    {
        public string Email { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public long UserType_ID { get; init; }
    }
}
