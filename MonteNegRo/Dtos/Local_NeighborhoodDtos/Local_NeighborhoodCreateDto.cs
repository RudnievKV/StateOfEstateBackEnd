using MonteNegRo.Dtos.LocalDtos;

namespace MonteNegRo.Dtos.Local_NeighborhoodDtos
{
    public record Local_NeighborhoodCreateDto
    {
        public string LocalNeighborhoodName { get; init; }
        public long Neighborhood_ID { get; init; }
        public long Local_ID { get; init; }
    }
}
