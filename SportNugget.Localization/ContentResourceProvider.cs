using Microsoft.Extensions.Localization;
using SportNugget.Localization.Interfaces;
using SportNugget.Localization.Resources;
using SportNugget.Logging.Interfaces;
using System.Globalization;

namespace SportNugget.Localization
{
    public class ContentResourceProvider : IContentResourceProvider
    {
        private readonly IStringLocalizer<SportNugget.Localization.Resources.ContentResources> _localizer;
        private readonly ILogger _logger;

        public ContentResourceProvider(IStringLocalizer<ContentResources> localizer, ILogger logger)
        {
            _localizer = localizer;
            _logger = logger;
        }

        public string GetResource(string key)
        {
            try
            {
                return _localizer[key];
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting resource value.");
                return string.Empty;
            }
        }
    }
}
