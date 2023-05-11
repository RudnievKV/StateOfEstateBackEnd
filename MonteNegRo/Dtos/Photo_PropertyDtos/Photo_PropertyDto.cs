using MonteNegRo.Dtos.PhotoDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Photo_PropertyDtos
{
    public record Photo_PropertyDto
    {
        public long Photo_Property_ID { get; init; }
        public long Property_ID { get; init; }
        public long Photo_ID { get; init; }
        public int PositionOrder { get; init; }
        public virtual PhotoDto Photo { get; init; }
        //public virtual PropertyDto Property { get; init; }
    }
}
