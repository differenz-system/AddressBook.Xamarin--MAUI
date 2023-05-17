using AddressBook.MAUI.Services;

namespace AddressBook.MAUI;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        if (SettingsService.IsLoggedIn)
        {
            SessionService.AutoLogin();
        }
        else
        {
            MainPage = new Views.LoginPage();
        }
    }
}

