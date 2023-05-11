using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Cloud.Translation.V2;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MonteNegRo.Dtos.TranslationDtos;
using MonteNegRo.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonteNegRo.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly TranslationClientImpl _translationClient;
        public TranslationService(IConfiguration configuration)
        {
            var translationService = new TranslateService(
                new BaseClientService.Initializer { ApiKey = configuration["ConnectionStrings:GoogleTranslate"] });
            this._translationClient = new TranslationClientImpl(translationService, TranslationModel.ServiceDefault);
        }

        public async Task<TranslationDto> GetTranslations(TranslationInputDto translationInputDto)
        {
            var translationTasks = new List<Task<TranslationResult>>();
            var languageCodeAndValueTaskDictionary = new Dictionary<string, Task<IList<TranslationResult>>>();
            foreach (var targetLanguageCode in translationInputDto.TargetLanguageCodes)
            {
                var translationTask = _translationClient.TranslateTextAsync(
                    translationInputDto.SourceLanguageCodeAndValue.Values,
                    targetLanguageCode,
                    translationInputDto.SourceLanguageCodeAndValue.LanguageCode);
                //translationTasks.Add(translationTask);
                languageCodeAndValueTaskDictionary[targetLanguageCode] = translationTask;
            }
            var languageCodeAndValueDictionary = await ToResults(languageCodeAndValueTaskDictionary);

            var source = new LanguageCodeAndValue()
            {
                LanguageCode = translationInputDto.SourceLanguageCodeAndValue.LanguageCode,
                Values = translationInputDto.SourceLanguageCodeAndValue.Values
            };

            var targetLanguageCodeAndValues = new List<LanguageCodeAndValue>();
            foreach (var keyValuePair in languageCodeAndValueDictionary)
            {
                var values = new List<string>();
                foreach (var translationResult in keyValuePair.Value)
                {
                    values.Add(translationResult.TranslatedText);
                }
                var targetLanguageCodeAndValue = new LanguageCodeAndValue()
                {
                    LanguageCode = keyValuePair.Key,
                    Values = values
                };
                targetLanguageCodeAndValues.Add(targetLanguageCodeAndValue);
            }
            var result = new TranslationDto()
            {
                SourceLanguageCodeAndValue = translationInputDto.SourceLanguageCodeAndValue,
                TargetLanguageCodeAndValues = targetLanguageCodeAndValues
            };


            return result;
        }
        public async Task<Dictionary<TKey, TResult>> ToResults<TKey, TResult>(IEnumerable<KeyValuePair<TKey, Task<TResult>>> input)
        {
            var pairs = await Task.WhenAll
            (
                input.Select
                (
                    async pair => new { Key = pair.Key, Value = await pair.Value }
                )
            );
            return pairs.ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
