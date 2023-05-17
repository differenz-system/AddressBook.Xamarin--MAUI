using System;
using AddressBook.MAUI.ViewModels;

namespace AddressBook.MAUI.Models
{
    public class HeaderModel : BindableBase
    {
        #region Private Properties
        private string _headerText = "";

        private string _leftText = "";

        private string _rightText = "";

        private Command _leftCommand = null;

        private Command _rightCommand = null;

        private Command<string> _searchContactCommand = null;

        private bool _isVisibleSearchEntry = false;

        private string _searchText;

        #endregion

        #region Public Properties
        public string HeaderText
        {
            get { return _headerText; }
            set { SetProperty(ref _headerText, value); }
        }

        public string LeftText
        {
            get { return _leftText; }
            set { SetProperty(ref _leftText, value); }
        }

        public string RightText
        {
            get { return _rightText; }
            set { SetProperty(ref _rightText, value); }
        }

        public Command LeftCommand
        {
            get { return _leftCommand; }
            set { SetProperty(ref _leftCommand, value); }
        }

        public Command RightCommand
        {
            get { return _rightCommand; }
            set { SetProperty(ref _rightCommand, value); }
        }

        public bool IsVisiblesearchEntry
        {
            get { return _isVisibleSearchEntry; }
            set { SetProperty(ref _isVisibleSearchEntry, value); }
        }

        public Command<string> SearchContactCommand
        {
            get { return _searchContactCommand; }
            set { SetProperty(ref _searchContactCommand, value); }
        }

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }
        #endregion
    }
}

