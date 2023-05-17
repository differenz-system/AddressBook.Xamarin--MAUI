namespace AddressBook.MAUI.Views;

public partial class MyDetailsPage : ContentPage
{
	public MyDetailsPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await MainStackLayout.ScaleTo(1, 250, Easing.Linear);
    }
}
