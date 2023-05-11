using MonteNegRo.Dtos.LocalDtos;

namespace MonteNegRo.Dtos.Local_NeighborhoodDtos
{
    public record Local_NeighborhoodDto
    {
        public long Local_Neighborhood_ID { get; init; }
        public virtual LocalDto Local { get; init; }
        public string LocalNeighborhoodName { get; init; }
    }
}
