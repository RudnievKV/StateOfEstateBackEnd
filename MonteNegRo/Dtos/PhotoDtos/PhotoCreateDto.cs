using MonteNegRo.Models;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.PhotoDtos
{
    public record PhotoCreateDto
    {
        public string PhotoUrl { get; init; }
    }
}
