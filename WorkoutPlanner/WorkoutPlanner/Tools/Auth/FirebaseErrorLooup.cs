using Firebase.Auth;

namespace WorkoutPlanner.Tools.Auth
{
    public static class FirebaseErrorLookup
    {
        public static string LookupError(FirebaseAuthException exception)
        {
            return LookupError(exception.Reason);
        }

        public static string LookupError(AuthErrorReason reason)
        {
            switch (reason)
            {
                case AuthErrorReason.UserNotFound:
                    return AppResources.fui_error_email_does_not_exist;
                case AuthErrorReason.InvalidEmailAddress:
                    return AppResources.fui_invalid_email_address;
                case AuthErrorReason.MissingPassword:
                    return AppResources.fui_error_invalid_password;
                case AuthErrorReason.WeakPassword:
                    return AppResources.fui_error_weak_password_other;
                case AuthErrorReason.EmailExists:
                    return AppResources.fui_email_account_creation_error;
                case AuthErrorReason.MissingEmail:
                    return AppResources.fui_missing_email_address;
                case AuthErrorReason.UnknownEmailAddress:
                    return AppResources.fui_invalid_email_address;
                case AuthErrorReason.WrongPassword:
                    return AppResources.fui_error_invalid_password;
                case AuthErrorReason.Undefined:
                    return AppResources.fui_no_internet;
                case AuthErrorReason.Unknown:
                    return AppResources.fui_error_unknown;
                default:
                    return $"{AppResources.fui_error_unknown} {reason}";
            }
        }
    }
}
