using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using WorkoutPlanner.Tools;
using WorkoutPlanner.Tools.Services;

namespace WorkoutPlanner;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<FirestoreService>();
        builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyBBQEm6Bu8zlN02L7aJLzWJi7nvmkTg9r8",
            AuthDomain = "thedreamlife-workoutplanner.firebaseapp.com",
            Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
            {
                new EmailProvider()
            },
            UserRepository = new FileUserRepository("thedreamlife-workoutplanner")
        }));
        builder.Services.AddScoped<StateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<StateProvider>());
        builder.Services.AddAuthorizationCore();
        builder.Services.AddBootstrapBlazor();

        return builder.Build();
	}
}
