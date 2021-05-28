using Caliburn.Micro;

using LoyaltyPrimeUI.Library.Api;

using LoyaltyPrimeWPF.EventsHelper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyPrimeWPF.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName = "";
        private string _password;
        private IAPIHelper _apiHelper;
        private string _errorMessage;
        private IEventAggregator _events;

        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
            IsProgressBarVisible = false;
        }

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;
                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output;
            }

        }

        private bool _isProgressBarVisible;

        public bool IsProgressBarVisible
        {
            get { return _isProgressBarVisible; }
            set
            {
                _isProgressBarVisible = value;
                NotifyOfPropertyChange(() => IsProgressBarVisible);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }


        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);

            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool CanLogIn
        {
            get
            {
                bool output = false;

                if (UserName?.Length > 0 && Password?.Length > 0 && IsProgressBarVisible == false)
                {
                    output = true;
                }
                return output;
            }
        }

        public async Task LogIn()
        {
            try
            {
                ErrorMessage = "";
                IsProgressBarVisible = true;

                var result = await _apiHelper.Authenticate(UserName, Password);

                IsProgressBarVisible = false;

                //Capture more information about the user
                await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

                _events.PublishOnUIThread(new LoginEvent());
            }
            catch (System.Exception ex)
            {
                ErrorMessage = ex.Message;
                IsProgressBarVisible = false;
            }
        }

    }
}
