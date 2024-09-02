using AddressBook.MAUI.Services;

namespace AddressBook.MAUI.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        SettingsService.IsLoggedIn = false;
        await mainStackView.ScaleTo(1, 250, Easing.Linear);
    }
}
