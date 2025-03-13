using Firebase.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using WorkoutPlanner.Tools.Auth;

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
        [Inject]
        private IStringLocalizer<AppResources> Localizer { get; set; }
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
                NavManager.NavigateTo("/workout", replace: true);
            }
        }

        #region Model
        public class SignInViewModel
        {
            [Required(ErrorMessageResourceName = nameof(AppResources.fui_required_field), ErrorMessageResourceType = typeof(AppResources))]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessageResourceName = nameof(AppResources.fui_required_field), ErrorMessageResourceType = typeof(AppResources))]
            [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
        #endregion
    }
}
