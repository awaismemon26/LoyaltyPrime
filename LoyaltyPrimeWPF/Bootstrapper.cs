using Caliburn.Micro;

using LoyaltyPrimeUI.Library.Api;
using LoyaltyPrimeUI.Library.Models;

using LoyaltyPrimeWPF.Helpers;
using LoyaltyPrimeWPF.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LoyaltyPrimeWPF
{
    public class Bootstrapper : BootstrapperBase
    {
        // Dependency Injection --> Handle the instantiation of all of our classes. Do most of the work of instantiation and container will do it for us.
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
            PasswordBoxHelper.BoundPasswordProperty,
            "Password",
            "PasswordChanged");
        }
        //where does container know what to connect to what
        protected override void Configure()
        {
            _container.Instance(_container)
                .PerRequest<IMemberEndPoint, MemberEndPoint>();

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IAPIHelper, APIHelper>()
                .Singleton<ILoggedInUserModel, LoggedInUserModel>();

            MessageBinder.SpecialValues.Add("$pressedkey", (context) =>
            {
                var keyArgs = context.EventArgs as KeyEventArgs;

                if (keyArgs != null)
                    return keyArgs.Key;

                return null;
            });

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
