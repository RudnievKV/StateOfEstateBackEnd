using MonteNegRo.Models;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.RealticAccountDtos
{
    public record RealticAccountCreateDto
    {
        public string? Name { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
