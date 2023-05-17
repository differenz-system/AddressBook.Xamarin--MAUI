using System;
using AddressBook.MAUI.Models;
using AddressBook.MAUI.LanguageResources;
using System.Collections.ObjectModel;
using AddressBook.MAUI.Services;
using System.Diagnostics;
using AddressBook.MAUI.Views;

namespace AddressBook.MAUI.ViewModels
{
    public class MyListPageViewModel : BaseViewModel
    {
        #region Constructor
        public MyListPageViewModel()
        {
            CurrentHeader = new HeaderModel();
            CurrentHeader.HeaderText = AppResources.Text_ADDRESS_BOOK;
            CurrentHeader.LeftText = "sign-out-alt";
            CurrentHeader.RightText = "plus";
            CurrentHeader.LeftCommand = LogoutCommand;
            CurrentHeader.RightCommand = AddCommand;
            CurrentHeader.IsVisiblesearchEntry = true;
            CurrentHeader.SearchContactCommand = SearchContactCommand;
        }
        #endregion

        #region Private Properties
        private List<UserData> _userList;
        private ObservableCollection<object> _items = new ObservableCollection<object>();
        private CancellationTokenSource throttleCts = new CancellationTokenSource();
        private string _searchText;
        private bool isVisibleMessage;
        #endregion

        #region Public Properties
        public List<UserData> UserList
        {
            get { return _userList; }
            set { SetProperty(ref _userList, value); }
        }

        public ObservableCollection<object> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public bool IsVisibleMessage
        {
            get { return isVisibleMessage; }
            set { SetProperty(ref isVisibleMessage, value); }
        }

        #endregion

        #region Commands

        public Command<UserData> DeleteItemCommand { get { return new Command<UserData>((obj) => DeleteAddress(obj)); } }
        public Command<UserData> SelectItemCommand { get { return new Command<UserData>((obj) => ViewAddress(obj)); } }
        public Command AddCommand { get { return new Command(Add); } }

        public Command<string> SearchContactCommand
        {
            get { return new Command<string>(async (obj) => await DelayedQueryForKeyboardTypingSearches(obj).ConfigureAwait(false)); }
        }
        #endregion

        #region Private Methods

        private async Task DelayedQueryForKeyboardTypingSearches(string searchtxt)
        {
            try
            {
                Interlocked.Exchange(ref this.throttleCts, new CancellationTokenSource()).Cancel(); await Task.Delay(TimeSpan.FromMilliseconds(500), this.throttleCts.Token)
.ContinueWith(task => SearchContact(searchtxt, this.throttleCts.Token), CancellationToken.None,
             TaskContinuationOptions.OnlyOnRanToCompletion,
             TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch
            {
            }
        }

        private void SearchContact(string searchtxt, CancellationToken token)
        {
            try
            {
                UserList = DatabaseService.GetAllItem(SettingsService.LoggedInUserEmail);
                if (searchtxt.Count() > 0)
                {
                    var sorted = UserList.Where(c => c.Name.ToLower().Contains(searchtxt.ToLower()))
                        .OrderBy(item => item.Name)
                        .GroupBy(item => item.Name[0].ToString())
                        .Select(itemGroup => new Grouping<string, UserData>(itemGroup.Key.ToUpper(), itemGroup))
                        .ToList();
                    Items = new ObservableCollection<object>(sorted);
                }
                else
                {
                    var sorted = UserList
                       .OrderBy(item => item.Name)
                       .GroupBy(item => item.Name[0].ToString())
                       .Select(itemGroup => new Grouping<string, UserData>(itemGroup.Key.ToUpper(), itemGroup))
                       .ToList();
                    Items = new ObservableCollection<object>(sorted);
                }

                IsVisibleMessage = Items.Count > 0 ? false : true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MyListPageViewModel ==> SearchContact \n\n" + ex.Message);
            }
        }

        private async void DeleteAddress(UserData obj)
        {
            try
            {
                if (obj != null)
                {
                    await ShowLoader(true);
                    DatabaseService.DeleteItem(obj.ID);
                    await ClosePopup();
                    UserList = DatabaseService.GetAllItem(SettingsService.LoggedInUserEmail);
                    var sorted = UserList.OrderBy(item => item.Name).GroupBy(item => item.Name[0].ToString())
                       .Select(itemGroup => new Grouping<string, UserData>(itemGroup.Key.ToUpper(), itemGroup)).ToList();
                    Items = new ObservableCollection<object>(sorted);
                    IsVisibleMessage = Items.Count > 0 ? false : true;
                }
            }
            catch (Exception ex)
            {
                await ClosePopup();
                Debug.WriteLine("MyListPageViewModel ==> DeleteAddress \n\n" + ex.Message);
            }
        }

        async void ViewAddress(UserData userData)
        {
            UserData data = new UserData();
            data.ID = userData.ID;
            data.EmailAddress = userData.EmailAddress;
            data.ContactNumber = userData.ContactNumber;
            data.Name = userData.Name;
            data.Active = userData.Active;

            await App.Current.MainPage.Navigation.PushAsync(new MyDetailsPage
            {
                BindingContext = new MyDetailPageViewModel
                {
                    Id = userData.ID,
                    EmailAddress = userData.EmailAddress,
                    Name = userData.Name,
                    Active = userData.Active,
                    ContactNumber = userData.ContactNumber,
                    SaveButtonText = userData.ID == 0 ? AppResources.TEXT_SAVE : AppResources.TEXT_UPDATE,
                    DeleteButtonText = userData.ID == 0 ? AppResources.TEXT_CANCEL : AppResources.TEXT_DELETE
                }
            });
        }

        async void Add()
        {
            try
            {
                await App.Current.MainPage.Navigation.PushAsync(new MyDetailsPage());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MyListPageViewModel ==> Add \n\n" + ex.Message);
            }
        }
        #endregion

        #region Public methods

        public void OnNavigatedTo()
        {
            try
            {
                UserList = DatabaseService.GetAllItem(SettingsService.LoggedInUserEmail);

                var sorted = UserList
                .OrderBy(item => item.Name)
                .GroupBy(item => item.Name[0].ToString())
                .Select(itemGroup => new Grouping<string, UserData>(itemGroup.Key.ToUpper(), itemGroup))
                .ToList();

                Items = new ObservableCollection<object>(sorted);
                IsVisibleMessage = Items.Count > 0 ? false : true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MyListPageViewModel ==> OnNavigatedTo \n\n" + ex.Message);
            }
        }
        #endregion
    }
}

