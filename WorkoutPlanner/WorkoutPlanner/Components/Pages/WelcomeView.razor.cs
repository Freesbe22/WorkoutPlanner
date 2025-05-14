using Firebase.Auth;
using Microsoft.AspNetCore.Components;

using WorkoutPlanner.Components.Pages.Execution;
using WorkoutPlanner.DataObject;
using WorkoutPlanner.Tools.Services;

namespace WorkoutPlanner.Components.Pages
{
    partial class WelcomeView : ComponentBase
    {
        #region Variables
        [Inject]
        public FirestoreService FirestoreService { get; set; }
        [Inject]
        public FirebaseAuthClient AuthClient { get; set; }

        private bool Initialised { get; set; } = false;

        #endregion

        #region Loading
        protected override async Task OnInitializedAsync()
        {
            Initialised = true;
            await InvokeAsync(() => { StateHasChanged(); });

            await base.OnInitializedAsync();
        }

        #endregion

    }
}
