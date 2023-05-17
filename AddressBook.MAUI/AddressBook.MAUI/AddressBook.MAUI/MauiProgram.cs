using Microsoft.Extensions.Logging;
using AddressBook.MAUI.CustomControls;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using CommunityToolkit.Maui;

namespace AddressBook.MAUI;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                fonts.AddFont("MaterialIcons.ttf", "MaterialIcons");
                fonts.AddFont("Font_Awesome_Free_Regular.otf", "FA_Regular");
                fonts.AddFont("Font_Awesome_Free_Solid.otf", "FA_Solid");
            })
            .UseMauiCompatibility()
            .UseMauiCommunityToolkit()
            .ConfigureMauiHandlers((handlers) =>
            {
#if ANDROID
                handlers.AddCompatibilityRenderer(typeof(CustomEntry), typeof(AddressBook.MAUI.Platforms.Android.Renderers.CustomEntryRenderer));
#endif
#if IOS
        				handlers.AddCompatibilityRenderer(typeof(CustomEntry), typeof(AddressBook.MAUI.Platforms.iOS.Renderers.CustomEntryRenderer));
#endif
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

