using Caliburn.Micro;
using Microsoft.Practices.Unity;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAD.AddressBook.HelpClasses;
using RAD.AddressBook.ViewModels;

namespace RAD.AddressBook
{
    public class AppBootstrapper : BootstrapperBase
    {
        private readonly IUnityContainer _container;

        public AppBootstrapper()
        {
            _container = new UnityContainer();

            _container.RegisterType<IWindowManager, WindowManager>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());

            _container.RegisterType<IExceptionCommunicator, ExceptionCommunicator>();
            _container.RegisterInstance(new EventAggregator());
            _container.RegisterInstance(new EnvironmentSettings());
            Initialize();
        }


        protected override object GetInstance(Type service, string key)
        {
            return _container.Resolve(service);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            DisplayRootViewFor<ShellViewModel>();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Exception occured in: " + sender + e);
        }
    }
}
