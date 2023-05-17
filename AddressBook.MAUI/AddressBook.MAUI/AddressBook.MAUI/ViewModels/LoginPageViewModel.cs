using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AddressBook.MAUI.Models;
using AddressBook.MAUI.Services;
using AddressBook.MAUI.LanguageResources;

namespace AddressBook.MAUI.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region Constructor
        public LoginPageViewModel()
        {
            CurrentHeader = new HeaderModel();
            CurrentHeader.HeaderText = "Login";
        }
        #endregion

        #region Private Properties
        private string _password;
        private string _email;
        #endregion

        #region Public Properties
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        public static EventHandler OnPresenter;
        #endregion

        #region Commands
        public Command LoginCommand { get { return new Command(() => Login()); } }
        #endregion

        #region Private Methods

        private async void Login()
        {
            try
            {
                if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Password))
                {
                    await DisplayAlertAsync(AppResources.TITLE_ERROR, AppResources.MESSAGE_ERROR_EMAIL_PASSWORD_ERROR, AppResources.TEXT_OK);
                    return;
                }

                if (!(Regex.IsMatch(Email, SessionService.EMAIL_REGEX, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250))))
                {
                    await DisplayAlertAsync(AppResources.TITLE_ERROR, AppResources.MESSAGE_ERROR_INVALID_EMAIL, AppResources.TEXT_OK);
                    return;
                }

                var isConnected = CheckConnectivity();
                if (!isConnected)
                {
                    await DisplayAlertAsync(AppResources.TITLE_ALERT, AppResources.MESSAGE_ERROR_NO_INTERNET, AppResources.TEXT_OK);
                    return;
                }

                await ShowLoader(true);
                LoginModel LoginData = new LoginModel();

                LoginData.Email = Email;
                LoginData.Password = Password;
                LoginData.DeviceOSType = "No Device";
                LoginData.DeviceToken = "";
                LoginData.DeviceUDID = "";

                var LoginUserList = DatabaseService.GetAll();

                var isUserExist = false;

                if (LoginUserList.Count > 0)
                    isUserExist = LoginUserList.Any(x => x.Email == Email);

                if (!isUserExist)
                {
                    var result = await LoginService.Login(LoginData);
                    DatabaseService.SaveLoginDetail(LoginData);

                    if (result != null)
                    {
                        await ClosePopup();
                        SettingsService.LoggedInUserEmail = LoginData.ID.ToString();
                        Debug.WriteLine($"Logged In User : {result.Email}");
                        SessionService.AutoLogin();
                    }
                    else
                    {
                        await ClosePopup();
                        await DisplayAlertAsync(AppResources.TITLE_ERROR, AppResources.MESSAGE_ERROR_EMAIL_PASSWORD_ERROR, AppResources.TEXT_OK);
                    }
                }
                else
                {
                    SettingsService.LoggedInUserEmail = LoginUserList.FirstOrDefault(a => a.Email == Email).ID.ToString();
                    SessionService.AutoLogin();
                }
                await ClosePopup();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("LoginPageViewModel ==> Login \n\n" + ex.Message);
            }
        }

        #endregion

        #region Public methods

        #endregion
    }
}

