using System;
using AddressBook.MAUI.Models;
using AddressBook.MAUI.LanguageResources;
using System.Text.RegularExpressions;
using AddressBook.MAUI.Services;
using System.Diagnostics;

namespace AddressBook.MAUI.ViewModels
{
    public class MyDetailPageViewModel : BaseViewModel
    {
        #region Constructor
        public MyDetailPageViewModel()
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

        public Command SaveCommand { get { return new Command(() => Save()); } }
        public Command DeleteCommand
        {
            get
            {
                return new Command(() =>
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
        public Command BackCommand { get { return new Command(() => Cancel()); } }

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
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlertAsync(AppResources.TITLE_VALIDATION_ERROR, AppResources.MESSAGE_ERROR_INSERT_ALL_DATA, AppResources.TEXT_OK);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MyDetailPageViewModel ==> Save \n\n" + ex.Message);
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
                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await ClosePopup();
                Debug.WriteLine("MyDetailPageViewModel ==> Delete \n\n" + ex.Message);
            }
        }

        async void Cancel()
        {
            try
            {
                await App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await ClosePopup();
                Debug.WriteLine("MyDetailPageViewModel ==> Cancel \n\n" + ex.Message);
            }
        }
        #endregion

        #region Public methods

        #endregion
    }
}

