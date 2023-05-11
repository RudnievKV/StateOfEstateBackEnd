using MonteNegRo.Dtos.TranslationDtos;
using System.Threading.Tasks;

namespace MonteNegRo.Services.Interfaces
{
    public interface ITranslationService
    {
        public Task<TranslationDto> GetTranslations(TranslationInputDto translationInputDto);
    }
}
