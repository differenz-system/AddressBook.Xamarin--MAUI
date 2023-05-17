using System;
using Mopups.Interfaces;
using Mopups.Services;

namespace AddressBook.MAUI.Services
{
	public class PopupNavigationService
	{
		public PopupNavigationService()
		{
		}
        static IPopupNavigation _instance;

        public static IPopupNavigation Instance
        {
            get
            {
                return _instance ?? (_instance = MopupService.Instance);
            }
            set
            {
                _instance = value;
            }
        }
    }
}

