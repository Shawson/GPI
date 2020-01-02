namespace GPI.Services.ContentHosts.Oculus.DataExtraction
{
    public interface IOculusWebsiteScraper
    {
        OculusWebsiteJson ScrapeDataForApplicationId(IWebView view, string appId);
    }
}