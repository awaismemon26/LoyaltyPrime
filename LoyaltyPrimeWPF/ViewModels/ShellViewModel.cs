using Caliburn.Micro;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyPrimeWPF.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        //private TodoViewModel _todoVM;
        private IEventAggregator _events;
        private SimpleContainer _container;
        private LoginViewModel _loginVM;

        public ShellViewModel(LoginViewModel loginVM, IEventAggregator events, SimpleContainer container)
        {
            _events = events;
            _container = container;
            _loginVM = loginVM;
            //_events.SubscribeOnUIThread(this);

            ActivateItemAsync(_loginVM);

            //ActivateItemAsync(_container.GetInstance<LoginViewModel>());
        }
    }
}