using SportNugget.Localization.Resources;

namespace SportNugget.Localization.Interfaces
{
    public interface IContentResourceProvider
    {
        string GetResource(string key);
    }
}
