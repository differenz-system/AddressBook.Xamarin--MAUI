using Microsoft.Extensions.Logging;
using AddressBook.MAUI.CustomControls;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using CommunityToolkit.Maui;
using AddressBook.MAUI.Views;
using AddressBook.MAUI.ViewModels;
using Prism.Navigation;
using AddressBook.MAUI.Services;
using Microsoft.Maui.LifecycleEvents;
using System.Diagnostics;

namespace AddressBook.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UsePrism(prism =>
            {
                prism.RegisterTypes((containerRegistry) =>
                {
                    containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
                    containerRegistry.RegisterForNavigation<MyListPage, MyListPageViewModel>();
                    containerRegistry.RegisterForNavigation<MyDetailsPage, MyDetailPageViewModel>();
                })
                .CreateWindow(async (container, navigation) =>
                {
                    SessionService.navigationService = navigation;
                    if (SettingsService.IsLoggedIn)
                    {
                        SettingsService.IsLoggedIn = true;
                        var result = await navigation.NavigateAsync($"/{nameof(MyListPage)}");
                    }
                    else
                    {
                        var result = await navigation.NavigateAsync($"/{nameof(LoginPage)}");
                    }
                });
            })
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
#if IOS
            .ConfigureLifecycleEvents(events =>
            {
                events.AddiOS(ios => ios.OnActivated((app) =>
                {
                    var uiStyle = UIKit.UIScreen.MainScreen.TraitCollection.UserInterfaceStyle;
                    if (uiStyle == UIKit.UIUserInterfaceStyle.Light) Application.Current.UserAppTheme = AppTheme.Light;
                    if (uiStyle == UIKit.UIUserInterfaceStyle.Dark) Application.Current.UserAppTheme = AppTheme.Dark;
                }
                ));
            })
#endif
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

