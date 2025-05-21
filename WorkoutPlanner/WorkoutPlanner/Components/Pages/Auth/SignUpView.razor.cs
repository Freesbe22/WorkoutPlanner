using BootstrapBlazor.Components;
using Firebase.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using WorkoutPlanner.Tools.Auth;

namespace WorkoutPlanner.Components.Pages.Auth
{
    partial class SignUpView : ComponentBase
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
        private RegisterViewModel RegisterModel { get; set; } = new RegisterViewModel();

        private EditContext _editContext;
        private ValidationMessageStore _messageStore;

        #endregion

        #region Loading

        protected override async Task OnInitializedAsync()
        {
            CurrentUserCheck();
            _editContext = new EditContext(RegisterModel);
            _messageStore = new ValidationMessageStore(_editContext);
            await InvokeAsync(() => { StateHasChanged(); });

            await base.OnInitializedAsync();
        }

        #endregion

        private async Task Register()
        {
            try
            {
                await AuthClient.CreateUserWithEmailAndPasswordAsync(RegisterModel.Email, RegisterModel.Password);
                CurrentUserCheck();
            }
            catch (FirebaseAuthHttpException ex)
            {
                _messageStore.Add(new FieldIdentifier(RegisterModel, string.Empty), "Une erreur est survenue : " + FirebaseErrorLookup.LookupError(ex));
                _editContext.NotifyValidationStateChanged();
            }
        }

        private void CurrentUserCheck()
        {
            if (AuthClient.User != null)
            {
                AuthStateProvider.ManageUser();
                NavManager.NavigateTo("/accountcreate");
            }
        }

        #region Model

        public class RegisterViewModel
        {
            [Required(ErrorMessageResourceName = nameof(AppResources.fui_required_field), ErrorMessageResourceType = typeof(AppResources))]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessageResourceName = nameof(AppResources.fui_required_field), ErrorMessageResourceType = typeof(AppResources))]
            [StringLength(255, ErrorMessage = "Must be between 6 and 255 characters", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessageResourceName = nameof(AppResources.fui_required_field), ErrorMessageResourceType = typeof(AppResources))]
            [StringLength(255, ErrorMessage = "Must be between 6 and 255 characters", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Compare(nameof(Password))]
            public string PasswordConfirm { get; set; }
        }

        #endregion
    }
}
