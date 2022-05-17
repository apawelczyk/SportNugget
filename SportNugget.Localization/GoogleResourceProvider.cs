using Google.Cloud.Translate.V3;
using Microsoft.Extensions.Localization;
using SportNugget.Localization.Interfaces;
using SportNugget.Localization.Resources;
using SportNugget.Logging.Interfaces;

namespace SportNugget.Localization
{
    public class GoogleResourceProvider : IContentResourceProvider
    {
        private readonly IStringLocalizer<SportNugget.Localization.Resources.ContentResources> _localizer;
        private readonly ILogger _logger;

        public string GetResource(string key, string culture = "en-US")
        {
            try
            {
                var value = _localizer[key];
                if (!string.IsNullOrEmpty(value))
                {
                    TranslationServiceClient client = TranslationServiceClient.Create();
                    TranslateTextRequest request = new TranslateTextRequest
                    {
                        Contents = { value },
                        TargetLanguageCode = culture
                    };
                    TranslateTextResponse response = client.TranslateText(request);
                    Translation translation = response.Translations[0];
                    return translation.TranslatedText;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting resource value from Google.");
            }
            return string.Empty;
        }
    }
}
