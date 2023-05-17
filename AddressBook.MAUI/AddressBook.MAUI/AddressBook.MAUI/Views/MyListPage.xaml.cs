using AddressBook.MAUI.ViewModels;

namespace AddressBook.MAUI.Views;

public partial class MyListPage : ContentPage
{
    private MyListPageViewModel ViewModel => BindingContext as MyListPageViewModel;

    public MyListPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await MyList.ScaleTo(1, 250, Easing.Linear);
        ViewModel.OnNavigatedTo();
    }
}
