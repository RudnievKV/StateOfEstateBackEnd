using System.Collections.Generic;

namespace MonteNegRo.Dtos.TranslationDtos
{
    public class TranslationInputDto
    {
        public LanguageCodeAndValue SourceLanguageCodeAndValue { get; init; }
        public IEnumerable<string> TargetLanguageCodes { get; init; }
    }
}
