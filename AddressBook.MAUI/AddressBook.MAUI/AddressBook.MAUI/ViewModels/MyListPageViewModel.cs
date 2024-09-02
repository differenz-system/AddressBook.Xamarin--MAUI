using System;
using AddressBook.MAUI.Models;
using AddressBook.MAUI.LanguageResources;
using System.Collections.ObjectModel;
using AddressBook.MAUI.Services;
using System.Diagnostics;
using AddressBook.MAUI.Views;
using Prism.Navigation;

namespace AddressBook.MAUI.ViewModels
{
    public class MyListPageViewModel : BaseViewModel
    {
        #region Constructor
        public MyListPageViewModel(INavigationService navigationService) : base(navigationService)
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

        public DelegateCommand<UserData> DeleteItemCommand { get { return new DelegateCommand<UserData>((obj) => DeleteAddress(obj)); } }
        public DelegateCommand<UserData> SelectItemCommand { get { return new DelegateCommand<UserData>((obj) => ViewAddress(obj)); } }
        public DelegateCommand AddCommand { get { return new DelegateCommand(Add); } }

        public DelegateCommand<string> SearchContactCommand
        {
            get { return new DelegateCommand<string>(async (obj) => await DelayedQueryForKeyboardTypingSearches(obj).ConfigureAwait(false)); }
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                Console.WriteLine("MyListPageViewModel ==> SearchContact \n\n" + ex.Message);
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
                Console.WriteLine("MyListPageViewModel ==> DeleteAddress \n\n" + ex.Message);
            }
        }

        async void ViewAddress(UserData userData)
        {
            try
            {
                var param = new NavigationParameters();
                param.Add("userdata", userData);
                await navigationService.NavigateAsync($"/{nameof(MyDetailsPage)}", param);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async void Add()
        {
            try
            {
                await navigationService.NavigateAsync($"/{nameof(MyDetailsPage)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("MyListPageViewModel ==> Add \n\n" + ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Public methods

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
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
                Console.WriteLine("MyListPageViewModel ==> OnNavigatedTo \n\n" + ex.Message);
            }
        }
        #endregion
    }
}

