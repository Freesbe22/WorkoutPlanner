using Firebase.Auth;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using WorkoutPlanner.Tools;

namespace WorkoutPlanner.Components.Pages.Auth
{
    partial class SignInView : ComponentBase
    {
        #region Variables
        [Inject]
        public FirebaseAuthClient AuthClient { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public StateProvider AuthStateProvider { get; set; }
        private SignInViewModel SignInModel { get; set; } = new SignInViewModel();
        private bool Initialised { get; set; } = false;
        #endregion

        #region Loading
        protected override async Task OnInitializedAsync()
        {
            CurrentUserCheck();
            Initialised = true;
            await InvokeAsync(() => { StateHasChanged(); });

            await base.OnInitializedAsync();
        }
        #endregion
        private async Task SignIn()
        {
            await AuthClient.SignInWithEmailAndPasswordAsync(SignInModel.Email, SignInModel.Password);
            CurrentUserCheck();
        }

        private void CurrentUserCheck()
        {
#if DEBUG
                //AuthClient.SignOut();
#endif
            if (AuthClient.User != null)
            {
                AuthStateProvider.ManageUser();
                NavManager.NavigateTo("/workout");
            }
        }

        #region Model
        public class SignInViewModel
        {
            [Required, EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
        #endregion
    }
}
