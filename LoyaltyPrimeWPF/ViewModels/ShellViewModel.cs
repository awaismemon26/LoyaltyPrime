using Caliburn.Micro;

using LoyaltyPrimeWPF.EventsHelper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoyaltyPrimeWPF.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LoginEvent>
    {
        private DashboardViewModel _dashboardVM;
        private IEventAggregator _events;
        private SimpleContainer _container;
        private LoginViewModel _loginVM;

        public ShellViewModel(LoginViewModel loginVM, DashboardViewModel dashboardVM, IEventAggregator events, SimpleContainer container)
        {
            _events = events;
            _container = container;
            _dashboardVM = dashboardVM;
            _loginVM = loginVM;
            _events.Subscribe(this);

            ActivateItem(_loginVM);
        }

        public void Handle(LoginEvent message)
        {
            ActivateItem(_dashboardVM);
        }
    }
}