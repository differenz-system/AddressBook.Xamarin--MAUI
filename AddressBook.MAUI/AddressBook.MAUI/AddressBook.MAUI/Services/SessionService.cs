using System;
using AddressBook.MAUI.Views;
using Prism.Navigation;

namespace AddressBook.MAUI.Services
{
    public class SessionService
    {
        #region Constructor
        public SessionService()
        {
        }
        #endregion


        #region Private Properties

        #endregion


        #region Public Properties
        public static readonly string EMAIL_REGEX = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public static readonly string PHONE_NO_REGEX = @"\d{10}";

        public static INavigationService navigationService { get; set; }
        #endregion


        #region Private Methods

        #endregion


        #region Public methods
        public static async void AutoLogin()
        {
            try
            {
                SettingsService.IsLoggedIn = true;
                await navigationService.NavigateAsync($"/{nameof(MyListPage)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }

        public static async void Logout()
        {
            try
            {
                SettingsService.IsLoggedIn = false;
                SettingsService.LoggedInUserEmail = string.Empty;
                await navigationService.NavigateAsync($"/{nameof(LoginPage)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}

