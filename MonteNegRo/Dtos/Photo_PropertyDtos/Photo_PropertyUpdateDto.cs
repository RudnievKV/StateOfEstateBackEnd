namespace MonteNegRo.Dtos.Photo_PropertyDtos
{
    public record Photo_PropertyUpdateDto
    {
        public long Property_ID { get; init; }
        public long Photo_ID { get; init; }
        public int PositionOrder { get; init; }
    }
}
