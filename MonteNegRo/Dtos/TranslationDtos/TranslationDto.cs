using System.Collections.Generic;

namespace MonteNegRo.Dtos.TranslationDtos
{
    public record TranslationDto
    {
        public LanguageCodeAndValue SourceLanguageCodeAndValue { get; init; }
        public IEnumerable<LanguageCodeAndValue> TargetLanguageCodeAndValues { get; init; }

    }
    public record LanguageCodeAndValue
    {
        public string LanguageCode { get; init; }
        public IEnumerable<string> Values { get; init; }
    }
}
