using Microsoft.AspNetCore.Components;

namespace SportNugget.Pages.Shared
{
    public class PageBase : ComponentBase
    {
        [CascadingParameter]
        public Error? Error { get; set; }
    }
}
