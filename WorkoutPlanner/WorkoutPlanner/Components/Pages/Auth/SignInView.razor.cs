using BootstrapBlazor.Components;
using Firebase.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using WorkoutPlanner.Tools.Auth;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private MessageService MessageService { get; set; }
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
            try
            {
                await AuthClient.SignInWithEmailAndPasswordAsync(SignInModel.Email, SignInModel.Password);
                CurrentUserCheck();
            }
            catch (FirebaseAuthHttpException ex)
            {
                await MessageService.Show(new MessageOption()
                {
                    Content = FirebaseErrorLookup.LookupError(ex),
                    Icon = "fa-solid fa-circle-xmark",
                    ShowDismiss = true,
                    Color = BootstrapBlazor.Components.Color.Danger,
                    OnDismiss = () =>
                    {
                        return Task.CompletedTask;
                    }
                });
                //Error = FirebaseErrorLookup.LookupError(ex);
            }
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
