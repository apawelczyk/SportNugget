using Microsoft.AspNetCore.Components;
using Radzen;
using SportNugget.Logging.Interfaces;

namespace SportNugget.Pages.Shared
{
    public partial class Error
    {
        [Inject]
        public ILogger Logger { get; set; }
        [Inject]
        public NotificationService NotificationService { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        public void ProcessError(Exception ex, string message = null)
        {
            try
            {
                var notification = new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = "Error on page",
                    Duration = 4000
                };
                NotificationService.Notify(notification);
                Logger.LogError(ex, $"Error on page. Message: {message}");
            }
            catch(Exception e)
            {

            }
        }
    }
}
