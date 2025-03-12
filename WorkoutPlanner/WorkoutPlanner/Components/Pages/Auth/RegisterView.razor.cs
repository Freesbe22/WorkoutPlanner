using Firebase.Auth;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using WorkoutPlanner.DataObject;
using WorkoutPlanner.Tools;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkoutPlanner.Components.Pages.Auth
{
    partial class RegisterView : ComponentBase
    {
        #region Variables
        [Inject]
        public FirebaseAuthClient AuthClient { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public StateProvider AuthStateProvider { get; set; }
        private RegisterViewModel RegisterModel { get; set; } = new RegisterViewModel();
        private bool Initialised { get; set; } = false;
        private string Error { get; set; }
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
        private async Task Register()
        {
            try
            {
                await AuthClient.CreateUserWithEmailAndPasswordAsync(RegisterModel.Email, RegisterModel.Password);
                CurrentUserCheck();
            }
            catch (FirebaseAuthHttpException ex)
            {
                Error = ex.Reason.ToString();
            }
        }

        private void CurrentUserCheck()
        {
            if (AuthClient.User != null)
            {
                AuthStateProvider.ManageUser();
                NavManager.NavigateTo("/workout");
            }
        }

        #region Model
        public class RegisterViewModel
        {
            [Required, EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(255, ErrorMessage = "Must be between 6 and 255 characters", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [StringLength(255, ErrorMessage = "Must be between 6 and 255 characters", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Compare(nameof(Password))]
            public string PasswordConfirm { get; set; }
        }
        #endregion
    }
}
