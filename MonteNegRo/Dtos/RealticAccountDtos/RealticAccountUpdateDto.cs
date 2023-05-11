namespace MonteNegRo.Dtos.RealticAccountDtos
{
    public record RealticAccountUpdateDto
    {
        public string? Name { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
