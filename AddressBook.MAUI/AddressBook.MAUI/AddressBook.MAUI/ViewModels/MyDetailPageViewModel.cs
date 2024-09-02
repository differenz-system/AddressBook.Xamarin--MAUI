using System;
using AddressBook.MAUI.Models;
using AddressBook.MAUI.LanguageResources;
using System.Text.RegularExpressions;
using AddressBook.MAUI.Services;
using System.Diagnostics;
using Prism.Navigation;
using AddressBook.MAUI.Views;

namespace AddressBook.MAUI.ViewModels
{
    public class MyDetailPageViewModel : BaseViewModel
    {
        #region Constructor
        public MyDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            CurrentHeader = new HeaderModel();
            CurrentHeader.HeaderText = AppResources.TEXT_DETAIL;
            CurrentHeader.LeftText = "book-open";
            CurrentHeader.RightText = "sign-out-alt";
            CurrentHeader.LeftCommand = BackCommand;
            CurrentHeader.RightCommand = LogoutCommand;
        }
        #endregion

        #region Private PropertiesS
        private int _id = 0;
        private string _name;
        private string _emailAddress;
        private string _contactNumber;
        private bool _active;
        private string _saveButtonText = AppResources.TEXT_SAVE;
        private string _deleteButtonText = AppResources.TEXT_CANCEL;
        #endregion

        #region Public Properties
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string EmailAddress
        {
            get { return _emailAddress; }
            set { SetProperty(ref _emailAddress, value); }
        }

        public string ContactNumber
        {
            get { return _contactNumber; }
            set { SetProperty(ref _contactNumber, value); }
        }

        public bool Active
        {
            get { return _active; }
            set { SetProperty(ref _active, value); }
        }

        public string SaveButtonText
        {
            get { return _saveButtonText; }
            set { SetProperty(ref _saveButtonText, value); }
        }

        public string DeleteButtonText
        {
            get { return _deleteButtonText; }
            set { SetProperty(ref _deleteButtonText, value); }
        }

        #endregion

        #region Commands

        public DelegateCommand SaveCommand { get { return new DelegateCommand(() => Save()); } }
        public DelegateCommand DeleteCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (Id > 0)
                    {
                        Delete();
                    }
                    else
                    {
                        Cancel();
                    }
                });
            }
        }
        public DelegateCommand BackCommand { get { return new DelegateCommand(() => Cancel()); } }

        #endregion

        #region Private Methods
        public async void Save()
        {
            try
            {
                if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(EmailAddress) && !string.IsNullOrEmpty(ContactNumber))
                {
                    if (!(Regex.IsMatch(EmailAddress, SessionService.EMAIL_REGEX, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250))))
                    {
                        await DisplayAlertAsync(AppResources.TITLE_ERROR, AppResources.MESSAGE_ERROR_INVALID_EMAIL, AppResources.TEXT_OK);
                        return;
                    }

                    if (!(Regex.IsMatch(ContactNumber, SessionService.PHONE_NO_REGEX, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250))))
                    {
                        await DisplayAlertAsync(AppResources.TITLE_ERROR, AppResources.MESSAGE_ERROR_INVALID_CONTACT_NO, AppResources.TEXT_OK);
                        return;
                    }

                    await ShowLoader(true);
                    var userData = new UserData();
                    userData.ID = Id;
                    userData.Name = Name;
                    userData.EmailAddress = EmailAddress;
                    userData.ContactNumber = ContactNumber;
                    userData.Active = Active;
                    userData.LoginID = SettingsService.LoggedInUserEmail;

                    DatabaseService.SaveItem(userData);
                    await ClosePopup();
                    await DisplayAlertAsync(AppResources.TITLE_SUCCESS, SaveButtonText == AppResources.TEXT_SAVE ? AppResources.MESSAGE_SUCCESS_DATA_SAVE : AppResources.MESSAGE_SUCCESS_DATA_UPDATED, AppResources.TEXT_OK);
                    await navigationService.NavigateAsync($"/{nameof(MyListPage)}");
                }
                else
                {
                    await DisplayAlertAsync(AppResources.TITLE_VALIDATION_ERROR, AppResources.MESSAGE_ERROR_INSERT_ALL_DATA, AppResources.TEXT_OK);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MyDetailPageViewModel ==> Save \n\n" + ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        async void Delete()
        {
            try
            {
                if (Id != 0)
                {
                    await ShowLoader(true);
                    DatabaseService.DeleteItem(Id);
                    await ClosePopup();
                    await navigationService.NavigateAsync($"/{nameof(MyListPage)}");
                }
            }
            catch (Exception ex)
            {
                await ClosePopup();
                Console.WriteLine("MyDetailPageViewModel ==> Delete \n\n" + ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        async void Cancel()
        {
            try
            {
                await navigationService.NavigateAsync($"/{nameof(MyListPage)}");
            }
            catch (Exception ex)
            {
                await ClosePopup();
                Console.WriteLine("MyDetailPageViewModel ==> Cancel \n\n" + ex.Message);
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
                if (parameters != null && parameters.ContainsKey("userdata"))
                {
                    UserData userData = (UserData)parameters["userdata"];
                    if (userData != null)
                    {
                        Name = userData.Name;
                        Id = userData.ID;
                        Active = userData.Active;
                        ContactNumber = userData.ContactNumber;
                        EmailAddress = userData.EmailAddress;
                    }
                }

                SaveButtonText = Id == 0 ? AppResources.TEXT_SAVE : AppResources.TEXT_UPDATE;
                DeleteButtonText = Id == 0 ? AppResources.TEXT_CANCEL : AppResources.TEXT_DELETE;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}

