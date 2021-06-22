using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Components.ComponentModels
{
    public class AlertMessageComponentBase : ComponentBase
    {
        protected bool AlertIsVizibile { get; set; } = false;

        public string ConfirmationTitle { get; set; }

        public string ConfirmationMessage { get; set; }

        [Parameter]
        public EventCallback<AlertMessageArgs> OnCloseCallback { get; set; }

        public void ShowAlert(string title, string message)
        {
            ConfirmationTitle = title;
            ConfirmationMessage = message; 
            AlertIsVizibile = true;
            this.StateHasChanged();
        }

        public async Task CloseAlert(bool result)
        {
            AlertMessageArgs args = new AlertMessageArgs(result);

            await OnCloseCallback.InvokeAsync(args);
            ConfirmationTitle = "";
            ConfirmationMessage = "";
            AlertIsVizibile = false;
            this.StateHasChanged();
        }
    }

    public class AlertMessageArgs
    {
        public AlertMessageArgs(bool? result)
        {
            Result = result;
        }

        public bool? Result { get; set; }
    }
}
