namespace MonteNegRo.Dtos.Benefit_PropertyDtos
{
    public record Benefit_PropertyUpdateDto
    {
        public long Benefit_ID { get; init; }
        public long Property_ID { get; init; }
    }
}
