using MonteNegRo.Dtos.Photo_PropertyDtos;
using MonteNegRo.Models;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.PhotoDtos
{
    public record PhotoDto
    {
        public long Photo_ID { get; init; }
        public string PhotoUrl { get; init; }
        public int Position { get; init; }
        public string PhotoName { get; init; }
        //public virtual IEnumerable<Photo_PropertyDto> Photo_Properties { get; init; }
    }
}
