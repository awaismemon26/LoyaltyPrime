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

        public ShellViewModel(DashboardViewModel dashboardVM, IEventAggregator events, SimpleContainer container)
        {
            _events = events;
            _container = container;
            _dashboardVM = dashboardVM;
            _events.Subscribe(this);

            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public void Handle(LoginEvent message)
        {
            ActivateItem(_dashboardVM);
        }
    }
}