using MonteNegRo.Dtos.PartnerDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.PartnerPhoneDtos
{
    public record PartnerPhoneDto
    {
        public long PartnerPhone_ID { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Note { get; init; }
        public long Partner_ID { get; init; }
        //public PartnerDto Partner { get; init; }
    }
}
