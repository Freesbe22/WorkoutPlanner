using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Firebase.Auth;

#region Region
namespace WorkoutPlanner.Tools.Auth
{
    public class StateProvider : AuthenticationStateProvider
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;

        public StateProvider(FirebaseAuthClient firebaseAuthClient)
        {
            _firebaseAuthClient = firebaseAuthClient;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                if (_firebaseAuthClient.User != null)
                {
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, _firebaseAuthClient.User.Info.Email),
                            //new Claim(ClaimTypes.Role, CurrentUser.Role)
                        };
                    identity = new ClaimsIdentity(claims, "authentication");
                    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }
            AuthenticationState authState = new AuthenticationState(new ClaimsPrincipal(identity));
            return authState;
        }

        public void ManageUser()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
#endregion