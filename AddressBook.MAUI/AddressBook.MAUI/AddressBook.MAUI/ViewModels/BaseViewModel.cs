using System;
using System.Diagnostics;
using AddressBook.MAUI.Models;
using AddressBook.MAUI.Services;
using AddressBook.MAUI.Views.Popups;
using Mopups.Pages;
using Prism.Mvvm;
using Prism.Navigation;

namespace AddressBook.MAUI.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        #region Constructor
        public BaseViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        #endregion

        #region Private Properties
        private HeaderModel _currentHeader = new HeaderModel();
        #endregion

        #region Public Properties

        public INavigationService navigationService { get; set; }

        public HeaderModel CurrentHeader
        {
            get { return _currentHeader; }
            set { SetProperty(ref _currentHeader, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand LogoutCommand { get { return new DelegateCommand(() => SessionService.Logout()); } }
        #endregion

        #region Private Methods

        #endregion

        #region Public methods

        public static async Task ShowPopup(PopupPage popup, bool animate = false)
        {
            try
            {
                await PopupNavigationService.Instance.PushAsync(popup, animate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("BindableBase ==> ShowPopup \n\n" + ex.Message);
            }
        }

        public static async Task ShowLoader(bool animate = false)
        {
            try
            {
                var doesPopupExists = PopupNavigationService.Instance.PopupStack.Any(p => p is LoadingPopup);
                if (doesPopupExists) return;

                await ShowPopup(new LoadingPopup(), animate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("BindableBase ==> ShowLoader \n\n" + ex.Message);
            }
        }

        public static async Task ClosePopup(bool animate = false)
        {
            try
            {
                if (PopupNavigationService.Instance.PopupStack != null && PopupNavigationService.Instance.PopupStack.Count > 0)
                {
                    await PopupNavigationService.Instance.PopAsync(animate);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("BindableBase ==> ClosePopup \n\n" + ex.Message);
            }
        }

        public static async Task DisplayAlertAsync(string title, string message, string buttonText)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    await App.Current.MainPage.DisplayAlert(title, message, buttonText);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("BindableBase ==> DisplayAlertAsync \n\n" + ex.Message);
            }
        }

        public bool CheckConnectivity()
        {
            try
            {
                return Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
            }
            catch (Exception ex)
            {
                Console.WriteLine("BindableBase ==> CheckConnectivity \n\n" + ex.Message);
                return false;
            }
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }

        #endregion
    }
}

