namespace MonteNegRo.Dtos.CounterpartyDtos
{
    public record CounterpartyCreateDto
    {
        public string Type { get; init; }
        public string? FullName { get; init; }
        public string? PhoneNumber { get; init; }
        public string? PhoneNumber2 { get; init; }
        public string? PhoneNumber3 { get; init; }
        public string? Viber { get; init; }
        public string? WhatsUp { get; init; }
        public string? Telegram { get; init; }
        public string? Email { get; init; }
        public string? Website { get; init; }
        public string? Description { get; init; }
        public bool IsActive { get; init; }
    }
}
