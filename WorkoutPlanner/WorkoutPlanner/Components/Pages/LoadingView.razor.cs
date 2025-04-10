using Firebase.Auth;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace WorkoutPlanner.Components.Pages
{
    partial class LoadingView : ComponentBase
    {
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public FirebaseAuthClient AuthClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
#if DEBUG
            //AuthClient.SignOut();
            //InvokeAsync(() => { StateHasChanged(); });
#endif
            await Task.Delay(3000);
            NavManager.NavigateTo("/workout", replace: true);
        }
    }
}
