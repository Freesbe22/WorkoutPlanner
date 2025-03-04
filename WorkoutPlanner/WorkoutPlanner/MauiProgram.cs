using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
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
        //builder.Services.AddSingleton(new FirebaseClient("https://thedreamlife-workoutplanner-default-rtdb.europe-west1.firebasedatabase.app/"));
        builder.Services.AddSingleton<FirestoreService>();
        builder.Services.AddBootstrapBlazor();

        return builder.Build();
	}
}
