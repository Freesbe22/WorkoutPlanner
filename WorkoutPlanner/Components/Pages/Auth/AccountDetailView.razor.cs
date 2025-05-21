using BootstrapBlazor.Components;

using Firebase.Auth;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

using System.ComponentModel.DataAnnotations;

using WorkoutPlanner.DataObject;
using WorkoutPlanner.Tools.Auth;
using WorkoutPlanner.Tools.Services;
using WorkoutPlanner.WinUI;

namespace WorkoutPlanner.Components.Pages.Auth
{
    partial class AccountDetailView : ComponentBase
    {
        #region Variables

        [Inject]
        public FirebaseAuthClient AuthClient { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public FirestoreService FirestoreService { get; set; }
        private DataObject.User User { get; set; } = new DataObject.User();

        private EditContext _editContext;

        #endregion

        #region Loading

        protected override async Task OnInitializedAsync ()
        {
            _editContext = new EditContext(User);
            await InvokeAsync(() => { StateHasChanged(); });

            await base.OnInitializedAsync();
        }

        #endregion

        private async Task Register ()
        {
            User.AuthId = AuthClient.User.Uid;
            await FirestoreService.db.Collection(typeof(DataObject.User).Name).AddAsync(User);
            NavManager.NavigateTo("/welcome", replace: true);
        }

    }
}
