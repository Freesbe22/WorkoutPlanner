using Firebase.Auth;
using Microsoft.AspNetCore.Components;

namespace WorkoutPlanner.Components.Pages
{
    partial class LoadingView : ComponentBase
    {
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public FirebaseAuthClient AuthClient { get; set; }

        protected override void OnInitialized()
        {
#if DEBUG
            //AuthClient.SignOut();
            //InvokeAsync(() => { StateHasChanged(); });
#endif
            //NavManager.NavigateTo("/workout", replace: true);
        }
    }
}
